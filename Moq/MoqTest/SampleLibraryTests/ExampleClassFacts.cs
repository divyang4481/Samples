using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SampleLibrary;
using Xunit;
using Moq;

namespace SampleLibraryTests
{
    public class ExampleClassFacts
    {
        public class GetDataMethod
        {
            private Mock<ILongRunningLibrary> _longRunningLibrary;

            public GetDataMethod()
            {
                _longRunningLibrary = new Mock<ILongRunningLibrary>();

                _longRunningLibrary
                    .Setup(lrl => lrl.RunForALongTime(It.IsAny<int>()))
                    .Returns((int interval) => string.Format("Waited {0} seconds", interval));

                _longRunningLibrary
                    .Setup(lrl => lrl.RunForALongTime(0))
                    .Throws(new ArgumentException("0 is not a valid interval"));
            }

            [Fact]
            public void returns_correct_interval_message()
            {
                // Arrange
                var target = new ExampleClass(_longRunningLibrary.Object);

                // Act
                var result = target.GetData();

                // Assert
                Assert.Equal("OK", result);
            }
        }
    }
}
