using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using BusinessEntities;
using System.Data;

namespace BLL
{
    public class customer_handler
    {
        private customer_details_data objcustomerData { get; set; }
        public customer_handler()
        {
            objcustomerData = new customer_details_data();
        }

        public bool insert_update_customer_login_details(customer Customer, ref Guid customer_id)
        {
            return objcustomerData.insert_update_customer_login_details(Customer, ref customer_id);
        }

        public int get_customer_login(customer CustomerLogin)
        {
            return objcustomerData.get_customer_login(CustomerLogin);
        }

        public DataSet check_customer_loginid(string email_id)
        {
            return objcustomerData.check_customer_loginid(email_id);
        }

        public DataSet check_guest_loginid(string email_id)
        {
            return objcustomerData.check_guest_loginid(email_id);
        }
        public bool insert_update_guest_customer(BusinessEntities.customer GuestCustomer, ref Guid customer_id)
        {
            return objcustomerData.insert_update_guest_customer(GuestCustomer, ref customer_id);
        }
        public bool insert_update_facebook_customer(BusinessEntities.customer GuestCustomer, ref Guid customer_id)
        {
            return objcustomerData.insert_update_facebook_customer(GuestCustomer, ref customer_id);
        }
        public bool insert_update_google_customer(BusinessEntities.customer GuestCustomer, ref Guid customer_id)
        {
            return objcustomerData.insert_update_google_customer(GuestCustomer, ref customer_id);
        }

        public bool delete_customer_address(Int64 customer_details_id)
        {
            return objcustomerData.delete_customer_address(customer_details_id);
        }

        public bool insert_customer_details(customer Customer)
        {
            return objcustomerData.insert_customer_details(Customer);
        }

        public int check_customer_emailid(customer _ForgetPassword)
        {
            return objcustomerData.check_customer_emailid(_ForgetPassword);
        }

        public long change_user_password(customer _change)
        {
            return objcustomerData.change_user_password(_change);
        }

        public DataSet get_customer_address(Guid customer_id)
        {
            return objcustomerData.get_customer_address(customer_id);
        }
        public DataSet get_customer_login_details(string email_id)
        {
            return objcustomerData.get_customer_login_details(email_id);
        }

        public bool update_guest_customer(BusinessEntities.customer GuestCustomer, ref Guid customer_id)
        {
            return objcustomerData.update_guest_customer(GuestCustomer, ref customer_id);
        }
        //public DataSet get_customer_detail_all(DateTime? FromDate, DateTime? ToDate, string order_status, string email_id)
        //{
        //    return objcustomerData.get_customer_detail_all(FromDate, ToDate, order_status,email_id);
        //}
        public DataSet get_customer_detail_all(long product_id)
        {
            return objcustomerData.get_customer_detail_all(product_id);
        }

        //Start: Added by Hetal Patel for XpressLane Users
        public DataSet check_isexistxpresslanecustomer(string emailID)
        {
            return objcustomerData.check_isexistxpresslanecustomer(emailID);
        }
        public bool insert_update_Xprsslane_customer(Guid customer_id, string email_id)
        {
            return objcustomerData.insert_update_Xprsslane_customer(customer_id, email_id);
        }
        //End: Added by Hetal Patel for XpressLane Users
    }
}
