import { extendZodWithOpenApi } from "@asteasolutions/zod-to-openapi";
import { z } from "zod";
import { notesSchema } from "./notes.schema.js";
import { audioSchema } from "./audio.schema.js";
import { videoSchema } from "./video.schema.js";

extendZodWithOpenApi(z);

export const messageSchema = z.object({
    title: z.string().min(1, "Title is required").default(""),
    description: z.string().optional().default(""),
    date: z.string().datetime().default(new Date().toISOString()),
    seriesId: z.number().min(1, "Series is required").default(0),
    notes: notesSchema.nullable().default(null),
    audio: audioSchema.nullable().default(null),
    video: videoSchema.nullable().default(null)
});
