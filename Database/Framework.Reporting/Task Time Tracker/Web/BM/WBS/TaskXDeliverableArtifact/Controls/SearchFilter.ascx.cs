using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker;
using DataModel.TaskTimeTracker.Task;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;
using TaskTimeTracker.Components.BusinessLayer.Task;
using ApplicationContainer.UI.Web.BaseUI;

namespace ApplicationContainer.UI.Web.TaskXDeliverableArtifact.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
    {

        #region variables      

        public TaskXDeliverableArtifactDataModel SearchParameters
        {
            get
            {
				var data = new TaskXDeliverableArtifactDataModel();

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