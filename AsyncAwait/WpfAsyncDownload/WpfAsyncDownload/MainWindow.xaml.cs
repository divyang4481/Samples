using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private string _imagesDirectory = "images";
        private DownloadResult _downloadResult;
        private readonly IOutputFileNameCreator _outputFileNameCreator = new OutputFileNameCreator();

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
            ButtonCancel.Click += (snd, ev) => 
            { 
                cancellationTokenSource.Cancel();
                TextBoxResults.Text = "Cancelled";
            };

            _downloadResult = new DownloadResult();

            int folderIndexFrom;
            int.TryParse(TextBoxFolderIndexFrom.Text, out folderIndexFrom);

            int folderIndexTo;
            int.TryParse(TextBoxFolderIndexTo.Text, out folderIndexTo);

            int indexFrom;
            int.TryParse(TextBoxIndexFrom.Text, out indexFrom);

            int indexTo;
            int.TryParse(TextBoxIndexTo.Text, out indexTo);

            var settings = new DownloadSettings
                {
                    Url = TextBoxUrl.Text,
                    StartIndex = indexFrom,
                    EndIndex = indexTo,
                    FolderStartIndex =  folderIndexFrom,
                    FolderEndIndex = folderIndexTo,
                    FolderNameFormat = ComboBoxFolderNameFormat.SelectedValue.ToString(),
                    NameFormat = ComboBoxNameFormat.SelectedValue.ToString(),
                    Prefix = TextBoxPrefix.Text,
                    Suffix = TextBoxSuffix.Text,
                    Extension = ComboBoxExtension.SelectedValue.ToString()
                };

            if (ComboBoxAlgorithm.SelectedIndex == 1)
            {
                downloader.UrlListCreator = new FolderNumberedUrlListCreator();
            }

            try
            {
                _downloadResult = await downloader.DownloadImagesAsync(settings, cancellationTokenSource.Token);
                DisplayResults();
            }
            catch (Exception exception)
            {
                TextBoxResults.Text = exception.Message;
            }
            finally
			{
				ButtonDownload.IsEnabled = true;

                if (_downloadResult.IsError && _downloadResult.AggregateException != null)
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

            foreach (UrlResponse responseMessage in _downloadResult.Responses.Where(r => r.HttpResponseMessage.IsSuccessStatusCode))
            {
                string fileName = _imagesDirectory + "/" + _outputFileNameCreator.Create(responseMessage);

                using (Stream contentStream = await responseMessage.HttpResponseMessage.Content.ReadAsStreamAsync(),
                    stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None, 1000000, useAsync:true))
                {
                    await contentStream.CopyToAsync(stream);
                }
            }

            _downloadResult = new DownloadResult();
            TextBoxResults.Text = "Saved";
            ButtonSave.IsEnabled = true;
        }

        private void ButtonOpenFileBrowser_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start(AppDomain.CurrentDomain.BaseDirectory + _imagesDirectory);
        }

        private void ButtonOutputFolder_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
            {
                _imagesDirectory = dialog.SelectedPath;
                TextBoxResults.Text = string.Format("{0} selected", _imagesDirectory);
            }
        }

        private void ComboBoxNameFormat_OnLoaded(object sender, RoutedEventArgs e)
        {
            var comboBox = (ComboBox)sender;

            comboBox.ItemsSource = new List<string> { "0", "00", "000", "0000", "00000" };
            comboBox.SelectedIndex = 0;
        }

        private void ComboBoxExtension_OnLoaded(object sender, RoutedEventArgs e)
        {
            ComboBoxExtension.ItemsSource = new List<string> {".jpg", ".jpeg", ".png"};
            ComboBoxExtension.SelectedIndex = 0;
        }

        private void ComboBoxAlgorithm_OnLoaded(object sender, RoutedEventArgs e)
        {
            ComboBoxAlgorithm.ItemsSource = new List<string> {"simple numbered", "subfolder numbered"};
            ComboBoxAlgorithm.SelectedIndex = 0;
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            Directory.CreateDirectory(_imagesDirectory);
        }
    }
}
