using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace WebApplicationNoob.Models
{
    public class LoginManager
    {
        public static bool CheckLogin(string username, string password)
        {
            var strConn = WebConfigurationManager.ConnectionStrings["DeafaultConnection"].ConnectionString;

            //  using : 개체 범위를 정의할 때 사용. 그 범위를 벗어나면 자동으로 dispose
            //  file, font, DB 경우 사용할 때 일정 부분의 메모리를 사용(컴퓨터의 자원 할당)
            //  반납해야 성능이 개선, 프로그램의 문제 발생X
            //  하지만 남발하면 가독성↓, 성능↓
            //  using문을 메소드 안에 커넥트 형식으로 {} 안에서만 사용하고 나면 바로 dispose
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                conn.Open();
                string sql = "SELECT NULL FROM Login WHERE Username=@user AND Password=@pwd";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@user", username);
                cmd.Parameters.AddWithValue("@pwd", password);
                object res = cmd.ExecuteScalar();

                return res != null;
            }
        }
    }

    //  EntityFramework(System.Data.Entity)의 DbContext 클래스를 상속
    public class GuestDbContext : DbContext
    {
        //  base 클래스의 생성자를 호출
        //  이를 통해 처음 GuestDbCOntext 객체가 생성되면 (Web.config의 정의에 따라)
        //  기존 DB가 없는 경우 자동으로 DB를 생성
        //  별도의 설정이 없이 디폴트로는 App_Data 폴더에 해당 클래스명(GuestDbContext)를 따서 Local DB 파일(*.mdf)를 생성
        public GuestDbContext() : base()
        {

        }

        public DbSet<Guest> Guests { get; set; }
    }

    //  [Table(테이블명)] Attribute는 DB에서 생성될 테이블명을 명시적으로 지정
    //  명시하지 않으면 디폴트로 클래스명을 테이블명으로 사용
    [Table("Guest")]
    public class Guest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public string Message { get; set; }
    }
}