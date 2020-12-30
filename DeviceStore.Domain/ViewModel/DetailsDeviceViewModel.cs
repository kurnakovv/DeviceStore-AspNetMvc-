using DeviceStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceStore.Domain.ViewModel
{
    public class DetailsDeviceViewModel
    {
        public Device Device { get; set; }
        public IEnumerable<Device> Company { get; set; }
    }
}
