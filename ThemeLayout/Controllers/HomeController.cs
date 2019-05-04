using System;
using System.Linq;
using System.Web.Mvc;
using ThemeLayout.Models.ViewModels;

namespace ThemeLayout.Controllers
{
    public class HomeController : Controller
    {
        private ProjectDbContext _db = new ProjectDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult HomePage()
        {
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [HttpPost]
        public ActionResult Search(SearchVm searchVm)
        {

            var searchTransportData = _db.Transports.AsQueryable();



            if (!string.IsNullOrEmpty(searchVm.From))
            {
                searchTransportData = searchTransportData.Where(c => c.From.Contains(searchVm.From));
            }





            if (searchVm.Date != null)
            {
                DateTime date = (DateTime)searchVm.Date;
                searchTransportData = searchTransportData.Where(c => c.DateTime.Date == date.Date);

            }

            if (searchVm.BudgetFrom > 0)
            {
                searchTransportData = searchTransportData.Where(c => c.Price >= searchVm.BudgetFrom);

            }

            if (searchVm.BudgetTo > 0)
            {
                searchTransportData = searchTransportData.Where(c => c.Price <= searchVm.BudgetTo);
            }


            searchVm.Transports = searchTransportData.ToList();

            var searchHotelData = _db.Hotels.AsQueryable();
            if (!string.IsNullOrEmpty(searchVm.To))
            {
                searchHotelData = searchHotelData.Where(c => c.Location.Contains(searchVm.To));
            }
            if (searchVm.BudgetFrom > 0)
            {
                searchHotelData = searchHotelData.Where(c => c.Price >= searchVm.BudgetFrom);

            }

            if (searchVm.BudgetTo > 0)
            {
                searchHotelData = searchHotelData.Where(c => c.Price <= searchVm.BudgetTo);
            }
            searchVm.Hotels = searchHotelData.ToList();
            return View(searchVm);


        }


    }
}