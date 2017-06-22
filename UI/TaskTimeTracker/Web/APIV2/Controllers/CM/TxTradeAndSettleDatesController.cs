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
	public class TxTradeAndSettleDatesController : ApiController
	{

		// GET api/summary/GetList
		public IEnumerable<TxTradeAndSettleDatesDataModel> GetList(string value, string value1)
		{
			var settingCategory = value1;
			var searchString = value;

			var dictionaryObject = JsonConvert.DeserializeObject<Dictionary<string, string>>(searchString);

			var TradeDateValue = string.Empty;
			if (dictionaryObject.Keys.Contains("TradeDate"))
			{
				TradeDateValue = dictionaryObject["TradeDate"];
				dictionaryObject["TradeDate"] = string.Empty;
			}

			var ContractualDateValue = string.Empty;
			if (dictionaryObject.Keys.Contains("ContractualDate"))
			{
				ContractualDateValue = dictionaryObject["ContractualDate"];
				dictionaryObject["ContractualDate"] = string.Empty;
			}

			var ActualDateValue = string.Empty;
			if (dictionaryObject.Keys.Contains("ActualDate"))
			{
				ActualDateValue = dictionaryObject["ActualDate"];
				dictionaryObject["ActualDate"] = string.Empty;
			}

			value = JsonConvert.SerializeObject(dictionaryObject);

			var dataQuery = JsonConvert.DeserializeObject<TxTradeAndSettleDatesDataModel>(value);
			if (dictionaryObject.Keys.Contains("TradeDate"))
			{
				var tmpArr = TradeDateValue.Split(new char[]{ '&' });

				var fromDateValue = tmpArr[0];
				var toDateValue = tmpArr[1];
				var preFilledItem = tmpArr[2];

				if (preFilledItem != "Custom" && string.IsNullOrEmpty(fromDateValue) && string.IsNullOrEmpty(toDateValue))
				{
					var ranges = DateRangeHelper.FillUpDate(value, SessionVariables.UserDateFormat);
					fromDateValue = ranges[0];
					toDateValue = ranges[1];
				}

				dataQuery.FromSearchTradeDate = DateTimeHelper.FromUserDateFormatToDate(fromDateValue);
				dataQuery.ToSearchTradeDate = DateTimeHelper.FromUserDateFormatToDate(toDateValue);
				dictionaryObject["TradeDate"] = DateTimeHelper.FromUserDateFormatToApplicationDateFormat(fromDateValue) + "&"
													+ DateTimeHelper.FromUserDateFormatToApplicationDateFormat(toDateValue) + "&"
													+ preFilledItem + "&";
			}
			if (dictionaryObject.Keys.Contains("ContractualDate"))
			{
				var tmpArr = ContractualDateValue.Split(new char[]{ '&' });

				var fromDateValue = tmpArr[0];
				var toDateValue = tmpArr[1];
				var preFilledItem = tmpArr[2];

				if (preFilledItem != "Custom" && string.IsNullOrEmpty(fromDateValue) && string.IsNullOrEmpty(toDateValue))
				{
					var ranges = DateRangeHelper.FillUpDate(value, SessionVariables.UserDateFormat);
					fromDateValue = ranges[0];
					toDateValue = ranges[1];
				}

				dataQuery.FromSearchContractualDate = DateTimeHelper.FromUserDateFormatToDate(fromDateValue);
				dataQuery.ToSearchContractualDate = DateTimeHelper.FromUserDateFormatToDate(toDateValue);
				dictionaryObject["ContractualDate"] = DateTimeHelper.FromUserDateFormatToApplicationDateFormat(fromDateValue) + "&"
													+ DateTimeHelper.FromUserDateFormatToApplicationDateFormat(toDateValue) + "&"
													+ preFilledItem + "&";
			}
			if (dictionaryObject.Keys.Contains("ActualDate"))
			{
				var tmpArr = ActualDateValue.Split(new char[]{ '&' });

				var fromDateValue = tmpArr[0];
				var toDateValue = tmpArr[1];
				var preFilledItem = tmpArr[2];

				if (preFilledItem != "Custom" && string.IsNullOrEmpty(fromDateValue) && string.IsNullOrEmpty(toDateValue))
				{
					var ranges = DateRangeHelper.FillUpDate(value, SessionVariables.UserDateFormat);
					fromDateValue = ranges[0];
					toDateValue = ranges[1];
				}

				dataQuery.FromSearchActualDate = DateTimeHelper.FromUserDateFormatToDate(fromDateValue);
				dataQuery.ToSearchActualDate = DateTimeHelper.FromUserDateFormatToDate(toDateValue);
				dictionaryObject["ActualDate"] = DateTimeHelper.FromUserDateFormatToApplicationDateFormat(fromDateValue) + "&"
													+ DateTimeHelper.FromUserDateFormatToApplicationDateFormat(toDateValue) + "&"
													+ preFilledItem + "&";
			}

			// save search filter parameters in user preference
			if (dictionaryObject != null)
			{
				foreach (var searchFilterColumnName in dictionaryObject.Keys)
				{
					var searchFilterValue = dictionaryObject[searchFilterColumnName];
					PreferenceUtility.UpdateUserPreference(settingCategory, searchFilterColumnName, dictionaryObject[searchFilterColumnName]);
				}
			}

			return TxTradeAndSettleDatesDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);
		}

		public TxTradeAndSettleDatesDataModel GetById(string value)
		{
			var dataQuery = new TxTradeAndSettleDatesDataModel();
			dataQuery.TxTradeAndSettleDatesId = int.Parse(value);

			var result = TxTradeAndSettleDatesDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile, 1);
			return result[0];
		}

		[HttpPost]
		public void Create([FromBody]dynamic data)
		{
			var jsonString = JsonConvert.SerializeObject(data);
			var dataCreate = JsonConvert.DeserializeObject<TxTradeAndSettleDatesDataModel>(jsonString);

			dataCreate.ApplicationId = SessionVariables.RequestProfile.ApplicationId;

			TxTradeAndSettleDatesDataManager.Create(dataCreate, SessionVariables.RequestProfile);
		}

		[HttpPost]
		public void Update([FromBody]dynamic data)
		{
			var jsonString = JsonConvert.SerializeObject(data);
			var dataUpdate = JsonConvert.DeserializeObject<TxTradeAndSettleDatesDataModel>(jsonString);
			TxTradeAndSettleDatesDataManager.Update(dataUpdate, SessionVariables.RequestProfile);
		}

		[System.Web.Http.AcceptVerbs("DELETE", "GET", "POST")]
		public void Delete(string value)
		{
			var dataDelete = new TxTradeAndSettleDatesDataModel();
			dataDelete.TxTradeAndSettleDatesId = int.Parse(value);
			TxTradeAndSettleDatesDataManager.Delete(dataDelete, SessionVariables.RequestProfile);
		}
	}
}
