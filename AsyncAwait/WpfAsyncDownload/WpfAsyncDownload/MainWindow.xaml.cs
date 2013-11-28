using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;

namespace WpfAsyncDownload
{
    public partial class MainWindow : Window
    {
        public ViewModel ViewModel { get; set; }

        private string _imagesDirectory = "images";
        private DownloadResult _downloadResult;
        private readonly IOutputFileNameCreator _outputFileNameCreator = new OutputFileNameCreator();

        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new ViewModel();
            GridMain.DataContext = ViewModel;
        }

        private async void ButtonDownloadWhenAny_OnClick(object sender, RoutedEventArgs e)
        {
            ButtonDownloadWhenAny.IsEnabled = false;
            ViewModel.ClearMessages();
            ViewModel.AddMessage("...");

            var cancellationTokenSource = GetCancellationTokenSource();
            _downloadResult = new DownloadResult();

            var progress = new Progress<string>(
                value => ViewModel.AddMessage(value));

            var downloader = new Downloader();

            try
            {
                _downloadResult = await downloader.DownloadImagesWhenAnyAsync(GetSettings(), cancellationTokenSource.Token, progress);
            }
            catch (OperationCanceledException)
            {
                ViewModel.AddMessage("Operation cancelled");
            }
            catch (Exception exception)
            {
                ViewModel.AddMessage(exception.Message);
            }
            finally
            {
                ButtonDownloadWhenAny.IsEnabled = true;
                ViewModel.AddMessage("Finished");
            }
        }

        private async void ButtonDownloadWhenAll_OnClick(object sender, RoutedEventArgs e)
        {
            ButtonDownloadWhenAll.IsEnabled = false;
            ViewModel.ClearMessages();
            ViewModel.AddMessage("...");
            
            CancellationTokenSource cancellationTokenSource = GetCancellationTokenSource();
            _downloadResult = new DownloadResult();
            var downloader = new Downloader();

            try
            {
                _downloadResult = await downloader.DownloadImagesWhenAllAsync(GetSettings(), cancellationTokenSource.Token);

                ViewModel.ClearMessages();
                _downloadResult.Responses.ForEach(x => ViewModel.AddMessage(string.Format("{0,-58} {1,8}", x.Url, x.HttpResponseMessage.StatusCode)));
            }
            catch (OperationCanceledException)
            {
                ViewModel.AddMessage("Operation cancelled");
            }
            catch (Exception exception)
            {
                ViewModel.AddMessage(exception.Message);
            }
            finally
			{
				ButtonDownloadWhenAll.IsEnabled = true;
                ViewModel.AddMessage("Finished");

                if (_downloadResult.IsError && _downloadResult.AggregateException != null)
                {
                    _downloadResult.AggregateException.InnerExceptions.ToList().ForEach(x => ViewModel.AddMessage(x.Message));
                }
			}
        }

        private CancellationTokenSource GetCancellationTokenSource()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            
            ButtonCancel.Click += (snd, ev) =>
            {
                cancellationTokenSource.Cancel();
                ViewModel.AddMessage("Cancelled");
            };

            return cancellationTokenSource;
        }

        private DownloadSettings GetSettings()
        {
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
                    FolderStartIndex = folderIndexFrom,
                    FolderEndIndex = folderIndexTo,
                    FolderNameFormat = ComboBoxFolderNameFormat.SelectedValue.ToString(),
                    NameFormat = ComboBoxNameFormat.SelectedValue.ToString(),
                    Prefix = TextBoxPrefix.Text,
                    Suffix = TextBoxSuffix.Text,
                    Extension = ComboBoxExtension.SelectedValue.ToString(),
                    UrlListCreator = ComboBoxAlgorithm.SelectedIndex == 1 // TODO: This can be done better
                                         ? (IUrlListCreator) new FolderUrlListCreator()
                                         : new SimpleUrlListCreator()
                };

            return settings;
        }

        private async void ButtonSave_OnClick(object sender, RoutedEventArgs e)
        {
            ButtonSave.IsEnabled = false;

            foreach (UrlResponse responseMessage in _downloadResult.Responses.Where(r => r.HttpResponseMessage.IsSuccessStatusCode))
            {
                string fileName = _imagesDirectory + "/" + _outputFileNameCreator.Create(responseMessage.Url);

                using (Stream contentStream = await responseMessage.HttpResponseMessage.Content.ReadAsStreamAsync(),
                    stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None, 1000000, useAsync:true))
                {
                    await contentStream.CopyToAsync(stream);
                }
            }

            _downloadResult = new DownloadResult();
            ViewModel.AddMessage("Saved");
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
                ViewModel.AddMessage(string.Format("{0} selected", _imagesDirectory));
            }
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            Directory.CreateDirectory(_imagesDirectory);
        }
    }

    public class ViewModel //: INotifyPropertyChanged
    {
        public void ClearMessages()
        {
            _messages.Clear();
        }

        public void AddMessage(string message)
        {
            _messages.Insert(0, message);
        }

        private readonly ObservableCollection<string> _messages = new ObservableCollection<string>();

        public ObservableCollection<string> Messages
        {
            get { return _messages; }
        }

        public List<string> NameFormats { get { return new List<string> {"0", "00", "000", "0000", "00000"}; } }
        public List<string> Extensions { get { return new List<string> {".jpg", ".jpeg", ".png"}; } }
        public List<string> Algorithms { get { return new List<string> {"simple", "folder"}; } }

        //public event PropertyChangedEventHandler PropertyChanged;

        //private void OnPropertyChanged(string propertyName)
        //{
        //    if (PropertyChanged != null)
        //        PropertyChanged(this,
        //            new PropertyChangedEventArgs(propertyName));
        //} 
    }
}
