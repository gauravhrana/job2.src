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
	public class ScheduleDetailActivityCategoryController : ApiController
	{
		// GET api/summary/GetList
		public IEnumerable<ScheduleDetailActivityCategoryDataModel> GetList()
		{
			return ScheduleDetailActivityCategoryDataManager.GetList(SessionVariables.RequestProfile);
		}

		public IEnumerable<ScheduleDetailActivityCategoryDataModel> GetList(string value, string value1)
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

			var dataQuery = JsonConvert.DeserializeObject<ScheduleDetailActivityCategoryDataModel>(searchString);
			return ScheduleDetailActivityCategoryDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);
		}

		[System.Web.Http.AcceptVerbs("GET")]
		public IEnumerable<ScheduleDetailActivityCategoryDataModel> GetListByApplication(string value)
		{
			var dataQuery = new ScheduleDetailActivityCategoryDataModel();

			dataQuery.Name = value;

			dataQuery.ApplicationId = 100047;

			return ScheduleDetailActivityCategoryDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);
		}

		[System.Web.Http.AcceptVerbs("GET")]
		public IEnumerable<ScheduleDetailActivityCategoryDataModel> GetListByApplication()
		{
			var dataQuery = new ScheduleDetailActivityCategoryDataModel();

			dataQuery.ApplicationId = 100047;

			return ScheduleDetailActivityCategoryDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);
		}


		public ScheduleDetailActivityCategoryDataModel GetById(string value)
		{
			var dataQuery = new ScheduleDetailActivityCategoryDataModel();

			dataQuery.ScheduleDetailActivityCategoryId = int.Parse(value);

			var result = ScheduleDetailActivityCategoryDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile, 1);

			return result[0];
		}

		public void Create([FromBody]ScheduleDetailActivityCategoryDataModel data)
		{
			ScheduleDetailActivityCategoryDataManager.Create(data, SessionVariables.RequestProfile);
		}

		public void Update([FromBody]ScheduleDetailActivityCategoryDataModel data)
		{
			ScheduleDetailActivityCategoryDataManager.Update(data, SessionVariables.RequestProfile);
		}

		[System.Web.Http.AcceptVerbs("DELETE", "GET", "POST")]
		public void Delete(string value)
		{
			var dataQuery = new ScheduleDetailActivityCategoryDataModel();
			dataQuery.ScheduleDetailActivityCategoryId = int.Parse(value);
			ScheduleDetailActivityCategoryDataManager.Delete(dataQuery, SessionVariables.RequestProfile);
		}
	}
}