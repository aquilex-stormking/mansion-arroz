import http from "../../shared/config/Axios.Config";
import AuthService from "../../services/Auth.Service";

export default class MarcasService {
  static getMarcas() {
    return http.get(`/Marcas`);
  }

  static createMarca(entry) {
    return http.post(`/Marcas`, { ...entry, usuarioAuditoria: AuthService.UserName });
  }

  static updateMarca(entry) {
    return http.put(`/Marcas/${entry.marcaId}`, { ...entry, usuarioAuditoria: AuthService.UserName });
  }

  static deleteMarca(entry) {
    return http.delete(`/Marcas/${entry.marcaId}`);
  }
}
