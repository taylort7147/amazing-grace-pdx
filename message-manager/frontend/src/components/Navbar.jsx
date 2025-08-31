import { Box, Flex, HStack, Link, Button, Spacer, Show } from "@chakra-ui/react"
import { NavLink } from "react-router-dom"
import { useAuth } from "../contexts/AuthContext"

export default function Navbar() {
  const { user, logout } = useAuth();

  return (
    <Box bg="brand.800" px={4} py={2} boxShadow="md">
      <Flex alignItems="center">
        {/* Left side - Logo / Brand */}
        <Box fontWeight="bold" fontSize="lg" color="white">
          MessageManager
        </Box>

        <Spacer />

        {/* Middle - Nav links (hidden on mobile) */}
        <HStack
          as="nav"
          spacing={6}
          display={{ base: "none", md: "flex" }}
        >
          <Link as={NavLink} to="/" color="white" _hover={{ color: "gray.300" }}>
            Home
          </Link>
          <Link as={NavLink} to="/messages" color="white" _hover={{ color: "gray.300" }}>
            Messages
          </Link>
          <Link as={NavLink} to="/series" color="white" _hover={{ color: "gray.300" }}>
            Series
          </Link>
        </HStack>

        <Spacer />

        {/* Right side - Actions */}
        <HStack spacing={4}>
          <Show when={!user}>
            <Button colorScheme="teal" size="sm">
              Login
            </Button>
          </Show>
          <Show when={user}>
            <Button colorScheme="teal" size="sm" onClick={logout}>
              Logout
            </Button>
          </Show>
        </HStack>
      </Flex>
    </Box>
  )
}
