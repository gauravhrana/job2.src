using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using System.Data;
using Shared.UI.Web.Controls;
using Framework.Components.DataAccess;

namespace Shared.UI.Web.Configuration.ApplicationRouteParameter.Controls
{
    public partial class SearchFilter : ControlSearchFilter
    {

        #region variables   

        public ApplicationRouteParameterDataModel SearchParameters
        {
            get
            {
                var data = new ApplicationRouteParameterDataModel();

                if (SearchParametersRepeater.Items.Count != 0)
                {
                    if (PreferenceUtility.GetUserPreferenceByKeyAsBoolean(
                        ApplicationRouteParameterDataModel.DataColumns.ParameterName + "Visibility", SettingCategory))
                    {
                        if (!string.IsNullOrEmpty(CheckAndGetFieldValue(
                                ApplicationRouteParameterDataModel.DataColumns.ParameterName).ToString()))
                        {
                            data.ParameterName = CheckAndGetFieldValue(
                               ApplicationRouteParameterDataModel.DataColumns.ParameterName).ToString();
                        }
                    }

                    if (PreferenceUtility.GetUserPreferenceByKeyAsBoolean(
                        ApplicationRouteParameterDataModel.DataColumns.ApplicationRoute + "Visibility", SettingCategory))
                    {
                        if(Convert.ToInt32(CheckAndGetFieldValue(ApplicationRouteParameterDataModel.DataColumns.ApplicationRoute)) != -1)
                        {
                            data.ApplicationRouteId = Convert.ToInt32(CheckAndGetFieldValue(
                               ApplicationRouteParameterDataModel.DataColumns.ApplicationRoute));
                        }
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

			if (fieldName.Equals("ApplicationRoute"))
			{
				var applicationData = Framework.Components.Core.ApplicationRouteDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(applicationData, dropDownListControl,
					ApplicationRouteDataModel.DataColumns.RouteName,
					ApplicationRouteDataModel.DataColumns.ApplicationRouteId);
			}
		}

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity                = SystemEntity.ApplicationRouteParameter;
			PrimaryEntityKey             = "ApplicationRouteParameter";

			SearchActionBarCore          = oSearchActionBar;
			SearchParametersRepeaterCore = SearchParametersRepeater;
		}

		#endregion

    }
}