using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.Configuration;
using DataModel.Framework.ReleaseLog;
using DataModel.Framework.DataAccess;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Text;
using System.Globalization;
using Shared.UI.Web.Controls;

namespace Shared.UI.Web.ApplicationManagement.ReleaseLog.Controls
{
	public partial class ReleaseNotesSearchControl : Framework.UI.Web.BaseClasses.ControlSearchFilterEntity
    {

		public string GroupBy
		{
			get
			{
				return SearchFilterControl.GroupBy;
			}
		}

		public string SubGroupBy
		{
			get
			{
				return SearchFilterControl.SubGroupBy;
			}
		}

		public ApplicationContainer.UI.Web.BaseUI.SearchFilterControl SearchControl
		{
			get
			{
				return SearchFilterControl;
				
			}
		}

        public ReleaseLogDetailDataModel SearchParameters
        {
            get
            {
                var data                      = new ReleaseLogDetailDataModel();
				
				SearchFilterControl.SetSearchParameters(data);
				
							
				var releaseDates              = SearchFilterControl.GetParameterValueForDatePanel(ReleaseLogDetailDataModel.DataColumns.ReleaseDate);
				data.ReleaseDateMin           = releaseDates.Count > 0 ? releaseDates[0] : null;
				data.ReleaseDateMax           = releaseDates.Count > 1 ? releaseDates[1] : null;
				
				
				var updatedRanges             = SearchFilterControl.GetParameterValueForDatePanel(ReleaseLogDetailDataModel.DataColumns.UpdatedRange);
				data.UpdatedDateRangeMin      = updatedRanges.Count > 0 ? updatedRanges[0] : null;
				data.UpdatedDateRangeMax      = updatedRanges.Count > 1 ? updatedRanges[1] : null;

				CommonSearchParameters();

                return data;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

			BaseSearchFilterControl                         = SearchFilterControl;
			SearchFilterControl.GetKendoComboBoConfigString = LoadKendoComboBoxSources;
        }
        
    }
}