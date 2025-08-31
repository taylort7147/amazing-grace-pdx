import api from "../../api";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { Box } from "@chakra-ui/react";
import MessageForm from "./components/MessageForm"

export function Edit() {
  const [message, setMessage] = useState(null);
  const { id } = useParams();

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
      <MessageForm initialData={message} onSubmit={handleSubmit} />
    </Box>
  );
}
