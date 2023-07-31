using HospitalProject.Class;
using HospitalProject.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalProject.DataAccess
{
    public class SpecialityDataContext
    {
        public async Task<List<SpecialityClass>> GetSpecialities()
        {
            List<SpecialityClass> listSpecialities = new List<SpecialityClass>();
            try
            {
                using (BdhospitalContext db = new BdhospitalContext())
                {
                    var query = from speciality in db.Especialidads
                                where speciality.Bhabilitado == 1
                                select new SpecialityClass
                                {
                                    Id = speciality.Iidespecialidad,
                                    Name = speciality.Nombre,
                                    Description = speciality.Descripcion
                                };
                    listSpecialities = await query.ToListAsync();
                };
            }
            catch (Exception)
            {

                throw;
            }

            return listSpecialities;
        }

        public async Task<List<SpecialityClass>> FilterSpecialtiesByName(string specialityName)
        {
            List<SpecialityClass> filterSpecialtiesByName = new List<SpecialityClass>();
            try
            {
                using (BdhospitalContext db = new BdhospitalContext())
                {
                    var query = from speciality in db.Especialidads
                                where speciality.Bhabilitado == 1
                                && speciality.Nombre.Contains(specialityName)
                                select new SpecialityClass
                                {
                                    Id = speciality.Iidespecialidad,
                                    Name = speciality.Nombre,
                                    Description = speciality.Descripcion
                                };
                    filterSpecialtiesByName = await query.ToListAsync();
                };
            }
            catch (Exception)
            {

                throw;
            }

            return filterSpecialtiesByName;
        }

        public async Task CreateSpeciality(SpecialityClass specialityClass)
        {
            
                using (BdhospitalContext db = new BdhospitalContext())
                {
                    Especialidad speciality = new Especialidad();
                    speciality.Nombre = specialityClass.Name;
                    speciality.Descripcion = specialityClass.Description;
                    speciality.Bhabilitado = 1;
                    await db.Especialidads.AddAsync(speciality);
                await db.SaveChangesAsync();
                }   
        }
    }
}
