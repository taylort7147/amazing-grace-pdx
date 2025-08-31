import { forwardRef } from "react";
import { Textarea } from "@chakra-ui/react";

const TextBox = forwardRef(({ value, ...props }, ref) => {
  return (
    <Textarea value={value}
      autoresize
      {...props}
      ref={ref} />
  );
});

export default TextBox;
