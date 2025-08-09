import React, { useState } from "react";
import { useNavigate, Link } from "react-router-dom";
import api from "../api";
import { validateRegisterForm } from "../utils/validation";

export default function RegisterPage() {
  const [formData, setFormData] = useState({ email: "", password: "" });
  const [errors, setErrors] = useState({});
  const navigate = useNavigate();
  
  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
    setErrors({ ...errors, [e.target.name]: "" }); // Clear field error when typing
  };
  
  const handleSubmit = async (e) => {
    e.preventDefault();
    const result = validateRegisterForm(formData);

    if (!result.valid) {
      setErrors(result.errors);
      return;
    }
    try {
      await api.post("/auth/register", formData);
      navigate("/login");
    } catch (err) {
      setErrors({ ...errors, form: "Registration failed: " + (err.response?.data?.error || "Unknown error") });
    }
  };

  return (
    <form onSubmit={handleSubmit} className="p-4">
      {errors.form && <p className="text-red-600 mb-2">{errors.form}</p>}
      {errors.email && <p className="text-red-600 mb-2">{errors.email}</p>}
      {errors.password && <p className="text-red-600 mb-2">{errors.password}</p>}
      <input placeholder="Email" name="email" value={formData.email} onChange={handleChange} className="block mb-2" />
      <input type="password" placeholder="Password" name="password" value={formData.password} onChange={handleChange} className="block mb-2" />
      <button type="submit">Register</button>
      <p className="mt-4">
        Already have an account? <Link to="/login">Login</Link>
      </p>
    </form>
  );
}
