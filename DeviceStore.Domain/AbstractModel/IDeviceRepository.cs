using DeviceStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceStore.Domain.AbstractModel
{
    public interface IDeviceRepository
    {
        IEnumerable<Device> Devices { get; }
        void SaveOrEditDevice(Device device);
        Device DeleteDevice(string deviceId);
        IEnumerable<Device> DeviceCompany();
    }
}
