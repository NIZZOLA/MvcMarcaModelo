using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcMarcaModelo.Models;

namespace MvcMarcaModelo.Data
{
    public class MvcMarcaModeloContext : DbContext
    {
        public MvcMarcaModeloContext (DbContextOptions<MvcMarcaModeloContext> options)
            : base(options)
        {
        }

        public DbSet<MvcMarcaModelo.Models.ModeloModel> Modelos { get; set; } = default!;

        public DbSet<MvcMarcaModelo.Models.MarcaModel> Marcas { get; set; } = default!;
    }
}
