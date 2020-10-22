using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceStore.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public string OrderId { get; set; }
        public string DeviceId { get; set; }
        public string DeviceName { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
    }
}
