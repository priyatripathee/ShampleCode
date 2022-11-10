using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntities;
using DAL;
using System.Data;


namespace BLL
{
    public class temp_cart_handler
    {
        private temp_cart_data tempcartData { get; set; }

        public temp_cart_handler()
        {
            tempcartData = new temp_cart_data();
        }

        public Int32 insert_temp_cart(Int32 temp_customer_id,Int64 product_id, Int32 quantity)
        {
            BusinessEntities.temp_cart cart = new temp_cart();
            cart.temp_customer_id = temp_customer_id;
            cart.product_id = product_id;
            cart.quantity = quantity;
            return tempcartData.insert_temp_cart(cart);
        }

        public Int32 insert_temp_customer(string email_id, string name, string contact_number, string address, string land_mark, string city, string state, string country, string pin_code, bool email_sent, string customer_medium)
        {
            temp_customer cust = new temp_customer();
            cust.email_id = email_id;
            cust.name = name;
            cust.contact_number = contact_number;
            cust.address = address;
            cust.land_mark = land_mark;
            cust.city = city;
            cust.state = state;
            cust.country = country;
            cust.pin_code = pin_code;
            cust.email_sent = email_sent;
            cust.customer_medium = customer_medium;
            return tempcartData.insert_temp_customer(cust);
        }

        public Int32 update_temp_customer(string email_id, string name, string contact_number, string address, string land_mark, string city, string state, string country, string pin_code)
        {
            temp_customer cust = new temp_customer();
            cust.email_id = email_id;
            cust.contact_number = contact_number;
            cust.name = name;
            cust.address = address;
            cust.land_mark = land_mark;
            cust.city = city;
            cust.state = state;
            cust.country = country;
            cust.pin_code = pin_code;
            return tempcartData.update_temp_customer(cust);
        }

        public bool delete_temp_cart_customer(string email_id)
        {
            return tempcartData.delete_temp_cart_customer(email_id);
        }

        public DataSet get_temp_customer(DateTime? from_date, DateTime? to_date, bool? email_sent)
        {
            return tempcartData.get_temp_customer(from_date, to_date, email_sent);
        }

        public DataSet get_temp_customer_cart(string email_id)
        {
            return tempcartData.get_temp_customer_cart(email_id);
        }
    }
}
