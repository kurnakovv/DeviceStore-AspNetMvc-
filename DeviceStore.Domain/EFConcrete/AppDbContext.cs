using DeviceStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceStore.Domain.EFConcrete
{
    public class AppDbContext : DbContext
    {
        public DbSet<Device> Devices { get; set; }
    }
}
