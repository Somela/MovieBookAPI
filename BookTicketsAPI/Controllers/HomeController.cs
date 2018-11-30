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
using System.Web.Script.Serialization;
using Newtonsoft.Json;

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
                List<object> MovieFullDetails = new List<object>();
                List<string> MovieQuery = new List<string>();
                JavaScriptSerializer jss = new JavaScriptSerializer();
                switch (getObj.PageName)
                {
                    case "NowPlaying":
                        {
                            Query = model.NowPlaying(getObj.Type, getObj.Selected);
                            dt = oReuse.GetDBData(Query);
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
                            dt = oReuse.GetDBData(Query);
                            foreach (DataRow dr in dt.Rows)
                            {
                                string movieName = dr["MovieName"].ToString();
                                oMovie.MovieName.Add(movieName);
                            }
                            MovieQuery = oReuse.GetMovieDetails(oMovie.MovieName, "MOVIESNAVBARSOON");
                            MovieFullDetails = oReuse.ReturnTable(MovieQuery);
                        }
                        break;
                    default:
                        break;
                }
                //return Ok(JsonConvert.SerializeObject(MovieFullDetails));  //return MovieName with Ok status      
                return Ok(MovieFullDetails);
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
        [HttpPost,Route("GetInfo")]
        public IHttpActionResult Details(DataModel.FormateRequest info)
        {
            DataTable dt = new DataTable();
            try
            {
                switch (info.PageName)
                {
                    case "NowPlaying":
                        {
                            dt = model.CallNowPlaying(info.type);
                        }
                        break;
                    case "CommingSoon":
                        {
                            dt = model.CallCommingSoon(info.type);
                        }
                        break;
                    default:
                        {
                            dt = model.CallNowPlaying(info.type);
                        }
                        break;
                }
                return Ok(dt);
            }catch(Exception ex)
            {
                oResponse.Response = new DataModel.ResponseMessage();
                oResponse.Response.Code = 400;
                oResponse.Response.Message = ex.Message;
                oResponse.Response.Severity = "error";
                Log.LogClass.LogMessage("sendDataToTable Method : " + ex.Message, Log.LogClass.LogLevel.ERROR);
                return Content(HttpStatusCode.BadRequest, oResponse);
            }
        }        
    }
}