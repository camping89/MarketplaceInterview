using Marketplace.Interview.Business.Basket;
using Marketplace.Interview.Business.Shipping;
using NUnit.Framework;
using System.Collections.Generic;

namespace Marketplace.Interview.Tests
{
	[TestFixture]
	public class ShippingOptionTests
	{
		[Test]
		public void FlatRateShippingOptionTest()
		{
			var flatRateShippingOption = new FlatRateShipping { FlatRate = 1.5m };
			var shippingAmount = flatRateShippingOption.GetAmount(new LineItem(), new Basket());

			Assert.That(shippingAmount, Is.EqualTo(1.5m), "Flat rate shipping not correct.");
		}

		[Test]
		public void PerRegionShippingOptionTest()
		{
			var perRegionShippingOption = new PerRegionShipping()
			{
				PerRegionCosts = new[]
				{
					new RegionShippingCost()
					{
						DestinationRegion =
							RegionShippingCost.Regions.UK,
						Amount = .75m
					},
					new RegionShippingCost()
					{
						DestinationRegion =
							RegionShippingCost.Regions.Europe,
						Amount = 1.5m
					}
				},
			};

			var shippingAmount = perRegionShippingOption.GetAmount(new LineItem() { DeliveryRegion = RegionShippingCost.Regions.Europe }, new Basket());
			Assert.That(shippingAmount, Is.EqualTo(1.5m));

			shippingAmount = perRegionShippingOption.GetAmount(new LineItem() { DeliveryRegion = RegionShippingCost.Regions.UK }, new Basket());
			Assert.That(shippingAmount, Is.EqualTo(.75m));
		}

		[Test]
		public void BasketShippingTotalTest()
		{
			var perRegionShippingOption = new PerRegionShipping()
			{
				PerRegionCosts = new[]
				{
					new RegionShippingCost()
					{
						DestinationRegion =
							RegionShippingCost.Regions.UK,
						Amount = .75m
					},
					new RegionShippingCost()
					{
						DestinationRegion =
							RegionShippingCost.Regions.Europe,
						Amount = 1.5m
					}
				},
			};

			var flatRateShippingOption = new FlatRateShipping { FlatRate = 1.1m };

			var basket = new Basket()
			{
				LineItems = new List<LineItem>
				{
					new LineItem()
					{
						DeliveryRegion = RegionShippingCost.Regions.UK,
						Shipping = perRegionShippingOption
					},
					new LineItem()
					{
						DeliveryRegion = RegionShippingCost.Regions.Europe,
						Shipping = perRegionShippingOption
					},
					new LineItem() {Shipping = flatRateShippingOption},
				}
			};

			foreach (var lineItem in basket.LineItems)
			{
				lineItem.ShippingAmount = lineItem.Shipping.GetAmount(lineItem, basket);
			}

			var calculator = new ShippingCalculator();

			decimal basketShipping = calculator.CalculateShipping(basket);

			Assert.That(basketShipping, Is.EqualTo(3.35m));
			Assert.That(basket.Shipping, Is.EqualTo(3.35m));
			Assert.That(basket.Discount, Is.EqualTo(0));
		}


		[Test]
		public void BasketShippingTotal_WithDiscount_Test()
		{
			var perRegionShippingOption = new PerRegionShipping()
			{
				PerRegionCosts = new[]
				{
					new RegionShippingCost()
					{
						DestinationRegion =
							RegionShippingCost.Regions.UK,
						Amount = .75m
					},
					new RegionShippingCost()
					{
						DestinationRegion =
							RegionShippingCost.Regions.Europe,
						Amount = 1.5m
					}
				},
			};

			var perRegionShippingExtendedOption = new PerRegionShippingExtended
			{
				PerRegionCosts = new[]
				{
					new RegionShippingCost()
					{
						DestinationRegion =
							RegionShippingCost.Regions.UK,
						Amount = .75m
					},
					new RegionShippingCost()
					{
						DestinationRegion =
							RegionShippingCost.Regions.Europe,
						Amount = 1.5m
					}
				},
			};

			var flatRateShippingOption = new FlatRateShipping { FlatRate = 1.1m };

			var basket = new Basket()
			{
				LineItems = new List<LineItem>
				{
					new LineItem()
					{
						DeliveryRegion = RegionShippingCost.Regions.UK,
						Shipping = perRegionShippingExtendedOption
					},
					new LineItem()
					{
						DeliveryRegion = RegionShippingCost.Regions.UK,
						Shipping = perRegionShippingExtendedOption
					},
					new LineItem()
					{
						DeliveryRegion = RegionShippingCost.Regions.UK,
						Shipping = perRegionShippingOption
					},
					new LineItem()
					{
						DeliveryRegion = RegionShippingCost.Regions.Europe,
						Shipping = perRegionShippingOption
					},
					new LineItem() {Shipping = flatRateShippingOption},
				}
			};

			foreach (var lineItem in basket.LineItems)
			{
				lineItem.ShippingAmount = lineItem.Shipping.GetAmount(lineItem, basket);
			}

			var calculator = new ShippingCalculator();

			decimal basketShipping = calculator.CalculateShipping(basket);

			Assert.That(basketShipping, Is.EqualTo(4.35m));
			Assert.That(basket.Shipping, Is.EqualTo(4.85m));
			Assert.That(basket.Discount, Is.EqualTo(.5m));
		}
	}
}