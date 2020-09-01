using DeviceStore.Domain.AbstractModel;
using System.Web.Mvc;

namespace DeviceStore.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IDeviceRepository _deviceRepository;

        public HomeController(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public ActionResult Index()
        {
            return View(_deviceRepository.Devices);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}