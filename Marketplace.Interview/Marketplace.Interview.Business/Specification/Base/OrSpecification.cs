namespace Marketplace.Interview.Business.Specification.Base
{
	public class OrSpecification<T> : CompositeSpecification<T>
	{
		protected readonly ISpecification<T> LeftSpecification;
		protected readonly ISpecification<T> RightSpecification;

		public OrSpecification(ISpecification<T> left, ISpecification<T> right)
		{
			LeftSpecification = left;
			RightSpecification = right;
		}

		public override bool IsSatisfiedBy(T candidate)
		{
			return LeftSpecification.IsSatisfiedBy(candidate)
			       || RightSpecification.IsSatisfiedBy(candidate);
		}
	}
}