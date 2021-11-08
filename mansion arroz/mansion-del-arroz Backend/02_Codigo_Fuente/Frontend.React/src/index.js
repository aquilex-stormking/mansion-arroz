import React from "react";
import ReactDOM from "react-dom";
import { BrowserRouter } from "react-router-dom";

// COMPONENTS
import App from "./app/App";
import Alerts from "./app/shared/layouts/alerts/Alerts";

// CSS Y ESTILOS
import "@fortawesome/fontawesome-free/css/all.min.css";
import "bootstrap/dist/css/bootstrap.min.css";
import "./assets/css/global.css";

//Interceptores
import "./app/shared/interceptores/Auth.Interceptor";
import "./app/shared/interceptores/Loggin.Interceptor";

ReactDOM.render(
  <BrowserRouter>
    <Alerts />
    <App />
  </BrowserRouter>,
  document.getElementById("root")
);
