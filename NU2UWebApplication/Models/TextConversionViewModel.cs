using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NU2UWebApplication.Models
{
    public class TextConversionViewModel : BaseConversionViewModel
    {
        public TextConversionViewModel()
        {
            NonUnicodeFontList = new List<string>();
            UnicodeFontList = new List<string>();
        }

        public string NonUnicodeText { get; set; }

        public string UnicodeText { get; set; }

        public List<string> NonUnicodeFontList { get; set; }

        public List<string> UnicodeFontList { get; set; }

        public string SelectedNonUnicodeFont { get; set; }

        public string SelectedUnicodeFont { get; set; }
    }
}