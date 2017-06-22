using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.DataAccess;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using ApplicationContainer.UI.Web.BaseUI;

namespace ApplicationContainer.UI.Web.UseCaseStep.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
    {
        public UseCaseStepDataModel SearchParameters
        {
            get
            {
                var data = new UseCaseStepDataModel();

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