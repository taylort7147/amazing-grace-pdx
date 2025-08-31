import { forwardRef } from "react";
import { HStack } from "@chakra-ui/react";
import TextBox from "./TextBox";
import ClipboardButton from "./ClipboardButton";

const CopyableTextBox = forwardRef(({ value, ...props }, ref) => {
  return (
    <HStack align="stretch" w="100%">
      <TextBox value={value} {...props} ref={ref} />
      <ClipboardButton value={value} />
    </HStack>
  );
});

export default CopyableTextBox;
