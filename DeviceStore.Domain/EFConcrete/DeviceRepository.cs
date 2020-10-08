using DeviceStore.Domain.AbstractModel;
using DeviceStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceStore.Domain.EFConcrete
{
    public class DeviceRepository : IDeviceRepository
    {
        AppDbContext appDbContext = new AppDbContext();
        
        public IEnumerable<Device> Devices { get => appDbContext.Devices; }

        public void SaveOrEditDevice(Device device)
        {
            if(device.Id != null)
            {
                Device deviceContext = appDbContext.Devices.Find(device.Id);

                if(deviceContext != null)
                {
                    deviceContext.DeviceName = device.DeviceName;
                    deviceContext.DeviceDescription = device.DeviceDescription;
                    deviceContext.DeviceCategory = device.DeviceCategory;
                    deviceContext.DeviceQuantity = device.DeviceQuantity;
                    deviceContext.DeviceFavorites = device.DeviceFavorites;
                    deviceContext.DevicePrice = device.DevicePrice;
                    deviceContext.DeviceImage = device.DeviceImage;
                }
                else if(deviceContext == null)
                {
                    appDbContext.Devices.Add(device);
                }
            }
            appDbContext.SaveChanges();
        }

        public Device DeleteDevice(string deviceId)
        {
            Device device = appDbContext.Devices.Find(deviceId);

            if(device != null)
            {
                appDbContext.Devices.Remove(device);
                appDbContext.SaveChanges();
            }
            return device;
        }        
    }
}
