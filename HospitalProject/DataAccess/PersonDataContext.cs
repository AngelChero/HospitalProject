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

        public async Task<Persona> GetPersonById(int id)
        {
            Persona getPersonById = new Persona();
            try
            {
                using (BdhospitalContext db = new BdhospitalContext())
                {
                    var query = from person in db.Personas
                                where person.Iidpersona == id
                                && person.Bhabilitado == 1
                                select person;
                    getPersonById = await query.FirstAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return getPersonById;
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

        public async Task<bool> DeletePerson(int id)
        {
            try
            {
                using (BdhospitalContext db = new BdhospitalContext())
                {
                    var query = from person in db.Personas
                                where person.Iidpersona == id
                                && person.Bhabilitado == 1
                                select person;
                    var model = await query.FirstAsync();
                    model.Bhabilitado = 0;
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

        public async Task<List<PersonClass>> PersonsFilterBySex(int? sexId)
        {
            List<PersonClass> personsFilterBySex = new List<PersonClass>();
            try
            {
                using (BdhospitalContext db = new BdhospitalContext())
                {
                    var query = from person in db.Personas
                                join sex in db.Sexos
                                on person.Iidsexo equals sex.Iidsexo
                                where person.Bhabilitado == 1
                                && person.Iidsexo == sexId
                                select new PersonClass
                                {
                                    Id = person.Iidpersona,
                                    Name = person.Nombre,
                                    LastName = person.Appaterno,
                                    Sex = sex.Nombre
                                };
                    personsFilterBySex = await query.ToListAsync();

                }                
            }
            catch (Exception)
            {

                throw;
            }

            return personsFilterBySex;
        }

        public async Task<bool> DuplicatePerson(PersonClass personClass)
        {
            try
            {
                using (BdhospitalContext db = new BdhospitalContext())
                {
                    var query = await (from person in db.Personas
                                      where person.Nombre.ToLower() == personClass.Name.ToLower()
                                      && person.Appaterno.ToLower() == personClass.LastNameP.ToLower()
                                      && person.Apmaterno.ToLower() == personClass.LastNameM.ToLower()
                                      && person.Bhabilitado == 1
                                      select person).AnyAsync();
                    return query;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
