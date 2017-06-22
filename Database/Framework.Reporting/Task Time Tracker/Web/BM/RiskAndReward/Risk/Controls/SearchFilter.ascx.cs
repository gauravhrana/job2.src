using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.RiskReward;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.DataAccess;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using ApplicationContainer.UI.Web.BaseUI;

namespace ApplicationContainer.UI.Web.RiskAndReward.Risk.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
	{
		public RiskDataModel SearchParameters
		{
			get
			{
				var data = new RiskDataModel();

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