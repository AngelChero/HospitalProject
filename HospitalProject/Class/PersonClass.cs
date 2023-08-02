using System.ComponentModel.DataAnnotations;

namespace HospitalProject.Class
{
    public class PersonClass
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(
            50, 
            MinimumLength = 6, 
            ErrorMessage = "El campo {0} requiere como mínimo {2} y máximo {1} caracteres.")]
        [Display(Name = "Nombre")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(
            50,
            MinimumLength = 6,
            ErrorMessage = "El campo {0} requiere como mínimo {2} y máximo {1} caracteres.")]
        [Display(Name = "Apellido Paterno")]
        public string LastNameP { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(
            50,
            MinimumLength = 6,
            ErrorMessage = "El campo {0} requiere como mínimo {2} y máximo {1} caracteres.")]
        [Display(Name = "Apellido Materno")]
        public string LastNameM { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(
            50,
            MinimumLength = 6,
            ErrorMessage = "El campo {0} requiere como mínimo {2} y máximo {1} caracteres.")]
        [Display(Name = "Teléfono Fijo")]
        public string Landline { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(
            50,
            MinimumLength = 6,
            ErrorMessage = "El campo {0} requiere como mínimo {2} y máximo {1} caracteres.")]
        [Display(Name = "Teléfono Celular")]
        public string CellPhone { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime Birthdate { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "El {0} ingresado no es válido")]
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; } = null!;

        public string? LastName { get; set; } = null!;

        [Display(Name = "Sexo")]
        public string? Sex { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Sexo")]
        public int? IdSexo { get; set; }
    }
}
