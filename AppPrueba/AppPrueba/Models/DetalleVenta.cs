using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppPrueba.Models
{
    public class DetalleVenta
    {
        public int Id { get; set; }
        public int VentaId { get; set; }
        public int ProductoId { get; set; }
        public Venta Venta { get; set; }
        public Producto Producto { get; set; }

    }
}
