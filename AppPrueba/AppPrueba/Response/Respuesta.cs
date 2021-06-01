using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppPrueba.Response
{
    public class Respuesta
    {
        public int Exito { get; set; }
        public string Mensaje { get; set; }
        public object Datos { get; set; }

        public Respuesta()
        {
            this.Exito = 0;
        }
    }
}
