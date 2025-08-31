import api from "../../api";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { Box, Button, Flex } from "@chakra-ui/react";
import MessageForm from "./components/MessageForm"
import DeleteDialog from "../../components/DeleteDialog";

export function Edit() {
  const [message, setMessage] = useState(null);
  const { id } = useParams();
  const navigate = useNavigate();

  const handleSubmit = async (data) => {
    await api.put(`/messages/${id}`, data).then((res) => {
    }).catch((err) => {
      console.error("Error updating message:", err);
    });
  };

  useEffect(() => {
    api.get(`/messages/${id}`).then((res) => {
      setMessage(res.data);
    });
  }, [id]);

  if (!message) {
    return <div>Loading...</div>; // don’t render MessageForm yet
  }

  return (
    <Box p={6} maxW="800px" mx="auto">
      <Box mb={3}>
        <MessageForm initialData={message} onSubmit={handleSubmit} />
      </Box>

      <Box mb={3}>
        <Flex width="100%" justify={"center"}>
          <DeleteDialog
            onDelete={() => api.delete(`/messages/${id}`).then((data) => navigate("/messages"))}
            trigger={
              <Button
                variant="outline"
                colorPalette="alert"
              >Delete Message</Button>} />
        </Flex>
      </Box>
    </Box>
  );
}
