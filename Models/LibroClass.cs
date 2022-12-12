using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba_proyecto_1.Models
{
    public class LibroClass
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } //Clave primaria

        [Required(ErrorMessage = "El Campo es obligatorio")]
        [Display(Name = "Titulo")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "El Campo es obligatorio")]
        [Display(Name = "Año")]
        public int Anio { get; set; }

        [Required(ErrorMessage = "El Campo es obligatorio")]
        [Display(Name = "Número de páginas")]
        public int Numero_paginas { get; set; }

        //Campo clave foranea
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Campo {0} es obligatorio")]
        [Display(Name = "autor")]
        public int AutorId { get; set; }

        //Campo clave foranea
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Campo {0} es obligatorio")]
        [Display(Name = "genero")]
        public int GeneroId { get; set; }

        [ForeignKey("AutorClass")]
        public virtual List<AutorClass>? Autor { get; set; } //Objeto de navegación virtual EFC
        
        [ForeignKey("GeneroClass")]
        public virtual List<GeneroClass>? Genero { get; set; } //Objeto de navegación virtual EFC

    }
}
