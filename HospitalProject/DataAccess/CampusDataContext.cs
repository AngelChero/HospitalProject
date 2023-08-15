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
