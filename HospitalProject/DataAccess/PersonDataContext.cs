using HospitalProject.Class;
using HospitalProject.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HospitalProject.DataAccess
{
    public class PersonDataContext
    {
        public async Task<List<PersonClass>> GetPersons()
        {
            List<PersonClass> listPerson = new List<PersonClass>();
            try
            {
                using(BdhospitalContext db = new BdhospitalContext())
                {
                    var query = from persons in db.Personas
                                join sex in db.Sexos on persons.Iidsexo equals sex.Iidsexo
                                where persons.Bhabilitado == 1
                                select new PersonClass
                                {
                                    Id = persons.Iidpersona,
                                    Name = persons.Nombre,
                                    LastName = persons.Appaterno,
                                    Sex = sex.Nombre
                                };
                    listPerson = await query.ToListAsync();
                };
            }
            catch (Exception)
            {

                throw;
            }

            return listPerson;
        }

        public async Task<List<PersonClass>> FilterPersons(string name)
        {
            List<PersonClass> listPersons = new List<PersonClass>();
            try
            {
                using (BdhospitalContext db = new BdhospitalContext())
                {
                    var query = from person in db.Personas
                                join sexo in db.Sexos
                                on person.Iidsexo equals sexo.Iidsexo
                                where person.Bhabilitado == 1
                                && person.Nombre.Contains(name)
                                select new PersonClass
                                {
                                    Id = person.Iidpersona,
                                    Name = person.Nombre,
                                    LastName = person.Appaterno,
                                    Sex = sexo.Nombre
                                };
                    listPersons = await query.ToListAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return listPersons;
        }

        public async Task<List<SelectListItem>> CboSex()
        {
            List<SelectListItem> listSex = new List<SelectListItem>();
            try
            {
                using (BdhospitalContext db = new BdhospitalContext())
                {
                    var query = from sex in db.Sexos
                                select new SelectListItem
                                {
                                    Value = sex.Iidsexo.ToString(),
                                    Text = sex.Nombre
                                };
                    listSex = await query.ToListAsync();
                }

                listSex.Insert(0, new SelectListItem
                {
                    Value = string.Empty,
                    Text = "Seleccione --"
                });
            }
            catch (Exception)
            {

                throw;
            }
                
            return listSex;
        }

        //public async Task<List<PersonClass>> FilterPersonsBySex(PersonClass selectedSex)
        //{
        //    List<PersonClass> listPersons = new List<PersonClass>();
        //    try
        //    {
        //        using (BdhospitalContext db = new BdhospitalContext())
        //        {
        //            var query = from person in db.Personas
        //                        join sexo in db.Sexos
        //                        on person.Iidsexo equals sexo.Iidsexo
        //                        where person.Bhabilitado == 1
        //                        && person.Iidsexo == selectedSex.IdSexo
        //                        select new PersonClass
        //                        {
        //                            Id = person.Iidpersona,
        //                            Name = person.Nombre,
        //                            LastName = person.Appaterno,
        //                            Sex = sexo.Nombre
        //                        };
        //            listPersons = await query.ToListAsync();
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //    return listPersons;
        //}
    }
}
