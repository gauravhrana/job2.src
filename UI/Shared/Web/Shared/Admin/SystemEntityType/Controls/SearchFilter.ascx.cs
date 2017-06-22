using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace Shared.UI.Web.Admin.SystemEntityType.Controls
{
	public partial class SearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilter
	{
        #region variables        

        public SystemEntityTypeDataModel SearchParameters
        {
            get
            {
                var data = new SystemEntityTypeDataModel();

				data.EntityName = CheckAndGetFieldValue(SystemEntityTypeDataModel.DataColumns.EntityName).ToString();

                return data;
            }
        }

        #endregion

        #region Events		

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntityKey				= "SystemEntityType";
            FolderLocationFromRoot			= "/Shared/Admin";
            PrimaryEntity					= Framework.Components.DataAccess.SystemEntity.SystemEntityType;

            SearchActionBarCore				= oSearchActionBar;
            SearchParametersRepeaterCore	= SearchParametersRepeater;
        }

        #endregion

	}
}