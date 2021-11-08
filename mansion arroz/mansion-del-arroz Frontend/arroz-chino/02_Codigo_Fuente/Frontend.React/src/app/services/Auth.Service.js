export default class AuthService {
  static setSesionSave(value) {
    try {
      localStorage.setItem("sesionSave", value.toString());
      return true;
    } catch (error) {
      return false;
    }
  }

  static get isSesionSave() {
    const sesionSave = localStorage.getItem("sesionSave");
    if (sesionSave === "true") {
      return true;
    } else {
      return false;
    }
  }

  static setData(data) {
    try {
      if (this.isSesionSave) {
        localStorage.setItem("data", JSON.stringify(data));
      } else {
        sessionStorage.setItem("data", JSON.stringify(data));
      }
      return true;
    } catch (error) {
      return false;
    }
  }

  static get data() {
    try {
      if (this.isSesionSave) {
        return JSON.parse(localStorage.getItem("data"));
      } else {
        return JSON.parse(sessionStorage.getItem("data"));
      }
    } catch (error) {
      return false;
    }
  }

  static get isData() {
    try {
      let data;
      if (this.isSesionSave) {
        data = JSON.parse(localStorage.getItem("data"));
      } else {
        data = JSON.parse(sessionStorage.getItem("data"));
      }
      if (data != null && data !== undefined) {
        return true;
      }
      return false;
    } catch (error) {
      return false;
    }
  }

  static get IdUser() {
    try {
      let data;
      if (this.isSesionSave) {
        data = JSON.parse(localStorage.getItem("data"));
      } else {
        data = JSON.parse(sessionStorage.getItem("data"));
      }
      return data.userId;
    } catch (error) {
      return false;
    }
  }

  static get UserName() {
    try {
      let data;
      if (this.isSesionSave) {
        data = JSON.parse(localStorage.getItem("data"));
      } else {
        data = JSON.parse(sessionStorage.getItem("data"));
      }
      return data.usuario;
    } catch (error) {
      return false;
    }
  }

  static get FullNameUser() {
    try {
      let data;
      if (this.isSesionSave) {
        data = JSON.parse(localStorage.getItem("data"));
      } else {
        data = JSON.parse(sessionStorage.getItem("data"));
      }
      return `${data.nombre} ${data.apellido}`;
    } catch (error) {
      return false;
    }
  }

  static logout() {
    localStorage.clear();
    sessionStorage.clear();
    document.location.href = "/login";
  }
}
