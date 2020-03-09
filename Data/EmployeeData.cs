
using MVCTEST.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVCTEST.Data
{
    public class EmployeeData
    {
        private string connetionString;
        private string sql;
        private SqlConnection sqlCnn;
        private SqlCommand sqlCmd;
        Employee emp = new Employee();
        List<Employee> emplist = new List<Employee>();
        public EmployeeData()
        {
            connetionString = "Data Source=LOCALHOST;Initial Catalog=DummyDatabase;Integrated Security=True;";
            sql = "SELECT * FROM [dbo].[employee]";

            sqlCnn = new SqlConnection(connetionString);

            sqlCnn.Open();
            sqlCmd = new SqlCommand(sql, sqlCnn);
            SqlDataReader sqlReader = sqlCmd.ExecuteReader();
            while (sqlReader.Read())
            {
                emplist.Add(new Employee { empno = (int)sqlReader.GetValue(0), empname = (string)sqlReader.GetValue(1) });
            }
            sqlReader.Close();
            sqlCmd.Dispose();
            sqlCnn.Close();
        }
        public List<Employee> GetEmployees()
        {

            return emplist;

        }
        public Employee GetEmpById(int id)
        {
            var obj = emplist.FirstOrDefault(a => a.empno == id);
            return obj;
        }
        public bool DeleteEmp(int id)
        {
            bool success = false;
            sql = "Delete FROM [dbo].[employee] where empno= @empno;";

            sqlCnn = new SqlConnection(connetionString);

            sqlCnn.Open();
            sqlCmd = new SqlCommand(sql, sqlCnn);
            sqlCmd.Parameters.AddWithValue("@empno", id);
            sqlCmd.ExecuteNonQuery();
            sqlCmd.Dispose();
            sqlCnn.Close();
            success = true;
            return success;
        }
        public bool updateEmp(int id, string value)
        {
            bool success = false;
            sql = "Update [dbo].[employee] set empname= @value where empno= @empno;";

            sqlCnn = new SqlConnection(connetionString);

            sqlCnn.Open();
            sqlCmd = new SqlCommand(sql, sqlCnn);
            sqlCmd.Parameters.AddWithValue("@value", value);
            sqlCmd.Parameters.AddWithValue("@empno", id);

            sqlCmd.ExecuteNonQuery();
            sqlCmd.Dispose();
            sqlCnn.Close();

            success = true;
            return success;
        }
        public bool createEmp(Employee emp)
        {
            bool success = false;
            sql = "Insert into [dbo].[employee] values(@empno,@empname,@empsal);";

            sqlCnn = new SqlConnection(connetionString);

            sqlCnn.Open();
            sqlCmd = new SqlCommand(sql, sqlCnn);
            sqlCmd.Parameters.AddWithValue("@empname", emp.empname);
            sqlCmd.Parameters.AddWithValue("@empno", emp.empno);
            sqlCmd.Parameters.AddWithValue("@empsal", emp.empsal);

            sqlCmd.ExecuteNonQuery();
            sqlCmd.Dispose();
            sqlCnn.Close();

            success = true;
            return success;
        }
    }
}