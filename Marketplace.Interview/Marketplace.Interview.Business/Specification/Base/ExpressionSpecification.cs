using System;

namespace Marketplace.Interview.Business.Specification.Base
{
	public class ExpressionSpecification<T> : CompositeSpecification<T>
	{
		private readonly Func<T, bool> _expression;
		public ExpressionSpecification(Func<T, bool> expression)
		{
			if (expression == null)
				throw new ArgumentNullException();
			_expression = expression;
		}

		public override bool IsSatisfiedBy(T candidate)
		{
			return _expression(candidate);
		}
	}
}
