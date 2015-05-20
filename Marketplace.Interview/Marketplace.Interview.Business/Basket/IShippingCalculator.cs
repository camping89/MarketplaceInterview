using Marketplace.Interview.Business.Specification;
using System.Linq;

namespace Marketplace.Interview.Business.Basket
{
	public interface IShippingCalculator
	{
		decimal CalculateShipping(Basket basket);
	}

	public class ShippingCalculator : IShippingCalculator
	{
		public const decimal DiscountAmount = .5m;

		public decimal CalculateShipping(Basket basket)
		{
			foreach (var lineItem in basket.LineItems)
			{
				lineItem.ShippingAmount = lineItem.Shipping.GetAmount(lineItem, basket);
				lineItem.ShippingDescription = lineItem.Shipping.GetDescription(lineItem, basket);
			}

			basket.Shipping = basket.LineItems.Sum(li => li.ShippingAmount);
			basket.Discount = new ToBeDiscountedBasketSpecification().IsSatisfiedBy(basket) ? DiscountAmount : 0;
			return basket.Shipping - basket.Discount;
		}
	}
}