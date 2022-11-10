using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;

namespace BLL
{
    public class order_handler
    {
        private order_data objOrderData { get; set; }

        public order_handler()
        {
            objOrderData = new order_data();
        }

        public long insert_order(BusinessEntities.order _order)
        {
            return objOrderData.insert_order(_order);
        }
        public long insert_order_customtag(Int64 custom_id, Int64 product_id, Int64 order_id, string letter, string style, string color, float price)
        {
            return objOrderData.insert_order_customtag(custom_id, product_id, order_id, letter, style, color, price);
        }
        public bool insert_order_detail(BusinessEntities.order _orderDetails)
        {
            return objOrderData.insert_order_detail(_orderDetails);
        }
        
        public bool update_product_quantity(BusinessEntities.order _orderDetails)
        {
            return objOrderData.update_product_quantity(_orderDetails);
        }
        public bool update_order_customer(BusinessEntities.order orderDetails)
        {
            return objOrderData.update_order_customer(orderDetails);
        }
        public DataSet get_order_status(DateTime? FromDate, DateTime? ToDate, string contact_number, string user_name,string order_status, string payment_status, string payment_status2, Nullable<Guid> customer_id, int flag,bool is_active)
        {
            return objOrderData.get_order_status(FromDate, ToDate, contact_number, user_name, order_status,payment_status , payment_status2,  customer_id, flag, is_active);
        }
        public DataSet get_order_export(DateTime? FromDate, DateTime? ToDate, string contact_number, string user_name, string order_status, string payment_status, string payment_status2, Nullable<Guid> customer_id, bool is_active)
        {
            return objOrderData.get_order_export(FromDate, ToDate, contact_number, user_name, order_status, payment_status, payment_status2, customer_id, is_active);
        }
        public DataSet get_order_return(string email_id, int flag)
        {
            return objOrderData.get_order_return(email_id, flag);
        }
        public DataSet chk_CouonCodeByEmail(string email_id, string coupon_code)
        {
            return objOrderData.chk_CouonCodeByEmail(email_id, coupon_code);
        }

        public DataSet get_order_customtag(Int64 order_detail_id)
        {
            return objOrderData.get_order_customtag(order_detail_id);
        }

        public DataSet get_preview_order_custom(Int64 order_detail_id)
        {
            return objOrderData.get_preview_order_custom(order_detail_id);
        }
        public DataSet get_order_search(Int64 order_id,Int64? order_detail_id)
        {
            return objOrderData.get_order_search(order_id, order_detail_id);
        }
        public DataSet get_order_search_order(Int64 order_id)
        {
            return objOrderData.get_order_search_order(order_id);
        }

        public DataSet get_order_search_orderdetail(Int64 order_id)
        {
            return objOrderData.get_order_search_orderdetail(order_id);
        }

        public bool update_order_PaymentType(Int64 order_id, string payment_type )
        {
            return objOrderData.update_order_PaymentType(order_id, payment_type);
        }

        public bool pr_delete_order(Int32 order_id)
        {
            return objOrderData.delete_order(order_id);
        }
        public bool update_order_status(BusinessEntities.order orderDetails)
        {
            return objOrderData.update_order_status(orderDetails);
        }
        public DataSet get_order_status_track(Int64 order_id, int flag)
        {
            return objOrderData.get_order_status_track(order_id, flag);
        }
        public bool update_cancel_product(BusinessEntities.order orderDetails)
        {
            return objOrderData.update_cancel_product(orderDetails);
        }
        public bool insert_raise_complain(BusinessEntities.order orderDetails)
        {
            return objOrderData.insert_raise_complain(orderDetails);
        }
        public bool insert_LeaveFeedback(BusinessEntities.order orderDetails)
        {
            return objOrderData.insert_LeaveFeedback(orderDetails);
        }
        public DataSet get_temp_order(Int64? order_id, DateTime? FromDate, DateTime? ToDate)
        {
            return objOrderData.get_temp_order(order_id,FromDate,ToDate);
        }
        public DataSet get_temp_partialorder(Int64? order_id)
        {
            return objOrderData.get_temp_partialorder(order_id);
        }

        public DataSet get_abandonedcart()
        {
            return objOrderData.get_abandonedcart();
        }

        public bool update_temp_customer_email_count(Int64? order_id,Int64? EmailCount)
        {
            return objOrderData.update_temp_customer_email_count(order_id, EmailCount);
        }

       
    }
}