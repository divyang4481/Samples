using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAsyncDownload
{
    public class DownloadSettings
    {
        private string _baseUrl;

        public DownloadSettings()
        {
            Url = "";
            Extension = ".jpg";
        }

        public int StartIndex { get; set; }
        public string Url
        {
            get { return _baseUrl.StartsWith("http://") ? _baseUrl : "http://" + _baseUrl; }
            set { _baseUrl = value; }
        }

        public int EndIndex { get; set; }
        public string NameFormat { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public string Extension { get; set; }

        public int FolderStartIndex { get; set; }
        public int FolderEndIndex { get; set; }
        public string FolderNameFormat { get; set; }
    }
}
