using MansionArroz.Model;
using MansionArroz.Net.Models.Proveedores;
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
    public class ProveedoresController : ControllerBase
    {
        private readonly mansion_arrozContext _context;

        public ProveedoresController(mansion_arrozContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<ActionResult> Crear(CreateProveedor createProveedor)
        {

            var JsonRespuesta = new JsonResponse();
            var proveedor = new AcAppProveedore();
            proveedor.Descripcion = createProveedor.descripcion;
            proveedor.CorreoElectronico = createProveedor.correoElectronico;
            proveedor.Telefono = createProveedor.telefono;
            proveedor.UsuarioCreacion = createProveedor.usuarioAuditoria;
            proveedor.UsuarioActualizacion = createProveedor.usuarioAuditoria;
            proveedor.Activo = true;            
            proveedor.FechaCreacion = DateTime.Now;
            proveedor.FechaActualizacion = DateTime.Now;
            

            _context.AcAppProveedores.Add(proveedor);

            var crear = await _context.SaveChangesAsync();
            if (crear > 0)
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = true;
                JsonRespuesta.Control.AlertType = "success";
                JsonRespuesta.Control.Code = "200";
                JsonRespuesta.Control.Message = "Se creó el proveedor con éxito";
                JsonRespuesta.Control.Show = true;

            }
            else
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "400";
                JsonRespuesta.Control.Message = "No fue posible crear al proveedor";
                JsonRespuesta.Control.Show = true;
            }
            return Ok(JsonRespuesta);
        }
        [HttpGet]
        public async Task<ActionResult> Consultar()
        {
            var JsonRespuesta = new JsonResponse();
            var proveedor = await _context.AcAppProveedores.ToListAsync();
            if (proveedor.Count() > 0)
            {
                JsonRespuesta.Data = proveedor;
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
                JsonRespuesta.Control.Message = "No existen registros proveedores";
                JsonRespuesta.Control.Show = true;
            }
            return Ok(JsonRespuesta);

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(long id)
        {
            var JsonRespuesta = new JsonResponse();
            var proveedor = await _context.AcAppProveedores.FirstOrDefaultAsync(x => x.ProveedorId == id);
            if (proveedor == null)
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "404";
                JsonRespuesta.Control.Message = "No existe el proveedor";
                JsonRespuesta.Control.Show = true;
                return Ok(JsonRespuesta);
            }
            _context.AcAppProveedores.Remove(proveedor);

            var guardar = await _context.SaveChangesAsync();
            if (guardar > 0)
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = true;
                JsonRespuesta.Control.AlertType = "success";
                JsonRespuesta.Control.Code = "200";
                JsonRespuesta.Control.Message = "Se eliminó proveedor con éxito";
                JsonRespuesta.Control.Show = true;
            }
            else
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "400";
                JsonRespuesta.Control.Message = "No fue posible eliminar el proveedor";
                JsonRespuesta.Control.Show = true;
            }
            return Ok(JsonRespuesta);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(long id, UpdateProveedor updateProveedor)
        {
            var JsonRespuesta = new JsonResponse();
            var proveedor = await _context.AcAppProveedores.FirstOrDefaultAsync(x=>x.ProveedorId==updateProveedor.proveedorId);
            if (proveedor == null)
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "404";
                JsonRespuesta.Control.Message = "No se pudo encontrar el proveedor";
                JsonRespuesta.Control.Show = true;
                return Ok(JsonRespuesta);
            }

            proveedor.Descripcion = updateProveedor.descripcion;
            proveedor.CorreoElectronico = updateProveedor.correoElectronico;
            proveedor.Telefono = updateProveedor.telefono;
            proveedor.Activo = updateProveedor.activo;
            proveedor.UsuarioActualizacion = updateProveedor.usuarioAuditoria;
            proveedor.FechaActualizacion = DateTime.Now;
            _context.Entry(proveedor).State = EntityState.Modified;

            var guardar = await _context.SaveChangesAsync();
            if (guardar > 0)
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = true;
                JsonRespuesta.Control.AlertType = "success";
                JsonRespuesta.Control.Code = "200";
                JsonRespuesta.Control.Message = "Se editó proveedor con éxito";
                JsonRespuesta.Control.Show = true;

            }
            else
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "400";
                JsonRespuesta.Control.Message = "No fue posible editar el proveedor";
                JsonRespuesta.Control.Show = true;
            }
            return Ok(JsonRespuesta);
        }
    }
}

