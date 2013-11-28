using Xunit;

namespace WpfAsyncDownload.Test
{
    public class OutputFileNameCreatorTest
    {
        public class CreateMethod
        {
            [Fact]
            public void should_create_filename()
            {
                // Arrange
                var target = new OutputFileNameCreator();
                const string url = "http://google.pl/x.png";

                //Act
                var result = target.Create(url);

                // Assert
                Assert.Equal("google_pl_x.png", result);
            }
        }
    }
}
