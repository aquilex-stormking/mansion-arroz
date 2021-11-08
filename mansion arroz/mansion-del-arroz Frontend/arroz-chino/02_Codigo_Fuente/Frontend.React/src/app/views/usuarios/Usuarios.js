import React, { useEffect, useState } from "react";
import UsuariosService from "../../api/usuarios";
import { Button, Col, Row } from "react-bootstrap";
import { useModal } from "../../hooks/useModal.Hook";
import { useTable } from "../../hooks/useTable.Hook";
import Tablas from "../../shared/layouts/tablas/Tablas";
import ModalUsuarios from "../../components/usuarios/ModalUsuarios";

export default function Usuarios() {
  const { data, setData, itemsPerPage, paginateData, hanbleChangePage } = useTable([]);
  const { showModal, addRegistro, setShowModal, setAddRegistro, informacionRegistro, setInformacionRegistro } =
    useModal({});
  const [headers] = useState([
    "Nombre",
    "Apellido",
    "Número Identificacion",
    "Teléfono",
    "Dirección",
    "Correo Electrónico",
    "Tipo Usuario",
    "Usuario",
    "Estado",
    "Acciones",
  ]);

  useEffect(() => {
    getUsers({});
    // eslint-disable-next-line
  }, []);

  const getUsers = (data) => {
    UsuariosService.getUsers(data).then((response) => {
      if (response?.result) {
        setData(response.data);
      }
    });
  };

  const loadInformation = (item) => {
    setInformacionRegistro(item);
    setAddRegistro(false);
    setShowModal(true);
  };

  const deleteItem = (item) => {
    UsuariosService.deleteUser({ userId: item.userId }).then((response) => {
      if (response?.result) {
        getUsers({});
      }
    });
  };

  return (
    <Row>
      <Col xs={12} className="pb-3 d-flex justify-content-between">
        <label className="h4 font-weight-light">Usuarios del sistema</label>
        <Button variant="outline-danger" onClick={() => setShowModal(!showModal)}>
          Agregar usuario
        </Button>
      </Col>
      <Tablas headers={headers} sizeData={data.length} itemsPerPage={itemsPerPage} hanbleChangePage={hanbleChangePage}>
        <tbody>
          {paginateData().map((item, index) => {
            return (
              <tr key={index}>
                <td>{item.nombre}</td>
                <td>{item.apellido}</td>
                <td>{item.numeroIdentificacion}</td>
                <td>{item.telefono}</td>
                <td>{item.direccion}</td>
                <td>{item.correoElectronico}</td>
                <td>{item.tipoUsuarioDescripcion}</td>
                <td>{item.usuario}</td>
                <td>{item.activo ? "Activo" : "Inactivo"}</td>
                <td className="p-0">
                  <Button
                    variant="link"
                    className="alert-info size-buttons-acciones rounded-circle my-2 mr-2"
                    onClick={() => loadInformation(item)}
                  >
                    <i className="fas fa-pencil-alt size-icon-edit-table text-info"></i>
                  </Button>
                  <Button
                    variant="link"
                    className="alert-danger size-buttons-acciones rounded-circle my-2 mr-2"
                    onClick={() => deleteItem(item)}
                  >
                    <i className="far fa-trash-alt size-icon-delect-table text-danger"></i>
                  </Button>
                </td>
              </tr>
            );
          })}
        </tbody>
      </Tablas>
      <ModalUsuarios
        isShowModal={showModal}
        showModal={setShowModal}
        addRegistro={addRegistro}
        getConsultaData={getUsers}
        setAddRegistro={setAddRegistro}
        informacionRegistro={informacionRegistro}
      />
    </Row>
  );
}
