import React, { useEffect, useState } from "react";
import ProveedoresService from "../../api/proveedores";
import { Button, Col, Row } from "react-bootstrap";
import { useModal } from "../../hooks/useModal.Hook";
import { useTable } from "../../hooks/useTable.Hook";
import Tablas from "../../shared/layouts/tablas/Tablas";
import ModalProveedores from "../../components/proveedores/ModalProveedores";

export default function Proveedores() {
  const [headers] = useState(["Nombre", "Teléfono", "Correo Electrónico", "Estado", "Acciones"]);
  const { data, setData, itemsPerPage, paginateData, hanbleChangePage } = useTable([]);
  const { showModal, addRegistro, setShowModal, setAddRegistro, informacionRegistro, setInformacionRegistro } =
    useModal({});

  useEffect(() => {
    getProveedores({});
    // eslint-disable-next-line
  }, []);

  const getProveedores = (data) => {
    ProveedoresService.getProveedores(data).then((response) => {
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
    ProveedoresService.deleteProveedor({ proveedorId: item.proveedorId }).then((response) => {
      if (response?.result) {
        getProveedores({});
      }
    });
  };

  return (
    <Row>
      <Col xs={12} className="pb-3 d-flex justify-content-between">
        <label className="h4 font-weight-light">Proveedores</label>
        <Button variant="outline-danger" onClick={() => setShowModal(!showModal)}>
          Agregar proveedor
        </Button>
      </Col>
      <Tablas headers={headers} sizeData={data.length} itemsPerPage={itemsPerPage} hanbleChangePage={hanbleChangePage}>
        <tbody>
          {paginateData().map((item, index) => {
            return (
              <tr key={index}>
                <td>{item.descripcion}</td>
                <td>{item.telefono}</td>
                <td>{item.correoElectronico}</td>
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
      <ModalProveedores
        isShowModal={showModal}
        showModal={setShowModal}
        addRegistro={addRegistro}
        getConsultaData={getProveedores}
        setAddRegistro={setAddRegistro}
        informacionRegistro={informacionRegistro}
      />
    </Row>
  );
}
