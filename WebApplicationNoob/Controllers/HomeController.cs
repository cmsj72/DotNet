using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;

namespace WebApplicationNoob.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //  예제 1
        public ActionResult NewGuid()
        {
            string guid = Guid.NewGuid().ToString();

            var result = new ContentResult();
            result.Content = guid;
            return result;
        }

        //  예제 1을 더 간단히
        //public ActionResult NewGuid()
        //{
        //    string guid = Guid.NewGuid().ToString();
        //    return COntent(guid);
        //}

        //  파일 다운로드 및 업로드
        //  파일 다운로드 예제
        [HttpGet]
        public FileResult DownloadSample()
        {
            //  웹서버 상의 파일 절대 경로를 구함
            string filePath = HttpContext.Server.MapPath("~/Downloads/Sample.dll");

            //  파일 읽기
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            //  FileResult 관련
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, System.IO.Path.GetFileName(filePath));
        }

        //  파일 업로드 예제
        [HttpPost]
        //  HttpResponse.Message 클래스를 사용하기 위해서는 
        //  System.Net.Http 네임스페이스 사용해야함
        public HttpResponseMessage Upload()
        {
            //  req.Files는 파일 정보와 파일데이터를 가지고 있음.
            //  복수 파일들이 업로드 될 수 있으며, 각 파일당 다른 이름으로 웹 서버에 저장.
            var req = HttpContext.Request;

            //string authVal = req.Headers["Authorization"];
            //if (authVal != "BASIC SGVsbG8=")
            //{
            //    return new HttpResponseMessage(HttpStatusCode.BadRequest);
            //}

            if(req.Files.Count < 1)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            //  웹 서버의 /Uploads 폴더에 업로드한 파일명 그대로 저장하고 있지만
            //  동일한 파일명이 다른 사람에 의해 다시 업로드 될 수 있으므로
            //   Unique한 이름으로 저장하는 것이 좋다.
            foreach (string file in req.Files)
            {
                var uploadFile = req.Files[file];
                var outputFilePath = HttpContext.Server.MapPath("~/Uploads/" + uploadFile.FileName);
                uploadFile.SaveAs(outputFilePath);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);

        }
    }
}