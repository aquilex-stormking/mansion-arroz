using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace MansionArroz.Net
{
    public partial class mansion_arrozContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public mansion_arrozContext()
        {
        }

        public mansion_arrozContext(DbContextOptions<mansion_arrozContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public virtual DbSet<AcAppCategoria> AcAppCategorias { get; set; }
        public virtual DbSet<AcAppCliente> AcAppClientes { get; set; }
        public virtual DbSet<AcAppDetalleVenta> AcAppDetalleVentas { get; set; }
        public virtual DbSet<AcAppDevolucione> AcAppDevoluciones { get; set; }
        public virtual DbSet<AcAppFuncionario> AcAppFuncionarios { get; set; }
        public virtual DbSet<AcAppInventario> AcAppInventarios { get; set; }
        public virtual DbSet<AcAppMarca> AcAppMarcas { get; set; }
        public virtual DbSet<AcAppProducto> AcAppProductos { get; set; }
        public virtual DbSet<AcAppPromocione> AcAppPromociones { get; set; }
        public virtual DbSet<AcAppProveedore> AcAppProveedores { get; set; }
        public virtual DbSet<AcAppRole> AcAppRoles { get; set; }
        public virtual DbSet<AcAppUsuariosPorRole> AcAppUsuariosPorRoles { get; set; }
        public virtual DbSet<AcAppVenta> AcAppVentas { get; set; }
        public virtual DbSet<AcBasTiposUsuario> AcBasTiposUsuarios { get; set; }
        public virtual DbSet<AcBasUsuario> AcBasUsuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(_configuration.GetConnectionString("PostgreSQL"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Spanish_Colombia.1252");

            modelBuilder.Entity<AcAppCategoria>(entity =>
            {
                entity.HasKey(e => e.CategoriaId);

                entity.ToTable("AC_APP_CATEGORIAS");

                entity.Property(e => e.CategoriaId).HasColumnName("CATEGORIA_ID");

                entity.Property(e => e.Activo).HasColumnName("ACTIVO");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("DESCRIPCION");

                entity.Property(e => e.FechaActualizacion).HasColumnName("FECHA_ACTUALIZACION");

                entity.Property(e => e.FechaCreacion).HasColumnName("FECHA_CREACION");

                entity.Property(e => e.UsuarioActualizacion)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("USUARIO_ACTUALIZACION");

                entity.Property(e => e.UsuarioCreacion)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("USUARIO_CREACION");
            });

            modelBuilder.Entity<AcAppCliente>(entity =>
            {
                entity.HasKey(e => e.ClienteId);

                entity.ToTable("AC_APP_CLIENTES");

                entity.Property(e => e.ClienteId).HasColumnName("CLIENTE_ID");

                entity.Property(e => e.Activo).HasColumnName("ACTIVO");

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("APELLIDO");

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnName("DIRECCION");

                entity.Property(e => e.FechaActualizacion).HasColumnName("FECHA_ACTUALIZACION");

                entity.Property(e => e.FechaCreacion).HasColumnName("FECHA_CREACION");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("NOMBRE");

                entity.Property(e => e.NumeroIdentificacion)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("NUMERO_IDENTIFICACION");

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(11)
                    .HasColumnName("TELEFONO");

                entity.Property(e => e.UsuarioActualizacion)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("USUARIO_ACTUALIZACION");

                entity.Property(e => e.UsuarioCreacion)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("USUARIO_CREACION");
            });

            modelBuilder.Entity<AcAppDetalleVenta>(entity =>
            {
                entity.HasKey(e => e.DetalleVentaId);

                entity.ToTable("AC_APP_DETALLE_VENTAS");

                entity.Property(e => e.DetalleVentaId).HasColumnName("DETALLE_VENTA_ID");

                entity.Property(e => e.Activo).HasColumnName("ACTIVO");

                entity.Property(e => e.Cantidad)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("CANTIDAD");

                entity.Property(e => e.FechaActualizacion).HasColumnName("FECHA_ACTUALIZACION");

                entity.Property(e => e.FechaCreacion).HasColumnName("FECHA_CREACION");

                entity.Property(e => e.ProductoId).HasColumnName("PRODUCTO_ID");

                entity.Property(e => e.UsuarioActualizacion)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("USUARIO_ACTUALIZACION");

                entity.Property(e => e.UsuarioCreacion)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("USUARIO_CREACION");

                entity.Property(e => e.ValorUnitario)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("VALOR_UNITARIO");

                entity.Property(e => e.ValorUnitarioImpuesto)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("VALOR_UNITARIO_IMPUESTO");

                entity.Property(e => e.VentaId).HasColumnName("VENTA_ID");

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.AcAppDetalleVenta)
                    .HasForeignKey(d => d.ProductoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AC_APP_DETALLE_VENTAS_PRODUCTO_ID");

                entity.HasOne(d => d.Venta)
                    .WithMany(p => p.AcAppDetalleVenta)
                    .HasForeignKey(d => d.VentaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AC_APP_DETALLE_VENTAS_VENTA_ID");
            });

            modelBuilder.Entity<AcAppDevolucione>(entity =>
            {
                entity.HasKey(e => e.DevolucionId);

                entity.ToTable("AC_APP_DEVOLUCIONES");

                entity.Property(e => e.DevolucionId).HasColumnName("DEVOLUCION_ID");

                entity.Property(e => e.Activo).HasColumnName("ACTIVO");

                entity.Property(e => e.FechaActualizacion).HasColumnName("FECHA_ACTUALIZACION");

                entity.Property(e => e.FechaCreacion).HasColumnName("FECHA_CREACION");

                entity.Property(e => e.UsuarioActualizacion)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("USUARIO_ACTUALIZACION");

                entity.Property(e => e.UsuarioCreacion)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("USUARIO_CREACION");

                entity.Property(e => e.VentaId).HasColumnName("VENTA_ID");

                entity.HasOne(d => d.Venta)
                    .WithMany(p => p.AcAppDevoluciones)
                    .HasForeignKey(d => d.VentaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AC_APP_DEVOLUCIONES_VENTA_ID");
            });

            modelBuilder.Entity<AcAppFuncionario>(entity =>
            {
                entity.HasKey(e => e.FuncionarioId);

                entity.ToTable("AC_APP_FUNCIONARIOS");

                entity.Property(e => e.FuncionarioId).HasColumnName("FUNCIONARIO_ID");

                entity.Property(e => e.Activo).HasColumnName("ACTIVO");

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("APELLIDO");

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnName("DIRECCION");

                entity.Property(e => e.FechaActualizacion).HasColumnName("FECHA_ACTUALIZACION");

                entity.Property(e => e.FechaCreacion).HasColumnName("FECHA_CREACION");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("NOMBRE");

                entity.Property(e => e.NumeroIdentificacion)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("NUMERO_IDENTIFICACION");

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(11)
                    .HasColumnName("TELEFONO");

                entity.Property(e => e.UsuarioActualizacion)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("USUARIO_ACTUALIZACION");

                entity.Property(e => e.UsuarioCreacion)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("USUARIO_CREACION");

                entity.Property(e => e.UsuarioId).HasColumnName("USUARIO_ID");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.AcAppFuncionarios)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AC_APP_FUNCIONARIOS_USUARIO_ID");
            });

            modelBuilder.Entity<AcAppInventario>(entity =>
            {
                entity.HasKey(e => e.InventarioId);

                entity.ToTable("AC_APP_INVENTARIOS");

                entity.Property(e => e.InventarioId).HasColumnName("INVENTARIO_ID");

                entity.Property(e => e.Activo).HasColumnName("ACTIVO");

                entity.Property(e => e.FechaActualizacion).HasColumnName("FECHA_ACTUALIZACION");

                entity.Property(e => e.FechaCreacion).HasColumnName("FECHA_CREACION");

                entity.Property(e => e.ProductoId).HasColumnName("PRODUCTO_ID");

                entity.Property(e => e.UsuarioActualizacion)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("USUARIO_ACTUALIZACION");

                entity.Property(e => e.UsuarioCreacion)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("USUARIO_CREACION");

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.AcAppInventarios)
                    .HasForeignKey(d => d.ProductoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AC_APP_INVENTARIOS_PRODUCTO_ID");
            });

            modelBuilder.Entity<AcAppMarca>(entity =>
            {
                entity.HasKey(e => e.MarcaId);

                entity.ToTable("AC_APP_MARCAS");

                entity.Property(e => e.MarcaId).HasColumnName("MARCA_ID");

                entity.Property(e => e.Activo).HasColumnName("ACTIVO");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("DESCRIPCION");

                entity.Property(e => e.FechaActualizacion).HasColumnName("FECHA_ACTUALIZACION");

                entity.Property(e => e.FechaCreacion).HasColumnName("FECHA_CREACION");

                entity.Property(e => e.UsuarioActualizacion)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("USUARIO_ACTUALIZACION");

                entity.Property(e => e.UsuarioCreacion)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("USUARIO_CREACION");
            });

            modelBuilder.Entity<AcAppProducto>(entity =>
            {
                entity.HasKey(e => e.ProductoId);

                entity.ToTable("AC_APP_PRODUCTOS");

                entity.Property(e => e.ProductoId).HasColumnName("PRODUCTO_ID");

                entity.Property(e => e.Activo).HasColumnName("ACTIVO");

                entity.Property(e => e.CategoriaId).HasColumnName("CATEGORIA_ID");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("DESCRIPCION");

                entity.Property(e => e.FechaActualizacion).HasColumnName("FECHA_ACTUALIZACION");

                entity.Property(e => e.FechaCreacion).HasColumnName("FECHA_CREACION");

                entity.Property(e => e.MarcaId).HasColumnName("MARCA_ID");

                entity.Property(e => e.Observaciones)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("OBSERVACIONES");

                entity.Property(e => e.ProveedorId).HasColumnName("PROVEEDOR_ID");

                entity.Property(e => e.UsuarioActualizacion)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("USUARIO_ACTUALIZACION");

                entity.Property(e => e.UsuarioCreacion)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("USUARIO_CREACION");

                entity.Property(e => e.Valor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("VALOR");

                entity.Property(e => e.ValorImpuesto)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("VALOR_IMPUESTO");

                entity.HasOne(d => d.Categoria)
                    .WithMany(p => p.AcAppProductos)
                    .HasForeignKey(d => d.CategoriaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AC_APP_PRODUCTOS_CATEGORIA_ID");

                entity.HasOne(d => d.Marca)
                    .WithMany(p => p.AcAppProductos)
                    .HasForeignKey(d => d.MarcaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AC_APP_PRODUCTOS_MARCA_ID");

                entity.HasOne(d => d.Proveedor)
                    .WithMany(p => p.AcAppProductos)
                    .HasForeignKey(d => d.ProveedorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AC_APP_PRODUCTOS_PROVEEDOR_ID");
            });

            modelBuilder.Entity<AcAppPromocione>(entity =>
            {
                entity.HasKey(e => e.PromocionId);

                entity.ToTable("AC_APP_PROMOCIONES");

                entity.Property(e => e.PromocionId).HasColumnName("PROMOCION_ID");

                entity.Property(e => e.Activo).HasColumnName("ACTIVO");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("DESCRIPCION");

                entity.Property(e => e.FechaActualizacion).HasColumnName("FECHA_ACTUALIZACION");

                entity.Property(e => e.FechaCreacion).HasColumnName("FECHA_CREACION");

                entity.Property(e => e.UsuarioActualizacion)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("USUARIO_ACTUALIZACION");

                entity.Property(e => e.UsuarioCreacion)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("USUARIO_CREACION");
            });

            modelBuilder.Entity<AcAppProveedore>(entity =>
            {
                entity.HasKey(e => e.ProveedorId);

                entity.ToTable("AC_APP_PROVEEDORES");

                entity.Property(e => e.ProveedorId).HasColumnName("PROVEEDOR_ID");

                entity.Property(e => e.Activo).HasColumnName("ACTIVO");

                entity.Property(e => e.CorreoElectronico)
                    .IsRequired()
                    .HasMaxLength(320)
                    .HasColumnName("CORREO ELECTRONICO");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("DESCRIPCION");

                entity.Property(e => e.FechaActualizacion).HasColumnName("FECHA_ACTUALIZACION");

                entity.Property(e => e.FechaCreacion).HasColumnName("FECHA_CREACION");

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("TELEFONO");

                entity.Property(e => e.UsuarioActualizacion)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("USUARIO_ACTUALIZACION");

                entity.Property(e => e.UsuarioCreacion)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("USUARIO_CREACION");
            });

            modelBuilder.Entity<AcAppRole>(entity =>
            {
                entity.HasKey(e => e.RolId);

                entity.ToTable("AC_APP_ROLES");

                entity.Property(e => e.RolId).HasColumnName("ROL_ID");

                entity.Property(e => e.Activo).HasColumnName("ACTIVO");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("DESCRIPCION");

                entity.Property(e => e.FechaActualizacion).HasColumnName("FECHA_ACTUALIZACION");

                entity.Property(e => e.FechaCreacion).HasColumnName("FECHA_CREACION");

                entity.Property(e => e.UsuarioActualizacion)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("USUARIO_ACTUALIZACION");

                entity.Property(e => e.UsuarioCreacion)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("USUARIO_CREACION");
            });

            modelBuilder.Entity<AcAppUsuariosPorRole>(entity =>
            {
                entity.HasKey(e => new { e.UsuarioId, e.RolId });

                entity.ToTable("AC_APP_USUARIOS_POR_ROLES");

                entity.Property(e => e.UsuarioId).HasColumnName("USUARIO_ID");

                entity.Property(e => e.RolId).HasColumnName("ROL_ID");

                entity.Property(e => e.Activo).HasColumnName("ACTIVO");

                entity.Property(e => e.FechaActualizacion).HasColumnName("FECHA_ACTUALIZACION");

                entity.Property(e => e.FechaCreacion).HasColumnName("FECHA_CREACION");

                entity.Property(e => e.UsuarioActualizacion)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("USUARIO_ACTUALIZACION");

                entity.Property(e => e.UsuarioCreacion)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("USUARIO_CREACION");

                entity.HasOne(d => d.Rol)
                    .WithMany(p => p.AcAppUsuariosPorRoles)
                    .HasForeignKey(d => d.RolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AC_APP_USUARIOS_POR_ROLES_ROL_ID");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.AcAppUsuariosPorRoles)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AC_APP_USUARIOS_POR_ROLES_USUARIO_ID");
            });

            modelBuilder.Entity<AcAppVenta>(entity =>
            {
                entity.HasKey(e => e.VentaId);

                entity.ToTable("AC_APP_VENTAS");

                entity.Property(e => e.VentaId).HasColumnName("VENTA_ID");

                entity.Property(e => e.Activo).HasColumnName("ACTIVO");

                entity.Property(e => e.ClienteId).HasColumnName("CLIENTE_ID");

                entity.Property(e => e.Descuento)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("DESCUENTO");

                entity.Property(e => e.FechaActualizacion).HasColumnName("FECHA_ACTUALIZACION");

                entity.Property(e => e.FechaCreacion).HasColumnName("FECHA_CREACION");

                entity.Property(e => e.FuncionarioId).HasColumnName("FUNCIONARIO_ID");

                entity.Property(e => e.Promocion)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("PROMOCION");

                entity.Property(e => e.UsuarioActualizacion)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("USUARIO_ACTUALIZACION");

                entity.Property(e => e.UsuarioCreacion)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("USUARIO_CREACION");

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.AcAppVenta)
                    .HasForeignKey(d => d.ClienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AC_APP_VENTAS_CLIENTE_ID");

                entity.HasOne(d => d.Funcionario)
                    .WithMany(p => p.AcAppVenta)
                    .HasForeignKey(d => d.FuncionarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AC_APP_VENTAS_FUNCIONARIO_ID");
            });

            modelBuilder.Entity<AcBasTiposUsuario>(entity =>
            {
                entity.HasKey(e => e.TipoUsuarioId);

                entity.ToTable("AC_BAS_TIPOS_USUARIOS");

                entity.Property(e => e.TipoUsuarioId).HasColumnName("TIPO_USUARIO_ID");

                entity.Property(e => e.Activo).HasColumnName("ACTIVO");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("DESCRIPCION");

                entity.Property(e => e.FechaActualizacion).HasColumnName("FECHA_ACTUALIZACION");

                entity.Property(e => e.FechaCreacion).HasColumnName("FECHA_CREACION");

                entity.Property(e => e.UsuarioActualizacion)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("USUARIO_ACTUALIZACION");

                entity.Property(e => e.UsuarioCreacion)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("USUARIO_CREACION");
            });

            modelBuilder.Entity<AcBasUsuario>(entity =>
            {
                entity.HasKey(e => e.UsuarioId);

                entity.ToTable("AC_BAS_USUARIOS");

                entity.Property(e => e.UsuarioId).HasColumnName("USUARIO_ID");

                entity.Property(e => e.Activo).HasColumnName("ACTIVO");

                entity.Property(e => e.ClaveAcceso)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnName("CLAVE_ACCESO");

                entity.Property(e => e.CorreoElectronico)
                    .IsRequired()
                    .HasMaxLength(320)
                    .HasColumnName("CORREO_ELECTRONICO");

                entity.Property(e => e.FechaActualizacion).HasColumnName("FECHA_ACTUALIZACION");

                entity.Property(e => e.FechaCreacion).HasColumnName("FECHA_CREACION");

                entity.Property(e => e.TipoUsuarioId).HasColumnName("TIPO_USUARIO_ID");

                entity.Property(e => e.Usuario)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("USUARIO");

                entity.Property(e => e.UsuarioActualizacion)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("USUARIO_ACTUALIZACION");

                entity.Property(e => e.UsuarioCreacion)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("USUARIO_CREACION");

                entity.HasOne(d => d.TipoUsuario)
                    .WithMany(p => p.AcBasUsuarios)
                    .HasForeignKey(d => d.TipoUsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AC_BAS_USUARIOS_TIPO_USUARIO_ID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
