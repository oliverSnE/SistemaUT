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

                var categorias = new Categoria[] { new Categoria { Name = "Programacion",
                Descripcion = "Desarrollo de aplicaciones",State = true} ,
                new Categoria{Name = "Exprecion oral y escrita",Descripcion ="Clase filosofia",
                    State = true } };

                foreach (Categoria c in categorias)//agrega los objetos al contexto para guardarlos en la base datos
                {
                    context.Categoria.Add(c);
                }
                context.SaveChanges();
            }
            if (context.Estudiante.Any())
            {
                return;


                var estudiantes = new Estudiante[] { new Estudiante {Name= "Edualo", Ap_paterno="Rosale", Ap_materno="Maltilez", Matricula="111666", EstudianteID=1},
                new Estudiante{Name= "Pelele", Ap_paterno="leamel", Ap_materno="golme", Matricula="666666", EstudianteID=2 } };

                foreach  (Estudiante e in estudiantes)
                {
                    context.Estudiante.Add(e);

                }
                context.SaveChanges();
            }
            if (context.Instructor.Any())
            {
                return;


                var instructor = new Instructor[] { new Instructor {Name= "Elmer", Apellidos="Meras molina", Correo="elmer12@gmail.com", NumEmpleado="666", Celular="6622018868"} };

                foreach (Instructor e in instructor)
                {
                    context.Instructor.Add(e);

                }
                context.SaveChanges();
            }
            //var categorias = new Categoria[] { new Categoria { Name = "Programacion",
            //    Descripcion = "Desarrollo de aplicaciones",State = true} ,
            //    new Categoria{Name = "Exprecion oral y escrita",Descripcion ="Clase filosofia",
            //        State = true } };

            //foreach (Categoria c in categorias)//agrega los objetos al contexto para guardarlos en la base datos
            //{
            //    context.Categoria.Add(c);
            //}
            //context.SaveChanges();//guarda el contexto

        }
    }
}
