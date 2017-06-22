using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityImageAttribute
{
	public partial class Clone : Framework.UI.Web.BaseClasses.PageClone
	{

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FunctionalityImageAttribute;
            PrimaryEntityKey = "FunctionalityImageAttribute";
            BreadCrumbObject = Master.BreadCrumbObject;
            GenericControlPath = ApplicationCommon.GetControlPath("FunctionalityImageAttribute", ControlType.GenericControl);
            PrimaryGenericControl = myGenericControl;
        }
	}
} 