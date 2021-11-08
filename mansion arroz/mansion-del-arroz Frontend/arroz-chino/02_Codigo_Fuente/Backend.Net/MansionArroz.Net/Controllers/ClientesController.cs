using MansionArroz.Model;
using MansionArroz.Net.Models.Clientes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MansionArroz.Net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly mansion_arrozContext _context;

        public ClientesController(mansion_arrozContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<ActionResult> Crear(CreateCliente createCliente)
        {

            var JsonRespuesta = new JsonResponse();
            var cliente = new AcAppCliente();
            cliente.Apellido = createCliente.apellido;
            cliente.Direccion = createCliente.direccion;
            cliente.Nombre = createCliente.nombre;
            cliente.NumeroIdentificacion = createCliente.numeroIdentificacion;
            cliente.Telefono = createCliente.telefono;
            cliente.Activo = true;
            cliente.FechaCreacion = DateTime.Now;
            cliente.FechaActualizacion = DateTime.Now;
            cliente.UsuarioCreacion = createCliente.usuarioAuditoria;
            cliente.UsuarioActualizacion = createCliente.usuarioAuditoria;

            _context.AcAppClientes.Add(cliente);

            var crear = await _context.SaveChangesAsync();
            if (crear > 0)
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = true;
                JsonRespuesta.Control.AlertType = "success";
                JsonRespuesta.Control.Code = "200";
                JsonRespuesta.Control.Message = "Se creó el cliente con éxito";
                JsonRespuesta.Control.Show = true;

            }
            else
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "400";
                JsonRespuesta.Control.Message = "No fue posible crear el cliente";
                JsonRespuesta.Control.Show = true;
            }
            return Ok(JsonRespuesta);
        }
        [HttpGet]
        public async Task<ActionResult> Consultar()
        {
            var JsonRespuesta = new JsonResponse();
            var clientes = await _context.AcAppClientes.ToListAsync();
            if (clientes.Count() > 0)
            {
                JsonRespuesta.Data = clientes;
                JsonRespuesta.Result = true;
                JsonRespuesta.Control.AlertType = "success";
                JsonRespuesta.Control.Code = "200";
                JsonRespuesta.Control.Message = "";
                JsonRespuesta.Control.Show = false;

            }
            else
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "404";
                JsonRespuesta.Control.Message = "No existen registros de clientes";
                JsonRespuesta.Control.Show = true;
            }
            return Ok(JsonRespuesta);

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(long id)
        {
            var JsonRespuesta = new JsonResponse();
            var cliente = await _context.AcAppClientes.FirstOrDefaultAsync(x => x.ClienteId == id);
            if (cliente == null)
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "404";
                JsonRespuesta.Control.Message = "No existe el cliente";
                JsonRespuesta.Control.Show = true;
                return Ok(JsonRespuesta);
            }
            _context.AcAppClientes.Remove(cliente);

            var guardar = await _context.SaveChangesAsync();
            if (guardar > 0)
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = true;
                JsonRespuesta.Control.AlertType = "success";
                JsonRespuesta.Control.Code = "200";
                JsonRespuesta.Control.Message = "Se eliminó el cliente con éxito";
                JsonRespuesta.Control.Show = true;

            }
            else
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "400";
                JsonRespuesta.Control.Message = "No fue posible eliminar el cliente";
                JsonRespuesta.Control.Show = true;
            }
            return Ok(JsonRespuesta);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(long id, UpdateCliente updateCliente)
        {
            var JsonRespuesta = new JsonResponse();
            var cliente = await _context.AcAppClientes.FirstOrDefaultAsync(x => x.ClienteId == updateCliente.clienteId);
            if (cliente == null)
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "404";
                JsonRespuesta.Control.Message = "No se pudo encontrar el cliente";
                JsonRespuesta.Control.Show = true;
                return Ok(JsonRespuesta);
            }

            cliente.Apellido = updateCliente.apellido;
            cliente.Nombre = updateCliente.nombre;
            cliente.Telefono = updateCliente.telefono;
            cliente.Direccion = updateCliente.direccion;
            cliente.Activo = updateCliente.activo;
            cliente.NumeroIdentificacion = updateCliente.numeroIdentificacion;
            cliente.UsuarioActualizacion = updateCliente.usuarioAuditoria;
            cliente.FechaActualizacion = DateTime.Now;
            _context.Entry(cliente).State = EntityState.Modified;

            var guardar = await _context.SaveChangesAsync();
            if (guardar > 0)
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = true;
                JsonRespuesta.Control.AlertType = "success";
                JsonRespuesta.Control.Code = "200";
                JsonRespuesta.Control.Message = "Se editó el cliente con éxito";
                JsonRespuesta.Control.Show = true;

            }
            else
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "400";
                JsonRespuesta.Control.Message = "No fue posible editar el cliente";
                JsonRespuesta.Control.Show = true;
            }
            return Ok(JsonRespuesta);
        }
    }
}

