import React, { useEffect, useState } from "react";
import CategoriasService from "../../api/categorias";
import { Button, Col, Row } from "react-bootstrap";
import { useModal } from "../../hooks/useModal.Hook";
import { useTable } from "../../hooks/useTable.Hook";
import Tablas from "../../shared/layouts/tablas/Tablas";
import ModalCategorias from "../../components/categorias/ModalCategorias";

export default function Categorias() {
  const [headers] = useState(["Descripcion", "Estado", "Acciones"]);
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
    getRoles({});
    // eslint-disable-next-line
  }, []);

  const getRoles = (data) => {
    CategoriasService.getCategorias(data).then((response) => {
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
    CategoriasService.deleteCategorias({ categoriaId: item.categoriaId }).then(
      (response) => {
        if (response?.result) {
          getRoles({});
        }
      }
    );
  };

  return (
    <Row>
      <Col xs={12} className="pb-3 d-flex justify-content-between">
        <label className="h4 font-weight-light">Categorías</label>
        <Button
          variant="outline-danger"
          onClick={() => setShowModal(!showModal)}
        >
          Agregar categoría
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
                <td>{item.descripcion}</td>
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
      <ModalCategorias
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
