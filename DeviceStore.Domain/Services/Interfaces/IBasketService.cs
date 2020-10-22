using DeviceStore.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DeviceStore.Domain.Services.Interfaces
{
    public interface IBasketService
    {
        void AddToBasket(HttpContextBase httpContextBase, string deviceId);
        void RemoveFromBasket(HttpContextBase httpContextBase, string itemId);
        List<BasketItemViewModel> GetBasketItems(HttpContextBase httpContextBase);
        BasketSummaryViewModel GetBasketSummary(HttpContextBase httpContext);
        void ClearBasket(HttpContextBase httpContextBase);
    }
}
