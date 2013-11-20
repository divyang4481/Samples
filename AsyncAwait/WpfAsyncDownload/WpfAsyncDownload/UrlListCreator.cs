using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WpfAsyncDownload
{
    public interface IUrlListCreator
    {
        List<string> Create(DownloadSettings settings);
    }

    public class SimpleNumberedUrlCreator : IUrlListCreator
    {
        public List<string> Create(DownloadSettings settings)
        {
            var urls = new List<string>();

            string fileName = UrlCreatorHelper.GetFileName(settings.Url);

            if (!Regex.IsMatch(fileName, @"\d+"))
            {
                urls.Add(settings.Url);
            }
            else
            {
                for (int i = settings.StartIndex; i < settings.EndIndex; i++)
                {
                    var url = new StringBuilder(UrlCreatorHelper.GetBaseUrl(settings.Url))
                        .AppendFormat(UrlCreatorHelper.CreateSingleFileName(settings, i));

                    urls.Add(url.ToString());
                }
            }

            return urls;
        }
    }

    public class SubfolderNumberedUrlCreator : IUrlListCreator
    {
        public List<string> Create(DownloadSettings settings)
        {
            var urls = new List<string>();

            for (int j = settings.FolderStartIndex; j < settings.FolderEndIndex; j++)
            {
                var baseUrl = new StringBuilder(UrlCreatorHelper.GetBaseUrl(settings.Url))
                    .Append(j)
                    .Append("/");

                for (int i = settings.StartIndex; i < settings.EndIndex; i++)
                {
                    var url = new StringBuilder(baseUrl.ToString())
                        .AppendFormat(UrlCreatorHelper.CreateSingleFileName(settings, i));

                    urls.Add(url.ToString());
                }
            }

            return urls;
        }
    }

    public static class UrlCreatorHelper
    {
        public static string CreateSingleFileName(DownloadSettings settings, int index)
        {
            return string.Format("{0}{1}{2}{3}", settings.Prefix, index.ToString(settings.NameFormat), settings.Suffix, settings.Extension);
        }

        public static string GetFileName(string url)
        {
            return url.Substring(url.LastIndexOf("/"));
        }

        public static string GetBaseUrl(string url)
        {
            return url.Substring(0, url.LastIndexOf("/") + 1);
        }
    }
}
