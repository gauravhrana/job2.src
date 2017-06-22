﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.WBS.TaskType
{
    public partial class Update : Framework.UI.Web.BaseClasses.PageUpdate
    {
        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskType;

            GenericControlPath = ApplicationCommon.GetControlPath("TaskType", ControlType.GenericControl);
            PrimaryPlaceHolder = plcUpdateList;
            PrimaryEntityKey = "TaskType";
            BreadCrumbObject = Master.BreadCrumbObject;

            BtnUpdate = btnUpdate;
            BtnClone = btnClone;
            BtnCancel = btnCancel;
        }

        #endregion

    }
}