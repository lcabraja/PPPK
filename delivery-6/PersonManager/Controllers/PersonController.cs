using PersonManager.Dal;
using PersonManager.Models;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PersonManager.Controllers
{
    public class PersonController : Controller
    {
        private static readonly ICosmosDbService service = CosmosDbServiceProvider.CosmosDbService;
        // GET: Person
        public async Task<ActionResult> Index()
        {
            return View(await service.GetPeopleAsync("SELECT * FROM Person"));
        }

        // GET: Person/Details/5
        public async Task<ActionResult> Details(string id)
        {
            return await ShowPerson(id);
        }

        private async Task<ActionResult> ShowPerson(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var person = await service.GetPersonAsync(id);
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
        public async Task<ActionResult> Create([Bind(Include = "FirstName, LastName, Age, Email")] Person person)
        {
            if (ModelState.IsValid)
            {
                person.Id = Guid.NewGuid().ToString();
                await service.AddPersonAsync(person);
                return RedirectToAction("Index");
            }
            return View(person);
        }

        // GET: Person/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            return await ShowPerson(id);
        }

        // POST: Person/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit([Bind(Include = "Id, FirstName, LastName, Age, Email")] Person person)
        {
            if (ModelState.IsValid)
            {
                await service.UpdatePersonAsync(person);
                return RedirectToAction("Index");
            }
            return View(person);
        }

        // GET: Person/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            return await ShowPerson(id);
        }

        // POST: Person/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete([Bind(Include = "Id, FirstName, LastName, Age, Email")] Person person)
        {   
            await service.DeletePersonAsync(person.Id);
            return RedirectToAction("Index");
        }
    }
}
