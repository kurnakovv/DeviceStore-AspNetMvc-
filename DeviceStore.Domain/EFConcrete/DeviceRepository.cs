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
    }
}
