using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Framework.Components.DataAccess;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.SystemIntegrity.QuickPaginationRun
{
    public partial class Insert : Framework.UI.Web.BaseClasses.PageInsert
    {

        #region private methods

        protected void InsertData()
        {
            var data = new QuickPaginationRunDataModel();

            data.QuickPaginationRunId = myGenericControl.QuickPaginationRunId;
            data.SystemEntityTypeId   = myGenericControl.SystemEntityTypeId;
            data.ApplicationUserId    = myGenericControl.ApplicationUserId;
            data.SortClause           = myGenericControl.SortClause;
            data.WhereClause          = myGenericControl.WhereClause;
            data.ExpirationTime       = myGenericControl.ExpirationTime;

			Framework.Components.Core.QuickPaginationRunDatatManager.Create(data, SessionVariables.RequestProfile);
        }

        #endregion

        #region Events

        

		protected override void OnInit(EventArgs e)
		{
			
			base.OnInit(e);

			PrimaryEntity = SystemEntity.QuickPaginationRun;
			PrimaryEntityKey = "QuickPaginationRun";
			PrimaryGenericControl = myGenericControl;
			BreadCrumbObject = Master.BreadCrumbObject;	
			
		}

        

        

        #endregion

    }
}