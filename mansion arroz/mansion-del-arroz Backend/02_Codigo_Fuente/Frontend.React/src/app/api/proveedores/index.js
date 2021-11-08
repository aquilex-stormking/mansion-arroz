import http from "../../shared/config/Axios.Config";
import AuthService from "../../services/Auth.Service";

export default class ProveedoresService {
  static getProveedores() {
    return http.get(`/Proveedores`);
  }

  static createProveedor(entry) {
    return http.post(`/Proveedores`, { ...entry, usuarioAuditoria: AuthService.UserName });
  }

  static updateProveedor(entry) {
    return http.put(`/Proveedores/${entry.proveedorId}`, { ...entry, usuarioAuditoria: AuthService.UserName });
  }

  static deleteProveedor(entry) {
    return http.delete(`/Proveedores/${entry.proveedorId}`);
  }
}
