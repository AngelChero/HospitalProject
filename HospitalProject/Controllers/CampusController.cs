using HospitalProject.Class;
using HospitalProject.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace HospitalProject.Controllers
{
    public class CampusController : Controller
    {
        private readonly CampusDataContext campusDataContext;

        public CampusController(CampusDataContext campusDataContext)
        {
            this.campusDataContext = campusDataContext;
        }
         
        public async Task<IActionResult> Index(string campusName)
        {
            List<CampusClass> listCampus;
            ViewBag.CampusName = campusName;
            if (string.IsNullOrEmpty(campusName))
            {
                listCampus = await campusDataContext.GetCampus();
            }
            else
            {
                listCampus = await campusDataContext.FilterCampus(campusName);
            }
            return View(listCampus);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(CampusClass campusClass)
        {
            bool campusSelected;
            try
            {
                campusSelected = await campusDataContext.DeleteCampus(campusClass);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
