import ReactDOM from "react-dom";
import http from "../config/Axios.Config";
import Spinner from "../layouts/spinner/Spinner";
import AuthService from "../../services/Auth.Service";

http.interceptors.request.use(
  (request) => {
    ReactDOM.render(<Spinner isOpen={true} />, document.getElementById("loader"));
    if (AuthService.isData) {
      request.headers.Authorization = `Bearer ${AuthService.token}`;
    } else {
      request.headers.Authorization = "PoUedj64]KPoein34/*66dppeuXMn{P]ejeje5&poTd";
    }
    return request;
  },
  (error) => {
    return Promise.reject(error);
  }
);
