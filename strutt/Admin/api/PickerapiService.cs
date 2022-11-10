using Newtonsoft.Json;
using PickerPinModel;
using PickerOrdModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.Script.Serialization;
using BLL;
using System.Data;
using BusinessEntities;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Security;


namespace api
{

    public class PickerapiService
    {
        #region Declaration

        protected string authtoken = ConfigurationManager.AppSettings["authtoken"].ToString();
        protected string pincodeserviceurl = ConfigurationManager.AppSettings["pincodeserviceurl"].ToString();
        protected string placeorderurl = ConfigurationManager.AppSettings["placeorderurl"].ToString();
        protected string downloadlablelurl = ConfigurationManager.AppSettings["downloadlablelurl"].ToString();
        protected int TotalQty = 0;
        decimal PayableAmt = 0;

        protected string frompincode, fromname, fromphone, fromaddress, fromemail, pickeupGST;
        admin_data_handler companyhandler = new admin_data_handler();
        PickerpincodeModel PickerPin = new PickerpincodeModel();
        PickerOrderModel PickerOrder = new PickerOrderModel();
        order Order = new order();
        order_handler orderHandler = new order_handler();


        #endregion

        #region Service - Checking Pickrr Pincode Serviceability

        public bool checkpincode_api(string ToPincode)
        {
            PickerPin.from_pincode = frompincode;
            PickerPin.to_pincode = ToPincode;
            PickerPin.auth_token = authtoken;
            pincodeserviceurl = pincodeserviceurl + "?from_pincode=" + PickerPin.from_pincode + "&to_pincode=" + PickerPin.to_pincode + "&auth_token=" + PickerPin.auth_token;

            
            HttpWebRequest request = System.Net.WebRequest.Create(pincodeserviceurl) as HttpWebRequest;
            request.Method = "GET";
            request.ContentType = "application/json; charset=utf-8";


            HttpWebResponse httpWebResponse = request.GetResponse() as HttpWebResponse;

            Stream stream = httpWebResponse.GetResponseStream();

            DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(PickerpincodeModel));
            PickerpincodeModel Response = (PickerpincodeModel)dataContractJsonSerializer.ReadObject(stream);

            PickerPin.has_pickup = Response.has_pickup;
            if (PickerPin.has_pickup == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Order - Place Order on Pickrr
        public bool placeorder_api(int Orderid, string productname, string custname, string custemail, string custnumber, string custpincode, string custaddress)
        {
            DataTable dtcompany = new DataTable();
            dtcompany = companyhandler.get_companydetails(1);
            if (dtcompany != null && dtcompany.Rows.Count > 0)
            {
                frompincode = dtcompany.Rows[0]["Pincode"].ToString();
                fromname = dtcompany.Rows[0]["CompanyName"].ToString();
                fromphone = dtcompany.Rows[0]["Phone1"].ToString();
                fromaddress = dtcompany.Rows[0]["Address1"].ToString();
                fromemail = dtcompany.Rows[0]["Email"].ToString();
                pickeupGST = dtcompany.Rows[0]["GST"].ToString();
            }

            PickerOrder.auth_token = authtoken;
            PickerOrder.item_name = productname;
            PickerOrderModel.item_list item;

            PickerOrder.Item_List = new List<PickerOrderModel.item_list>();

            DataSet dsOrdDet = new DataSet();
            dsOrdDet = orderHandler.get_order_search_orderdetail(Convert.ToInt32(Orderid));
            if (dsOrdDet != null && dsOrdDet.Tables.Count > 0)
            {
                DataTable dtOrdDet = dsOrdDet.Tables[0];
                for (int i = 0; i < dtOrdDet.Rows.Count; i++)
                {
                    item = new PickerOrderModel.item_list();
                    item.price = Convert.ToDecimal(dtOrdDet.Rows[i]["total_price"]);
                    item.item_name = dtOrdDet.Rows[i]["product_name"].ToString();
                    item.quantity = Convert.ToInt32(dtOrdDet.Rows[i]["quantity"]);
                    item.sku = "N/A";
                    item.item_tax_percentage = 0;
                    PickerOrder.Item_List.Add(item);
                    PayableAmt = Convert.ToDecimal(dtOrdDet.Rows[0]["price"]);
                }



                TotalQty = (from DataRow drCart in dtOrdDet.AsEnumerable() select (Convert.ToInt32(drCart["quantity"]))).Sum();
            }

            PickerOrder.from_name = fromname;
            PickerOrder.from_email = fromemail;
            PickerOrder.from_phone_number = fromphone;
            PickerOrder.from_address = fromaddress;
            PickerOrder.from_pincode = frompincode;
            PickerOrder.pickup_gstin = pickeupGST;
            PickerOrder.to_name = custname;
            PickerOrder.to_email = custemail;
            PickerOrder.to_phone_number = custnumber;
            PickerOrder.to_pincode = custpincode;
            if (checkpincode_api(PickerOrder.to_pincode))
            {
                PickerOrder.to_address = custaddress;
                PickerOrder.quantity = TotalQty;
                PickerOrder.invoice_value = PayableAmt;
                //Not sure - Need to confirm
                DataSet ds = new DataSet();
                ds = orderHandler.get_order_search(Orderid, null);
                if (ds != null && ds.Tables.Count > 0)
                {

                    PickerOrder.cod_amount = 0;
                    PickerOrder.client_order_id = Convert.ToString(Orderid);
                    PickerOrder.item_breadth = 1;
                    PickerOrder.item_height = 1;
                    PickerOrder.item_length = 1;
                    PickerOrder.item_weight = Convert.ToDecimal(0.5);
                    PickerOrder.item_tax_percentage = 0;
                    PickerOrder.invoice_number = ds.Tables[0].Rows[0]["invoice_id"].ToString();
                    PickerOrder.total_discount = Convert.ToInt32(ds.Tables[0].Rows[0]["discount_price"]);
                    PickerOrder.shipping_charge = Convert.ToInt32(ds.Tables[0].Rows[0]["shipping_price"]);
                    PickerOrder.transaction_charge = 0;
                    PickerOrder.giftwrap_charge = 0;
                    PickerOrder.ewaybill_no = "N/A";
                    PickerOrder.has_dg = false;
                    PickerOrder.is_reverse = false;
                    PickerOrder.has_surface = false;
                    PickerOrder.next_day_delivery = false;
                }
                //Not sure - Need to confirm 

                HttpWebRequest request = System.Net.WebRequest.Create(placeorderurl) as HttpWebRequest;

                request.Method = "POST";
                request.ContentType = "application/json; charset=utf-8";

                JavaScriptSerializer jss = new JavaScriptSerializer();
                var orderdata = jss.Serialize(PickerOrder);


                var streamWriter = new StreamWriter(request.GetRequestStream());

                streamWriter.Write(orderdata);
                streamWriter.Flush();
                streamWriter.Close();

                var httpResponse = (HttpWebResponse)request.GetResponse();
                var streamReader = new StreamReader(httpResponse.GetResponseStream());

                var OrderDetails = JsonConvert.DeserializeObject<PickerOrderModel>(streamReader.ReadToEnd());

                PickerOrder.success = OrderDetails.success;
                PickerOrder.order_id = OrderDetails.order_id;
                PickerOrder.order_pk = OrderDetails.order_pk;
                PickerOrder.tracking_id = OrderDetails.tracking_id;
                PickerOrder.manifest_link = OrderDetails.manifest_link;
                PickerOrder.routing_code = OrderDetails.routing_code;
                PickerOrder.client_order_id = OrderDetails.client_order_id;
                PickerOrder.courier = OrderDetails.courier;
                PickerOrder.dispatch_mode = OrderDetails.dispatch_mode;
                PickerOrder.childwaybill_list = OrderDetails.childwaybill_list;

                if (PickerOrder.success == true)
                {
                    Order.order_id = Orderid;
                    Order.ship_id = PickerOrder.tracking_id;
                    Order.order_status = "Scheduled";
                    Order.ship_via = PickerOrder.courier;
                    Order.Flag = 8;
                    Order.manifest_link = downloadlablelurl + authtoken + "/" + PickerOrder.order_pk + "/";
                    bool result = orderHandler.update_order_status(Order);
                    if (result)
                    {
                        return true;
                    }
                    else
                        return false;
                }

                else
                    return false;
            }

            else
                return false;
        }
        #endregion

        #region Order - Cancelling orders on Pickrr
        public bool cancelorder_api(string trackingid)
        {
            PickerOrder.auth_token = authtoken;
            PickerOrder.tracking_id = trackingid;

            HttpWebRequest request = System.Net.WebRequest.Create(placeorderurl) as HttpWebRequest;

            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            JavaScriptSerializer jss = new JavaScriptSerializer();
            var orderdata = jss.Serialize(PickerOrder);


            var streamWriter = new StreamWriter(request.GetRequestStream());

            streamWriter.Write(orderdata);
            streamWriter.Flush();
            streamWriter.Close();

           

            var httpResponse = (HttpWebResponse)request.GetResponse();

            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {
                return true;

            }
            else
                return false;
        }
        #endregion

        #region Get Certificate
        public void BypassCertificate()
        {

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = (snder, cert, chain, error) => true;
            System.Net.ServicePointManager.Expect100Continue = false;
        }
        #endregion
    }




}