using DeviceStore.Domain.Entities;
using DeviceStore.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceStore.Domain.Services.Interfaces
{
    public interface IOrderService 
    {
        void CreateNewOrder(Order order, List<BasketItemViewModel> basketItems);
    }
}
