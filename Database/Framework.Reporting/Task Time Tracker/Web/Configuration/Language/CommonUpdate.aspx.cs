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

namespace Shared.UI.Web.Configuration.Language
{
    public partial class CommonUpdate : PageCommonUpdate
    {
        #region Method

        protected override DataTable UpdateData()	
        {
            var UpdatedData = new DataTable();
            var data = new LanguageDataModel();
			UpdatedData = Framework.Components.Core.LanguageDataManager.Search(data, SessionVariables.RequestProfile).Clone();
            for (var i = 0; i < SelectedData.Rows.Count; i++)
            {
                data.LanguageId =
                    Convert.ToInt32(SelectedData.Rows[i][LanguageDataModel.DataColumns.LanguageId].ToString());
                data.Name = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Name))
                    ? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Name).ToString()
                    : SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();

                data.Description =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
                    ? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description).ToString()
                    : SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

                data.SortOrder =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
                    ? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder).ToString())
                    : int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());

				Framework.Components.Core.LanguageDataManager.Update(data, SessionVariables.RequestProfile);
                data = new LanguageDataModel();
                data.LanguageId = Convert.ToInt32(SelectedData.Rows[i][LanguageDataModel.DataColumns.LanguageId].ToString());
				var dt = Framework.Components.Core.LanguageDataManager.Search(data, SessionVariables.RequestProfile);

                if (dt.Rows.Count == 1)
                {
                    UpdatedData.ImportRow(dt.Rows[0]);
                }
            }
            return UpdatedData;
        }

        protected override DataTable GetEntityData(int? entityKey)
        {
            var languagedata = new LanguageDataModel();
            languagedata.LanguageId = entityKey;
			var results = Framework.Components.Core.LanguageDataManager.Search(languagedata, SessionVariables.RequestProfile);
            return results;
        }
        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Language;
            PrimaryEntityKey = "Language";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion


    }
}