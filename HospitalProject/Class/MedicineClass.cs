using System.ComponentModel.DataAnnotations;

namespace HospitalProject.Class
{
    public class MedicineClass
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(50, 
            MinimumLength = 3, 
            ErrorMessage = "El campo {0} debe tener como mínimo {2} y máximo {1} caracteres.")]
        [Display(Name = "Nombre")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(50,
            MinimumLength = 3,
            ErrorMessage = "El campo {0} debe tener como mínimo {2} y máximo {1} caracteres.")]
        [Display(Name = "Concentración")]
        public string Concentration { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Precio")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Almacén")]
        public int? Stock { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(50,
            MinimumLength = 3,
            ErrorMessage = "El campo {0} debe tener como mínimo {2} y máximo {1} caracteres.")]
        [Display(Name = "Presentación")]
        public string Presentation { get; set; } = null!;

        [Display(Name = "Forma Farmacéutica")]
        public string? PharmaceuticalForm { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Forma Farmacéutica")]
        public int? IdPharmaceuticalForm { get; set; }
    }
}
