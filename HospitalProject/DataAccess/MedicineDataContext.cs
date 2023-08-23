﻿using HospitalProject.Class;
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

        public async Task<MedicineClass> GetMedicineById(int medicineId)
        {
            MedicineClass getMedicineById = new MedicineClass();
            try
            {
                using (BdhospitalContext db = new BdhospitalContext())
                {
                    var query = from medicine in db.Medicamentos
                                where medicine.Iidmedicamento == medicineId
                                select new MedicineClass
                                {
                                    Name = medicine.Nombre,
                                    Concentration = medicine.Concentracion,
                                    IdPharmaceuticalForm = medicine.Iidformafarmaceutica,
                                    Price = medicine.Precio,
                                    Stock = medicine.Stock,
                                    Presentation = medicine.Presentacion
                                };
                    getMedicineById = await query.FirstAsync();
                }

                return getMedicineById;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> CreateMedicine(MedicineClass medicineClass)
        {
            try
            {
                using (BdhospitalContext db = new BdhospitalContext())
                {
                    Medicamento medicamento = new Medicamento();
                    medicamento.Nombre = medicineClass.Name;
                    medicamento.Concentracion = medicineClass.Concentration;
                    medicamento.Iidformafarmaceutica = medicineClass.IdPharmaceuticalForm;
                    medicamento.Precio = medicineClass.Price;
                    medicamento.Stock = medicineClass.Stock;
                    medicamento.Presentacion = medicineClass.Presentation;
                    medicamento.Bhabilitado = 1;
                    await db.Medicamentos.AddAsync(medicamento);
                    await db.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Medicamento> EditMedicine(MedicineClass medicineClass)
        {
            Medicamento editMedicine = new Medicamento();
            try
            {
                using (BdhospitalContext db = new BdhospitalContext())
                {
                    editMedicine.Iidmedicamento = medicineClass.Id;
                    editMedicine.Nombre = medicineClass.Name;
                    editMedicine.Concentracion = medicineClass.Concentration;
                    editMedicine.Iidformafarmaceutica = medicineClass.IdPharmaceuticalForm;
                    editMedicine.Precio = medicineClass.Price;
                    editMedicine.Stock = medicineClass.Stock;
                    editMedicine.Presentacion = medicineClass.Presentation;
                    editMedicine.Bhabilitado = 1;
                    db.Update(editMedicine);
                    await db.SaveChangesAsync();
                }

                return editMedicine;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteMedicine(int medicineId)
        {
            bool successOperation;
            try
            {
                using (BdhospitalContext db = new BdhospitalContext())
                {
                    var query = from medicine in db.Medicamentos
                                where medicine.Iidmedicamento == medicineId
                                && medicine.Bhabilitado == 1
                                select medicine;
                    var deleteMedicine = await query.FirstAsync();
                    deleteMedicine.Bhabilitado = 0;
                    await db.SaveChangesAsync();
                    successOperation = true;
                }
            }
            catch (Exception)
            {
                successOperation = false;
                throw;
            }

            return successOperation;
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

        public async Task<List<MedicineClass>> FilterMedicineByPharmaceuticalForm(int? pharmaceuticalFormid)
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
                                && medicine.Iidformafarmaceutica == pharmaceuticalFormid
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
