using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using BusinessEntities;
using System.Data;

namespace BLL
{
    public class product_handler
    {
        private product_data obj_ProductData { get; set; }
        public product_handler()
        {
            obj_ProductData = new product_data();
        }

        public Int32 insert_update_product_type(Int32 product_type_id, Int32 child_menu_id, string product_type_name, string product_type_url)
        {
            return obj_ProductData.insert_update_product_type(product_type_id, child_menu_id, product_type_name, product_type_url);
        }
        public DataSet get_product_type(Int64 product_type_id, Int32 child_menu_id)
        {
            return obj_ProductData.get_product_type(product_type_id, child_menu_id);
        }
        public bool delete_product_type(Int32 product_type_id)
        {
            return obj_ProductData.delete_product_type(product_type_id);
        }

        public bool insert_update_product_details(string xmlProductDetails, string xmlProductImages, ref string msg)
        {
            return obj_ProductData.insert_update_product_details(xmlProductDetails, xmlProductImages, ref msg);
        }

        public bool insert_update_productimport(int product_id, string menu_name, string sub_menu_name, string color_name, string material_name,
            string product_name, string size, string weight, float price, float discount, float sale_price, int quantity, bool in_stock, 
            bool in_wishlist, bool is_deal, bool is_latest, bool is_default, bool is_active, string meta_keywords, string meta_title,int orderby, 
            string updated_by,bool is_best_seller, bool is_exclusive, byte? gendertype,ref byte success)
        {
            return obj_ProductData.insert_update_productimport(product_id, menu_name, sub_menu_name, color_name, material_name, product_name,
                size, weight, price, discount, sale_price, quantity, in_stock, in_wishlist, is_deal, is_latest, is_default, is_active,
                meta_keywords, meta_title, orderby, updated_by, is_best_seller, is_exclusive, gendertype, ref success);
        }

        public DataSet get_product_details(long? product_id)
        {
            return obj_ProductData.get_product_details(product_id);
        }

        public bool delete_product_image(long product_image_id, ref string fileName)
        {
            return obj_ProductData.delete_product_image(product_image_id, ref fileName);
        }

        public bool delete_proudct(long product_id, ref string fileName)
        {
            return obj_ProductData.delete_proudct(product_id, ref fileName);
        }

        public bool assign_default_product_image(long product_image_id)
        {
            return obj_ProductData.assign_default_product_image(product_image_id);
        }

        //public bool insert_update_product_image_extra(productextraimage extraimageData, ref string returnMessage)
        //{
        //    throw new NotImplementedException();
        //}

        public DataSet get_home_product_details(long product_id, int Flag)
        {
            return obj_ProductData.get_home_product_details(product_id, Flag);
        }

        public DataSet get_filter_product(Int64 product_id, Int32 product_type_id)
        {
            return obj_ProductData.get_filter_product(product_id, product_type_id);
        }
        public DataSet filter_product(Int32 menu_id, Int32 sub_menu_id, Int32 child_menu_id, string product_name, Int32 PageIndex, Int32 PageSize, bool IsSales, bool IsExclusive, bool IsBestSeller, byte? GenderType, Int32 RecordCount)
        {
            return obj_ProductData.filter_product(menu_id, sub_menu_id, child_menu_id, product_name, PageIndex, PageSize, IsSales, IsExclusive, IsBestSeller, GenderType, RecordCount);
        }

        public DataSet filter_productlist(Int32 menu_id, Int32 sub_menu_id, Int32 child_menu_id, long product_type_id, decimal MinPrice, decimal MaxPrice, string ColorIds, string MaterialIds,string productname, bool IsSales, bool IsExclusive, bool IsBestSeller, byte? gid, Int32 PageIndex, Int32 PageSize,string Orderby, Int32 RecordCount)
        {
            return obj_ProductData.filter_productlist(menu_id, sub_menu_id, child_menu_id, product_type_id, MinPrice, MaxPrice, ColorIds, MaterialIds,productname,IsSales,IsExclusive,IsBestSeller, gid, PageIndex, PageSize,Orderby, RecordCount);
        }

        public DataSet get_search_quantity(long product_id)
        {
            return obj_ProductData.get_search_quantity(product_id);
        }

        public DataSet get_search_productId(long product_id)
        {
            return obj_ProductData.get_search_productId(product_id);
        }

        public DataSet get_coupon_code(string coupon_code, long menu_id, long sub_menu_id, int Flag)
        {
            return obj_ProductData.get_coupon_code(coupon_code, menu_id, sub_menu_id, Flag);
        }

        public Int32 insert_update_product_size(Int64 size_id, Int64 product_id, string size, string price, string discount, string saleprice)
        {
            return obj_ProductData.insert_update_product_size(size_id, product_id, size, price, discount, saleprice);
        }

        public DataSet get_product_size(Int64 size_id, Int32 Flag)
        {
            return obj_ProductData.get_product_size(size_id, Flag);
        }

        public bool delete_product_size(Int64 size_id)
        {
            return obj_ProductData.delete_product_size(size_id);
        }
        public DataSet filter_product_size(Int64 size_id, Int64 product_id, Int32 Flag)
        {
            return obj_ProductData.filter_product_size(size_id, product_id, Flag);
        }
        public bool update_refer_coupon_code(BusinessEntities.coupon _coupon)
        {
            return obj_ProductData.update_refer_coupon_code(_coupon);
        }
        //Comment by Chandni 22+-Jan-2021
        //public DataSet get_new_home_product(Int32 Flag)
        //{
        //    return obj_ProductData.get_new_home_product(Flag);
        //}
        // Added by Chandni 22-Jan-2021
        public DataSet get_new_home_product_new(Int32? menu_id, int? sub_menu_id)
        {
            return obj_ProductData.get_new_home_product_new(menu_id, sub_menu_id);
        }
        public DataSet get_product_topsell(Int32 menu_id, Int32 sub_menu_id)
        {
            return obj_ProductData.get_product_topsell(menu_id, sub_menu_id);
        }
        // Added by Chandni 22-Jan-2021
        //public DataSet get_latest_arriwals_product(Int32 menu_id, Int32 sub_menu_id, Int64 product_id)
        //{
        //    return obj_ProductData.get_latest_arriwals_product(menu_id, sub_menu_id, product_id);
        //}
        public DataSet get_latest_arriwals_product()
        {
            return obj_ProductData.get_latest_arriwals_product();
        }

        public DataSet get_cust_details_ccavenue(Int64 order_id)
        {
            return obj_ProductData.get_cust_details_ccavenue(order_id);
        }

        public DataSet pr_get_product_details(Int64 product_id)
        {
            return obj_ProductData.pr_get_product_details(product_id);
        }

        public bool delete_product_image_extra(Int32 product_image_extra_id, ref string returnMessage)
        {
            return obj_ProductData.delete_product_image_extra(product_image_extra_id, ref returnMessage);
        }

        public int insert_update_product_image_extra(productextraimage extraimageData)//long product_image_extra_id, long product_id, string thumb_image, bool is_active, bool image_order)
        {
            return obj_ProductData.insert_update_product_image_extra(extraimageData.product_image_extra_id, extraimageData.product_id, extraimageData.thumb_image, extraimageData.is_active, extraimageData.image_order,extraimageData.created_by);
        }

        public DataSet get_product_image_extra(long product_id)
        {
            return obj_ProductData.get_product_image_extra(product_id);
        }

        public int update_custome_image(long product_id, string custom_Image)
        {
            return obj_ProductData.update_custome_image(product_id, custom_Image);
        }

        public DataSet pr_product_serach_admin(long? product_id, Int32? menu_id, Int32? sub_menu_id, bool? is_active, bool? in_stock, bool? is_latest, Int32 PageIndex, Int32 PageSize)
        {
            return obj_ProductData.pr_product_serach_admin(product_id, menu_id, sub_menu_id, is_active, in_stock, is_latest, PageIndex, PageSize);
        }
        public DataSet per_product_selectsall(long? product_id, string child_name, Int32 menu_id, Int32 sub_menu_id, Int32 child_menu_id, Int64 product_type_id, bool is_active, bool in_stock, bool is_default, bool is_latest, bool is_best_seller, Int32 PageIndex, Int32 PageSize)
        {
            return obj_ProductData.per_product_selectsall(product_id, child_name, menu_id, sub_menu_id, child_menu_id, product_type_id, is_active, in_stock, is_default, is_latest, is_best_seller, PageIndex, PageSize);
        }
    }
}