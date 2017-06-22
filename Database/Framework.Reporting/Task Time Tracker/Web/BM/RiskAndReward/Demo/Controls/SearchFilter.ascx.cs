	using System;
	using Framework.UI.Web.BaseClasses;
	using DataModel.TaskTimeTracker.RiskReward;
	using TaskTimeTracker.Components.Module.RiskReward;


	namespace ApplicationContainer.UI.Web.RiskAndReward.Demo.Controls
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
