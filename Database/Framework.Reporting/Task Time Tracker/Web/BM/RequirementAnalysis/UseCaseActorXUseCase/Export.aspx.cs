﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using Shared.WebCommon.UI.Web;
using Shared.UI.Web.Controls;

namespace ApplicationContainer.UI.Web.UseCaseActorXUseCase
{
	public partial class Export : Shared.UI.WebFramework.BasePage
	{
		#region variables

		string searchCondition = String.Empty;

		#endregion

		#region private methods

        private System.Data.DataTable GetData()
        {
            // TODO: on all export pages 
            var data = new UseCaseActorXUseCaseDataModel();

            var dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseActorXUseCaseDataManager.Search(data, SessionVariables.RequestProfile);

            return dt;
        }

		private string[] GetColumns()
		{
			var validColumns = FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.UseCaseActorXUseCase, "DBColumns", SessionVariables.RequestProfile);
			return validColumns;
		}

		#endregion

		#region Events

		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				// searchCondition = Request.QueryString["SearchCondition"];

				oList.Setup("UseCaseActorXUseCase", " ", "UseCaseActorXUseCaseId", false, GetData, GetColumns, false);
				oList.ExportMenu.Visible = false;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);

			}
		}

		protected override void OnLoadComplete(EventArgs e)
		{
			base.OnLoadComplete(e);
			oList.ShowData(true, true);
		}

		#endregion
	}
}