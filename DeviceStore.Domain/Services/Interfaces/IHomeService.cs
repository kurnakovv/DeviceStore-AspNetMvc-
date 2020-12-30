using DeviceStore.Domain.Entities;
using DeviceStore.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceStore.Domain.Services.Interfaces
{
    public interface IHomeService
    {
        IEnumerable<Device> ListDevices(string categoryDevices, int page);
        PagingInfo PagingInfo(string categoryDevices, int page);
        IEnumerable<Device> DeviceSearch(string deviceName);
        Device GetDeviceById(string id);
        IEnumerable<Device> DeviceCompany();
    }
}
