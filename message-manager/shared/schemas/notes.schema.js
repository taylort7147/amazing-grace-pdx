import { extendZodWithOpenApi } from "@asteasolutions/zod-to-openapi";
import { z } from "zod";

extendZodWithOpenApi(z);

export const notesSchema = z.object({
    url: z.string().url({ message: "Must be a valid URL" }).default("")
});
