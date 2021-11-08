using MansionArroz.Model;
using MansionArroz.Net.Models;
using MansionArroz.Net.Models.Users;
using MansionArroz.Net.Utility;
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
    public class UsersController : ControllerBase
    {


        private readonly mansion_arrozContext _context;

        public UsersController(mansion_arrozContext context)
        {
            _context = context;

        }
        [HttpPost]
        public async Task<ActionResult> Crear(CreateUser createUser)
        {
            var JsonRespuesta = new JsonResponse();
            var usuario = new AcBasUsuario();
            usuario.Usuario = createUser.usuario;
            usuario.ClaveAcceso = Seguridad.Encriptar(createUser.claveAcceso);
            usuario.CorreoElectronico = createUser.correoElectronico;
            usuario.Activo = true;
            usuario.TipoUsuarioId = createUser.tipoUsuarioId;
            usuario.FechaCreacion = DateTime.Now;
            usuario.FechaActualizacion = DateTime.Now;
            usuario.UsuarioCreacion = createUser.usuarioAuditoria;
            usuario.UsuarioActualizacion = createUser.usuarioAuditoria;

            _context.AcBasUsuarios.Add(usuario);

            await _context.SaveChangesAsync();


            var funcionario = new AcAppFuncionario();
            funcionario.UsuarioId = usuario.UsuarioId;
            funcionario.NumeroIdentificacion = createUser.numeroIdentificacion;
            funcionario.Nombre = createUser.nombre;
            funcionario.Apellido = createUser.apellido;
            funcionario.Telefono = createUser.telefono;
            funcionario.Direccion = createUser.direccion;
            funcionario.Activo = true;
            funcionario.FechaCreacion = DateTime.Now;
            funcionario.FechaActualizacion = DateTime.Now;
            funcionario.UsuarioCreacion = createUser.usuarioAuditoria;
            funcionario.UsuarioActualizacion = createUser.usuarioAuditoria;

            _context.AcAppFuncionarios.Add(funcionario);

            var crear = await _context.SaveChangesAsync();
            if (crear > 0)
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = true;
                JsonRespuesta.Control.AlertType = "success";
                JsonRespuesta.Control.Code = "200";
                JsonRespuesta.Control.Message = "Se creó el usuario con éxito";
                JsonRespuesta.Control.Show = true;

            }
            else
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "400";
                JsonRespuesta.Control.Message = "No fue posible crear al usuario";
                JsonRespuesta.Control.Show = true;
            }
            return Ok(JsonRespuesta);
        }
        [HttpGet]
        public async Task<ActionResult> Consultar()
        {

            var JsonRespuesta = new JsonResponse();
            var usuarios = await _context.AcAppFuncionarios
                .Select(x => new User
                {
                    userId = x.UsuarioId,
                    funcionarioId = x.FuncionarioId,
                    nombre = x.Nombre,
                    apellido = x.Apellido,
                    correoElectronico = x.Usuario.CorreoElectronico,
                    usuario = x.Usuario.Usuario,
                    claveAcceso = Seguridad.Desencriptar(x.Usuario.ClaveAcceso),
                    numeroIdentificacion = x.NumeroIdentificacion,
                    direccion = x.Direccion,
                    activo = x.Activo,
                    telefono = x.Telefono,
                    tipoUsuarioId = x.Usuario.TipoUsuarioId,
                    tipoUsuarioDescripcion = x.Usuario.TipoUsuario.Descripcion

                }).ToListAsync();
            if (usuarios.Count() > 0)
            {
                JsonRespuesta.Data = usuarios;
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
            var usuario = await _context.AcBasUsuarios.FirstOrDefaultAsync(x => x.UsuarioId == id);
            if (usuario == null)
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "404";
                JsonRespuesta.Control.Message = "No existe el usuario";
                JsonRespuesta.Control.Show = true;
                return Ok(JsonRespuesta);
            }

            var funcionario = await _context.AcAppFuncionarios.FirstOrDefaultAsync(x => x.UsuarioId == id);

            _context.AcAppFuncionarios.Remove(funcionario);

            await _context.SaveChangesAsync();

            _context.AcBasUsuarios.Remove(usuario);

            var guardar = await _context.SaveChangesAsync();
            if (guardar > 0)
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = true;
                JsonRespuesta.Control.AlertType = "success";
                JsonRespuesta.Control.Code = "200";
                JsonRespuesta.Control.Message = "Se eliminó el usuario con éxito";
                JsonRespuesta.Control.Show = true;

            }
            else
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "400";
                JsonRespuesta.Control.Message = "No fue posible eliminar al usuario";
                JsonRespuesta.Control.Show = true;
            }
            return Ok(JsonRespuesta);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(long id, UpdateUsers updateUsuario)
        {
            var JsonRespuesta = new JsonResponse();
            var usuario = await _context.AcBasUsuarios.FirstOrDefaultAsync(x => x.UsuarioId == updateUsuario.usuarioId);
            if (usuario == null)
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "404";
                JsonRespuesta.Control.Message = "No se pudo encontrar el usuario";
                JsonRespuesta.Control.Show = true;
                return Ok(JsonRespuesta);
            }
            usuario.Usuario = updateUsuario.usuario;
            usuario.ClaveAcceso = Seguridad.Encriptar(updateUsuario.claveAcceso);
            usuario.CorreoElectronico = updateUsuario.correoElectronico;
            usuario.Activo = true;
            usuario.TipoUsuarioId = updateUsuario.tipoUsuarioId;
            usuario.FechaCreacion = DateTime.Now;
            usuario.FechaActualizacion = DateTime.Now;
            usuario.UsuarioActualizacion = updateUsuario.usuarioAuditoria;

            _context.Entry(usuario).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            var funcionario = await _context.AcAppFuncionarios.FirstOrDefaultAsync(x => x.UsuarioId == updateUsuario.usuarioId);
            funcionario.NumeroIdentificacion = updateUsuario.numeroIdentificacion;
            funcionario.Nombre = updateUsuario.nombre;
            funcionario.Apellido = updateUsuario.apellido;
            funcionario.Telefono = updateUsuario.telefono;
            funcionario.Direccion = updateUsuario.direccion;
            funcionario.Activo = updateUsuario.activo;
            funcionario.FechaCreacion = DateTime.Now;
            funcionario.FechaActualizacion = DateTime.Now;
            funcionario.UsuarioCreacion = updateUsuario.usuarioAuditoria;
            funcionario.UsuarioActualizacion = updateUsuario.usuarioAuditoria;

            _context.Entry(funcionario).State = EntityState.Modified;

            var guardar = await _context.SaveChangesAsync();
            if (guardar > 0)
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = true;
                JsonRespuesta.Control.AlertType = "success";
                JsonRespuesta.Control.Code = "200";
                JsonRespuesta.Control.Message = "Se editó el usuario con éxito";
                JsonRespuesta.Control.Show = true;

            }
            else
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "400";
                JsonRespuesta.Control.Message = "No fue posible editar al usuario";
                JsonRespuesta.Control.Show = true;
            }
            return Ok(JsonRespuesta);
        }

        [Route("Login")]
        [HttpPost]
        public async Task<ActionResult> Login(UserLogin usuarioLogin)
        {
            var JsonRespuesta = new JsonResponse();

            if (string.IsNullOrEmpty(usuarioLogin.user) || string.IsNullOrEmpty(usuarioLogin.password))
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "401";
                JsonRespuesta.Control.Message = "Complete los campos";
                JsonRespuesta.Control.Show = true;
            }
            else
            {
                var user = await _context.AcAppFuncionarios
                    .Where(x => x.Usuario.Usuario == usuarioLogin.user
                    && x.Usuario.ClaveAcceso == Seguridad.Encriptar(usuarioLogin.password))
                    .Select(x => new User
                    {
                        userId = x.UsuarioId,
                        funcionarioId = x.FuncionarioId,
                        nombre = x.Nombre,
                        apellido = x.Apellido,
                        correoElectronico = x.Usuario.CorreoElectronico,
                        usuario = x.Usuario.Usuario,
                        claveAcceso = Seguridad.Desencriptar(x.Usuario.ClaveAcceso),
                        numeroIdentificacion = x.NumeroIdentificacion,
                        direccion = x.Direccion,
                        activo = x.Activo,
                        telefono = x.Telefono,
                        tipoUsuarioId = x.Usuario.TipoUsuarioId,
                        tipoUsuarioDescripcion = x.Usuario.TipoUsuario.Descripcion

                    }).ToListAsync();

                if (user.Count() > 0)
                {
                    JsonRespuesta.Data = user;
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
                    JsonRespuesta.Control.Message = "El usuario y/o contraseña son incorrectos";
                    JsonRespuesta.Control.Show = true;
                }
            }
            return Ok(JsonRespuesta);
        }

        [Route("RecuperatePass")]
        [HttpPost]
        public async Task<ActionResult> RecuperatePass(RecuperatePassword recuperatePassword)
        {
            var JsonRespuesta = new JsonResponse();

            if (string.IsNullOrEmpty(recuperatePassword.correoElectronico))
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "401";
                JsonRespuesta.Control.Message = "Complete los campos";
                JsonRespuesta.Control.Show = false;
            }
            else
            {
                var user = await _context.AcBasUsuarios.FirstOrDefaultAsync(x => x.CorreoElectronico.Equals(recuperatePassword.correoElectronico));
                if (user is null)
                {
                    JsonRespuesta.Data = null;
                    JsonRespuesta.Result = false;
                    JsonRespuesta.Control.AlertType = "danger";
                    JsonRespuesta.Control.Code = "404";
                    JsonRespuesta.Control.Message = "El correo electrónico no se encuentra registrado en la aplicación";
                    JsonRespuesta.Control.Show = true;


                }
                else
                {
                    var clave = Seguridad.GenerarContraseña();

                    user.ClaveAcceso = Seguridad.Encriptar(clave);
                    _context.Entry(user).State = EntityState.Modified;

                    var guardar = await _context.SaveChangesAsync();


                    if (guardar > 0)
                    {
                        Correo correo = new Correo();

                        correo.EnvioCorreo(user.CorreoElectronico, user.Usuario, clave);

                        JsonRespuesta.Data = null;
                        JsonRespuesta.Result = true;
                        JsonRespuesta.Control.AlertType = "success";
                        JsonRespuesta.Control.Code = "200";
                        JsonRespuesta.Control.Message = "Se envió la contraseña al correo electrónico asociado";
                        JsonRespuesta.Control.Show = true;
                    }
                    else
                    {

                        JsonRespuesta.Data = null;
                        JsonRespuesta.Result = false;
                        JsonRespuesta.Control.AlertType = "danger";
                        JsonRespuesta.Control.Code = "400";
                        JsonRespuesta.Control.Message = "El correo electrónico no se encuentra registrado en la aplicación";
                        JsonRespuesta.Control.Show = true;

                    }
                }
            }
            return Ok(JsonRespuesta);
        }

    }


}

