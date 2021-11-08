import http from "../../shared/config/Axios.Config";
import AuthService from "../../services/Auth.Service";

export default class UsuariosporRolesService {
  static getUsersbyRoles() {
    return http.get(`/UsersByRoles/GetListUsersByRoles`);
  }

  static createUsersbyRoles(entry) {
    return http.post(`/UsersByRoles/Create`, {
      ...entry,
      usuarioAuditoria: AuthService.UserName,
    });
  }

  static deleteUsersbyRoles(entry) {
    return http.delete(
      `/UsersByRoles/userId/${entry.userId}/roleId/${entry.roleId}`
    );
  }
  static getUsuarios() {
    return http.get(`/Users`);
  }

  static getRoles() {
    return http.get(`/Roles/GetListRoles`);
  }
}
