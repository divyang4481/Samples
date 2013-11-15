using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace WpfAsyncApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Tab1: simpliest example
        // Network request takes some time to finish but UI is not frozen
        // Update: with cancellation
        private async void ButtonGetHtmlAsync_OnClick(object sender, RoutedEventArgs e)
        {
            ButtonGetHtmlAsync.IsEnabled = false;

            var cancellationTokenSource = new CancellationTokenSource();
            ButtonCancel.Click += (snd, ev) => cancellationTokenSource.Cancel();

            using (var httpClient = new HttpClient())
            {
                Task<HttpResponseMessage> task = httpClient.GetAsync("http://www.google.pl", cancellationTokenSource.Token);

                try
                {
                    TextBoxResults.Text = (await task).ToString();
                }
                catch (TaskCanceledException exception)
                {
                    TextBoxResults.Text = exception.Message;
                }
            }
            
            ButtonGetHtmlAsync.IsEnabled = true;
        }

        // Tab2: long running computation
        // Task.Run 

        // See also:
        //Task t = Task.Factory.StartNew(() => MyLongComputation(a, b),
        //    cancellationToken,
        //    TaskCreationOptions.LongRunning,
        //    taskScheduler);

        private async void ButtonComputeAsync_OnClick(object sender, RoutedEventArgs e)
        {
            ButtonComputeAsync.IsEnabled = false;

            Task<int> task = Task.Run(() => LongComputation());

            TextBoxComputeResults.Text = (await task).ToString();

            ButtonComputeAsync.IsEnabled = true;   
        }

        // Hint: delaying for a period of time
        // await Task.Run(() => Thread.Sleep(100));
        // but it blocks a thread

        // Task.Delay(10000);
        private int LongComputation()
        {
            Thread.Sleep(TimeSpan.FromSeconds(10)); // simulates the long running computation
            return 10;
        }

        // Tab3: puppet task
        private async void ButtonCreatePuppetTaskAsync_OnClick(object sender, RoutedEventArgs e)
        {
            TextBoxPuppetTaskResults.Text = (await GetUserInput()).ToString();
        }

        private Task<bool> GetUserInput()
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();

            var dialog = new DialogWindow { Owner = Application.Current.MainWindow };
            dialog.Closed += (sender, args) => taskCompletionSource.SetResult(dialog.Result);

            dialog.Show();

            return taskCompletionSource.Task;
        }

        // Tab4: Old asynchronous patterns interaction

        // See also:
        //Task t = Task<IPHostEntry>.Factory.FromAsync<string>(Dns.BeginGetHostEntry,
        //Dns.EndGetHostEntry,
        //hostNameOrAddress,
        //null);

        private async void ButtonOldPatternInteractionAsync_OnClick(object sender, RoutedEventArgs e)
        {
            ButtonOldPatternInteractionAsync.IsEnabled = false;

            TextBoxOldPatternInteractionResults.Text = string.Join(", ", (await GetHostEntryAsync("www.google.com")).AddressList.ToList());

            ButtonOldPatternInteractionAsync.IsEnabled = true;
        }

        public static Task<IPHostEntry> GetHostEntryAsync(string hostNameOrAddress)
        {
            var taskCompletionSource = new TaskCompletionSource<IPHostEntry>();
            Dns.BeginGetHostEntry(hostNameOrAddress, (asyncResult) =>
                {
                    try
                    {
                        IPHostEntry result = Dns.EndGetHostEntry(asyncResult);
                        taskCompletionSource.SetResult(result);
                    }
                    catch (Exception e)
                    {
                        taskCompletionSource.SetException(e);
                    }
                }, null);

            return taskCompletionSource.Task;
        }

        // Tab5: Waiting for a collection of tasks
        private async void ButtonWaitAllAsync_OnClick(object sender, RoutedEventArgs e)
        {
            ButtonWaitAllAsync.IsEnabled = false;

            var addresses = new List<string> {"http://www.google.com", "http://www.stackoverflow.com"};
            
            IEnumerable<Task<byte[]>> tasks = addresses.Select(GetData);

            // The IEnumerable from Select is lazy, so evaluate it to start the tasks
            tasks = tasks.ToList();

            byte[][] bytes = await Task.WhenAll(tasks);

            TextBoxWaitAllResults.Text = string.Join(", ", bytes.Select(b => b.Length.ToString()));

            ButtonWaitAllAsync.IsEnabled = true;
        }

        private async Task<byte[]> GetData(string url)
        {
            byte[] result; 

            using (var webClient = new WebClient())
            {
                result = await webClient.DownloadDataTaskAsync(new Uri(url));
            }

            return result;
        }

        // Tab6: Progress
        private async void ButtonProgressAsync_OnClick(object sender, RoutedEventArgs e)
        {
        //    //construct Progress<T>, passing ReportProgress as the Action<T> 
        //    var progressIndicator = new Progress<int>(progress => ProgressBar1.Value = progress);
        //    //call async method
        //    int uploads = await UploadPicturesAsync(GenerateTestImages(), progressIndicator);
        }

        //private List<Image> GenerateTestImages()
        //{
        //    return null;
        //}

        //private async Task<int> UploadPicturesAsync(List<Image> imageList, IProgress<int> progress)
        //{
        //    int totalCount = imageList.Count;
        //    int processCount = await Task.Run<int>(() =>
        //    {
        //        int tempCount = 0;
        //        foreach (var image in imageList)
        //        {
        //            //await the processing and uploading logic here
        //            int processed = await UploadAndProcessAsync(image);
        //            if (progress != null)
        //            {
        //                progress.Report((tempCount * 100 / totalCount));
        //            }
        //            tempCount++;
        //        }

        //        return tempCount;
        //    });
        //    return processCount;
        //}

        //private async Task<int> UploadAndProcessAsync(Image image)
        //{
        //    await 
        //}

    }
}
