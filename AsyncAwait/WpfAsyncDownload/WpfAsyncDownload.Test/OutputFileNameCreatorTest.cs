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
                var url = "http://google.pl/x.png";

                //Act
                var result = target.Create(url);

                // Assert
                Assert.Equal("google_pl_x.png", result);
            }
        }
    }
}
