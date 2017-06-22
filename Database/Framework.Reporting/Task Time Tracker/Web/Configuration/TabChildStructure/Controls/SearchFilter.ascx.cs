using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;
using System.Data;
using Shared.UI.Web.Controls;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.Configuration.TabChildStructure.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
	{
		#region variables

		public TabChildStructureDataModel SearchParameters
		{
			get
			{
				var data = new TabChildStructureDataModel();

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