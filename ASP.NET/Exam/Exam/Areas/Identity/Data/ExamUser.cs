using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Exam.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ExamUser class
public class ExamUser : IdentityUser
{
    [Required]
    [Display(Name = "Passport Number")]
    public string PassportNumber { get; set; }
    [Display(Name = "Phone")]
    [Phone]
    public long Phone { get; set; }
    [Display(Name = "Birthday date")]
    public DateTime BDate { get; set; }
}

