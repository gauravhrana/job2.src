using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.Competency;
using DataModel.TaskTimeTracker.Task;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer.Task;
using TaskTimeTracker.Components.Module.Competency;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.WBS.TaskXCompetency.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
    {

        #region variables

        public TaskXCompetencyDataModel SearchParameters
        {
            get
            {
                var data = new TaskXCompetencyDataModel();

				SearchFilterControl.SetSearchParameters(data);

				return data;
		
            }
        }

        #endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			BaseSearchFilterControl							= SearchFilterControl;
			SearchFilterControl.GetKendoComboBoConfigString = LoadKendoComboBoxSources;
		}

		#endregion        

    }
}
