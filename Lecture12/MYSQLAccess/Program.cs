using System;

using MySql.Data.MySqlClient;

namespace MYSQLAccess
{
    internal class Program
    {
        private static void Main()
        {
            // 데이터베이스와의 연결을 추가 합니다.
            string strConn = "Server=localhost;Database=ckgame;Uid=root;Pwd=root;";
            MySqlConnection conn = new MySqlConnection(strConn);
            try
            {
                // 데이터베이스를 open 합니다.
                conn.Open();
                // 적절한 QUERY 를 만들고 실행 합니다.
                string sql = "INSERT INTO login VALUES ('ironman', '0425')";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                // 데이터베이스를 close 합니다.
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
