import http from "../../shared/config/Axios.Config";
import AuthService from "../../services/Auth.Service";

export default class CategoriasService {
  static getCategorias() {
    return http.get(`/Categorias`);
  }

  static createCategorias(entry) {
    return http.post(`/Categorias`, { ...entry, usuarioAuditoria: AuthService.UserName });
  }

  static updateCategorias(entry) {
    return http.put(`/Categorias/${entry.categoriaId}`, { ...entry, usuarioAuditoria: AuthService.UserName });
  }

  static deleteCategorias(entry) {
    return http.delete(`/Categorias/${entry.categoriaId}`);
  }
}
