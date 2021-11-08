using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MansionArroz.Net;
using MansionArroz.Net.Models;
using MansionArroz.Model;

namespace MansionArroz.Net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly mansion_arrozContext _context;

        public RolesController(mansion_arrozContext context)
        {
            _context = context;
        }

        [Route("GetListRoles")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AcAppRole>>> GetRoles()
        {
            var JsonRespuesta = new JsonResponse();
            var roles = await _context.AcAppRoles.Select(x => new Role
            {
                roleId = x.RolId,
                descripcion = x.Descripcion,
                active = x.Activo
            }).ToListAsync();


            if (roles.Any())
            {
                JsonRespuesta.Data = roles;
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

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(long id, UpdateRole updateRole)
        {
            var JsonRespuesta = new JsonResponse();
            var role = await _context.AcAppRoles.FirstOrDefaultAsync(x => x.RolId == updateRole.roleId);
            if (role == null)
            {
                return NotFound();
            }
            role.Descripcion = updateRole.descripcion;
            role.Activo = updateRole.active;
            role.UsuarioCreacion = updateRole.usuarioAuditoria;
            role.FechaActualizacion = DateTime.Now;

            _context.Entry(role).State = EntityState.Modified;

            var guardar = await _context.SaveChangesAsync();
            if (guardar > 0)
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = true;
                JsonRespuesta.Control.AlertType = "success";
                JsonRespuesta.Control.Code = "200";
                JsonRespuesta.Control.Message = "El registro se editó con éxito";
                JsonRespuesta.Control.Show = true;
            }
            else

            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "400";
                JsonRespuesta.Control.Message = "No fue posible editar el registro";
                JsonRespuesta.Control.Show = true;
            }

            return Ok(JsonRespuesta);
        }

        [Route("Create")]
        [HttpPost]
        public async Task<ActionResult<AcAppRole>> CreateRole(CreateRole createRole)
        {
            var JsonRespuesta = new JsonResponse();
            var role = new AcAppRole();
            role.Descripcion = createRole.descripcion;
            role.Activo = true;
            role.UsuarioCreacion = createRole.usuarioAuditoria;
            role.UsuarioActualizacion = createRole.usuarioAuditoria;
            role.FechaCreacion = DateTime.Now;
            role.FechaActualizacion = DateTime.Now;




            _context.AcAppRoles.Add(role);
            var guardar = await _context.SaveChangesAsync();
            if (guardar > 0)
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = true;
                JsonRespuesta.Control.AlertType = "success";
                JsonRespuesta.Control.Code = "200";
                JsonRespuesta.Control.Message = "El registro se creó con éxito";
                JsonRespuesta.Control.Show = true;
            }
            else

            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "400";
                JsonRespuesta.Control.Message = "No fue posible crear el registro";
                JsonRespuesta.Control.Show = true;
            }

            return Ok(JsonRespuesta);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(long id)
        {
            var JsonRespuesta = new JsonResponse();

            var role = await _context.AcAppRoles.FirstOrDefaultAsync(x => x.RolId == id);
            if (role == null)
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "404";
                JsonRespuesta.Control.Message = "El registro no existe";
                JsonRespuesta.Control.Show = true;
                return Ok(JsonRespuesta);

            }

            _context.AcAppRoles.Remove(role);

            var guardar = await _context.SaveChangesAsync();
            if (guardar > 0)
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = true;
                JsonRespuesta.Control.AlertType = "success";
                JsonRespuesta.Control.Code = "200";
                JsonRespuesta.Control.Message = "El registro se eliminó con éxito";
                JsonRespuesta.Control.Show = true;
            }
            else

            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "400";
                JsonRespuesta.Control.Message = "No fue posible eliminar el registro";
                JsonRespuesta.Control.Show = true;
            }

            return Ok(JsonRespuesta);

        }
    }
}
