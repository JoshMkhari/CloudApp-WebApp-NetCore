using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace _20104681CLDV6211POE.Models
{
    public class RateDAL
    {
        //String connectionStringDev = @"Data Source=DESKTOP-NA2OFQC\SQLEXPRESS;Initial Catalog=DomingoRoofs_20104681;Integrated Security=True";
        String connectionStringDev = "Server = tcp:serverdomingoroofworksnj.database.windows.net,1433;Initial Catalog = dbDomingoRoofWorks20104681; Persist Security Info=False;User ID = joshmkhari; Password=10171906Easy;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30;";
        public IEnumerable<RateData> GetRates() // will return a list off all employees
        {
            List<RateData> rateList = new List<RateData>();
            using (SqlConnection con = new SqlConnection(connectionStringDev))
            {
                SqlCommand cmd = new SqlCommand("SP_GetRates", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();//reading info from database
                while (dr.Read())
                {
                    //read each coloumn returned from Stored Procedure
                    RateData ra = new RateData();
                    ra.Rate = Convert.ToInt32( dr["RATE"].ToString());
                    ra.JobType = dr["JOB_TYPE"].ToString();

                    rateList.Add(ra);
                }
                con.Close();
            }
            return rateList;
        }


        public RateData GetRate(string? JobType)
        {
            RateData ra = new RateData();
            using (SqlConnection con = new SqlConnection(connectionStringDev))
            {
                SqlCommand cmd = new SqlCommand("SP_GetRate", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@JobType", JobType);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ra.Rate = Convert.ToInt32(dr["RATE"].ToString());

                }
                con.Close();
            }
            return ra;
        }

        public void UpdateRate(RateData ra, String JobType)
        {
            using (SqlConnection con = new SqlConnection(connectionStringDev))
            {
                SqlCommand cmd = new SqlCommand("SP_UpdateRate", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@JobType", JobType);
                cmd.Parameters.AddWithValue("@Rate", ra.Rate);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
        }

    }

}
