using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTinTuc.Context;
using WebTinTuc.ViewModels;

namespace WebTinTuc.Controllers
{
    public class HomeController : Controller
    {
        private WebtintucContext db = new WebtintucContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            IQueryable<MemberBirthdayGroup> data = from member in db.Members
                                                   group member by member.Birthday into birthdayGroup
                                                   select new MemberBirthdayGroup()
                                                   {
                                                       MemberBirthday = birthdayGroup.Key,
                                                       MemberCount = birthdayGroup.Count()
                                                   };
            return View(data.ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}