using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntities;
using System.Data;
using DAL;
namespace BLL
{
    public class newsletter_handler
    {
        private newsletter_data newsletterdata { get; set; }

        public newsletter_handler()
        {
            newsletterdata = new newsletter_data();
        }
        public bool insert_update_newsletter(newsletter _newsletter)
        {
            return newsletterdata.insert_update_newsletter(_newsletter);
        }
        public bool insert_update_newsletter_footer(newsletter _Newsletter)
        {
            return newsletterdata.insert_update_newsletter_footer(_Newsletter);
        }
        public bool update_newsletter_unsub(newsletter _code)
        {
            return newsletterdata.update_newsletter_unsub(_code);
        }
        public DataSet get_newsletter(long news_letter_id, string email, int Flag)
        {
            return newsletterdata.get_newsletter(news_letter_id, email, Flag);
        }
        public DataSet get_newsletter_code(long code)
        {
            return newsletterdata.get_newsletter_code(code);
        }
        public bool delete_newsletter(long news_letter_id)
        {
            return newsletterdata.delete_newsletter(news_letter_id);
        }
    }
}
