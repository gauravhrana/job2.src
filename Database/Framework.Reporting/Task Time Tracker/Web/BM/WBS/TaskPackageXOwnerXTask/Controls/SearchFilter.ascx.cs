using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.Task;
using DataModel.Framework.DataAccess;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.BaseUI;

namespace ApplicationContainer.UI.Web.TaskPackageXOwnerXTask.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
	{

		#region variables				

		public TaskPackageXOwnerXTaskDataModel SearchParameters
		{
			get
			{
				var data = new TaskPackageXOwnerXTaskDataModel();

				SearchFilterControl.SetSearchParameters(data);

				CommonSearchParameters();				
				
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