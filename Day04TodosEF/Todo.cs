using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Day04TodosEF
{
    public class Todo
    {
        int Id { get; set; }

        [Required]
        [StringLength(100)]
        [RegularExpression("^[a-zA-Z0-9./,;+)(*!-]{1,100}")]
        string Task { get; set; } // 1-100 characters, only letters, digits, space ./,;-+)(*! allowed

        [Required]
        [Range(1,5)]
        int Difficulty { get; set; } // 1-5 only front-end validation

        [Required]
        [DataType(DataType.DateTime)]
        DateTime DueDate { get; set; } // 1900-2099 year required, format in GUI is whatever the OS is configured to use

        [Required]
        Statuses Status { get; set; }
        enum Statuses { Pending, Done, Delegated }
    }
}
