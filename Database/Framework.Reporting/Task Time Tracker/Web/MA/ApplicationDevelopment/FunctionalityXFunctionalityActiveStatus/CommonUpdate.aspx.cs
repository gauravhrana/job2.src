using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityXFunctionalityActiveStatus
{
    public partial class CommonUpdate : Framework.UI.Web.BaseClasses.PageCommonUpdate
	{

		private DataTable GetData()
		{
			return SelectedData;
		}
        		

		protected void Page_Load(object sender, EventArgs e)
		{
			DynamicUpdatePanel.SetUp(GetColumns(), "FunctionalityXFunctionalityActiveStatus", GetData());
		}

		override protected void btnBack_Click(object sender, EventArgs e)
		{
			Response.Redirect(Page.GetRouteUrl("FunctionalityXFunctionalityActiveStatusEntityRoute", new { Action = "Default" }), false);

		}		

		override protected void btnUpdate_Click(object sender, EventArgs e)
		{
			var UpdatedData = new DataTable();
			var data = new FunctionalityXFunctionalityActiveStatusDataModel();
			UpdatedData = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityXFunctionalityActiveStatusDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.FunctionalityXFunctionalityActiveStatusId =
					Convert.ToInt32(SelectedData.Rows[i][FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.FunctionalityXFunctionalityActiveStatusId].ToString());
				data.FunctionalityId =
					Convert.ToInt32(SelectedData.Rows[i][FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.FunctionalityId].ToString());
				
				data.FunctionalityActiveStatusId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.FunctionalityActiveStatusId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.FunctionalityActiveStatusId).ToString())
					: int.Parse(SelectedData.Rows[i][FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.FunctionalityActiveStatusId].ToString());

				data.FunctionalityId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.FunctionalityId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.FunctionalityId).ToString())
					: int.Parse(SelectedData.Rows[i][FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.FunctionalityId].ToString());

				data.Memo =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.Memo))
					? CheckAndGetRepeaterTextBoxValue(FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.Memo)
					: SelectedData.Rows[i][FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.Memo].ToString();

				data.AcknowledgedBy =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.AcknowledgedBy))
					? CheckAndGetRepeaterTextBoxValue(FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.AcknowledgedBy)
					: SelectedData.Rows[i][FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.AcknowledgedBy].ToString();

				data.KnowledgeDate =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.KnowledgeDate))
                    ? DateTime.Parse(CheckAndGetRepeaterTextBoxValue(FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.KnowledgeDate).ToString())
                    : DateTime.Parse(SelectedData.Rows[i][FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.KnowledgeDate].ToString());


				TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityXFunctionalityActiveStatusDataManager.Update(data, SessionVariables.RequestProfile);
				data = new FunctionalityXFunctionalityActiveStatusDataModel();
				data.FunctionalityXFunctionalityActiveStatusId = Convert.ToInt32(SelectedData.Rows[i][FunctionalityXFunctionalityActiveStatusDataModel.DataColumns.FunctionalityXFunctionalityActiveStatusId].ToString());
				var dt = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityXFunctionalityActiveStatusDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
				}
			
			}
			DynamicUpdatePanel.SetUp(GetColumns(), "FunctionalityXFunctionalityActiveStatus", UpdatedData);
		}

		protected override void OnInit(EventArgs e)
		{

			try
			{
				DynamicUpdatePanel.AddColumns(GetColumns());
				SuperKey = ApplicationCommon.GetSuperKey();
				SetId = ApplicationCommon.GetSetId();
				var key = 0;
				var functionalityXFunctionalityActiveStatusdata = new FunctionalityXFunctionalityActiveStatusDataModel();
				var results = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityXFunctionalityActiveStatusDataManager.Search(functionalityXFunctionalityActiveStatusdata, SessionVariables.RequestProfile).Clone();

				if (!string.IsNullOrEmpty(SuperKey))
				{
					var data = new SuperKeyDetailDataModel();
					data.SuperKeyId = Convert.ToInt32(SuperKey);

					// Change System Entity Type
					data.SystemEntityTypeId = (int)Framework.Components.DataAccess.SystemEntity.FunctionalityXFunctionalityActiveStatus;
					var dt = Framework.Components.Core.SuperKeyDetailDataManager.Search(data, SessionVariables.RequestProfile);
					if (dt != null && dt.Rows.Count > 0)
					{
						foreach (System.Data.DataRow dr in dt.Rows)
						{
							key = Convert.ToInt32(dr[SuperKeyDetailDataModel.DataColumns.EntityKey]);
							functionalityXFunctionalityActiveStatusdata.FunctionalityXFunctionalityActiveStatusId = key;
							var functionalityXFunctionalityActiveStatusdt = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityXFunctionalityActiveStatusDataManager.Search(functionalityXFunctionalityActiveStatusdata, SessionVariables.RequestProfile);
							if (functionalityXFunctionalityActiveStatusdt.Rows.Count == 1)
							{
								results.ImportRow(functionalityXFunctionalityActiveStatusdt.Rows[0]);
							}
						}
					}
				}
				else
				{
					key = SetId;
					functionalityXFunctionalityActiveStatusdata.FunctionalityXFunctionalityActiveStatusId = key;
					var functionalityXFunctionalityActiveStatusdt = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityXFunctionalityActiveStatusDataManager.Search(functionalityXFunctionalityActiveStatusdata, SessionVariables.RequestProfile);
					if (functionalityXFunctionalityActiveStatusdt.Rows.Count == 1)
					{
						results.ImportRow(functionalityXFunctionalityActiveStatusdt.Rows[0]);
					}
				}
				SelectedData = new DataTable();
				SelectedData = results.Copy();
				base.OnInit(e);
			}
			catch (Exception ex)
			{

				System.Diagnostics.Debug.WriteLine(ex.Message);
				//throw
			}
		}
	}
}