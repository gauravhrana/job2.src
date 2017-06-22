using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.DataAccess;
using Framework.UI.Web.BaseClasses;
using Framework.Components.DataAccess;
using ApplicationContainer.UI.Web.BaseUI;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.WorkTicket.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
	{
		#region variables

		public WorkTicketDataModel SearchParameters
		{
			get
			{
				var data = new WorkTicketDataModel();

				SearchFilterControl.SetSearchParameters(data);

				return data;
			}
		}

		#endregion

		#region private methods

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			BaseSearchFilterControl = SearchFilterControl;
		}

		#endregion
	}
}