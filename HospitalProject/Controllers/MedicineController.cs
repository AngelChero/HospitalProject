using HospitalProject.Class;
using HospitalProject.DataAccess;
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
            List<MedicineClass> listMedicines;
            ViewBag.ListPharmaceuticalForm = await medicineDataContext.ListPharmaceuticalForm();
            if (medicineClass.IdPharmaceuticalForm == 0 || medicineClass.IdPharmaceuticalForm == null)
            {
                listMedicines = await medicineDataContext.GetMedicines();
            }
            else
            {
                listMedicines = await medicineDataContext.FilterMedicineByPharmaceuticalForm(medicineClass);
            }

            return View(listMedicines);
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
    }
}
