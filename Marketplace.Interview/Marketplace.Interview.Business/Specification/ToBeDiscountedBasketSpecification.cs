using Marketplace.Interview.Business.Shipping;
using Marketplace.Interview.Business.Specification.Base;
using System.Linq;

namespace Marketplace.Interview.Business.Specification
{
	public class ToBeDiscountedBasketSpecification : CompositeSpecification<Basket.Basket>
	{
		public override bool IsSatisfiedBy(Basket.Basket basket)
		{
			return basket.LineItems
				.Where(_ => _.Shipping.GetType() == typeof(PerRegionShippingExtended))
				.GroupBy(li => new
				{
					li.SupplierId,
					li.DeliveryRegion
				})
				.Any(g => g.Count() > 1);
		}
	}
}