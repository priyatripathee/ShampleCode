using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace DAL
{
    public class pincode_data
    {
       

        public DataSet get_pincode_search(string State, string CityName, string PinCode, int flag)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {
			    new SqlParameter("@state", State),
                new SqlParameter("@city_name", CityName),
                new SqlParameter("@pin_code", PinCode),
                new SqlParameter("@Flag", flag)
		    };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_pin_code_search", parameters);
            return ds;
        }

        public bool check_Pincode_COD(string Pincode)
        {
            SqlConnection con = new SqlConnection(Connection.ConnstruttDB);
            SqlCommand cmd = new SqlCommand("pr_check_Pincode_COD", con);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd.Parameters.AddWithValue("@pincode", Pincode);
                SqlParameter retPram = new SqlParameter("@ReturnValue", SqlDbType.Bit);
                retPram.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retPram);
                con.Open();
                cmd.ExecuteNonQuery();
                bool resultValue = Convert.ToBoolean(retPram.Value);
                con.Close();
                return resultValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


    }
}
