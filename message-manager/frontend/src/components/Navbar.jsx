import React from "react";
import { Link, useNavigate } from "react-router-dom";
import { useAuth } from "../contexts/AuthContext";

export default function Navbar() {
  const { user, logout } = useAuth();
  const navigate = useNavigate();

  const handleLogout = () => {
    logout();
    navigate("/login");
  };

  return (
    <nav className="bg-gray-800 text-white p-4 flex justify-between items-center">
      <div>
        <Link to="/dashboard" className="mr-4 hover:underline">Dashboard</Link>
        {user?.role === "admin" && (
          <Link to="/users" className="mr-4 hover:underline">Users</Link>
        )}
      </div>
      <div>
        {user ? (
          <div className="relative inline-block text-left">
            <button className="hover:underline">{user.email}</button>
            <button onClick={handleLogout} className="ml-4 bg-red-500 px-2 py-1 rounded">Logout</button>
          </div>
        ) : (
          <Link to="/login" className="hover:underline">Login</Link>
        )}
      </div>
    </nav>
  );
}
