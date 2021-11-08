import React, { useEffect, useState } from "react";
import MarcasService from "../../api/marcas";
import { useForm } from "../../hooks/useForm.Hook";
import { Button, Modal, Form } from "react-bootstrap";

export default function ModalMarcas(props) {
  const informacionRegistro = props.informacionRegistro;
  const [isFormValid, setIsFormValid] = useState(false);

  const { form, setForm, resetForm, handleChangeForm } = useForm({
    descripcion: "",
    active: "",
  });

  const openedModal = () => {
    if (!props.addRegistro) {
      setForm({
        descripcion: informacionRegistro.descripcion,
        active: informacionRegistro.active ? "true" : "false",
      });
    }
  };

  useEffect(() => {
    validateForm();
    // eslint-disable-next-line
  }, [form]);

  const validateForm = () => {
    setIsFormValid(true);

    if (form.descripcion !== "" && form.active !== "") {
      setIsFormValid(true);
    } else {
      setIsFormValid(false);
    }
  };

  const sendForm = () => {
    if (props.addRegistro) {
      MarcasService.createMarca(form).then((response) => {
        if (response?.result) {
          onCloseModal();
          props.getConsultaData({});
        }
      });
    } else {
      MarcasService.updateMarca({
        ...form,
        marcaId: informacionRegistro.marcaId.toString(),
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
        <Modal.Title>{props.addRegistro ? "Agregar marca" : "Editar marca"}</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Form>
          <Form.Group>
            <label className="gris-oscuro">Descripción</label>
            <input
              type="text"
              className="form-control"
              value={form.descripcion || ""}
              name="descripcion"
              onChange={handleChangeForm}
            />
          </Form.Group>

          <Form.Group>
            <label className="gris-oscuro">Estado</label>
            <select className="form-control" value={form.active || ""} name="active" onChange={handleChangeForm}>
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
