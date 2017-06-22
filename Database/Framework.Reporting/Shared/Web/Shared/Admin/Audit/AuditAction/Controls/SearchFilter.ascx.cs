using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.Admin.Audit.AuditAction.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
	{
		public DataModel.Framework.Audit.AuditActionDataModel SearchParameters
		{
			get
			{
				var data = new DataModel.Framework.Audit.AuditActionDataModel();

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
