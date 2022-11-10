using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DAL;
using BusinessEntities;

namespace BLL
{
    public class wishlist_handler
    {
        wishlist_data objWishlist = null;

        public wishlist_handler()
        {
            objWishlist = new wishlist_data();
        }
        public bool InsertWishList(wishlist wishlist)
        {
            return objWishlist.InsertWishList(wishlist);
        }

        public DataSet get_wishlist_recommendation(Int64 wishlist_id, string email_id, Int32 Flag)
        {
            return objWishlist.get_wishlist_recommendation(wishlist_id, email_id, Flag);
        }

        public bool DeleteWishlist(Int64 ProductId)
        {
            return objWishlist.DeleteWishlist(ProductId);
        }
    }
}
