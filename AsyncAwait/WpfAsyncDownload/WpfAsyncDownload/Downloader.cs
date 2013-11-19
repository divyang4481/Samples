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
        public async Task<DownloadResult> DownloadPagesAsync(DownloadSetting settings, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(settings.BaseUrl))
            {
                throw new ArgumentException("BaseUrl cannot be empty");
            }

            List<string> urlList = SetUpUrlList(settings);

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

        private List<string> SetUpUrlList(DownloadSetting settings)
        {
            var urls = new List<string>();

            string fileName = settings.BaseUrl.Substring(settings.BaseUrl.LastIndexOf("/"));

            if (!Regex.IsMatch(fileName, @"\d+"))
            {
                urls.Add(settings.BaseUrl);
            }
            else
            {
                for (int i = settings.StartIndex; i < settings.EndIndex; i++)
                {
                    var url = new StringBuilder(settings.BaseUrl.Substring(0, settings.BaseUrl.LastIndexOf("/") + 1));

                    url.Append(settings.Prefix);
                    url.Append(i.ToString(settings.NameFormat));
                    url.Append(settings.Suffix);
                    url.Append(settings.Extension);

                    urls.Add(url.ToString());
                }
    
            }
            
            return urls;
        }
    }
}
