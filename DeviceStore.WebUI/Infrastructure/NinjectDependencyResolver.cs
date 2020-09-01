using DeviceStore.Domain.AbstractModel;
using DeviceStore.Domain.EFConcrete;
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
        }
    }
}