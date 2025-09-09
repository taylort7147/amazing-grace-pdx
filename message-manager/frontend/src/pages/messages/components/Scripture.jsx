import { useEffect, useState } from "react";
import { getBibleReferenceString } from "../../../utils/bibleApiUtils.js"

const Scripture = ({ bibleReference }) => {
  const [formatted, setFormatted] = useState("");
  useEffect(() => {
    if (bibleReference) {
      const referenceRange = bibleReference;
      getBibleReferenceString(referenceRange).then(response => setFormatted(response.data)
      ).catch(err => {
        console.error("Error fetching Bible reference string:", err);
      });
    }
  }, [bibleReference]);

  return <p>{formatted}</p>;
};

export default Scripture;
