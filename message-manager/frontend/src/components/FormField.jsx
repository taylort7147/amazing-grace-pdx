import { forwardRef } from "react";
import { Field } from "@chakra-ui/react";

const FormField = forwardRef(({ form, label, name, helpText, required, children }, ref) => {
  const { formState: { errors } } = form;
  const fieldError = name.split('.').reduce((acc, key) => acc?.[key], errors);
  return (
    <Field.Root required={required} invalid={!!fieldError} ref={ref}>
      <Field.Label>{label}<Field.RequiredIndicator /></Field.Label>
      {children}
      <Field.ErrorText>{fieldError?.message}</Field.ErrorText>
      {helpText && <Field.HelpText>{helpText}</Field.HelpText>}
    </Field.Root>
  );
});

export default FormField;
