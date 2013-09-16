using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DynamicObjectsTests
{
    public class AddToSubstractExpressionVisitorTest
    {
        [Fact]
        public void TestVisitor()
        {
            // Arrange
            var target = new AddToSubstractExpressionVisitor();
            Expression<Func<int, int, int>> expression = (a, b) => a + b;

            // Act
            var result = (LambdaExpression) target.Modify(expression);

            // Assert
            Assert.Equal(1, result.Compile().DynamicInvoke(10, 9));
        }
    }
}
