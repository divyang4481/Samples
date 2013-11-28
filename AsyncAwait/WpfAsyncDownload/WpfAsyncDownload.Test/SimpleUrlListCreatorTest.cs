using Xunit;

namespace WpfAsyncDownload.Test
{
    public class SimpleUrlListCreatorTest
    {
        public class CreateMethod
        {
            [Fact]
            public void should_create_list_of_urls()
            {
                // Arrange
                var target = new SimpleUrlListCreator();
                var downloadSettings = new DownloadSettings
                    {
                        Url = "google.pl/1.jpg",
                        StartIndex = 0,
                        EndIndex = 3,
                        NameFormat = "00",
                        Prefix = "a",
                        Suffix = "b"
                    };

                // Act
                var result = target.Create(downloadSettings);

                // Assert
                Assert.Equal(3, result.Count);
                Assert.Equal("http://google.pl/a00b.jpg", result[0]);
                Assert.Equal("http://google.pl/a01b.jpg", result[1]);
                Assert.Equal("http://google.pl/a02b.jpg", result[2]);
            }

            [Fact]
            public void should_create_one_url_for_no_number()
            {
                // Arrange
                var target = new SimpleUrlListCreator();
                var downloadSettings = new DownloadSettings
                {
                    Url = "google.pl/x.png"
                };

                // Act
                var result = target.Create(downloadSettings);

                // Assert
                Assert.Equal(1, result.Count);
                Assert.Equal("http://google.pl/x.png", result[0]);
            }
        }
    }
}
