using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace WpfAsyncDownload
{
    public partial class MainWindow : Window
    {
        private DownloadResult _downloadResult;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void ButtonDownload_OnClick(object sender, RoutedEventArgs e)
        {
            ButtonDownload.IsEnabled = false;

            TextBoxResults.Clear();
            var downloader = new Downloader();
            
            var cancellationTokenSource = new CancellationTokenSource();
            ButtonCancel.Click += (snd, ev) => cancellationTokenSource.Cancel();

            _downloadResult = new DownloadResult();

            var settings = new DownloadSetting
                {
                    BaseUrl = TextBoxUrl.Text,
                    StartIndex = (int) ComboBoxIndexFrom.SelectedValue,
                    EndIndex = (int) ComboBoxIndexTo.SelectedValue,
                    NameFormat = ComboBoxNameFormat.SelectedValue.ToString(),
                    Prefix = TextBoxPrefix.Text,
                    Suffix = TextBoxSuffix.Text,
                    Extension = ComboBoxExtension.SelectedValue.ToString()
                };

            try
            {
                _downloadResult = await downloader.DownloadPagesAsync(settings, cancellationTokenSource.Token);
                DisplayResults();
            }
            catch (Exception exception)
            {
                TextBoxResults.Text = exception.Message;
            }
            finally
			{
				ButtonDownload.IsEnabled = true;

                if (_downloadResult.IsError)
                {
                    TextBoxResults.Text += string.Join("\r\n", _downloadResult.AggregateException.InnerExceptions.Select(x => x.Message));
                }
			}
        }

        private void DisplayResults()
        {
            foreach (var response in _downloadResult.Responses)
            {
                var displayUrl = response.Url.Replace("http://", "");
                TextBoxResults.Text += string.Format("\n{0,-58} {1,8}", displayUrl, response.HttpResponseMessage.StatusCode);
            }
        }

        private async void ButtonSave_OnClick(object sender, RoutedEventArgs e)
        {
            ButtonSave.IsEnabled = false;

            Directory.CreateDirectory("images");

            foreach (UrlResponse responseMessage in _downloadResult.Responses.Where(r => r.HttpResponseMessage.IsSuccessStatusCode))
            {
                string fileName = "images/" + Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + "-" + responseMessage.Url.Substring(responseMessage.Url.LastIndexOf("/", System.StringComparison.Ordinal) + 1);

                using (Stream contentStream = await responseMessage.HttpResponseMessage.Content.ReadAsStreamAsync(),
                    stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None, 1000000, useAsync:true))
                {
                    await contentStream.CopyToAsync(stream);
                }
            }

            _downloadResult = new DownloadResult();
            ButtonSave.IsEnabled = true;
        }

        private void ComboBoxIndexFrom_OnLoaded(object sender, RoutedEventArgs e)
        {
            ComboBoxIndexFrom.ItemsSource = Enumerable.Range(0, 20);
            ComboBoxIndexFrom.SelectedIndex = 0;
        }

        private void ComboBoxIndexTo_OnLoaded(object sender, RoutedEventArgs e)
        {
            ComboBoxIndexTo.ItemsSource = Enumerable.Range(0, 21);
            ComboBoxIndexTo.SelectedIndex = 20;
        }

        private void ComboBoxNameFormat_OnLoaded(object sender, RoutedEventArgs e)
        {
            ComboBoxNameFormat.ItemsSource = new List<string> { "0", "00", "000", "0000" };
            ComboBoxNameFormat.SelectedIndex = 0;
        }

        private void ComboBoxExtension_OnLoaded(object sender, RoutedEventArgs e)
        {
            ComboBoxExtension.ItemsSource = new List<string> {".jpg", ".jpeg", ".png"};
            ComboBoxExtension.SelectedIndex = 0;
        }
    }
}
