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
        public ActionResult Index()
        {
            return View(db.Contacts);
        }
    }
}