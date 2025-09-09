using System;
using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using BibleReferenceParser.Data;
using BibleReferenceParser.Grammar.Generated;

namespace BibleReferenceParser.Parsing
{
    public class Parser
    {
        public static List<BibleReferenceRange> Parse(string input)
        {
            var inputStream = CharStreams.fromString(input);
            var bibleReferenceLexer = new BibleReferenceLexer(inputStream);
            bibleReferenceLexer.RemoveErrorListeners();
            bibleReferenceLexer.AddErrorListener(ThrowingErrorListener.Instance);
            var commonTokenStream = new CommonTokenStream(bibleReferenceLexer);
            var bibleReferenceParser = new BibleReferenceParser.Grammar.Generated.BibleReferenceParser(commonTokenStream);
            var referenceContext = bibleReferenceParser.reference();
            var listener = new BibleReferenceListener();

            var tokens = commonTokenStream.GetTokens();
            Console.WriteLine();
            Console.WriteLine("Tokens:");
            foreach (var token in tokens)
            {
                Console.WriteLine($"  {token.Text} [{bibleReferenceLexer.ChannelNames[token.Channel]}]");
            }
            var walker = new ParseTreeWalker();

            walker.Walk(listener, referenceContext);

            return listener.References;
        }

        public static List<BibleReferenceRange> TryParse(string input)
        {
            try
            {
                return Parse(input);
            }
            catch (Exception)
            {
            }
            return null;
        }
    }
}