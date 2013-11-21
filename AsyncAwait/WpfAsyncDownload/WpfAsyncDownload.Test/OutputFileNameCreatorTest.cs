using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                var response = new UrlResponse {Url = "http://google.pl/x.png"};

                //Act
                var result = target.Create(response);

                // Assert
                Assert.Equal("google_pl_x.png", result);
            }
        }
    }
}
