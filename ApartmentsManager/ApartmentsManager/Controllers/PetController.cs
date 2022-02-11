using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;
using System.Web.UI;

namespace ApartmentsManager.Controllers
{
    public class PetController : Controller
    {
        ~PetController()
        {
            if (db!=null)
            {
                db.Dispose();
            }
        }
        private readonly ModelContainer db = new ModelContainer();
        HttpCookie cookie = new HttpCookie("Warning");
        // GET: Pet
        public ActionResult Index()
        {
            if (db.People.Count() < 1)
            {
                Response.Write("<script>alert('You need to create at least 1 person before you can create a pet')</script>");
            }
            return View(db.Pets);
        }

        // GET: Pet/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pet pet = db.Pets
                .Include(cg => cg.UploadedFiles)
                .SingleOrDefault(cg => cg.IDPet == id);
            if (pet == null)
            {
                return HttpNotFound();
            }
            return View(pet);
        }

        // GET: Pet/Create
        public ActionResult Create()
        {
            var people = db.People;
            if (people.Count() < 1)
            {
                return RedirectToAction("Index");
            }
            ViewBag.People = people;       
            return View();
        }

        // POST: Pet/Create
        [HttpPost]
        public ActionResult Create(Pet pet,
            IEnumerable<HttpPostedFileBase> files)
        {
            pet.Owner = db.People.Find(pet.OwnerID);
            if (ModelState.IsValid)
            {
                pet.UploadedFiles = new List<UploadedFile>();
                foreach (var file in files)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var picture = new UploadedFile
                        {
                            Name = Path.GetFileName(file.FileName),
                            ContentType = file.ContentType
                        };
                        using (var reader = new BinaryReader(file.InputStream))
                        {
                            picture.Content = reader.ReadBytes(file.ContentLength);
                        }
                        pet.UploadedFiles.Add(picture);
                    }
                }

                db.Pets.Add(pet);
                db.SaveChanges();

            }
            return RedirectToAction("Index");  
        }// GET: Pet

        // GET: Pet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Pet pet = db.Pets
                .Include(cg => cg.UploadedFiles)
                .SingleOrDefault(cg => cg.IDPet == id);
            if (pet == null)
            {
                return HttpNotFound();
            }

            ViewBag.People = db.People.Where(p => p.IDPerson != pet.OwnerID);

            return View(pet);
        }

        // POST: Pet/Edit/5
        [HttpPost]
        [ActionName("Edit")]
        public ActionResult EditConfirmed(int id,
            int ownerID,
            IEnumerable<HttpPostedFileBase> files)
        {
            Pet petToUpdate = db.Pets.Find(id);
            if (TryUpdateModel(petToUpdate, "",
                new string[] { "Location, FirstPlayer, SecondPlayer"}))
            {
                petToUpdate.Owner = db.People.Find(ownerID);

                if (petToUpdate.UploadedFiles == null)
                {
                    petToUpdate.UploadedFiles = new List<UploadedFile>(); 
                }
                foreach (var file in files)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var picture = new UploadedFile
                        {
                            Name = Path.GetFileName(file.FileName),
                            ContentType = file.ContentType
                        };
                        using (var reader = new BinaryReader(file.InputStream))
                        {
                            picture.Content = reader.ReadBytes(file.ContentLength);
                        }
                        petToUpdate.UploadedFiles.Add(picture);
                    }
                }

                db.Entry(petToUpdate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(petToUpdate);
        }

        // GET: Pet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pet pet = db.Pets
                .Include(cg => cg.UploadedFiles)
                .SingleOrDefault(cg => cg.IDPet == id);
            if (pet == null)
            {
                return HttpNotFound();
            }
            return View(pet);
        }

        // POST: Pet/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            db.UploadedFiles.RemoveRange(db.UploadedFiles.Where(file => file.PetID == id));
            db.Pets.Remove(db.Pets.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
