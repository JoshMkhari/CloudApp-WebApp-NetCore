using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace _20104681CLDV6211POE.Models
{
    public class JobCardDAL
    {
        //String connectionStringDev = @"Data Source=DESKTOP-NA2OFQC\SQLEXPRESS;Initial Catalog=DomingoRoofs_20104681;Integrated Security=True";
        String connectionStringDev = "Server = tcp:serverdomingoroofworksnj.database.windows.net,1433;Initial Catalog = dbDomingoRoofWorks20104681; Persist Security Info=False;User ID = joshmkhari; Password=10171906Easy;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30;";
        String EmployeeAssigned;
        bool EquipId = false;
        bool jobID = false;
        String EquiptId;
        public static String JobsID;

        public IEnumerable<JobCardData> GetAllJobCards() // will return a list off all employees
        {
            List<JobCardData> jobCardList = new List<JobCardData>();
            using (SqlConnection con = new SqlConnection(connectionStringDev))
            {
                SqlCommand cmd = new SqlCommand("SP_GetAllJobCards", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();//reading info from database
                while (dr.Read())
                {
                    //read each coloumn returned from Stored Procedure
                    JobCardData job = new JobCardData();
                    job.JobCardNo = dr["JOB_CARD_NO"].ToString();
                    job.NoOfDays = dr["NO_OF_DAYS"].ToString();
                    job.EmployeesAssigned= dr["Employees Assigned"].ToString();

                    jobCardList.Add(job);
                }
                con.Close();
            }
            return jobCardList;
        }

        public void AddEmployeeToJob(String empID)
        {
          using (SqlConnection con = new SqlConnection(connectionStringDev))
            {
                SqlCommand cmd = new SqlCommand("SP_AddEmployeeToJobCard", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@JobCard", JobsID);
                cmd.Parameters.AddWithValue("@EmployeeNo",empID);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            //SetEquiptmentMaterialsID();
        }

        public void GetAllEmployees(string jobCardNo) // will return a list off all employees
        {
            EmployeeAssigned = "";
            List<String> EmployeesList = new List<string>();
            using (SqlConnection con = new SqlConnection(connectionStringDev))
            {
                SqlCommand cmd = new SqlCommand("SP_EmployeesWorkingOnJob", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@JobCardNo", jobCardNo);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();//reading info from database
                while (dr.Read())
                {
                    //read each coloumn returned from Stored Procedure
                    JobCardData job = new JobCardData();
                    job.EmployeeNo = dr["EmpNo"].ToString();
                    job.EmployeeName = dr["Employee Name"].ToString();
                    job.EmployeeSurname = dr["Employee Surname"].ToString();

                    EmployeesList.Add(job.EmployeeNo + " " + job.EmployeeName + " " + job.EmployeeSurname);
                }
                con.Close();
            }
            for(int i =0; i<EmployeesList.Count;i++)
            {
                EmployeeAssigned = EmployeeAssigned + EmployeesList[i] + ", ";
            }
        }


        public String Rate(String jobCardNo) // will return a list off all employees
        {
            List<JobCardData> jobCardList = new List<JobCardData>();
            String Rate;
            JobCardData job1 = new JobCardData();
            using (SqlConnection con = new SqlConnection(connectionStringDev))
            {
                SqlCommand cmd = new SqlCommand("SP_GetRateForJobCard", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@JobCardNo", jobCardNo);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    job1.JobType = dr["JobType"].ToString();
                }
                con.Close();
                Rate = job1.JobType;
            }
            return Rate;
        }
        public IEnumerable<JobCardData> GetInvoiceFloorBoarding(string jobCardNo, string JobType)
        {
            List<JobCardData> jobCardList = new List<JobCardData>();
            JobCardData job = new JobCardData();
            switch (JobType)
                {
                    case "1":
                    GetAllEmployees(jobCardNo);

                    using (SqlConnection con = new SqlConnection(connectionStringDev))
                    {
                        SqlCommand cmd = new SqlCommand("SP_GetInvoiceFloorBoarding", con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@JobCardNo", jobCardNo);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            job.JobCardNo = dr["JOB_CARD_NO"].ToString();
                            job.NoOfDays = dr["NO_OF_DAYS"].ToString();
                            job.EmployeesAssigned = EmployeeAssigned;
                            job.CustomerName = dr["Customer Name"].ToString();
                            job.CustomerSurname = dr["Customer Name"].ToString();
                            job.AddressLineOne = dr["Add One"].ToString();
                            job.AddressLineTwo = dr["Add two"].ToString();
                            job.City = dr["city"].ToString();
                            job.PostalCode = dr["code"].ToString();
                            job.JobType = dr["Job Type"].ToString();
                            job.Rate = dr["Rate"].ToString();
                            job.SubTotal = dr["Subtotal"].ToString();
                            job.Vat = dr["VAT"].ToString();
                            job.Total = dr["Total:"].ToString();
                            job.FloorBoards = dr["FloorBoards"].ToString();
                            if (job.AddressLineTwo.Length == 0)
                            {
                                job.AddressLine = job.AddressLineOne;
                            }
                            else
                                job.AddressLine = job.AddressLineOne + ", " + job.AddressLineTwo;
                            jobCardList.Add(job);
                        }
                        con.Close();
                    }
                    break;
                        //case "2":
                        //    GetSemiConversion(id)
                }

                return jobCardList;
        }

        public IEnumerable<JobCardData> GetSemi(string jobCardNo, string JobType)
        {
            List<JobCardData> jobCardList = new List<JobCardData>();
            JobCardData job = new JobCardData();
                    GetAllEmployees(jobCardNo);

                    using (SqlConnection con = new SqlConnection(connectionStringDev))
                    {
                        SqlCommand cmd = new SqlCommand("SP_GetInvoiceSemi", con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@JobCardNo", jobCardNo);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            job.JobCardNo = dr["JOB_CARD_NO"].ToString();
                            job.NoOfDays = dr["NO_OF_DAYS"].ToString();
                            job.EmployeesAssigned = EmployeeAssigned;
                            job.CustomerName = dr["Customer Name"].ToString();
                            job.CustomerSurname = dr["Customer Name"].ToString();
                            job.AddressLineOne = dr["Add One"].ToString();
                            job.AddressLineTwo = dr["Add two"].ToString();
                            job.City = dr["city"].ToString();
                            job.PostalCode = dr["code"].ToString();
                            job.JobType = dr["Job Type"].ToString();
                            job.Rate = dr["Rate"].ToString();
                            job.SubTotal = dr["Subtotal"].ToString();
                            job.Vat = dr["VAT"].ToString();
                            job.Total = dr["Total:"].ToString();
                            if (JobType.Equals("1"))
                            {
                                job.FloorBoards = "0";
                                job.PowerPoints = "0";
                                job.Sew = "0";
                            }
                            else
                            {
                                job.FloorBoards = dr["FloorBoards"].ToString();
                                job.PowerPoints = dr["PowerPoints"].ToString();
                                job.Sew = dr["SEW"].ToString();
                            }

                            if (JobType.Equals("3"))
                                job.Stairs = "1";
                            else
                                job.Stairs = "0";
                            if (job.AddressLineTwo.Length == 0)
                            {
                                job.AddressLine = job.AddressLineOne;
                            }
                            else
                                job.AddressLine = job.AddressLineOne + ", " + job.AddressLineTwo;
                            jobCardList.Add(job);
                        }
                        con.Close();
            }

            return jobCardList;
        }

        public void DeleteJobCard(String? jobCardNo)
        {
            if (EquipId)
            {
                using (SqlConnection con = new SqlConnection(connectionStringDev))
                {
                    SqlCommand cmd = new SqlCommand("SP_DeleteEquiptmentJob", con); //job equiptment materials
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@JobCardNo", JobsID);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            if (jobID)
            {
                using (SqlConnection con = new SqlConnection(connectionStringDev))
                {
                    SqlCommand cmd = new SqlCommand("SP_DeleteEmployeeJob", con);//employee job
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@JobCardNo", JobsID);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            using (SqlConnection con = new SqlConnection(connectionStringDev))
            {
                SqlCommand cmd = new SqlCommand("SP_DeleteJobCard", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@JobCardNo", JobsID);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void CheckJobCard(String? jobCardNo)
        {

            using (SqlConnection con = new SqlConnection(connectionStringDev))
            {
                SqlCommand cmd = new SqlCommand("SP_CheckJobCard", con);
                cmd.CommandType = CommandType.StoredProcedure;
                JobCardData job = new JobCardData();
                cmd.Parameters.AddWithValue("@JobCardNo", jobCardNo);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    job.JobCardNo = dr["JOB_CARD_NO"].ToString();
                    job.EquptmentID = dr["EquipID"].ToString();
                    if (job.JobCardNo.Length != 0)
                    {
                        jobID = true;
                        JobsID = job.JobCardNo;
                    }
                    if (job.EquptmentID.Length !=0)
                    {
                        EquipId = true;
                        EquiptId = job.EquptmentID;
                    }
                }
                con.Close();
            }



        }
        public IEnumerable<JobCardData> GetAllEmployeesT(String jobCardNo) // will return a list off all employees
        {
            List<JobCardData> empList = new List<JobCardData>();
            using (SqlConnection con = new SqlConnection(connectionStringDev))
            {
                SqlCommand cmd = new SqlCommand("SP_GetAllEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();//reading info from database
                while (dr.Read())
                {
                    //read each coloumn returned from Stored Procedure
                    JobCardData job = new JobCardData();
                    job.EmployeeNo = dr["EMPLOYEE_NO"].ToString();
                    job.EmployeeName = dr["NAME"].ToString();
                    job.EmployeeSurname = dr["SURNAME"].ToString();

                    empList.Add(job);
                }
                con.Close();
            }
            return empList;
        }
    }
}
