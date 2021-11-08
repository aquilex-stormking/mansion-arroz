import http from "../../shared/config/Axios.Config";
import AuthService from "../../services/Auth.Service";

export default class TipoUsuariosService {
  static getTipoUsuario(entry) {
    return http.get(`/TipoUsuarios`);
  }

  static createTipoUsuario(entry) {
    return http.post(`/TipoUsuarios`, { ...entry, usuarioAuditoria: AuthService.UserName });
  }

  static updateTipoUsuario(entry) {
    return http.put(`/TipoUsuarios/${entry.userTypeId}`, { ...entry, usuarioAuditoria: AuthService.UserName });
  }

  static deleteTipoUsuario(entry) {
    return http.delete(`/TipoUsuarios/${entry.userTypeId}`);
  }
}
