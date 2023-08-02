using HospitalProject.Class;
using HospitalProject.DataAccess;
using HospitalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HospitalProject.Controllers
{
    public class PersonController : Controller
    {
        private readonly PersonDataContext personDataContext;

        public PersonController(PersonDataContext personDataContext)
        {
            this.personDataContext = personDataContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index(PersonClass personClass)
        {
            ViewBag.SexsList = await personDataContext.SexsList();
            List<PersonClass> personsList;
            if (string.IsNullOrEmpty(personClass.IdSexo.ToString()))
            {
                personsList = await personDataContext.GetPersons();
            }
            else 
            {
                personsList = await personDataContext.PersonsFilterBySex(personClass);
            }
            return View(personsList);  
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.SexsList = await personDataContext.SexsList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PersonClass personClass)
        {
            ViewBag.SexsList = await personDataContext.SexsList();
            if (!ModelState.IsValid)
            {
                return View(personClass);
            }
            else
            {
                await personDataContext.CreatePerson(personClass);
            }

            return RedirectToAction("Index");
        }
    }
}
