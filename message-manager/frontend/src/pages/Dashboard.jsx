import React from "react";
import { useAuth } from "../contexts/AuthContext";

export default function Dashboard() {
  const { user } = useAuth();
  return (
    <div className="p-4">
      <h1>Welcome to the Dashboard</h1>
      <p>Your role: {user?.role}</p>
    </div>
  );
}
