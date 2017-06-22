using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker;
using Framework.Components.DataAccess;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.ProjectPortfolioGroupXProjectPortfolio
{
	public partial class CommonUpdate : PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new DataTable();

			var data = new ProjectPortfolioGroupXProjectPortfolioDataModel();
            UpdatedData = ProjectPortfolioGroupXProjectPortfolioDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.ProjectPortfolioGroupXProjectPortfolioId =
					Convert.ToInt32(SelectedData.Rows[i][ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.ProjectPortfolioGroupXProjectPortfolioId].ToString());
				
				data.ProjectPortfolioGroupId =
					Convert.ToInt32(SelectedData.Rows[i][ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.ProjectPortfolioGroupId].ToString());
				
				data.ProjectPortfolioId =
					Convert.ToInt32(SelectedData.Rows[i][ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.ProjectPortfolioId].ToString());

				data.Description =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.Description))
					? CheckAndGetRepeaterTextBoxValue(ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.Description)
					: SelectedData.Rows[i][ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.Description].ToString();

				data.SortOrder =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.SortOrder))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.SortOrder).ToString())
					: int.Parse(SelectedData.Rows[i][ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.SortOrder].ToString());

				data.ApplicationId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(BaseDataModel.BaseDataColumns.ApplicationId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(BaseDataModel.BaseDataColumns.ApplicationId).ToString())
					: int.Parse(SelectedData.Rows[i][BaseDataModel.BaseDataColumns.ApplicationId].ToString());

				data.CreatedByAuditId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.CreatedByAuditId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.CreatedByAuditId).ToString())
					: int.Parse(SelectedData.Rows[i][ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.CreatedByAuditId].ToString());

				data.ModifiedByAuditId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.ModifiedByAuditId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.ModifiedByAuditId).ToString())
					: int.Parse(SelectedData.Rows[i][ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.ModifiedByAuditId].ToString());

				data.CreatedDate =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.CreatedDate))
					? DateTime.Parse(CheckAndGetRepeaterTextBoxValue(ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.CreatedDate).ToString())
					: DateTime.Parse(SelectedData.Rows[i][ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.CreatedDate].ToString());

				data.ModifiedDate =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.ModifiedDate))
					? DateTime.Parse(CheckAndGetRepeaterTextBoxValue(ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.ModifiedDate).ToString())
					: DateTime.Parse(SelectedData.Rows[i][ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.ModifiedDate].ToString());

				ProjectPortfolioGroupXProjectPortfolioDataManager.Update(data, SessionVariables.RequestProfile);
				data = new ProjectPortfolioGroupXProjectPortfolioDataModel();
				data.ProjectPortfolioGroupXProjectPortfolioId = Convert.ToInt32(SelectedData.Rows[i][ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.ProjectPortfolioGroupXProjectPortfolioId].ToString());
                var dt = ProjectPortfolioGroupXProjectPortfolioDataManager.Search(data,  SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
				}
			}
			return UpdatedData;
		}		

		protected override DataTable GetEntityData(int? entityKey)
		{
			var projectPortfolioGroupXProjectPortfoliodata = new ProjectPortfolioGroupXProjectPortfolioDataModel();
			projectPortfolioGroupXProjectPortfoliodata.ProjectPortfolioGroupXProjectPortfolioId = entityKey;
            var results = ProjectPortfolioGroupXProjectPortfolioDataManager.Search(projectPortfolioGroupXProjectPortfoliodata, SessionVariables.RequestProfile);
			return results;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{

			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity = SystemEntity.ProjectPortfolioGroupXProjectPortfolio;
			PrimaryEntityKey = "ProjectPortfolioGroupXProjectPortfolio";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion

	}
}