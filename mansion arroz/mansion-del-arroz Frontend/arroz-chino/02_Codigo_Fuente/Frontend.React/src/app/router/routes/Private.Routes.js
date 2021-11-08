import Roles from "../../views/roles/Roles";
import Marcas from "../../views/marcas/Marcas";
import Ventas from "../../views/ventas/Ventas";
import Usuarios from "../../views/usuarios/Usuarios";
import Clientes from "../../views/clientes/Clientes";
import Dashboard from "../../views/dashboard/Dashboard";
import Productos from "../../views/productos/Productos";
import Categorias from "../../views/categorias/Categorias";
import Proveedores from "../../views/proveedores/Proveedores";
import TipoUsuarios from "../../views/tipo-usuarios/TipoUsuarios";
import UsuariosPorRoles from "../../views/usuarios-por-roles/UsuariosPorRoles";

const privateRoutes = [
  {
    name: "Dashboard",
    path: "/dashboard",
    icon: "fas fa-home",
    component: Dashboard,
  },
  {
    name: "Proveedores",
    path: "/Proveedores",
    component: Proveedores,
    icon: "fas fa-address-book",
  },
  {
    name: "Marcas",
    path: "/marcas",
    component: Marcas,
    icon: "fas fa-user-tag",
  },
  {
    name: "Categorías",
    path: "/categorias",
    component: Categorias,
    icon: "fas fa-carrot",
  },
  {
    name: "Productos",
    path: "/productos",
    component: Productos,
    icon: "fas fa-boxes",
  },
  {
    name: "Clientes",
    path: "/clientes",
    component: Clientes,
    icon: "fas fa-user-tie",
  },
  {
    name: "Ventas",
    path: "/ventas",
    component: Ventas,
    icon: "fas fa-clipboard-check",
  },
  {
    name: "Usuarios",
    path: "/usuarios",
    component: Usuarios,
    icon: "fas fa-users",
  },
  {
    name: "Tipo de Usuarios",
    path: "/tipos-de-usuario",
    component: TipoUsuarios,
    icon: "fas fa-user-tag",
  },
  {
    name: "Roles",
    path: "/roles",
    component: Roles,
    icon: "fas fa-user-shield",
  },
  {
    name: "Usuarios por Roles",
    path: "/usuarios-por-rol",
    component: UsuariosPorRoles,
    icon: "fas fa-users-cog",
  },
  {
    name: "404",
    path: "*",
    rediret: "/dashboard",
  },
];

export default privateRoutes;
