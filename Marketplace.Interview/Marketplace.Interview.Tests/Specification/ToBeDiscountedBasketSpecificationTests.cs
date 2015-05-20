using Marketplace.Interview.Business.Basket;
using Marketplace.Interview.Business.Shipping;
using Marketplace.Interview.Business.Specification;
using NUnit.Framework;
using System.Collections.Generic;

namespace Marketplace.Interview.Tests.Specification
{
	[TestFixture]
	public class ToBeDiscountedBasketSpecificationTests
	{
		private ToBeDiscountedBasketSpecification ToBeDiscountedBasketSpecification { get; set; }

		[SetUp]
		public void Init()
		{
			ToBeDiscountedBasketSpecification = new ToBeDiscountedBasketSpecification();
		}

		[Test]
		public void IsSatisfiedTest()
		{
			var basket = new Basket()
			{
				LineItems = new List<LineItem>
				{
					new LineItem()
					{
						Shipping = new PerRegionShippingExtended(),
						DeliveryRegion = RegionShippingCost.Regions.UK,
						SupplierId = 1,
					},
					new LineItem()
					{
						Shipping = new PerRegionShippingExtended(),
						DeliveryRegion = RegionShippingCost.Regions.UK,
						SupplierId = 1,
					},
				}
			};

			Assert.IsTrue(ToBeDiscountedBasketSpecification.IsSatisfiedBy(basket));
		}

		[Test]
		public void IsSatisfied_DifferentShipping_ReturnsFalse()
		{
			var basket = new Basket()
			{
				LineItems = new List<LineItem>
				{
					new LineItem()
					{
						Shipping = new PerRegionShippingExtended(),
						DeliveryRegion = RegionShippingCost.Regions.UK,
						SupplierId = 1,
					},
					new LineItem()
					{
						Shipping = new PerRegionShipping(),
						DeliveryRegion = RegionShippingCost.Regions.UK,
						SupplierId = 1,
					},
				}
			};

			Assert.IsFalse(ToBeDiscountedBasketSpecification.IsSatisfiedBy(basket));
		}

		[Test]
		public void IsSatisfied_DifferentRegion_ReturnsFalse()
		{
			var basket = new Basket()
			{
				LineItems = new List<LineItem>
				{
					new LineItem()
					{
						Shipping = new PerRegionShippingExtended(),
						DeliveryRegion = RegionShippingCost.Regions.UK,
						SupplierId = 1,
					},
					new LineItem()
					{
						Shipping = new PerRegionShippingExtended(),
						DeliveryRegion = RegionShippingCost.Regions.Europe,
						SupplierId = 1,
					},
				}
			};

			Assert.IsFalse(ToBeDiscountedBasketSpecification.IsSatisfiedBy(basket));
		}

		[Test]
		public void IsSatisfied_DifferentSupplier_ReturnsFalse()
		{
			var basket = new Basket()
			{
				LineItems = new List<LineItem>
				{
					new LineItem()
					{
						Shipping = new PerRegionShippingExtended(),
						DeliveryRegion = RegionShippingCost.Regions.UK,
						SupplierId = 1,
					},
					new LineItem()
					{
						Shipping = new PerRegionShippingExtended(),
						DeliveryRegion = RegionShippingCost.Regions.UK,
						SupplierId = 2,
					},
				}
			};

			Assert.IsFalse(ToBeDiscountedBasketSpecification.IsSatisfiedBy(basket));
		}
	}
}
