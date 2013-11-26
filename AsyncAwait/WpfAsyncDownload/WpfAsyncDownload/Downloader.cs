using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace WpfAsyncDownload
{
    public class Downloader
    {
        private readonly HttpClient _httpClient;

        public Downloader()
        {
            _httpClient = new HttpClient
            {
                MaxResponseContentBufferSize = 1000000 /* bytes */
            };
        }

        public async Task<DownloadResult> DownloadImagesWhenAllAsync(DownloadSettings settings, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(settings.Url))
            {
                throw new ArgumentException("BaseUrl cannot be empty");
            }

            List<string> urlList = settings.UrlListCreator.Create(settings);
            IEnumerable<Task<UrlResponse>> downloadTaskQuery = urlList.Select(url => ProcessUrlAsync(url, cancellationToken));
            List<Task<UrlResponse>> downloadTasks = downloadTaskQuery.ToList();

            Task<UrlResponse[]> allTask = Task.WhenAll(downloadTasks);

			var result = new DownloadResult();
			
			try
			{
				UrlResponse[] contents = await allTask;
				result.Responses = contents.ToList();
			}
			catch
			{
			    result.IsError = true;
				result.AggregateException = allTask.Exception;
			}
			
            return result;
        }

        public async Task<DownloadResult> DownloadImagesWhenAnyAsync(DownloadSettings settings, CancellationToken cancellationToken, IProgress<string> progress)
        {
            if (string.IsNullOrWhiteSpace(settings.Url))
            {
                throw new ArgumentException("BaseUrl cannot be empty");
            }

            List<string> urlList = settings.UrlListCreator.Create(settings);
            IEnumerable<Task<UrlResponse>> downloadTaskQuery = urlList.Select(url => ProcessUrlAsync(url, cancellationToken));
            List<Task<UrlResponse>> downloadTasks = downloadTaskQuery.ToList();

            var result = new DownloadResult();

            while (downloadTasks.Count > 0)
            {
                Task<UrlResponse> finishedTask = await Task.WhenAny(downloadTasks);

                downloadTasks.Remove(finishedTask);

                UrlResponse response = await finishedTask;
                result.Responses.Add(response);

                progress.Report(string.Format("{0,-58} {1,8}", response.Url, response.HttpResponseMessage.StatusCode));
            }

            return result;
        }

        private async Task<UrlResponse> ProcessUrlAsync(string url, CancellationToken cancellationToken)
        {
            HttpResponseMessage responseMessage = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseContentRead, cancellationToken);

            return new UrlResponse {Url = url, HttpResponseMessage = responseMessage};
        }
    }
}
