using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using BusinessEntities;
using System.Data;

namespace BLL
{
    public class city_handler
    {
        private city_data cityData { get; set; }
        public city_handler()
        {
            cityData = new city_data();
        }

       
        public Int32 insert_update_city(Int64 city_id, Int64 state_id, string city_name, bool is_active)
        {
            return cityData.insert_update_city(city_id, state_id, city_name,is_active);
        }

        public DataSet get_city(Int64? city_id)
        {
            return cityData.get_city(city_id);
        }
    }
}
