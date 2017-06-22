using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using ApplicationContainer.UI.Web.BaseUI;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.AllEntityDetailDataManager.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
	{
		#region variables

		public AllEntityDetailDataModel SearchParameters
		{
			get
			{
				var data = new AllEntityDetailDataModel();

				//data.EntityName = SearchFilterControl.GetParameterValue(AllEntityDetailDataModel.DataColumns.EntityName);

				//data.DB_Name = SearchFilterControl.GetParameterValue(AllEntityDetailDataModel.DataColumns.DBName);

				//data.DB_Project_Name = SearchFilterControl.GetParameterValue(AllEntityDetailDataModel.DataColumns.DBProjectName);

				//data.Component_Project_Name = SearchFilterControl.GetParameterValue(AllEntityDetailDataModel.DataColumns.DBComponentName);

				SearchFilterControl.SetSearchParameters(data);               

				CommonSearchParameters();

				return data;
			}
		}

		#endregion

        #region Events

        protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			BaseSearchFilterControl = SearchFilterControl;
		}

		#endregion
	}
}