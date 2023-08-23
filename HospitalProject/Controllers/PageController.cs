using HospitalProject.Class;
using HospitalProject.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace HospitalProject.Controllers
{
    public class PageController : Controller
    {
        private readonly PageDataContext pageDataContext;

        public PageController(PageDataContext pageDataContext)
        {
            this.pageDataContext = pageDataContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<List<PageClass>> PageList()
        {
            var pageList = await pageDataContext.GetPages();
            return pageList;
        }

        public async Task<List<PageClass>> PageByMessage(string message)
        {
            var pageByMessage = await pageDataContext.FilterPageByMessage(message);
            return pageByMessage;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PageClass pageClass)
        {
            if (!ModelState.IsValid)
            {
                return View(pageClass);
            }

            await pageDataContext.CreatePage(pageClass);
            return RedirectToAction("Index");
        }

        public async Task<bool> Delete(int pageId)
        {
            var successDelete = await pageDataContext.DeletePage(pageId);
            try
            {
                if (successDelete)
                {
                    return successDelete;
                }
                else
                {
                    return successDelete;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
