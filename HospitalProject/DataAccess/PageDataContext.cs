using HospitalProject.Class;
using HospitalProject.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalProject.DataAccess
{
    public class PageDataContext
    {
        public async Task<List<PageClass>> GetPages()
        {
            List<PageClass> listPages = new List<PageClass>();
            try
            {
                using (BdhospitalContext db = new BdhospitalContext())
                {
                    var query = from pages in db.Paginas
                                where pages.Bhabilitado == 1
                                select new PageClass
                                {
                                    Id = pages.Iidpagina,
                                    Message = pages.Mensaje,
                                    Action = pages.Accion,
                                    Controller = pages.Controlador
                                };
                    listPages = await query.ToListAsync();
                    
                }
            }
            catch (Exception)
            {

                throw;
            }

            return listPages;
        }

        public async Task<List<PageClass>> FilterPageByMessage(PageClass pageClass)
        {
            List<PageClass> listPagesByMessage = new List<PageClass>();
            try
            {
                using (BdhospitalContext db = new BdhospitalContext())
                {
                    var query = from page in db.Paginas
                                where page.Bhabilitado == 1
                                && page.Mensaje.Contains(pageClass.Message)
                                select new PageClass
                                {
                                    Id = page.Iidpagina,
                                    Message = page.Mensaje,
                                    Action = page.Accion,
                                    Controller = page.Controlador
                                };

                    listPagesByMessage = await query.ToListAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return listPagesByMessage;
        }
    }
}
