using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataModel.Framework.Configuration;
using Framework.Components.DataAccess;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;

namespace Web.Api.Controllers
{
    public class FieldConfigurationController : ApiController
    {

		public IEnumerable<FieldConfigurationModeDataModel> GetFCModes(string value)
		{
			var systemEntity = (SystemEntity)Enum.Parse(typeof(SystemEntity), value, true);

			var dtFCModes = FieldConfigurationUtility.GetApplicableModesList(systemEntity);
			var lstFCModes = new List<FieldConfigurationModeDataModel>();

			foreach (DataRow dataRow in dtFCModes.Rows)
			{
				var model = new FieldConfigurationModeDataModel();
				model.Name = dataRow["Name"].ToString();
				model.FieldConfigurationModeId = int.Parse(dataRow["FieldConfigurationModeId"].ToString());

				lstFCModes.Add(model);
			}

			return lstFCModes;
		}

		[System.Web.Http.AcceptVerbs("GET")]
		public IEnumerable<FieldConfigurationDataModel> GetFieldConfigurations(string value, string value1)
		{
            var entityName = value1;
			var lstColumns = new List<FieldConfigurationDataModel>();

			var systemEntityTypeId = ((int)(SystemEntity)Enum.Parse(typeof(SystemEntity), entityName, true));

			var dtConfigurations = FieldConfigurationUtility.GetFieldConfigurations(systemEntityTypeId, Convert.ToInt32(value), string.Empty);

			foreach (DataRow row in dtConfigurations.Rows)
			{
				var fcObj                           = new FieldConfigurationDataModel();

				fcObj.Name                          = row[FieldConfigurationDataModel.DataColumns.Name].ToString();
				fcObj.FieldConfigurationDisplayName = row[FieldConfigurationDataModel.DataColumns.FieldConfigurationDisplayName].ToString();
                fcObj.HorizontalAlignment           = row[FieldConfigurationDataModel.DataColumns.HorizontalAlignment].ToString();
                fcObj.GridViewPriority              = int.Parse(row[FieldConfigurationDataModel.DataColumns.GridViewPriority].ToString());
                fcObj.ControlType                   = row[FieldConfigurationDataModel.DataColumns.ControlType].ToString();
				fcObj.Formatting					= row[FieldConfigurationDataModel.DataColumns.Formatting].ToString();

				lstColumns.Add(fcObj);
			}

			return lstColumns;
		}

        [System.Web.Http.AcceptVerbs("GET")]
        public IEnumerable<FieldConfigurationDataModel> GetSearchFilterColumns(string value, string value1)
        {
            var settingCategory = value1;
            var searchControlModeId = SessionVariables.SearchControlColumnsModeId.ToString();
            var entityName = value;
            var lstColumns = GetFieldConfigurations(searchControlModeId, entityName).ToList();

            // get previously stored values using Setting Category and Name of the Field Configuration, 
            // storing it in Value field as that is used as the model in angular
            for (var i = 0; i < lstColumns.Count;i++ )                
            {
                lstColumns[i].Value = PreferenceUtility.GetUserPreferenceByKey(lstColumns[i].Name, settingCategory);
                if (string.IsNullOrEmpty(lstColumns[i].Value))
                {
                    lstColumns[i].Value = "";
                }
                else if (lstColumns[i].Value == "None")
                {
                    lstColumns[i].Value = "-1";
                }

                if (lstColumns[i].ControlType == "DatePanel" && !string.IsNullOrEmpty(lstColumns[i].Value))
                {
                    var workDateValue = lstColumns[i].Value;

                    var tmpArr = workDateValue.Split(new string[] { "&" }, StringSplitOptions.None);

                    if (tmpArr.Length > 2)
                    {                        
                        var fromDateValue = tmpArr[0];
                        var toDateValue = tmpArr[1];
                        var preFilledItem = tmpArr[2];

                        lstColumns[i].Value = DateTimeHelper.FromApplicationDateFormatToUserDateFormat(fromDateValue) + "&"
                            + DateTimeHelper.FromApplicationDateFormatToUserDateFormat(toDateValue) + "&"
                            + preFilledItem + "&";
                    }
                    else
                    {
                        lstColumns[i].Value = "&&&";
                    }
                }
            }

            return lstColumns;
        }

        [System.Web.Http.AcceptVerbs("GET")]
        public IEnumerable<FieldConfigurationDataModel> ResetSearchFilterColumns(string value, string value1)
        {
            var settingCategory = value1;
            var searchControlModeId = SessionVariables.SearchControlColumnsModeId.ToString();
            var entityName = value;
            var lstColumns = GetFieldConfigurations(searchControlModeId, entityName).ToList();

            // get previously stored values using Setting Category and Name of the Field Configuration, 
            // storing it in Value field as that is used as the model in angular
            for (var i = 0; i < lstColumns.Count;i++ )                
            {
                lstColumns[i].Value = PreferenceUtility.ResetUserPreferenceByKey(lstColumns[i].Name, settingCategory);
                if (string.IsNullOrEmpty(lstColumns[i].Value))
                {
                    lstColumns[i].Value = "";
                }
                else if (lstColumns[i].Value == "None")
                {
                    lstColumns[i].Value = "-1";
                }
            }

            return lstColumns;
        }

		public FieldConfigurationModeDataModel GetUserFieldConfigurationMode(string value)
		{
			var settingCategory = value + "ListView";
			var firstFCMode = PreferenceUtility.GetUserPreferenceByKeyAsInt(ApplicationCommon.FieldConfigurationMode, settingCategory);

			var data = new FieldConfigurationModeDataModel();
			data.FieldConfigurationModeId = firstFCMode;

			var list = FieldConfigurationModeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile, 0);
			if (list.Count > 0)
			{
				data = list[0];
			}

			return data;
		}

		[System.Web.Http.AcceptVerbs("GET")]
		public void UpdateUserFieldConfigurationMode(string value, string value1)
        {
            var entityName = value1;
			var settingCategory = entityName + "ListView";
			PreferenceUtility.UpdateUserPreference(settingCategory, ApplicationCommon.FieldConfigurationMode, value);
		}

    }
}
