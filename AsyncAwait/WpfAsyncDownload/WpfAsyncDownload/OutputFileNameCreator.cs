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
        string Create(string url);
    }

    public class OutputFileNameCreator : IOutputFileNameCreator
    {
        public string Create(string url)
        {
            string fileName = Path.GetFileName(url);
            string rootPath = UrlHelper.GetBaseUrl(url);

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
