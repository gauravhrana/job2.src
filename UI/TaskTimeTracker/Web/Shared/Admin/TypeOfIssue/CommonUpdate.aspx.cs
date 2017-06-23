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
using DataModel.Framework.Audit;
using Framework.UI.Web.BaseClasses;
using Dapper;

namespace Shared.UI.Web.Admin.TypeOfIssue
{
    public partial class CommonUpdate : PageCommonUpdate
    {
        #region Methods

        protected override DataTable UpdateData()
        {
            var UpdatedData = new List<TypeOfIssueDataModel>();		
			var data = new TypeOfIssueDataModel();

			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.TypeOfIssueId =
					Convert.ToInt32(SelectedData.Rows[i][TypeOfIssueDataModel.DataColumns.TypeOfIssueId].ToString());
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

				data.Category =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(TypeOfIssueDataModel.DataColumns.Category))
					? CheckAndGetRepeaterTextBoxValue(TypeOfIssueDataModel.DataColumns.Category).ToString()
					: SelectedData.Rows[i][TypeOfIssueDataModel.DataColumns.Category].ToString();

                Framework.Components.Audit.TypeOfIssueDataManager.Update(data, SessionVariables.RequestProfile);
				data = new TypeOfIssueDataModel();
				data.TypeOfIssueId = Convert.ToInt32(SelectedData.Rows[i][TypeOfIssueDataModel.DataColumns.TypeOfIssueId].ToString());
				var dt = Framework.Components.Audit.TypeOfIssueDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

                if (dt.Count == 1)
                {
                    UpdatedData.Add(dt[0]);
                }
            }
            return UpdatedData.ToDataTable();
        }
       
        protected override DataTable GetEntityData(int? entityKey)
        {
            var typeOfIssuedata = new TypeOfIssueDataModel();
            typeOfIssuedata.TypeOfIssueId = entityKey;
			var results = Framework.Components.Audit.TypeOfIssueDataManager.GetEntityDetails(typeOfIssuedata, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);
            return results.ToDataTable();
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {

            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TypeOfIssue;
            PrimaryEntityKey = "TypeOfIssue";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion

    }
}