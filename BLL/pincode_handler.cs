using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;

namespace BLL
{
    public class pincode_handler
    {
        pincode_data objPinCode = null;

        public pincode_handler()
        {
            objPinCode = new pincode_data();
        }

        public DataSet get_pincode_search(string State, string CityName, string PinCode, int flag)
        {
            return objPinCode.get_pincode_search(State, CityName, PinCode, flag);
        }

        public bool check_Pincode_COD(string Pincode)
        {
            return objPinCode.check_Pincode_COD(Pincode);
        }
    }
}
