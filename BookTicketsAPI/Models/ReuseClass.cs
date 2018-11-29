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
            foreach (string condition in conditions)
            {
                string query = "SELECT * FROM " + TableName;
                string MovieName = "MovieName";
                string joiner = " WHERE ";
                query = query +joiner+ MovieName + "=" + "'" + condition + "'";
                SelectQuery.Add(query);
                //query += joiner + MovieName + "=" + "'" + condition + "'";
                //string[] lang = condition.Split(',');
                //foreach (string language in lang)
                //{
                //    query += joiner + MovieName + "=" + "'" + language + "'";
                //    SelectQuery.Add(query);
                //}
            }
            return SelectQuery;
        }
        DataModel.GetData getInfo = new DataModel.GetData();
        public List<object> ReturnTable(List<string> queries)
        {
            string Con = ConfigurationManager.ConnectionStrings["ConnectionDB"].ConnectionString;
            List<string> GetTable;
            List<object> GetTableData = new List<object>();
            List<DataModel.GetData> Info = new List<DataModel.GetData>();
            
            SqlConnection con = new SqlConnection(Con);
            foreach (string query in queries)
            {
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GetTable = new List<string>();
                DataRow row = dt.Rows[0];
                for (int i = 0; i < row.ItemArray.Length; i++)
                {
                    GetTable.Add(row.ItemArray[i].ToString());
                }
                GetTableData.Add(new DataModel.GetData()
                {
                    MovieName = row.ItemArray[1].ToString(),
                    NowRunning = row.ItemArray[2].ToString(),
                    CurrentRunning = row.ItemArray[3].ToString(),
                    Exclusive = row.ItemArray[4].ToString(),
                    Duration = row.ItemArray[5].ToString(),
                    ReleaseData = row.ItemArray[6].ToString()

                });
            }
            return GetTableData;
        }
    }
}