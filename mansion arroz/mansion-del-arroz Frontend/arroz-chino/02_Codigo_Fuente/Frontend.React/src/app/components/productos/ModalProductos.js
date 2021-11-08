import React, { useEffect, useState } from "react";
import ProductosService from "../../api/productos";
import { useForm } from "../../hooks/useForm.Hook";
import { Button, Modal, Form, Col, Row } from "react-bootstrap";

export default function ModalProductos(props) {
  const informacionRegistro = props.informacionRegistro;

  const [marcas, setMarcas] = useState([]);
  const [categorias, setCategorias] = useState([]);
  const [proveedores, setProveedores] = useState([]);
  const [isFormValid, setIsFormValid] = useState(false);

  const { form, setForm, resetForm, handleChangeForm } = useForm({
    valor: "",
    activo: "",
    marcaId: "",
    cantidad: "",
    proveedorId: "",
    categoriaId: "",
    descripcion: "",
    observaciones: "",
    valorimpuesto: "",
  });

  const openedModal = () => {
    consultarListas();
    if (!props.addRegistro) {
      setForm({ ...informacionRegistro });
    }
  };

  const consultarListas = () => {
    ProductosService.getMarcas().then((response) => {
      if (response.result) {
        setMarcas(response.data);
      }
    });
    ProductosService.getCategorias().then((response) => {
      if (response.result) {
        setCategorias(response.data);
      }
    });
    ProductosService.getProveedores().then((response) => {
      if (response.result) {
        setProveedores(response.data);
      }
    });
  };

  useEffect(() => {
    validateForm();
    // eslint-disable-next-line
  }, [form]);

  const validateForm = () => {
    console.log(form.cantidad);
    if (
      form.valor !== "" &&
      form.activo !== "" &&
      form.marcaId !== "" &&
      form.cantidad !== "" &&
      form.proveedorId !== "" &&
      form.categoriaId !== "" &&
      form.descripcion !== "" &&
      form.observaciones !== "" &&
      form.valorimpuesto !== ""
    ) {
      setIsFormValid(true);
    } else {
      setIsFormValid(false);
    }
  };

  const sendForm = () => {
    if (props.addRegistro) {
      ProductosService.createProducto(form).then((response) => {
        if (response?.result) {
          onCloseModal();
          props.getConsultaData({});
        }
      });
    } else {
      ProductosService.updateProducto({ ...form, productoId: informacionRegistro.productoId }).then((response) => {
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
      size="lg"
      centered={true}
      backdrop="static"
      onShow={openedModal}
      onHide={onCloseModal}
      onExiting={resetModal}
    >
      <Modal.Header closeButton>
        <Modal.Title>{props.addRegistro ? "Agregar producto" : "Editar producto"}</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Form>
          <Row>
            <Col md={4}>
              <Form.Group>
                <label>Descripción</label>
                <input
                  type="text"
                  name="descripcion"
                  className="form-control"
                  onChange={handleChangeForm}
                  value={form.descripcion || ""}
                />
              </Form.Group>
            </Col>
            <Col md={4}>
              <Form.Group>
                <label>Valor</label>
                <input
                  type="text"
                  name="valor"
                  className="form-control"
                  value={form.valor || ""}
                  onChange={handleChangeForm}
                />
              </Form.Group>
            </Col>
            <Col md={4}>
              <Form.Group>
                <label>Valor Impuesto</label>
                <input
                  type="text"
                  name="valorimpuesto"
                  className="form-control"
                  onChange={handleChangeForm}
                  value={form.valorimpuesto || ""}
                />
              </Form.Group>
            </Col>
            <Col md={4}>
              <Form.Group>
                <label>Cantidad</label>
                <input
                  type="number"
                  name="cantidad"
                  className="form-control"
                  value={form.cantidad || ""}
                  onChange={handleChangeForm}
                />
              </Form.Group>
            </Col>
            <Col md={8}>
              <Form.Group>
                <label>Marca</label>
                <select name="marcaId" className="form-control" onChange={handleChangeForm} value={form.marcaId || ""}>
                  <option value="">Seleccione</option>
                  {marcas.map((item, index) => {
                    return (
                      <option key={index} value={item.marcaId}>
                        {item.descripcion}
                      </option>
                    );
                  })}
                </select>
              </Form.Group>
            </Col>
            <Col md={4}>
              <Form.Group>
                <label>Categoría</label>
                <select
                  name="categoriaId"
                  className="form-control"
                  onChange={handleChangeForm}
                  value={form.categoriaId || ""}
                >
                  <option value="">Seleccione</option>
                  {categorias.map((item, index) => {
                    return (
                      <option key={index} value={item.categoriaId}>
                        {item.descripcion}
                      </option>
                    );
                  })}
                </select>
              </Form.Group>
            </Col>
            <Col md={4}>
              <Form.Group>
                <label>Proveedor</label>
                <select
                  name="proveedorId"
                  className="form-control"
                  onChange={handleChangeForm}
                  value={form.proveedorId || ""}
                >
                  <option value="">Seleccione</option>
                  {proveedores.map((item, index) => {
                    return (
                      <option key={index} value={item.proveedorId}>
                        {item.descripcion}
                      </option>
                    );
                  })}
                </select>
              </Form.Group>
            </Col>
            <Col md={4}>
              <Form.Group>
                <label className="gris-oscuro">Estado</label>
                <select className="form-control" value={form.activo || ""} name="activo" onChange={handleChangeForm}>
                  <option value="">Seleccione</option>
                  <option value="true">Activo</option>
                  <option value="false">Inactivo</option>
                </select>
              </Form.Group>
            </Col>
            <Col md={12}>
              <Form.Group>
                <label>Observaciones</label>
                <textarea
                  name="observaciones"
                  className="form-control"
                  onChange={handleChangeForm}
                  value={form.observaciones || ""}
                />
              </Form.Group>
            </Col>
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
