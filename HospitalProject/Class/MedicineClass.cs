using System.ComponentModel.DataAnnotations;

namespace HospitalProject.Class
{
    public class MedicineClass
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        public string Name { get; set; } = null!;

        [Display(Name = "Precio")]
        public decimal Price { get; set; }

        [Display(Name = "Almacén")]
        public int Stock { get; set; }

        [Display(Name = "Forma Farmacéutica")]
        public string PharmaceuticalForm { get; set; } = null!;
        public int IdPharmaceuticalForm { get; set; }
    }
}
