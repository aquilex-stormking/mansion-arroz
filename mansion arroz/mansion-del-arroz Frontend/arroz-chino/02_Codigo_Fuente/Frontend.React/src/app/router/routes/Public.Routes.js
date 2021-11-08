import Login from "../../views/login/Login";
import RecuperarClave from "../../views/recuperar-clave/RecuperarClave";

const publicRoutes = [
  {
    path: "/login",
    component: Login,
  },
  {
    path: "/recuperar-clave",
    component: RecuperarClave,
  },
  {
    path: "*",
    rediret: "/login",
  },
];

export default publicRoutes;
