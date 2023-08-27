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

        [HttpGet]
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

        [HttpGet]
        public async Task<CampusClass> Edit(int campusId)
        {
            var getCampusById = await campusDataContext.GetCampusById(campusId);
            return getCampusById;
        }

        [HttpPost]
        public async Task<bool> Edit(CampusClass campusClass)
        {
            bool succesOperation = await campusDataContext.EditCampus(campusClass);
            if (succesOperation)
            {
                return succesOperation;
            }
            else
            {
                return succesOperation;
            }
        }

        public async Task<bool> Create(CampusClass campusClass)
        {
            bool successOperation = await campusDataContext.CreateCampus(campusClass);
            try
            {

                if (successOperation)
                {
                    return successOperation;
                }
                else
                {
                    return successOperation;
                }
            }
            catch (Exception)
            {

                throw;
            }
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
