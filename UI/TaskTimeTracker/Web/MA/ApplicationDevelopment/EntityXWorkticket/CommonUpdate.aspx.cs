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
using Dapper;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.EntityXWorkTicket
{
	public partial class CommonUpdate : Framework.UI.Web.BaseClasses.PageCommonUpdate
	{

		private DataTable GetData()
		{
			return SelectedData;
		}


		protected void Page_Load(object sender, EventArgs e)
		{
			DynamicUpdatePanel.SetUp(GetColumns(), "EntityXWorkTicket", GetData());
		}

		override protected void btnBack_Click(object sender, EventArgs e)
		{
			Response.Redirect(Page.GetRouteUrl("EntityXWorkTicketEntityRoute", new { Action = "Default" }), false);

		}

		override protected void btnUpdate_Click(object sender, EventArgs e)
		{
			var UpdatedData = new List<EntityXWorkTicketDataModel>();
			var data = new EntityXWorkTicketDataModel();

			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.EntityXWorkTicketId =
					Convert.ToInt32(SelectedData.Rows[i][EntityXWorkTicketDataModel.DataColumns.EntityXWorkTicketId].ToString());
				data.EntityId =
					Convert.ToInt32(SelectedData.Rows[i][EntityXWorkTicketDataModel.DataColumns.EntityId].ToString());

				data.WorkTicketId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(EntityXWorkTicketDataModel.DataColumns.WorkTicketId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(EntityXWorkTicketDataModel.DataColumns.WorkTicketId).ToString())
					: int.Parse(SelectedData.Rows[i][EntityXWorkTicketDataModel.DataColumns.WorkTicketId].ToString());

				data.EntityId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(EntityXWorkTicketDataModel.DataColumns.EntityId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(EntityXWorkTicketDataModel.DataColumns.EntityId).ToString())
					: int.Parse(SelectedData.Rows[i][EntityXWorkTicketDataModel.DataColumns.EntityId].ToString());

				data.Memo =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(EntityXWorkTicketDataModel.DataColumns.Memo))
					? CheckAndGetRepeaterTextBoxValue(EntityXWorkTicketDataModel.DataColumns.Memo)
					: SelectedData.Rows[i][EntityXWorkTicketDataModel.DataColumns.Memo].ToString();

				data.AcknowledgedBy =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(EntityXWorkTicketDataModel.DataColumns.AcknowledgedBy))
					? CheckAndGetRepeaterTextBoxValue(EntityXWorkTicketDataModel.DataColumns.AcknowledgedBy)
					: SelectedData.Rows[i][EntityXWorkTicketDataModel.DataColumns.AcknowledgedBy].ToString();

				data.KnowledgeDate =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(EntityXWorkTicketDataModel.DataColumns.KnowledgeDate))
					? DateTime.Parse(CheckAndGetRepeaterTextBoxValue(EntityXWorkTicketDataModel.DataColumns.KnowledgeDate).ToString())
					: DateTime.Parse(SelectedData.Rows[i][EntityXWorkTicketDataModel.DataColumns.KnowledgeDate].ToString());


				TaskTimeTracker.Components.Module.ApplicationDevelopment.EntityXWorkTicketDataManager.Update(data, SessionVariables.RequestProfile);
				data = new EntityXWorkTicketDataModel();
				data.EntityXWorkTicketId = Convert.ToInt32(SelectedData.Rows[i][EntityXWorkTicketDataModel.DataColumns.EntityXWorkTicketId].ToString());
				var dt = TaskTimeTracker.Components.Module.ApplicationDevelopment.EntityXWorkTicketDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

				if (dt.Count == 1)
				{
					UpdatedData.Add(dt[0]);
				}

			}
			DynamicUpdatePanel.SetUp(GetColumns(), "EntityXWorkTicket", UpdatedData.ToDataTable());
		}

		protected override void OnInit(EventArgs e)
		{

			try
			{
				DynamicUpdatePanel.AddColumns(GetColumns());
				SuperKey = ApplicationCommon.GetSuperKey();
				SetId = ApplicationCommon.GetSetId();
				var key = 0;
				var EntityXWorkTicketdata = new EntityXWorkTicketDataModel();
                var results = new List<EntityXWorkTicketDataModel>();

				if (!string.IsNullOrEmpty(SuperKey))
				{
					var data = new SuperKeyDetailDataModel();
					data.SuperKeyId = Convert.ToInt32(SuperKey);

					// Change System Entity Type
					data.SystemEntityTypeId = (int)Framework.Components.DataAccess.SystemEntity.EntityXWorkTicket;
					var listSuperKeyDetails = Framework.Components.Core.SuperKeyDetailDataManager.GetEntityDetails(data, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);

                    if (listSuperKeyDetails != null && listSuperKeyDetails.Count > 0)
                    {
                        foreach (var itemSuperKeyDetail in listSuperKeyDetails)
                        {
                            key = itemSuperKeyDetail.EntityKey.Value;
							EntityXWorkTicketdata.EntityXWorkTicketId = key;
							var EntityXWorkTicketdt = TaskTimeTracker.Components.Module.ApplicationDevelopment.EntityXWorkTicketDataManager.GetEntityDetails(EntityXWorkTicketdata, SessionVariables.RequestProfile);
							if (EntityXWorkTicketdt.Count == 1)
							{
								results.Add(EntityXWorkTicketdt[0]);
							}
						}
					}
				}
				else
				{
					key = SetId;
					EntityXWorkTicketdata.EntityXWorkTicketId = key;
					var EntityXWorkTicketdt = TaskTimeTracker.Components.Module.ApplicationDevelopment.EntityXWorkTicketDataManager.GetEntityDetails(EntityXWorkTicketdata, SessionVariables.RequestProfile);
					if (EntityXWorkTicketdt.Count == 1)
					{
						results.Add(EntityXWorkTicketdt[0]);
					}
				}
				SelectedData = results.ToDataTable();
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