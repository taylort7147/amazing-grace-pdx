using System;

namespace BibleReferenceParser.Data
{
    public class BibleReferenceRangeBuilder
    {
        private BibleReference First;
        private BibleReference Last;

        public BibleReferenceRangeBuilder AddReference(BibleReference reference)
        {
            if (Last != null)
            {
                throw new InvalidOperationException("Too many references added. Only two references are supported.");
            }
            if (First == null)
            {
                First = reference;
            }
            else
            {
                Last = reference;
            }
            return this;
        }

        public BibleReferenceRange Build()
        {
            if (First == null)
            {
                throw new InvalidOperationException("Must add at least one reference");
            }

            var range = new BibleReferenceRange { First = First, Last = Last };
            return range;
        }
    }
}