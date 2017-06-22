using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.UI.Web.BaseClasses;
using TaskTimeTracker.Components.Module.ApplicationDevelopment.DomainModel;

namespace ApplicationContainer.UI.Web.MA.ApplicationDevelopment.Customer.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
	{
		public CustomerDataModel SearchParameters
		{
			get
			{
				var data = new CustomerDataModel();

				SearchFilterControl.SetSearchParameters(data);

				CommonSearchParameters();

				return data;
			}
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			BaseSearchFilterControl = SearchFilterControl;
			SearchFilterControl.GetKendoComboBoConfigString = LoadKendoComboBoxSources;
		}
	}
}