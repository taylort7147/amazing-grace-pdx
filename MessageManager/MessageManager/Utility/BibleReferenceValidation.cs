using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BibleReferenceParser.Data;
using BibleReferenceParser.Parsing;

namespace MessageManager.Utility
{
    public class BibleReferenceValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var s = value as string;
            if (s is null)
            {
                return new ValidationResult("Invalid object type");
            }
            if (s.Length == 0)
            {
                return ValidationResult.Success;
            }
            try
            {
                var referenceRanges = Parser.Parse(s);
                var invalidReferences = new List<string>();
                foreach (var range in referenceRanges)
                {
                    if (!BibleDetails.IsValidBibleReferenceRange(range))
                    {
                        invalidReferences.Add(range.ToFriendlyString());
                    }
                }
                if (invalidReferences.Count > 0)
                {
                    var errorMessage = "Invalid Bible references:\n" + string.Join(", ", invalidReferences);
                    return new ValidationResult(errorMessage);
                }
            }
            catch (InvalidOperationException e)
            {
                return new ValidationResult("Unable to parse bible reference string. Details: " + e.Message);
            }
            return ValidationResult.Success;
        }
    }

}