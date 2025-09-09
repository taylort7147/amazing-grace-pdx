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
    return <PublicRoute>{children}</PublicRoute>;
};

const PublicRoute = ({ children }) => {
    return (
        <>
            <Navbar />
            {children}
        </>
    )
}

const system = createSystem(defaultConfig, {
    theme: {
        tokens: {
            colors: {
                brand: {
                    50: { value: "#e6f2ff" },
                    100: { value: "#e6f2ff" },
                    200: { value: "#bfdeff" },
                    300: { value: "#99caff" },
                    400: { value: "#66b3ff" },
                    500: { value: "#3399ff" },
                    600: { value: "#007fff" },
                    700: { value: "#0066cc" },
                    800: { value: "#005bb5" },
                    900: { value: "#004494" },
                    950: { value: "#001a33" },
                },
                alert: {
                    50: { value: "#fff5f5" },
                    100: { value: "#ffe3e3" },
                    200: { value: "#ffbdbd" },
                    300: { value: "#ff8787" },
                    400: { value: "#ff6f6f" },
                    500: { value: "#ff4c4c" },
                    600: { value: "#ff2b2b" },
                    700: { value: "#ff0000" },
                    800: { value: "#cc0000" },
                    900: { value: "#990000" },
                    950: { value: "#660000" },
                },
            },
        },
        semanticTokens: {
            colors: {
                brand: {
                    solid: { value: "{colors.brand.500}" },
                    contrast: { value: "{colors.brand.100}" },
                    fg: { value: "{colors.brand.700}" },
                    muted: { value: "{colors.brand.100}" },
                    subtle: { value: "{colors.brand.200}" },
                    emphasized: { value: "{colors.brand.300}" },
                    focusRing: { value: "{colors.brand.500}" },
                },
                alert: {
                    solid: { value: "{colors.alert.500}" },
                    contrast: { value: "{colors.alert.100}" },
                    fg: { value: "{colors.alert.700}" },
                    muted: { value: "{colors.alert.100}" },
                    subtle: { value: "{colors.alert.200}" },
                    emphasized: { value: "{colors.alert.300}" },
                    focusRing: { value: "{colors.alert.500}" },
                },
            },
        }
    }, 
    globalCss: {
        html: {
            colorPalette: "brand", 
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
                        <Route
                            path="/login" element={
                                <PublicRoute>
                                    <LoginPage />
                                </PublicRoute>
                            } />
                        <Route
                            path="/register" element={
                                <PublicRoute>
                                    <RegisterPage />
                                </PublicRoute>
                            } />
                        <Route
                            path="/dashboard"
                            element={
                                <PrivateRoute>
                                    <Dashboard />
                                </PrivateRoute>
                            } />
                        <Route
                            path="/users"
                            element={
                                <PrivateRoute roles={["admin"]}>
                                    <UsersPage />
                                </PrivateRoute>}
                        />
                        <Route
                            path="/messages"
                            element={
                                <PrivateRoute roles={["admin"]}>
                                    <Messages.Table />
                                </PrivateRoute>
                            } />
                        <Route
                            path="/messages/details/:id"
                            element={
                                <PrivateRoute roles={["admin"]}>
                                    <Messages.Details />
                                </PrivateRoute>
                            }
                        />
                        <Route
                            path="/messages/edit/:id"
                            element={
                                <PrivateRoute roles={["admin"]}>
                                    <Messages.Edit />
                                </PrivateRoute>
                            }
                        />
                        <Route
                            path="/messages/create"
                            element={
                                <PrivateRoute roles={["admin"]}>
                                    <Messages.Create />
                                </PrivateRoute>
                            }
                        />
                    </Routes>
                </Router>
            </AuthProvider>
        </ChakraAppWrapper>
    );
}
