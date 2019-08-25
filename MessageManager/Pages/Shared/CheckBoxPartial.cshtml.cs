using System;
using System.ComponentModel.DataAnnotations;

namespace MessageManager.Pages.Shared
{
    public class CheckBoxModel
    {
        public string DisplayName { get; set; }

        public bool IsChecked { get; set; }
    }
}