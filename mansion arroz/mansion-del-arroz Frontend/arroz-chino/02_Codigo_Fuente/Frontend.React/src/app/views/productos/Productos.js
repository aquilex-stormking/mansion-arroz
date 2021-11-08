import React, { useEffect, useState } from "react";
import ProductosService from "../../api/productos";
import { Button, Col, Row } from "react-bootstrap";
import { useModal } from "../../hooks/useModal.Hook";
import { useTable } from "../../hooks/useTable.Hook";
import Tablas from "../../shared/layouts/tablas/Tablas";
import ModalProductos from "../../components/productos/ModalProductos";

export default function Productos() {
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
  const [headers] = useState([
    "Descripcion",
    "Cantidad",
    "Valor",
    "Valor Impuesto",
    "Observacion",
    "Marca",
    "Proveedor",
    "Categoria",
    "Estado",
    "Acciones",
  ]);

  useEffect(() => {
    getProductos({});
    // eslint-disable-next-line
  }, []);

  const getProductos = (data) => {
    ProductosService.getProductos(data).then((response) => {
      if (response?.result) {
        setData(response.data == null ? [] : response.data);
      }
    });
  };

  const loadInformation = (item) => {
    setInformacionRegistro(item);
    setAddRegistro(false);
    setShowModal(true);
  };

  const deleteItem = (item) => {
    ProductosService.deleteProducto({ userId: item.userId }).then(
      (response) => {
        if (response?.result) {
          getProductos({});
        }
      }
    );
  };

  return (
    <Row>
      <Col xs={12} className="pb-3 d-flex justify-content-between">
        <label className="h4 font-weight-light">Productos</label>
        <Button
          variant="outline-danger"
          onClick={() => setShowModal(!showModal)}
        >
          Agregar producto
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
                <td>{item.cantidad}</td>
                <td>{item.valor}</td>
                <td>{item.valorimpuesto}</td>
                <td>{item.observaciones}</td>
                <td>{item.marcaId}</td>
                <td>{item.proveedorId}</td>
                <td>{item.categoriaId}</td>
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
      <ModalProductos
        isShowModal={showModal}
        showModal={setShowModal}
        addRegistro={addRegistro}
        getConsultaData={getProductos}
        setAddRegistro={setAddRegistro}
        informacionRegistro={informacionRegistro}
      />
    </Row>
  );
}
