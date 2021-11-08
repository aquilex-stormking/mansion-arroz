import React, { useEffect, useState } from "react";
import UsuariosService from "../../api/usuarios";
import { useForm } from "../../hooks/useForm.Hook";
import { Button, Modal, Form, Col, Row } from "react-bootstrap";

export default function ModalUsuarios(props) {
  const informacionRegistro = props.informacionRegistro;
  const [isFormValid, setIsFormValid] = useState(false);
  const [typeUsers, setTypeUsers] = useState([]);

  const { form, setForm, resetForm, handleChangeForm } = useForm({
    nombre: "",
    usuario: "",
    apellido: "",
    telefono: "",
    direccion: "",
    claveAcceso: "",
    tipoUsuarioId: "",
    correoElectronico: "",
    numeroIdentificacion: "",
  });

  const openedModal = () => {
    consultarListas();
    if (!props.addRegistro) {
      setForm({ ...informacionRegistro });
    }
  };

  const consultarListas = () => {
    UsuariosService.getTipoUsuarios().then((response) => {
      if (response?.result) {
        setTypeUsers(response.data);
      }
    });
  };

  useEffect(() => {
    validateForm();
    // eslint-disable-next-line
  }, [form]);

  const validateForm = () => {
    if (
      form.nombre !== "" &&
      form.usuario !== "" &&
      form.apellido !== "" &&
      form.telefono !== "" &&
      form.direccion !== "" &&
      form.claveAcceso !== "" &&
      form.tipoUsuarioId !== "" &&
      form.correoElectronico !== "" &&
      form.numeroIdentificacion !== ""
    ) {
      setIsFormValid(true);
    } else {
      setIsFormValid(false);
    }
  };

  const sendForm = () => {
    if (props.addRegistro) {
      UsuariosService.createUser(form).then((response) => {
        if (response?.result) {
          onCloseModal();
          props.getConsultaData({});
        }
      });
    } else {
      UsuariosService.updateUser({ ...form, usuarioId: informacionRegistro.userId }).then((response) => {
        if (response?.result) {
          onCloseModal();
          props.getConsultaData({});
        }
      });
    }
  };

  const onCloseModal = () => {
    props.showModal();
  };

  const resetModal = () => {
    resetForm();
    setIsFormValid(false);
    props.setAddRegistro(true);
  };

  return (
    <Modal
      show={props.isShowModal}
      size="lg"
      centered={true}
      backdrop="static"
      onShow={openedModal}
      onHide={onCloseModal}
      onExiting={resetModal}
    >
      <Modal.Header closeButton>
        <Modal.Title>{props.addRegistro ? "Agregar usuario" : "Editar usuario"}</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Form>
          <Row>
            <Col md={4}>
              <Form.Group>
                <label>Nombre</label>
                <input
                  type="text"
                  name="nombre"
                  className="form-control"
                  value={form.nombre || ""}
                  onChange={handleChangeForm}
                />
              </Form.Group>
            </Col>
            <Col md={4}>
              <Form.Group>
                <label>Apellido</label>
                <input
                  type="text"
                  name="apellido"
                  className="form-control"
                  value={form.apellido || ""}
                  onChange={handleChangeForm}
                />
              </Form.Group>
            </Col>
            <Col md={4}>
              <Form.Group>
                <label>Número de Identificación</label>
                <input
                  type="text"
                  className="form-control"
                  name="numeroIdentificacion"
                  onChange={handleChangeForm}
                  value={form.numeroIdentificacion || ""}
                />
              </Form.Group>
            </Col>
            <Col md={4}>
              <Form.Group>
                <label>Teléfono</label>
                <input
                  type="text"
                  name="telefono"
                  className="form-control"
                  value={form.telefono || ""}
                  onChange={handleChangeForm}
                />
              </Form.Group>
            </Col>
            <Col md={4}>
              <Form.Group>
                <label>Dirección</label>
                <input
                  type="text"
                  name="direccion"
                  className="form-control"
                  value={form.direccion || ""}
                  onChange={handleChangeForm}
                />
              </Form.Group>
            </Col>
            <Col md={4}>
              <Form.Group>
                <label>Correo Electrónico</label>
                <input
                  type="text"
                  name="correoElectronico"
                  className="form-control"
                  onChange={handleChangeForm}
                  value={form.correoElectronico || ""}
                />
              </Form.Group>
            </Col>
            <Col md={4}>
              <Form.Group>
                <label>Tipo Usuario</label>
                <select
                  name="tipoUsuarioId"
                  className="form-control"
                  onChange={handleChangeForm}
                  value={form.tipoUsuarioId || ""}
                >
                  <option value="">Seleccione</option>
                  {typeUsers.map((item, index) => {
                    return (
                      <option key={index} value={item.userTypeId}>
                        {item.descripcion}
                      </option>
                    );
                  })}
                </select>
              </Form.Group>
            </Col>
            <Col md={4}>
              <Form.Group>
                <label>Usuario</label>
                <input
                  type="text"
                  name="usuario"
                  className="form-control"
                  value={form.usuario || ""}
                  onChange={handleChangeForm}
                />
              </Form.Group>
            </Col>
            <Col md={4}>
              <Form.Group>
                <label>Contraseña</label>
                <input
                  type="password"
                  name="claveAcceso"
                  className="form-control"
                  value={form.claveAcceso || ""}
                  onChange={handleChangeForm}
                />
              </Form.Group>
            </Col>
          </Row>
        </Form>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="outline-danger" onClick={onCloseModal}>
          Cancelar
        </Button>
        <Button variant="danger" disabled={!isFormValid} onClick={sendForm}>
          {props.addRegistro ? "Agregar" : "Editar"}
        </Button>
      </Modal.Footer>
    </Modal>
  );
}
