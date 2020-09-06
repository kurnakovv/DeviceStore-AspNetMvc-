using DeviceStore.Domain.AbstractModel;
using DeviceStore.Domain.Entities;
using DeviceStore.WebUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DeviceStore.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IDeviceRepository _deviceRepository;

        public HomeController(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public ViewResult Index(string categoryDevices)
        {
            var filteredDevicesList = new FilteredDevicesList();           

            filteredDevicesList.CurrentCategory = categoryDevices;

            return View(_deviceRepository.Devices
                       .Where(c => categoryDevices == null || c.DeviceCategory == categoryDevices));
        }

        public ActionResult DevicesSearch(string deviceName)
        {
            IEnumerable<Device> device = from m in _deviceRepository.Devices
                                         select m;

            if (!string.IsNullOrEmpty(deviceName))
            {
                device = device.Where(s => s.DeviceName.Contains(deviceName));
            }
            else
            {
                // TODO: Devices not found (finish up)
                return View("DevicesNotFound");
            }

            return View(device);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}