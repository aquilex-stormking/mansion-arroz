using MansionArroz.Model;
using MansionArroz.Net.Models.Categorias;
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
    public class CategoriasController : ControllerBase
    {
        private readonly mansion_arrozContext _context;

        public CategoriasController(mansion_arrozContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<ActionResult> Crear(CreateCategoria createCategoria)
        {

            var JsonRespuesta = new JsonResponse();
            var categoria = new AcAppCategoria();
            categoria.Descripcion = createCategoria.descripcion;
            categoria.Activo = true;
            categoria.FechaCreacion = DateTime.Now;
            categoria.FechaActualizacion = DateTime.Now;
            categoria.UsuarioCreacion = createCategoria.usuarioAuditoria;
            categoria.UsuarioActualizacion = createCategoria.usuarioAuditoria;

            _context.AcAppCategorias.Add(categoria);

            var crear = await _context.SaveChangesAsync();
            if (crear > 0)
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = true;
                JsonRespuesta.Control.AlertType = "success";
                JsonRespuesta.Control.Code = "200";
                JsonRespuesta.Control.Message = "Se creó la categoría con éxito";
                JsonRespuesta.Control.Show = true;

            }
            else
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "400";
                JsonRespuesta.Control.Message = "No fue posible crear la categoría";
                JsonRespuesta.Control.Show = true;
            }
            return Ok(JsonRespuesta);
        }
        [HttpGet]
        public async Task<ActionResult> Consultar()
        {
            var JsonRespuesta = new JsonResponse();
            var categorias = await _context.AcAppCategorias.ToListAsync();
            if (categorias.Count() > 0)
            {
                JsonRespuesta.Data = categorias;
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
                JsonRespuesta.Control.Message = "No existen las categorías";
                JsonRespuesta.Control.Show = true;
            }
            return Ok(JsonRespuesta);

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(long id)
        {
            var JsonRespuesta = new JsonResponse();
            var categoria = await _context.AcAppCategorias.FirstOrDefaultAsync(x => x.CategoriaId == id);
            if (categoria == null)
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "404";
                JsonRespuesta.Control.Message = "No existe la categoria";
                JsonRespuesta.Control.Show = true;
                return Ok(JsonRespuesta);
            }
            _context.AcAppCategorias.Remove(categoria);

            var guardar = await _context.SaveChangesAsync();
            if (guardar > 0)
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = true;
                JsonRespuesta.Control.AlertType = "success";
                JsonRespuesta.Control.Code = "200";
                JsonRespuesta.Control.Message = "Se eliminó la categoría con éxito";
                JsonRespuesta.Control.Show = true;

            }
            else
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "400";
                JsonRespuesta.Control.Message = "No fue posible eliminar la categoría";
                JsonRespuesta.Control.Show = true;
            }
            return Ok(JsonRespuesta);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(long id, UpdateCategoria updatecategoria)
        {
            var JsonRespuesta = new JsonResponse();
            var categoria = await _context.AcAppCategorias.FirstOrDefaultAsync(x=>x.CategoriaId==updatecategoria.categoriaId);
            if (categoria== null)
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "404";
                JsonRespuesta.Control.Message = "No se pudo encontrar la categoría";
                JsonRespuesta.Control.Show = true;
                return Ok(JsonRespuesta);
            }

            categoria.Descripcion = updatecategoria.descripcion;
            categoria.UsuarioActualizacion = updatecategoria.usuarioAuditoria;
            categoria.FechaActualizacion = DateTime.Now;
            categoria.Activo = updatecategoria.activo;
            _context.Entry(categoria).State = EntityState.Modified;

            var guardar = await _context.SaveChangesAsync();
            if (guardar > 0)
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = true;
                JsonRespuesta.Control.AlertType = "success";
                JsonRespuesta.Control.Code = "200";
                JsonRespuesta.Control.Message = "Se editó la categoría con éxito";
                JsonRespuesta.Control.Show = true;

            }
            else
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "400";
                JsonRespuesta.Control.Message = "No fue posible editar la categoría";
                JsonRespuesta.Control.Show = true;
            }
            return Ok(JsonRespuesta);
        }
    }
}

