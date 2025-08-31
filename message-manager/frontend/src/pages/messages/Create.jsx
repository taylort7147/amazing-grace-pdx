import api from "../../api";
import { useNavigate } from "react-router-dom";
import { Box } from "@chakra-ui/react";
import MessageForm from "./components/MessageForm"
import { messageSchema } from "@message-manager/shared";

export function Create() {
  const navigate = useNavigate();
  const handleSubmit = async (data) => {
    await api.post(`/messages`, data).then((res) => {
      const id = res.data.id;
      navigate(`/messages/edit/${id}`);
    }).catch((err) => {
      console.error("Error creating message:", err);
    });
  };

  const skeletonMessage = messageSchema.parse({});
  
  return (
    <Box p={6} maxW="800px" mx="auto">
      <MessageForm initialData={skeletonMessage} onSubmit={handleSubmit} />
    </Box>
  );
}
