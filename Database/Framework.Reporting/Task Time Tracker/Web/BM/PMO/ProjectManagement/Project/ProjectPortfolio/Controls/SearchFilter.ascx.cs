using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.DataAccess;
using Framework.UI.Web.BaseClasses;
using ApplicationContainer.UI.Web.BaseUI;

namespace ApplicationContainer.UI.Web.ProjectManagement.Project.ProjectPortfolio.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
	{
		public ProjectPortfolioDataModel SearchParameters
		{
			get
			{
				var data = new ProjectPortfolioDataModel();

                SearchFilterControl.SetSearchParameters(data);

				CommonSearchParameters();

				return data;
			}
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			BaseSearchFilterControl = SearchFilterControl;
		}
	}
}