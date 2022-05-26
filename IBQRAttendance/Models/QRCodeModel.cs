using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using ZXing;
using System.Web.Mvc;

namespace IBQRAttendance.Models
{
    public class QRCodeModel
    {
        [Display(Name = "QRCode Text")]
        [Required(ErrorMessage = "QRCode Required!")]
        public string QRCodeText { get; set; }
        
        [Display(Name = "QRCode Image")]
        public string QRCodeImagePath { get; set; }
        public string QRValue { get; set; }
        public DateTime QRTime { get; set; }

        public string FULLNAME { get; set; }
        public string STAMPS { get; set; }
        public string ACTIVITY { get; set; }
        public string DAYS { get; set; }
        public string LEVEL { get; set; }
        public string DEPARTMENT { get; set; }
    }
}