using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PickerPinModel
{
    public class PickerpincodeModel
    {
        //Request Param
        public string from_pincode { get; set; }
        public string to_pincode { get; set; }
        public string auth_token { get; set; }

        
        //Response Param
        public bool has_prepaid { get; set; }
        public bool has_cod { get; set; }
        public decimal cod_limit { get; set; }
        public bool has_pickup { get; set; }

        public string to_city { get; set; }

        public string to_state { get; set; }

    }
}