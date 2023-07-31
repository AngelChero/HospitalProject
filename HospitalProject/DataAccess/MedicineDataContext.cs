using HospitalProject.Class;
using HospitalProject.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HospitalProject.DataAccess
{
    public class MedicineDataContext
    {
        public async Task<List<MedicineClass>> GetMedicines()
        {
            List<MedicineClass> listMedicines = new List<MedicineClass>();
            try
            {
                using (BdhospitalContext db = new BdhospitalContext())
                {
                    var query = from medicines in db.Medicamentos
                                join pharmaceuticalForm in db.FormaFarmaceuticas 
                                on medicines.Iidformafarmaceutica 
                                equals pharmaceuticalForm.Iidformafarmaceutica
                                where medicines.Bhabilitado == 1
                                select new MedicineClass
                                {
                                    Id = medicines.Iidmedicamento,
                                    Name = medicines.Nombre,
                                    Price = (decimal)medicines.Precio,
                                    Stock = (int)medicines.Stock,
                                    PharmaceuticalForm = pharmaceuticalForm.Nombre
                                };
                    listMedicines = await query.ToListAsync();
                };
            }
            catch (Exception)
            {

                throw;
            }

            return listMedicines;
        }

        public async Task<List<MedicineClass>> FilterMedicineByName(string medicineName)
        {
            List<MedicineClass> filterMedicine = new List<MedicineClass>();
            try
            {
                using (BdhospitalContext db = new BdhospitalContext())
                {
                    var query = from medicine in db.Medicamentos
                                join pharmaceuticalForm in db.FormaFarmaceuticas
                                on medicine.Iidformafarmaceutica
                                equals pharmaceuticalForm.Iidformafarmaceutica
                                where medicine.Bhabilitado == 1
                                && medicine.Nombre.Contains(medicineName)
                                select new MedicineClass
                                {
                                    Id = medicine.Iidmedicamento,
                                    Name = medicine.Nombre,
                                    Price = (decimal)medicine.Precio,
                                    Stock = (int)medicine.Stock,
                                    PharmaceuticalForm = pharmaceuticalForm.Nombre
                                };
                    filterMedicine = await query.ToListAsync();
                };
            }
            catch (Exception)
            {

                throw;
            }

            return filterMedicine;
        }

        public async Task<List<SelectListItem>> ListPharmaceuticalForm()
        {
            List<SelectListItem> listPharmaceuticalForm = new List<SelectListItem>();
            try
            {
                using (BdhospitalContext db = new BdhospitalContext())
                {
                    var query = from pharmaceuticalForm in db.FormaFarmaceuticas
                                select new SelectListItem
                                {
                                    Value = pharmaceuticalForm.Iidformafarmaceutica.ToString(),
                                    Text = pharmaceuticalForm.Nombre
                                };
                    listPharmaceuticalForm = await query.ToListAsync();
                    listPharmaceuticalForm.Insert(0, new SelectListItem
                    {
                        Value = string.Empty,
                        Text = "Seleccione --"
                    });
                };
            }
            catch (Exception)
            {

                throw;
            }

            return listPharmaceuticalForm;
        }

        public async Task<List<MedicineClass>> FilterMedicineByPharmaceuticalForm(MedicineClass medicineClass)
        {
            List<MedicineClass> listMedicineByPharmaceuticalForm = new List<MedicineClass>();
            try
            {
                using (BdhospitalContext db = new BdhospitalContext())
                {
                    var query = from medicine in db.Medicamentos
                                join pharmaceuticalForm in db.FormaFarmaceuticas
                                on medicine.Iidformafarmaceutica equals pharmaceuticalForm.Iidformafarmaceutica
                                where medicine.Bhabilitado == 1
                                && pharmaceuticalForm.Iidformafarmaceutica == medicineClass.IdPharmaceuticalForm
                                select new MedicineClass
                                {
                                    Id = medicine.Iidmedicamento,
                                    Name = medicine.Nombre,
                                    Price = (decimal)medicine.Precio,
                                    Stock = (int)medicine.Stock,
                                    PharmaceuticalForm = pharmaceuticalForm.Nombre
                                };
                    listMedicineByPharmaceuticalForm = await query.ToListAsync();
                };
            }
            catch (Exception)
            {

                throw;
            }

            return listMedicineByPharmaceuticalForm;
        }
    }
}
