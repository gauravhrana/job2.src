using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;


namespace Shared.UI.Web.Admin.Log4Net.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
	{
		public Framework.Components.LogAndTrace.Log4NetDataModel SearchParameters
		{
			get
			{
				var data = new Framework.Components.LogAndTrace.Log4NetDataModel();


				SearchFilterControl.SetSearchParameters(data);

				CommonSearchParameters();

				var date = SearchFilterControl.CheckAndGetFieldValue(Framework.Components.LogAndTrace.Log4NetDataModel.DataColumns.Date).ToString();
				if (!string.IsNullOrEmpty(date))
				{
					var dates = date.Split('&');
					if (Boolean.Parse(dates[2]))
					{
						data.Date = DateTimeHelper.FromApplicationDateFormatToDate(dates[0]);						
					}
				}
    

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