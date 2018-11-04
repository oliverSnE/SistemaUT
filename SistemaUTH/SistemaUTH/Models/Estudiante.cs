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

        //Nombre
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El minimo de letras permitido son 3 y maximo 50")]
        public string Name { get; set; }

        //Matricula
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El minimo de letras permitido son 3 y maximo 50")]
        public Int32 Matricula { get; set; }

        //apellido paterno
        [Required]
        [StringLength(50, MinimumLength =3, ErrorMessage = "El minimo de letras permitido son 3 y maximo 50")]
        public string Ap_paterno { get; set; }

        //apellido Materno
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El minimo de letras permitido son 3 y maximo 50")]
        public string Ap_materno { get; set; }

    }
}
