using IBQRAttendance.Models;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ZXing;

namespace IBQRAttendance.Controllers
{
    public class QRCodeController : System.Web.Mvc.Controller
    {
        public ActionResult Views()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Register(PassengerInfo passengerInfo)
        {
            var result = PassengerInfo.Register(passengerInfo);
            return Json(result);
        }
        [HttpPost]
        public ActionResult Login(PassengerInfo passengerInfo)
        {
            var result = PassengerInfo.Login(passengerInfo);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetDriverInfos()
        {
            var result = DriverInfo.GetDriverInfos();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Booked(Booking booking)
        {
            var result = Booking.Booked(booking);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult CancelBooked(int id)
        {
            var result = Booking.CancelBooked(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult IsBooked(int id)
        {
            var result = Booking.IsBooked(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ChangePassword(PassengerInfo passengerInfo)
        {
            var result = PassengerInfo.ChangePassword(passengerInfo);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetHistories(int id)
        {
            var result = History.GetDriverHistory(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Authenticate(PassengerInfo passengerInfo)
        {
            var result = PassengerInfo.Authenticate(passengerInfo);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
    
}
