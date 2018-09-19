using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NU2UWebApplication.Models
{
    public class ConverterModel
    {
        public string SourceFont { get; set; }

        public string TargetFont { get; set; }

        public bool IsConverterAvailable { get; set; }
    }

    public class ConvertersModel : BaseConversionViewModel
    {
        public List<string> ConvertersIDList { get; set; }

        public List<string> TargetFontList { get; set; }

        public List<ConverterModel> ConvertersList { get; set; }

        public string Error { get; set; }

        public ConvertersModel()
        {
            ConvertersList = new List<ConverterModel>();
            ConvertersIDList = new List<string>();
            TargetFontList = new List<string>();
        }
    }
}