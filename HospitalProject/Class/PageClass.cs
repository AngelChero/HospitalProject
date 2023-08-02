using System.ComponentModel.DataAnnotations;

namespace HospitalProject.Class
{
    public class PageClass
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(50, 
            MinimumLength = 6, 
            ErrorMessage = "El campo {0} requiere como mínimo {2} y máximo {1} caracteres.")]
        [Display(Name = "Mensaje")]
        public string Message { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(50,
            MinimumLength = 6,
            ErrorMessage = "El campo {0} requiere como mínimo {2} y máximo {1} caracteres.")]
        [Display(Name = "Acción")]
        public string Action { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(50,
            MinimumLength = 6,
            ErrorMessage = "El campo {0} requiere como mínimo {2} y máximo {1} caracteres.")]
        [Display(Name = "Controlador")]
        public string Controller { get; set; } = null!;
    }
}
