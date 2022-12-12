using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba_proyecto_1.Models
{
    public class AutorClass
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } //Clave primaria

        [Required(ErrorMessage = "El Campo es obligatorio")]
        [Display(Name = "Titulo")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El Campo es obligatorio")]
        [Display(Name = "Ciudad de procedencia")]
        public string Ciudad_procedencia { get; set; }

        [Required(ErrorMessage = "El Campo es obligatorio")]
        [Display(Name = ", Correo electrónico")]
        public string Correo_electrónico { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = false)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Campo {0} es obligatorio")]
        [Display(Name = "Fecha de nacimiento")]
        public DateTime Fecha_nacimiento { get; set; }

    }
}
