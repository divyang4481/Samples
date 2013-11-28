using System;
using System.Collections.Generic;
using System.Net.Http;

namespace WpfAsyncDownload
{
    public class DownloadResult
    {
        public DownloadResult()
        {
            Responses = new List<UrlResponse>();
        }

        public List<UrlResponse> Responses { get; set; }

        public AggregateException AggregateException { get; set; }
        public bool IsError { get; set; }
    }

    public class UrlResponse
    {
        public string Url { get; set; }
        public HttpResponseMessage HttpResponseMessage { get; set; }
    }
}
