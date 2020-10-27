using DeviceStore.Domain.AbstractModel;
using DeviceStore.Domain.Entities;
using DeviceStore.Domain.Services;
using DeviceStore.Domain.Services.Interfaces;
using DeviceStore.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DeviceStore.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;
        private IDeviceRepository _deviceRepository;
        public int pageSize = 5;       

        public HomeController(IDeviceRepository deviceRepository, IHomeService homeService)
        {
            _deviceRepository = deviceRepository;
            _homeService = homeService;
        }

        public ViewResult Index(string categoryDevices, int page = 1)
        {
            FilteredDevicesListViewModel filteredDevicesList = new FilteredDevicesListViewModel
            {
                CurrentCategory = categoryDevices,

                Devices = _homeService.ListDevices(categoryDevices, page),

                PagingInfo = _homeService.PagingInfo(categoryDevices, page),
            };

            return View(filteredDevicesList);
        }

        public ActionResult DevicesSearch(string deviceName, IEnumerable<Device> devices)
        {
            if (!string.IsNullOrEmpty(deviceName))
            {
                devices = _homeService.DeviceSearch(deviceName);
            }
            else
            {
                // TODO: Devices not found (finish up)
                return View("DevicesNotFound");
            }

            return View(devices);
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