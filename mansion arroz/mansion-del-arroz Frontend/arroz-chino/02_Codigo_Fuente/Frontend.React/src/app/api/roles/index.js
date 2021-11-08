import http from "../../shared/config/Axios.Config";
import AuthService from "../../services/Auth.Service";

export default class RolesService {
  static getRoles() {
    return http.get(`/Roles/GetListRoles`);
  }

  static createRol(entry) {
    return http.post(`/Roles/Create`, { ...entry, usuarioAuditoria: AuthService.UserName });
  }

  static updateRol(entry) {
    return http.put(`/Roles/${entry.roleId}`, { ...entry, usuarioAuditoria: AuthService.UserName });
  }

  static deleteRol(entry) {
    return http.delete(`/Roles/${entry.roleId}`);
  }
}
