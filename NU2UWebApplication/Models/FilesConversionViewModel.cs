using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NU2UWebApplication.Models
{
    public class FilesConversionViewModel : BaseConversionViewModel
    {
        public FilesConversionViewModel()
        {
            InputFileList = new List<string>();
            OutputFileList = new List<string>();
        }

       
        public bool IsNonUnicodeToUnicode { get; set; }

        public List<string> InputFileList { get; set; }

        public List<string> OutputFileList { get; set; }

        public string Error { get; set; }

    }
}