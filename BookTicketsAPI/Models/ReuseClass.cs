using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace BookTicketsAPI.Models
{
    public class ReuseClass
    {
        public List<string> GetMovieDetails(List<string> conditions, string TableName)
        {
            List<string> SelectQuery = new List<string>();
            string query = "SELECT * FROM " + TableName;
            string joiner = " WHERE ";
            string MovieName = "MovieName";
            foreach (string condition in conditions)
            {
                string[] lang = condition.Split(',');
                foreach (string language in lang)
                {
                    query += joiner + MovieName + "=" + "'" + language + "'";
                    SelectQuery.Add(query);
                }
            }
            return SelectQuery;
        }
        public List<string> ReturnTable(List<string> queries)
        {
            string Con = ConfigurationManager.ConnectionStrings["ConnectionDB"].ConnectionString;
            List<string> GetTableData = new List<string>();
            SqlConnection con = new SqlConnection(Con);
            DataTable dt = new DataTable();
            foreach (string query in queries)
            {
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                GetTableData.Add(dt.Rows[0].ToString());
            }
            return GetTableData;
        }
    }
}