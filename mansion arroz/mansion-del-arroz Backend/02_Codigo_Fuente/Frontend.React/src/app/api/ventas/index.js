import http from "../../shared/config/Axios.Config";
import AuthService from "../../services/Auth.Service";

export default class VentasService {
  static getVentas() {
    return http.get(`/ventas`);
  }

  static createVenta(entry) {
    return http.post(`/ventas`, { ...entry, usuarioAuditoria: AuthService.UserName });
  }

  static deleteVenta(entry) {
    return http.delete(`/ventas/${entry.roleId}`);
  }
}
