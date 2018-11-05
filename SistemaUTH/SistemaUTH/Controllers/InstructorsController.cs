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
    public class InstructorsController : Controller
    {
        private readonly SistemaUTHContext _context;

        public InstructorsController(SistemaUTHContext context)
        {
            _context = context;
        }

        // GET: Instructors
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;//obtiene la obicacion actual;

            ViewData["NumSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DescripcionSortParm"] = sortOrder == "descripcion_asc" ? "descripcion_desc" : "descripcion_asc";
            ViewData["CurrentFilter"] = searchString; // obtiene el valor a buscar en el input

            if (searchString != null)
            {
                page = 1;
            }//validacion de la busqueda muestra, si hay resultado o noes lo que muestra.
            else
            {
                searchString = currentFilter;
            }
            var instructores = from s in _context.Instructor select s; //un select referente a una query

            if (!String.IsNullOrEmpty(searchString))//verificar si la var Serchstring tiene nombre o descripcio
            {
                instructores = instructores.Where(s => s.NumEmpleado.Contains(searchString) || s.Name.Contains(searchString));
            }
            switch (sortOrder) // ordena lascategorias
            {
                case "name_desc":
                    instructores = instructores.OrderByDescending(s => s.NumEmpleado);
                    break;
                case "descripcion_asc":
                    instructores = instructores.OrderBy(s => s.Name);
                    break;
                case "descripcion_desc":
                    instructores = instructores.OrderByDescending(s => s.Name);
                    break;
                default:
                    instructores = instructores.OrderBy(s => s.NumEmpleado);
                    break;
            }
            
            int pageSize = 5;//visualisa el nuemero de elementos que muestra una vista
            return View(await Paginacion<Instructor>.CreatesAsync(instructores.AsNoTracking(), page ?? 1, pageSize));//regresa el total de resultado de elemento. 

            //return View(await _context.Instructor.ToListAsync());
        }

        // GET: Instructors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructor
                .FirstOrDefaultAsync(m => m.instructorID == id);
            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }

        // GET: Instructors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Instructors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("instructorID,NumEmpleado,Name,Apellidos,Correo,Celular")] Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(instructor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(instructor);
        }

        // GET: Instructors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructor.FindAsync(id);
            if (instructor == null)
            {
                return NotFound();
            }
            return View(instructor);
        }

        // POST: Instructors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("instructorID,NumEmpleado,Name,Apellidos,Correo,Celular")] Instructor instructor)
        {
            if (id != instructor.instructorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(instructor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstructorExists(instructor.instructorID))
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
            return View(instructor);
        }

        // GET: Instructors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructor
                .FirstOrDefaultAsync(m => m.instructorID == id);
            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }

        // POST: Instructors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var instructor = await _context.Instructor.FindAsync(id);
            _context.Instructor.Remove(instructor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstructorExists(int id)
        {
            return _context.Instructor.Any(e => e.instructorID == id);
        }
    }
}
