using HospitalProject.Class;
using HospitalProject.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace HospitalProject.Controllers
{
    public class SpecialityController : Controller
    {
        private readonly SpecialityDataContext specialityDataContext;

        public SpecialityController(SpecialityDataContext specialityDataContext)
        {
            this.specialityDataContext = specialityDataContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string specialityName)
        {
            List<SpecialityClass> listSpecialities;
            ViewBag.specialityName = specialityName;
            if (string.IsNullOrEmpty(specialityName))
            {
                listSpecialities = await specialityDataContext.GetSpecialities();
            }
            else
            {
                listSpecialities = await specialityDataContext.FilterSpecialtiesByName(specialityName);
            }
            return View(listSpecialities);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SpecialityClass specialityClass)
        {
            if (!ModelState.IsValid)
            {
                return View(specialityClass);
            }

            await specialityDataContext.CreateSpeciality(specialityClass);
            return RedirectToAction("Index");
        }

    }
}
