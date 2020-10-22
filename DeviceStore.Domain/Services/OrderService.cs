using DeviceStore.Domain.AbstractModel;
using DeviceStore.Domain.Entities;
using DeviceStore.Domain.Services.Interfaces;
using DeviceStore.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceStore.Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;

        public OrderService(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void CreateNewOrder(Order order, List<BasketItemViewModel> basketItems)
        {
            foreach(BasketItemViewModel basketItemViewModel in basketItems)
            {
                order.OrderItems.Add(new OrderItem()
                {
                    DeviceId = basketItemViewModel.BasketItemId,
                    DeviceName = basketItemViewModel.DeviceName,
                    Price = basketItemViewModel.Price,
                    Image = basketItemViewModel.Image,
                    Quantity = basketItemViewModel.QuantityDevicesItems,
                });
            }

            _orderRepository.Insert(order);
            _orderRepository.Commit();
        }
    }
}
