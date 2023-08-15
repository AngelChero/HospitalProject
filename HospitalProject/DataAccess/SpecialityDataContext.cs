using HospitalProject.Class;
using HospitalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace HospitalProject.DataAccess
{
    public class SpecialityDataContext
    {
        public static List<SpecialityClass> list;
        public async Task<List<SpecialityClass>> GetSpecialities()
        {
            //List<SpecialityClass> listSpecialities = new List<SpecialityClass>();
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
                    //listSpecialities = await query.ToListAsync();
                    list = await query.ToListAsync();
                };

                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<SpecialityClass> GetSpecialitiesById(int id)
        {
            SpecialityClass getSpecialitiesById = new SpecialityClass();
            try
            {
                using (BdhospitalContext db = new BdhospitalContext())
                {
                    var query = from speciality in db.Especialidads
                                where speciality.Bhabilitado == 1
                                && speciality.Iidespecialidad == id
                                select new SpecialityClass
                                {
                                    Id = speciality.Iidespecialidad,
                                    Name = speciality.Nombre,
                                    Description = speciality.Descripcion
                                };
                   getSpecialitiesById = await query.FirstOrDefaultAsync();
                }

                return getSpecialitiesById;
            }
            catch (Exception)
            {

                throw;
            }
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

        public async Task<bool> EditSpeciality(SpecialityClass specialityClass)
        {
            Especialidad speciality = new Especialidad();
            try
            {
                using (BdhospitalContext db = new BdhospitalContext())
                {
                    speciality.Nombre = specialityClass.Name;
                    speciality.Descripcion = specialityClass.Description;
                    speciality.Bhabilitado = 1;
                    db.Update(speciality);
                    await db.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteSpeciality(int id)
        {
            try
            {
                using (BdhospitalContext db = new BdhospitalContext())
                {
                    var query = from speciality in db.Especialidads
                                where speciality.Bhabilitado == 1
                                && speciality.Iidespecialidad == id
                                select speciality;
                    var deleteSpeciality = await query.FirstAsync();
                    deleteSpeciality.Bhabilitado = 0;
                    await db.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DuplicateName(string name)
        {
            
            try
            {
                using (BdhospitalContext db = new BdhospitalContext())
                {
                    var query = await (from speciality in db.Especialidads
                                       where speciality.Nombre == name
                                       select speciality).AnyAsync();

                    return query;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<FileResult> ExportExcel()
        {
            byte[] buffer = await ExportDataToExcel(list);
            return new FileContentResult(buffer, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = "archivo.xlsx" // Cambia el nombre del archivo según tu preferencia
            };
        }

        public async Task<byte[]> ExportDataToExcel<T>(List<T> list)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    using (ExcelPackage ep = new ExcelPackage())
                    {
                        ep.Workbook.Worksheets.Add("List Especialities");
                        ExcelWorksheet ew = ep.Workbook.Worksheets[0];
                        ep.SaveAs(ms);
                        byte[] buffer = ms.ToArray();
                        return buffer;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
