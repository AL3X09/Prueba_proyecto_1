using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba_proyecto_1.Models
{
    public class GeneroClass
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } //Clave primaria

        [Required(ErrorMessage = "El Campo es obligatorio")]
        [Display(Name = "Genero")]
        public string Genero { get; set; }
    }
}
