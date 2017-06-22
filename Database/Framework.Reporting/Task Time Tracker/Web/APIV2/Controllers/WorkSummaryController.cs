using DataModel.Framework.Core;
using Framework.Components.Core;
using Shared.WebCommon.UI.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Data;
using Newtonsoft.Json;
using System.Text;
using DataModel.Framework.AuthenticationAndAuthorization;
using Framework.Components.ApplicationUser;
using DataModel.TaskTimeTracker.TimeTracking;
using TaskTimeTracker.Components.Module.TimeTracking;
using System.Dynamic;
using Newtonsoft.Json.Linq;

namespace Web.Api.Controllers
{
    
    public class WorkSummaryController : ApiController
    {
        
        [System.Web.Http.AcceptVerbs("GET")]
        public IEnumerable<ApplicationUserDataModel> ListUsers()
        {
            var data = new ApplicationUserDataModel();
            data.ApplicationId = SessionVariables.RequestProfile.ApplicationId;
            var listUsers = ApplicationUserDataManager.GetEntityDetails(data, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfo);
            return listUsers;
        }

        public List<WorkCategoryRecord> GetWorkCategoryReportData(string value1, string value2, string value3)
        {
            var applicationUserId = value1;
            var fromSearchDate = value2;
            var toSearchDate = value3;

            var dateFormat = "dd.MM.yyyy";

            var scheduleData = new ScheduleDetailDataModel();

            scheduleData.PersonId = int.Parse(applicationUserId);
            scheduleData.FromSearchDate = DateTime.ParseExact(fromSearchDate, dateFormat, System.Globalization.DateTimeFormatInfo.InvariantInfo);
            scheduleData.ToSearchDate = DateTime.ParseExact(toSearchDate, dateFormat, System.Globalization.DateTimeFormatInfo.InvariantInfo);

            var lstResult = ScheduleDetailDataManager.GetWorkCategoryData(scheduleData, SessionVariables.RequestProfile);
            return lstResult;
        }

        public List<BranchRecord> GetBranchSummaryReportData(string value1, string value2, string value3)
        {
            var applicationUserId = value1;
            var fromSearchDate = value2;
            var toSearchDate = value3;

            var dateFormat = "dd.MM.yyyy";

            var customTimeLogData = new CustomTimeLogDataModel();

            customTimeLogData.PersonId = int.Parse(applicationUserId);
            customTimeLogData.FromSearchDate = DateTime.ParseExact(fromSearchDate, dateFormat, System.Globalization.DateTimeFormatInfo.InvariantInfo);
            customTimeLogData.ToSearchDate = DateTime.ParseExact(toSearchDate, dateFormat, System.Globalization.DateTimeFormatInfo.InvariantInfo);

            var lstResult = CustomTimeLogDataManager.GetBranchSummaryReportData(customTimeLogData, SessionVariables.RequestProfile);
            return lstResult;
        }

        [System.Web.Http.AcceptVerbs("GET")]
        public JObject GetStoredSearchData(string value)
        {
            var settingCategoryName = value;

            var fields = new List<string>(){ 
                    "ApplicationUserId", 
                    "FromSearchDate", 
                    "ToSearchDate"};

            var jObject = new JObject();

            foreach (var fieldName in fields)
            {
                jObject[fieldName] = PerferenceUtility.GetUserPreferenceByKey(fieldName, settingCategoryName);
            }

            return jObject;

        }

        [System.Web.Http.AcceptVerbs("GET")]
        public bool SetStoredSearchData(string value, string value1)
        {
            var settingCategoryName = value;

            var fields = new List<string>(){ 
                    "ApplicationUserId",
                    "FromSearchDate", 
                    "ToSearchDate"};

            var storedValues = JsonConvert.DeserializeObject<Dictionary<string, string>>(value1);

            foreach (var fieldName in fields)
            {
                PerferenceUtility.UpdateUserPreference(settingCategoryName, fieldName, storedValues[fieldName]);
            }

            return true;

        }

    }

}
