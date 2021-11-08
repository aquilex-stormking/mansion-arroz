import http from "../../shared/config/Axios.Config";
import AuthService from "../../services/Auth.Service";

export default class ClientesService {
  static getClientes() {
    return http.get(`/Clientes`);
  }

  static createClientes(entry) {
    return http.post(`/Clientes`, { ...entry, usuarioAuditoria: AuthService.UserName });
  }

  static updateClientes(entry) {
    return http.put(`/Clientes/${entry.clienteId}`, { ...entry, usuarioAuditoria: AuthService.UserName });
  }

  static deleteClientes(entry) {
    return http.delete(`/Clientes/${entry.clienteId}`);
  }
}
