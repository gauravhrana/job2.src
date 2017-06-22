using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.Import;

namespace Shared.UI.Web.BatchFileSet.Controls
{
    public partial class SearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilter
    {
        #region variables
        public BatchFileSetDataModel SearchParameters
        {
            get
            {
                var data = new BatchFileSetDataModel();


                if (SearchParametersRepeater.Items.Count != 0 && PreferenceUtility.GetUserPreferenceByKeyAsBoolean(
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

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.BatchFileSet;
			PrimaryEntityKey = "BatchFileSet";
			FolderLocationFromRoot = "Shared/Admin/BatchFileSet";

			SearchActionBarCore = oSearchActionBar;
			SearchParametersRepeaterCore = SearchParametersRepeater;
		}

		#endregion
    }
}