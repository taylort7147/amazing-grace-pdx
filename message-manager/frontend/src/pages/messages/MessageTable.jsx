import api from "../../api";
import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { Box, Button, HStack } from "@chakra-ui/react";
import { Table } from "@chakra-ui/react/table";
import { Link as RouterLink } from "react-router-dom";

export function MessageTable() {

  const [messages, setMessages] = useState([]);
  const navigate = useNavigate();
  useEffect(() => {
    api.get("/messages").then((res) => setMessages(res.data));
  }, []);

  return (
    <Box p={6}>
      <Table.Root size="md" variant="line" borderWidth="1px" borderRadius="lg" boxShadow="sm">
        <Table.Header bg="gray.50">
          <Table.Row>
            <Table.ColumnHeader>Title</Table.ColumnHeader>
            <Table.ColumnHeader>Media</Table.ColumnHeader>
            <Table.ColumnHeader textAlign="center">Actions</Table.ColumnHeader>
          </Table.Row>
        </Table.Header>

        <Table.Body>
          {messages.map((message) => (
            <Table.Row
              key={message.id}
              _hover={{ bg: "gray.50", cursor: "pointer" }}
            >
              <Table.Cell fontWeight="medium"
                onClick={() => navigate(`./details/${message.id}`)}
              >{message.title}</Table.Cell>
              <Table.Cell>{message.media}</Table.Cell>
              <Table.Cell>
                <HStack spacing={3} justify="center">
                  <Button
                    as={RouterLink}
                    to={`./edit/${message.id}`}
                    size="sm"
                    colorScheme="teal"
                  >
                    Edit
                  </Button>
                </HStack>
              </Table.Cell>
            </Table.Row>
          ))}
        </Table.Body>
      </Table.Root>
    </Box>
  );
}
