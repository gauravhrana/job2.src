using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.EntityXWorkTicket
{
	public partial class Details : Framework.UI.Web.BaseClasses.PageDetails
	{
		#region Methods

		protected override Control GetTabControl(int setId, Control detailsControl)
		{
			var tabControl = ApplicationCommon.GetNewDetailTabControl();

			tabControl.AddTab("Entity", detailsControl, String.Empty, true);

			var listControl = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);

			tabControl.AddTab("WorkTicket", listControl);

			listControl.Setup("WorkTicket", String.Empty, "WorkTicketId", setId, true, GetData, GetWorkTicketColumns, "Entity");
			listControl.SetSession("true");

			tabControl.Setup("EntityDetailsView");

			return tabControl;
		}


		private DataTable GetData(string key)
		{
			return GetWorkTicketData(int.Parse(key));
		}

		private Shared.UI.Web.Controls.DetailTab1Control GetVerticalTabControl(int setId, Control detailsControl)
		{
			var tabControl = ApplicationCommon.GetNewDetail1TabControl(); ;

			tabControl.AddTab("Entity", detailsControl, "Entity", false);

			var listControl = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);

			tabControl.AddTab("WorkTicket", listControl, "WorkTicket");

			listControl.Setup("WorkTicket", String.Empty, "WorkTicketId", setId, true, GetData, GetWorkTicketColumns, "WorkTicket");
			listControl.SetSession("true");

			//tabControl.AddTab("WorkTicket", "", 2, "WorkTicketId", setId, true, GetData, GetWorkTicketColumns);
			//tabControl.AddLastTab();
			return tabControl;
		}

		private DataTable GetWorkTicketData(int entityId)
		{
			var dt = TaskTimeTracker.Components.Module.ApplicationDevelopment.EntityXWorkTicketDataManager.GetByEntity(entityId, SessionVariables.RequestProfile);
			var WorkTicketdt = TaskTimeTracker.Components.Module.ApplicationDevelopment.WorkTicketDataManager.Search(
                WorkTicketDataModel.Empty,
                SessionVariables.RequestProfile);
			var resultdt = WorkTicketdt.Clone();

			foreach (DataRow row in dt.Rows)
			{
				var rows = WorkTicketdt.Select("WorkTicketId = " + row[WorkTicketDataModel.DataColumns.WorkTicketId]);
				resultdt.ImportRow(rows[0]);
			}

			return resultdt;
		}

		private string[] GetWorkTicketColumns()
		{
			return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.WorkTicket, "DBColumns", SessionVariables.RequestProfile);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.EntityOwner;
			PrimaryEntityKey = "EntityOwner";
			DetailsControlPath = ApplicationCommon.GetControlPath("EntityOwner", ControlType.DetailsControl);
			PrimaryPlaceHolder = oDetailsControl.PlaceHolderDetails;
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion

	}
}