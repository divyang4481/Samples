using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAsyncDownload
{
    public class DownloadSetting
    {
        private string _baseUrl;

        public DownloadSetting()
        {
            BaseUrl = "";
            Extension = ".jpg";
        }

        public int StartIndex { get; set; }
        public string BaseUrl
        {
            get { return _baseUrl.StartsWith("http://") ? _baseUrl : "http://" + _baseUrl; }
            set { _baseUrl = value; }
        }

        public int EndIndex { get; set; }
        public string NameFormat { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public string Extension { get; set; }
    }
}
