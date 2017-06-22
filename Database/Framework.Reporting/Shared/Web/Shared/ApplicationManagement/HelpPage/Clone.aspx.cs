using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.HelpPage
{
    public partial class Clone : Framework.UI.Web.BaseClasses.PageClone
    {
        #region private methods

        protected override void InsertData()
        {
            var data = new HelpPageDataModel();

            data.HelpPageId         = myGenericControl.HelpPageId;
            data.Name               = myGenericControl.Name;
            data.Content            = myGenericControl.Content;
            data.SortOrder          = myGenericControl.SortOrder;
            data.SystemEntityTypeId = myGenericControl.SystemEntityTypeId;
            data.HelpPageContextId  = myGenericControl.HelpPageContextId;

			Framework.Components.Core.HelpPageDataManager.Create(data, SessionVariables.RequestProfile);
        }

        #endregion

        #region Events      

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			
			SettingCategory = "HelpPageDefaultView";
			
		}

        #endregion

    }
}