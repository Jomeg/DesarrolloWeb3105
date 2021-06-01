using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppPrueba.Models;
using AppPrueba.Response;
using AppPrueba.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppPrueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private readonly Context _miBd;
        public VentaController(Context miBd)
        {
            _miBd = miBd;

        }

        [HttpGet]

        public IActionResult Get()
        {
            Respuesta oRespuesta = new Respuesta();

            try
            {
                var Lista = (from i in _miBd.Ventas
                             select new VentaViewModel
                             {
                                 Id = i.Id,
                                 CodigoVenta = i.CodigoVenta,
                                 fechaVenta = i.fechaVenta,
                                 ValorVenta = i.ValorVenta,
                                 Cliente = _miBd.Clientes.Where(c => c.Id == i.ClienteId).FirstOrDefault(),
                                 DetallesVentas = _miBd.DetalleVentas.Include("Producto").Where(d => d.VentaId == i.Id).ToList()
                             }).ToList();
                oRespuesta.Exito = 1;
                oRespuesta.Datos = Lista;
            }
            catch (Exception e)
            {
                oRespuesta.Mensaje = e.Message;

            }
            return Ok(oRespuesta);
        }

        [HttpPost]

        public IActionResult Add(VentaViewModel oVenta)
        {
            Respuesta oRespuesta = new Respuesta();

            try
            {
                Venta venta = new Venta();
                venta.CodigoVenta = oVenta.CodigoVenta;
                venta.ClienteId = oVenta.ClienteId;
                venta.fechaVenta = oVenta.fechaVenta;
                venta.ValorVenta = oVenta.ValorVenta;
                _miBd.Ventas.Add(venta);
                _miBd.SaveChanges();
                foreach (var item in oVenta.ProductosIds)
                {
                    DetalleVenta detalle = new DetalleVenta();
                    detalle.VentaId = venta.Id;
                    detalle.ProductoId = item;
                    _miBd.DetalleVentas.Add(detalle);
                    _miBd.SaveChanges();
                }
                oRespuesta.Exito = 1;

            }
            catch (Exception e)
            {
                oRespuesta.Mensaje = e.Message;

            }
            return Ok(oRespuesta);
        }

        [HttpPut]

        public IActionResult Update(VentaViewModel oVenta)
        {
            Respuesta oRespuesta = new Respuesta();

            try
            {
                var venta = _miBd.Ventas.Find(oVenta.Id);
                venta.CodigoVenta = oVenta.CodigoVenta;
                venta.ClienteId = oVenta.ClienteId;
                _miBd.Ventas.Update(venta);
                _miBd.SaveChanges();
                oRespuesta.Exito = 1;

            }
            catch (Exception e)
            {
                oRespuesta.Mensaje = e.Message;

            }
            return Ok(oRespuesta);
        }

        [HttpDelete("{Id}")]

        public IActionResult Delete(int Id)
        {
            Respuesta oRespuesta = new Respuesta();

            try
            {
                var venta = _miBd.Ventas.Find(Id);
                _miBd.Ventas.Remove(venta);
                _miBd.SaveChanges();
                oRespuesta.Exito = 1;

            }
            catch (Exception e)
            {
                oRespuesta.Mensaje = e.Message;

            }
            return Ok(oRespuesta);
        }

        [HttpGet ("Clientes")]

        public IActionResult GetClientes()
        {

            Respuesta oRespuesta = new Respuesta();
            try
            {
                var Lista = _miBd.Clientes.Where(C=> C.Estado == true).ToList();
                oRespuesta.Exito = 1;
                oRespuesta.Datos = Lista;

            }
            catch (Exception e)
            {
                oRespuesta.Mensaje = e.Message;
            }

            return Ok(oRespuesta);

        }

        [HttpGet("Productos")]

        public IActionResult GetProductos()
        {

            Respuesta oRespuesta = new Respuesta();
            try
            {
                var Lista = _miBd.Productos.ToList();
                oRespuesta.Exito = 1;
                oRespuesta.Datos = Lista;

            }
            catch (Exception e)
            {
                oRespuesta.Mensaje = e.Message;
            }

            return Ok(oRespuesta);

        }
    }

}