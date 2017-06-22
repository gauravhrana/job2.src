using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace ApplicationContainer.UI.Web.Aptitude.Skill
{
    public partial class Clone : Framework.UI.Web.BaseClasses.PageClone
    {

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Skill;
            PrimaryEntityKey = "Skill";
            BreadCrumbObject = Master.BreadCrumbObject;
            GenericControlPath = ApplicationCommon.GetControlPath("Skill", ControlType.GenericControl);
            PrimaryGenericControl = myGenericControl;
        }

        #endregion

    }
}