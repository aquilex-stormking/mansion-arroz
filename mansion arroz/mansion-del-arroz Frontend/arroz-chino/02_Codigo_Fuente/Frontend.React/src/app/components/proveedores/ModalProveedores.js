import React, { useEffect, useState } from "react";
import { useForm } from "../../hooks/useForm.Hook";
import { Button, Modal, Form } from "react-bootstrap";
import ProveedoresService from "../../api/proveedores";

export default function ModalProveedores(props) {
  const informacionRegistro = props.informacionRegistro;
  const [isFormValid, setIsFormValid] = useState(false);

  const { form, setForm, resetForm, handleChangeForm } = useForm({
    Activo: "",
    telefono: "",
    descripcion: "",
    correoElectronico: "",
  });

  const openedModal = () => {
    if (!props.addRegistro) {
      setForm({
        telefono: informacionRegistro.telefono,
        descripcion: informacionRegistro.descripcion,
        correoElectronico: informacionRegistro.correoElectronico,
        Activo: informacionRegistro.activo ? "true" : "false",
      });
    }
  };

  useEffect(() => {
    validateForm();
    // eslint-disable-next-line
  }, [form]);

  const validateForm = () => {
    if (form.descripcion !== "" && form.correoElectronico !== "" && form.telefono !== "" && form.Activo !== "") {
      setIsFormValid(true);
    } else {
      setIsFormValid(false);
    }
  };

  const sendForm = () => {
    if (props.addRegistro) {
      ProveedoresService.createProveedor(form).then((response) => {
        if (response?.result) {
          onCloseModal();
          props.getConsultaData({});
        }
      });
    } else {
      ProveedoresService.updateProveedor({
        ...form,
        proveedorId: informacionRegistro.proveedorId.toString(),
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
        <Modal.Title>{props.addRegistro ? "Agregar proveedor" : "Editar proveedor"}</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Form>
          <Form.Group>
            <label className="gris-oscuro">Nombre</label>
            <input
              type="text"
              className="form-control"
              value={form.descripcion || ""}
              name="descripcion"
              onChange={handleChangeForm}
            />
          </Form.Group>
          <Form.Group>
            <label className="gris-oscuro">Correo Electrónico</label>
            <input
              type="text"
              className="form-control"
              value={form.correoElectronico || ""}
              name="correoElectronico"
              onChange={handleChangeForm}
            />
          </Form.Group>
          <Form.Group>
            <label className="gris-oscuro">Teléfono</label>
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
            <select className="form-control" value={form.Activo || ""} name="Activo" onChange={handleChangeForm}>
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
