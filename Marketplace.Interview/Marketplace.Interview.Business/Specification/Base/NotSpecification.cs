namespace Marketplace.Interview.Business.Specification.Base
{
	public class NotSpecification<T> : CompositeSpecification<T>
	{
		protected readonly ISpecification<T> Specification;

		public NotSpecification(ISpecification<T> left)
		{
			Specification = left;
		}

		public override bool IsSatisfiedBy(T candidate)
		{
			return !Specification.IsSatisfiedBy(candidate);
		}
	}
}