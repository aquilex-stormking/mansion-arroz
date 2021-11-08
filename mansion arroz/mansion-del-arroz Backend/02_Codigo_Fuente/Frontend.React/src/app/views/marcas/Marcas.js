import React, { useEffect, useState } from "react";
import MarcasService from "../../api/marcas";
import { Button, Col, Row } from "react-bootstrap";
import { useModal } from "../../hooks/useModal.Hook";
import { useTable } from "../../hooks/useTable.Hook";
import Tablas from "../../shared/layouts/tablas/Tablas";
import ModalMarcas from "../../components/marcas/ModalMarcas";

export default function Marcas() {
  const [headers] = useState(["Descripción", "Estado", "Acciones"]);
  const { data, setData, itemsPerPage, paginateData, hanbleChangePage } = useTable([]);
  const { showModal, addRegistro, setShowModal, setAddRegistro, informacionRegistro, setInformacionRegistro } =
    useModal({});

  useEffect(() => {
    getMarcas({});
    // eslint-disable-next-line
  }, []);

  const getMarcas = (data) => {
    MarcasService.getMarcas(data).then((response) => {
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
    MarcasService.deleteMarca({ marcaId: item.marcaId }).then((response) => {
      if (response?.result) {
        getMarcas({});
      }
    });
  };

  return (
    <Row>
      <Col xs={12} className="pb-3 d-flex justify-content-between">
        <label className="h4 font-weight-light">Marcas</label>
        <Button variant="outline-danger" onClick={() => setShowModal(!showModal)}>
          Agregar marca
        </Button>
      </Col>
      <Tablas headers={headers} sizeData={data.length} itemsPerPage={itemsPerPage} hanbleChangePage={hanbleChangePage}>
        <tbody>
          {paginateData().map((item, index) => {
            return (
              <tr key={index}>
                <td>{item.descripcion}</td>
                <td>{item.activo ? "Activo" : "Inactivo"}</td>
                <td className="p-0">
                  <Button
                    variant="link"
                    className="alert-info size-buttons-acciones rounded-circle my-2 mr-2"
                    onClick={() => loadInformation(item)}
                  >
                    <i className="fas fa-pencil-alt size-icon-edit-table text-primary"></i>
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
      <ModalMarcas
        isShowModal={showModal}
        showModal={setShowModal}
        addRegistro={addRegistro}
        getConsultaData={getMarcas}
        setAddRegistro={setAddRegistro}
        informacionRegistro={informacionRegistro}
      />
    </Row>
  );
}
