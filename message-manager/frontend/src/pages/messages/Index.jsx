import api from "../../api";
import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { Box, Button, HStack } from "@chakra-ui/react";
import { Table } from "@chakra-ui/react/table";
import { Link as RouterLink } from "react-router-dom";
import { MessageTable } from "./MessageTable";
import { MessageDetails } from "./MessageDetails";

export const Messages = {
    Table: MessageTable,
    Details: MessageDetails,
};
