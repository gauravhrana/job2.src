using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.Task;
using DataModel.TaskTimeTracker.TimeTracking;
using Shared.WebCommon.UI.Web;
using System.Data;
using TaskTimeTracker.Components.Module.TimeTracking;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.WBS.TaskXActivityInstance.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
    {

        #region variables 
       
        public TaskXActivityInstanceDataModel SearchParameters
        {
            get
            {
				var data = new TaskXActivityInstanceDataModel();

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