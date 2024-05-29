using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationNoob.Models;

namespace WebApplicationNoob.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Check()
        {
            string user = Request["username"];
            string pwd = Request["password"];

            bool success = LoginManager.CheckLogin(user, pwd);

            ViewBag.Success = success;
            return View();
        }

        //  컨트롤러에서 View로 데이터 넘기기
        public ActionResult MyView(int id)
        {
            //  ViewBag에 임의의 속성 지정
            ViewBag.Title = id + " 자료";

            //  ViewData 해시테이블 사용
            ViewData["MethodName"] = nameof(ShowGuest);

            Guest guest = new Guest
            {
                Id = 1,
                Name = "Alex",
                CreateDate = DateTime.Now,
                Message = "Congrats!"
            };

            return View(guest);
        }
    }
}