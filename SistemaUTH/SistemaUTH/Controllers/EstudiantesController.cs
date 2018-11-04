using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaUTH.Models;

namespace SistemaUTH.Controllers
{
    public class EstudiantesController : Controller
    {
        private readonly SistemaUTHContext _context;

        public EstudiantesController(SistemaUTHContext context)
        {
            _context = context;
        }

        // GET: Estudiantes
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? page)
        {

            // return View(await _context.Estudiante.ToListAsync());

            ViewData["CurrentSort"] = sortOrder;//obtiene la ubicacion actual;

            ViewData["NombreSortParm"] = String.IsNullOrEmpty(sortOrder) ? "nombre_desc" : "";
            ViewData["ApellidosSortParm"] = sortOrder == "apellidos_asc" ? "apellidos_desc" : "apellidos_asc";
            ViewData["CurrentFilter"] = searchString; // obtiene el valor a buscar en el input

            if (searchString != null)
            {
                page = 1;
            }//validacion de la busqueda muestra, si hay resultado o noes lo que muestra.
            else
            {
                searchString = currentFilter;
            }
            var estudiantes = from s in _context.Estudiante select s; //un select referente a una query

            if (!String.IsNullOrEmpty(searchString))//verificar si la var Serchstring tiene nombre o descripcio
            {
                estudiantes = estudiantes.Where(s => s.Nombre.Contains(searchString) || s.Apellidos.Contains(searchString));
            }
            switch (sortOrder) // ordena lascategorias
            {
                case "nombre_desc":
                    estudiantes = estudiantes.OrderByDescending(s => s.Nombre);
                    break;
                case "apellidos_asc":
                    estudiantes = estudiantes.OrderBy(s => s.Apellidos);
                    break;
                case "apellidos_desc":
                    estudiantes = estudiantes.OrderByDescending(s => s.Apellidos);
                    break;
                default:
                    estudiantes = estudiantes.OrderBy(s => s.Nombre);
                    break;
            }
            //return View(await _context.Categoria.ToListAsync());
            // regresa la ista con el ordenamiento realizado a la coleccion Categorias
            //return View(await categorias.AsNoTracking().ToListAsync());

            int pageSize = 3;//visualisa el nuemero de elementos que muestra una vista
            return View(await Paginacion<Estudiante>.CreatesAsync(estudiantes.AsNoTracking(), page ?? 1, pageSize));//regresa el total de resultado de elemento. 

        }

        // GET: Estudiantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiante = await _context.Estudiante
                .FirstOrDefaultAsync(m => m.EstudianteID == id);
            if (estudiante == null)
            {
                return NotFound();
            }

            return View(estudiante);
        }

        // GET: Estudiantes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Estudiantes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EstudianteID,Matricula,Nombre,Apellidos")] Estudiante estudiante)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estudiante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estudiante);
        }

        // GET: Estudiantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiante = await _context.Estudiante.FindAsync(id);
            if (estudiante == null)
            {
                return NotFound();
            }
            return View(estudiante);
        }

        // POST: Estudiantes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EstudianteID,Matricula,Nombre,Apellidos")] Estudiante estudiante)
        {
            if (id != estudiante.EstudianteID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estudiante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstudianteExists(estudiante.EstudianteID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(estudiante);
        }

        // GET: Estudiantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiante = await _context.Estudiante
                .FirstOrDefaultAsync(m => m.EstudianteID == id);
            if (estudiante == null)
            {
                return NotFound();
            }

            return View(estudiante);
        }

        // POST: Estudiantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estudiante = await _context.Estudiante.FindAsync(id);
            _context.Estudiante.Remove(estudiante);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstudianteExists(int id)
        {
            return _context.Estudiante.Any(e => e.EstudianteID == id);
        }
    }
}
