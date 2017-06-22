using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using DataModel.TaskTimeTracker.TimeTracking;
using Framework.Components.DataAccess;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.Module.TimeTracking;


namespace Web.Api.Controllers
{
    //[Authorize]
	public class ScheduleStateController : ApiController
	{
		// GET api/summary/GetList
		public IEnumerable<ScheduleStateDataModel> GetList()
		{
			return ScheduleStateDataManager.GetScheduleStateList(SessionVariables.RequestProfile);
		}

		public IEnumerable<ScheduleStateDataModel> GetList(string value, string value1)
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

			var dataQuery = JsonConvert.DeserializeObject<ScheduleStateDataModel>(searchString);
			return ScheduleStateDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);
		}

		[System.Web.Http.AcceptVerbs("GET")]
		public IEnumerable<ScheduleStateDataModel> GetListByApplication(string value)
		{
			var dataQuery = new ScheduleStateDataModel();

			dataQuery.Name = value;

			dataQuery.ApplicationId = 100047;

			return ScheduleStateDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);
		}

		[System.Web.Http.AcceptVerbs("GET")]
		public IEnumerable<ScheduleStateDataModel> GetListByApplication()
		{
			var dataQuery = new ScheduleStateDataModel();

			dataQuery.ApplicationId = 100047;

			return ScheduleStateDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);
		}

		
		public ScheduleStateDataModel GetById(string value)
		{
			var dataQuery = new ScheduleStateDataModel();

			dataQuery.ScheduleStateId = int.Parse(value);

			var result = ScheduleStateDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile, 1);

			return result[0];
		}

		public void Create([FromBody]ScheduleStateDataModel data)
		{
			ScheduleStateDataManager.Create(data, SessionVariables.RequestProfile);
		}

		public void Update([FromBody]ScheduleStateDataModel data)
		{
			ScheduleStateDataManager.Update(data, SessionVariables.RequestProfile);
		}

		[System.Web.Http.AcceptVerbs("DELETE", "GET", "POST")]
		public void Delete(string value)
		{
			var dataQuery = new ScheduleStateDataModel();
			dataQuery.ScheduleStateId = int.Parse(value);
			ScheduleStateDataManager.Delete(dataQuery, SessionVariables.RequestProfile);
		}
	}
}