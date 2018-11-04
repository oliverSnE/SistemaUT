using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SistemaUTH.Models
{
    public class Categoria
    {
        public int CategoriaID { get; set; }
        
        //Nombre
        [Required]
        [StringLength(50, MinimumLength =3,ErrorMessage ="El minimo de letras permitido son 3 y maximo 50")]
        public string Name { get; set; }
        
        //Descripcion
        [StringLength(250,ErrorMessage ="Te sugerimos que incluya descripcion")]
        public string Descripcion { get; set; }

        public Boolean State { get; set; } = true;
        
    }
}
