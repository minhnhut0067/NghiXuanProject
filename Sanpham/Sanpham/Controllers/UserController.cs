using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Sanpham.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        SoftwareTechnologyEntities db = new SoftwareTechnologyEntities();
        public ActionResult Index()
        {
            return View(db.Accounts.ToList());
        }

        public ActionResult Create()
        {
            IEnumerable<SelectListItem> memberid = db.Members.Select(
               b => new SelectListItem { Value = b.MemberID.ToString(), Text = b.FullName });
            ViewData["MemberID"] = memberid;
            return View();
        }
        [HttpPost]
        public ActionResult Create(Account account)
        {
            if (ModelState.IsValid)
            {
                db.Accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(account);
        }

        //GET User/Edit/5
        public ActionResult Edit(string username)
        {
            if (username == null || username == "")
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Account account = db.Accounts.Find(username);
            IEnumerable<SelectListItem> memberid = db.Members.Select(
               b => new SelectListItem { Value = b.MemberID.ToString(), Text = b.FullName });

            ViewData["MemberID"] = memberid;
            if (account == null)
                return HttpNotFound();
            return View(account);
        }
        //POST User/Edit/5
        [HttpPost]
        public ActionResult Edit(Account account)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(account).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(account);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Details(string username)
        {
            if (username == null || username == "")
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Account account = db.Accounts.Find(username);

            if (account == null)
                return HttpNotFound();
            return View(account);
        }
    }
}