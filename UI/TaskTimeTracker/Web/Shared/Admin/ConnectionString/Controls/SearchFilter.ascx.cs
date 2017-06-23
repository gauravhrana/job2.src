using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using System.Data;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.RequirementAnalysis;
namespace Shared.UI.Web.Admin.ConnectionString.Controls
{
	public partial class SearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilter
	{
        #region variables        

        public ConnectionStringDataModel SearchParameters
        {
            get
            {
                var data = new ConnectionStringDataModel();

				data.Name = CheckAndGetFieldValue(StandardDataModel.StandardDataColumns.Name).ToString();
                
                return data;
            }
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

			PrimaryEntityKey				= "ConnectionString";
			FolderLocationFromRoot			= "ConnectionString";
			PrimaryEntity					= Framework.Components.DataAccess.SystemEntity.ConnectionString;

			SearchActionBarCore				= oSearchActionBar;
			SearchParametersRepeaterCore	= SearchParametersRepeater;
        }

        #endregion

	}
}