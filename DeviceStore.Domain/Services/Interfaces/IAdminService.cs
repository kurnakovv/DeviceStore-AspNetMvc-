using DeviceStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DeviceStore.Domain.Services.Interfaces
{
    public interface IAdminService
    {
        void EditDevice(Device device);
        void RemoveDevice(string id);
    }
}
