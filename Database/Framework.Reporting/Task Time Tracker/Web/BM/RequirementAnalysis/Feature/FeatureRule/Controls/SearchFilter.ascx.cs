using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.Feature;
using Shared.WebCommon.UI.Web;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using ApplicationContainer.UI.Web.BaseUI;

namespace ApplicationContainer.UI.Web.FeatureRule.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
	{
		public FeatureRuleDataModel SearchParameters
		{
			get
			{
				var data = new FeatureRuleDataModel();

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