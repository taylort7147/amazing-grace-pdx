import { cloneElement, useState } from "react";
import { Button, CloseButton, Dialog } from "@chakra-ui/react";

const DeleteDialog = ({ trigger, onDelete }) => {
    return (
        <>
            <Dialog.Root role="alertdialog" placement="center">
                <Dialog.Trigger asChild>{trigger}</Dialog.Trigger>
                <Dialog.Backdrop />
                <Dialog.Positioner>
                    <Dialog.Content>
                        <Dialog.CloseTrigger />
                        <Dialog.Header>
                            <Dialog.Title>Confirm Delete</Dialog.Title>
                        </Dialog.Header>
                        <Dialog.Body>Are you sure you want to delete this item?</Dialog.Body>
                        <Dialog.Footer>
                            <Dialog.ActionTrigger asChild>
                                <Button variant="outline">Cancel</Button>
                            </Dialog.ActionTrigger>
                            <Button
                                colorPalette="alert"
                                onClick={() => {
                                    onDelete();
                                    setIsOpen(false);
                                }}
                            >Delete</Button>
                        </Dialog.Footer>
                    </Dialog.Content>
                </Dialog.Positioner>
            </Dialog.Root>
        </>
    );
};
export default DeleteDialog;
