import ReactDOM from "react-dom";
import http from "../config/Axios.Config";
import Spinner from "../layouts/spinner/Spinner";
import AuthService from "../../services/Auth.Service";
import AlertsService from "../../services/Alerts.Service";

http.interceptors.response.use(
  (response) => {
    ReactDOM.render(<Spinner isOpen={false} />, document.getElementById("loader"));
    const responseData = response.data;
    if (responseData.control.show) {
      AlertsService.publicAlert(responseData.control.alertType, responseData.control.message);
    }
    return response.data;
  },
  (error) => {
    ReactDOM.render(<Spinner isOpen={false} />, document.getElementById("loader"));
    if (error) {
      if (AuthService.isData || !AuthService.token) {
        AlertsService.error("El servicio no está disponible, por favor intente más tarde.");
      }
    }
  }
);
