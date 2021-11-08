using MansionArroz.Model;
using MansionArroz.Net.Models;
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
    public class ProductosController : ControllerBase
    {
        private readonly mansion_arrozContext _context;

        public ProductosController(mansion_arrozContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CreateProducto createProducto)
        {
            var JsonRespuesta = new JsonResponse();
            var producto = new AcAppProducto();
            producto.MarcaId = createProducto.marcaId;
            producto.ProveedorId = createProducto.proveedorId;
            producto.CategoriaId = createProducto.categoriaId;
            producto.Descripcion = createProducto.descripcion;
            producto.Observaciones = createProducto.observaciones;
            producto.Valor = createProducto.valor;
            producto.ValorImpuesto = createProducto.valorimpuesto;
            producto.Activo = createProducto.activo;
            producto.FechaCreacion = DateTime.Now;
            producto.UsuarioCreacion = createProducto.usuarioAuditoria;
            producto.FechaActualizacion = DateTime.Now;
            producto.UsuarioActualizacion = createProducto.usuarioAuditoria;

            _context.AcAppProductos.Add(producto);
            await _context.SaveChangesAsync();


            var inventario = new List<AcAppInventario>();

            for (long i = 0; i < createProducto.cantidad; i++)
            {
                var añadirProducto = new AcAppInventario();
                añadirProducto.ProductoId = producto.ProductoId;
                añadirProducto.Activo = createProducto.activo;
                añadirProducto.FechaCreacion = DateTime.Now;
                añadirProducto.UsuarioCreacion = createProducto.usuarioAuditoria;
                añadirProducto.FechaCreacion = DateTime.Now;
                añadirProducto.UsuarioActualizacion = createProducto.usuarioAuditoria;

                inventario.Add(añadirProducto);
            }

            _context.AcAppInventarios.AddRange(inventario);

            var guardar = await _context.SaveChangesAsync();

            if (guardar > 0)
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = true;
                JsonRespuesta.Control.AlertType = "success";
                JsonRespuesta.Control.Code = "200";
                JsonRespuesta.Control.Message = "Se creó el producto con éxito";
                JsonRespuesta.Control.Show = true;
            }
            else
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "400";
                JsonRespuesta.Control.Message = "No fue posible crear el producto";
                JsonRespuesta.Control.Show = true;
            }
            return Ok(JsonRespuesta);
        }

        [HttpGet]
        public async Task<IActionResult> Consultar()
        {
            var JsonRespuesta = new JsonResponse();
            var productos = await _context.AcAppProductos
                .Select(x => new productos
                {
                    productoId = x.ProductoId,
                    marcaId = x.MarcaId,
                    proveedorId = x.ProveedorId,
                    categoriaId = x.CategoriaId,
                    descripcion = x.Descripcion,
                    observaciones = x.Observaciones,
                    valor = x.Valor,
                    valorimpuesto = x.ValorImpuesto,
                    cantidad = x.AcAppInventarios.Count(y => y.ProductoId == x.ProductoId),
                    activo = x.Activo,
                }).ToListAsync();

            if (productos.Count() > 0)
            {
                JsonRespuesta.Data = productos;
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
        public async Task<IActionResult> Editar(long id, UpdateProducto updateProducto)
        {
            var JsonRespuesta = new JsonResponse();

            var producto = await _context.AcAppProductos.FirstOrDefaultAsync(x => x.ProductoId == updateProducto.productoId);

            if (producto is null)
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "404";
                JsonRespuesta.Control.Message = "El producto no existe";
                JsonRespuesta.Control.Show = true;
                return Ok(JsonRespuesta);

            }
            else
            {
                producto.MarcaId = updateProducto.marcaId;
                producto.ProveedorId = updateProducto.proveedorId;
                producto.CategoriaId = updateProducto.categoriaId;
                producto.Descripcion = updateProducto.descripcion;
                producto.Observaciones = updateProducto.observaciones;
                producto.Valor = updateProducto.valor;
                producto.ValorImpuesto = updateProducto.valorimpuesto;
                producto.Activo = updateProducto.activo;
                producto.FechaActualizacion = DateTime.Now;
                producto.UsuarioActualizacion = updateProducto.usuarioAuditoria;

                _context.Entry(producto).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                var inventario = new List<AcAppInventario>();
                var inventarioActual = _context.AcAppInventarios
                    .Where(x => x.ProductoId == producto.ProductoId)
                    .Count();

                var cantidadPorAgregar = updateProducto.cantidad - inventarioActual;

                if (cantidadPorAgregar > 0)
                {
                    for (long i = 0; i < cantidadPorAgregar; i++)
                    {
                        var añadirProducto = new AcAppInventario();
                        añadirProducto.ProductoId = producto.ProductoId;
                        añadirProducto.Activo = updateProducto.activo;
                        añadirProducto.FechaCreacion = DateTime.Now;
                        añadirProducto.UsuarioCreacion = updateProducto.usuarioAuditoria;
                        añadirProducto.FechaCreacion = DateTime.Now;
                        añadirProducto.UsuarioActualizacion = updateProducto.usuarioAuditoria;

                        inventario.Add(añadirProducto);
                    }
                }
                else if (cantidadPorAgregar < 0)
                {

                    var cantidadPorEliminar = -cantidadPorAgregar;
                    var itemsPorEliminar = _context.AcAppInventarios.Where(x => x.ProductoId == producto.ProductoId).Take(cantidadPorAgregar).ToList();

                    _context.RemoveRange(itemsPorEliminar);
                }

                var guardar = await _context.SaveChangesAsync();

                if (guardar > 0)
                {
                    JsonRespuesta.Data = null;
                    JsonRespuesta.Result = true;
                    JsonRespuesta.Control.AlertType = "success";
                    JsonRespuesta.Control.Code = "200";
                    JsonRespuesta.Control.Message = "Se actualizó el producto con éxito";
                    JsonRespuesta.Control.Show = true;
                }
                else
                {
                    JsonRespuesta.Data = null;
                    JsonRespuesta.Result = false;
                    JsonRespuesta.Control.AlertType = "danger";
                    JsonRespuesta.Control.Code = "400";
                    JsonRespuesta.Control.Message = "No fue posible actualizar el producto";
                    JsonRespuesta.Control.Show = true;
                }
            }
            return Ok(JsonRespuesta);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(long id)
        {
            var JsonRespuesta = new JsonResponse();

            var producto = _context.AcAppProductos.FirstOrDefaultAsync(x => x.ProductoId == id).Result;

            if (producto == null)
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "404";
                JsonRespuesta.Control.Message = "El producto no existe";
                JsonRespuesta.Control.Show = true;

                return Ok(JsonRespuesta);

            }

            var existeEnVenta = _context.AcAppDetalleVentas.Any(x => x.ProductoId == id);

            if (existeEnVenta)
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "400";
                JsonRespuesta.Control.Message = "No es posible eliminar producto, porque se encuentra asociado a una venta";
                JsonRespuesta.Control.Show = true;
                return Ok(JsonRespuesta);

            }


            var invetarioPorEliminar = _context.AcAppInventarios.Where(x => x.ProductoId == id).ToList();

            if (invetarioPorEliminar.Count() > 0)
            {
                _context.AcAppInventarios.RemoveRange(invetarioPorEliminar);
            }

            _context.AcAppProductos.Remove(producto);

            var guardar = await _context.SaveChangesAsync();


            if (guardar > 0)
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "success";
                JsonRespuesta.Control.Code = "200";
                JsonRespuesta.Control.Message = "Se eliminó el producto con éxito";
                JsonRespuesta.Control.Show = true;
            }
            else
            {
                JsonRespuesta.Data = null;
                JsonRespuesta.Result = false;
                JsonRespuesta.Control.AlertType = "danger";
                JsonRespuesta.Control.Code = "400";
                JsonRespuesta.Control.Message = "No fue posible eliminar el producto";
                JsonRespuesta.Control.Show = true;

            }

            return Ok(JsonRespuesta);
        }
    }
}
