import http from "../../shared/config/Axios.Config";
import AuthService from "../../services/Auth.Service";

export default class ProductosService {
  static getProductos() {
    return http.get(`/Productos`);
  }

  static createProducto(entry) {
    return http.post(`/Productos`, { ...entry, usuarioAuditoria: AuthService.UserName });
  }

  static updateProducto(entry) {
    return http.put(`/Productos/${entry.productoId}`, { ...entry, usuarioAuditoria: AuthService.UserName });
  }

  static deleteProducto(entry) {
    return http.delete(`/Productos/${entry.productoId}`);
  }

  static getMarcas() {
    return http.get(`/Marcas`);
  }

  static getCategorias() {
    return http.get(`/Categorias`);
  }

  static getProveedores() {
    return http.get(`/Proveedores`);
  }
}
