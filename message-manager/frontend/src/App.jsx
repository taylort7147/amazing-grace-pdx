import React from "react";
import { BrowserRouter as Router, Routes, Route, Navigate } from "react-router-dom";
import LoginPage from "./pages/LoginPage";
import RegisterPage from "./pages/RegisterPage";
import Dashboard from "./pages/Dashboard";
import { Messages } from "./pages/messages/Index";
import UsersPage from "./pages/UsersPage";
import { AuthProvider, useAuth } from "./contexts/AuthContext";
import Navbar from "./components/Navbar";
import { ChakraProvider, createSystem, defaultConfig } from "@chakra-ui/react";

const PrivateRoute = ({ children, roles }) => {
    const { user, token, loading } = useAuth();
    if (loading) return null;

    if (!user || !token) return <Navigate to="/login" />;

    if (roles && !roles.includes(user.role)) return <Navigate to="/dashboard" />;
    return (
        <>
            <Navbar />
            {children}
        </>
    );
};

const system = createSystem(defaultConfig, {
    theme: {
        tokens: {
            colors: {
                brand: { value: "#4A90E2" },
            },
        },
    },
});

function ChakraAppWrapper({ children }) {
    return <ChakraProvider value={system}>{children}</ChakraProvider>;
}

export default function App() {
    return (
        <ChakraAppWrapper>
            <AuthProvider>
                <Router>
                    <Routes>
                        <Route path="/login" element={<LoginPage />} />
                        <Route path="/register" element={<RegisterPage />} />
                        <Route
                            path="/dashboard"
                            element={
                                <PrivateRoute>
                                    <Dashboard />
                                </PrivateRoute>
                            }
                        />
                        <Route
                            path="/users"
                            element={
                                <PrivateRoute roles={["admin"]}>
                                    <UsersPage />
                                </PrivateRoute>
                            }
                        />
                        <Route
                            path="/messages"
                            element={
                                <PrivateRoute roles={["admin"]}>
                                    <Messages.Table />
                                </PrivateRoute>
                            }
                        />
                        <Route
                            path="/messages/details/:id"
                            element={
                                <PrivateRoute roles={["admin"]}>
                                    <Messages.Details />
                                </PrivateRoute>
                            }
                        />
                    </Routes>
                </Router>
            </AuthProvider>
        </ChakraAppWrapper>
    );
}
