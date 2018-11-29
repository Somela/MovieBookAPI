using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookTicketsAPI.Models
{
    public class DataAccess
    {
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