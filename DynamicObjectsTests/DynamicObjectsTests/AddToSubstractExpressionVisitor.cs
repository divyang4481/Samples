using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectsTests
{
    public class AddToSubstractExpressionVisitor : ExpressionVisitor
    {
        public Expression Modify(Expression node)
        {
            return Visit(node);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            return node.NodeType == ExpressionType.Add ?
                Expression.Subtract(node.Left, node.Right) : node;
        }
    }
}
