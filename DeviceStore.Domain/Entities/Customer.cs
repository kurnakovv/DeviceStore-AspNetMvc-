using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceStore.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string UserId { get; set; }
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "Город")]
        public string City { get; set; }

        [Display(Name = "Улица")]
        public string Street { get; set; }

        [Display(Name = "Номер дома")]
        public string HouseNumber { get; set; }

        [Display(Name = "Номер квартиры")]
        public string ApartmentNumber { get; set; }

        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Электронный адрес на который выслать чек покупки")]
        public string Email { get; set; }

        [Display(Name = "Подарочная упоковка")]
        public bool GiftWrap { get; set; }
    }
}
