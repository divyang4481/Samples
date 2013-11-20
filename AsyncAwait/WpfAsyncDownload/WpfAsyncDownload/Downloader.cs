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
        public IUrlListCreator UrlListCreator { get; set; }

        public Downloader()
        {
            UrlListCreator = new SimpleNumberedUrlCreator();
        }

        public async Task<DownloadResult> DownloadImagesAsync(DownloadSettings settings, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(settings.Url))
            {
                throw new ArgumentException("BaseUrl cannot be empty");
            }

            List<string> urlList = UrlListCreator.Create(settings);

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

        private async Task<UrlResponse> ProcessUrlAsync(string url, CancellationToken cancellationToken)
        {
            var httpClient = new HttpClient { MaxResponseContentBufferSize = 1000000 };

            HttpResponseMessage responseMessage = await httpClient.GetAsync(url, cancellationToken);

            return new UrlResponse {Url = url, HttpResponseMessage = responseMessage};
        }
    }
}
