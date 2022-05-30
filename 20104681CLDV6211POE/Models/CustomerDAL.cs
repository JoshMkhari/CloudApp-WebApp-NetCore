using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace _20104681CLDV6211POE.Models
{
    public class CustomerDAL
    {
        public string currentCustomerID;
        public string currentJobCardNo;
        public string currentEqID;
        public static int RateID;
        public static int EquuID;
        //String connectionStringDev = @"Data Source=DESKTOP-NA2OFQC\SQLEXPRESS;Initial Catalog=DomingoRoofs_20104681;Integrated Security=True";
        String connectionStringDev = "Server = tcp:serverdomingoroofworksnj.database.windows.net,1433;Initial Catalog = dbDomingoRoofWorks20104681; Persist Security Info=False;User ID = joshmkhari; Password=10171906Easy;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30;";
    public IEnumerable<CustomerData> GetAllCustomer() // will return a list off all customers
        {
            List<CustomerData> custList = new List<CustomerData>();
            using (SqlConnection con = new SqlConnection(connectionStringDev))
            {
                SqlCommand cmd = new SqlCommand("SP_GetAllCustomer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();//reading info from database
                while (dr.Read())
                {
                    //read each coloumn returned from Stored Procedure
                    CustomerData cus = new CustomerData();
                    cus.CustomerID = (dr["CUSTOMER_ID"].ToString());
                    cus.CustomerName = dr["NAME"].ToString();
                    cus.CustomerSurname = dr["SURNAME"].ToString();
                    cus.AddressLineOne = dr["ADDRESS_LINE_ONE"].ToString();
                    cus.AddressLineTwo = dr["ADDRESS_LINE_TWO"].ToString();
                    cus.City = dr["CITY"].ToString();
                    cus.PostalCode = dr["POSTAL_CODE"].ToString();
                    custList.Add(cus);
                }
                con.Close();
            }
            return custList;
        }

        public CustomerData GetCustomer(int? cusID)
        {
            CustomerData cus = new CustomerData();
            using (SqlConnection con = new SqlConnection(connectionStringDev))
            {
                SqlCommand cmd = new SqlCommand("SP_GetCustomer", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CUSTOMER_ID", cusID);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cus.CustomerID = (dr["CUSTOMER_ID"].ToString());
                    cus.CustomerName = dr["NAME"].ToString();
                    cus.CustomerSurname = dr["SURNAME"].ToString();
                    cus.AddressLineOne = dr["ADDRESS_LINE_ONE"].ToString();
                    cus.AddressLineTwo = dr["ADDRESS_LINE_TWO"].ToString();
                    cus.City = dr["CITY"].ToString();
                    cus.PostalCode = dr["POSTAL_CODE"].ToString();
                }
                con.Close();
            }
            return cus;
        }
        public void SetCurrentCustomer()
        {
            CustomerData cus = new CustomerData();
            using (SqlConnection con = new SqlConnection(connectionStringDev))
            {
                SqlCommand cmd = new SqlCommand("SP_GetCurrentCustomer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cus.CustomerID = (dr["CUSTOMER_ID"].ToString());
                }
                con.Close();
            }
            currentCustomerID = cus.CustomerID;
        }

        public void SetCurrentJobCard()
        {
            CustomerData cus = new CustomerData();
            using (SqlConnection con = new SqlConnection(connectionStringDev))
            {
                SqlCommand cmd = new SqlCommand("SP_GetCurrentJobCardNo", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cus.JobCardNo = (dr["JOB_CARD_NO"].ToString());
                }
                con.Close();
            }
            currentJobCardNo = cus.JobCardNo;
        }
        public void UpdateCustomer(CustomerData cus)
        {
            using (SqlConnection con = new SqlConnection(connectionStringDev))
            {
                SqlCommand cmd = new SqlCommand("SP_UpdateCustomer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CUSTOMER_ID", cus.CustomerID);
                cmd.Parameters.AddWithValue("@NAME", cus.CustomerName);
                cmd.Parameters.AddWithValue("@SURNAME", cus.CustomerSurname);
                cmd.Parameters.AddWithValue("@ADDRESS_LINE_ONE", cus.AddressLineOne);
                cmd.Parameters.AddWithValue("@ADDRESS_LINE_TWO", cus.AddressLineTwo);
                cmd.Parameters.AddWithValue("@CITY", cus.City);
                cmd.Parameters.AddWithValue("@POSTAL_CODE", cus.PostalCode);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
        }

        public void AddCustomer(CustomerData cus)
        {
            using (SqlConnection con = new SqlConnection(connectionStringDev))
            {
                SqlCommand cmd = new SqlCommand("SP_InsertCustomer", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@NAME", cus.CustomerName);
                cmd.Parameters.AddWithValue("@SURNAME", cus.CustomerSurname);
                cmd.Parameters.AddWithValue("@ADDRESS_LINE_ONE", cus.AddressLineOne);
                cmd.Parameters.AddWithValue("@ADDRESS_LINE_TWO", cus.AddressLineTwo);
                cmd.Parameters.AddWithValue("@CITY", cus.City);
                cmd.Parameters.AddWithValue("@POSTAL_CODE", cus.PostalCode);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void DeleteCustomer(String? custId)
        {
            using (SqlConnection con = new SqlConnection(connectionStringDev))
            {
                SqlCommand cmd = new SqlCommand("SP_DeleteCustomer", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CUSTOMER_ID", custId);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }


        public void AddJobCard(CustomerData cus,String id,bool customer)
        {
            if (customer)
                currentCustomerID = id;
            else
            SetCurrentCustomer();
              using (SqlConnection con = new SqlConnection(connectionStringDev))
            {
                SqlCommand cmd = new SqlCommand("SP_CreateJob", con);
                cmd.CommandType = CommandType.StoredProcedure;

                currentJobCardNo = cus.JobCardNo;
                cmd.Parameters.AddWithValue("@JOB_CARD_NO", cus.JobCardNo);
                cmd.Parameters.AddWithValue("@NO_OF_DAYS", cus.NoOfDays);
                cmd.Parameters.AddWithValue("@CUSTOMER_ID", currentCustomerID);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            //SetEquiptmentMaterialsID();
        }

        public void AddMaterials(CustomerData cus, int ID)
        {
             using (SqlConnection con = new SqlConnection(connectionStringDev))
            {
                SqlCommand cmd = new SqlCommand("SP_AddEqID", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@RateID", ID);
                cmd.Parameters.AddWithValue("@StandardFloorBoards", cus.StandardFloorBoards);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            SetCurrentEquiptmentID();
        }

        public void SetCurrentEquiptmentID()
        {
            CustomerData cus = new CustomerData();
            using (SqlConnection con = new SqlConnection(connectionStringDev))
            {
                SqlCommand cmd = new SqlCommand("SP_GetCurrentEquip", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cus.currentEquiptmenID = (dr["EQUIPTMENT_MATERIALS_ID"].ToString());
                }
                con.Close();
            }
            currentEqID = cus.currentEquiptmenID;
            EquuID = Convert.ToInt32(cus.currentEquiptmenID);
        }

        public void AddJobEquiptmentMaterials()
        {
            using (SqlConnection con = new SqlConnection(connectionStringDev))
            {
                SqlCommand cmd = new SqlCommand("SP_AddJobEMaterials", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SetCurrentJobCard();
                cmd.Parameters.AddWithValue("@EquiptmentMaterialsID", currentEqID);
                cmd.Parameters.AddWithValue("@JobCardNo", currentJobCardNo);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void AddEquiptment(CustomerData cus)
        {
            using (SqlConnection con = new SqlConnection(connectionStringDev))
            {
                SqlCommand cmd = new SqlCommand("SP_SelectMaterials", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SetCurrentJobCard();
                cmd.Parameters.AddWithValue("@EQUIPTMENT_MATERIALS_ID", currentEqID);
                cmd.Parameters.AddWithValue("@POWER_POINTS", cus.PowerPoints);
                cmd.Parameters.AddWithValue("@STANDARD_ELECTRICAL_WIRING", cus.StandardElectricalWiting);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
