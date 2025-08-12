import { extendZodWithOpenApi } from "@asteasolutions/zod-to-openapi";
import { z } from "zod";

extendZodWithOpenApi(z);

export const registerSchema = z.object({
    email: z.string().email(),
    password: z.string().min(8),
});

export const loginSchema = z.object({
    email: z.string().email(),
    password: z.string().min(8),
});
