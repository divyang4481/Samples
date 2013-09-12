using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DynamicObjectsTests
{
    public class ExpressionsTests
    {
        [Fact]
        public void TestAddExpression()
        {
            // Lambda
            //Func<int, int, int> function = (a, b) => a + b;

            // Expression
            Expression<Func<int, int, int>> expression = (a, b) => a + b;

            var body = (BinaryExpression)expression.Body;

            Assert.Equal("(a + b)", body.ToString());
            Assert.Equal("a", expression.Parameters[0].ToString());
            Assert.Equal("b", expression.Parameters[1].ToString());

            var left = (ParameterExpression)body.Left;
            var right = (ParameterExpression)body.Right;

            Assert.Equal("a", left.Name);
            Assert.Equal(typeof(int), left.Type);
            Assert.Equal(ExpressionType.Parameter, left.NodeType);

            Assert.Equal("b", right.Name);
            Assert.Equal(typeof(int), right.Type);
            Assert.Equal(ExpressionType.Parameter, right.NodeType);

            Assert.Equal(typeof(int), body.Type);
            Assert.Equal(ExpressionType.Add, body.NodeType);

            int result = expression.Compile()(8, 2);
            Assert.Equal(10, result);
        }

        [Fact]
        public void TestMultiplyAndAddExpression()
        {
            var paramExprA = Expression.Parameter(typeof (int), "paramA");
            var paramExprB = Expression.Parameter(typeof (decimal), "paramB");
            var paramExprC = Expression.Parameter(typeof (decimal), "paramC");

            // Conversion
            var paramExprAConverted = Expression.Convert(paramExprA, typeof (decimal));

            // Multiplication
            var oper1 = Expression.MultiplyChecked(paramExprAConverted, paramExprB);

            // Adding
            var oper2 = Expression.AddChecked(oper1, paramExprC);

            var lambdaExpr = Expression.Lambda<Func<int, decimal, decimal, decimal>>(oper2, new[] { paramExprA, paramExprB, paramExprC });
            var testMethod = lambdaExpr.Compile();

            // (10 * 33.4) + 2 == 336
            Assert.Equal(336m, testMethod(10, 33.4m, 2m));

            //Expression.AndAlso() - short circuting
        }

        [Fact]
        public void TestPrintingExpression()
        {
            Expression<Action<int>> printExpr = arg => Console.WriteLine(arg);
            printExpr.Compile()(10);
        }

        [Fact]
        public void TestMethodCall()
        {
            ParameterExpression parameter = Expression.Parameter(typeof(int), "arg");
            MethodCallExpression methodCall = Expression.Call(typeof (Console).GetMethod("WriteLine", new[] {typeof (int)}), parameter);

            Expression.Lambda<Action<int>>(methodCall, new[] {parameter})
                .Compile()(10);
        }

        [Fact]
        public void TestBlockExpression()
        {
            // This (lambda) gives compiler error:
            //{
            //    Expression<Action<int>> printTwoLinesExpr = arg =>
            //        {
            //            Console.WriteLine("Print arg:");
            //            Console.WriteLine(arg);
            //        };
            //}

            // but this not:

            ParameterExpression parameter = Expression.Parameter(typeof(int), "arg");

            MethodCallExpression firstMethodCall = Expression.Call(
                typeof(Console).GetMethod("WriteLine", new[] { typeof(string) }),
                Expression.Constant("Print arg:"));

            MethodCallExpression secondMethodCall = Expression.Call(
                typeof(Console).GetMethod("WriteLine", new[] { typeof(int) }),
                parameter);

            BlockExpression blockExpression = Expression.Block(firstMethodCall, secondMethodCall);

            Expression.Lambda<Action<int>>(
                blockExpression,
                new[] { parameter }).Compile()(10);
        }
    }
}
