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
using DataModel.ReferenceData;

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
                    var src = MenuCategoryDataManager.GetList(SessionVariables.RequestProfile);
                    lstOptions = src.Select(x => x.Name).ToList();
                }
            }
            else if (primaryEntity.Equals("Question", StringComparison.InvariantCultureIgnoreCase))
            {
                if (searchField == "QuestionPhrase")
                {
                    var src = QuestionDataManager.GetList(SessionVariables.RequestProfile);
                    lstOptions = src.Select(x => x.QuestionPhrase).ToList();
                }
            }

            else if (primaryEntity.Equals("Log4Net", StringComparison.InvariantCultureIgnoreCase))
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
            else if (primaryEntity.Equals("CommissionCode", StringComparison.InvariantCultureIgnoreCase))
            {
                if (searchField == "CommissionCodeCode")
                {
                    var src = CommissionCodeDataManager.GetList(SessionVariables.RequestProfile);
                    lstOptions = src.Select(x => x.CommissionCodeCode).ToList();
                }
            }

            return lstOptions;
        }

        public IEnumerable<FieldConfigurationDataModel> GetGroupByList(string value)
        {
            var entityName = value;

            var systemEntityTypeId = (SystemEntity)Enum.Parse(typeof(SystemEntity), entityName, true);
            var result = new List<FieldConfigurationDataModel>();

            var data = new FieldConfigurationDataModel();
            data.SystemEntityTypeId = (int)systemEntityTypeId;
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

            var oData = new FieldConfigurationDataModel();
            oData.FieldConfigurationDisplayName = "None";
            oData.Name = "-1";
            result.Add(oData);

            foreach (var item in items)
            {
                if (!item.Name.Contains("GroupBy") && !LstGroupByItems.Contains(item.Name))
                {
                    oData = new FieldConfigurationDataModel();
                    oData.Name = item.Name.ToString();
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

        public IEnumerable<PortfolioDataModel> GetPortfolioList()
        {
            var items = PortfolioDataManager.GetEntityDetails(PortfolioDataModel.Empty, SessionVariables.RequestProfile);
            return items;
        }

        public IEnumerable<TransactionTypeDataModel> GetTransactionTypeList()
        {
            var items = TransactionTypeDataManager.GetEntityDetails(TransactionTypeDataModel.Empty, SessionVariables.RequestProfile);
            return items;
        }

        public IEnumerable<CustodianDataModel> GetCustodianList()
        {
            var items = CustodianDataManager.GetEntityDetails(CustodianDataModel.Empty, SessionVariables.RequestProfile);
            return items;
        }

        public IEnumerable<AccountSpecificTypeDataModel> GetAccountSpecificTypeList()
        {
            var items = AccountSpecificTypeDataManager.GetEntityDetails(AccountSpecificTypeDataModel.Empty, SessionVariables.RequestProfile);
            return items;
        }

        public IEnumerable<InvestmentTypeDataModel> GetInvestmentTypeList()
        {
            var items = InvestmentTypeDataManager.GetEntityDetails(InvestmentTypeDataModel.Empty, SessionVariables.RequestProfile);
            return items;
        }

        public IEnumerable<ManagementFirmDataModel> GetManagementFirmList()
        {
            var items = ManagementFirmDataManager.GetEntityDetails(ManagementFirmDataModel.Empty, SessionVariables.RequestProfile);
            return items;
        }

        public IEnumerable<TransactionEventDataModel> GetTransactionEventList()
        {
            var items = TransactionEventDataManager.GetEntityDetails(TransactionEventDataModel.Empty, SessionVariables.RequestProfile);
            return items;
        }

        public IEnumerable<SecurityDataModel> GetSecurityList()
        {
            var items = SecurityDataManager.GetEntityDetails(SecurityDataModel.Empty, SessionVariables.RequestProfile);
            return items;
        }

        public IEnumerable<TradingEventTypeDataModel> GetTradingEventTypeList()
        {
            var items = TradingEventTypeDataManager.GetEntityDetails(TradingEventTypeDataModel.Empty, SessionVariables.RequestProfile);
            return items;
        }

        public IEnumerable<OrderRequestDataModel> GetOrderRequestList()
        {
            var items = OrderRequestDataManager.GetEntityDetails(OrderRequestDataModel.Empty, SessionVariables.RequestProfile);
            return items;
        }

        public IEnumerable<BrokerDataModel> GetBrokerList()
        {
            var items = BrokerDataManager.GetEntityDetails(BrokerDataModel.Empty, SessionVariables.RequestProfile);
            return items;
        }

        public IEnumerable<OrderActionDataModel> GetOrderActionList()
        {
            var items = OrderActionDataManager.GetEntityDetails(OrderActionDataModel.Empty, SessionVariables.RequestProfile);
            return items;
        }

        public IEnumerable<OrderTypeDataModel> GetOrderTypeList()
        {
            var items = OrderTypeDataManager.GetEntityDetails(OrderTypeDataModel.Empty, SessionVariables.RequestProfile);
            return items;
        }

        public IEnumerable<StrategyDataModel> GetStrategyList()
        {
            var items = StrategyDataManager.GetEntityDetails(StrategyDataModel.Empty, SessionVariables.RequestProfile);
            return items;
        }

        public IEnumerable<OrderStatusGroupDataModel> GetOrderStatusGroupList()
        {
            var items = OrderStatusGroupDataManager.GetEntityDetails(OrderStatusGroupDataModel.Empty, SessionVariables.RequestProfile);
            return items;
        }

        public IEnumerable<OrderStatusTypeDataModel> GetOrderStatusTypeList()
        {
            var items = OrderStatusTypeDataManager.GetEntityDetails(OrderStatusTypeDataModel.Empty, SessionVariables.RequestProfile);
            return items;
        }

        public IEnumerable<MSPAFileDataModel> GetMSPAFileList()
        {
            var items = MSPAFileDataManager.GetEntityDetails(MSPAFileDataModel.Empty, SessionVariables.RequestProfile);
            return items;
        }

        public IEnumerable<CommissionCodeDataModel> GetCommissionCodeList()
        {
            var items = CommissionCodeDataManager.GetEntityDetails(CommissionCodeDataModel.Empty, SessionVariables.RequestProfile);
            return items;
        }

        public IEnumerable<ExchangeDataModel> GetExchangeList()
        {
            var items = ExchangeDataManager.GetEntityDetails(ExchangeDataModel.Empty, SessionVariables.RequestProfile);
            return items;
        }

        public IEnumerable<OrderStatusDataModel> GetOrderList()
        {
            var items = OrderStatusDataManager.GetEntityDetails(OrderStatusDataModel.Empty, SessionVariables.RequestProfile);
            return items;
        }

        public IEnumerable<IssuerDataModel> GetIssuerList()
        {
            var items = IssuerDataManager.GetEntityDetails(IssuerDataModel.Empty, SessionVariables.RequestProfile);
            return items;
        }

        public IEnumerable<DeliveryAgentDataModel> GetDeliveryAgentList()
        {
            var items = DeliveryAgentDataManager.GetEntityDetails(DeliveryAgentDataModel.Empty, SessionVariables.RequestProfile);
            return items;
        }

        public IEnumerable<DataModel.ReferenceData.CountryDataModel> GetCountryList()
        {
            var items = ReferenceData.Components.BusinessLayer.CountryDataManager.GetEntityDetails(DataModel.ReferenceData.CountryDataModel.Empty, SessionVariables.RequestProfile);
            return items;
        }

        public IEnumerable<DataModel.ReferenceData.CityDataModel> GetCityList()
        {
            var items = ReferenceData.Components.BusinessLayer.CityDataManager.GetEntityDetails(DataModel.ReferenceData.CityDataModel.Empty, SessionVariables.RequestProfile);
            return items;
        }

        public IEnumerable<DataModel.ReferenceData.StateDataModel> GetStateList()
        {
            var items = ReferenceData.Components.BusinessLayer.StateDataManager.GetEntityDetails(DataModel.ReferenceData.StateDataModel.Empty, SessionVariables.RequestProfile);
            return items;
        }

        public IEnumerable<DataModel.ReferenceData.ProvinceTypeDataModel> GetProvinceTypeList()
        {
            var items = ReferenceData.Components.BusinessLayer.ProvinceTypeDataManager.GetEntityDetails(DataModel.ReferenceData.ProvinceTypeDataModel.Empty, SessionVariables.RequestProfile);
            return items;
        }

        public IEnumerable<DataModel.DayCare.CourseDataModel> GetCourseList()
        {
            var items = DayCare.Components.BusinessLayer.CourseDataManager.GetEntityDetails(DataModel.DayCare.CourseDataModel.Empty, SessionVariables.RequestProfile);
            return items;
        }

        public IEnumerable<DataModel.DayCare.StudentDataModel> GetStudentList()
        {
            var items = DayCare.Components.BusinessLayer.StudentDataManager.GetEntityDetails(DataModel.DayCare.StudentDataModel.Empty, SessionVariables.RequestProfile);
            return items;
        }

        public IEnumerable<ScheduleStateDataModel> GetScheduleStateNameList()
        {
            var items = ScheduleStateDataManager.GetEntityDetails(ScheduleStateDataModel.Empty, SessionVariables.RequestProfile);
            return items;
        }

		public IEnumerable<ScheduleDetailActivityCategoryDataModel> GetScheduleDetailActivityCategoryList()
		{
			var items = ScheduleDetailActivityCategoryDataManager.GetEntityDetails(ScheduleDetailActivityCategoryDataModel.Empty, SessionVariables.RequestProfile);
			return items;
		}

		public IEnumerable<TeacherDataModel> GetTeacherList()
		{
			var items = TeacherDataManager.GetEntityDetails(TeacherDataModel.Empty, SessionVariables.RequestProfile);
			return items;
		}

		public IEnumerable<DepartmentDataModel> GetDepartmentList()
		{
			var items = DepartmentDataManager.GetEntityDetails(DepartmentDataModel.Empty, SessionVariables.RequestProfile);
			return items;
		}

        

        //public IEnumerable<KeyValuePair<string, int>> GetPreFilledDateItemsList()
        public IEnumerable<string> GetPreFilledDateItemsList()
        {
            var list = new List<KeyValuePair<string, int>>();

            var iCnt = -1;

            list.Add(new KeyValuePair<string, int>("None", iCnt++));   
            list.Add(new KeyValuePair<string, int>("Custom", iCnt++));         

            list.Add(new KeyValuePair<string, int>("Today", iCnt++));
            list.Add(new KeyValuePair<string, int>("This Week", iCnt++));
            list.Add(new KeyValuePair<string, int>("This Week-to-date", iCnt++));
            list.Add(new KeyValuePair<string, int>("This Month", iCnt++));
            list.Add(new KeyValuePair<string, int>("This Month-to-date", iCnt++));
            list.Add(new KeyValuePair<string, int>("This Quarter", iCnt++));
            list.Add(new KeyValuePair<string, int>("This Quarter-to-date", iCnt++));
            list.Add(new KeyValuePair<string, int>("This Year", iCnt++));
            list.Add(new KeyValuePair<string, int>("This Year-to-date", iCnt++));
            list.Add(new KeyValuePair<string, int>("Yesterday", iCnt++));
            list.Add(new KeyValuePair<string, int>("Last Week", iCnt++));
            list.Add(new KeyValuePair<string, int>("Last Week-to-date", iCnt++));
            list.Add(new KeyValuePair<string, int>("Last Month", iCnt++));
            list.Add(new KeyValuePair<string, int>("Last Month-to-date", iCnt++));
            list.Add(new KeyValuePair<string, int>("Last Quarter", iCnt++));
            list.Add(new KeyValuePair<string, int>("Last Quarter-to-date", iCnt++));
            list.Add(new KeyValuePair<string, int>("Last Year", iCnt++));
            list.Add(new KeyValuePair<string, int>("Last Year-to-date", iCnt++));
            list.Add(new KeyValuePair<string, int>("Next Week", iCnt++));
            list.Add(new KeyValuePair<string, int>("Next 4 Weeks", iCnt++));
            list.Add(new KeyValuePair<string, int>("Next Month", iCnt++));
            list.Add(new KeyValuePair<string, int>("Next Quarter", iCnt++));
            list.Add(new KeyValuePair<string, int>("Next Year", iCnt++));

            return list.Select(x => x.Key).ToList();
            //return list;
        }

        public IEnumerable<string> GetDatesFromPreFilledItem(string value)
        {
            var dateRange = Shared.UI.Web.DateRangeHelper.FillUpDate(value, SessionVariables.UserDateFormat);            
            return dateRange.ToList();
        }

    }
}
