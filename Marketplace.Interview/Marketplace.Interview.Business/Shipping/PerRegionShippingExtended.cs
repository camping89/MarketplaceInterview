using Marketplace.Interview.Business.Basket;

namespace Marketplace.Interview.Business.Shipping
{
	public class PerRegionShippingExtended : PerRegionShipping
	{
		public override string GetDescription(LineItem lineItem, Basket.Basket basket)
		{
			return string.Format("Shipping to {0} (Ext)", lineItem.DeliveryRegion);
		}
	}
}