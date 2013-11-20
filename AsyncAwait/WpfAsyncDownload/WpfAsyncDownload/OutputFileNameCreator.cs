using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAsyncDownload
{
    public interface IOutputFileNameCreator
    {
        string Create(UrlResponse urlResponse);
    }

    public class OutputFileNameCreator : IOutputFileNameCreator
    {
        public string Create(UrlResponse urlResponse)
        {
            //string fileName = urlResponse.Url.Substring(urlResponse.Url.LastIndexOf("/", System.StringComparison.Ordinal) + 1);
            //return Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + "-" + fileName;

            return urlResponse.Url.Replace("/", "_");
        }
    }
}
