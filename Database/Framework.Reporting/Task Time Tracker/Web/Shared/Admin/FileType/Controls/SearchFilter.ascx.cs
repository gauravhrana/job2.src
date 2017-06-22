using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.Framework.Import;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.FileType.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
	{
		public FileTypeDataModel SearchParameters
		{
			get
			{
				var data = new FileTypeDataModel();

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