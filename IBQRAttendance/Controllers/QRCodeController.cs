using IBQRAttendance.Code;
using IBQRAttendance.Models;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ZXing;

namespace IBQRAttendance.Controllers
{
    public class QRCodeController : Controller
    {
        // GET: QRCode
        public ActionResult Index()
        {
            //string controller = RouteData.Values["controller"].ToString();
            //return RedirectToAction("Attendance", controller, new { deptid = 0});
            return View()
;        }

        public ActionResult Read()
        {
            return View(ReadQRCode());
        }

        // GET: QRCode/Create
        [HttpPost]
        public ActionResult Generate(GlobalClass qrcode)
        {
            try
            {
                qrcode.qRCodeModel = GenerateQRCode(qrcode.qRCodeModel.QRCodeText);
                ViewBag.Message = "QR Code Created successfully";
            }
            catch (Exception ex)
            {
                //catch exception if there is any
            }
            return View("Index", qrcode);
        }
        public QRCodeModel GenerateQRCode(string qrcodeText)
        {
            QRCodeModel qR = new QRCodeModel();
            string folderPath = "/Images/";
            Guid id = Guid.NewGuid();
            string imagePath = "/Images/"+ id +"_QrCode.jpg";
            if (!Directory.Exists(Server.MapPath(folderPath)))
            {
                Directory.CreateDirectory(Server.MapPath(folderPath));
            }

            var barcodeWriter = new BarcodeWriter();
            barcodeWriter.Format = BarcodeFormat.QR_CODE;
            var result = barcodeWriter.Write(qrcodeText);

            string barcodePath = Server.MapPath(imagePath);
            var barcodeBitmap = new Bitmap(result);
            using (MemoryStream memory = new MemoryStream())
            {
                using (FileStream fs = new FileStream(barcodePath, FileMode.Create, FileAccess.ReadWrite))
                {
                    barcodeBitmap.Save(memory, ImageFormat.Jpeg);
                    byte[] bytes = memory.ToArray();
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
            qR.QRCodeImagePath = imagePath;
            return qR;
        }
        public QRCodeModel ReadQRCode()
        {
            string barcodeText = "";
            string imagePath = "~/Images/QrCode.jpg";
            string barcodePath = Server.MapPath(imagePath);
            var barcodeReader = new BarcodeReader();

            var result = barcodeReader.Decode(new Bitmap(barcodePath));
            if (result != null)
            {
                barcodeText = result.Text;
            }
            return new QRCodeModel() { QRCodeText = barcodeText, QRCodeImagePath = imagePath };
        }
        [HttpPost]
        public ActionResult Register(GlobalClass qrcode)
        {
            try
            {
                Codes code = new Codes();
                bool result = code.Register(qrcode);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Index");
            }
        }
        [HttpPost]
        public ActionResult Login(QRCodeModel qrcode)
        {
            Codes code = new Codes();
            int result = code.Login(qrcode);
            return Json(Convert.ToString(result), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Attendance()
        {
            Codes codes = new Codes();
            GlobalClass gClass = new GlobalClass();
            gClass.LisqRCodeModel = codes.GetTimeList(0);
            return View(gClass);
        }
        public ActionResult RegisteredList()
        {
            Codes codes = new Codes();
            GlobalClass gClass = new GlobalClass();
            gClass.ListUsers = codes.GetRegiteredList();
            return View(gClass);
        }
        public ActionResult RemoveFromList(int USERID)
        {
            Codes codes = new Codes();
            bool result = codes.RemoveFromList(USERID);
            return RedirectToAction("RegisteredList");
        }
    }
}
