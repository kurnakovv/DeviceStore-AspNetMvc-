using DeviceStore.Domain.AbstractModel;
using DeviceStore.Domain.Entities;
using DeviceStore.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeviceStore.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminCompanyController : Controller
    {
        private readonly IAdminCompanyService _adminCompanyService;
        private readonly IRepository<Company> _companyRepository;

        public AdminCompanyController(IAdminCompanyService adminCompanyService,
                                      IRepository<Company> companyRepository)
        {
            _adminCompanyService = adminCompanyService;
            _companyRepository = companyRepository;
        }

        public ActionResult Index()
        {
            List<Company> customersList = _companyRepository.Collection().ToList();
            return View(customersList);
        }

        public ActionResult CreateCompany()
        {
            return View(new Company());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCompany(Company company, HttpPostedFileBase companyImage)
        {
            if (ModelState.IsValid)
            {
                if (companyImage != null)
                {
                    AddImage(company, companyImage);
                }

                _adminCompanyService.AddOrUpdateCompany(company);
                TempData["messageEdit"] = string.Format("Создана компания \"{0}\"", company.Name);

                return RedirectToAction("Index");
            }

            return View(company);
        }

        public ActionResult EditCompany(string id)
        {
            return View(_adminCompanyService.GetCompanyById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCompany(Company company, HttpPostedFileBase companyImage)
        {
            if (ModelState.IsValid)
            {
                if (companyImage != null)
                {
                    AddImage(company, companyImage);
                }

                _adminCompanyService.AddOrUpdateCompany(company);
                TempData["messageEdit"] = string.Format("Изменения в компании \"{0}\" были сохранены", company.Name);
            }

            return RedirectToAction("Index");
        }

        public ActionResult DetailsCompany(string id)
        {
            return PartialView(_adminCompanyService.GetCompanyById(id));
        }        

        public ActionResult DeleteCompany(string id)
        {
            return PartialView(_adminCompanyService.GetCompanyById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCompany(Company company, string id)
        {
            _adminCompanyService.DeleteById(company, id);
            TempData["messageRemove"] = string.Format("Удалена компания");
            return RedirectToAction("Index");
        }

        private void AddImage(Company company, HttpPostedFileBase companyImage)
        {
            if (companyImage != null)
            {
                var allowedExtensions = new List<string> { ".jpeg", ".png", ".jpg" };
                var fileExtension = Path.GetExtension(companyImage.FileName);
                if (allowedExtensions.Contains(fileExtension))
                {
                    var fileName = Path.GetFileName(companyImage.FileName);
                    var directoryToSave = Server.MapPath(Url.Content("~/Content/CompanyImages/"));

                    var pathToSave = Path.Combine(directoryToSave, fileName);
                    companyImage.SaveAs(pathToSave);
                    company.Image = fileName;
                }
            }
        }
    }
}