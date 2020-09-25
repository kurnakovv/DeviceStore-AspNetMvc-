using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceStore.Domain.Entities
{
    public class BasketItem : BaseEntity
    {
        public string BasketId { get; set; }
        public string DeviceId { get; set; }
        public int Quantity { get; set; }

    }
}
