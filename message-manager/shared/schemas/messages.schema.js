import { extendZodWithOpenApi } from "@asteasolutions/zod-to-openapi";
import { z } from "zod";

extendZodWithOpenApi(z);

export const messageSchema = z.object({
    title: z.string().min(1, "Title is required"),
    description: z.string().optional(),
    date: z.string().datetime()
});
