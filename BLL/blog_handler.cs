using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using BusinessEntities;
using System.Data;

namespace BLL
{
    public class blog_handler
    {
        private blog_data blogData { get; set; }
        public blog_handler()
        {
            blogData = new blog_data();
        }

        public bool insert_update_blog(blog _blog, ref string returnMessage)
        {
            return blogData.insert_update_blog(_blog, ref returnMessage);
        }
        public DataSet get_bog(Int64 blog_id)
        {
            return blogData.get_bog(blog_id);
        }
        public bool delete_blog(long blog_id, ref string imageName, ref string returnMessage)
        {
            return blogData.delete_blog(blog_id, ref imageName, ref returnMessage);
        }

        public Int32 insert_update_blogComment(Int32 blog_id, Guid customer_id, string comment, bool is_active)
        {
            return blogData.insert_update_blogComment(blog_id, customer_id, comment, is_active);
        }
        public DataSet get_customerblogcomment(Int32? id, Int32 blog_id)
        {
            return blogData.get_customerblogcomment(id, blog_id);
        }
    }
}
