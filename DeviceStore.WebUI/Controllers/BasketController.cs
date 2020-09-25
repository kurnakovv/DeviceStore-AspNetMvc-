using DeviceStore.Domain.Services.Interfaces;
using DeviceStore.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeviceStore.WebUI.Controllers
{
    public class BasketController : Controller
    {
        IBasketService _basketService;
        
        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
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
    }
}