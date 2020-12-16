using DeviceStore.Domain.AbstractModel;
using DeviceStore.Domain.Entities;
using DeviceStore.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceStore.Domain.Services
{
    public class AdminCompanyService : IAdminCompanyService
    {
        private readonly IRepository<Company> _companyRepository;

        public AdminCompanyService(IRepository<Company> companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public void AddOrUpdateCompany(Company company)
        {
            Company companyContext = _companyRepository.Find(company.Id);
            if (companyContext != null)
            {
                companyContext.Name = company.Name;
                companyContext.Image = company.Image;
                //_companyRepository.Update(company); // Dont work
                _companyRepository.Commit();
            }
            else if (companyContext == null)
            {
                _companyRepository.Insert(company);
                _companyRepository.Commit();
            }
        }

        public void DeleteById(Company company, string id)
        {
            if (id != null)
            {
                // TODO: Edit the exception when we delete a company that already has devices.
                if (company.DevicesList.Count == 0)
                {
                    _companyRepository.Delete(id);
                    _companyRepository.Commit();
                }
            }
        }

        public Company GetCompanyById(string id)
        {
            return _companyRepository.Find(id);
        }
    }
}
