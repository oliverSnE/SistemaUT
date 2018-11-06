using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SistemaUTH.Models;

namespace SistemaUTH.Models
{
    public class SistemaUTHContext : IdentityDbContext
    {
        public SistemaUTHContext (DbContextOptions<SistemaUTHContext> options)
            : base(options)
        {
        }

        public DbSet<SistemaUTH.Models.Categoria> Categoria { get; set; }

        public DbSet<SistemaUTH.Models.Estudiante> Estudiante { get; set; }
    }
}
