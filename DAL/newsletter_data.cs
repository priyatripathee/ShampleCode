using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using BusinessEntities;
using System.Data;


namespace DAL
{
    public class newsletter_data
    {
        public bool insert_update_newsletter(newsletter _newsletter)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
		        {
                    new SqlParameter("@NewsLetterId", _newsletter.news_letter_id),
			        new SqlParameter("@Email", _newsletter.email),
                    new SqlParameter("@URL", _newsletter.url),
                    new SqlParameter("@Code", _newsletter.code)
		        };

                int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "pr_insert_update_newsletter", parameters);
                return resultValue > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool insert_update_newsletter_footer(newsletter _Newsletter)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
          {
           new SqlParameter("@NewsLetterId", _Newsletter.news_letter_id),
                    new SqlParameter("@Email",_Newsletter.email),
                    new SqlParameter("@URL",_Newsletter.url),
                    new SqlParameter("@Code", _Newsletter.code)
                    
          };

                int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "pr_insert_update_newsletter_footer", parameters);
                return resultValue > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool update_newsletter_unsub(newsletter _code)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
          {
           new SqlParameter("@news_letter_id", _code.code)
                    
          };

                int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "pr_update_newsletter_unsub", parameters);
                return resultValue > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataSet get_newsletter(long news_letter_id, string email, int Flag)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@news_letter_id", news_letter_id),
                new SqlParameter("@email", email),
                new SqlParameter("@Flag", Flag)
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_newsletter", parameters);
            return ds;
        }

        public DataSet get_newsletter_code(long code)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@code", code)
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_newsletter_code", parameters);
            return ds;
        }

        public bool delete_newsletter(long news_letter_id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@news_letter_id", news_letter_id)			       
                };

                int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "pr_delete_newsletter", parameters);
                return resultValue > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
