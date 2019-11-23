using myschooldbapplication.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace myschooldbapplication.Models
{
    public partial class Student
    {
        public Student()
        {
            Admission1 = new HashSet<Admission1>();
        }

        public int StId { get; set; }
        [Display(Name ="Student Name")]
        [Required(ErrorMessage = "Name field can not be blank")]
        public string StName { get; set; }
        [DataType(DataType.Date)]
        [Required]
        [AgeCheck]
        [Display(Name ="Date Of Birth")]
        public DateTime Dob { get; set; }
        [Display(Name = "Place of Birth")]
        public string POB { get; set; }
      
        [Display(Name = "Nationality")]
        public string Nationality { get; set; }
 
        [Display(Name = "Please select Gender")]
        public string Gender { get; set; }
        [Display(Name = "Father/Guradian's Name")]

        public string ParentName { get; set; }
        [Display(Name = "Father Mobile Number")]
        [StringLength(10)]
        public string ParentMobile { get; set; }
        [Display(Name = "Father's WhatsappNo")]
   
        public string ParentWhatsappNo { get; set; }
        [Required(ErrorMessage = "Email id Can not be blank")]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        [RegularExpression(".+\\@.+\\..+",
ErrorMessage = "Please enter a valid email address")]
        [Display(Name = "Email ID")]
        public string EmailId { get; set; }
        [Display(Name = "Father Qualification ")]
        public string Qualification { get; set; }
        [Display(Name = "Father Occupation")]
        public string Occupation { get; set; }
        [Display(Name = "Mother's Name")]
        public string MotherName { get; set; }
        [Display(Name = "Mother's Phone Number")]
        public string MotherPhone { get; set; }
        [Display(Name = "Mother's Qualification")]
        public string MotherQualification { get; set; }
        [Display(Name = "Mother's Occupation")]
        public string MotherOccu { get; set; }
        [Display(Name = "Mother's Whatsapp Number")]
        public string MotherWhatsapp { get; set; }



        public string  Address { get; set; }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public byte[] Data { get; set; }

        public int? Length { get; set; }

        public int? Width { get; set; }

        public int? Height { get; set; }

        public string ContentType { get; set; }

        public ICollection<Admission1> Admission1 { get; set; }
    }
    public class AgeCheck:ValidationAttribute
    {
        protected override ValidationResult IsValid(object obj,ValidationContext validationContext)
        {
         
                var model = validationContext.ObjectInstance as EnrollModel;
                if (model == null)

                {
                    throw new ArgumentException("Attribute not applied on student");
                }

           DateTime backthree= DateTime.Now.AddYears(-3);
                if (model.Dob > backthree)
                {
                    return new ValidationResult(GetErrorMessage(validationContext));
                }
                return ValidationResult.Success;
           
           
            
        }

        private string GetErrorMessage(ValidationContext validationContext)
        {
            if(!string.IsNullOrEmpty(this.ErrorMessage))
            {
                return this.ErrorMessage;
            }
            return $"{ validationContext.DisplayName} student should be of three year";
        }
    }
}
