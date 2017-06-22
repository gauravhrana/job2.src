using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.Configuration;
using Framework.Components.UserPreference;
using TaskTimeTracker.Components.Module.ApplicationDevelopment;
using Framework.UI.Web.BaseClasses;
using Framework.Components.DataAccess;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityImageAttribute.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
	{
		#region variables

		public FunctionalityImageAttributeDataModel SearchParameters
		{
			get
			{
				var data = new FunctionalityImageAttributeDataModel();

                data.ApplicationId = SearchFilterControl.GetParameterValueAsInt(BaseDataModel.BaseDataColumns.ApplicationId);

                SearchFilterControl.SetSearchParameters(data);

				data.FunctionalityImageId = SearchFilterControl.GetParameterValueAsInt(FunctionalityImageAttributeDataModel.DataColumns.FunctionalityImage);

				return data;
			}
		}

		#endregion

		#region private methods

		#endregion

		#region Events		

		protected void Page_Load(object sender, EventArgs e)
		{
			//base.OnLoad(e);
		}


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

			BaseSearchFilterControl = SearchFilterControl;
			SearchFilterControl.GetKendoComboBoConfigString = LoadKendoComboBoxSources;
        }

		#endregion
	}
}