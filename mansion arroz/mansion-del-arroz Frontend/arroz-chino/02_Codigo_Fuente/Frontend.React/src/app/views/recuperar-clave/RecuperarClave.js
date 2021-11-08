import { Link } from "react-router-dom";
import UsuariosService from "../../api/usuarios";
import { useForm } from "../../hooks/useForm.Hook";
import AlertsService from "../../services/Alerts.Service";
import { Button, Card, Col, Container, Form, Row } from "react-bootstrap";

export default function RecuperarClave() {
  const { form, handleChangeForm } = useForm({ correoElectronico: "" });

  const recuperarClave = () => {
    if (form.correoElectronico !== "") {
      UsuariosService.recuperarClave(form).then(() => {});
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
                Recuperar clave
              </Card.Title>
              <Form.Group className="mb-3">
                <label>Correo Electrónico</label>
                <input
                  type="email"
                  className="form-control"
                  name="correoElectronico"
                  onChange={handleChangeForm}
                />
              </Form.Group>
              <div>
                <Button
                  variant="danger"
                  className="btn-login btn-block text-uppercase"
                  onClick={recuperarClave}
                >
                  Recuperar Clave
                </Button>
              </div>
              <hr className="my-3" />
              <div className="text-center">
                <Link to="/login"> Iniciar sesión </Link>
              </div>
            </Card.Body>
          </Card>
        </Col>
      </Row>
    </Container>
  );
}
