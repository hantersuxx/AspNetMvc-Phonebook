using AspNetMvc_Phonebook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Net;

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
                Value = "phone"
            };
            SelectListItem option2 = new SelectListItem()
            {
                Text = "имени или фамилии",
                Value = "name"
            };
            searchList.Add(new SelectListItem() { Value = "" });
            searchList.Add(option1);
            searchList.Add(option2);
            ViewBag.SearchSelect = searchList;
        }

        public ActionResult DisplayTable(string sortOrder, string searchSelect, string searchString, int? page)
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

            int pageSize = 15;
            //if page is null pageNumber=1
            int pageNumber = (page ?? 1);
            return PartialView(contacts.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        [Authorize(Users = "7732155@gmail.com")]
        public ActionResult Actions(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(id);
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Users = "7732155@gmail.com")]
        public ActionResult Delete(int? id)
        {
            var contact = db.Contacts.Find(id);
            if (id == null || contact == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.Contacts.Remove(contact);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Users = "7732155@gmail.com")]
        public ActionResult Edit(int? id)
        {
            var contact = db.Contacts.Find(id);
            if (id == null || contact == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(contact);
        }

        [HttpPost]
        [Authorize(Users = "7732155@gmail.com")]
        public ActionResult Edit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contact).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        [HttpGet]
        [Authorize(Users = "7732155@gmail.com")]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Users = "7732155@gmail.com")]
        public ActionResult Add(Contact contact)
        {
            if (ModelState.IsValid)
            {
                if (Matches(contact) == 0)
                {
                    db.Contacts.Add(contact);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(contact);
        }

        private int? Matches(Contact contact)
        {
            var contacts = from c in db.Contacts
                           where c.PhoneNumber == contact.PhoneNumber
                           select c;
            return contacts.Count();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}