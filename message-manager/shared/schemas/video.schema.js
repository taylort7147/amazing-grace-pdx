import { extendZodWithOpenApi } from "@asteasolutions/zod-to-openapi";
import { z } from "zod";

extendZodWithOpenApi(z);

export const videoSchema = z.object({
    youTubeVideoId: z.string().min(1, "YouTube Video ID is required").default(""),
    messageStartTimeSeconds: z.int().min(0, "Message start time cannot be negative").default(0)
});
