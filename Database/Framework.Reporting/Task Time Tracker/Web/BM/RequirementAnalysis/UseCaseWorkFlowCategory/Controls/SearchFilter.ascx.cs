using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.BaseUI;

namespace ApplicationContainer.UI.Web.UseCaseWorkFlowCategory.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
    {
        public UseCaseWorkFlowCategoryDataModel SearchParameters
        {
            get
            {
                var data = new UseCaseWorkFlowCategoryDataModel();

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