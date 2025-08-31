import { extendZodWithOpenApi } from "@asteasolutions/zod-to-openapi";
import { z } from "zod";

extendZodWithOpenApi(z);

export const seriesSchema = z.object({
    name: z.string().min(1, "Name is required"),
    description: z.string().nullable().optional(),
    id: z.int().optional(),
    playlistId: z.string().nullable().optional()
});
