using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using DeviceStore.Domain.AbstractModel;
using DeviceStore.Domain.Entities;
using DeviceStore.Domain.Services;
using DeviceStore.Domain.Services.Interfaces;
using DeviceStore.Domain.ViewModel;
using DeviceStore.WebUI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DeviceStore.WebUI.Tests
{
    [TestClass]
    public class BasketTest
    {
        [TestMethod]
        public void CanAddBasketDevice()
        {

            IRepository<Device> devices = new MemoryRepository<Device>();
            IRepository<Basket> baskets = new MemoryRepository<Basket>();
            IBasketService basketService = new BasketService(devices, baskets);            

            var controller = new BasketController(basketService);
            var httpContext = new HttpContext();
             
            controller.ControllerContext = new ControllerContext(httpContext, new RouteData(), controller);


            controller.AddToBasket("Guid1");
            Basket basket = baskets.Collection().FirstOrDefault();

            Assert.IsNotNull(basket);
            Assert.AreEqual(1, basket.BasketItems.Count);
            Assert.AreEqual("Guid1", basket.BasketItems.ToList().FirstOrDefault().DeviceId);
        }

        [TestMethod]
        public void CanGetSummaryViewModel()
        {
            IRepository<Device> devices = new MemoryRepository<Device>();
            IRepository<Basket> baskets = new MemoryRepository<Basket>();
            IBasketService basketService = new BasketService(devices, baskets);

            devices.Insert(new Device() { Id = "1", DevicePrice = 10000.00m });
            devices.Insert(new Device() { Id = "2", DevicePrice = 15000.00m });

            Basket basket = new Basket();
            basket.BasketItems.Add(new BasketItem() { DeviceId = "1", Quantity = 3 });
            basket.BasketItems.Add(new BasketItem() { DeviceId = "2", Quantity = 1 });

            baskets.Insert(basket);

            var controller = new BasketController(basketService);
            var httpContext = new HttpContext();
            httpContext.Request.Cookies.Add(new System.Web.HttpCookie("myBasket") { Value = basket.Id });

            controller.ControllerContext = new ControllerContext(httpContext, new RouteData(), controller);

            var result = controller.BasketSummary();

            var basketSummary = (BasketSummaryViewModel)result.ViewData.Model;

            Assert.AreEqual(4, basketSummary.BasketItems);
            Assert.AreEqual(45000.00m, basketSummary.BasketTotal);
        }
    }
}