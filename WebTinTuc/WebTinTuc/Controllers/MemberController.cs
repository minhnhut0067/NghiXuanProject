using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebTinTuc.Context;
using WebTinTuc.Models;

namespace WebTinTuc.Controllers
{
    public class MemberController : Controller
    {
        private WebtintucContext db = new WebtintucContext();
        //
        // GET: /Member/
        public ActionResult Index()
        {
            return View(db.Members.ToList());
        }

        //
        // GET: /Member/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Member member = db.Members.Find(id);
            if (member == null)
                return HttpNotFound();
            return View(member);
        }

        //
        // GET: /Member/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Member/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Member member)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    db.Members.Add(member);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(member);
        }

        //
        // GET: /Member/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Member member = db.Members.Find(id);
            if (member == null)
                return HttpNotFound();
            return View(member);
        }

        //
        // POST: /Member/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id)
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
        // GET: /Member/Delete/5
        public ActionResult Delete(int id, bool? saveChangesError = false)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (saveChangesError.GetValueOrDefault())
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            Member member = db.Members.Find(id);
            if (member == null)
                return HttpNotFound();
            return View(member);
        }

        //
        // POST: /Member/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                //Member student = db.Members.Find(id);
                //db.Members.Remove(student);
                Member memberToDelete = new Member() { MemberID = id };
                db.Entry(memberToDelete).State = EntityState.Deleted;
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
