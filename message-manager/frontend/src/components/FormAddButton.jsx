import { forwardRef } from "react";
import { Button } from "@chakra-ui/react";

const FormAddButton = forwardRef(({ form, name, schema, ...props }, ref) => {
  const { setValue } = form;
  const newValue = schema.parse({});
  return (
    <Button
      onClick={() => {
        setValue(name, newValue, {
          shouldDirty: true,
          shouldValidate: true,
          shouldTouch: true
        })
      }}
      ref={ref}
      {...props}
    >Add
    </Button>);
});

export default FormAddButton;
