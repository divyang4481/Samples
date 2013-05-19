using BusinessApplication.Core;
using Ninject;
using Xunit;

namespace BusinessApplication.UnitTests
{
    public class BusinessServiceTests
    {
        private IBusinessService target;

        public BusinessServiceTests()
        {
            var kernel = new StandardKernel(new CoreModule());
            target = kernel.Get<IBusinessService>();
        }

        [Fact]
        public void should_be_able_to_get_BusinessService_from_Ninject()
        {
            Assert.NotNull(target);
        }

        [Fact]
        public void should_be_able_get_Logger_from_BusinessService_from_Ninject()
        {
            Assert.NotNull(target.Logger);
        }
    }
}
