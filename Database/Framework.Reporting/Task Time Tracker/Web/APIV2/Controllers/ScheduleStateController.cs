using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
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

		[System.Web.Http.AcceptVerbs("GET")]
		public IEnumerable<ScheduleStateDataModel> GetListByApplication(string value)
		{
			var dataQuery = new ScheduleStateDataModel();

			dataQuery.Name = value;

			dataQuery.ApplicationId = 100047;

			return ScheduleStateDataManager.GetEntityDetails(dataQuery,SessionVariables.RequestProfile);
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
    }
}