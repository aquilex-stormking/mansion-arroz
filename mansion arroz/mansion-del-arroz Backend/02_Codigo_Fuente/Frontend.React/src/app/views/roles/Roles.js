import React, { useEffect, useState } from "react";
import RolesService from "../../api/roles";
import { Button, Col, Row } from "react-bootstrap";
import { useModal } from "../../hooks/useModal.Hook";
import { useTable } from "../../hooks/useTable.Hook";
import Tablas from "../../shared/layouts/tablas/Tablas";
import ModalRoles from "../../components/roles/ModalRoles";

export default function Roles() {
  const [headers] = useState(["Descripción", "Estado", "Acciones"]);
  const { data, setData, itemsPerPage, paginateData, hanbleChangePage } = useTable([]);
  const { showModal, addRegistro, setShowModal, setAddRegistro, informacionRegistro, setInformacionRegistro } =
    useModal({});

  useEffect(() => {
    getRoles({});
    // eslint-disable-next-line
  }, []);

  const getRoles = (data) => {
    RolesService.getRoles(data).then((response) => {
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
    RolesService.deleteRol({ Rol_Id: item.roleId }).then((response) => {
      if (response?.result) {
        getRoles({});
      }
    });
  };

  return (
    <Row>
      <Col xs={12} className="pb-3 d-flex justify-content-between">
        <label className="h4 font-weight-light">Roles del sistema</label>
        <Button variant="outline-danger" onClick={() => setShowModal(!showModal)}>
          Agregar rol
        </Button>
      </Col>
      <Tablas headers={headers} sizeData={data.length} itemsPerPage={itemsPerPage} hanbleChangePage={hanbleChangePage}>
        <tbody>
          {paginateData().map((item, index) => {
            return (
              <tr key={index}>
                <td>{item.descripcion}</td>
                <td>{item.active ? "Activo" : "Inactivo"}</td>
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
      <ModalRoles
        isShowModal={showModal}
        showModal={setShowModal}
        addRegistro={addRegistro}
        getConsultaData={getRoles}
        setAddRegistro={setAddRegistro}
        informacionRegistro={informacionRegistro}
      />
    </Row>
  );
}
