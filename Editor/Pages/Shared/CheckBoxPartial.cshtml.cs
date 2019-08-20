using System;
using System.ComponentModel.DataAnnotations;

namespace Editor.Pages.Shared
{
    public class CheckBoxModel
    {
        public string DisplayName { get; set; }

        public bool IsChecked { get; set; }
    }
}