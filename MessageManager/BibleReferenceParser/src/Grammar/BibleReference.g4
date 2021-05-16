grammar BibleReference;

// Parser rules
reference: book_reference;

// Books
book_reference:
    (book_range | book_with_chapter | book_without_chapter)
    (
        separator_op (book_range | book_with_chapter | book_without_chapter)
    )*;
book_range:
    /* Range */ (book_with_chapter | book_without_chapter) range_op
    (
        book_with_chapter
        | book_without_chapter
    );
book_with_chapter:    book chapter_reference;
book_without_chapter: book;
book:                 BOOK;

// Chapters
chapter_reference:
    (chapter | chapter_to_chapter)
    (
        separator_op (chapter_to_chapter | chapter_without_verse)
    )* (separator_op chapter_to_chapter_verse)?
    (
        separator_op (chapter_verse_to_chapter_verse | chapter_with_verse)
    )*
    |
    (
        chapter_verse_to_chapter_verse
        | chapter_to_chapter_verse
        | chapter_with_verse
    ) (separator_op (chapter_verse_to_chapter_verse | chapter_with_verse))*;

chapter_verse_to_chapter_verse:
    chapter_with_verse range_op chapter_with_verse;
chapter_to_chapter_verse: chapter range_op chapter_with_verse;
chapter_to_chapter:       chapter_without_verse range_op chapter_without_verse;

chapter_with_verse:    chapter index_op verse_reference;
chapter_without_verse: chapter;
chapter:               NUMBER;

// Verses
verse_reference: (verse_to_verse | verse)
    (
        separator_op (verse_to_verse | verse)
    )*;
verse_to_verse: verse range_op verse;
verse:          NUMBER;

// Operators
range_op:     RANGE_OP;
separator_op: SEP_OP;
index_op:     CHAPTER_INDEX_OP;

// Lexer rules
BOOK:
    'Genesis'
    | 'Exodus'
    | 'Leviticus'
    | 'Numbers'
    | 'Deuteronomy'
    | 'Joshua'
    | 'Judges'
    | 'Ruth'
    | '1 Samuel'
    | '2 Samuel'
    | '1 Kings'
    | '2 Kings'
    | '1 Chronicles'
    | '2 Chronicles'
    | 'Ezra'
    | 'Nehemiah'
    | 'Esther'
    | 'Job'
    | 'Psalms'
    | 'Proverbs'
    | 'Ecclesiastes'
    | 'Song of ' ('Songs' | 'Solomon')
    | 'Isaiah'
    | 'Jeremiah'
    | 'Lamentations'
    | 'Ezekiel'
    | 'Daniel'
    | 'Hosea'
    | 'Joel'
    | 'Amos'
    | 'Obadiah'
    | 'Jonah'
    | 'Micah'
    | 'Nahum'
    | 'Habakkuk'
    | 'Zephaniah'
    | 'Haggai'
    | 'Zechariah'
    | 'Malachi'
    | 'Matthew'
    | 'Mark'
    | 'Luke'
    | 'John'
    | 'Acts'
    | 'Romans'
    | '1 Corinthians'
    | '2 Corinthians'
    | 'Galatians'
    | 'Ephesians'
    | 'Philippians'
    | 'Colossians'
    | '1 Thessalonians'
    | '2 Thessalonians'
    | '1 Timothy'
    | '2 Timothy'
    | 'Titus'
    | 'Philemon'
    | 'Hebrews'
    | 'James'
    | '1 Peter'
    | '2 Peter'
    | '1 John'
    | '2 John'
    | '3 John'
    | 'Jude'
    | 'Revelation';

fragment DIGIT: [0-9];
NUMBER:         DIGIT+;
RANGE_OP:       '-';
SEP_OP:         ',';

CHAPTER_INDEX_OP: ':';

WS: [ \t\r\n] -> skip;
