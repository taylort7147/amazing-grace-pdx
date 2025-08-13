import { extendZodWithOpenApi } from "@asteasolutions/zod-to-openapi";
import { z } from "zod";

extendZodWithOpenApi(z);

export const audioSchema = z.object({
    streamUrl: z.string().min(1, "Stream URL is required"),
    downloadUrl: z.string().min(1, "Download URL is required")
});
