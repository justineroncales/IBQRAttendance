using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IBQRAttendance.Models
{
    public class Users
    {
        public int USERID { get; set; }
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name Required!")]
        public string FIRSTNAME { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name Required!")]
        public string LASTNAME { get; set; }

        [Display(Name = "Middle Name")]
        [Required(ErrorMessage = "Middle Name Required!")]
        public string MIDDLENAME { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email Required!")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail id is not valid")]
        public string EMAIL { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address Required!")]
        public string ADDRESS { get; set; }

        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "Entered phone format is not valid.")]
        [Required(ErrorMessage = "Phone Number Required!")]
        [Display(Name = "Phone Number")]
        public string PHONENUMBER { get; set; }
        public string _LEVEL { get; set; }
        public string _DEPARTMENT { get; set; }
        public string URLPATH { get; set; }

        [Display(Name = "Department")]
        [Required(ErrorMessage = "Department Required!")]
        public Department department { get; set; }
        public Level level { get; set; }
    }
    public enum Department 
    {
        [Display(Name = "Junior High")]
        JUNIORHIGH = 1,
        [Display(Name = "Senior High")]
        SENIORHIGH = 2,
        [Display(Name = "Education")]
        EDUCATION = 3,
        [Display(Name = "Business")]
        BUSINESS = 4,
        [Display(Name = "Computer Science")]
        COMSCI =5
    }

    public enum Level
    {
        [Display(Name = "G7")]
        GSEVEN = 1,
        [Display(Name = "G8")]
        GEIGHT = 2,
        [Display(Name = "G9")]
        GNINE = 3,
        [Display(Name = "G10")]
        GTEN = 4,
        [Display(Name = "G11")]
        GELEVEN = 5,
        [Display(Name = "G12")]
        GTWELVE = 6,
        [Display(Name = "First Year")]
        FIRSTYEAR = 7,
        [Display(Name = "Second Year")]
        sECONDYEAR = 8,
        [Display(Name = "Third Year")]
        THIRDYEAR = 9,
        [Display(Name = "Fourth Year")]
        FOURTHYEAR = 10,
    }
    public class GlobalClass
    {
        public Users users { get; set; }
        public QRCodeModel qRCodeModel { get; set; }
        public List<QRCodeModel> LisqRCodeModel { get; set; }
        public List<Users> ListUsers { get; set; }

    }
}