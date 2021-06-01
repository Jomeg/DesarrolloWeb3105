using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppPrueba.Models
{
    public class Venta
    {
        public int Id { get; set; }
        public string CodigoVenta  { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime fechaVenta { get; set; }
        public double ValorVenta { get; set; }

    }
}
