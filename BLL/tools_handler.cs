using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using BusinessEntities;
using System.Data;

namespace BLL
{
    public class tools_handler
    {
        private tools_data toolsData { get; set; }
        public tools_handler()
        {
            toolsData = new tools_data();
        }

        public Int32 insert_update_lookbook(Int32 lookbookId, string description, string image)
        {
            return toolsData.insert_update_lookbook(lookbookId, description, image);
        }
        public DataSet get_lookbook(Int32? lookbookId, byte? is_active)
        {
            return toolsData.get_lookbook(lookbookId, is_active);
        }
        public bool delete_lookbook(Int64 lookbookId)
        {
            return toolsData.delete_lookbook(lookbookId);
        }
        public DataSet get_corporate(Int32? corporateId, byte? is_active)
        {
            return toolsData.get_corporate(corporateId, is_active);
        }
        public Int32 insert_update_corporate(Int32 corporateId, string description, string image)
        {
            return toolsData.insert_update_corporate(corporateId, description, image);
        }
        public bool delete_corporate(Int64 corporateId)
        {
            return toolsData.delete_corporate(corporateId);
        }
        #region color 
        public Int32 insert_update_color(Int32 color_id, string color_name, string color_code)
        {
            return toolsData.insert_update_color(color_id, color_name, color_code);
        }

        public DataSet get_color(Int32 color_id, byte? is_active)
        {
            return toolsData.get_color(color_id, is_active);
        }

        public bool delete_color(Int64 color_id)
        {
            return toolsData.delete_color(color_id);
        }
        #endregion

        #region material 
        public Int32 insert_update_material(Int32 material_id, string material_name)
        {
            return toolsData.insert_update_material(material_id, material_name);
        }

        public DataSet get_material(Int32 material_id)
        {
            return toolsData.get_material(material_id);
        }

        public bool delete_material(Int64 material_id)
        {
            return toolsData.delete_material(material_id);
        }
        #endregion

        #region coupon 
        public Int32 insert_update_coupon(Int32 coupon_code_id, Int32 menu_id, string menu_name, Int32 sub_menu_id, string sub_menu_name, Int32 child_menu_id, string child_name, string coupon_code, Int32 price, string sender_email, string reciever_email, string created_by, string updated_by, byte? gendertype)
        {
            return toolsData.insert_update_coupon(coupon_code_id, menu_id, menu_name, sub_menu_id, sub_menu_name, child_menu_id, child_name, coupon_code, price, sender_email, reciever_email, created_by, updated_by,gendertype);
        }
        public DataSet get_coupon(Int32 coupon_code_id)
        {
            return toolsData.get_coupon(coupon_code_id);
        }
        public bool delete_coupon(Int32 coupon_code_id)
        {
            return toolsData.delete_coupon(coupon_code_id);
        }
        #endregion
    }
}
