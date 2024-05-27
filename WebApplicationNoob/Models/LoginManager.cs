using System;
using System.Collections.Generic;
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
}