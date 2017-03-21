using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebTinTuc.Context;
using WebTinTuc.Models;

namespace WebTinTuc.Controllers
{
    public class AccountController : Controller
    {
        private WebtintucContext db = new WebtintucContext();
        //
        // GET: /Account/
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.FullNameSortParm = String.IsNullOrEmpty(sortOrder) ? "fullname_desc" : "";
            ViewBag.UsernameSortParm = sortOrder == "Username" ? "username_desc" : "Username";
            ViewBag.PasswordSortParm = sortOrder == "Password" ? "password_desc" : "Password";
            ViewBag.DecendalizationSortParm = sortOrder == "Decendalization" ? "decendalization_desc" : "Decendalization";
            ViewBag.RegistrationDateSortParm = sortOrder == "RegistrationDate" ? "registrationdate_desc" : "RegistrationDate";
            ViewBag.StatusSortParm = sortOrder == "Status" ? "status_desc" : "Status";
            var accounts = from a in db.Accounts
                          select a;
            if (!String.IsNullOrEmpty(searchString))
            {
                accounts = accounts.Where(a => a.Member.FullName.Contains(searchString)
                                       || a.Username.Contains(searchString)
                                       || a.Password.Contains(searchString)
                                       || a.Decendalization.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "fullname_desc":
                    accounts = accounts.OrderByDescending(a => a.Member.FullName);
                    break;
                case "Username":
                    accounts = accounts.OrderBy(a => a.Username);
                    break;
                case "username_desc":
                    accounts = accounts.OrderByDescending(a => a.Username);
                    break;
                case "Password":
                    accounts = accounts.OrderBy(a => a.Password);
                    break;
                case "password_desc":
                    accounts = accounts.OrderByDescending(a => a.Password);
                    break;
                case "Decendalization":
                    accounts = accounts.OrderBy(a => a.Decendalization);
                    break;
                case "decendalization_desc":
                    accounts = accounts.OrderByDescending(a => a.Decendalization);
                    break;
                case "RegistrationDate":
                    accounts = accounts.OrderBy(a => a.RegistrationDate);
                    break;
                case "registrationdate_desc":
                    accounts = accounts.OrderByDescending(a => a.RegistrationDate);
                    break;
                case "Status":
                    accounts = accounts.OrderBy(a => a.Status);
                    break;
                case "status_desc":
                    accounts = accounts.OrderByDescending(a => a.Status);
                    break;             
                default:
                    accounts = accounts.OrderBy(a => a.Member.FullName);
                    break;
            }
            return View(accounts.ToList());
        }

        //
        // GET: /Account/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Account account = db.Accounts.Find(id);
            if (account == null)
                return HttpNotFound();
            return View(account);
        }

        //
        // GET: /Account/Create
        public ActionResult Create()
        {
            List<Member> members = db.Members.ToList();
            List<SelectListItem> ltsmember = new List<SelectListItem>();
            foreach (var m in members)
            {
                ltsmember.Add(new SelectListItem
                {
                    Text = m.FullName,
                    Value = m.MemberID.ToString()
                });
            }

            ViewBag.memberdrop = ltsmember;
            return View();
        }

        //
        // POST: /Account/Create
        [HttpPost]
        public ActionResult Create(Account account)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    db.Accounts.Add(account);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(account);
        }

        //
        // GET: /Account/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Account account = db.Accounts.Find(id);
            if (account == null)
                return HttpNotFound();

            List<Member> members = db.Members.Where(m => m.MemberID != account.MemberID) .ToList();
            List<SelectListItem> ltsmember = new List<SelectListItem>();
            foreach (var m in members)
            {
                ltsmember.Add(new SelectListItem
                {
                    Text = m.FullName,
                    Value = m.MemberID.ToString()
                });
            }

            ViewBag.memberdrop = ltsmember;
            return View(account);            
        }

        //
        // POST: /Account/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var memberToUpdate = db.Members.Find(id);
            if (TryUpdateModel(memberToUpdate, "", new string[] { "Email", "FullName", "Birthday", "Gender", "IdentityCard", "Address", "PhoneNumber" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(memberToUpdate);
        }

        //
        // GET: /Account/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (saveChangesError.GetValueOrDefault())
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            Account account = db.Accounts.Find(id);
            if (account == null)
                return HttpNotFound();
            return View(account);
        }

        //
        // POST: /Account/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                //Member student = db.Members.Find(id);
                //db.Members.Remove(student);
                Account accountToDelete = new Account() { AccountID = id };
                db.Entry(accountToDelete).State = EntityState.Deleted;
                db.SaveChanges();
            }
            catch (DataException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
