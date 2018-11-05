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
    public class CategoriasController : Controller
    {
        private readonly SistemaUTHContext _context;

        public CategoriasController(SistemaUTHContext context)
        {
            _context = context;
        }

        // GET: Categorias
        public async Task<IActionResult> Index(string sortOrder, string searchString,string currentFilter, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;//obtiene la obicacion actual;

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DescripcionSortParm"] = sortOrder== "descripcion_asc"?"descripcion_desc":"descripcion_asc";
            ViewData["CurrentFilter"] = searchString; // obtiene el valor a buscar en el input
            
            if (searchString != null) {
                page = 1;
            }//validacion de la busqueda muestra, si hay resultado o noes lo que muestra.
            else {
                searchString = currentFilter;
            }
            var categorias = from s in _context.Categoria select s; //un select referente a una query

            if (!String.IsNullOrEmpty(searchString))//verificar si la var Serchstring tiene nombre o descripcio
            {
                categorias = categorias.Where(s => s.Name.Contains(searchString) || s.Descripcion.Contains(searchString));
            }
            switch (sortOrder) // ordena lascategorias
            {
                case "name_desc":
                    categorias = categorias.OrderByDescending(s => s.Name);
                        break;
                case "descripcion_asc":
                    categorias = categorias.OrderBy(s => s.Descripcion);
                    break;
                case "descripcion_desc":
                    categorias = categorias.OrderByDescending(s => s.Descripcion);
                    break;
                default:
                    categorias = categorias.OrderBy(s => s.Name);
                    break;
            }
            //return View(await _context.Categoria.ToListAsync());
            // regresa la ista con el ordenamiento realizado a la coleccion Categorias
            //return View(await categorias.AsNoTracking().ToListAsync());

            int pageSize = 5;//visualisa el nuemero de elementos que muestra una vista
            return View(await Paginacion<Categoria>.CreatesAsync(categorias.AsNoTracking(),page??1,pageSize));//regresa el total de resultado de elemento. 

        }

        // GET: Categorias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categoria
                .FirstOrDefaultAsync(m => m.CategoriaID == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // GET: Categorias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoriaID,Name,Descripcion,State")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        // GET: Categorias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categoria.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoriaID,Name,Descripcion,State")] Categoria categoria)
        {
            if (id != categoria.CategoriaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaExists(categoria.CategoriaID))
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
            return View(categoria);
        }

        // GET: Categorias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categoria
                .FirstOrDefaultAsync(m => m.CategoriaID == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoria = await _context.Categoria.FindAsync(id);
            _context.Categoria.Remove(categoria);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaExists(int id)
        {
            return _context.Categoria.Any(e => e.CategoriaID == id);
        }
    }
}
