using System;
using System.IO;
using Antlr4.Runtime;

namespace BibleReferenceParser.Parsing
{
    public class ThrowingErrorListener : IAntlrErrorListener<int>
    {
        public static ThrowingErrorListener Instance = new ThrowingErrorListener();

        public void SyntaxError(TextWriter output, IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            throw new InvalidOperationException(
                "Line " + line.ToString() + ", char " + charPositionInLine.ToString() + ", " + msg
            );
        }
    }
}