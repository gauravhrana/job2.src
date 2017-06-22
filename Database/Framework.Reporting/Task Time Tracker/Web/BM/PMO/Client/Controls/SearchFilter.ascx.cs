using System;
using DataModel.TaskTimeTracker;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.PMO.Client.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
    {
        public ClientDataModel SearchParameters
        {
            get
            {
                var data = new ClientDataModel();

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