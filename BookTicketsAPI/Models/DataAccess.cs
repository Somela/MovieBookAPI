using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace BookTicketsAPI.Models
{
    public class DataAccess
    {
        ReuseClass oReuse = new ReuseClass();
        string Query = "";
        public string CommingSoon(string Type, List<string> Selected)
        {
            switch (Type)
            {
                case "Language":
                    {
                        if (Selected.Count() > 0)
                        {
                            Query = AddCondition(Selected, "MovieLanguageSOON");
                        }
                        break;
                    }
                case "Genre":
                    {
                        if (Selected.Count() > 0)
                        {
                            Query = AddCondition(Selected, "MovieGenSOON");
                        }
                        break;
                    }
                case "Formate":
                    {
                        if (Selected.Count() > 0)
                        {
                            Query = AddFormatCondition(Selected, "MovieFormatSOON");
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            return Query;
        }
        public string NowPlaying(string Type, List<string> Selected)
        {
            switch (Type)
            {
                case "Language":
                    {
                        if (Selected.Count() > 0)
                        {
                            Query = AddCondition(Selected, "MovieLanguage");
                        }
                        break;
                    }
                case "Genre":
                    {
                        if (Selected.Count() > 0)
                        {
                            Query = AddCondition(Selected, "MovieGen");
                        }
                        break;
                    }
                case "Formate":
                    {
                        if (Selected.Count() > 0)
                        {
                            Query = AddFormatCondition(Selected, "MovieFormat");
                        }
                        break;
                    }
                default:
                    {
                        if (Selected.Count() > 0)
                        {
                            Query = AddFormatCondition(Selected, "MovieFormat");
                        }
                        break;
                    }
            }
            return Query;
        }
        public DataTable CallNowPlaying(string type)
        {
            DataTable dt = new DataTable();
            switch (type)
            {
                case "Language":
                    {
                        Query = getQuery("LANGUAGETABLE");
                        dt=oReuse.GetDBData(Query);           
                    }
                    break;
                case "Genre":
                    {
                        Query = getQuery("GenreTable");
                        dt=oReuse.GetDBData(Query);
                    }
                    break;
                case "Formate":
                    {
                        Query = getQuery("FormatTable");
                        dt=oReuse.GetDBData(Query);
                    }
                    break;
                default:
                    break;
            }
            return dt;
        }
        public DataTable CallCommingSoon(string type)
        {
            DataTable dt = new DataTable();
            switch (type)
            {
                case "Language":
                    {
                        Query = getQuery("LANGUAGETABLE");
                        dt = oReuse.GetDBData(Query);
                    }
                    break;
                case "Genre":
                    {
                        Query = getQuery("GenreTable");
                        dt = oReuse.GetDBData(Query);
                    }
                    break;
                case "Formate":
                    {
                        Query = getQuery("FormatTable");
                        dt = oReuse.GetDBData(Query);
                    }
                    break;
                default:
                    break;
            }
            return dt;
        }
        public string getQuery(string table)
        {
            string query = "select * from " + table;
            return query;
        }
        
        public string AddCondition(List<string> conditions, string TableName)
        {
            string query = "SELECT * FROM " + TableName;
            string joiner = " WHERE ";
            int conValue = 1;
            foreach (string condition in conditions)
            {
                string[] lang = condition.Split(',');
                foreach(string language in lang)
                {
                    query += joiner + language + "=" + conValue;
                    joiner = " AND ";
                }
            }
            return query;
        }
        public string AddFormatCondition(List<string> conditions, string TableName)
        {
            string query = "SELECT * FROM " + TableName;
            string joiner = " WHERE ";
            int conValue = 1;
            foreach (string condition in conditions)
            {
                string[] lang = condition.Split(',');
                foreach (string language in lang)
                {
                    query += joiner + '"'+language+'"' + "=" + conValue;
                    joiner = " AND ";
                }
            }
            return query;
        }
    }
}