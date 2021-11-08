using MansionArroz.Model;
using MansionArroz.Net.Models;
using MansionArroz.Net.Models.Ventas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MansionArroz.Net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ventasController : ControllerBase
    {
        private readonly mansion_arrozContext _context;

        public ventasController(mansion_arrozContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Consultar()
        {
            var JsonRespuesta = new JsonResponse();

            var ventas = _context.AcAppVentas.Select(x => new Ventas
            {
                ventaId = x.VentaId,
                clienteId = x.ClienteId,
                clienteNumeroIdentificacion = x.Cliente.NumeroIdentificacion,
                clienteNombreCompleto = $"{x.Cliente.Nombre}  {x.Cliente.Apellido}",
                funcionarioId = x.FuncionarioId,
                funcionarioNumeroIdentificacion = x.Funcionario.NumeroIdentificacion,
                funcionarioNombreCompleto = $"{x.Funcionario.Nombre}  {x.Funcionario.Apellido}",
                promocion = x.Promocion,
                descuento = x.Descuento,
                totalVenta = x.AcAppDetalleVenta.Where(y => y.VentaId == x.VentaId).Sum(x => int.Parse(x.ValorUnitario) * int.Parse(x.Cantidad)),
                total = x.AcAppDetalleVenta.Where(y => y.VentaId == x.VentaId).Sum(x => int.Parse(x.ValorUnitario) * int.Parse(x.Cantidad)),
                detalleVentas = x.AcAppDetalleVenta.Where(y => y.VentaId == x.VentaId).Select(y => new DetalleVenta
                {
                    productoId = y.ProductoId,
                    ProductoDescricpcion = y.Producto.Descripcion,
                    valorUnitario = y.ValorUnitario,
                    valorUnitarioImpuesto = y.ValorUnitarioImpuesto,
                    cantidad = y.Cantidad,
                    total = (int.Parse(y.Cantidad) * int.Parse(y.ValorUnitario)).ToString()
                }).ToList()

            }).ToList();

            if (ventas.Count() > 0)
            {
                JsonRespuesta.Data = ventas;
                JsonRespuesta.Result = true;
                JsonRespuesta.Control.AlertType = "success";
                JsonRespuesta.Control.Code = "200";
                JsonRespuesta.Control.Message = "";
                JsonRespuesta.Control.Show = false;
            }
            else
            {
                JsonRespuesta.Data = ventas;
                JsonRespuesta.Result = true;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "404";
                JsonRespuesta.Control.Message = "no existen registros";
                JsonRespuesta.Control.Show = true;
            }
            return Ok(JsonRespuesta);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CreateVenta createVenta)
        {
            var JsonRespuesta = new JsonResponse();

            var venta = new AcAppVenta();
            venta.FuncionarioId = createVenta.funcionarioId;
            venta.ClienteId = createVenta.clienteId;
            venta.Promocion = createVenta.promocion;
            venta.Descuento = createVenta.descuento;
            venta.Activo = true;
            venta.FechaCreacion = DateTime.Now;
            venta.UsuarioCreacion = createVenta.usuarioAuditoria;
            venta.FechaActualizacion = DateTime.Now;
            venta.UsuarioActualizacion = createVenta.usuarioAuditoria;

            _context.AcAppVentas.Add(venta);

            await _context.SaveChangesAsync();

            var itemsDetalleVenta = new List<AcAppDetalleVenta>();

            foreach (var item in createVenta.productoVenta)
            {
                var producto = _context.AcAppProductos.FirstOrDefault(x => x.ProductoId == item.productoId);

                var detalleVenta = new AcAppDetalleVenta();
                detalleVenta.VentaId = venta.VentaId;
                detalleVenta.ProductoId = item.productoId;
                detalleVenta.ValorUnitarioImpuesto = producto.ValorImpuesto;
                detalleVenta.Cantidad = item.cantidad;
                detalleVenta.ValorUnitario = producto.Valor;
                detalleVenta.Activo = true;
                detalleVenta.FechaCreacion = DateTime.Now;
                detalleVenta.UsuarioCreacion = createVenta.usuarioAuditoria;
                detalleVenta.FechaActualizacion = DateTime.Now;
                detalleVenta.UsuarioActualizacion = createVenta.usuarioAuditoria;

                itemsDetalleVenta.Add(detalleVenta);
            }

            var guardar = await _context.SaveChangesAsync();

            if (guardar > 0)
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = true;
                JsonRespuesta.Control.AlertType = "success";
                JsonRespuesta.Control.Code = "200";
                JsonRespuesta.Control.Message = "Se creó la venta con éxito";
                JsonRespuesta.Control.Show = false;

            }
            else
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "400";
                JsonRespuesta.Control.Message = "No fue posible realizar la venta";
                JsonRespuesta.Control.Show = true;
            }
            return Ok(JsonRespuesta);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(long id)
        {
            var JsonRespuesta = new JsonResponse();

            var venta = _context.AcAppVentas.FirstOrDefault(x => x.VentaId == id);

            if (venta is null)
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "400";
                JsonRespuesta.Control.Message = "No fue posible realizar la venta";
                JsonRespuesta.Control.Show = true;

                return Ok(JsonRespuesta);
            }

            var itemsDetalleVenta = _context.AcAppDetalleVentas.Where(x => x.VentaId == id).ToList();

            if (itemsDetalleVenta.Count > 0)
            {
                _context.AcAppDetalleVentas.RemoveRange(itemsDetalleVenta);
            }

            _context.AcAppVentas.Remove(venta);

            var guardar = await _context.SaveChangesAsync();

            if (guardar > 0)
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = true;
                JsonRespuesta.Control.AlertType = "success";
                JsonRespuesta.Control.Code = "200";
                JsonRespuesta.Control.Message = "Se eliminó la venta con éxito";
                JsonRespuesta.Control.Show = false;

            }
            else
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "400";
                JsonRespuesta.Control.Message = "No fue posible eliminar la venta";
                JsonRespuesta.Control.Show = true;
            }
            return Ok(JsonRespuesta);



        }

    }
}
