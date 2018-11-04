using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaUTH.Models;

namespace SistemaUTH.Data
{
    public class DbInitializer
    {
        public static void initiliza(SistemaUTHContext context)
        {
            context.Database.EnsureCreated();

            if (context.Categoria.Any())//verificar si existen registro en la tabla
            {
                return;//regresa registros 
            }
            //var categorias = new Categoria[] { new Categoria { Name = "Programacion",
            //    Descripcion = "Desarrollo de aplicaciones",State = true} ,
            //    new Categoria{Name = "Exprecion oral y escrita",Descripcion ="Clase filosofia",
            //        State = true } };

            //foreach (Categoria c in categorias)//agrega los objetos al contexto para guardarlos en la base datos
            //{
            //    context.Categoria.Add(c);
            //}
            context.SaveChanges();//guarda el contexto

        }
    }
}
