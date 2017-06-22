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
using DataModel.TaskTimeTracker.TimeTracking;
using TaskTimeTracker.Components.Module.TimeTracking;
using Newtonsoft.Json.Linq;

namespace Web.Api.Controllers
{
    //[Authorize]
    public class CustomTimeLogController : ApiController
    {

        // GET api/summary/GetList
        public IEnumerable<CustomTimeLogDataModel> GetList(string value, string value1)
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

            var dataQuery = JsonConvert.DeserializeObject<CustomTimeLogDataModel>(searchString);
            return CustomTimeLogDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);
        }

        public IEnumerable<CustomTimeCategoryDataModel> GetListCustomTimeCategory()
        {
            var dataQuery = new CustomTimeCategoryDataModel();
            return CustomTimeCategoryDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);
        }

        public CustomTimeLogDataModel GetById(string value)
        {

            var dataQuery = new CustomTimeLogDataModel();
            dataQuery.CustomTimeLogId = int.Parse(value);
            var result = CustomTimeLogDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile, 1);
            return result[0];

        }

        [HttpPost]
        public void Create([FromBody]dynamic data)
        {
            var jsonString = JsonConvert.SerializeObject(data);
            var dataQuery = JsonConvert.DeserializeObject<CustomTimeLogDataModel>(jsonString);

            dataQuery.PromotedDate = DateTime.Now;
            dataQuery.ApplicationId = SessionVariables.RequestProfile.ApplicationId;

            CustomTimeLogDataManager.Create(dataQuery, SessionVariables.RequestProfile);
        }

        [HttpPost]
        public void Update([FromBody]dynamic data)
        {
            var jsonString = JsonConvert.SerializeObject(data);
            var dataQuery = JsonConvert.DeserializeObject<CustomTimeLogDataModel>(jsonString);
            CustomTimeLogDataManager.Update(dataQuery, SessionVariables.RequestProfile);
        }

        [System.Web.Http.AcceptVerbs("DELETE", "GET", "POST")]
        public void Delete(string value)
        {
            var dataQuery = new CustomTimeLogDataModel();
            dataQuery.CustomTimeLogId = int.Parse(value);
            CustomTimeLogDataManager.Delete(dataQuery, SessionVariables.RequestProfile);
        }

    }
}
