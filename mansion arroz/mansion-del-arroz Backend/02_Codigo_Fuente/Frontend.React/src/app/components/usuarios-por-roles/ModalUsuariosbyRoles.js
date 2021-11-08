import React, { useEffect, useState } from "react";
import { Button, Modal, Form } from "react-bootstrap";
import UsersbyRolesService from "../../api/usuarios-por-roles";
import { useForm } from "../../hooks/useForm.Hook";

export default function ModalUsersbyRoles(props) {
  const informacionRegistro = props.informacionRegistro;
  const [isFormValid, setIsFormValid] = useState(false);
  const [Users, setUsers] = useState([]);
  const [Roles, setRoles] = useState([]);
  const { form, setForm, resetForm, handleChangeForm } = useForm({
    userId: "",
    roleId: "",
    activo: "",
  });

  const openedModal = () => {
    consultarListas();
    if (!props.addRegistro) {
      setForm({
        roleId: informacionRegistro.roleId,
        userId: informacionRegistro.userId,
        activo: informacionRegistro.activo ? "true" : "false",
      });
    }
  };

  useEffect(() => {
    validateForm();
    // eslint-disable-next-line
  }, [form]);

  const validateForm = () => {
    if (form.userId !== "" && form.activo !== "" && form.roleId !== "") {
      setIsFormValid(true);
    } else {
      setIsFormValid(false);
    }
  };
  const consultarListas = () => {
    UsersbyRolesService.getUsuarios().then((response) => {
      if (response.result) {
        setUsers(response.data);
      }
    });
    UsersbyRolesService.getRoles().then((response) => {
      if (response.result) {
        setRoles(response.data);
      }
    });
  };

  const sendForm = () => {
    if (props.addRegistro) {
      UsersbyRolesService.createUsersbyRoles({ ...form }).then((response) => {
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
          {props.addRegistro
            ? "Agregar usuario por rol"
            : "Editar usuario por rol"}
        </Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Form>
          <Form.Group>
            <label>Usuario</label>
            <select
              name="userId"
              className="form-control"
              onChange={handleChangeForm}
              value={form.userId || ""}
            >
              <option value="">Seleccione</option>
              {Users.map((item, index) => {
                return (
                  <option key={index} value={item.userId}>
                    {item.usuario}
                  </option>
                );
              })}
            </select>
          </Form.Group>
          <Form.Group>
            <label>Rol</label>
            <select
              name="roleId"
              className="form-control"
              onChange={handleChangeForm}
              value={form.roleId || ""}
            >
              <option value="">Seleccione</option>
              {Roles.map((item, index) => {
                return (
                  <option key={index} value={item.roleId}>
                    {item.descripcion}
                  </option>
                );
              })}
            </select>
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
