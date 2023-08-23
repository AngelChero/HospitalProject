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
        public async Task<IActionResult> Index()
        {
            ViewBag.SexsList = await personDataContext.SexsList();
            return View();
        }

        public async Task<List<PersonClass>> ListPersons(int? sexId)
        {
            ViewBag.SexsList = await personDataContext.SexsList();
            List<PersonClass> listPersons = new List<PersonClass>();
            try
            {
                if (sexId == null)
                {
                    listPersons = await personDataContext.GetPersons();    
                }
                else
                {
                    listPersons = await personDataContext.PersonsFilterBySex(sexId);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return listPersons;
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
            var duplicatePerson = await personDataContext.DuplicatePerson(personClass);
            ViewBag.SexsList = await personDataContext.SexsList();
            if (duplicatePerson)
            {
                ModelState.AddModelError(nameof(personClass),
                    $"Ya existe una persona con el mismo nombre: {personClass.Name} y los apellidos: {personClass.LastNameP} {personClass.LastNameM}, por favor ingrese otros datos diferentes.");
                return View(personClass);
            }
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
        
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await personDataContext.DeletePerson(id);
            return RedirectToAction("Index");
        }
    }
}
