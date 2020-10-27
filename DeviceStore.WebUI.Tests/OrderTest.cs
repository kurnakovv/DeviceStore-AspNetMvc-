using DeviceStore.Domain.AbstractModel;
using DeviceStore.Domain.Entities;
using DeviceStore.Domain.Services;
using DeviceStore.Domain.Services.Interfaces;
using DeviceStore.WebUI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Routing;

namespace DeviceStore.WebUI.Tests
{
    [TestClass]
    public class OrderTest
    {
        [TestMethod]
        public void CanToOrderAndCreateValidOrder()
        {
            IRepository<Device> devices = new MemoryRepository<Device>();
            IRepository<Basket> baskets = new MemoryRepository<Basket>();
            IRepository<Customer> customers = new MemoryRepository<Customer>();
            IRepository<Order> orders = new MemoryRepository<Order>();
            IBasketService basketService = new BasketService(devices, baskets);
            IOrderService orderService = new OrderService(orders);

            devices.Insert(new Device() { Id = "Guid1", DevicePrice = 10000.00m });
            devices.Insert(new Device() { Id = "Guid2", DevicePrice = 15000.00m });

            Basket basket = new Basket();
            basket.BasketItems.Add(new BasketItem() { DeviceId = "Guid1", Quantity = 3 });
            basket.BasketItems.Add(new BasketItem() { DeviceId = "Guid2", Quantity = 1 });

            baskets.Insert(basket);

            customers.Insert(new Customer()
            {
                Id = "Guid1",
                ApartmentNumber = "1",
                City = "City",
                Email = "maks@gmail.com",
                FirstName = "Maksim",
                LastName = "Kur",
                GiftWrap = true,
                HouseNumber = "1",
                PhoneNumber = "123456789",
                Street = "Street",                
            });

            IPrincipal FakeUser = new GenericPrincipal(new GenericIdentity("maks@gmail.com", "Forms"), null);

            var controller = new BasketController(basketService, orderService, customers);
            var httpContext = new HttpContext();
            httpContext.User = FakeUser;
            httpContext.Request.Cookies.Add(new System.Web.HttpCookie("myBasket") { Value = basket.Id });

            controller.ControllerContext = new ControllerContext(httpContext, new RouteData(), controller);

            Order order = new Order();
            controller.ToOrder(order);

            Assert.AreEqual(0, order.OrderItems.Count);
            Assert.AreEqual(2, basket.BasketItems.Count);

            Order orderInDb = orders.Find(order.Id);
            Assert.AreEqual(2, orderInDb.OrderItems.Count);
        }
    }
}