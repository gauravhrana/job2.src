using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.Admin.Country
{
    public partial class CommonUpdate : PageCommonUpdate
    {
        #region Methods

        protected override DataTable UpdateData()    
        {
            var UpdatedData = new DataTable();
            var data = new CountryDataModel();
			UpdatedData = Framework.Components.Core.CountryDataManager.Search(data, SessionVariables.RequestProfile).Clone();
            for (var i = 0; i < SelectedData.Rows.Count; i++)
            {
                data.CountryId =
                    Convert.ToInt32(SelectedData.Rows[i][CountryDataModel.DataColumns.CountryId].ToString());
                data.Name = SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();
                data.Description =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
                    ? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description)
                    : SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

                data.SortOrder =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
                    ? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder).ToString())
                    : int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());

                data.TimeZoneId =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(CountryDataModel.DataColumns.TimeZoneId))
                    ? int.Parse(CheckAndGetRepeaterTextBoxValue(CountryDataModel.DataColumns.TimeZoneId).ToString())
                    : int.Parse(SelectedData.Rows[i][CountryDataModel.DataColumns.TimeZoneId].ToString());

                Framework.Components.Core.CountryDataManager.Update(data, SessionVariables.RequestProfile);
                data = new CountryDataModel();
                data.CountryId = Convert.ToInt32(SelectedData.Rows[i][CountryDataModel.DataColumns.CountryId].ToString());
				var dt = Framework.Components.Core.CountryDataManager.Search(data, SessionVariables.RequestProfile);

                if (dt.Rows.Count == 1)
                {
                    UpdatedData.ImportRow(dt.Rows[0]);
                }
            }
            return UpdatedData;
        }

        protected override DataTable GetEntityData(int? entityKey)
        {
            var countrydata = new CountryDataModel();
            countrydata.CountryId = entityKey;
			var results = Framework.Components.Core.CountryDataManager.Search(countrydata, SessionVariables.RequestProfile);
            return results;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Country;
            PrimaryEntityKey = "Country";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion

    }
}