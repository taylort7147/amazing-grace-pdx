using System;
using System.Collections.Generic;
using Antlr4.Runtime.Misc;
using BibleReferenceParser.Data;

namespace BibleReferenceParser.Parsing
{
    public class BibleReferenceListener : BibleReferenceParser.Grammar.Generated.BibleReferenceBaseListener
    {
        public List<BibleReferenceRange> References { get; private set; }

        private BibleReferenceRangeBuilder ReferenceRangeBuilder;
        private BibleReferenceBuilder ReferenceBuilder;

        public BibleReferenceListener()
        {
            Reset();
        }

        public void Reset()
        {
            ReferenceRangeBuilder = null;
            ReferenceBuilder = null;
            References = null;
        }

        /// <summary>
        /// Enter a parse tree produced by <see cref="BibleReferenceParser.reference"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public override void EnterReference([NotNull] BibleReferenceParser.Grammar.Generated.BibleReferenceParser.ReferenceContext context)
        {
            ReferenceRangeBuilder = new BibleReferenceRangeBuilder();
            ReferenceBuilder = new BibleReferenceBuilder();
            References = new List<BibleReferenceRange>();
        }
        /// <summary>
        /// Exit a parse tree produced by <see cref="BibleReferenceParser.reference"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public override void ExitReference([NotNull] BibleReferenceParser.Grammar.Generated.BibleReferenceParser.ReferenceContext context)
        {
            var reference = ReferenceBuilder.Build();
            ReferenceRangeBuilder.AddReference(reference);
            var referenceRange = ReferenceRangeBuilder.Build();
            References.Add(referenceRange);
            ReferenceRangeBuilder = new BibleReferenceRangeBuilder();
        }
        /// <summary>
        /// Enter a parse tree produced by <see cref="BibleReferenceParser.book_reference"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public override void EnterBook_reference([NotNull] BibleReferenceParser.Grammar.Generated.BibleReferenceParser.Book_referenceContext context)
        {
        }
        /// <summary>
        /// Exit a parse tree produced by <see cref="BibleReferenceParser.book_reference"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public override void ExitBook_reference([NotNull] BibleReferenceParser.Grammar.Generated.BibleReferenceParser.Book_referenceContext context) { }
        /// <summary>
        /// Enter a parse tree produced by <see cref="BibleReferenceParser.book_range"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public override void EnterBook_range([NotNull] BibleReferenceParser.Grammar.Generated.BibleReferenceParser.Book_rangeContext context) { }
        /// <summary>
        /// Exit a parse tree produced by <see cref="BibleReferenceParser.book_range"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public override void ExitBook_range([NotNull] BibleReferenceParser.Grammar.Generated.BibleReferenceParser.Book_rangeContext context)
        {
        }
        /// <summary>
        /// Enter a parse tree produced by <see cref="BibleReferenceParser.book_with_chapter"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public override void EnterBook_with_chapter([NotNull] BibleReferenceParser.Grammar.Generated.BibleReferenceParser.Book_with_chapterContext context) { }
        /// <summary>
        /// Exit a parse tree produced by <see cref="BibleReferenceParser.book_with_chapter"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public override void ExitBook_with_chapter([NotNull] BibleReferenceParser.Grammar.Generated.BibleReferenceParser.Book_with_chapterContext context) { }
        /// <summary>
        /// Enter a parse tree produced by <see cref="BibleReferenceParser.book_without_chapter"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public override void EnterBook_without_chapter([NotNull] BibleReferenceParser.Grammar.Generated.BibleReferenceParser.Book_without_chapterContext context) { }
        /// <summary>
        /// Exit a parse tree produced by <see cref="BibleReferenceParser.book_without_chapter"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public override void ExitBook_without_chapter([NotNull] BibleReferenceParser.Grammar.Generated.BibleReferenceParser.Book_without_chapterContext context) { }
        /// <summary>
        /// Enter a parse tree produced by <see cref="BibleReferenceParser.book"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public override void EnterBook([NotNull] BibleReferenceParser.Grammar.Generated.BibleReferenceParser.BookContext context)
        {
            ReferenceBuilder.SetBook(context.GetChild(0).GetText());
        }
        /// <summary>
        /// Exit a parse tree produced by <see cref="BibleReferenceParser.book"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public override void ExitBook([NotNull] BibleReferenceParser.Grammar.Generated.BibleReferenceParser.BookContext context) { }
        /// <summary>
        /// Enter a parse tree produced by <see cref="BibleReferenceParser.chapter_reference"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public override void EnterChapter_reference([NotNull] BibleReferenceParser.Grammar.Generated.BibleReferenceParser.Chapter_referenceContext context) { }
        /// <summary>
        /// Exit a parse tree produced by <see cref="BibleReferenceParser.chapter_reference"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public override void ExitChapter_reference([NotNull] BibleReferenceParser.Grammar.Generated.BibleReferenceParser.Chapter_referenceContext context) { }
        /// <summary>
        /// Enter a parse tree produced by <see cref="BibleReferenceParser.chapter_verse_to_chapter_verse"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public override void EnterChapter_verse_to_chapter_verse([NotNull] BibleReferenceParser.Grammar.Generated.BibleReferenceParser.Chapter_verse_to_chapter_verseContext context) { }
        /// <summary>
        /// Exit a parse tree produced by <see cref="BibleReferenceParser.chapter_verse_to_chapter_verse"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public override void ExitChapter_verse_to_chapter_verse([NotNull] BibleReferenceParser.Grammar.Generated.BibleReferenceParser.Chapter_verse_to_chapter_verseContext context) { }
        /// <summary>
        /// Enter a parse tree produced by <see cref="BibleReferenceParser.chapter_to_chapter_verse"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public override void EnterChapter_to_chapter_verse([NotNull] BibleReferenceParser.Grammar.Generated.BibleReferenceParser.Chapter_to_chapter_verseContext context) { }
        /// <summary>
        /// Exit a parse tree produced by <see cref="BibleReferenceParser.chapter_to_chapter_verse"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public override void ExitChapter_to_chapter_verse([NotNull] BibleReferenceParser.Grammar.Generated.BibleReferenceParser.Chapter_to_chapter_verseContext context) { }
        /// <summary>
        /// Enter a parse tree produced by <see cref="BibleReferenceParser.chapter_to_chapter"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public override void EnterChapter_to_chapter([NotNull] BibleReferenceParser.Grammar.Generated.BibleReferenceParser.Chapter_to_chapterContext context) { }
        /// <summary>
        /// Exit a parse tree produced by <see cref="BibleReferenceParser.chapter_to_chapter"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public override void ExitChapter_to_chapter([NotNull] BibleReferenceParser.Grammar.Generated.BibleReferenceParser.Chapter_to_chapterContext context) { }
        /// <summary>
        /// Enter a parse tree produced by <see cref="BibleReferenceParser.chapter_with_verse"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public override void EnterChapter_with_verse([NotNull] BibleReferenceParser.Grammar.Generated.BibleReferenceParser.Chapter_with_verseContext context) { }
        /// <summary>
        /// Exit a parse tree produced by <see cref="BibleReferenceParser.chapter_with_verse"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public override void ExitChapter_with_verse([NotNull] BibleReferenceParser.Grammar.Generated.BibleReferenceParser.Chapter_with_verseContext context) { }
        /// <summary>
        /// Enter a parse tree produced by <see cref="BibleReferenceParser.chapter_without_verse"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public override void EnterChapter_without_verse([NotNull] BibleReferenceParser.Grammar.Generated.BibleReferenceParser.Chapter_without_verseContext context) { }
        /// <summary>
        /// Exit a parse tree produced by <see cref="BibleReferenceParser.chapter_without_verse"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public override void ExitChapter_without_verse([NotNull] BibleReferenceParser.Grammar.Generated.BibleReferenceParser.Chapter_without_verseContext context) { }
        /// <summary>
        /// Enter a parse tree produced by <see cref="BibleReferenceParser.chapter"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public override void EnterChapter([NotNull] BibleReferenceParser.Grammar.Generated.BibleReferenceParser.ChapterContext context)
        {
            ReferenceBuilder.SetChapter(int.Parse(context.GetChild(0).GetText()));
        }
        /// <summary>
        /// Exit a parse tree produced by <see cref="BibleReferenceParser.chapter"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public override void ExitChapter([NotNull] BibleReferenceParser.Grammar.Generated.BibleReferenceParser.ChapterContext context) { }
        /// <summary>
        /// Enter a parse tree produced by <see cref="BibleReferenceParser.verse_reference"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public override void EnterVerse_reference([NotNull] BibleReferenceParser.Grammar.Generated.BibleReferenceParser.Verse_referenceContext context) { }
        /// <summary>
        /// Exit a parse tree produced by <see cref="BibleReferenceParser.verse_reference"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public override void ExitVerse_reference([NotNull] BibleReferenceParser.Grammar.Generated.BibleReferenceParser.Verse_referenceContext context) { }
        /// <summary>
        /// Enter a parse tree produced by <see cref="BibleReferenceParser.verse_to_verse"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public override void EnterVerse_to_verse([NotNull] BibleReferenceParser.Grammar.Generated.BibleReferenceParser.Verse_to_verseContext context) { }
        /// <summary>
        /// Exit a parse tree produced by <see cref="BibleReferenceParser.verse_to_verse"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public override void ExitVerse_to_verse([NotNull] BibleReferenceParser.Grammar.Generated.BibleReferenceParser.Verse_to_verseContext context) { }
        /// <summary>
        /// Enter a parse tree produced by <see cref="BibleReferenceParser.verse"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public override void EnterVerse([NotNull] BibleReferenceParser.Grammar.Generated.BibleReferenceParser.VerseContext context)
        {
            ReferenceBuilder.SetVerse(int.Parse(context.GetChild(0).GetText()));
        }
        /// <summary>
        /// Exit a parse tree produced by <see cref="BibleReferenceParser.verse"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public override void ExitVerse([NotNull] BibleReferenceParser.Grammar.Generated.BibleReferenceParser.VerseContext context) { }
        /// <summary>
        /// Enter a parse tree produced by <see cref="BibleReferenceParser.range_op"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public override void EnterRange_op([NotNull] BibleReferenceParser.Grammar.Generated.BibleReferenceParser.Range_opContext context)
        {
            var reference = ReferenceBuilder.Build();
            ReferenceRangeBuilder.AddReference(reference);
        }
        /// <summary>
        /// Exit a parse tree produced by <see cref="BibleReferenceParser.range_op"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public override void ExitRange_op([NotNull] BibleReferenceParser.Grammar.Generated.BibleReferenceParser.Range_opContext context) { }
        /// <summary>
        /// Enter a parse tree produced by <see cref="BibleReferenceParser.separator_op"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public override void EnterSeparator_op([NotNull] BibleReferenceParser.Grammar.Generated.BibleReferenceParser.Separator_opContext context)
        {
            var reference = ReferenceBuilder.Build();
            ReferenceRangeBuilder.AddReference(reference);
            var referenceRange = ReferenceRangeBuilder.Build();
            References.Add(referenceRange);
            ReferenceRangeBuilder = new BibleReferenceRangeBuilder();
        }
        /// <summary>
        /// Exit a parse tree produced by <see cref="BibleReferenceParser.separator_op"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public override void ExitSeparator_op([NotNull] BibleReferenceParser.Grammar.Generated.BibleReferenceParser.Separator_opContext context) { }
        /// <summary>
        /// Enter a parse tree produced by <see cref="BibleReferenceParser.index_op"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public override void EnterIndex_op([NotNull] BibleReferenceParser.Grammar.Generated.BibleReferenceParser.Index_opContext context) { }
        /// <summary>
        /// Exit a parse tree produced by <see cref="BibleReferenceParser.index_op"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public override void ExitIndex_op([NotNull] BibleReferenceParser.Grammar.Generated.BibleReferenceParser.Index_opContext context) { }
    }
}