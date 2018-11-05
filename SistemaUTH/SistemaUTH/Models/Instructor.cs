using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaUTH.Models
{
    public class Instructor
    {
        public int instructorID { get; set; }

        //Numero de trabajador
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El minimo de letras permitido son 3 y maximo 50")]
        public String NumEmpleado { get; set; }

        //Nombre
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El minimo de letras permitido son 3 y maximo 50")]
        public string Name { get; set; }

        //apellido paterno
        [Required]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "El minimo de letras permitido son 3")]
        public string Apellidos { get; set; }

        //correo
        [Required]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "El minimo de letras permitido son 3")]
        public string Correo { get; set; }

        //telefono
        [Required]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "El minimo de letras permitido son 3")]
        public string Celular { get; set; }
    }
}
