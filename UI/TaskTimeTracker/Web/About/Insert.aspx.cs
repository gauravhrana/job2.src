using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.ReleaseLog;

namespace ApplicationContainer.UI.Web.About
{
	public partial class Insert : Framework.UI.Web.BaseClasses.PageInsert
    {
        #region private methods

        protected void InsertData()
        {
            var data = new ReleaseLogDataModel();
            var AuditId = SessionVariables.RequestProfile.AuditId;
            data.ReleaseLogId = myGenericControl.ReleaseLogId;
            data.Name = myGenericControl.Name;
            data.VersionNo = myGenericControl.VersionNo;
            data.ReleaseDate = DateTime.Parse(myGenericControl.ReleaseDate);
            data.Description = myGenericControl.Description;
            data.SortOrder = myGenericControl.SortOrder;

			Framework.Components.ReleaseLog.ReleaseLogDataManager.Create(data, SessionVariables.RequestProfile);
        }

        #endregion

        #region Events

    

        #endregion
    }
}