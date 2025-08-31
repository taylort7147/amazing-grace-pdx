import { forwardRef } from "react";
import { Controller } from "react-hook-form";
import CopyableTextBox from "./CopyableTextBox";

const CopyableTextBoxController = forwardRef(({ name, form, ...props }, ref) => {
  const { control } = form;
  return (
    <Controller
      control={control}
      name={name}
      render={({ field: { value, onChange } }) => (
        <CopyableTextBox value={value} onChange={onChange} {...props} ref={ref} />
      )}
    />
  );
});

export default CopyableTextBoxController;
