using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Newtonsoft.Json;
using BookTicketsAPI.Models;



namespace BookTicketsAPI.Controllers
{
    public class HomeController : ApiController
    {
        DataAccess model = new DataAccess();
        DataModel.GetCreditCardInfoResponse oMovie = new DataModel.GetCreditCardInfoResponse();
        DataModel.GetMovieDetailsResponse oResponse = new DataModel.GetMovieDetailsResponse();
        ReuseClass oReuse = new ReuseClass();
        string Query = "";
        [HttpPost,Route("GetMovie")]
        public IHttpActionResult sendDataToTable(DataModel.GetMovieInfoRequest getObj)
        {
            DataTable dt = new DataTable();
            DataTable dtMovie = new DataTable();
            try
            {
                oMovie.MovieName = new List<string>();
                oResponse.MovieDetails = new List<string>();
                List<string> MovieFullDetails = new List<string>();
                List<string> MovieQuery = new List<string>();
                switch (getObj.PageName)
                {
                    case "NowPlaying":
                        {
                            Query = model.NowPlaying(getObj.Type, getObj.Selected);
                            dt = GetDBData(Query);
                            foreach (DataRow dr in dt.Rows)
                            {
                                string movieName = dr["MovieName"].ToString();
                                oMovie.MovieName.Add(movieName);
                            }
                            MovieQuery = oReuse.GetMovieDetails(oMovie.MovieName, "MOVIESNAVBAR");
                            MovieFullDetails = oReuse.ReturnTable(MovieQuery);
                        }
                        break;
                    case "CommingSoon":
                        {
                            Query = model.CommingSoon(getObj.Type, getObj.Selected);
                            dt = GetDBData(Query);
                            foreach (DataRow dr in dt.Rows)
                            {
                                string movieName = dr["MovieName"].ToString();
                                oMovie.MovieName.Add(movieName);
                            }
                        }
                        break;
                    default:
                        break;
                }
                return Ok(MovieFullDetails);  //return MovieName with Ok status      
            }
            catch(Exception ex)
            {
                oResponse.Response = new DataModel.ResponseMessage();
                oResponse.Response.Code = 400;
                oResponse.Response.Message = ex.Message;
                oResponse.Response.Severity = "error";
                Log.LogClass.LogMessage("sendDataToTable Method : " + ex.Message, Log.LogClass.LogLevel.ERROR);
                return Content(HttpStatusCode.BadRequest, oResponse);
            }
        }
        public DataTable GetDBData(string Query)
        {
            string Con = ConfigurationManager.ConnectionStrings["ConnectionDB"].ConnectionString;
            SqlConnection con = new SqlConnection(Con);
            SqlCommand cmd = new SqlCommand(Query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }
    }
}