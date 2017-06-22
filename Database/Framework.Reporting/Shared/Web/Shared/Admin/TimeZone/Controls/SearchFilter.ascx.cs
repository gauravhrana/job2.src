using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Admin.TimeZone.Controls
{
    public partial class SearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilter
    {
        #region variables
        public TimeZoneDataModel SearchParameters
        {
            get
            {
				var data = new TimeZoneDataModel();

                if (SearchParametersRepeater.Items.Count != 0 && PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
				   StandardDataModel.StandardDataColumns.Name + "Visibility", SettingCategory)
                   && CheckAndGetFieldValue(
					   StandardDataModel.StandardDataColumns.Name) != "")
                {
                    data.Name = CheckAndGetFieldValue(
					   StandardDataModel.StandardDataColumns.Name).ToString();
                }

                return data;
            }
        }
        #endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TimeZone;
			PrimaryEntityKey = "TimeZone";
			FolderLocationFromRoot = "Shared/Admin/TimeZone";

			SearchActionBarCore = oSearchActionBar;
			SearchParametersRepeaterCore = SearchParametersRepeater;
		}

		#endregion
    }
}