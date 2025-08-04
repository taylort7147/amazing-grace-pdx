import React, { useEffect, useState } from "react";
import api from "../api";

export default function MessagesApi() {
  const [messages, setMessages] = useState([]);

  useEffect(() => {
    api.get("/messages").then((res) => setMessages(res.data));
  }, []);

  return (
    <div className="p-4">
      <h1 className="text-xl mb-4">Messages</h1>
      <ul>
        {messages.map((message) => (
          <li key={message.id} className="mb-2">
            <h2>
              {console.log(message)}
              {message.title}
            </h2>
            <p>{message.description}</p>
          </li>
        ))}
      </ul>
    </div>
  );
}
