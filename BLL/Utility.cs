using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BLL
{
    public class Utility
    {
        static string ordPrifix = "STR10001";
        public static bool change_update_status(int id, bool status, string col, string key, string tblName)
        {
            return DAL.Utility.change_update_status(id, status, col, key, tblName);
        }

        public static string generatecode()
        {
            return DAL.Utility.generatecode();
        }


        public static string generatecouponcode()
        {
            return DAL.Utility.generatecouponcode();
        }
        public static string SendSms(string Number, String Message)
        {
            return DAL.Utility.SendSms(Number, Message);
        }
        //public static string SendUpdateWishListItem()
        //{
        //    return DAL.Utility.SendUpdateWishListItem();
        //}
        //public static void SendSmsSingup(string Number, String Username)
        //{
        //    string Message = "Happiness " + Username + " Thanks for signing up . As a return gift from us use FLAT 20 and avail 20% discount on your first buy.";
        //    DAL.Utility.SendSms(Number, Message);
        //}

        public static void SendSmsCodOtpSend(string Mobile, string Username,string OTP)
        {
            DAL.Utility.SendSms(Mobile, string.Format("Hi {0}, Your OTP for STRUTT Order is {1}. For any queries mail us at connect@thestruttstore.com"
                , Username, OTP));
        }

       
        public static void SendSmsCODConfirmation(string Mobile, string Username, string OrderNumber, string Amount)
        {
            OrderNumber = ordPrifix + OrderNumber;
            DAL.Utility.SendSms(Mobile, string.Format("Hi {0}, Your order No. {1} from Strutt is CONFIRMED. Will get delivered in 7-8 days. Please keep Rs.{2} handy at the time of delivery. For any queries mail us at connect@thestruttstore.com"
                , Username, OrderNumber, Amount));
           // DAL.Utility.SendSms(Mobile, "Hi Kalpesh Patel, Your order No. STRUTT10001123456 from Strutt is CONFIRMED. Will get delivered in 5-9 days. Please keep Rs.5000.00 handy at the time of delivery. For any queries mail us at connect@thestruttstore.com");
        }

        public static void SendSmsOnlinePaymentConfirmation(string Mobile, string Username, string OrderNumber)
        {
            OrderNumber = ordPrifix + OrderNumber;
            DAL.Utility.SendSms(Mobile, string.Format("Hi {0}, Your order No. {1} from Strutt is CONFIRMED. Will get delivered in 7-8 days.We have received the payment for your order . For any queries mail us at connect@thestruttstore.com"
                , Username, OrderNumber));
        }

        public static void SendSmsOrderDispatch(string Mobile, string Username, string OrderNumber, string TrackingId, string CourierName)
        {
            OrderNumber = ordPrifix + OrderNumber;
            DAL.Utility.SendSms(Mobile, string.Format("Hi {0}, Your package with order No. {1} has been dispatched and will be delivered in 3-7 days with tracking ID {2}, by {3}. For any query please mail us at connect@thestruttstore.com"
                , Username, OrderNumber, TrackingId, CourierName));
        }

        public static void SendSmsOrderCancel(string Mobile, string ProductName, string OrderNumber)
        {
            OrderNumber = ordPrifix + OrderNumber;
            DAL.Utility.SendSms(Mobile, string.Format("Hi {0},in your order with order ID {1} has been cancelled. Please check your email for more details."
                , ProductName, OrderNumber));
        }
        public static void SendSmsOrderConfirmation(string Mobile, string Username, string OrderNumber)
        {
            OrderNumber = ordPrifix + OrderNumber;
            DAL.Utility.SendSms(Mobile, string.Format("Hi {0}, Your order No. {1} from Strutt has been Delivered. Hope you loved the bag !!Looking forward to styling you again!"
                , Username, OrderNumber));
        }
    }
}
