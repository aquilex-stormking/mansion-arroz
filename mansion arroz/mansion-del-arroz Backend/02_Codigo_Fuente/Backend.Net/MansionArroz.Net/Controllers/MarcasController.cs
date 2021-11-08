using MansionArroz.Model;
using MansionArroz.Net.Models.Marcas;
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
    public class MarcasController : ControllerBase
    {
        private readonly mansion_arrozContext _context;

        public MarcasController(mansion_arrozContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<ActionResult> Crear(CreateMarca createMarca)
        {

            var JsonRespuesta = new JsonResponse();
            var marca = new AcAppMarca();
            marca.Descripcion = createMarca.descripcion;
            marca.Activo = true;
            marca.FechaCreacion = DateTime.Now;
            marca.FechaActualizacion = DateTime.Now;
            marca.UsuarioCreacion = createMarca.usuarioAuditoria;
            marca.UsuarioActualizacion = createMarca.usuarioAuditoria;

            _context.AcAppMarcas.Add(marca);

            var crear = await _context.SaveChangesAsync();
            if (crear > 0)
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = true;
                JsonRespuesta.Control.AlertType = "success";
                JsonRespuesta.Control.Code = "200";
                JsonRespuesta.Control.Message = "Se creó la marca con éxito";
                JsonRespuesta.Control.Show = true;

            }
            else
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "400";
                JsonRespuesta.Control.Message = "No fue posible crear la marca";
                JsonRespuesta.Control.Show = true;
            }
            return Ok(JsonRespuesta);
        }
        [HttpGet]
        public async Task<ActionResult> Consultar()
        {
            var JsonRespuesta = new JsonResponse();
            var marcas = await _context.AcAppMarcas.ToListAsync();
            if (marcas.Count() > 0)
            {
                JsonRespuesta.Data = marcas;
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
                JsonRespuesta.Control.Message = "No existen registros de marcas";
                JsonRespuesta.Control.Show = true;
            }
            return Ok(JsonRespuesta);

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(long id)
        {
            var JsonRespuesta = new JsonResponse();
            var marca = await _context.AcAppMarcas.FirstOrDefaultAsync(x => x.MarcaId == id);
            if (marca == null)
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "404";
                JsonRespuesta.Control.Message = "No existe la marca";
                JsonRespuesta.Control.Show = true;
                return Ok(JsonRespuesta);
            }
            _context.AcAppMarcas.Remove(marca);

            var guardar = await _context.SaveChangesAsync();
            if (guardar > 0)
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = true;
                JsonRespuesta.Control.AlertType = "success";
                JsonRespuesta.Control.Code = "200";
                JsonRespuesta.Control.Message = "Se eliminó la marca con éxito";
                JsonRespuesta.Control.Show = true;

            }
            else
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "400";
                JsonRespuesta.Control.Message = "No fue posible eliminar la marca";
                JsonRespuesta.Control.Show = true;
            }
            return Ok(JsonRespuesta);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(long id, UpdateMarca updateMarca)
        {
            var JsonRespuesta = new JsonResponse();
            var marca = await _context.AcAppMarcas.FirstOrDefaultAsync(x=>x.MarcaId==updateMarca.marcaId);
            if (marca== null)
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "404";
                JsonRespuesta.Control.Message = "No se pudo encontrar la marca";
                JsonRespuesta.Control.Show = true;
                return Ok(JsonRespuesta);
            }

            marca.Descripcion = updateMarca.descripcion;
            marca.Activo = updateMarca.activo;
            marca.UsuarioActualizacion = updateMarca.usuarioAuditoria;
            marca.FechaActualizacion = DateTime.Now;
            _context.Entry(marca).State = EntityState.Modified;

            var guardar = await _context.SaveChangesAsync();
            if (guardar > 0)
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = true;
                JsonRespuesta.Control.AlertType = "success";
                JsonRespuesta.Control.Code = "200";
                JsonRespuesta.Control.Message = "Se editó la marca con éxito";
                JsonRespuesta.Control.Show = true;

            }
            else
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "400";
                JsonRespuesta.Control.Message = "No fue posible editar la marca";
                JsonRespuesta.Control.Show = true;
            }
            return Ok(JsonRespuesta);
        }
    }
}

