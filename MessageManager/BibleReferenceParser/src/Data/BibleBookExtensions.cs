using System;

namespace BibleReferenceParser.Data
{
    public static class BibleBookExtensions
    {
        public static BibleBook ToBibleBook(string book)
        {
            var bookLower = book.ToLower();
            switch (bookLower)
            {
                case "genesis": return BibleBook.Genesis;
                case "exodus": return BibleBook.Exodus;
                case "leviticus": return BibleBook.Leviticus;
                case "numbers": return BibleBook.Numbers;
                case "deuteronomy": return BibleBook.Deuteronomy;
                case "joshua": return BibleBook.Joshua;
                case "judges": return BibleBook.Judges;
                case "ruth": return BibleBook.Ruth;
                case "1 samuel": return BibleBook.Samuel_1;
                case "2 samuel": return BibleBook.Samuel_2;
                case "1 kings": return BibleBook.Kings_1;
                case "2 kings": return BibleBook.Kings_2;
                case "1 chronicles": return BibleBook.Chronicles_1;
                case "2 chronicles": return BibleBook.Chronicles_2;
                case "ezra": return BibleBook.Ezra;
                case "nehemiah": return BibleBook.Nehemiah;
                case "esther": return BibleBook.Esther;
                case "job": return BibleBook.Job;
                case "psalm":
                case "psalms": return BibleBook.Psalms;
                case "proverbs": return BibleBook.Proverbs;
                case "ecclesiastes": return BibleBook.Ecclesiastes;
                case "song of solomon":
                case "song of songs": return BibleBook.Song_Of_Songs;
                case "isaiah": return BibleBook.Isaiah;
                case "jeremiah": return BibleBook.Jeremiah;
                case "lamentations": return BibleBook.Lamentations;
                case "ezekiel": return BibleBook.Ezekiel;
                case "daniel": return BibleBook.Daniel;
                case "hosea": return BibleBook.Hosea;
                case "joel": return BibleBook.Joel;
                case "amos": return BibleBook.Amos;
                case "obadiah": return BibleBook.Obadiah;
                case "jonah": return BibleBook.Jonah;
                case "micah": return BibleBook.Micah;
                case "nahum": return BibleBook.Nahum;
                case "habakkuk": return BibleBook.Habakkuk;
                case "zephaniah": return BibleBook.Zephaniah;
                case "haggai": return BibleBook.Haggai;
                case "zechariah": return BibleBook.Zechariah;
                case "malachi": return BibleBook.Malachi;
                case "matthew": return BibleBook.Matthew;
                case "mark": return BibleBook.Mark;
                case "luke": return BibleBook.Luke;
                case "john": return BibleBook.John;
                case "acts": return BibleBook.Acts;
                case "romans": return BibleBook.Romans;
                case "1 corinthians": return BibleBook.Corinthians_1;
                case "2 corinthians": return BibleBook.Corinthians_2;
                case "galatians": return BibleBook.Galatians;
                case "ephesians": return BibleBook.Ephesians;
                case "philippians": return BibleBook.Philippians;
                case "colossians": return BibleBook.Colossians;
                case "1 thessalonians": return BibleBook.Thessalonians_1;
                case "2 thessalonians": return BibleBook.Thessalonians_2;
                case "1 timothy": return BibleBook.Timothy_1;
                case "2 timothy": return BibleBook.Timothy_2;
                case "titus": return BibleBook.Titus;
                case "philemon": return BibleBook.Philemon;
                case "hebrews": return BibleBook.Hebrews;
                case "james": return BibleBook.James;
                case "1 peter": return BibleBook.Peter_1;
                case "2 peter": return BibleBook.Peter_2;
                case "1 john": return BibleBook.John_1;
                case "2 john": return BibleBook.John_2;
                case "3 john": return BibleBook.John_3;
                case "jude": return BibleBook.Jude;
                case "revelation": return BibleBook.Revelation;
            }
            throw new ArgumentException($"'{book}' is not a recognized book.");
        }

        public static string ToFriendlyString(this BibleBook book, Boolean hasChapter = false)
        {
            switch (book)
            {
                case BibleBook.Genesis: return "Genesis";
                case BibleBook.Exodus: return "Exodus";
                case BibleBook.Leviticus: return "Leviticus";
                case BibleBook.Numbers: return "Numbers";
                case BibleBook.Deuteronomy: return "Deuteronomy";
                case BibleBook.Joshua: return "Joshua";
                case BibleBook.Judges: return "Judges";
                case BibleBook.Ruth: return "Ruth";
                case BibleBook.Samuel_1: return "1 Samuel";
                case BibleBook.Samuel_2: return "2 Samuel";
                case BibleBook.Kings_1: return "1 Kings";
                case BibleBook.Kings_2: return "2 Kings";
                case BibleBook.Chronicles_1: return "1 Chronicles";
                case BibleBook.Chronicles_2: return "2 Chronicles";
                case BibleBook.Ezra: return "Ezra";
                case BibleBook.Nehemiah: return "Nehemiah";
                case BibleBook.Esther: return "Esther";
                case BibleBook.Job: return "Job";
                case BibleBook.Psalms:
                    {
                        if (hasChapter)
                        {
                            return "Psalm";
                        }
                        else
                        {
                            return "Psalms";
                        }
                    }
                case BibleBook.Proverbs: return "Proverbs";
                case BibleBook.Ecclesiastes: return "Ecclesiastes";
                case BibleBook.Song_Of_Songs: return "Song of Songs";
                case BibleBook.Isaiah: return "Isaiah";
                case BibleBook.Jeremiah: return "Jeremiah";
                case BibleBook.Lamentations: return "Lamentations";
                case BibleBook.Ezekiel: return "Ezekiel";
                case BibleBook.Daniel: return "Daniel";
                case BibleBook.Hosea: return "Hosea";
                case BibleBook.Joel: return "Joel";
                case BibleBook.Amos: return "Amos";
                case BibleBook.Obadiah: return "Obadiah";
                case BibleBook.Jonah: return "Jonah";
                case BibleBook.Micah: return "Micah";
                case BibleBook.Nahum: return "Nahum";
                case BibleBook.Habakkuk: return "Habakkuk";
                case BibleBook.Zephaniah: return "Zephaniah";
                case BibleBook.Haggai: return "Haggai";
                case BibleBook.Zechariah: return "Zechariah";
                case BibleBook.Malachi: return "Malachi";
                case BibleBook.Matthew: return "Matthew";
                case BibleBook.Mark: return "Mark";
                case BibleBook.Luke: return "Luke";
                case BibleBook.John: return "John";
                case BibleBook.Acts: return "Acts";
                case BibleBook.Romans: return "Romans";
                case BibleBook.Corinthians_1: return "1 Corinthians";
                case BibleBook.Corinthians_2: return "2 Corinthians";
                case BibleBook.Galatians: return "Galatians";
                case BibleBook.Ephesians: return "Ephesians";
                case BibleBook.Philippians: return "Philippians";
                case BibleBook.Colossians: return "Colossians";
                case BibleBook.Thessalonians_1: return "1 Thessalonians";
                case BibleBook.Thessalonians_2: return "2 Thessalonians";
                case BibleBook.Timothy_1: return "1 Timothy";
                case BibleBook.Timothy_2: return "2 Timothy";
                case BibleBook.Titus: return "Titus";
                case BibleBook.Philemon: return "Philemon";
                case BibleBook.Hebrews: return "Hebrews";
                case BibleBook.James: return "James";
                case BibleBook.Peter_1: return "1 Peter";
                case BibleBook.Peter_2: return "2 Peter";
                case BibleBook.John_1: return "1 John";
                case BibleBook.John_2: return "2 John";
                case BibleBook.John_3: return "3 John";
                case BibleBook.Jude: return "Jude";
                case BibleBook.Revelation: return "Revelation";
            }
            throw new ArgumentException($"'{book.ToString()}' is not a recognized book.");
        }
    }
}