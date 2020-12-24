using DeviceStore.Domain.AbstractModel;
using DeviceStore.Domain.Entities;
using DeviceStore.Domain.Services;
using DeviceStore.Domain.Services.Interfaces;
using DeviceStore.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace DeviceStore.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
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

        public ActionResult DevicesSearch(string deviceName)
        {
            IEnumerable<Device> devices = _homeService.DeviceSearch(deviceName);

            if (devices != null)
            {
                return View(devices);
            }
            else
            {
                return View("DevicesNotFound");
            }
        }

        public ActionResult DeviceDetails(string id)
        {
            if (id != null)
            {
                Device device = _homeService.GetDeviceById(id);
                return PartialView(device);
            }

            return HttpNotFound();
        }
    }
}