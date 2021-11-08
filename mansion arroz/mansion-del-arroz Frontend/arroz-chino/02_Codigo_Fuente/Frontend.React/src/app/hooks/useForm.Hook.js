import { useState } from "react";

export function useForm(initState) {
  const [form, setForm] = useState(initState);

  const handleChangeForm = ({ target }) => {
    const { name, value } = target;
    setForm({ ...form, [name]: value === "null" ? null : value });
  };

  const resetForm = () => {
    setForm(initState);
  };

  return { form, setForm, handleChangeForm, resetForm };
}
