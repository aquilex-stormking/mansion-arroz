import React, { useEffect, useState } from "react";
import VentasService from "../../api/ventas";
import { useForm } from "../../hooks/useForm.Hook";
import { Button, Modal, Form, Row, Col } from "react-bootstrap";

export default function ModalVentas(props) {
  const [isFormValid, setIsFormValid] = useState(false);
  const [productos, setProductos] = useState([]);
  const [clientes, setClientes] = useState([]);
  const [productosCliente, setProductosCliente] = useState([{ productoId: "", cantidad: "" }]);

  const { form, resetForm, handleChangeForm } = useForm({
    activo: "",
    clienteId: "",
    promocion: "-",
    descuento: "-",
    funcionarioId: "",
  });

  const openedModal = () => {
    setProductos([]);
    setClientes([]);
  };

  useEffect(() => {
    validateForm();
    // eslint-disable-next-line
  }, [form]);

  const validateForm = () => {
    if (form.Descripcion !== "" && form.Activo !== "") {
      setIsFormValid(true);
    } else {
      setIsFormValid(false);
    }
  };

  const sendForm = () => {
    if (props.addRegistro) {
      VentasService.createVenta(form).then((response) => {
        if (response?.result) {
          onCloseModal();
          props.getConsultaData({});
        }
      });
    }
  };

  const addProducto = () => {
    setProductosCliente([...productosCliente, { productoId: "", cantidad: "" }]);
  };

  const deleteProducto = (item) => {
    setProductosCliente(productosCliente.filter((x) => x !== item));
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
        <Modal.Title>{props.addRegistro ? "Agregar venta" : "Editar venta"}</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Form>
          <Row>
            <Col md={6}>
              <Form.Group>
                <label>Cliente</label>
                <select
                  name="clienteId"
                  className="form-control"
                  onChange={handleChangeForm}
                  value={form.clienteId || ""}
                >
                  <option value="">Seleccione</option>
                  {clientes.map((item, index) => {
                    return (
                      <option key={index} value={item.clienteId}>
                        {item.descripcion}
                      </option>
                    );
                  })}
                </select>
              </Form.Group>
            </Col>
            <Col md={6}>
              <Form.Group>
                <label className="gris-oscuro">Estado</label>
                <select className="form-control" value={form.activo || ""} name="activo" onChange={handleChangeForm}>
                  <option value="">Seleccione</option>
                  <option value="true">Activo</option>
                  <option value="false">Inactivo</option>
                </select>
              </Form.Group>
            </Col>
            <Col xs={12}>
              <hr className="mb-2 mt-1" />
            </Col>
            <Col xs={12} className="d-flex justify-content-end pt-2">
              <Button size="sm" variant="outline-danger" onClick={addProducto}>
                <i className="fas fa-plus-circle"></i> Producto
              </Button>
            </Col>
            {productosCliente.map((item, index) => {
              return (
                <Col xs={12} key={index}>
                  <Row>
                    <Col md={6}>
                      <Form.Group>
                        <label>Producto</label>
                        <select
                          name="productoId"
                          className="form-control"
                          onChange={(e) => (item.productoId = e.target.value)}
                          value={item.productoId || ""}
                        >
                          <option value="">Seleccione</option>
                          {productos.map((item, index) => {
                            return (
                              <option key={index} value={item.productoId}>
                                {item.descripcion}
                              </option>
                            );
                          })}
                        </select>
                      </Form.Group>
                    </Col>
                    <Col md={6} className="d-flex align-items-center">
                      <Form.Group>
                        <label>Cantidad</label>
                        <input
                          type="number"
                          name="cantidad"
                          className="form-control"
                          value={item.cantidad || ""}
                          onChange={(e) => (item.cantidad = e.target.value)}
                        />
                      </Form.Group>
                      {productosCliente.length > 1 ? (
                        <Button variant="link" className="mt-3" onClick={() => deleteProducto(item)}>
                          <i className="fas fa-trash-alt text-danger"></i>
                        </Button>
                      ) : null}
                    </Col>
                  </Row>
                </Col>
              );
            })}
          </Row>
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
