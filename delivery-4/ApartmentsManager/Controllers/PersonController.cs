using System.Linq;
using System.Net;
using System.Data.Entity;
using System.Web.Mvc;
using System.Collections.Generic;

namespace ApartmentsManager.Controllers
{
    public class PersonController : Controller
    {
        ~PersonController()
        {
            if (db != null)
            {
                db.Dispose();
            }
        }
        private readonly ModelContainer db = new ModelContainer();

        // GET: Person
        public ActionResult Index()
        {
            return View(db.People);
        }

        // GET: Person/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People
                .SingleOrDefault(p => p.IDPerson == id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // GET: Person/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Person/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "FirstName, LastName, Age, Email")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.People.Add(person);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: Person/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People
                .SingleOrDefault(p => p.IDPerson == id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: Person/Edit/5
        [HttpPost]
        [ActionName("Edit")]
        public ActionResult EditConfirmed(int id)
        {
            Person personToUpdate = db.People.Find(id);
            if (TryUpdateModel(personToUpdate, "",
                new string[] { "FirstName", "LastName", "Age", "Email", "Pets" }))
            {

                db.Entry(personToUpdate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(personToUpdate);
        }

        // GET: Person/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People
                .SingleOrDefault(p => p.IDPerson == id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: Person/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = db.People.Find(id);
            IEnumerable <Pet> pets = db.Pets.Where(pet => pet.OwnerID == id);
            foreach (Pet pet in pets)
            {
                db.UploadedFiles.RemoveRange(db.UploadedFiles.Where(file => file.PetID == pet.IDPet));
            }

            db.Pets.RemoveRange(pets);
            db.People.Remove(db.People.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
