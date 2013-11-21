using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            string fileName = Path.GetFileName(urlResponse.Url);
            string rootPath = UrlCreatorHelper.GetBaseUrl(urlResponse.Url);

            return string.Concat(
                rootPath
                    .Replace("http://", "")
                    .Replace("/", "_")
                    .Replace(".", "_"),
                "_",
                fileName);
        }
    }
}
