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
        List<SelectListItem> searchList;


        public HomeController()
        {
            searchList = new List<SelectListItem>();
            SelectListItem option1 = new SelectListItem()
            {
                Text = "номеру телефона",
                Value = "phone",
                Selected = true
            };
            SelectListItem option2 = new SelectListItem()
            {
                Text = "имени или фамилии",
                Value = "name"
            };
            searchList.Add(option1);
            searchList.Add(option2);
            ViewBag.SearchSelect = searchList;
        }

        // GET: Home
        public ActionResult Index(string sortOrder, string searchSelect, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentSelect = searchSelect;
            ViewBag.CurrentSearch = searchString;
            ViewBag.PhoneNumberSortParam = String.IsNullOrEmpty(sortOrder) ? "phoneNumber_desc" : "";
            ViewBag.FirstNameSortParam = sortOrder == "firstName" ? "firstName_desc" : "firstName";
            ViewBag.LastNameSortParam = sortOrder == "lastName" ? "lastName_desc" : "lastName";
            ViewBag.EmailSortParam = sortOrder == "email" ? "email_desc" : "email";

            var contacts = from c in db.Contacts
                           select c;

            //search
            if (!String.IsNullOrEmpty(searchString))
            {
                switch (searchSelect)
                {
                    case "phone":
                        contacts = contacts.Where(c => c.PhoneNumber.Contains(searchString));
                        break;
                    case "name":
                        contacts = contacts.Where(c => c.FirstName.Contains(searchString) || c.LastName.Contains(searchString));
                        break;
                }
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

            int pageSize = 10;
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