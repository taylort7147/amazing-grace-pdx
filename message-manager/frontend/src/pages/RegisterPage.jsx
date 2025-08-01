import React, { useState } from "react";
import { useNavigate, Link } from "react-router-dom";
import axios from "axios";

export default function RegisterPage() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState(null);
  const navigate = useNavigate();

  const validateEmail = (email) => /.+@.+\..+/.test(email);
  const validatePassword = (password) => password.length >= 8;

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!validateEmail(email)) return setError("Invalid email format.");
    if (!validatePassword(password)) return setError("Password must be at least 8 characters.");

    try {
      await axios.post("/api/auth/register", { email, password });
      navigate("/login");
    } catch (err) {
      setError("Registration failed: " + (err.response?.data?.error || "Unknown error"));
    }
  };

  return (
    <form onSubmit={handleSubmit} className="p-4">
      {error && <p className="text-red-600 mb-2">{error}</p>}
      <input placeholder="Email" value={email} onChange={(e) => setEmail(e.target.value)} className="block mb-2" />
      <input type="password" placeholder="Password" value={password} onChange={(e) => setPassword(e.target.value)} className="block mb-2" />
      <button type="submit">Register</button>
      <p className="mt-4">
        Already have an account? <Link to="/login">Login</Link>
      </p>
    </form>
  );
}
