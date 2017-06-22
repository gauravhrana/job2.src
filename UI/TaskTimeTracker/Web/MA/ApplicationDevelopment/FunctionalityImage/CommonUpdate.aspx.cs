using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using System.Text;
using TaskTimeTracker.Components.Module.ApplicationDevelopment;
using Dapper;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityImage
{
    public partial class CommonUpdate : PageCommonUpdate
	{
       #region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new List<FunctionalityImageDataModel>();
			var data = new FunctionalityImageDataModel();

			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.FunctionalityImageId =
					Convert.ToInt32(SelectedData.Rows[i][FunctionalityImageDataModel.DataColumns.FunctionalityImageId].ToString());

				data.Title =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FunctionalityImageDataModel.DataColumns.Title))
					? CheckAndGetRepeaterTextBoxValue(FunctionalityImageDataModel.DataColumns.Title)
					: SelectedData.Rows[i][FunctionalityImageDataModel.DataColumns.Title].ToString();

				data.ApplicationId =
					Convert.ToInt32(SelectedData.Rows[i][BaseDataModel.BaseDataColumns.ApplicationId].ToString());
				data.Image =
					Encoding.ASCII.GetBytes(SelectedData.Rows[i][FunctionalityImageDataModel.DataColumns.Image].ToString());

				FunctionalityImageDataManager.Update(data, SessionVariables.RequestProfile);
				data = new FunctionalityImageDataModel();
				data.FunctionalityImageId =
					Convert.ToInt32(SelectedData.Rows[i][FunctionalityImageDataModel.DataColumns.FunctionalityImageId].ToString());
				var dt = FunctionalityImageDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

				if (dt.Count == 1)
				{
					UpdatedData.Add(dt[0]);
				}

			}
			return UpdatedData.ToDataTable();
		}		

		protected override DataTable GetEntityData(int? entityKey)
		{
			var functionalityImagedata = new FunctionalityImageDataModel();
			functionalityImagedata.FunctionalityImageId = entityKey;
			var results = FunctionalityImageDataManager.GetEntityDetails(functionalityImagedata, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);
			return results.ToDataTable();
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FunctionalityImage;
			PrimaryEntityKey = "FunctionalityImage";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion	
    }
}