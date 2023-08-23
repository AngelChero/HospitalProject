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

        public async Task<List<PageClass>> FilterPageByMessage(string message)
        {
            List<PageClass> listPagesByMessage = new List<PageClass>();
            try
            {
                using (BdhospitalContext db = new BdhospitalContext())
                {
                    var query = from page in db.Paginas
                                where page.Bhabilitado == 1
                                && page.Mensaje.Contains(message)
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

        public async Task<bool> CreatePage(PageClass pageClass)
        {
            try
            {
                using (BdhospitalContext db = new BdhospitalContext())
                {
                    Pagina pagina = new Pagina()
                    {
                        Mensaje = pageClass.Message,
                        Accion = pageClass.Action,
                        Controlador = pageClass.Controller,
                        Bhabilitado = 1
                    };

                    await db.Paginas.AddAsync(pagina);
                    await db.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeletePage(int pageId)
        {
            bool successDelete;
            try
            {
                using (BdhospitalContext db = new BdhospitalContext())
                {
                    var query = await (from page in db.Paginas
                                       where page.Bhabilitado == 1
                                       && page.Iidpagina == pageId
                                       select page).FirstAsync();
                    query.Bhabilitado = 0;
                    await db.SaveChangesAsync();
                    successDelete = true;
                }
            }
            catch (Exception)
            {
                successDelete = false;
                throw;
            }

            return successDelete;
        }
    }
}
