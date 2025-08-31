
import { forwardRef } from "react";
import { Button, Clipboard } from "@chakra-ui/react";

const ClipboardButton = forwardRef(({ value }, ref) => {
  return (
    <Clipboard.Root value={value}>
      <Clipboard.Trigger asChild>
        <Button variant="surface" size="sm" height="100%">
          <Clipboard.Indicator />
        </Button>
      </Clipboard.Trigger>
    </Clipboard.Root>
  );
});

export default ClipboardButton;
