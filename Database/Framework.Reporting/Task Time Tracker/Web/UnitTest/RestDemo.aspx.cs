using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApplicationContainer.UI.Web.Areas.V45.Models;
using DataModel.DayCare;
using RestSharp;
using System.Net;
using DataModel.Framework.AuthenticationAndAuthorization;

namespace ApplicationContainer.UI.Web.UnitTest
{
	public partial class RestDemo : System.Web.UI.Page
	{

        private void EntityOperations(Cookie cookie)
        {
            var client = new RestClient("http://localhost:53331");
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            var cookieContainer = new CookieContainer();
            cookieContainer.Add(cookie);
            client.CookieContainer = cookieContainer;

            var data = new BathRoomDataModel();
            data.Name = "Test Rest";
            data.Description = "Test Rest";
            data.SortOrder = 1;

            // get existing record
            var request = new RestRequest("apiV2/rest/BathRooms/", Method.GET);
            request.RequestFormat = DataFormat.Json;

            // pass parameter in order to search based on data model class.
            //request.AddParameter("data", request.JsonSerializer.Serialize(data));

            var response = client.Execute<List<BathRoomDataModel>>(request);
            var name = response.Data;

            // if found existing record, delete it
            if (response.Data.Count > 0)
            {
                var searchResult = response.Data.Find(x => x.Name == "Test Rest");
                if (searchResult != null)
                {
                    var bathRoomId = searchResult.BathRoomId;

                    var requestDelete = new RestRequest("apiV2/rest/BathRooms", Method.DELETE) { RequestFormat = DataFormat.Json };
                    requestDelete.AddParameter("id", bathRoomId);

                    var responseDelete = client.Execute<BathRoomDataModel>(requestDelete);
                }
            }

            // post (insert/update record)
            //var requestPost = new RestRequest("apiV2/rest/BathRooms", Method.POST);

            //requestPost.AddObject(data);
            //var responsePost = client.Execute(requestPost);

            var requestPost2 = new RestRequest("apiV2/rest/BathRooms", Method.POST) { RequestFormat = DataFormat.Json };
            requestPost2.AddBody(data);

            var responsePost2 = client.Execute<BathRoomDataModel>(requestPost2);

            //if (responsePost2.StatusCode != HttpStatusCode.Created)
            //{
            //    throw new Exception(responsePost2.ErrorMessage);
            //}

        }

        private Cookie Login()
        {
            var client = new RestClient("http://localhost:53331");            

            var request = new RestRequest("apiV2/rest/Login", Method.GET);

            request.AddParameter("username", "test"); // adds to POST or URL querystring based on Method
            request.AddParameter("password", "test");

            // execute the request
            IRestResponse response = client.Execute(request);
            var content = response.Content;

            if (response.Content == "true")
            {
                var cookie = response.Cookies.FirstOrDefault();
                var resultCookie = new Cookie(cookie.Name, cookie.Value, cookie.Path, cookie.Domain);
                return resultCookie;
            }
            return null;
        }

        private void ChangeApplicationId(Cookie cookie)
        {
            var obj = new ApplicationDataModel();

            obj.ApplicationId = 200;
            obj.Code = "DC";

            var client = new RestClient("http://localhost:53331");

            var request = new RestRequest("apiV2/rest/ApplicationChanger/{id}", Method.PUT) { RequestFormat = DataFormat.Json };

            request.AddParameter("id", 0, ParameterType.UrlSegment);
            request.AddBody(obj);

            var cookieContainer = new CookieContainer();
            cookieContainer.Add(cookie);
            client.CookieContainer = cookieContainer;

            // execute the request
            IRestResponse response = client.Execute(request);
        }

		protected void Page_Load(object sender, EventArgs e)
		{            
            var cookie = Login();
            ChangeApplicationId(cookie);
            EntityOperations(cookie);
		}

	}
}