using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DynamicObjectsTests
{
    public class MyDynamicObjectTest
    {
        [Fact]
        public void GetAndSetShouldWork()
        {
            dynamic myDynamic = new MyDynamicObject();

            myDynamic.A = "A";
            myDynamic.B = 10;

            Assert.Equal("A", myDynamic.A);
            Assert.Equal(10, myDynamic.B);

            var dynamicMemberNames = (IEnumerable<string>)myDynamic.GetDynamicMemberNames();

            Assert.Equal("A", dynamicMemberNames.ElementAt(0));
            Assert.Equal("B", dynamicMemberNames.ElementAt(1));
        }
    }
}
