import { Button } from "@chakra-ui/react";

const FormSaveButton = ({ form, onSubmit }) => {
  const { handleSubmit, formState: { isDirty, isValid } } = form;

  return (<Button size="sm" disabled={!isDirty || !isValid} onClick={handleSubmit(onSubmit)}>Save</Button>);
}

export default FormSaveButton;
