using DeviceStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceStore.Domain.Services.Interfaces
{
    public interface IAdminCompanyService
    {
        void AddOrUpdateCompany(Company company);
        Company GetCompanyById(string id);
        void DeleteById(Company company, string id);
    }
}
