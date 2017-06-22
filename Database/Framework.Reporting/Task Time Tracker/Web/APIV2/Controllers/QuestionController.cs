using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Shared.WebCommon.UI.Web;
using Newtonsoft.Json;
using DataModel.TaskTimeTracker;
using TaskTimeTracker.Components.BusinessLayer;
using Framework.Components.DataAccess;


namespace Web.Api.Controllers
{
    public class QuestionController : ApiController
    {

        // GET api/summary/GetList
        public IEnumerable<QuestionDataModel> GetList(string value, string value1)
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
            var dataQuery = new QuestionDataModel();

            try
            {
                dataQuery = JsonConvert.DeserializeObject<QuestionDataModel>(searchString);
            }
            catch { }

            // igonore when value is -1
            if (dataQuery.QuestionCategoryId == -1)
            {
                dataQuery.QuestionCategoryId = null;
            }
            return QuestionDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);
       
        }

    }
}
