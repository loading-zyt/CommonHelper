using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace DbOperate
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            /*读取*/
            //User u = new User();
            //u = p.getUserById(2);
            //Console.WriteLine(u.ID + "|" + u.UserName + "|" + u.Password);
            //Console.ReadKey();
            //DataView data = p.getUsers();
            /*更新*/
            User user = new User();
            user.ID = 4;
            user.UserName = "abcde";
            user.Password = "4567";
            p.UpdateUser(user);
            /*追加*/
            //User user = new User();
            //user.UserName = "haha";
            //user.Password = "111";
            //p.InsertUser(user);
            /*删除*/
            p.DeleteUser(3);
        }
        public OleDbConnection getConn()
        {
            string connStr = @"Provider=Microsoft.Jet.OLEDB.4.0 ;Data Source=E:\play\demo\CSharp\test.mdb";
            OleDbConnection conn = new OleDbConnection(connStr);
            return conn;
        }

        public User getUserById(int id)
        {
            User user = new User();
            try
            {
                // 获取连接对象
                OleDbConnection conn = getConn();
                // 定义SQL
                string strSql = "SELECT * FROM TestUser WHERE ID = " + id;
                // 建立对数据源执行的 SQL 语句或存储过程
                OleDbCommand myCommand = new OleDbCommand(strSql, conn);
                // 打开数据库连接
                conn.Open();
                OleDbDataReader reader;
                // 执行，获取结果
                reader = myCommand.ExecuteReader();
                if (reader.Read())
                {
                    user.ID = (int)reader["ID"];
                    user.UserName = reader["Name"].ToString();
                    user.Password = reader["Password"].ToString();
                }
                else
                {
                    throw (new Exception("record is null."));
                }
                reader.Close();
                conn.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
            return user;
        }

        public DataView getUsers()
        {
            DataView dataView;
            System.Data.DataSet myDataSet;
            try
            {
                OleDbConnection conn = getConn();
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                string sqlStr = "SELECT * FROM TestUser";
                myDataSet = new DataSet();
                adapter.SelectCommand = new OleDbCommand(sqlStr, conn);
                adapter.Fill(myDataSet, "Users");
                conn.Close();
            }
            catch (Exception e)
            {

                throw e;
            }
            dataView = new DataView(myDataSet.Tables["Users"]);
            return dataView;
        }

        public Boolean UpdateUser(User user)
        {
            Boolean ret = false;
            try
            {
                OleDbConnection conn = getConn();
                conn.Open();
                //string strSql = "UPDATE TestUser SET Name = {0}, Password = {1} WHERE ID = {2}", user.UserName, user.Password, user.ID;
                StringBuilder strSql = new StringBuilder();
                strSql.Append("UPDATE TestUser SET Name ='");
                strSql.Append(user.UserName);
                strSql.Append("',Password ='");
                strSql.Append(user.Password);
                strSql.Append("' WHERE ID = ");
                strSql.Append(user.ID);
                OleDbCommand myCmd = new OleDbCommand(strSql.ToString(), conn);
                myCmd.ExecuteNonQuery();
                conn.Close();
                ret = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            return ret;
        }

        public Boolean DeleteUser(int id)
        {
            Boolean ret = false;
            try
            {
                OleDbConnection conn = getConn();
                conn.Open();
                string strSql = "DELETE * FROM TestUser WHERE ID = " + id;
                OleDbCommand myCmd = new OleDbCommand(strSql, conn);
                myCmd.ExecuteNonQuery();
                conn.Close();
                ret = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            return ret;
        }

        public Boolean InsertUser(User user)
        {
            Boolean ret = false;
            try
            {
                OleDbConnection conn = getConn();
                conn.Open();
                string strSql = "INSERT INTO TestUser(Name,Password) VALUES('";
                strSql += user.UserName + "','";
                strSql += user.Password + "')";
                OleDbCommand myCmd = new OleDbCommand(strSql, conn);
                myCmd.ExecuteNonQuery();
                conn.Close();
                ret = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            return ret;
        }
    }
}