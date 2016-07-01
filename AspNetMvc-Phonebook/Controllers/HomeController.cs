using AspNetMvc_Phonebook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetMvc_Phonebook.Controllers
{
    public class HomeController : Controller
    {
        PhonebookContext db = new PhonebookContext();

        // GET: Home
        public ActionResult Index(string sortOrder)
        {
            ViewBag.PhoneNumberSortParam = String.IsNullOrEmpty(sortOrder) ? "phoneNumber_desc" : "phoneNumber";
            ViewBag.FirstNameSortParam = sortOrder == "firstName" ? "firstName_desc" : "firstName";
            ViewBag.LastNameSortParam = sortOrder == "lastName" ? "lastName_desc" : "lastName";
            ViewBag.EmailSortParam = sortOrder == "email" ? "email_desc" : "email";

            ViewBag.CurrentSort = sortOrder;

            var contacts = from c in db.Contacts
                           select c;

            switch (sortOrder)
            {
                case "phoneNumber":
                    contacts = contacts.OrderBy(c => c.PhoneNumber);
                    break;
                case "phoneNumber_desc":
                    contacts = contacts.OrderByDescending(c => c.PhoneNumber);
                    break;
                case "firstName":
                    contacts = contacts.OrderBy(c => c.FirstName);
                    break;
                case "firstName_desc":
                    contacts = contacts.OrderByDescending(c => c.FirstName);
                    break;
                case "lastName":
                    contacts = contacts.OrderBy(c => c.LastName);
                    break;
                case "lastName_desc":
                    contacts = contacts.OrderByDescending(c => c.LastName);
                    break;
                case "email":
                    contacts = contacts.OrderBy(c => c.Email);
                    break;
                case "email_desc":
                    contacts = contacts.OrderByDescending(c => c.Email);
                    break;
                default:
                    break;

            }

            return View(contacts.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}