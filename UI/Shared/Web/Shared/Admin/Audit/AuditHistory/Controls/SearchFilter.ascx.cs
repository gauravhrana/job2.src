using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;
using System.Globalization;
using DataModel.Framework.Configuration;
using DataModel.Framework.ReleaseLog;
using Framework.Components.UserPreference;
using Shared.UI.Web.Controls;
using System.Text;


namespace Shared.UI.Web.Admin.Audit.AuditHistory.Controls
{
    public partial class SearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilter
	{
		#region variables

		//List<string> LstGroupByItems = new List<string>();

        public DataModel.Framework.Audit.AuditHistory SearchParameters
        {
            get
            {
                var data = new DataModel.Framework.Audit.AuditHistory();

				if (CheckIfValueIsValidAsInt(CheckAndGetFieldValue(DataModel.Framework.Audit.AuditHistory.DataColumns.SystemEntityId).ToString()) != null)
					data.SystemEntityId = GetParameterValueAsInt(DataModel.Framework.Audit.AuditHistory.DataColumns.SystemEntityId);

				if (CheckIfValueIsValidAsInt(CheckAndGetFieldValue(DataModel.Framework.Audit.AuditHistory.DataColumns.AuditActionId).ToString()) != null)
					data.AuditActionId = GetParameterValueAsInt(DataModel.Framework.Audit.AuditHistory.DataColumns.AuditActionId);

				if (CheckIfValueIsValidAsInt(CheckAndGetFieldValue(DataModel.Framework.Audit.AuditHistory.DataColumns.CreatedByPersonId).ToString()) != null)
					data.CreatedByPersonId = GetParameterValueAsInt(DataModel.Framework.Audit.AuditHistory.DataColumns.CreatedByPersonId);

				if (CheckIfValueIsValidAsInt(CheckAndGetFieldValue(BaseDataModel.BaseDataColumns.TraceId).ToString()) != null)
					data.TraceId = GetParameterValueAsInt(BaseDataModel.BaseDataColumns.TraceId);

				if (!string.IsNullOrEmpty(CheckAndGetFieldValue(DataModel.Framework.Audit.AuditHistory.DataColumns.EntityKey).ToString()))
					data.EntityKey = int.Parse(CheckAndGetFieldValue(DataModel.Framework.Audit.AuditHistory.DataColumns.EntityKey).ToString());

				//var columnName = DataModel.Framework.Audit.AuditHistory.DataColumns.UpdatedRange;
				//var date = Convert.ToString(CheckAndGetFieldValue(columnName));
				//if (!string.IsNullOrEmpty(date))
				//{
				//	var dates = date.Split('&');
				//	if (Boolean.Parse(dates[2]))
				//	{
				//		data.FromSearchDate = Shared.WebCommon.UI.Web.DateTimeHelper.FromApplicationDateFormatToDate(dates[0]);
				//		data.ToSearchDate = Shared.WebCommon.UI.Web.DateTimeHelper.FromApplicationDateFormatToDate(dates[1]);
				//	}
				//}	
                
                return data;
            }
        }

       #endregion

		#region private methods

		public override string LoadKendoComboBoxSources(string fieldName, TextBox txtBox, PlaceHolder plcControlHolder)
		{
			if (fieldName.Equals("SystemEntityId"))
			{
				return AjaxHelper.GetKendoComboBoxConfigScript("GetSystemEntityList", "EntityName", "SystemEntityTypeId", plcControlHolder);	
			}
			else if (fieldName.Equals("EntityKey"))
			{
				return AjaxHelper.GetKendoComboBoxConfigScript("GetSystemEntityList", "EntityName", "SystemEntityTypeId", plcControlHolder);	
			}
			else if (fieldName.Equals("CreatedByPersonId"))
			{
				return AjaxHelper.GetKendoComboBoxConfigScript("GetApplicationUserList", "FullName", "ApplicationUserId", plcControlHolder);					
			}
			else if (fieldName.Equals("AuditActionId"))
			{
				return AjaxHelper.GetKendoComboBoxConfigScript("GetAuditActionList", "Name", "AuditActionId", plcControlHolder);	
			}
			else if (fieldName.Equals("TraceId"))
			{
				return AjaxHelper.GetKendoComboBoxConfigScript("GetTraceList", "Name", "TraceId", plcControlHolder);
			}
			return string.Empty;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.AuditHistory;
			PrimaryEntityKey = "AuditHistory";
			FolderLocationFromRoot = "Shared/Admin/Audit";

			SearchActionBarCore = oSearchActionBar;
			SearchParametersRepeaterCore = SearchParametersRepeater;
		}	


		//protected void SearchParametersRepeater_ItemCreated(object sender, RepeaterItemEventArgs e)
		//{
		//	if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
		//	{
		//		//var plcControlHolder = (PlaceHolder)e.Item.FindControl("plcControlHolder");
		//		var oDateRange = (DateRangeControl)e.Item.FindControl("oDateRange");

		//		if (oDateRange != null)
		//		{
		//			oDateRange.Key = e.Item.ItemIndex.ToString();
		//			var funccall = "Fillup" + oDateRange.GetKey() + "();";
		//			oDateRange.DateRangeDropDown.Attributes.Add("onchange", funccall);
		//			funccall = "chkdate_checkedchanged" + oDateRange.GetKey() + "();";
		//			oDateRange.DateRangeCheckBox.Attributes.Add("onclick", funccall);
		//			oDateRange.HideLabel();
		//		}
		//	}
		//}

       #endregion

	}
}


