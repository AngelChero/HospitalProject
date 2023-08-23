using HospitalProject.Class;
using HospitalProject.DataAccess;
using HospitalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalProject.Controllers
{
    public class SpecialityController : Controller
    {
        private readonly SpecialityDataContext specialityDataContext;

        public SpecialityController(SpecialityDataContext specialityDataContext)
        {
            this.specialityDataContext = specialityDataContext;
        }

        //public async Task<FileResult> ExportExcel()
        //{
        //    return await specialityDataContext.ExportExcel();
        //}

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<List<SpecialityClass>> ListSpeciality()
        {
            var listSpecialities = await specialityDataContext.GetSpecialities();
            return listSpecialities;
        }

        public async Task<List<SpecialityClass>> SpecialtiesByName(string specialityName)
        {
            var filterSpecialtiesByName = await specialityDataContext.FilterSpecialtiesByName(specialityName);
            return filterSpecialtiesByName;
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

        //public int Delete(int id)
        //{
        //    int rpta = 0;
        //    try
        //    {
        //        using (BdhospitalContext db = new BdhospitalContext())
        //        {
        //            Especialidad specialities = new Especialidad();
        //            var query = (from speciality in db.Especialidads
        //                         where speciality.Iidespecialidad == id
        //                         && speciality.Bhabilitado == 1
        //                         select speciality).FirstOrDefault();
        //            query.Bhabilitado = 0;
        //            db.SaveChanges();
        //            rpta = 1;
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        rpta = 0;
        //    }

        //    return rpta;
        //}

        public async Task<bool> Delete(int specialityId)
        {
            bool deleteSpeciality;
            deleteSpeciality = await specialityDataContext.DeleteSpeciality(specialityId);
            return deleteSpeciality;
        }
    }
}
