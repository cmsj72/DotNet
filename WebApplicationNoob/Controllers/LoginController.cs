using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
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

        public ActionResult AddGuest()
        {
            string name = Request["name"];
            string msg = Request["msg"];

            var db = new GuestDbContext();

            Guest g = new Guest();
            g.Name = name;
            g.CreateDate = DateTime.Now;
            g.Message = msg;

            db.Guests.Add(g);
            db.SaveChanges();

            return RedirectToAction("ShowGuests");
        }

        public ActionResult ShowGuest()
        {
            var db = new GuestDbContext();

            //  select top 10 * from guest order by id desc;
            List<Guest> guests = db.Guests.OrderByDescending(p => p.Id).Take(10).ToList();

            //  View 메서드의 파라미터로 전달
            //  모델 객체를 MVC의 View로 넘기는 방법 중의 하나
            //  이렇게 넘겨진 모델 객체는 View에서 @model로 전달된 모델 타입을 지정해주고(전체 네임스페이스 써야함)
            //  View에서 Model이라는 속성을 사용하여 해당 모델 객체를 사용.
            return View(guests);
        }

        //public ActionResult NoFunc()
        //{
        //    var strConn = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        //    using(SqlConnection conn = new SqlConnection(strConn))
        //    {
        //        conn.Open();
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Connection = conn;

        //        string sql = "SELECT 1 FROM Login WHERE Username=@User AND Password=@Pwd";
        //        cmd.CommandText = sql;
        //        cmd.Parameters.AddWithValue("@user", username);
        //        cmd.Parameters.AddWithValue("@Pwd", password);

        //        object oresult = cmd.ExecuteScalar();
        //        //  생략
        //    }
        //    return View();
        //}
    }
}