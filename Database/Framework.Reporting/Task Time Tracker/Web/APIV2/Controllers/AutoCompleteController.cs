using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataModel.Framework.Core;
using Framework.Components.Core;
using Framework.Components.DataAccess;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;
using DataModel.TaskTimeTracker;
using TaskTimeTracker.Components.BusinessLayer;
using DataModel.Framework.Configuration;
using DataModel.TaskTimeTracker.TimeTracking;
using TaskTimeTracker.Components.Module.TimeTracking;
using DataModel.Framework.AuthenticationAndAuthorization;
using Framework.Components.ApplicationUser;
using DataModel.DayCare;
using DayCare.Components.BusinessLayer;
using CapitalMarkets.Components.BusinessLayer;
using DataModel.CapitalMarkets;

namespace Web.Api.Controllers
{
    public class AutoCompleteController : ApiController
    {

        // GET api/summary/GetList
        public IEnumerable<string> GetAutoCompleteList(string value, string value1)
        {
            var primaryEntity = value;
            var searchField = value1;
            var lstOptions = new List<string>();

            if (primaryEntity.Equals("MenuCategory", StringComparison.InvariantCultureIgnoreCase))
            {
                if (searchField == "Name")
                {
                    var dt = MenuCategoryDataManager.GetList(SessionVariables.RequestProfile);
                    lstOptions = (from row in dt.AsEnumerable()
                                select row["Name"].ToString().Trim()).ToList();
                }
            }
            else if (primaryEntity.Equals("Question", StringComparison.InvariantCultureIgnoreCase))
            {
                if (searchField == "QuestionPhrase")
                {
                    var dt = QuestionDataManager.GetList(SessionVariables.RequestProfile);
                    lstOptions = (from row in dt.AsEnumerable()
                                select row["QuestionPhrase"].ToString().Trim()).ToList();
                }
            }

            else if(primaryEntity.Equals("Log4Net", StringComparison.InvariantCultureIgnoreCase))
            {
                if (searchField == "Computer")
                {
                    lstOptions = Framework.Components.LogAndTrace.Log4NetDataManager.GetComputerDetails(SessionVariables.RequestProfile);
                }
                if (searchField == "ConnectionKey")
                {
                    lstOptions = Framework.Components.LogAndTrace.Log4NetDataManager.GetConnectionKeyList(SessionVariables.RequestProfile);
                }
            }

            return lstOptions;
        }

        public IEnumerable<FieldConfigurationDataModel> GetGroupByList(string value)
        {
            var entityName = value;

            var systemEntityTypeId        = (SystemEntity)Enum.Parse(typeof(SystemEntity), entityName, true);
            var result                    = new List<FieldConfigurationDataModel>();

            var data                      = new FieldConfigurationDataModel();
            data.SystemEntityTypeId       = (int)systemEntityTypeId;
            data.FieldConfigurationModeId = SessionVariables.SearchControlColumnsModeId;

            var items = FieldConfigurationDataManager.GetFieldConfigurationList(data, SessionVariables.RequestProfile);

            var LstGroupByItems = new List<string>();

            // method used to get the list of items not required to be bound to the GroupBy dropdownlist
            if (entityName == "ReleaseLogDetail")
            {
                for (var i = 0; i < items.Count(); i++)
                {
                    var item = items[i];

                    if (item.Name.Equals("UpdatedRange"))
                    {
                        LstGroupByItems.Add(item.Name);
                    }
                }
            }

            var oData                           = new FieldConfigurationDataModel();
            oData.FieldConfigurationDisplayName = "None";
            oData.Name                          = "-1";
            result.Add(oData);

            foreach(var item in items)
            {
                if (!item.Name.Contains("GroupBy") && !LstGroupByItems.Contains(item.Name))
                {
                    oData                               = new FieldConfigurationDataModel();
                    oData.Name                          = item.Name.ToString();
                    oData.FieldConfigurationDisplayName = item.FieldConfigurationDisplayName;
                    result.Add(oData);
                }
            }

            return result.OrderBy(o => o.Name).ToList();
        }

        public IEnumerable<QuestionCategoryDataModel> GetQuestionCategoryList()
        {
            var items = QuestionCategoryDataManager.GetEntityDetails(QuestionCategoryDataModel.Empty, SessionVariables.RequestProfile);
            return items;
        }

        public IEnumerable<CustomTimeCategoryDataModel> GetCustomTimeCategoryList()
        {
            var items = CustomTimeCategoryDataManager.GetEntityDetails(CustomTimeCategoryDataModel.Empty, SessionVariables.RequestProfile);
            return items;
        }

        public IEnumerable<ApplicationUserDataModel> GetApplicationUserList()
        {
            var items = ApplicationUserDataManager.GetEntityDetails(ApplicationUserDataModel.Empty, SessionVariables.RequestProfile);
            return items;
        }

        public IEnumerable<ApplicationUserDataModel> GetPersonList()
        {
            var items = ApplicationUserDataManager.GetEntityDetails(ApplicationUserDataModel.Empty, SessionVariables.RequestProfile);
            return items;
        }

        public IEnumerable<EventTypeDataModel> GetEventTypeList()
        {
            var items = EventTypeDataManager.GetEntityDetails(EventTypeDataModel.Empty, SessionVariables.RequestProfile);
            return items;
        }

        public IEnumerable<ApplicationDataModel> GetApplicationList()
        {
            var items = ApplicationDataManager.GetEntityDetails(ApplicationDataModel.Empty, SessionVariables.RequestProfile);
            return items;
        }

        public IEnumerable<FundDataModel> GetFundList()
        {
            var items = FundDataManager.GetEntityDetails(FundDataModel.Empty, SessionVariables.RequestProfile);
            return items;
        }

    }
}
