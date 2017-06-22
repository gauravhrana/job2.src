using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Shared.WebCommon.UI.Web;
using Newtonsoft.Json;
using DataModel.CapitalMarkets;
using CapitalMarkets.Components.BusinessLayer;
using Framework.Components.DataAccess;
using Framework.Components.UserPreference;
using Shared.UI.Web;
using System.Web.Http;

namespace Web.Api.Controllers
{
	//[Authorize]
	public class TxTradeFiguresController : ApiController
	{

		// GET api/summary/GetList
		public IEnumerable<TxTradeFiguresDataModel> GetList(string value, string value1)
		{
			var settingCategory = value1;
			var searchString = value;

			var dictionaryObject = JsonConvert.DeserializeObject<Dictionary<string, string>>(searchString);

			var dataQuery = JsonConvert.DeserializeObject<TxTradeFiguresDataModel>(value);

			// save search filter parameters in user preference
			if (dictionaryObject != null)
			{
				foreach (var searchFilterColumnName in dictionaryObject.Keys)
				{
					var searchFilterValue = dictionaryObject[searchFilterColumnName];
					PreferenceUtility.UpdateUserPreference(settingCategory, searchFilterColumnName, dictionaryObject[searchFilterColumnName]);
				}
			}

			return TxTradeFiguresDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);
		}

		public TxTradeFiguresDataModel GetById(string value)
		{
			var dataQuery = new TxTradeFiguresDataModel();
			dataQuery.TxTradeFiguresId = int.Parse(value);

			var result = TxTradeFiguresDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile, 1);
			return result[0];
		}

		[HttpPost]
		public void Create([FromBody]dynamic data)
		{
			var jsonString = JsonConvert.SerializeObject(data);
			var dataCreate = JsonConvert.DeserializeObject<TxTradeFiguresDataModel>(jsonString);

			dataCreate.ApplicationId = SessionVariables.RequestProfile.ApplicationId;

			TxTradeFiguresDataManager.Create(dataCreate, SessionVariables.RequestProfile);
		}

		[HttpPost]
		public void Update([FromBody]dynamic data)
		{
			var jsonString = JsonConvert.SerializeObject(data);
			var dataUpdate = JsonConvert.DeserializeObject<TxTradeFiguresDataModel>(jsonString);
			TxTradeFiguresDataManager.Update(dataUpdate, SessionVariables.RequestProfile);
		}

		[System.Web.Http.AcceptVerbs("DELETE", "GET", "POST")]
		public void Delete(string value)
		{
			var dataDelete = new TxTradeFiguresDataModel();
			dataDelete.TxTradeFiguresId = int.Parse(value);
			TxTradeFiguresDataManager.Delete(dataDelete, SessionVariables.RequestProfile);
		}
	}
}
