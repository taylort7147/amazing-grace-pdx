using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MessageManager.Models;

namespace MessageManager.Pages.Shared
{
    public class _MessageTableRowModel
    {
        public Message Message { get; set; }

        public IList<string> MatchingBibleReferences { get; set; }
    }
}