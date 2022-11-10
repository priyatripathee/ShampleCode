using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class order_data
    {
        public long insert_order(BusinessEntities.order _order)
        {
            SqlConnection con = new SqlConnection(Connection.ConnstruttDB);
            SqlCommand cmd = new SqlCommand("pr_insert_order", con);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd.Parameters.AddWithValue("@order_id", _order.order_id);
                cmd.Parameters.AddWithValue("@customer_id", _order.customer_id);
                cmd.Parameters.AddWithValue("@order_number", _order.order_number);
                //cmd.Parameters.AddWithValue("@coupon_price", _order.coupon_price);
                cmd.Parameters.AddWithValue("@sale_price", _order.sale_price);
                cmd.Parameters.AddWithValue("@price", _order.price);
                cmd.Parameters.AddWithValue("@discount_price", _order.discount_price);
                cmd.Parameters.AddWithValue("@shipping_price", _order.shipping_price);
                cmd.Parameters.AddWithValue("@coupon_code", _order.coupon_code);
                cmd.Parameters.AddWithValue("@user_name", _order.user_name);
                cmd.Parameters.AddWithValue("@contact_number", _order.contact_number);
                cmd.Parameters.AddWithValue("@email_id", _order.email_id);
                cmd.Parameters.AddWithValue("@address", _order.address);
                cmd.Parameters.AddWithValue("@land_mark", _order.land_mark);
                cmd.Parameters.AddWithValue("@city", _order.city);
                cmd.Parameters.AddWithValue("@state", _order.state);
                cmd.Parameters.AddWithValue("@pin_code", _order.pin_code);
                cmd.Parameters.AddWithValue("@message", _order.message);
                cmd.Parameters.AddWithValue("@payment_through", _order.PaymentThrough);

                SqlParameter retPram = new SqlParameter("@ReturnValue", SqlDbType.Int);
                retPram.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retPram);
                con.Open();
                cmd.ExecuteNonQuery();
                long resultValue = Convert.ToInt32(retPram.Value);
                cmd.Parameters.Clear();
                con.Close();
                return resultValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool insert_order_detail(BusinessEntities.order _orderDetails)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@order_detail_id", _orderDetails.order_detail_id),
                    new SqlParameter("@order_id",_orderDetails.order_id),
                    new SqlParameter("@product_id",_orderDetails.product_id),
                    new SqlParameter("@quantity",_orderDetails.quantity),
                    new SqlParameter("@thumb_image",_orderDetails.thumb_image),
                    new SqlParameter("@order_status",_orderDetails.order_status),
                    new SqlParameter("@TotalPrice",_orderDetails.TotalPrice),
                    new SqlParameter("@custom_bag_price",_orderDetails.custom_bag_price),
                    new SqlParameter("@custom_bag_param",_orderDetails.custom_bag_param),
                    new SqlParameter("@x_point",_orderDetails.x_point),
                    new SqlParameter("@y_point",_orderDetails.y_point),
                    //new SqlParameter("@ThumbImage",_orderDetails.ThumbImage)
		        };

                int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "pr_insert_order_detail", parameters);
                return resultValue > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Int32 insert_order_customtag(Int64 custom_id, Int64 product_id, Int64 order_id, string letter, string style, string color, float price)
        {
            using (SqlConnection cn = new SqlConnection(Connection.ConnstruttDB))
            {
                SqlCommand cmd = new SqlCommand("pr_insert_order_customtag", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@custom_id", SqlDbType.BigInt).Value = custom_id;
                cmd.Parameters.Add("@product_id", SqlDbType.BigInt).Value = product_id;
                cmd.Parameters.Add("@order_id", SqlDbType.BigInt).Value = order_id;
                cmd.Parameters.Add("@letter", SqlDbType.VarChar).Value = letter;
                cmd.Parameters.Add("@style", SqlDbType.VarChar).Value = style;
                cmd.Parameters.Add("@color", SqlDbType.VarChar).Value = color;
                cmd.Parameters.Add("@price", SqlDbType.Float).Value = price;
                SqlParameter retPram = new SqlParameter("@return_value", SqlDbType.Int);
                retPram.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retPram);
                cn.Open();
                int resultValue = cmd.ExecuteNonQuery();
                //int ret = Convert.ToInt32(retPram.Value);
                cn.Close();
                return resultValue;
            }
        }

        public bool update_product_quantity(BusinessEntities.order _orderDetails)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@product_id",_orderDetails.product_id),
                    new SqlParameter("@quantity",_orderDetails.quantity),
                    new SqlParameter("@in_stock",_orderDetails.in_stock)
                };

                int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "pr_update_product_quantity", parameters);
                return resultValue > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet get_order_status(DateTime? FromDate, DateTime? ToDate, string contact_number, string user_name, string order_status, string payment_status, string payment_status2, Nullable<Guid> customer_id, int flag, bool is_active)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
               new SqlParameter("@from_date", FromDate),
                new SqlParameter("@to_date", ToDate),
                new SqlParameter("@contact_number", (contact_number == null ? DBNull.Value : (object)contact_number)),
                new SqlParameter("@user_name", (user_name == null ? DBNull.Value : (object)user_name)),
                new SqlParameter("@order_status", (order_status == null ? DBNull.Value : (object)order_status)),
                new SqlParameter("@payment_status", (payment_status  == null ? DBNull.Value : (object)payment_status)),
                //new SqlParameter("@payment_status2", (payment_status  == null ? DBNull.Value : (object)payment_status)),
                new SqlParameter("@payment_status2",(payment_status2 == null ? DBNull.Value : (object)payment_status2 )),
                new SqlParameter("@customer_id", (customer_id == null ? DBNull.Value : (object)customer_id)),
                new SqlParameter("@Flag", flag),
                new SqlParameter("@is_active", is_active),
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_order_status", parameters);
            return ds;
        }
        public DataSet get_order_export(DateTime? FromDate, DateTime? ToDate, string contact_number, string user_name, string order_status, string payment_status, string payment_status2, Nullable<Guid> customer_id, bool is_active)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
               new SqlParameter("@from_date", FromDate),
                new SqlParameter("@to_date", ToDate),
                new SqlParameter("@contact_number", (contact_number == null ? DBNull.Value : (object)contact_number)),
                new SqlParameter("@user_name", (user_name == null ? DBNull.Value : (object)user_name)),
                new SqlParameter("@order_status", (order_status == null ? DBNull.Value : (object)order_status)),
                new SqlParameter("@payment_status", (payment_status  == null ? DBNull.Value : (object)payment_status)),
                //new SqlParameter("@payment_status2", (payment_status  == null ? DBNull.Value : (object)payment_status)),
                new SqlParameter("@payment_status2",(payment_status2 == null ? DBNull.Value : (object)payment_status2 )),
                new SqlParameter("@customer_id", (customer_id == null ? DBNull.Value : (object)customer_id)),
                new SqlParameter("@is_active", is_active),
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_order_export", parameters);
            return ds;
        }
        public DataSet get_order_customtag(Int64 order_detail_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@order_detail_id",order_detail_id),
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_order_customtag", parameters);
            return ds;
        }

        public DataSet get_preview_order_custom(Int64 order_detail_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@order_detail_id",order_detail_id),
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_preview_order_custom", parameters);
            return ds;
        }
        public DataSet get_order_return(string email_id, int flag)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                //new SqlParameter("@order_detail_id", order_detail_id),
                new SqlParameter("@email_id",email_id),
                new SqlParameter("@Flag", flag)
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_order_return", parameters);
            return ds;
        }
        public DataSet chk_CouonCodeByEmail(string email_id, string coupon_code)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                //new SqlParameter("@order_detail_id", order_detail_id),
                new SqlParameter("@email_id",email_id),
                new SqlParameter("@coupon_code", coupon_code)
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_chk_CouonCodeByEmail", parameters);
            return ds;
        }

        public DataSet get_order_search(Int64 order_id, Int64? order_detail_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@order_id", order_id),
                new SqlParameter("@order_detail_id", order_detail_id)
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_order_search", parameters);
            return ds;
        }


        public DataSet get_order_search_order(Int64 order_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@order_id", order_id),
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_order_search_order", parameters);
            return ds;
        }

        public DataSet get_order_search_orderdetail(Int64 order_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@order_id", order_id)
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_order_search_orderdetail", parameters);
            return ds;
        }

        public bool update_order_PaymentType(Int64 order_id, string payment_type)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@order_id", order_id),
                new SqlParameter("@payment_through", payment_type)
            };
                int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "update_order_PaymentType", parameters);

                return resultValue > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public bool update_order_status(BusinessEntities.order orderDetails)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@order_id", orderDetails.order_id),
                    new SqlParameter("@ship_via",orderDetails.ship_via),
                    new SqlParameter("@ship_id",orderDetails.ship_id ),
                    new SqlParameter("@freight",orderDetails.freight),
                    new SqlParameter("@order_status",orderDetails.order_status),
                    new SqlParameter("@payment_response",orderDetails.payment_response),
                    new SqlParameter("@Flag",orderDetails.Flag),
                    new SqlParameter("@manifest_link",orderDetails.manifest_link)
                };

                int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "pr_update_order_status", parameters);
                return resultValue > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool update_order_customer(BusinessEntities.order orderDetails)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@order_id", orderDetails.order_id),
                    new SqlParameter("@address",orderDetails.address),
                    new SqlParameter("@city",orderDetails.city),
                    new SqlParameter("@state",orderDetails.state),
                    new SqlParameter("@pin_code",orderDetails.pin_code),
                    new SqlParameter("@contact_number",orderDetails.contact_number)
                };

                int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "pr_update_order_customer", parameters);
                return resultValue > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool update_order_Xpresslanecustomer(BusinessEntities.order orderDetails)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@order_id", orderDetails.order_id),
                     new SqlParameter("@customer_id", orderDetails.customer_id),
                    new SqlParameter("@address",orderDetails.address),
                    new SqlParameter("@city",orderDetails.city),
                    new SqlParameter("@state",orderDetails.state),
                    new SqlParameter("@pin_code",orderDetails.pin_code),
                    new SqlParameter("@contact_number",orderDetails.contact_number)
                };

                int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "update_order_Xpresslanecustomer", parameters);
                return resultValue > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet get_order_status_track(Int64 order_id, int flag)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
               // new SqlParameter("@order_detail_id", order_detail_id),
                new SqlParameter("@order_id", order_id),
                new SqlParameter("@Flag", flag)
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_order_status_track", parameters);
            return ds;
        }

        public bool update_cancel_product(BusinessEntities.order orderDetails)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@order_id", orderDetails.order_id),
                    new SqlParameter("@order_status",orderDetails.order_status),
                    new SqlParameter("@reason_for_cancellation",orderDetails.reason_for_cancellation),
                    new SqlParameter("@Comments",orderDetails.Comments)
                };

                int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "pr_update_cancel_product", parameters);
                return resultValue > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool insert_raise_complain(BusinessEntities.order orderDetails)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@raise_id", orderDetails.raise_id),
                    new SqlParameter("@login_id",orderDetails.login_id),
                    new SqlParameter("@name",orderDetails.user_name),
                    new SqlParameter("@contact",orderDetails.contact_number),
                    new SqlParameter("@email",orderDetails.email_id),
                    new SqlParameter("@complain_type",orderDetails.complain_type),
                    new SqlParameter("@details",orderDetails.details)
                };

                int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "pr_insert_raise_complain", parameters);
                return resultValue > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool insert_LeaveFeedback(BusinessEntities.order orderDetails)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@OrderId", orderDetails.order_id),
                    new SqlParameter("@EmailId",orderDetails.email_id),
                    new SqlParameter("@Rating",orderDetails.rating),
                    new SqlParameter("@ItemArrived",orderDetails.ItemArrived),
                    new SqlParameter("@ItemDescribed",orderDetails.ItemDescribed),
                    new SqlParameter("@DepartureOnTime",orderDetails.DepartureOnTime),
                    new SqlParameter("@Comment",orderDetails.FeedbackComment)
                };

                int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "pr_insert_update_leaveFeedback", parameters);
                return resultValue > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool delete_order(Int32 order_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@OrderId", order_id)
            };

            int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "pr_delete_order", parameters);
            return resultValue > 0 ? true : false;
        }
        public DataSet get_temp_order(Int64? order_id, DateTime? FromDate, DateTime? ToDate)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
              //  new SqlParameter("@order_id", order_id),
                 new SqlParameter("@order_id", (order_id == null ? DBNull.Value : (object)order_id)),
                 new SqlParameter("@from_date", (FromDate == null ? DBNull.Value : (object)FromDate)),
                new SqlParameter("@to_date", (ToDate == null ? DBNull.Value : (object)ToDate)),
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_temp_order", parameters);
            return ds;
        }
        public DataSet get_temp_partialorder(Int64? order_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
              //  new SqlParameter("@order_id", order_id),
                 new SqlParameter("@order_id", (order_id == null ? DBNull.Value : (object)order_id)),
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_temp_partialorder", parameters);
            return ds;
        }

        public DataSet get_abandonedcart()
        {
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_abandonedcart");
            return ds;
        }

        public bool update_temp_customer_email_count(Int64? order_id, Int64? EmailCount)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@order_id", (order_id == null ? DBNull.Value : (object)order_id)),
                    new SqlParameter("@EmailCount", (EmailCount == null ? DBNull.Value : (object)EmailCount)),
                };

                int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "pr_update_temp_customer_email_count_Date", parameters);
                return resultValue > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      
        public DataSet check_order(Guid Order_number)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Order_number", Order_number),

            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "check_order", parameters);
            return ds;
        }

    }
}
