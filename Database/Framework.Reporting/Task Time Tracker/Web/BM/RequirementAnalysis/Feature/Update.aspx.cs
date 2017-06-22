using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.Feature;
using Shared.WebCommon.UI.Web;
using DataModel.TaskTimeTracker.Task;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.Feature
{
    public partial class Update : Framework.UI.Web.BaseClasses.PageUpdate
    {
        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FeatureXFeatureRule;

            GenericControlPath = ApplicationCommon.GetControlPath("FeatureXFeatureRule", ControlType.GenericControl);
            PrimaryPlaceHolder = plcUpdateList;
            PrimaryEntityKey = "FeatureXFeatureRule";
            BreadCrumbObject = Master.BreadCrumbObject;

            BtnUpdate = btnUpdate;
            BtnClone = btnClone;
            BtnCancel = btnCancel;
        }

        #endregion


    }
}