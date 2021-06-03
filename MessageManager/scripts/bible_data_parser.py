import os
from os.path import dirname, join, isdir
import json
"""
This parser is written agains the Bible json found here:
https://github.com/phillipsk/Open_Verse/blob/master/Bible_KJV.json

However, there are a couple of JSON formatting errors that prevent this from
being able to use that file directly. The file must be cleaned before using
this script to parse it.


Expected input format:
{
    "<book_name>" : {
        "<chapter>": {
            "<verse_number>": "<verse_text>",
            "<verse_number>": "<verse_text>",
            ...
        },
        "<chapter>": {
            "<verse_number>": "<verse_text>",
            "<verse_number>": "<verse_text>",
            ...
        },
        ...
    },
    "<book_name>" : {
        "<chapter>": {
            "<verse_number>": "<verse_text>",
            "<verse_number>": "<verse_text>",
            ...
        },
        "<chapter>": {
            "<verse_number>": "<verse_text>",
            "<verse_number>": "<verse_text>",
            ...
        },
        ...
    },
    ...    
}


Output format:
[
    {
        "book": "<book_name>",
        "verse_count_by_chapter":{
            "<chapter>": <number_of_verses>,
            "<chapter>": <number_of_verses>,
            ...
        }
    },
    {
        "book": "<book_name>",
        "verse_count_by_chapter":{
            "<chapter>": <number_of_verses>,
            "<chapter>": <number_of_verses>,
            ...
        }
    },
    ...
]
"""


working_dir = dirname(__file__)
bible_json_path = join(working_dir, "data", "bible.json")
bible_stats_json_path = join(
    working_dir, "..", "BibleReferenceParser", "Embedded", "bible_details.json")
bible_books_regex_path = join(working_dir, "data", "bible_books_regex.txt")

# Create output paths
if not isdir(dirname(bible_stats_json_path)):
    os.makedirs(dirname(bible_stats_json_path))
if not isdir(dirname(bible_books_regex_path)):
    os.makedirs(dirname(bible_books_regex_path))

with open(bible_json_path, "r") as fh:
    bible = json.load(fh)

bible_stats = []
for book, book_content in bible.items():
    verses_by_chapter = {}
    for chapter, chapter_content in book_content.items():
        verses_by_chapter[chapter] = len(chapter_content)
    bible_stats.append(
        {"book": book, "verse_count_by_chapter": verses_by_chapter})

with open(bible_stats_json_path, "w") as fh:
    json.dump(bible_stats, fh, separators=(',', ':'), indent=4)

book_regex = "(" + "|".join(f"'{book}'" for book in bible.keys()) + ")"
with open(bible_books_regex_path, "w") as fh:
    print("antlr:", file=fh)
    print(book_regex, file=fh)
