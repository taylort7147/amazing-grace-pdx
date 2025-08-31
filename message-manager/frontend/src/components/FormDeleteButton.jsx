import { forwardRef } from "react";
import { Button } from "@chakra-ui/react";
import DeleteDialog from "./DeleteDialog";

const FormDeleteButton = forwardRef(({ form, name, ...props }, ref) => {
  const { setValue } = form;
  const onDelete = () => {
    setValue(name, null, {
      shouldDirty: true,
      shouldValidate: true,
      shouldTouch: true,
    });
  };
  return (
    <DeleteDialog
      trigger={
        <Button
          variant="outline"
          colorPalette="alert"
          ref={ref}
          {...props}
        >Delete
        </Button>
      }
      onDelete={onDelete}
    />);
});

export default FormDeleteButton;
