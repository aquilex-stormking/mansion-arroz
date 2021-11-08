import React, { useEffect, useState } from "react";
import { Button, Modal, Form } from "react-bootstrap";
import ClientesService from "../../api/clientes";
import { useForm } from "../../hooks/useForm.Hook";

export default function ModalRoles(props) {
  const informacionRegistro = props.informacionRegistro;
  const [isFormValid, setIsFormValid] = useState(false);

  const { form, setForm, resetForm, handleChangeForm } = useForm({
    numeroIdentificacion: "",
    nombre: "",
    apellido: "",
    telefono: "",
    direccion: "",
    activo: "",
  });

  const openedModal = () => {
    if (!props.addRegistro) {
      setForm({
        numeroIdentificacion: informacionRegistro.numeroIdentificacion,
        nombre: informacionRegistro.nombre,
        apellido: informacionRegistro.apellido,
        telefono: informacionRegistro.telefono,
        direccion: informacionRegistro.direccion,
        activo: informacionRegistro.activo ? "true" : "false",
      });
    }
  };

  useEffect(() => {
    validateForm();
    // eslint-disable-next-line
  }, [form]);

  const validateForm = () => {
    if (
      form.nombre !== "" &&
      form.apellido !== "" &&
      form.numeroIdentificacion !== "" &&
      form.telefono !== "" &&
      form.direccion !== "" &&
      form.activo !== ""
    ) {
      setIsFormValid(true);
    } else {
      setIsFormValid(false);
    }
  };

  const sendForm = () => {
    if (props.addRegistro) {
      ClientesService.createClientes(form).then((response) => {
        if (response?.result) {
          onCloseModal();
          props.getConsultaData({});
        }
      });
    } else {
      ClientesService.updateClientes({
        ...form,
        clienteId: informacionRegistro.clienteId.toString(),
      }).then((response) => {
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
      size="md"
      centered={true}
      backdrop="static"
      onShow={openedModal}
      onHide={onCloseModal}
      onExiting={resetModal}
    >
      <Modal.Header closeButton>
        <Modal.Title>
          {props.addRegistro ? "Agregar cliente" : "Editar cliente"}
        </Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Form>
          <Form.Group>
            <label className="gris-oscuro">Numero de Identificacion</label>
            <input
              type="text"
              className="form-control"
              value={form.numeroIdentificacion || ""}
              name="numeroIdentificacion"
              onChange={handleChangeForm}
            />
          </Form.Group>
          <Form.Group>
            <label className="gris-oscuro">Nombre</label>
            <input
              type="text"
              className="form-control"
              value={form.nombre || ""}
              name="nombre"
              onChange={handleChangeForm}
            />
          </Form.Group>
          <Form.Group>
            <label className="gris-oscuro">Apellido</label>
            <input
              type="text"
              className="form-control"
              value={form.apellido || ""}
              name="apellido"
              onChange={handleChangeForm}
            />
          </Form.Group>
          <Form.Group>
            <label className="gris-oscuro">Direccion</label>
            <input
              type="text"
              className="form-control"
              value={form.direccion || ""}
              name="direccion"
              onChange={handleChangeForm}
            />
          </Form.Group>
          <Form.Group>
            <label className="gris-oscuro">Telefono</label>
            <input
              type="text"
              className="form-control"
              value={form.telefono || ""}
              name="telefono"
              onChange={handleChangeForm}
            />
          </Form.Group>
          <Form.Group>
            <label className="gris-oscuro">Estado</label>
            <select
              className="form-control"
              value={form.activo || ""}
              name="activo"
              onChange={handleChangeForm}
            >
              <option value="">Seleccione</option>
              <option value="true">Activo</option>
              <option value="false">Inactivo</option>
            </select>
          </Form.Group>
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
