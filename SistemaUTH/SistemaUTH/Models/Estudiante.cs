using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaUTH.Models
{
    public class Estudiante
    {
        public int EstudianteID { get; set; }

        //Matricula
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El minimo de letras permitido son 3 y maximo 50")]
        public Int32 Matricula { get; set; }
        //Nombre
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El minimo de letras permitido son 3 y maximo 50")]
        public string Nombre { get; set; }
        
        //apellido paterno
        [Required]
        [StringLength(50, MinimumLength =3, ErrorMessage = "El minimo de letras permitido son 3 y maximo 50")]
        public string Apellidos { get; set; }

       

    }
}
