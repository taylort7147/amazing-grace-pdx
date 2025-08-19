import { NumberInput, Input } from "@chakra-ui/react";
import { useState } from "react";

// Format seconds into mm:ss
function formatSeconds(seconds) {
  if (isNaN(seconds) || seconds < 0) return "";
  const m = Math.floor(seconds / 60);
  const s = seconds % 60;
  return `${m}:${s.toString().padStart(2, "0")}`;
}

// Parse mm:ss into seconds
function parseFormatted(value) {
  if (!value) return 0;
  const parts = value.split(":").map((p) => parseInt(p, 10));
  if (parts.length === 1) return parts[0]; // Already in seconds
  if (parts.length === 2) return parts[0] * 60 + parts[1]; // "mm:ss"
  return 0;
}

export default function DurationInput({ value, onValueChange }) {
  const [display, setDisplay] = useState(formatSeconds(value || 0));

  const handleNumberInputChange = (val) => {
    const seconds = val.valueAsNumber;
    console.log("seconds", seconds);
    setDisplay(formatSeconds(seconds ?? 0));
    if (onValueChange) onValueChange(seconds); // Propagate internal seconds up
  };

  const handleInputChange = (e) => {
    const value = e.target.value;
    const seconds = parseFormatted(value);
    setDisplay(value);
    if (isNaN(seconds)) return; // Ignore invalid input
    if (onValueChange) onValueChange(seconds);
  };

  const handleBlur = () => {
    console.log("handleBlur")
    // reformat display after editing
    const seconds = parseFormatted(display);
    setDisplay(formatSeconds(seconds));
  };

  return (
    <NumberInput.Root
      value={value}
      onValueChange={handleNumberInputChange}
      onBlur={handleBlur}
      clampValueOnBlur={false}
      min={0}
    >
      <NumberInput.Control />
      {/* This input is hidden and used for controlling the numeric value */}
      <NumberInput.Input  hidden/>
      {/* This input is for displaying and inputting the formatted value */}
      <Input value={display} onChange={handleInputChange}  />
    </NumberInput.Root>
  );
}
