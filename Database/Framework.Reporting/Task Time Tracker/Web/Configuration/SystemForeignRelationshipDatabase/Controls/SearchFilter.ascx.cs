using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.BaseUI;
using Framework.Components.DataAccess;

namespace Shared.UI.Web.Configuration.SystemForeignRelationshipDatabase.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
	{
		#region variables

		public SystemForeignRelationshipDatabaseDataModel SearchParameters
		{
			get
			{
				var data = new SystemForeignRelationshipDatabaseDataModel();

                SearchFilterControl.SetSearchParameters(data);

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