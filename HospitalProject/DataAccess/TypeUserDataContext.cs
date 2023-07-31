using HospitalProject.Class;
using HospitalProject.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalProject.DataAccess
{
    public class TypeUserDataContext
    {
        public async Task<List<TypeUserClass>> GetTypeUsers()
        {
            List<TypeUserClass> listTypeUsers = new List<TypeUserClass>();
            try
            {
                using (BdhospitalContext db = new BdhospitalContext())
                {
                    var query = from typeUser in db.TipoUsuarios
                                where typeUser.Bhabilitado == 1
                                select new TypeUserClass
                                {
                                    Id = typeUser.Iidtipousuario,
                                    Name = typeUser.Nombre,
                                    Description = typeUser.Descripcion
                                };
                    listTypeUsers = await query.ToListAsync();
                };
            }
            catch (Exception)
            {

                throw;
            }

            return listTypeUsers;
        }

        public async Task<List<TypeUserClass>> FilterTypeUsers(TypeUserClass typeUserClass)
        {
            List<TypeUserClass> filterTypeUsers = new List<TypeUserClass>();
            try
            {
                using (BdhospitalContext db = new BdhospitalContext())
                {
                    var query = from typeUser in db.TipoUsuarios
                                where typeUser.Bhabilitado == 1
                                select new TypeUserClass
                                {
                                    Id = typeUserClass.Id,
                                    Name = typeUserClass.Name,
                                    Description = typeUserClass.Description
                                };
                    filterTypeUsers = await query.ToListAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return filterTypeUsers;
        }
    }
}
