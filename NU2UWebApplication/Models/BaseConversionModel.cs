using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NU2UWebApplication.Models
{
    public class BaseConversionViewModel
    {
        string ProcessID { get; set; }

        string UserMessage { get; set; }

        string ErrorMessage { get; set; }
    }
}