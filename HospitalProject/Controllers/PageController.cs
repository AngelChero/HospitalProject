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

        public async Task<IActionResult> Index(PageClass pageClass)
        {
            List<PageClass> listPages;
            ViewBag.PageName = pageClass.Message;
            if (string.IsNullOrEmpty(pageClass.Message))
            {
                listPages = await pageDataContext.GetPages();
            }
            else
            {
                listPages = await pageDataContext.FilterPageByMessage(pageClass);
            }
            
            return View(listPages);
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
    }
}
