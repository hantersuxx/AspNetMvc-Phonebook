using AspNetMvc_Phonebook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace AspNetMvc_Phonebook.Controllers
{
    public class HomeController : Controller
    {
        PhonebookContext db = new PhonebookContext();

        // GET: Home
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.PhoneNumberSortParam = String.IsNullOrEmpty(sortOrder) ? "phoneNumber_desc" : "";
            ViewBag.FirstNameSortParam = sortOrder == "firstName" ? "firstName_desc" : "firstName";
            ViewBag.LastNameSortParam = sortOrder == "lastName" ? "lastName_desc" : "lastName";
            ViewBag.EmailSortParam = sortOrder == "email" ? "email_desc" : "email";

            //if the search string is changed during paging
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var contacts = from c in db.Contacts
                           select c;

            //search
            if (!String.IsNullOrEmpty(searchString))
            {
                contacts = contacts.Where(c => c.PhoneNumber.Contains(searchString));
            }

            //sort
            switch (sortOrder)
            {
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
                    contacts = contacts.OrderBy(c => c.PhoneNumber);
                    break;

            }

            int pageSize = 3;
            //if page is null pageNumber=1
            int pageNumber = (page ?? 1);
            return View(contacts.ToPagedList(pageNumber, pageSize));
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}