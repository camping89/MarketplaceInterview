﻿
namespace Marketplace.Interview.Business.Specification.Base
{
	public interface ISpecification<T>
	{
		bool IsSatisfiedBy(T candidate);
		ISpecification<T> And(ISpecification<T> specification);
		ISpecification<T> Or(ISpecification<T> specification);
		ISpecification<T> Not(ISpecification<T> specification);
	}
}
