import bibleApi from "../bibleApi.js";


export async function parseBibleReference(bibleReferenceString) {
  return await bibleApi.post("/parse", { value: bibleReferenceString })
}

export async function getBibleReferenceString(bibleReferenceRange) {
  return await bibleApi.post("/tostring", bibleReferenceRange);
}
