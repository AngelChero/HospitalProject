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

        public async Task<bool> CreatePerson(PersonClass personClass)
        {
            try
            {
                using (BdhospitalContext db = new BdhospitalContext())
                {
                    Persona persona = new Persona()
                    {
                        Nombre = personClass.Name,
                        Appaterno = personClass.LastNameP,
                        Apmaterno = personClass.LastNameM,
                        Email = personClass.Email,
                        Telefonofijo = personClass.Landline,
                        Telefonocelular = personClass.CellPhone,
                        Fechanacimiento = personClass.Birthdate,
                        Iidsexo = personClass.IdSexo,
                        Bhabilitado = 1
                    };

                    await db.Personas.AddAsync(persona);
                    await db.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<PersonClass>> FilterPersonsByName(string name)
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

        public async Task<List<SelectListItem>> SexsList()
        {
            List<SelectListItem> sexsList = new List<SelectListItem>();
            try
            {
                using (BdhospitalContext db = new BdhospitalContext())
                {
                    var query = from sex in db.Sexos
                                select new SelectListItem
                                {
                                    Value = sex.Iidsexo.ToString(),
                                    Text = sex.Nombre,
                                };
                    sexsList = await query.ToListAsync();
                    sexsList.Insert(0, new SelectListItem
                    {
                        Value = string.Empty,
                        Text = "Seleccione --"
                    });
                }

                return sexsList;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<PersonClass>> PersonsFilterBySex(PersonClass personClass)
        {
            List<PersonClass> personsFilterByName = new List<PersonClass>();
            try
            {
                using (BdhospitalContext db = new BdhospitalContext())
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
                    personsFilterByName = await query.ToListAsync();

                }

                return personsFilterByName;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
