using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace NU2UWebApplication.Models
{
    public class UnicodeConversion
    {
        [Key]
        public string Project_ID { get; set; }
    }

    public class UnicodeConversionDBContext : DbContext
    {
        public DbSet<UnicodeConversion> UnicodeConversions { get; set; }
    }
}

    