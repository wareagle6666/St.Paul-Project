using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApplication2.Models;



namespace WebApplication2.Controllers
{
    [Authorize]
    public class ContactsController : Controller
    {
        // GET: Contacts
        public ActionResult Index(string search)
        {
            IList<Contacts> FinalList;

            Contacts contacts = new Contacts();

            if(search == null)
            {
                FinalList = contacts.GetContacts().ToList();
            }
            else
            {
                var currentList = contacts.GetContacts().ToList();
                FinalList = currentList.Where(x => x.FirstName.ToLower().Contains(search.ToLower()) || x.LastName.ToLower().Contains(search.ToLower())).ToList();
            } 

            return View(FinalList);
        }

        // GET: Contacts/Details/5
        public ActionResult Details(string id)
        {
            IList<Contacts> contacts = new Contacts().GetContacts();

            foreach (var contact in contacts)
            {
                if (contact.Id == id)
                {
                    ViewBag.FirstName = contact.FirstName;
                    ViewBag.LastName = contact.LastName;
                    ViewBag.Email = contact.Email;
                    ViewBag.PhoneNumber = contact.PhoneNumber;
                    ViewBag.UserName = contact.UserName;
                    ViewBag.UserID = contact.Id;
                }
            }

            Address address = new Address().GetAddress(id);
            if (address != null)
            {
                ViewBag.AddressID = address.ID;
                ViewBag.Street1 = address.Street1;
                ViewBag.Street2 = address.Street2;
                ViewBag.City = address.City;
                ViewBag.State = address.State;
                ViewBag.Zipcode = address.Zipcode;
            }



            IList<Family> family = new Family().GetFamilies(id);

            return View(family);
        }

        // GET: Contacts/Create
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Address(string AddressID, string UserID)
        {
            Address address;

            if (AddressID == null)
            {
                address = new Address();
                ViewBag.AddressID = null;
                ViewBag.UserID = UserID;
                address.ID = null;
                address.Street1 = null;
                address.Street2 = null;
                address.City = null;
                address.State = null;
                address.Zipcode = null;
                return View(address);
            }
            else
            {
                ViewBag.AddressID = AddressID;
                address = new Address().GetAddressByID(AddressID);
             
            }
            return View(address);
        }
        // POST: Contacts/Create
        [HttpPost]
        public ActionResult AddressCreate(FormCollection collection)
        {
            try
            {
                var Street1 = collection["Street1"];
                var Street2 = collection["Street2"];
                var City = collection["City"];
                var State = collection["State"];
                var Zipcode = collection["Zipcode"];
                var UserID = collection["UserID"];
                var address = new Address().CreateAddress(UserID, Street1, Street2, City, State, Zipcode);
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ErrorSignal.FromCurrentContext().Raise(e);
                return View();
            }
        }

        // GET: Contacts/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Contacts/Edit/5
        [HttpPost]
        public ActionResult AddressEdit( FormCollection collection)
        {
            try
            {
                var Street1 = collection["Street1"];
                var Street2 = collection["Street2"];
                var City = collection["City"];
                var State = collection["State"];
                var Zipcode = collection["Zipcode"];
                var AddressID = collection["AddressID"];
                var UserID = collection["UserID"];

                var address = new Address().UpdateAddress(AddressID, Street1, Street2, City, State, Zipcode);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ErrorSignal.FromCurrentContext().Raise(e);
                return View();
            }
        }

        // GET: Contacts/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Contacts/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ErrorSignal.FromCurrentContext().Raise(e);
                return View();
            }
        }
    }
}
