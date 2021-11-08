import { useState } from "react";

export function useModal(initState) {
  const [showModal, setShowModal] = useState(false);
  const [addRegistro, setAddRegistro] = useState(true);
  const [informacionRegistro, setInformacionRegistro] = useState(initState);

  return {
    showModal,
    addRegistro,
    setShowModal,
    setAddRegistro,
    informacionRegistro,
    setInformacionRegistro,
  };
}
