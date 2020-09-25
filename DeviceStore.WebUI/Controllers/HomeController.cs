using DeviceStore.Domain.AbstractModel;
using DeviceStore.Domain.Entities;
using DeviceStore.WebUI.Models;
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
        public int pageSize = 5;

        public HomeController(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public ViewResult Index(string categoryDevices, int page = 1)
        {
            FilteredDevicesList filteredDevicesList = new FilteredDevicesList
            {
                CurrentCategory = categoryDevices,

                Devices = _deviceRepository.Devices.
                        Where(c => categoryDevices == null
                              || c.DeviceCategory == categoryDevices)
                        .OrderBy(device => device.Id)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    DevicesPerPage = pageSize,
                    TotalDevices = categoryDevices == null ?
                    _deviceRepository.Devices.Count() :
                    _deviceRepository.Devices.Where(device => device.DeviceCategory == categoryDevices).Count()
                },
            };

            return View(filteredDevicesList);
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