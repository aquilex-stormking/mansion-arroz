using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MansionArroz.Net;
using MansionArroz.Net.Models.Roles;
using MansionArroz.Model;

namespace MansionArroz.Net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersByRolesController : ControllerBase
    {
        private readonly mansion_arrozContext _context;

        public UsersByRolesController(mansion_arrozContext context)
        {
            _context = context;
        }

        [Route("GetListUsersByRoles")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AcAppUsuariosPorRole>>> GetUsersByRoles()
        {
            var JsonRespuesta = new JsonResponse();

            var usuariosPorRoles = await _context.AcAppUsuariosPorRoles
                .Include(x => x.Usuario)
                .Include(x => x.Rol)
                .Select(x => new UserByRole
                {
                    roleId = x.RolId,
                    role_Descripcion = x.Rol.Descripcion,
                    userId = x.UsuarioId,
                    user = x.Usuario.Usuario,
                    activo = x.Activo

                }).ToListAsync();


            if (usuariosPorRoles.Any())
            {
                JsonRespuesta.Data = usuariosPorRoles;
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


        [Route("Create")]
        [HttpPost]
        public async Task<ActionResult<AcAppRole>> CreateUserByRole(CreateUserByRole createUserByRole)
        {
            var JsonRespuesta = new JsonResponse();

            var userByRole = new AcAppUsuariosPorRole();
            userByRole.UsuarioId = createUserByRole.userId;
            userByRole.RolId = createUserByRole.roleId;
            userByRole.Activo = true;
            userByRole.UsuarioCreacion = createUserByRole.usuarioAuditoria;
            userByRole.UsuarioActualizacion = createUserByRole.usuarioAuditoria;
            userByRole.FechaCreacion = DateTime.Now;
            userByRole.FechaActualizacion = DateTime.Now;

            _context.AcAppUsuariosPorRoles.Add(userByRole);

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

        [HttpDelete("userId/{userId}/roleId/{roleId}/")]
        public async Task<IActionResult> DeleteUserByRole(long userId, long roleId)
        {
            var JsonRespuesta = new JsonResponse();

            var userByRole = await _context.AcAppUsuariosPorRoles.FirstOrDefaultAsync(x => x.RolId == roleId && x.UsuarioId == userId);

            if (userByRole == null)
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "404";
                JsonRespuesta.Control.Message = "El registro no existe";
                JsonRespuesta.Control.Show = true;
                return Ok(JsonRespuesta);

            }



            _context.AcAppUsuariosPorRoles.Remove(userByRole);
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
