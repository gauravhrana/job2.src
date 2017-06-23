using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.Framework.LogAndTrace;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Globalization;
using Shared.UI.Web.Controls;
using Framework.UI.Web.BaseClasses;


namespace Shared.UI.Web.Admin.UserLogin.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
	{

        #region variables

        public Framework.Components.LogAndTrace.UserLoginDataModel SearchParameters
        {
            get
            {
                var data = new Framework.Components.LogAndTrace.UserLoginDataModel();
				
				if (SearchFilterControl.CheckAndGetFieldValue(SearchFilterControl.CheckAndGetFieldValue(Framework.Components.LogAndTrace.UserLoginDataModel.DataColumns.UserName).ToString()) != null)
					data.UserName = SearchFilterControl.GetParameterValue(Framework.Components.LogAndTrace.UserLoginDataModel.DataColumns.UserName);

				if (SearchFilterControl.CheckIfValueIsValidAsInt(SearchFilterControl.CheckAndGetFieldValue(Framework.Components.LogAndTrace.UserLoginDataModel.DataColumns.UserLoginStatusId).ToString()) != null)
					data.UserLoginStatusId = SearchFilterControl.GetParameterValueAsInt(Framework.Components.LogAndTrace.UserLoginDataModel.DataColumns.UserLoginStatusId);								

				var date = SearchFilterControl.CheckAndGetFieldValue(Framework.Components.LogAndTrace.UserLoginDataModel.DataColumns.RecordDate).ToString();

				if (!string.IsNullOrEmpty(date))
				{
					var dates = date.Split('&');

					if (Boolean.Parse(dates[2]))
					{
						data.RecordDate = DateTimeHelper.FromApplicationDateFormatToDate(dates[0]);
						data.RecordDate2 = DateTimeHelper.FromApplicationDateFormatToDate(dates[1]);
					}
				}

                return data;
            }
        }

        #endregion

        #region Methods

        public override void LoadDropDownListSources(string fieldName, DropDownList dropDownListControl)
        {
            base.LoadDropDownListSources(fieldName, dropDownListControl);

            if (fieldName.Equals("UserLoginStatusId"))
            {
				var userLoginStatusData = Framework.Components.LogAndTrace.UserLoginStatusDataManager.GetList(SessionVariables.RequestProfile);
                UIHelper.LoadDropDown(userLoginStatusData, dropDownListControl, 
                    StandardDataModel.StandardDataColumns.Name,
                    UserLoginStatusDataModel.DataColumns.UserLoginStatusId);
            }
        }

        #endregion

        #region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			BaseSearchFilterControl = SearchFilterControl;
			SearchFilterControl.GetKendoComboBoConfigString = LoadKendoComboBoxSources;
		}

        #endregion

	}
}