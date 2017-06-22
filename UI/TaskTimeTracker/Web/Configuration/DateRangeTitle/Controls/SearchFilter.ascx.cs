using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using DataModel.Framework.DataAccess;
using Shared.UI.Web.Controls;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using ApplicationContainer.UI.Web.BaseUI;

namespace Shared.UI.Web.Configuration.DateRangeTitle.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
	{
		#region variables		

		public DateRangeTitleDataModel SearchParameters
		{
			get
			{
				var data = new DateRangeTitleDataModel();

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