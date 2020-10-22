using DeviceStore.Domain.AbstractModel;
using DeviceStore.Domain.EFConcrete;
using DeviceStore.Domain.Entities;
using DeviceStore.Domain.Services;
using DeviceStore.Domain.Services.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeviceStore.WebUI.Infrastructure
{
    internal class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel _kernel;

        internal NinjectDependencyResolver(IKernel kernelParam)
        {
            _kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            _kernel.Bind<IDeviceRepository>().To<DeviceRepository>();
            _kernel.Bind<IBasketService>().To<BasketService>();
            _kernel.Bind<IAdminService>().To<AdminService>();
            _kernel.Bind<IOrderService>().To<OrderService>();

            _kernel.Bind<IRepository<Basket>>().To<MemoryRepository<Basket>>();
            _kernel.Bind<IRepository<BasketItem>>().To<MemoryRepository<BasketItem>>();
            _kernel.Bind<IRepository<Device>>().To<MemoryRepository<Device>>();
            _kernel.Bind<IRepository<Customer>>().To<MemoryRepository<Customer>>();
            _kernel.Bind<IRepository<Order>>().To<MemoryRepository<Order>>();
        }
    }
}