using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.BaseUI;
using Shared.WebCommon.UI.Web;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using DataModel.Framework.DataAccess;

namespace Shared.UI.Web.SystemIntegrity.StoredProcedureLog.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
	{
		#region variables	

		public Framework.Components.LogAndTrace.StoredProcedureLogDataModel SearchParameters
		{
			get
			{
				var data = new Framework.Components.LogAndTrace.StoredProcedureLogDataModel();

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

			BaseSearchFilterControl = SearchFilterControl;
		}

		#endregion

	}
}