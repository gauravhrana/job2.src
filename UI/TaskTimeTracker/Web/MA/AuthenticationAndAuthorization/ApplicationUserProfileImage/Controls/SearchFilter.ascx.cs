using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.Configuration;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;
using Shared.UI.Web.Controls;
using Framework.Components.DataAccess;

namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationUserProfileImage.Controls
{
	public partial class SearchFilter : ControlSearchFilter
	{

		#region variables

        public ApplicationUserProfileImageDataModel SearchParameters
		{
			get
			{
				var data = new ApplicationUserProfileImageDataModel();

				data.ApplicationId = GetParameterValueAsInt(BaseDataModel.BaseDataColumns.ApplicationId);

				data.ApplicationUserId = GetParameterValueAsInt(ApplicationUserProfileImageDataModel.DataColumns.ApplicationUserId);
					

				return data;
			}
		}

		#endregion

		#region private methods

		public override void LoadDropDownListSources(string fieldName, DropDownList dropDownListControl)
		{
			base.LoadDropDownListSources(fieldName, dropDownListControl);

			if (fieldName.Equals("ApplicationId"))
			{
				var applicationData = Framework.Components.ApplicationUser.ApplicationDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(applicationData, dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
					BaseDataModel.BaseDataColumns.ApplicationId);

				dropDownListControl.SelectedValue = SessionVariables.RequestProfile.ApplicationId.ToString();
			}
			else if (fieldName.Equals("ApplicationUserId"))
			{
				var applicationUserData = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(applicationUserData, dropDownListControl,
					ApplicationUserDataModel.DataColumns.ApplicationUserName,
					ApplicationUserDataModel.DataColumns.ApplicationUserId);
			}
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey             = "ApplicationUserProfileImage";
			FolderLocationFromRoot       = "ApplicationUserProfileImage";
			PrimaryEntity                = SystemEntity.ApplicationUserProfileImage;

			SearchActionBarCore          = oSearchActionBar;
			SearchParametersRepeaterCore = SearchParametersRepeater;
		}

		#endregion

	}
}