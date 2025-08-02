import { useAuth } from "../contexts/AuthContext";

export default function Dashboard() {
  const { user, logout } = useAuth();
  return (
    <div className="p-4">
      <h1>Welcome to the Dashboard</h1>
      <p>Your role: {user?.role}</p>
      <button onClick={logout} className="mt-4 bg-red-500 text-white px-4 py-2 rounded">Logout</button>
    </div>
  );
}
