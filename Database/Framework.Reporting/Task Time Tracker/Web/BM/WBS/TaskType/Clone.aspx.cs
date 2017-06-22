using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace ApplicationContainer.UI.Web.WBS.TaskType
{
    public partial class Clone : Framework.UI.Web.BaseClasses.PageClone
    {

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskType;
            PrimaryEntityKey = "TaskType";
            BreadCrumbObject = Master.BreadCrumbObject;
            GenericControlPath = ApplicationCommon.GetControlPath("TaskType", ControlType.GenericControl);
            PrimaryGenericControl = myGenericControl;
        }

        #endregion

    }
}