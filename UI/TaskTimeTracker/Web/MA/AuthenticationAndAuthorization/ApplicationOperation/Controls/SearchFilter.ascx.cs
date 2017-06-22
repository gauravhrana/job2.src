using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.DataAccess;
using Framework.Components.DataAccess;
using Shared.UI.Web.Controls;
using Framework.UI.Web.BaseClasses;
using ApplicationContainer.UI.Web.BaseUI;
using DataModel.Framework.Configuration;


namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationOperation.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
    {

       #region variables                

        public ApplicationOperationDataModel SearchParameters
        {
			get
			{
				var data = new ApplicationOperationDataModel();

				SearchFilterControl.SetSearchParameters(data);

				CommonSearchParameters();

				return data;                			
			}
		}

        #endregion

        #region Methods

		public override void LoadDropDownListSources(string fieldName, DropDownList dropDownListControl)
		{
			base.LoadDropDownListSources(fieldName, dropDownListControl);

			if (fieldName.Equals("Application"))
			{
				var applicationData = Framework.Components.ApplicationUser.ApplicationDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(applicationData, dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
					BaseDataModel.BaseDataColumns.ApplicationId);

				dropDownListControl.SelectedValue = SessionVariables.RequestProfile.ApplicationId.ToString();
			}
			else if (fieldName.Equals("SystemEntityType"))
			{
				var data = Framework.Components.Core.SystemEntityTypeDataManager.GetList(SessionVariables.RequestProfile);

				UIHelper.LoadDropDown(data, dropDownListControl,
					SystemEntityTypeDataModel.DataColumns.EntityName,
					SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId);

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