using HospitalProject.Class;
using HospitalProject.DataAccess;
using HospitalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HospitalProject.Controllers
{
    public class PersonController : Controller
    {

        [HttpGet]
        public async Task<IActionResult> Index(PersonClass personClass)
        {   
            ListSex();
            List<PersonClass> listPersons = new List<PersonClass>();
            using (BdhospitalContext db = new BdhospitalContext())
            {
                if (personClass.IdSexo == 0)
                {
                    var query = from person in db.Personas
                                join sex in db.Sexos
                                on person.Iidsexo equals sex.Iidsexo
                                where person.Bhabilitado == 1
                                select new PersonClass
                                {
                                    Id = person.Iidpersona,
                                    Name = person.Nombre,
                                    LastName = person.Appaterno,
                                    Sex = sex.Nombre
                                };
                    listPersons = await query.ToListAsync();
                }
                else
                {
                    var query = from person in db.Personas
                                join sex in db.Sexos
                                on person.Iidsexo equals sex.Iidsexo
                                where person.Bhabilitado == 1
                                && person.Iidsexo == personClass.IdSexo
                                select new PersonClass
                                {
                                    Id = person.Iidpersona,
                                    Name = person.Nombre,
                                    LastName = person.Appaterno,
                                    Sex = sex.Nombre
                                };
                    listPersons = await query.ToListAsync();
                }

                
            }
            return View(listPersons);
        }

        public async Task<IActionResult> ListSex()
        {
            List<SelectListItem> listSex = new List<SelectListItem>();
            using (BdhospitalContext db = new BdhospitalContext())
            {
                var query = from sex in db.Sexos
                            select new SelectListItem
                            {
                                Value = sex.Iidsexo.ToString(),
                                Text = sex.Nombre
                            };
                listSex = await query.ToListAsync();
                listSex.Insert(0, new SelectListItem
                {
                    Value = "",
                    Text = "Seleccione --"
                });
                return ViewBag.ListSex = listSex;
            }
        }
    }
}
