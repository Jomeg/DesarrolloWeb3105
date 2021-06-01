using AppPrueba.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppPrueba
{
    public class Context: DbContext 
    {
        public Context(DbContextOptions<Context> options): base(options)
        {
            
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<DetalleVenta> DetalleVentas { get; set; }



    }
}
