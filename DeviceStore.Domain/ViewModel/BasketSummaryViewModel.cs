using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceStore.Domain.ViewModel
{
    public class BasketSummaryViewModel
    {
        public int BasketItems { get; set; }
        public decimal BasketTotal { get; set; }

        public BasketSummaryViewModel() { }
        public BasketSummaryViewModel(int basketItems, decimal basketTotal) 
        {
            BasketItems = basketItems;
            BasketTotal = basketTotal;
        }
    }
}
