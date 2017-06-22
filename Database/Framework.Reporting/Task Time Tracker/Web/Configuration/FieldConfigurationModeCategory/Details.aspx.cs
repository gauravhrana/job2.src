using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Configuration.FieldConfigurationModeCategory
{
    public partial class Details : Framework.UI.Web.BaseClasses.PageDetails
    {

        #region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FieldConfigurationModeCategory;
			PrimaryEntityKey = "FieldConfigurationModeCategory";
			DetailsControlPath = ApplicationCommon.GetControlPath("FieldConfigurationModeCategory", ControlType.DetailsControl);
			PrimaryPlaceHolder = oDetailsControl.PlaceHolderDetails;
			BreadCrumbObject = Master.BreadCrumbObject;
		}

        #endregion
    }
}