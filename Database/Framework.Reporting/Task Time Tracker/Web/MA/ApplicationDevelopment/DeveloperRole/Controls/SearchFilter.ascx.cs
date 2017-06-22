using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.DataAccess;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.Configuration;
using Framework.Components.UserPreference;
using System.Text;
using Framework.Components.ApplicationUser;
using Framework.Components.DataAccess;
using Shared.UI.Web.Controls;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.DeveloperRole.Controls
{
    public partial class SearchFilter : ControlSearchFilter
    {

        #region variables

        public DeveloperRoleDataModel SearchParameters
        {
            get
            {
                var data = new DeveloperRoleDataModel();

                data.ApplicationId = GetParameterValueAsInt(BaseDataModel.BaseDataColumns.ApplicationId);

                GetParameterValue(data, StandardDataModel.StandardDataColumns.Name);

                GroupBy = GetParameterValue("GroupBy");

                SubGroupBy = GetParameterValue("SubGroupBy");

                return data;
            }
        }

        #endregion

		#region Methods

		public override void LoadDropDownListSources(string fieldName, DropDownList dropDownListControl)
		{
			base.LoadDropDownListSources(fieldName, dropDownListControl);

			if (fieldName.Equals("ApplicationId"))
			{
				var applicationData = ApplicationDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(applicationData, dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
					BaseDataModel.BaseDataColumns.ApplicationId);

				dropDownListControl.SelectedValue = SessionVariables.RequestProfile.ApplicationId.ToString();
			}
		}

		public override string LoadKendoComboBoxSources(string fieldName, TextBox txtBox, PlaceHolder plcControlHolder)
		{
			if (fieldName.Equals("ApplicationId"))
			{
				return AjaxHelper.GetKendoComboBoxConfigScript("GetApplicationList", "Name", "ApplicationId", plcControlHolder);	
			}
			return string.Empty;
		}

		#endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity					= SystemEntity.DeveloperRole;
            PrimaryEntityKey				= "DeveloperRole";
            FolderLocationFromRoot			= "DeveloperRole";

            SearchActionBarCore				= oSearchActionBar;
            SearchParametersRepeaterCore	= SearchParametersRepeater;

            TabHeaderContainer				= divTabHeaderList;
            TabContainer					= divTabContentContainer;
        }

        #endregion

    }
}