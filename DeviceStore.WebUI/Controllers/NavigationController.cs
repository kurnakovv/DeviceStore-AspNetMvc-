using DeviceStore.Domain.AbstractModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeviceStore.WebUI.Controllers
{
    public class NavigationController : Controller
    {
        private IDeviceRepository _deviceRepository;

        public NavigationController(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public PartialViewResult CategoryMenu()
        {
            IEnumerable<string> categories = _deviceRepository.Devices
                .Select(device => device.DeviceCategory)
                .Distinct()
                .OrderBy(x => x);

            return PartialView(categories);    
        }

        public PartialViewResult SearchDevices()
        {
            return PartialView();
        }
    }
}