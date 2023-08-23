using HospitalProject.Class;
using HospitalProject.DataAccess;
using HospitalProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace HospitalProject.Controllers
{
    public class MedicineController : Controller
    {
        private readonly MedicineDataContext medicineDataContext;

        public MedicineController(MedicineDataContext medicineDataContext)
        {
            this.medicineDataContext = medicineDataContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index(MedicineClass medicineClass)
        {
            ViewBag.ListPharmaceuticalForm = await medicineDataContext.ListPharmaceuticalForm();
            return View();
        }

        public async Task<List<MedicineClass>> ListMedicine()
        {
            List<MedicineClass> listMedicine = await medicineDataContext.GetMedicines();
            return listMedicine;
        }

        public async Task<List<MedicineClass>> MedicineByPharmaceuticalForm(int? pharmaceuticalFormid)
        {
            List<MedicineClass> listMedicine;
            try
            {
                if (pharmaceuticalFormid == null)
                {
                    listMedicine = await medicineDataContext.GetMedicines();
                }
                else
                {
                    listMedicine = 
                        await medicineDataContext.FilterMedicineByPharmaceuticalForm(pharmaceuticalFormid);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return listMedicine;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.ListPharmaceuticalForm = await medicineDataContext.ListPharmaceuticalForm();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MedicineClass medicineClass)
        {
            ViewBag.ListPharmaceuticalForm = await medicineDataContext.ListPharmaceuticalForm();
            if (!ModelState.IsValid)
            {
                return View(medicineClass);
            }

            await medicineDataContext.CreateMedicine(medicineClass);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.listPharmaceuticalForm = await medicineDataContext.ListPharmaceuticalForm();
            var editMedicine = await medicineDataContext.GetMedicineById(id);
            return View(editMedicine);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MedicineClass medicineClass)
        {
            if (!ModelState.IsValid)
            {
                return View(medicineClass);
            }

            await medicineDataContext.EditMedicine(medicineClass);
            return RedirectToAction("Index");
        }

        public async Task<bool> Delete(int medicineId)
        {
            bool success = await medicineDataContext.DeleteMedicine(medicineId);
            try
            {
                if (success)
                {
                    return success;
                }
                else
                {
                    return success;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        
    }
}
