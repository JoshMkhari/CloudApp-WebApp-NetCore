using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace _20104681CLDV6211POE.Models
{
    public class EmployeeDAL
    {
        //String connectionStringDev = @"Data Source=DESKTOP-NA2OFQC\SQLEXPRESS;Initial Catalog=DomingoRoofs_20104681;Integrated Security=True";
        String connectionStringDev = "Server = tcp:serverdomingoroofworksnj.database.windows.net,1433;Initial Catalog = dbDomingoRoofWorks20104681; Persist Security Info=False;User ID = joshmkhari; Password=10171906Easy;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30;";

        //Get All Employees Stored Procedure
        public IEnumerable<EmployeeData> GetAllEmployees() // will return a list off all employees
        {
            List<EmployeeData> empList = new List<EmployeeData>();
            using (SqlConnection con = new SqlConnection(connectionStringDev))
            {
                SqlCommand cmd = new SqlCommand("SP_GetAllEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();//reading info from database
                while (dr.Read())
                {
                    //read each coloumn returned from Stored Procedure
                    EmployeeData emp = new EmployeeData();
                    emp.EmployeeNo = dr["EMPLOYEE_NO"].ToString();
                    emp.EmployeeName = dr["NAME"].ToString();
                    emp.EmployeeSurname = dr["SURNAME"].ToString();

                    empList.Add(emp);
                }
                con.Close();
            }
            return empList;
        }

        //Insert EMployees Procedure
        public void AddEmployee(EmployeeData emp)
        {
            using (SqlConnection con = new SqlConnection(connectionStringDev))
            {
                SqlCommand cmd = new SqlCommand("SP_InsertEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EMPLOYEE_NO", emp.EmployeeNo);
                cmd.Parameters.AddWithValue("@NAME", emp.EmployeeName);
                cmd.Parameters.AddWithValue("@SURNAME", emp.EmployeeSurname);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public EmployeeData GetEmployee(string? empId)
        {
            EmployeeData emp = new EmployeeData();
            using (SqlConnection con = new SqlConnection(connectionStringDev))
            {
                SqlCommand cmd = new SqlCommand("SP_GetEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EMPLOYEE_NO", empId);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    emp.EmployeeNo = dr["EMPLOYEE_NO"].ToString();
                    emp.EmployeeName = dr["NAME"].ToString();
                    emp.EmployeeSurname = dr["SURNAME"].ToString();
                }
                con.Close();
            }
            return emp;
        }

        //Update Employees Procedure
        public void UpdateEmployees(EmployeeData emp)
        {
            using (SqlConnection con = new SqlConnection(connectionStringDev))
            {
                SqlCommand cmd = new SqlCommand("SP_UpdateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EMPLOYEE_NO", emp.EmployeeNo);
                cmd.Parameters.AddWithValue("@NAME", emp.EmployeeName);
                cmd.Parameters.AddWithValue("@SURNAME", emp.EmployeeSurname);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
        }

    }
}
