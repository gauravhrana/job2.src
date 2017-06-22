using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using Framework.Components.DataAccess;
using ApplicationContainer.UI.Web.BaseUI;

namespace Shared.UI.Web.Configuration.SystemForeignRelationshipType.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
	{
		#region variables

		public SystemForeignRelationshipTypeDataModel SearchParameters
		{
			get
			{
				var data = new SystemForeignRelationshipTypeDataModel();

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