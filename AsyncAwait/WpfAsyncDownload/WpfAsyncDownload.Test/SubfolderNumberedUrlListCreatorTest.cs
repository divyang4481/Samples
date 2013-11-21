using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WpfAsyncDownload.Test
{
    public class SubfolderNumberedUrlListCreatorTest
    {
        public class CreateMethod
        {
            [Fact]
            public void should_create_list_of_urls()
            {
                // Arrange
                var target = new FolderNumberedUrlListCreator();
                var downloadSettings = new DownloadSettings
                {
                    Url = "google.pl/122/1.jpg",
                    StartIndex = 0,
                    EndIndex = 3,
                    NameFormat = "00",
                    Prefix = "a",
                    Suffix = "b",
                    FolderStartIndex = 0,
                    FolderEndIndex = 2,
                    FolderNameFormat = "000"
                };

                // Act
                var result = target.Create(downloadSettings);

                // Assert
                Assert.Equal(6, result.Count);
                Assert.Equal("http://google.pl/000/a00b.jpg", result[0]);
                Assert.Equal("http://google.pl/000/a01b.jpg", result[1]);
                Assert.Equal("http://google.pl/000/a02b.jpg", result[2]);

                Assert.Equal("http://google.pl/001/a00b.jpg", result[3]);
                Assert.Equal("http://google.pl/001/a01b.jpg", result[4]);
                Assert.Equal("http://google.pl/001/a02b.jpg", result[5]);
            }

        }
    }
}
