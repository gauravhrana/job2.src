using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Shared.WebCommon.UI.Web;
using Newtonsoft.Json;
using DataModel.DayCare;
using DayCare.Components.BusinessLayer;
using Framework.Components.DataAccess;
using Framework.Components.UserPreference;

namespace Web.Api.Controllers
{
	//[Authorize]
	public class CommentController : ApiController
	{

		// GET api/summary/GetList
		public IEnumerable<CommentDataModel> GetList(string value, string value1)
		{
			var settingCategory = value1;
			var searchString = value;

			var dictionaryObject = JsonConvert.DeserializeObject<Dictionary<string, string>>(searchString);

			// save search filter parameters in user preference
			if (dictionaryObject != null)
			{
				foreach (var searchFilterColumnName in dictionaryObject.Keys)
				{
					var searchFilterValue = dictionaryObject[searchFilterColumnName];
					PerferenceUtility.UpdateUserPreference(settingCategory, searchFilterColumnName, dictionaryObject[searchFilterColumnName]);
				}
			}

			var dataQuery = JsonConvert.DeserializeObject<CommentDataModel>(searchString);
			return CommentDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);
		}

		public CommentDataModel GetById(string value)
		{

			var dataQuery = new CommentDataModel();
			dataQuery.CommentId = int.Parse(value);
			var result = CommentDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile, 1);
			return result[0];

		}

		public void Create([FromBody]CommentDataModel data)
		{
			CommentDataManager.Create(data, SessionVariables.RequestProfile);
		}

		public void Update([FromBody]CommentDataModel data)
		{
			CommentDataManager.Update(data, SessionVariables.RequestProfile);
		}

		[System.Web.Http.AcceptVerbs("DELETE", "GET", "POST")]
		public void Delete(string value)
		{
			var dataQuery = new CommentDataModel();
			dataQuery.CommentId = int.Parse(value);
			CommentDataManager.Delete(dataQuery, SessionVariables.RequestProfile);
		}
	}
}
