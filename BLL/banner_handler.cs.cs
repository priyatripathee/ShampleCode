using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using BusinessEntities;
using System.Data;

namespace BLL
{
   public class banner_handler
    {
       private banner_data bannerData { get; set; }
       public banner_handler()
       {
           bannerData = new banner_data();
       }

       public bool insert_update_banner(banner banner, ref string returnMessage)
       {
           return bannerData.insert_update_banner(banner, ref returnMessage);
       }

        public DataSet get_banner(Int32? bannerId, byte? type)
        {
            return bannerData.get_banner(bannerId, type);
        }
        //public DataSet get_banner(Int32 bannerId)
        //{
        //    return bannerData.get_banner(bannerId);
        //}
        public bool delete_banner(long bannerId, ref string imageName, ref string returnMessage)
       {
           return bannerData.delete_banner(bannerId, ref imageName, ref returnMessage);
       }


       public bool insert_update_Category_link(banner advertisment, ref string returnMessage)
       {
           return bannerData.insert_update_Category_link(advertisment, ref returnMessage);
       }

       public DataSet get_category_link(Int32 category_link_id)
       {
           return bannerData.get_category_link(category_link_id);
       }

       public bool delete_category_link(Int32 category_link_id, ref string imageName, ref string returnMessage)
       {
           return bannerData.delete_category_link(category_link_id, ref imageName, ref returnMessage);
       }

       
    }
}
