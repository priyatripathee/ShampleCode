using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using BusinessEntities;
using System.Data;

namespace BLL
{
   public class receiver_address_handler
    {
       private receiver_address_data objreceiverAddressData { get; set; }

       public receiver_address_handler()
       {
           objreceiverAddressData = new receiver_address_data();
       }
       public bool insert_customer_address(receiver_address receiverAddress)
       {
           return objreceiverAddressData.insert_customer_address(receiverAddress);
       }
       public DataSet get_customer_address(Guid customer_id)
       {
           return objreceiverAddressData.get_customer_address(customer_id);
       }
       public DataSet delete_save_customer_address(long customer_details_id)
       {
           return objreceiverAddressData.delete_save_customer_address(customer_details_id);
       }
       public DataSet get_save_customer_address(long customer_details_id)
       {
           return objreceiverAddressData.get_save_customer_address(customer_details_id);
       }
    }
}
