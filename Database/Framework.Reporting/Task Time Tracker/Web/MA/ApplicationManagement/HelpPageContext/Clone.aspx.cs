using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.ApplicationManagement.HelpPageContext
{
	public partial class Clone : Framework.UI.Web.BaseClasses.PageClone
	{

		#region private methods

		protected override void InsertData()
		{
            var data = new HelpPageContextDataModel();

			data.HelpPageContextId   = myGenericControl.HelpPageContextId;
			data.Name                = myGenericControl.Name;
			data.Description         = myGenericControl.Description;
			data.SortOrder           = myGenericControl.SortOrder;

			Framework.Components.Core.HelpPageContextDataManager.Create(data, SessionVariables.RequestProfile);
		}

		#endregion

		#region Events

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			
			SettingCategory = "HelpPageContextDefaultView";
			
		}

		#endregion
	}
}