import React, { useEffect, useState } from "react";
import api from "../api";

export default function UsersPage() {
  const [users, setUsers] = useState([]);

  useEffect(() => {
    api.get("/users").then((res) => setUsers(res.data));
  }, []);

  const promote = async (id) => {
    await api.post(`/users/${id}/promote`);
    setUsers(users.map((u) => (u.id === id ? { ...u, role: "admin" } : u)));
  };

  return (
    <div className="p-4">
      <h1 className="text-xl mb-4">User Management</h1>
      <ul>
        {users.map((user) => (
          <li key={user.id} className="mb-2">
            {user.email} — {user.role}
            {user.role !== "admin" && (
              <button onClick={() => promote(user.id)} className="ml-4">Promote to Admin</button>
            )}
          </li>
        ))}
      </ul>
    </div>
  );
}
