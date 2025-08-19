import { extendZodWithOpenApi } from "@asteasolutions/zod-to-openapi";
import { z } from "zod";
import { messageSchema } from "./messages.schema.js";

extendZodWithOpenApi(z);

export const seriesSchema = z.object({
    name: z.string().min(1, "Name is required"),
    description: z.string().optional(),
    messages: z.array(messageSchema).optional()
});
