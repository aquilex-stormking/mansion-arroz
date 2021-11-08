using MansionArroz.Model;
using MansionArroz.Net.Models;
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
    public class TipoUsuariosController : ControllerBase
    {
        private readonly mansion_arrozContext _context;

        public TipoUsuariosController(mansion_arrozContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<ActionResult> Crear(CreateUserTypes createUserTypes)
        {

            var JsonRespuesta = new JsonResponse();
            var tipoUsuario = new AcBasTiposUsuario();
            tipoUsuario.Descripcion = createUserTypes.descripcion;
            //tipoUsuario.Activo = createUserTypes.activo;
            tipoUsuario.FechaCreacion = DateTime.Now;
            tipoUsuario.FechaActualizacion = DateTime.Now;
            tipoUsuario.UsuarioCreacion = createUserTypes.usuarioAuditoria;
            tipoUsuario.UsuarioActualizacion = createUserTypes.usuarioAuditoria;

            _context.AcBasTiposUsuarios.Add(tipoUsuario);

            var crear = await _context.SaveChangesAsync();
            if (crear > 0)
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = true;
                JsonRespuesta.Control.AlertType = "success";
                JsonRespuesta.Control.Code = "200";
                JsonRespuesta.Control.Message = "Se creó el tipo de usuario con éxito";
                JsonRespuesta.Control.Show = true;

            }
            else
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "400";
                JsonRespuesta.Control.Message = "No fue posible crear el tipo de usuario";
                JsonRespuesta.Control.Show = true;
            }
            return Ok(JsonRespuesta);
        }
        [HttpGet]
        public async Task<ActionResult> Consultar()
        {
            var JsonRespuesta = new JsonResponse();
            var tipoUsuarios = await _context.AcBasTiposUsuarios
                .Select(x => new UserTypes
                {
                    userTypeId = x.TipoUsuarioId,
                    descripcion = x.Descripcion,
                    active = x.Activo,

                }).ToListAsync();
            if (tipoUsuarios.Count() > 0)
            {
                JsonRespuesta.Data = tipoUsuarios;
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
                JsonRespuesta.Control.Message = "No existen registros";
                JsonRespuesta.Control.Show = true;
            }
            return Ok(JsonRespuesta);

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(long id)
        {
            var JsonRespuesta = new JsonResponse();
            var tipoUsuario = await _context.AcBasTiposUsuarios.FirstOrDefaultAsync(x => x.TipoUsuarioId == id);
            if (tipoUsuario == null)
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "404";
                JsonRespuesta.Control.Message = "No existe el tipo de usuario";
                JsonRespuesta.Control.Show = true;
                return Ok(JsonRespuesta);
            }
            _context.AcBasTiposUsuarios.Remove(tipoUsuario);

            var guardar = await _context.SaveChangesAsync();
            if (guardar > 0)
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = true;
                JsonRespuesta.Control.AlertType = "success";
                JsonRespuesta.Control.Code = "200";
                JsonRespuesta.Control.Message = "Se eliminó el tipo de usuario con éxito";
                JsonRespuesta.Control.Show = true;

            }
            else
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "400";
                JsonRespuesta.Control.Message = "No fue posible eliminar el tipo de usuario";
                JsonRespuesta.Control.Show = true;
            }
            return Ok(JsonRespuesta);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(long id, UpdateUserTypes updateUserTypes)
        {
            var JsonRespuesta = new JsonResponse();
            var tipoUsuario = await _context.AcBasTiposUsuarios.FirstOrDefaultAsync(x => x.TipoUsuarioId == updateUserTypes.userTypeId);
            if (tipoUsuario == null)
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "404";
                JsonRespuesta.Control.Message = "el tipo de usuario no existe";
                JsonRespuesta.Control.Show = true;
                return Ok(JsonRespuesta);
            }

            tipoUsuario.Descripcion = updateUserTypes.descripcion;
            tipoUsuario.Activo = updateUserTypes.active;
            tipoUsuario.UsuarioActualizacion = updateUserTypes.usuarioAuditoria;
            tipoUsuario.FechaActualizacion = DateTime.Now;
            _context.Entry(tipoUsuario).State = EntityState.Modified;

            var guardar = await _context.SaveChangesAsync();
            if (guardar > 0)
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = true;
                JsonRespuesta.Control.AlertType = "success";
                JsonRespuesta.Control.Code = "200";
                JsonRespuesta.Control.Message = "Se editó el tipo de usuario con éxito";
                JsonRespuesta.Control.Show = true;

            }
            else
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "400";
                JsonRespuesta.Control.Message = "No fue posible editar el tipo de usuario";
                JsonRespuesta.Control.Show = true;
            }
            return Ok(JsonRespuesta);
        }
    }
}

