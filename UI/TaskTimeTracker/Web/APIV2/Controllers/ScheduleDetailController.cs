using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using DataModel.TaskTimeTracker.TimeTracking;
using Framework.Components.DataAccess;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using TaskTimeTracker.Components.Module.TimeTracking;


namespace Web.Api.Controllers
{
	//[Authorize]
	public class ScheduleDetailController : ApiController
	{
		// GET api/summary/GetList
		public IEnumerable<ScheduleDetailDataModel> GetList()
		{
			return ScheduleDetailDataManager.GetScheduleDetailList(SessionVariables.RequestProfile);
		}

		public IEnumerable<ScheduleDetailDataModel> GetList(string value)
		{
			var searchString = value;

			var dictionaryObject = JsonConvert.DeserializeObject<Dictionary<string, string>>(searchString);

			// save search filter parameters in user preference
			if (dictionaryObject != null)
			{
				foreach (var searchFilterColumnName in dictionaryObject.Keys)
				{
					var searchFilterValue = dictionaryObject[searchFilterColumnName];
					//PreferenceUtility.UpdateUserPreference(settingCategory, searchFilterColumnName, dictionaryObject[searchFilterColumnName]);
				}
			}

			var dataQuery = JsonConvert.DeserializeObject<ScheduleDetailDataModel>(searchString);

			dataQuery.ScheduleDetailId	= dataQuery.ScheduleDetailId < 0 ? null : dataQuery.ScheduleDetailId;
			dataQuery.ScheduleId		= dataQuery.ScheduleId < 0 ? null : dataQuery.ScheduleId;
			dataQuery.PersonId			= dataQuery.PersonId < 0 ? null : dataQuery.PersonId;
			
			return ScheduleDetailDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);
		
		}

		public IEnumerable<ScheduleDetailDataModel> GetList(string value, string value1)
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
					PreferenceUtility.UpdateUserPreference(settingCategory, searchFilterColumnName, dictionaryObject[searchFilterColumnName]);
				}
			}

			var dataQuery = JsonConvert.DeserializeObject<ScheduleDetailDataModel>(searchString);

			dataQuery.ScheduleDetailId = dataQuery.ScheduleDetailId < 0 ? null : dataQuery.ScheduleDetailId;
			dataQuery.ScheduleId = dataQuery.ScheduleId < 0 ? null : dataQuery.ScheduleId;
			dataQuery.PersonId = dataQuery.PersonId < 0 ? null : dataQuery.PersonId;

			return ScheduleDetailDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);
		}

		[System.Web.Http.AcceptVerbs("GET")]
		public IEnumerable<ScheduleDetailDataModel> GetListByApplication(string value)
		{
			var dataQuery = new ScheduleDetailDataModel();

		    dataQuery.ApplicationId = 100047;

			return ScheduleDetailDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);
		}

		[System.Web.Http.AcceptVerbs("GET")]
		public IEnumerable<ScheduleDetailDataModel> GetListByApplication()
		{
			var dataQuery = new ScheduleDetailDataModel();

			dataQuery.ApplicationId = 100047;

			return ScheduleDetailDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);
		}


		public ScheduleDetailDataModel GetById(string value)
		{
			var dataQuery = new ScheduleDetailDataModel();

			dataQuery.ScheduleDetailId = int.Parse(value);

			var result = ScheduleDetailDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile, 1);

			return result[0];
		}

		public DataTable GetHierarchyList(string value, string value1)
		{
			var sourceEntity = new JavaScriptSerializer().Deserialize<string>(value);
			var sourceEntityValue = new JavaScriptSerializer().Deserialize<string>(value1);

			var lst = ScheduleDetailDataManager.GetHierarchyList(sourceEntity, sourceEntityValue);
			return lst;
		}

		public DataTable GetHierarchySourceList(string value)
		{
			var lst = ScheduleDetailDataManager.GetHierarchySourceList(value);
			return lst;
		}

		public DataTable GetScheduleDetailDistinctList()
		{
			var lst = ScheduleDetailDataManager.GetScheduleDetailDistinctList();
			return lst;
		}

		public void Create([FromBody]ScheduleDetailDataModel data)
		{
			ScheduleDetailDataManager.Create(data, SessionVariables.RequestProfile);
		}

		public void Update([FromBody]ScheduleDetailDataModel data)
		{
			ScheduleDetailDataManager.Update(data, SessionVariables.RequestProfile);
		}

		[System.Web.Http.AcceptVerbs("DELETE", "GET", "POST")]
		public void Delete(string value)
		{
			var dataQuery = new ScheduleDetailDataModel();
			dataQuery.ScheduleDetailId = int.Parse(value);
			ScheduleDetailDataManager.Delete(dataQuery, SessionVariables.RequestProfile);
		}

		[System.Web.Http.AcceptVerbs("DELETE", "GET", "POST")]
		public JObject RemoveEntityRecords(string value1, string value2, string value3)
		{
			var sourceEntity = new JavaScriptSerializer().Deserialize<string>(value1);
			var targetEntityList = new JavaScriptSerializer().Deserialize<List<string>>(value3);

			var parentWorkTicket = string.Empty;
			var workTicket = string.Empty;

			foreach (var tmpValue in targetEntityList)
			{
				if (sourceEntity == "ParentWorkTicket")
				{
					parentWorkTicket = new JavaScriptSerializer().Deserialize<string>(value2); ;
					workTicket = tmpValue;
				}
				else if (sourceEntity == "WorkTicket")
				{
					workTicket = new JavaScriptSerializer().Deserialize<string>(value2); ;
					parentWorkTicket = tmpValue;
				}

				ScheduleDetailDataManager.DeleteWorkTicketHierarchy(parentWorkTicket, workTicket);	
			}		

			var jObject = new JObject();
			jObject["Result"] = true;
			return jObject;
		}

		[System.Web.Http.AcceptVerbs("DELETE", "GET", "POST")]
		public JObject AddEntityRecords(string value1, string value2, string value3)
		{
			var sourceEntity = new JavaScriptSerializer().Deserialize<string>(value1);
			var sourceEntityId = new JavaScriptSerializer().Deserialize<string>(value2);
			var targetEntityList = new JavaScriptSerializer().Deserialize<List<string>>(value3);

			foreach (var tmpValue in targetEntityList)
			{
				var parentWorkTicket = string.Empty;
				var workTicket = string.Empty;

				if (sourceEntity == "ParentWorkTicket")
				{
					parentWorkTicket = sourceEntityId;
					workTicket = tmpValue;
				}
				else if (sourceEntity == "WorkTicket")
				{
					workTicket = sourceEntityId;
					parentWorkTicket = tmpValue;
				}

				ScheduleDetailDataManager.CreateWorkTicketHierarchy(parentWorkTicket, workTicket, SessionVariables.RequestProfile);
			}

			var jObject = new JObject();
			jObject["Result"] = true;
			return jObject;
		}


	}
}