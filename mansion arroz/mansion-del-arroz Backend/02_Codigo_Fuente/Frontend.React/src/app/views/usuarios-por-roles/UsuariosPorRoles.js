import React, { useEffect, useState } from "react";
import UsersbyRolesService from "../../api/usuarios-por-roles/";
import { Button, Col, Row } from "react-bootstrap";
import { useModal } from "../../hooks/useModal.Hook";
import { useTable } from "../../hooks/useTable.Hook";
import Tablas from "../../shared/layouts/tablas/Tablas";
import ModalUsersbyRoles from "../../components/usuarios-por-roles/ModalUsuariosbyRoles";

export default function UsuariosPorRoles() {
  const [headers] = useState(["Usuario", "Rol", "Estado", "Acciones"]);
  const { data, setData, itemsPerPage, paginateData, hanbleChangePage } =
    useTable([]);
  const {
    showModal,
    addRegistro,
    setShowModal,
    setAddRegistro,
    informacionRegistro,
    setInformacionRegistro,
  } = useModal({});

  useEffect(() => {
    getUsersbyRoles({});
    // eslint-disable-next-line
  }, []);

  const getUsersbyRoles = (data) => {
    UsersbyRolesService.getUsersbyRoles(data).then((response) => {
      if (response?.result) {
        setData(response.data);
      }
    });
  };
  const deleteItem = (item) => {
    UsersbyRolesService.deleteUsersbyRoles({
      roleId: item.roleId,
      userId: item.userId,
    }).then((response) => {
      if (response?.result) {
        getUsersbyRoles({});
      }
    });
  };

  return (
    <Row>
      <Col xs={12} className="pb-3 d-flex justify-content-between">
        <label className="h4 font-weight-light">
          Usuarios por Roles del sistema
        </label>
        <Button
          variant="outline-danger"
          onClick={() => setShowModal(!showModal)}
        >
          Agregar usuario por rol
        </Button>
      </Col>
      <Tablas
        headers={headers}
        sizeData={data.length}
        itemsPerPage={itemsPerPage}
        hanbleChangePage={hanbleChangePage}
      >
        <tbody>
          {paginateData().map((item, index) => {
            return (
              <tr key={index}>
                <td>{item.user}</td>
                <td>{item.role_Descripcion}</td>
                <td>{item.activo ? "Activo" : "Inactivo"}</td>
                <td className="p-0">
                  {/* <Button
                    variant="link"
                    className="alert-info size-buttons-acciones rounded-circle my-2 mr-2"
                    onClick={() => loadInformation(item)}
                  >
                    <i className="fas fa-pencil-alt size-icon-edit-table text-info"></i>
                 
                  </Button> */}
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
      <ModalUsersbyRoles
        isShowModal={showModal}
        showModal={setShowModal}
        addRegistro={addRegistro}
        getConsultaData={getUsersbyRoles}
        setAddRegistro={setAddRegistro}
        informacionRegistro={informacionRegistro}
      />
    </Row>
  );
}
