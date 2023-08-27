using System.ComponentModel.DataAnnotations;

namespace HospitalProject.Class
{
    public class CampusClass
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Nombre")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Dirección")]
        public string Direction { get; set; } = null!;

    }
}
