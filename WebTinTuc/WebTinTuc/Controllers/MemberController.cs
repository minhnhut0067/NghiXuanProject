﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebTinTuc.Context;
using WebTinTuc.Models;
using PagedList;
using System.Data.Entity.Infrastructure;

namespace WebTinTuc.Controllers
{
    public class MemberController : Controller
    {
        private WebtintucContext db = new WebtintucContext();
        //
        // GET: /Member/
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.FullNameSortParm = String.IsNullOrEmpty(sortOrder) ? "fullname_desc" : "";
            ViewBag.EmailSortParm = sortOrder == "Email" ? "email_desc" : "Email";
            ViewBag.BirthdaySortParm = sortOrder == "Birthday" ? "birthday_desc" : "Birthday";
            ViewBag.GenderSortParm = sortOrder == "Gender" ? "gender_desc" : "Gender";
            ViewBag.IdentityCardSortParm = sortOrder == "IdentityCard" ? "identitycard_desc" : "IdentityCard";
            ViewBag.AddressSortParm = sortOrder == "Address" ? "address_desc" : "Address";
            ViewBag.PhoneNumberSortParm = sortOrder == "PhoneNumber" ? "phonenumber_desc" : "PhoneNumber";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var members = from m in db.Members
                           select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                members = members.Where(m => m.FullName.Contains(searchString)
                                       || m.Email.Contains(searchString)
                                       || m.Address.Contains(searchString)
                                       || m.Gender.Contains(searchString)
                                       || m.IdentityCard.Contains(searchString)
                                       || m.PhoneNumber.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "fullname_desc":
                    members = members.OrderByDescending(m => m.FullName);
                    break;
                case "Email":
                    members = members.OrderBy(m => m.Email);
                    break;
                case "email_desc":
                    members = members.OrderByDescending(m => m.Email);
                    break;
                case "Birthday":
                    members = members.OrderBy(m => m.Birthday);
                    break;
                case "birthday_desc":
                    members = members.OrderByDescending(m => m.Birthday);
                    break;
                case "Gender":
                    members = members.OrderBy(m => m.Gender);
                    break;
                case "gender_desc":
                    members = members.OrderByDescending(m => m.Gender);
                    break;
                case "IdentityCard":
                    members = members.OrderBy(m => m.IdentityCard);
                    break;
                case "identitycard_desc":
                    members = members.OrderByDescending(m => m.IdentityCard);
                    break;
                case "Address":
                    members = members.OrderBy(m => m.Address);
                    break;
                case "address_desc":
                    members = members.OrderByDescending(m => m.Address);
                    break;
                case "PhoneNumber":
                    members = members.OrderBy(m => m.PhoneNumber);
                    break;
                case "phonenumber_desc":
                    members = members.OrderByDescending(m => m.PhoneNumber);
                    break;
                default:
                    members = members.OrderBy(m => m.FullName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(members.ToPagedList(pageNumber, pageSize));
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
            catch (RetryLimitExceededException)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
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
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(memberToUpdate);
        }

        //
        // GET: /Member/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
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
            catch (RetryLimitExceededException/* dex */)
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
