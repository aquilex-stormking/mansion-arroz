import React, { useState } from "react";
import { Link } from "react-router-dom";
import UsuariosService from "../../api/usuarios";
import { useForm } from "../../hooks/useForm.Hook";
import AuthService from "../../services/Auth.Service";
import AlertsService from "../../services/Alerts.Service";
import { Button, Card, Col, Container, Form, Row } from "react-bootstrap";

export default function Login() {
  const [isSaveSesion, setisSaveSesion] = useState(false);
  const { form, handleChangeForm } = useForm({ user: "", password: "" });

  const login = () => {
    if (form.user !== "" && form.password !== "") {
      UsuariosService.login(form).then((response) => {
        if (response.data != null) {
          const isSaveOpcion = AuthService.setSesionSave(isSaveSesion);
          if (isSaveOpcion) {
            const isAuth = AuthService.setData({ ...response.data[0] });
            if (isAuth) document.location.href = "/dashboard";
          }
        }
      });
    } else {
      AlertsService.error("Ingrese las credenciales.");
    }
  };

  return (
    <Container>
      <Row>
        <Col sm={9} md={7} lg={5} className="mx-auto">
          <Card className="shadow my-5">
            <Card.Body className="card-body p-4 p-sm-5">
              <Card.Title className="text-center mb-5">
                Iniciar Sesión
              </Card.Title>
              <Form.Group className="mb-3">
                <label>Usuario</label>
                <input
                  className="form-control"
                  name="user"
                  onChange={handleChangeForm}
                />
              </Form.Group>
              <Form.Group className="mb-3">
                <label>Contraseña</label>
                <input
                  type="password"
                  className="form-control"
                  name="password"
                  onChange={handleChangeForm}
                />
              </Form.Group>
              <div className="custom-control custom-checkbox mb-3">
                <input
                  id="saveSesion"
                  type="checkbox"
                  className="custom-control-input"
                  onClick={() => setisSaveSesion(!isSaveSesion)}
                />
                <label className="custom-control-label" htmlFor="saveSesion">
                  Recordarme
                </label>
              </div>
              <div>
                <Button
                  variant="danger"
                  className="btn-login btn-block text-uppercase"
                  onClick={login}
                >
                  Ingresar
                </Button>
              </div>
              <hr className="my-3" />
              <div className="text-center">
                <Link to="/recuperar-clave">Recuperar Clave</Link>
              </div>
            </Card.Body>
          </Card>
        </Col>
      </Row>
    </Container>
  );
}
