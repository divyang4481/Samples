using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using Nancy.Testing;
using NancyApplication1;
using Xunit;

namespace NancyApplication1Tests
{
    public class IndexModuleTests
    {
        [Fact]
        public void Should_return_status_ok_when_route_exists()
        {
            // Given
            //var bootstrapper = new DefaultNancyBootstrapper();
            //var browser = new Browser(bootstrapper);

            var browser = new Browser(with => 
            { 
                with.Module(new IndexModule(null));
                //with.RootPathProvider<CustomRootPathProvider>();
            });

            // http://stackoverflow.com/questions/10466315/nancy-testing-project-cant-find-views

            //var boostrapper = new ConfiguratableBootstrapper(with =>
            //{
            //    with.RootPathProvider<CustomRootPathProvider>();
            //});

            // or "copy always" in views

            // When
            var result = browser.Get("/", with =>
            {
                with.HttpRequest();
            });

            // Then
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            
        }
    }
}
