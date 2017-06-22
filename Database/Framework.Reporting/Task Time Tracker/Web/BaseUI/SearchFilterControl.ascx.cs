using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Shared.UI.Web.Controls;
using ControlSearchFilter = Framework.UI.Web.BaseClasses.ControlSearchFilter;

namespace ApplicationContainer.UI.Web.BaseUI
{
	public partial class SearchFilterControl : ControlSearchFilter
	{
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity					= SystemEntity.Client;
			PrimaryEntityKey				= "{NA}";
			FolderLocationFromRoot			= "{NA}";

			SearchActionBarCore				= oSearchActionBar;
			SearchParametersRepeaterCore	= SearchParametersRepeater;

			TabHeaderContainer		= divTabHeaderList;
			TabContainer			= divTabContentContainer;
		}
	}
}