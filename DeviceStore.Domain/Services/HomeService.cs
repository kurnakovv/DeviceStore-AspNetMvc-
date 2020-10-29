using DeviceStore.Domain.AbstractModel;
using DeviceStore.Domain.Entities;
using DeviceStore.Domain.Model;
using DeviceStore.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceStore.Domain.Services
{
    public class HomeService : IHomeService
    {
        private readonly IDeviceRepository _deviceRepository;
        private int pageSize = 5;

        public HomeService(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public IEnumerable<Device> ListDevices(string categoryDevices, int page)
        {
            return _deviceRepository.Devices.
                        Where(c => categoryDevices == null
                              || c.DeviceCategory == categoryDevices)
                        .OrderBy(device => device.Id)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize);
        }

        public PagingInfo PagingInfo(string categoryDevices, int page = 1)
        {
            var pagingInfo = new PagingInfo
            {
                CurrentPage = page,
                DevicesPerPage = pageSize,
                TotalDevices = categoryDevices == null ?
                    _deviceRepository.Devices.Count() :
                    _deviceRepository.Devices.Where(device => device.DeviceCategory == categoryDevices)
                                             .Count()
            };

            return pagingInfo;
        }

        public IEnumerable<Device> DeviceSearch(string deviceName)
        {
            IEnumerable<Device> devices = from m in _deviceRepository.Devices
                                         select m;

            if (devices.Where(x => x.DeviceName.Contains(deviceName)).Count() != 0 && !string.IsNullOrEmpty(deviceName))
            {
                devices = devices.Where(s => s.DeviceName.Contains(deviceName));
            }
            else
            {
                devices = null;
            }

            return devices;
        }
    }
}
