using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;
using Shared.UI.Web.Controls;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using ApplicationContainer.UI.Web.BaseUI;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityImage.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
	{
		#region variables		
		
		public FunctionalityImageDataModel SearchParameters
		{
			get
			{
				var data = new FunctionalityImageDataModel();

                data.ApplicationId = SearchFilterControl.GetParameterValueAsInt(BaseDataModel.BaseDataColumns.ApplicationId);

                data.Title = SearchFilterControl.GetParameterValue(FunctionalityImageDataModel.DataColumns.Title);

				SearchFilterControl.SetSearchParameters(data);

				return data;
			}
		}		

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