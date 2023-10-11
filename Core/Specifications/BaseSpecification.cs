using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public abstract class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification() { }
        public BaseSpecification(Expression<Func<T, bool>> condition)
        {
            Condition = condition;
        }

        public Expression<Func<T, bool>> Condition { get; }

        public List<Expression<Func<T, object>>> Values { get; } = new List<Expression<Func<T, object>>> ();

        protected void AddValue(Expression<Func<T, object>> expression) { Values.Add(expression); }
    }
}
