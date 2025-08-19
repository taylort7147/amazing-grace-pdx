import { useState } from "react";
import { Button, Input, HStack } from "@chakra-ui/react";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";

export default function DateField({ value, onChange }) {

    return (
        <HStack>
            <DatePicker
                selected={value}
                onChange={onChange}
                dateFormat="MM/dd/yyyy"
                customInput={<Input />}
            />
            <Button onClick={() => onChange(new Date())} variant="outline">
                Today
            </Button>
        </HStack>
    );
}
