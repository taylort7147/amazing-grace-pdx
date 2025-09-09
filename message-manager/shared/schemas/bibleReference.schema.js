import { extendZodWithOpenApi } from "@asteasolutions/zod-to-openapi";
import { z } from "zod";

extendZodWithOpenApi(z);

export const bibleReferenceSchema = z.object({
    startBook: z.number(),
    startChapter: z.number(),
    startVerse: z.number(),
    endBook: z.number(),
    endChapter: z.number(),
    endVerse: z.number()
});
