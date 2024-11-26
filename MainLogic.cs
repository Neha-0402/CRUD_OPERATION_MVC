using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using NehaFeedBackForm.Models;
using System.Xml.Linq;
namespace NehaFeedBackForm
{
    public class MainLogic
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["FeedbackNeha"].ToString());
        public void InsertFeedback(InserFeebacks insertFeedback)
        {

            SqlCommand cmd = new SqlCommand("[dbo].[InsertCustomerDetails]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@HName", insertFeedback.HName);
            cmd.Parameters.AddWithValue("@CountryID", insertFeedback.CountryID);
            cmd.Parameters.AddWithValue("@EmailID", insertFeedback.EmailID);
            cmd.Parameters.AddWithValue("@MobileNumber", insertFeedback.MobileNumber);
            cmd.Parameters.AddWithValue("@Remarks", insertFeedback.Remarks);
            cmd.Parameters.AddWithValue("@Rating", insertFeedback.Rating);
            cmd.Parameters.AddWithValue("@CreatedDate", insertFeedback.CreatedDate);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

        }

        //For index page

        public List<ListOfHotelDetailsM> GetAllHotelDetails()
        {
            List<ListOfHotelDetailsM> FeedBackList = new List<ListOfHotelDetailsM>();
            SqlCommand cmd = new SqlCommand("[dbo].[ListoFCustomer]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            conn.Open();
            adp.Fill(dt);
            conn.Close();
            foreach (DataRow dr in dt.Rows)
            {
                FeedBackList.Add(new ListOfHotelDetailsM
                {
                    HotelId = Convert.ToInt32(dr["HotelId"]),
                    HName = Convert.ToString(dr["HName"]),
                    countryID = Convert.ToInt32(dr["countryID"]),
                    EmailID = Convert.ToString(dr["EmailID"]),
                    MobileNumber = Convert.ToString(dr["MobileNumber"]),
                    Remarks = Convert.ToString(dr["Remarks"]),
                    Rating = Convert.ToString(dr["Rating"]),
                    CreatedDate = Convert.ToDateTime(dr["CreatedDate"]),


                });
            }
            return FeedBackList;
        }



        //for update data
        public void UpdateFeedback(UpdateFeedbackM updateModel)
        {
            SqlCommand cmd = new SqlCommand("[dbo].[UpdateCustomerData]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@HotelId", updateModel.HotelId);
            cmd.Parameters.AddWithValue("@HName", updateModel.HName);
            cmd.Parameters.AddWithValue("@CountryID", updateModel.CountryID);
            cmd.Parameters.AddWithValue("@EmailID", updateModel.EmailID);
            cmd.Parameters.AddWithValue("@MobileNumber", updateModel.MobileNumber);
            cmd.Parameters.AddWithValue("@Remarks", updateModel.Remarks);
            cmd.Parameters.AddWithValue("@Rating", updateModel.Rating);
            cmd.Parameters.AddWithValue("@CreatedDate", updateModel.CreatedDate);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

        }
        //For delet

        public void DeleteFeedback(int id)
        {
            SqlCommand cmd = new SqlCommand("[dbo].[DeleteCustomerHotelData]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@HotelId", id);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();


        }




    }
}