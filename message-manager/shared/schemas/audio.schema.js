import { extendZodWithOpenApi } from "@asteasolutions/zod-to-openapi";
import { z } from "zod";

extendZodWithOpenApi(z);

export const audioSchema = z.object({
    downloadUrl: z.string().url({ message: "Must be a valid URL" }).default(""),
    streamUrl: z.string().url({ message: "Must be a valid URL" }).default("")
});
