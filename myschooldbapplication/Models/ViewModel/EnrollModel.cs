using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace myschooldbapplication.Models.ViewModel
{
    public class EnrollModel
    { 
        public int? AdId { get; set; }
        public int? StId { get; set; }
        public int? StdId { get; set; }
        public int? FTId { get; set; }
        public int? FeeYear { get; set; }
        public int? PaidAmount { get; set; }
        public int? Totalfees { get; set; }
        public int? Pendingamt { get; set; }
        public DateTime? Chequedt { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }

        [Display(Name = "Student's Full Name")]
        [Required(ErrorMessage = "Name field can not be blank")]
        public string StName { get; set; }
        [DataType(DataType.Date)]
        [Required]
       [AgeCheck]
        [Display(Name = "Date Of Birth")]
        public DateTime Dob { get; set; }
        [Required]
        [Display(Name ="Place of Birth")]
        public string POB { get; set; }
        [Required]
        [Display(Name = "Nationality")]
        public string Nationality { get; set; }
        [Required]
        [Display(Name ="Please select Gender")]
        public string Gender { get; set; }
       
        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Parent Name can not be blank")]
         [Display(Name = "Standard")]
        public string Stname { get; set; }

        [Display(Name = "Father's Name")]
        public string ParentName { get; set; }
        [Display(Name = "Father Mobile Number")]
        [StringLength(10)]
        public string ParentMobile { get; set; }
        [Display(Name ="Father's WhatsappNo")]
        [Required]
        public string ParentWhatsappNo { get; set; }
        [Required(ErrorMessage = "Email id Can not be blank")]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        [RegularExpression(".+\\@.+\\..+",
ErrorMessage = "Please enter a valid email address")]
        [Display(Name = "Email ID")]
        public string EmailId { get; set; }
      [Display(Name ="Father Qualification ")]
        public string Qualification { get; set; }
        [Display(Name ="Father Occupation")]
        public string Occupation { get; set; }
        [Display(Name ="Mother's Name")]
        public string MotherName { get; set; }
        [Display(Name ="Mother's Phone Number")]
        public string MotherPhone { get; set; }
        [Display(Name ="Mother's Qualification")]
        public string MotherQualification { get; set; }
        [Display(Name ="Mother's Occupation")]
        public string MotherOccu { get; set; }
        [Display(Name ="Mother's Whatsapp Number")]
        public string MotherWhatsapp { get; set; }
        public int AdTId { get; set; }

        public string Term { get; set; }

        public int? TermAmount { get; set; }
       
        [DataType(DataType.Date)]
        public DateTime? TermDate { get; set; }
        
        public int? AdYear { get; set; }
                
      
        public string Feetype1 { get; set; }
        
        public int? TotalFees { get; set; }
        public string paystatusid { get; set; }
        public string divdata { get; set; }
        public IFormFile files { get; set; }
        public Guid Pic { get; set; }
    }
}
