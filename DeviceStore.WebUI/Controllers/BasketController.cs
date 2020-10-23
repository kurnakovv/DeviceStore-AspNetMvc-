using DeviceStore.Domain.AbstractModel;
using DeviceStore.Domain.Entities;
using DeviceStore.Domain.Services.Interfaces;
using DeviceStore.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DeviceStore.WebUI.Controllers
{
    public class BasketController : Controller
    {
        // The order of construction of variables is important.
        private readonly IRepository<Customer> _customerRepository;
        IBasketService _basketService;
        private readonly IOrderService _orderService;
        

        public BasketController(IBasketService basketService, IOrderService orderService, IRepository<Customer> customerRepository)
        {
            _basketService = basketService;
            _orderService = orderService;
            _customerRepository = customerRepository;
        }

        public ActionResult Index()
        {
            List<BasketItemViewModel> model = _basketService.GetBasketItems(HttpContext);
            
            return View(model);
        }

        public ActionResult AddToBasket(string id)
        {
            _basketService.AddToBasket(HttpContext, id);

            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromBasket(string id)
        {
            _basketService.RemoveFromBasket(HttpContext, id);

            return RedirectToAction("Index");
        }

        public PartialViewResult BasketSummary()
        {
            BasketSummaryViewModel basketSummary = _basketService.GetBasketSummary(HttpContext);

            return PartialView("BasketSummary", basketSummary);
        }

        [Authorize]
        public ActionResult ToOrder()
        {
            Customer customer = _customerRepository
                .Collection()
                .FirstOrDefault(c => c.Email == User.Identity.Name);

            if (customer != null)
            {
                var order = new Order()
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    City = customer.City,
                    Street = customer.Street,
                    HouseNumber = customer.HouseNumber,
                    ApartmentNumber = customer.ApartmentNumber,
                    PhoneNumber = customer.PhoneNumber,
                    Email = customer.Email,
                    GiftWrap = customer.GiftWrap,
                };

                return View(order);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> ToOrder(Order order)
        {
            List<BasketItemViewModel> basketItems =
                _basketService.GetBasketItems(HttpContext);

            order.OrderStatus = "Заказ создан";
            order.Email = User.Identity.Name;

            // Here we have to process the payment. Here is the lite version.  

            EmailService emailService = new EmailService();
            await emailService.SendEmailAsync(order.Email, 
                "Вам пришел ваш чек",
                $"Здраствуйте, ваш чек: <img><br/> {new { order.Id}}.");

            order.OrderStatus = "Платеж обработан";
            _orderService.CreateNewOrder(order, basketItems);
            _basketService.ClearBasket(HttpContext);

            return RedirectToAction("ThankYou", new { OrderId = order.Id });
        }

        public ActionResult ThankYou(string orderId)
        {
            ViewBag.OrderId = orderId;
            return View();
        }
    }
}