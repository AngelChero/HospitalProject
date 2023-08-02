using System.ComponentModel.DataAnnotations;

namespace HospitalProject.Class
{
    public class TypeUserClass
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(50, 
            MinimumLength = 6, 
            ErrorMessage = "El campo {0} requiere como mímo {2} y máximo {1} caracteres.")]
        [Display(Name = "Nombre")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(50,
            MinimumLength = 6,
            ErrorMessage = "El campo {0} requiere como mímo {2} y máximo {1} caracteres.")]
        [Display(Name = "Descripción")]
        public string Description { get; set; } = null!;
    }
}
