using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.SystemIntegrity.QuickPaginationRun
{
    public partial class Clone : Framework.UI.Web.BaseClasses.PageClone
    {

        #region private methods

        protected override void InsertData()
        {
            var data = new QuickPaginationRunDataModel();

            data.QuickPaginationRunId     = myGenericControl.QuickPaginationRunId;
            data.SystemEntityTypeId       = myGenericControl.SystemEntityTypeId;
            data.ApplicationUserId        = myGenericControl.ApplicationUserId;
            data.SortClause               = myGenericControl.SortClause;
            data.WhereClause              = myGenericControl.WhereClause;
            data.ExpirationTime           = myGenericControl.ExpirationTime;

			Framework.Components.Core.QuickPaginationRunDatatManager.Create(data, SessionVariables.RequestProfile);
        }

        #endregion

        #region Events
       
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			
			SettingCategory = "QuickPaginationRunDefaultView";
			
		}
       
        #endregion

    }
}