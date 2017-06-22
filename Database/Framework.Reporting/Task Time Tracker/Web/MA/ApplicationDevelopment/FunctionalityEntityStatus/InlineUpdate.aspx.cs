﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityEntityStatus
{
	public partial class InlineUpdate : Shared.UI.WebFramework.BasePage
	{
		public delegate void UpdateDelegate(Dictionary<string, string> values);
		private DataTable GetData()
		{
			try
			{
				var superKey = ApplicationCommon.GetSuperKey();
                var setId = ApplicationCommon.GetSetId();
				var selectedrows = new DataTable();
				var FunctionalityEntityStatusdata = new FunctionalityEntityStatusDataModel();

				selectedrows = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityEntityStatusDataManager.GetDetails(FunctionalityEntityStatusdata, SessionVariables.RequestProfile).Clone();
				if (!string.IsNullOrEmpty(superKey))
				{
					var data = new SuperKeyDetailDataModel();
					data.SuperKeyId = Convert.ToInt32(superKey);
					// Change System Entity Type
					data.SystemEntityTypeId = (int)Framework.Components.DataAccess.SystemEntity.FunctionalityEntityStatus;
					var dt = Framework.Components.Core.SuperKeyDetailDataManager.Search(data, SessionVariables.RequestProfile);

					if (dt != null && dt.Rows.Count > 0)
					{
						var keys = new int[dt.Rows.Count];
						for (var i = 0; i < dt.Rows.Count; i++)
						{

							keys[i] = Convert.ToInt32(dt.Rows[i][SuperKeyDetailDataModel.DataColumns.EntityKey]);
							FunctionalityEntityStatusdata.FunctionalityEntityStatusId = keys[i];
							var result = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityEntityStatusDataManager.GetDetails(FunctionalityEntityStatusdata, SessionVariables.RequestProfile);
							selectedrows.ImportRow(result.Rows[0]);


						}
					}
				}
				else 
				{
					var key = setId;
					FunctionalityEntityStatusdata.FunctionalityEntityStatusId = key;
					var result = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityEntityStatusDataManager.GetDetails(FunctionalityEntityStatusdata, SessionVariables.RequestProfile);
					selectedrows.ImportRow(result.Rows[0]);

				}
				return selectedrows;
			}
			catch (Exception ex)
			{
				Response.Write(ex.Message);
			}
			return null;
		}

        private string[] GetColumns()
		{

			return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.FunctionalityEntityStatus, "InlineUpdate", SessionVariables.RequestProfile);
		}

		protected override void OnInit(EventArgs e)
		{
			InlineEditingList.AddColumns(GetColumns());

		}

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            
            SettingCategory = "FunctionalityEntityStatusDefaultView";
           

        }

		protected void Page_Load(object sender, EventArgs e)
		{
			UpdateDelegate delupdate = new UpdateDelegate(Update);
			this.InlineEditingList.DelUpdateRef = delupdate;
			if (!IsPostBack)
			{
				InlineEditingList.SetUp(GetColumns(), "FunctionalityEntityStatus", GetData());
                for (var i = 0; i < InlineEditingList.EditableGridViewControl.HeaderRow.Cells.Count; i++)
                {
                    if (InlineEditingList.EditableGridViewControl.HeaderRow.Cells[i].Text.ToLower().Contains("id"))
                        InlineEditingList.EditableGridViewControl.Columns[i].Visible = false;
                }
			}
		}

		private void Update(Dictionary<string, string> values)
		{
			var data = new FunctionalityEntityStatusDataModel();
			data.FunctionalityEntityStatusId = int.Parse(values[FunctionalityEntityStatusDataModel.DataColumns.FunctionalityEntityStatusId].ToString());
			data.AssignedTo = values[FunctionalityEntityStatusDataModel.DataColumns.AssignedTo].ToString();
			data.Memo = values[FunctionalityEntityStatusDataModel.DataColumns.Memo].ToString();
            data.TargetDate = DateTime.Parse(values[FunctionalityEntityStatusDataModel.DataColumns.TargetDate].ToString());
            data.StartDate = DateTime.Parse(values[FunctionalityEntityStatusDataModel.DataColumns.StartDate].ToString());
            data.SystemEntityTypeId = int.Parse(values[FunctionalityEntityStatusDataModel.DataColumns.SystemEntityTypeId].ToString());
            data.FunctionalityId = int.Parse(values[FunctionalityEntityStatusDataModel.DataColumns.FunctionalityId].ToString());
            data.FunctionalityPriorityId = int.Parse(values[FunctionalityEntityStatusDataModel.DataColumns.FunctionalityPriorityId].ToString());
            data.FunctionalityStatusId = int.Parse(values[FunctionalityEntityStatusDataModel.DataColumns.FunctionalityStatusId].ToString());
			TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityEntityStatusDataManager.Update(data, SessionVariables.RequestProfile);
			InlineEditingList.Data = GetData();
		}
	}
}