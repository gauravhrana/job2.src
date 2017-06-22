using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.Framework.Import;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.Admin.FileType
{
    public partial class CommonUpdate : PageCommonUpdate
    {
        #region Methods

        protected override DataTable UpdateData()
        {
            var UpdatedData = new DataTable();

            var data = new FileTypeDataModel();
			UpdatedData = Framework.Components.Import.FileTypeDataManager.Search(data, SessionVariables.RequestProfile).Clone();
            for (var i = 0; i < SelectedData.Rows.Count; i++)
            {
                data.FileTypeId =
                    Convert.ToInt32(SelectedData.Rows[i][FileTypeDataModel.DataColumns.FileTypeId].ToString());
                data.Name = SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();
                data.Description =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
                    ? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description)
                    : SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

                data.SortOrder =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
                    ? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder).ToString())
                    : int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());

				Framework.Components.Import.FileTypeDataManager.Update(data, SessionVariables.RequestProfile);
                data = new FileTypeDataModel();
                data.FileTypeId = Convert.ToInt32(SelectedData.Rows[i][FileTypeDataModel.DataColumns.FileTypeId].ToString());
				var dt = Framework.Components.Import.FileTypeDataManager.Search(data, SessionVariables.RequestProfile);

                if (dt.Rows.Count == 1)
                {
                    UpdatedData.ImportRow(dt.Rows[0]);
                }
            }
            return UpdatedData;
        }

        protected override DataTable GetEntityData(int? entityKey)
        {
            var fileTypedata = new FileTypeDataModel();
            fileTypedata.FileTypeId = entityKey;
			var results = Framework.Components.Import.FileTypeDataManager.Search(fileTypedata, SessionVariables.RequestProfile);
            return results;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {

            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FileType;
            PrimaryEntityKey = "FileType";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion

    }
}