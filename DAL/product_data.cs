using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntities;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class product_data
    {
        #region product type 
        public Int32 insert_update_product_type(Int32 product_type_id, Int32 child_menu_id, string product_type_name, string product_type_url)
        {
            using (SqlConnection cn = new SqlConnection(Connection.ConnstruttDB))
            {
                SqlCommand cmd = new SqlCommand("pr_insert_update_product_type", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@product_type_id", SqlDbType.BigInt).Value = product_type_id;
                cmd.Parameters.Add("@child_menu_id", SqlDbType.BigInt).Value = child_menu_id;
                cmd.Parameters.Add("@product_type_name", SqlDbType.VarChar).Value = product_type_name;
                cmd.Parameters.Add("@product_type_url", SqlDbType.VarChar).Value = product_type_url;
                SqlParameter retPram = new SqlParameter("@return_value", SqlDbType.Int);
                retPram.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retPram);
                cn.Open();
                int resultValue = cmd.ExecuteNonQuery();
                cn.Close();
                return resultValue;
            }
        }
        public DataSet get_product_type(Int64 product_type_id, Int32 child_menu_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@product_type_id", product_type_id),
                new SqlParameter("@child_menu_id", child_menu_id)
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_product_type", parameters);
            return ds;
        }
        public bool delete_product_type(Int32 product_type_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@product_type_id", product_type_id)
            };

            int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "pr_delete_product_type", parameters);
            return resultValue > 0 ? true : false;
        }
        #endregion

        #region product details

        public DataSet get_filter_product(Int64 product_id, Int32 product_type_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@product_id", product_id),
                new SqlParameter("@product_type_id", product_type_id)
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_filter_product", parameters);
            return ds;
        }


        public bool insert_update_product_details(string xmlProductDetails, string xmlProductImages, ref string msg)
        {
            SqlConnection con = new SqlConnection(Connection.ConnstruttDB);
            SqlCommand cmd = new SqlCommand("pr_insert_update_product_details", con);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd.Parameters.AddWithValue("@xmlProductDetails", xmlProductDetails);
                cmd.Parameters.AddWithValue("@xmlProductImages", xmlProductImages);
                SqlParameter retPram1 = new SqlParameter("@customMessage", SqlDbType.NVarChar, 500);
                retPram1.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retPram1);
                con.Open();
                cmd.ExecuteNonQuery();
                msg = retPram1.Value.ToString();
                con.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool insert_update_productimport(int product_id, string menu_name, string sub_menu_name, string color_name, string material_name,
            string product_name, string size, string weight, float price, float discount, float sale_price, int quantity, bool in_stock,
            bool in_wishlist, bool is_deal, bool is_latest, bool is_default, bool is_active, string meta_keywords, string meta_title, int orderby,
            string updated_by, bool is_best_seller, bool is_exclusive, byte? gendertype, ref byte success)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Connection.ConnstruttDB))
                {
                    SqlCommand cmd = new SqlCommand("pr_insert_update_productimport", cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = product_id;
                    cmd.Parameters.Add("@menu_name", SqlDbType.VarChar).Value = (menu_name == null ? DBNull.Value : (object)menu_name);
                    cmd.Parameters.Add("@sub_menu_name", SqlDbType.VarChar).Value = (sub_menu_name == null ? DBNull.Value : (object)sub_menu_name);
                    cmd.Parameters.Add("@color_name", SqlDbType.VarChar).Value = (color_name == null ? DBNull.Value : (object)color_name);
                    cmd.Parameters.Add("@material_name", SqlDbType.VarChar).Value = (material_name == null ? DBNull.Value : (object)material_name);
                    cmd.Parameters.Add("@product_name", SqlDbType.VarChar).Value = (product_name == null ? DBNull.Value : (object)product_name);
                    //cmd.Parameters.Add("@short_description", SqlDbType.VarChar).Value = (short_description == null ? DBNull.Value : (object)short_description);
                    // cmd.Parameters.Add("@full_description", SqlDbType.VarChar).Value = (full_description == null ? DBNull.Value : (object)full_description);
                    //cmd.Parameters.Add("@specification", SqlDbType.VarChar).Value = (specification == null ? DBNull.Value : (object)specification);
                    cmd.Parameters.Add("@size", SqlDbType.VarChar).Value = (size == null ? DBNull.Value : (object)size);
                    cmd.Parameters.Add("@weight", SqlDbType.VarChar).Value = (weight == null ? DBNull.Value : (object)weight);

                    cmd.Parameters.Add("@price", SqlDbType.Float).Value = price;
                    cmd.Parameters.Add("@discount", SqlDbType.Float).Value = discount;
                    cmd.Parameters.Add("@sale_price", SqlDbType.Float).Value = sale_price;
                    cmd.Parameters.Add("@quantity", SqlDbType.Int).Value = quantity;
                    cmd.Parameters.Add("@in_stock", SqlDbType.Bit).Value = in_stock;
                    cmd.Parameters.Add("@in_wishlist", SqlDbType.Bit).Value = in_wishlist;
                    cmd.Parameters.Add("@is_deal", SqlDbType.Bit).Value = is_deal;
                    cmd.Parameters.Add("@is_latest", SqlDbType.Bit).Value = is_latest;
                    cmd.Parameters.Add("@is_default", SqlDbType.Bit).Value = is_default;
                    cmd.Parameters.Add("@is_active", SqlDbType.Bit).Value = is_active;
                    //cmd.Parameters.Add("@deal_date", SqlDbType.DateTime).Value = deal_date;
                    // cmd.Parameters.Add("@deal_of_the_day", SqlDbType.Int).Value = deal_of_the_day;
                    //cmd.Parameters.Add("@is_gift", SqlDbType.Bit).Value = is_gift;
                    cmd.Parameters.Add("@meta_keywords", SqlDbType.Text).Value = (meta_keywords == null ? DBNull.Value : (object)meta_keywords);
                    cmd.Parameters.Add("@meta_title", SqlDbType.Text).Value = (meta_title == null ? DBNull.Value : (object)meta_title);
                    // cmd.Parameters.Add("@meta_description", SqlDbType.Text).Value = (meta_description == null ? DBNull.Value : (object)meta_description);
                    cmd.Parameters.Add("@orderby", SqlDbType.Int).Value = orderby;
                    //cmd.Parameters.Add("@created_by", SqlDbType.VarChar).Value = (created_by == null ? DBNull.Value : (object)created_by);
                    // cmd.Parameters.Add("@created_date", SqlDbType.DateTime).Value = created_date;
                    //cmd.Parameters.Add("@updated_date", SqlDbType.DateTime).Value = updated_date;
                    cmd.Parameters.Add("@updated_by", SqlDbType.VarChar).Value = (updated_by == null ? DBNull.Value : (object)updated_by);
                    cmd.Parameters.Add("@is_best_seller", SqlDbType.Bit).Value = is_best_seller;
                    cmd.Parameters.Add("@is_exclusive", SqlDbType.Bit).Value = is_exclusive;
                    //  cmd.Parameters.Add("@features", SqlDbType.Text).Value = (features == null ? DBNull.Value : (object)features);
                    cmd.Parameters.Add("@gendertype", SqlDbType.VarChar).Value = (gendertype == null ? DBNull.Value : (object)gendertype);
                    SqlParameter retPram = new SqlParameter("@success", SqlDbType.TinyInt);
                    retPram.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(retPram);

                    cn.Open();
                    int resultValue = cmd.ExecuteNonQuery();
                    success = Convert.ToByte(cmd.Parameters["@success"].Value);
                    cn.Close();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet get_product_details(long? product_id)
        {
            DataSet dsProductDetails = new DataSet();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@product_id", product_id)
            };

            dsProductDetails = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_product_details", parameters);
            return dsProductDetails;
        }
        public bool delete_product_image(long product_image_id, ref string fileName)
        {
            SqlConnection con = new SqlConnection(Connection.ConnstruttDB);
            SqlCommand cmd = new SqlCommand("pr_delete_product_image", con);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd.Parameters.AddWithValue("@product_image_id", product_image_id);
                SqlParameter retPram1 = new SqlParameter("@ReturnFileName", SqlDbType.VarChar, 200);
                retPram1.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retPram1);
                con.Open();
                cmd.ExecuteNonQuery();
                fileName = retPram1.Value.ToString();
                con.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool delete_proudct(long product_id, ref string fileName)
        {
            SqlConnection con = new SqlConnection(Connection.ConnstruttDB);
            SqlCommand cmd = new SqlCommand("pr_delete_proudct", con);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd.Parameters.AddWithValue("@product_id", product_id);
                SqlParameter retPram1 = new SqlParameter("@ReturnFileName", SqlDbType.VarChar, 200);
                retPram1.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retPram1);
                con.Open();
                cmd.ExecuteNonQuery();
                fileName = retPram1.Value.ToString();
                con.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool delete_product_image_extra(long product_image_extra_id, ref string fileName)
        {
            SqlConnection con = new SqlConnection(Connection.ConnstruttDB);
            SqlCommand cmd = new SqlCommand("pr_delete_product_image_extra", con);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd.Parameters.AddWithValue("@product_image_extra_id", product_image_extra_id);
                SqlParameter retPram1 = new SqlParameter("@ReturnImageName", SqlDbType.VarChar, 200);
                retPram1.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retPram1);
                con.Open();
                cmd.ExecuteNonQuery();
                fileName = retPram1.Value.ToString();
                con.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool assign_default_product_image(long product_image_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@product_image_id", product_image_id)

            };
            int result = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "pr_assign_default_product_image", parameters);
            return result > 0 ? true : false;
        }
        public DataSet get_home_product_details(long product_id, int Flag)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@product_id", product_id),
                    new SqlParameter("@Flag", Flag)
                };
                DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_home_product_details", parameters);
                return ds;
            }
            catch
            {
                return null;
            }
        }


        public DataSet filter_product(Int32 menu_id, Int32 sub_menu_id, Int32 child_menu_id, string product_name, Int32 PageIndex, Int32 PageSize, bool IsSales, bool IsExclusive, bool IsBestSeller, byte? GenderType, Int32 RecordCount)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@menu_id", menu_id),
                    new SqlParameter("@sub_menu_id", sub_menu_id),
                    new SqlParameter("@child_menu_id", child_menu_id),
                    new SqlParameter("@product_name", product_name),
                    new SqlParameter("@PageIndex", PageIndex),
                    new SqlParameter("@PageSize", PageSize),
                    new SqlParameter("@IsSales", IsSales),
                    new SqlParameter("@IsExclusive", IsExclusive),
                    new SqlParameter("@IsBestSeller", IsBestSeller),
                    new SqlParameter("@GenderType", GenderType),
                    new SqlParameter("@RecordCount", RecordCount)
                };
                DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_filter_product", parameters);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet filter_productlist(Int32 menu_id, Int32 sub_menu_id, Int32 child_menu_id, long product_type_id, decimal MinPrice, decimal MaxPrice, string ColorIds, string MaterialIds, string productname, bool IsSales, bool IsExclusive, bool IsBestSeller, byte? gid, Int32 PageIndex, Int32 PageSize, string Orderby, Int32 RecordCount)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@menu_id", menu_id),
                    new SqlParameter("@sub_menu_id", sub_menu_id),
                    new SqlParameter("@child_menu_id", child_menu_id),
                    new SqlParameter("@product_type_id", product_type_id),
                    new SqlParameter("@MinPrice", MinPrice),
                    new SqlParameter("@MaxPrice", MaxPrice),
                    new SqlParameter("@ColorIds", ColorIds),
                    new SqlParameter("@MaterialIds", MaterialIds),
                    new SqlParameter("@ProductName", productname),
                    new SqlParameter("@IsSales", IsSales),
                    new SqlParameter("@IsExclusive", IsExclusive),
                    new SqlParameter("@IsBestSeller", IsBestSeller),
                    new SqlParameter("@GenderType", gid),
                    new SqlParameter("@PageIndex", PageIndex),
                    new SqlParameter("@PageSize", PageSize),
                    new SqlParameter("@OrderBy",Orderby),
                    new SqlParameter("@RecordCount", RecordCount)
                };

                DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_filter_productlist", parameters);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet get_search_quantity(long product_id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@product_id", product_id)
                };
                DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_search_quantity", parameters);
                return ds;
            }
            catch
            {
                return null;
            }
        }

        public DataSet get_search_productId(long product_id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@product_id", product_id)
                };
                DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_search_productId", parameters);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        public DataSet get_coupon_code(string coupon_code, long menu_id, long sub_menu_id, int Flag)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@coupon_code", coupon_code),
                    new SqlParameter("@menu_id", menu_id),
                    new SqlParameter("@sub_menu_id", sub_menu_id),
                    new SqlParameter("@Flag", Flag)
                };
                DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_coupon_code", parameters);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Int32 insert_update_product_size(Int64 size_id, Int64 product_id, string size, string price, string discount, string saleprice)
        {
            using (SqlConnection cn = new SqlConnection(Connection.ConnstruttDB))
            {
                SqlCommand cmd = new SqlCommand("pr_insert_update_product_size", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@size_id", SqlDbType.BigInt).Value = size_id;
                cmd.Parameters.Add("@product_id", SqlDbType.BigInt).Value = product_id;
                cmd.Parameters.Add("@size", SqlDbType.VarChar).Value = size;
                cmd.Parameters.Add("@price", SqlDbType.VarChar).Value = price;
                cmd.Parameters.Add("@discount", SqlDbType.VarChar).Value = discount;
                cmd.Parameters.Add("@sale_price", SqlDbType.VarChar).Value = saleprice;
                SqlParameter retPram = new SqlParameter("@return_value", SqlDbType.Int);
                retPram.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retPram);
                cn.Open();
                int resultValue = cmd.ExecuteNonQuery();
                cn.Close();
                return resultValue;
            }
        }

        public DataSet get_product_size(Int64 size_id, Int32 Flag)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@size_id", size_id),
                new SqlParameter("@Flag", Flag)
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_product_size", parameters);
            return ds;
        }

        public DataSet filter_product_size(Int64 size_id, Int64 product_id, Int32 Flag)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@size_id", size_id),
                new SqlParameter("@product_id", product_id),
                new SqlParameter("@Flag", Flag)
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_filter_product_size", parameters);
            return ds;
        }

        public bool delete_product_size(Int64 size_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@size_id", size_id)
            };

            int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "pr_delete_product_size", parameters);
            return resultValue > 0 ? true : false;
        }

        public bool update_refer_coupon_code(BusinessEntities.coupon _coupon)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@reciever_email",_coupon.reciever_email)
                };

                int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "pr_update_refer_coupon_code", parameters);
                return resultValue > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Comment by Chandni 22+-Jan-2021
        //public DataSet get_new_home_product(Int32 Flag)
        //{
        //    SqlParameter[] parameters = new SqlParameter[]
        //    {
        //        new SqlParameter("@Flag", Flag)
        //    };
        //    DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_new_home_product", parameters);
        //    return ds;
        //}
        //Added by chandni 22-Jan-2021
        public DataSet get_new_home_product_new(Int32? menu_id, int? sub_menu_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@menu_id", menu_id == null ? DBNull.Value : (object)menu_id),
                new SqlParameter("@sub_menu_id", sub_menu_id == null ? DBNull.Value : (object)sub_menu_id),
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_new_home_product_new", parameters);
            return ds;
        }
        public DataSet get_product_topsell(Int32 menu_id, Int32 sub_menu_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@menu_id", menu_id),
                new SqlParameter("@sub_menu_id", sub_menu_id)
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_product_topsell", parameters);
            return ds;
        }
        //Added by chandni 22-Jan-2021

        public int insert_update_product_image_extra(long product_image_extra_id, long product_id, string thumb_image, bool is_active, byte image_order, string created_by)
        {
            using (SqlConnection cn = new SqlConnection(Connection.ConnstruttDB))
            {
                SqlCommand cmd = new SqlCommand("pr_insert_update_product_image_extra", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@product_image_extra_id", SqlDbType.BigInt).Value = product_image_extra_id;
                cmd.Parameters.Add("@product_id", SqlDbType.BigInt).Value = product_id;
                cmd.Parameters.Add("@thumb_image", SqlDbType.VarChar).Value = thumb_image;
                cmd.Parameters.Add("@is_active", SqlDbType.Bit).Value = is_active;
                cmd.Parameters.Add("@image_order", SqlDbType.TinyInt).Value = image_order;
                // cmd.Parameters.Add("@created_by", SqlDbType.VarChar).Value = created_by;
                cmd.Parameters.Add("@created_by", SqlDbType.VarChar).Value = (created_by == null ? DBNull.Value : (object)created_by);
                //SqlParameter retPram = new SqlParameter("@return_value", SqlDbType.Int);
                //retPram.Direction = ParameterDirection.Output;
                //cmd.Parameters.Add(retPram);
                cn.Open();
                int resultValue = cmd.ExecuteNonQuery();
                cn.Close();
                return resultValue;
            }
        }
        public DataSet get_product_image_extra(long product_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@product_id", @product_id)
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_product_image_extra", parameters);
            return ds;
        }

        public int update_custome_image(long product_id, string custom_Image)
        {
            using (SqlConnection cn = new SqlConnection(Connection.ConnstruttDB))
            {
                SqlCommand cmd = new SqlCommand("pr_update_custome_image", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@product_id", SqlDbType.BigInt).Value = product_id;
                cmd.Parameters.Add("@custom_Image", SqlDbType.VarChar).Value = custom_Image;
                cn.Open();
                int resultValue = cmd.ExecuteNonQuery();
                cn.Close();
                return resultValue;
            }
        }

        //public DataSet get_latest_arriwals_product(Int32 menu_id, Int32 sub_menu_id, Int64 product_id)
        //{
        //    SqlParameter[] parameters = new SqlParameter[]
        //    {
        //        new SqlParameter("@menu_id", menu_id),
        //        new SqlParameter("@sub_menu_id", sub_menu_id),
        //        new SqlParameter("@product_id", product_id)
        //    };
        //    DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_latest_arriwals_product", parameters);
        //    return ds;
        //}
        public DataSet get_latest_arriwals_product()
        {
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_latest_arriwals_product");
            return ds;
        }

        public DataSet get_cust_details_ccavenue(Int64 order_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@order_id", order_id)
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_cust_details_ccavenue", parameters);
            return ds;
        }

        public DataSet pr_get_product_details(long product_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@product_id", product_id)
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_product_details", parameters);
            return ds;
        }
        public DataSet pr_product_serach_admin(long? product_id, int? menu_id, int? sub_menu_id, bool? is_active, bool? in_stock, bool? is_latest, Int32 PageIndex, Int32 PageSize)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@product_id", product_id == null ? DBNull.Value : (object)product_id),
                new SqlParameter("@menu_id", menu_id == null ? DBNull.Value : (object)menu_id),
                new SqlParameter("@sub_menu_id", sub_menu_id == null ? DBNull.Value : (object)sub_menu_id),
                new SqlParameter("@is_active", is_active == null ? DBNull.Value : (object)is_active),
                new SqlParameter("@in_stock", in_stock == null ? DBNull.Value : (object)in_stock),
                new SqlParameter("@is_latest", is_latest == null ? DBNull.Value : (object)is_latest),
                new SqlParameter("@PageIndex", PageIndex),
                new SqlParameter("@PageSize", PageSize),
            };

            //new SqlParameter("@product_id", product_id),
            //        new SqlParameter("@menu_id", menu_id),
            //        new SqlParameter("@sub_menu_id", sub_menu_id),
            //        new SqlParameter("@is_active", is_active),
            //        new SqlParameter("@in_stock", in_stock),
            //        new SqlParameter("@is_latest", is_latest),
            //        new SqlParameter("@PageIndex", PageIndex),
            //        new SqlParameter("@PageSize", PageSize),
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_product_serach_admin", parameters);
            return ds;
        }
        public DataSet per_product_selectsall(long? product_id, string child_name, Int32 menu_id, Int32 sub_menu_id, Int32 child_menu_id, Int64 product_type_id, bool is_active, bool in_stock, bool is_default, bool is_latest, bool is_best_seller, Int32 PageIndex, Int32 PageSize)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@product_id", product_id),
                    new SqlParameter("@child_name", child_name),
                    new SqlParameter("@menu_id", menu_id),
                    new SqlParameter("@sub_menu_id", sub_menu_id),
                    new SqlParameter("@child_menu_id", child_menu_id),
                    new SqlParameter("@product_type_id", product_type_id),
                    new SqlParameter("@is_active", is_active),
                    new SqlParameter("@in_stock", in_stock),
                    new SqlParameter("@is_default", is_default),
                    new SqlParameter("@is_latest", is_latest),
                    new SqlParameter("@is_best_seller", is_best_seller),
                    new SqlParameter("@PageIndex", PageIndex),
                    new SqlParameter("@PageSize", PageSize),
                };
                DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "per_product_selectsall", parameters);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
