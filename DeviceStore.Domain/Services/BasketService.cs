using DeviceStore.Domain.AbstractModel;
using DeviceStore.Domain.Entities;
using DeviceStore.Domain.Services.Interfaces;
using DeviceStore.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DeviceStore.Domain.Services
{
    public class BasketService : IBasketService
    {
        private readonly IRepository<Device> _deviceContext;
        private readonly IRepository<Basket> _basketContext;

        public const string BasketSessionName = "myBasket";

        public BasketService(IRepository<Device> deviceContext, 
                             IRepository<Basket> basketContext)
        {
            _deviceContext = deviceContext;
            _basketContext = basketContext;
        }

        private Basket GetBasketCookies(HttpContextBase httpContextBase,
                                       bool createInNull)
        {
            HttpCookie httpCookie = httpContextBase.Request.Cookies.Get(BasketSessionName);

            Basket basket = new Basket();

            if(httpCookie != null)
            {
                string basketId = httpCookie.Value;
                if(!string.IsNullOrEmpty(basketId))
                {
                    basket = _basketContext.Find(basketId);
                }
                else
                {
                    if(createInNull)
                    {
                        basket = CreateNewBasket(httpContextBase);
                    }
                }
            }
            else
            {
                if (createInNull)
                {
                    basket = CreateNewBasket(httpContextBase);
                }
            }

            return basket;
        }



        private Basket CreateNewBasket(HttpContextBase httpContextBase)
        {
            Basket basket = new Basket();
            _basketContext.Insert(basket);
            _basketContext.Commit();

            HttpCookie httpCookie = new HttpCookie(BasketSessionName);
            httpCookie.Value = basket.Id;
            httpCookie.Expires = DateTime.Now.AddDays(1);
            httpContextBase.Response.Cookies.Add(httpCookie);

            return basket;
        }

        public void AddToBasket(HttpContextBase httpContextBase, string deviceId)
        {
            Basket basket = GetBasketCookies(httpContextBase, true);
            BasketItem basketItem = basket.BasketItems.FirstOrDefault(i => i.DeviceId == deviceId);

            if(basketItem == null)
            {
                basketItem = new BasketItem()
                {
                    BasketId = basket.Id,
                    DeviceId = deviceId,
                    Quantity = 1
                };

                basket.BasketItems.Add(basketItem);
            }
            else
            {
                basketItem.Quantity = basketItem.Quantity + 1;
            }

            _basketContext.Commit();
        }

        public List<BasketItemViewModel> GetBasketItems(HttpContextBase httpContextBase)
        {
            Basket basket = GetBasketCookies(httpContextBase, false);

            if(basket != null)
            {
                var results = (from b in basket.BasketItems
                               join d in _deviceContext.Collection() on b.DeviceId equals d.Id
                               select new BasketItemViewModel()
                               {
                                   BasketItemId = b.Id,
                                   QuantityDevicesItems = b.Quantity,
                                   DeviceName = d.DeviceName,
                                   Image = d.DeviceImage,
                                   Price = d.DevicePrice,

                               }).ToList();

                return results;
            }
            else
            {
                return new List<BasketItemViewModel>();
            }
        }

        public BasketSummaryViewModel GetBasketSummary(HttpContextBase httpContext)
        {
            Basket basket = GetBasketCookies(httpContext, false);

            BasketSummaryViewModel model = new BasketSummaryViewModel(0, 0);

            if(basket != null)
            {
                int? basketItems = (from item in basket.BasketItems
                                    select item.Quantity).Sum();



                decimal? basketTotal = (from item in basket.BasketItems
                                        join d in _deviceContext.Collection() on item.DeviceId equals d.Id
                                        select item.Quantity * d.DevicePrice).Sum();

                model.BasketItems = basketItems ?? 0;
                model.BasketTotal = basketTotal ?? decimal.Zero;

                return model;
            }
            else
            {
                return model;
            }
        }

        public void RemoveFromBasket(HttpContextBase httpContextBase, string itemId)
        {
            Basket basket = GetBasketCookies(httpContextBase, true);

            BasketItem basketItem = basket.BasketItems.FirstOrDefault(i => i.Id == itemId);
        
            if(basketItem != null)
            {
                basket.BasketItems.Remove(basketItem);
                _basketContext.Commit();
            }
        }

        public void ClearBasket(HttpContextBase httpContextBase)
        {
            Basket basket = GetBasketCookies(httpContextBase, false);
            basket.BasketItems.Clear();
            _basketContext.Commit();
        }
    }
}
