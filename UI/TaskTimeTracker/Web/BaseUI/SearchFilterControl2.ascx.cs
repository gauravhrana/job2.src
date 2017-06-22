using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;

namespace ApplicationContainer.UI.Web.BaseUI
{
	public partial class SearchFilterControl2 : Framework.UI.Web.BaseClasses.ControlSearchFilter2
	{
		
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity                = SystemEntity.Client;
			PrimaryEntityKey             = "Client";
			FolderLocationFromRoot       = "Client";

			SearchActionBarCore          = oSearchActionBar;
			SearchParametersRepeaterCore = SearchParametersRepeater;
			PlaceHolderGridRowContainer  = plcGridRowContainer;

			TabHeaderContainer           = divTabHeaderList;
			TabContainer                 = divTabContentContainer;
		}

	}
}