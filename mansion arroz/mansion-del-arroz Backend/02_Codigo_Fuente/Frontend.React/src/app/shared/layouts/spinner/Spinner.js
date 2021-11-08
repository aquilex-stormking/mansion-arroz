import React from "react";
import "./Spinner.css";
import PulseLoader from "react-spinners/PulseLoader";

export default function Spinner(props) {
  return props.isOpen ? (
    <div className="d-flex justify-content-center align-items-center modal-backdrop show zindex-loading" tabIndex={-1}>
      <PulseLoader color="#f8f9fa" loading={props.isOpen} size={24} />
    </div>
  ) : null;
}
