using IBQRAttendance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;
using System.Web.Mvc;

namespace IBQRAttendance.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddDriversInformation()
        {
            Cluster cluster = new Cluster();
            var items = cluster.getListofCluster();

            if (items != null)
            {
                ViewBag.data = items;
            }
            return View();
        }
        public ActionResult RegisterDriver(Users users)
        {
            Users _user = new Users();
            var result = _user.RegisterDriver(users);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetClusters()
        {
            Cluster cluster = new Cluster();
            var result = cluster.getListofCluster();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetDriverLocation(int id)
        {
            Users _user = new Users();
            var result = _user.GetDriverLocation(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Login(Users user)
        {
            Users _user = new Users();
            var result = _user.Login(user);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Logind(Users user)
        {
            Users _user = new Users();
            var result = _user.LoginD(user);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Register(Users user)
        {
            Users _user = new Users();
            var result = _user.Register(user);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateLocation(Users user)
        {
            Users _user = new Users();
            var result = _user.UpdateLocation(user);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetSchedules(int id) 
        {
            Schedule schedule = new Schedule();
            var result = schedule.GetSchedules(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SendReport(Report report)
        {
            Report _report = new Report();
            var result = _report.SendReport(report);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetUsersContact(int id)
        {
            Users _user = new Users();
            var result = _user.GetUsersContact(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetDriverPerC(int id)
        {
            Users _user = new Users();
            var result = _user.GetDriverPerC(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}