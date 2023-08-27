using HospitalProject.Class;
using HospitalProject.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalProject.DataAccess
{
    public class CampusDataContext
    {
        public async Task<List<CampusClass>> GetCampus()
        {
            List<CampusClass> listCampus  = new List<CampusClass>();
            try
            {
                using (BdhospitalContext db = new BdhospitalContext())
                {
                    var query = from campus in db.Sedes
                                where campus.Bhabilitado == 1
                                select new CampusClass
                                {
                                    Id = campus.Iidsede,
                                    Name = campus.Nombre,
                                    Direction = campus.Direccion
                                };
                    listCampus = await query.ToListAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return listCampus;
        }

        public async Task<CampusClass> GetCampusById(int campusId)
        {
            CampusClass getCampusById = new CampusClass();
            try
            {
                using (BdhospitalContext db = new BdhospitalContext())
                {
                    var query = await (from campus in db.Sedes
                                       where campus.Iidsede == campusId
                                       && campus.Bhabilitado == 1
                                       select new CampusClass
                                       {
                                           Id = campus.Iidsede,
                                           Name = campus.Nombre,
                                           Direction = campus.Direccion
                                       }).FirstAsync();
                    getCampusById = query;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return getCampusById;
        }

        public async Task<bool> EditCampus(CampusClass campusClass)
        {
            bool success;
            try
            {
                using (BdhospitalContext db = new BdhospitalContext())
                {
                    Sede sede = new Sede()
                    {
                        Iidsede = campusClass.Id,
                        Nombre = campusClass.Name,
                        Direccion = campusClass.Direction,
                        Bhabilitado = 1
                    };
                    db.Update(sede);
                    db.SaveChangesAsync();
                    success = true;
                }
            }
            catch (Exception)
            {

                success = false;
            }

            return success;
        }

        public async Task<bool> CreateCampus(CampusClass campusClass)
        {
            bool success;
            try
            {
                using (BdhospitalContext db = new BdhospitalContext())
                {
                    Sede sede = new Sede();
                    sede.Nombre = campusClass.Name;
                    sede.Direccion = campusClass.Direction;
                    sede.Bhabilitado = 1;
                    await db.AddAsync(sede);
                    await db.SaveChangesAsync();
                    success = true;
                }
            }
            catch (Exception)
            {
                success = false;
                throw;
            }

            return success;
        }

        public async Task<List<CampusClass>> FilterCampus(string campusName)
        {
            List<CampusClass> filterCampus = new List<CampusClass>();
            try
            {
                using (BdhospitalContext db = new BdhospitalContext())
                {
                    var query = from campus in db.Sedes
                                where campus.Bhabilitado == 1
                                && campus.Nombre.Contains(campusName)
                                select new CampusClass
                                {
                                    Id = campus.Iidsede,
                                    Name = campus.Nombre,
                                    Direction = campus.Direccion
                                };
                    filterCampus = await query.ToListAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return filterCampus;
        }

        public async Task<bool> DeleteCampus(CampusClass campusClass)
        {
            try
            {
                using (BdhospitalContext db = new BdhospitalContext())
                {
                    var query = from campus in db.Sedes
                                where campus.Bhabilitado == 1
                                && campus.Iidsede == campusClass.Id
                                select campus;
                    var selectedCampus = await query.FirstAsync();
                    selectedCampus.Bhabilitado = 0;
                    await db.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        
    }
}
