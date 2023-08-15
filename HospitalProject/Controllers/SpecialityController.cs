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

        public async Task<FileResult> ExportExcel()
        {
            return await specialityDataContext.ExportExcel();
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
            var duplicateName = await specialityDataContext.DuplicateName(specialityClass.Name);
            if (duplicateName)
            {
                ModelState.AddModelError(nameof(specialityClass.Name),
                    $"El nombre {specialityClass.Name} ya existe, por favor ingrese otro nombre diferente.");
                return View(specialityClass);
            }
            if (!ModelState.IsValid)
            {
                return View(specialityClass);
            }

            await specialityDataContext.CreateSpeciality(specialityClass);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var editSpeciality = await specialityDataContext.GetSpecialitiesById(id);
            return View(editSpeciality);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SpecialityClass specialityClass)
        {
            var duplicateName = await specialityDataContext.DuplicateName(specialityClass.Name);
            if (duplicateName)
            {
                ModelState.AddModelError(nameof(specialityClass.Name),
                    $"Ya existe una especialidad con el nombre {specialityClass.Name}, ingrese otro.");
                return View(specialityClass);
            }
            await specialityDataContext.EditSpeciality(specialityClass);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await specialityDataContext.DeleteSpeciality(id);
            return RedirectToAction("Index");
        }
    }
}
