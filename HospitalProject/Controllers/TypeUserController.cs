using HospitalProject.Class;
using HospitalProject.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace HospitalProject.Controllers
{
    public class TypeUserController : Controller
    {
        private readonly TypeUserDataContext typeUserDataContext;

        public TypeUserController(TypeUserDataContext typeUserDataContext)
        {
            this.typeUserDataContext = typeUserDataContext;
        }

        public async Task<IActionResult> Index(TypeUserClass typeUserClass)
        {
            List<TypeUserClass> listTypeUsers;
            
            if (string.IsNullOrEmpty(typeUserClass.Name) && string.IsNullOrEmpty(typeUserClass.Description)
                && typeUserClass.Id == 0)
            {
                listTypeUsers = await typeUserDataContext.GetTypeUsers(); //Lista sin filtrar
                ViewBag.Id = 0;
                ViewBag.Name = "";
                ViewBag.Description = "";
            }
            else
            {
                listTypeUsers = await typeUserDataContext.GetTypeUsers();
                if (!string.IsNullOrEmpty(typeUserClass.Name))
                {
                    listTypeUsers = listTypeUsers
                        .Where(p => p.Name.Contains(typeUserClass.Name)).ToList();
                }
                if (!string.IsNullOrEmpty(typeUserClass.Description))
                {
                    listTypeUsers = listTypeUsers
                        .Where(p => p.Description.Contains(typeUserClass.Description)).ToList();
                }
                if (typeUserClass.Id != 0)
                {
                    listTypeUsers = listTypeUsers
                        .Where(p => p.Id == typeUserClass.Id).ToList();
                }

                ViewBag.Id = typeUserClass.Id;
                ViewBag.Name = typeUserClass.Name;
                ViewBag.Description = typeUserClass.Description;
            }

            return View(listTypeUsers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TypeUserClass typeUserClass)
        {
            if (!ModelState.IsValid)
            {
                return View(typeUserClass);
            }

            await typeUserDataContext.CreateTypeUser(typeUserClass);
            return RedirectToAction("Index");
        }
    }
}
