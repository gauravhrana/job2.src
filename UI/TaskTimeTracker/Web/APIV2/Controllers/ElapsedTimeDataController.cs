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
using System.Reflection;
using DataModel.Framework.AuthenticationAndAuthorization;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;

namespace Web.Api.Controllers
{
    public class ElapsedTimeDataController : ApiController
    {

        private static string DataTableToJSON(DataTable table)
        {
            StringBuilder sb = new StringBuilder();
            foreach (DataRow dr in table.Rows)
            {
                if (sb.Length != 0)
                    sb.Append(",");
                sb.Append("{");
                StringBuilder sb2 = new StringBuilder();
                foreach (DataColumn col in table.Columns)
                {
                    string fieldname = col.ColumnName;
                    string fieldvalue = dr[fieldname].ToString();
					if (fieldname.Equals("ElapsedTime"))
					{
						fieldvalue = Convert.ToDecimal(fieldvalue).ToString("#,0");
					}
					if (fieldname.Equals("RecordCount"))
					{
						fieldvalue = Convert.ToDecimal(fieldvalue).ToString("#,0");
					}
                    if (sb2.Length != 0)
                        sb2.Append(",");
                    sb2.Append(string.Format("{0}:\"{1}\"", fieldname, fieldvalue));
                }
                sb.Append(sb2.ToString());
                sb.Append("}");
            }
            //if (e == makejson.e_with_square_brackets)
            //{/
            sb.Insert(0, "[");
            sb.Append("]");
            //}
            return sb.ToString();
        }

        [System.Web.Http.AcceptVerbs("GET")]
        public IEnumerable<ApplicationUserDataModel> ListUsers()
        {
            var items = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetAllApplicationUserList(SessionVariables.RequestProfile);
            items.Insert(0, new ApplicationUserDataModel { ApplicationCode = "All", ApplicationUserId = -1 });
            return items;
        }

        [System.Web.Http.AcceptVerbs("GET")]
        public JArray GetElapsedTimeData(string value1, string value2, string value3)
        {
            // deserialize strings passed from client side
            var computerName = new JavaScriptSerializer().Deserialize<string>(value1);
            var connectionKey = new JavaScriptSerializer().Deserialize<string>(value2);

            var applicationUser = value3;

            var data = new Framework.Components.LogAndTrace.Log4NetDataModel();

            if (!string.IsNullOrEmpty(computerName))
            {
                data.Computer = computerName;
            }
            if (!string.IsNullOrEmpty(connectionKey))
            {
                data.ConnectionKey = connectionKey;
            }
            if (applicationUser != "-1")
            {
                data.LogUser = applicationUser;
            }

            var dt = Framework.Components.LogAndTrace.Log4NetDataManager.GetElapsedTimeRecords(data, SessionVariables.RequestProfile);

            var jsonSring = DataTableToJSON(dt);			
            var jArray = JArray.Parse(jsonSring);
			
            return jArray;
        }

        [System.Web.Http.AcceptVerbs("GET")]
        public JObject GetStoredSearchData(string value)
        {
            var settingCategoryName = value;

            var fields = new List<string>(){ 
                    ApplicationCommon.Computer, 
                    ApplicationCommon.ConnectionKey, 
                    ApplicationCommon.LogUser, 
                    ApplicationCommon.GroupBy};

            var jObject = new JObject();

            foreach (var fieldName in fields)
            {               
                jObject[fieldName] = PreferenceUtility.GetUserPreferenceByKey(fieldName, settingCategoryName);
            }

            return jObject;
           
        }

        [System.Web.Http.AcceptVerbs("GET")]
        public bool SetStoredSearchData(string value, string value1)
        {
            var settingCategoryName = value;

            var fields = new List<string>(){ 
                    ApplicationCommon.Computer, 
                    ApplicationCommon.ConnectionKey, 
                    ApplicationCommon.LogUser, 
                    ApplicationCommon.GroupBy};

            var storedValues = JsonConvert.DeserializeObject<Dictionary<string, string>>(value1);

            foreach (var fieldName in fields)
            {
                PreferenceUtility.UpdateUserPreference(settingCategoryName, fieldName, storedValues[fieldName]);
            }

            return true;

        }

    }
}
