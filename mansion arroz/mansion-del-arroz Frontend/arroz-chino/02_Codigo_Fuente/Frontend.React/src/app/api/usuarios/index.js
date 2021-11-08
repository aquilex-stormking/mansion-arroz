import http from "../../shared/config/Axios.Config";
import AuthService from "../../services/Auth.Service";

export default class UsuariosService {
  static login(entry) {
    return http.post(`/Users/Login`, { ...entry });
  }

  static recuperarClave(entry) {
    return http.post(`/Users/RecuperatePass`, { ...entry });
  }

  static getUsers() {
    return http.get(`/Users`);
  }

  static createUser(entry) {
    return http.post(`/Users`, { ...entry, usuarioAuditoria: AuthService.UserName });
  }

  static updateUser(entry) {
    return http.put(`/Users/${entry.userId}`, { ...entry, usuarioAuditoria: AuthService.UserName });
  }

  static deleteUser(entry) {
    return http.delete(`/Users/${entry.userId}`);
  }

  static getTipoUsuarios() {
    return http.get(`/TipoUsuarios`);
  }
}
