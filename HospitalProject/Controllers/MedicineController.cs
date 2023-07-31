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

        public async Task<IActionResult> Index(MedicineClass medicineClass)
        {
            List<MedicineClass> listMedicines;
            ViewBag.ListPharmaceuticalForm = await medicineDataContext.ListPharmaceuticalForm();
            if (medicineClass.IdPharmaceuticalForm == 0)
            {
                listMedicines = await medicineDataContext.GetMedicines();
            }
            else
            {
                listMedicines = await medicineDataContext.FilterMedicineByPharmaceuticalForm(medicineClass);
            }

            return View(listMedicines);
        }
    }
}
