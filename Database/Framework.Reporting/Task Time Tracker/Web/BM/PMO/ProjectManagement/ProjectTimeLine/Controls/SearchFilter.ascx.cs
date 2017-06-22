using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using Shared.WebCommon.UI.Web;
using System.Data;
using TaskTimeTracker.Components.BusinessLayer;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.ProjectManagement.ProjectTimeLine.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
	{
		public ProjectTimeLineDataModel SearchParameters
		{
			get
			{
				var data = new ProjectTimeLineDataModel();

				SearchFilterControl.SetSearchParameters(data);

				CommonSearchParameters();

				return data;
			}
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			BaseSearchFilterControl = SearchFilterControl;
			SearchFilterControl.LoadComboBoxSourceMethod = LoadDropDownListSources;
		}
	}
}
