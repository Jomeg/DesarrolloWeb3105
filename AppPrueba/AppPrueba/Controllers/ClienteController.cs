using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppPrueba.Response;
using AppPrueba.ViewModels;
using AppPrueba.Models;

namespace AppPrueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly Context _miBd;

        public ClienteController(Context miBd)
        {
            _miBd = miBd;
        }

        [HttpGet]

        public IActionResult Get()
        {

            Respuesta oRespuesta = new Respuesta();
            try
            {
                var Lista = _miBd.Clientes.ToList();
                oRespuesta.Exito = 1;
                oRespuesta.Datos = Lista;

            }
            catch (Exception e)
            {
                oRespuesta.Mensaje= e.Message;
            }

            return Ok(oRespuesta);

        }

        [HttpPost]

        public IActionResult Add(ClienteViewModel oCliente)
        {
            Respuesta oRespuesta = new Respuesta();

            try
            {
                Cliente cliente = new Cliente();
                cliente.Nombre = oCliente.Nombre;
                cliente.Apellido = oCliente.Apellido;
                cliente.Direccion = oCliente.Direccion;
                cliente.Documento = oCliente.Documento;
                cliente.Estado = true;
                _miBd.Clientes.Add(cliente);
                _miBd.SaveChanges();
                oRespuesta.Exito = 1;

            }
            catch (Exception e)
            {
                oRespuesta.Mensaje = e.Message;

            }
            return Ok(oRespuesta);
        }

        [HttpPut]

        public IActionResult Update(ClienteViewModel oCliente)
        {
            Respuesta oRespuesta = new Respuesta();

            try
            {
                var cliente = _miBd.Clientes.Find(oCliente.Id);
                cliente.Nombre = oCliente.Nombre;
                cliente.Apellido = oCliente.Apellido;
                cliente.Direccion = oCliente.Direccion;
                cliente.Documento = oCliente.Documento;
                _miBd.Clientes.Update(cliente);
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

        public IActionResult Delete(int Id )
        {
            Respuesta oRespuesta = new Respuesta();

            try
            {
                var cliente = _miBd.Clientes.Find(Id);
                if (cliente.Estado)
                {
                    cliente.Estado = false;
                }
                else 
                {
                    cliente.Estado = true;                
                }
                _miBd.Clientes.Update(cliente);
                _miBd.SaveChanges();
                oRespuesta.Exito = 1;

            }
            catch (Exception e)
            {
                oRespuesta.Mensaje = e.Message;

            }

            return Ok(oRespuesta);
        }

    }
}
