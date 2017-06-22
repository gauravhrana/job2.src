using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using TaskTimeTracker.Components.Module.ApplicationDevelopment;
using Framework.Components.Core;
using DataModel.Framework.Core;
using System.Data;
using System.Linq;

namespace Shared.UI.Web.ApplicationManagement.Development
{
	public partial class FESBulkEntry : Framework.UI.Web.BaseClasses.PageBasePage
	{
		#region properties

		public int? FunctionalityId
		{
			get
			{
				return int.Parse(ddlFunctionality.SelectedItem.Value);
			}			
		}

		public string Functionality
		{
			get
			{
				return ddlFunctionality.SelectedItem.Text;
			}
		}

		public int? ApplicationId
		{
			get
			{
				return int.Parse(ddlApplication.SelectedItem.Value);
			}			
		}

		#endregion properties

		protected void btnCreate_Click(object sender, EventArgs e)
		{
			var dataFES = new FunctionalityEntityStatusDataModel();

			dataFES.FunctionalityId = FunctionalityId;
			 
			var fesData = FunctionalityEntityStatusDataManager.Search(dataFES, SessionVariables.RequestProfile);

			if (fesData.Rows.Count == 0)
			{
				var dtMenu = MenuDataManager.GetList(SessionVariables.RequestProfile);				

				var entitydt = SystemEntityTypeDataManager.GetList(SessionVariables.RequestProfile);

				//get data based on Entity Name from Menu and SystemEntity table to get the Primary Developer value
 
				var assignedTo = from tblMenu in dtMenu.AsEnumerable()
							  join tblEntity in entitydt.AsEnumerable() on tblMenu.Field<string>(MenuDataModel.DataColumns.Name) equals tblEntity.Field<string>(SystemEntityTypeDataModel.DataColumns.EntityName)
							  select tblEntity;

				//get data from SystemEntity table which doesnt have matching entries in Menu table

				var assignedToAdmin =	from tblEntity in entitydt.AsEnumerable()
										where !assignedTo.Contains(tblEntity)
										select tblEntity;
				
				string assignedToValue = string.Empty;

				//data inserted to FES with AssignedTo value as PrimaryDeveloper value

				for (int i = 0; i < assignedTo.Count(); i++)
				{
					var primaryDeveloper = from tblMenu in dtMenu.AsEnumerable()
										   join tblEntity in entitydt.AsEnumerable() on tblMenu.Field<string>(MenuDataModel.DataColumns.Name) equals tblEntity.Field<string>(SystemEntityTypeDataModel.DataColumns.EntityName)
										   where tblMenu.Field<string>(MenuDataModel.DataColumns.Name) == assignedTo.CopyToDataTable().Rows[i][SystemEntityTypeDataModel.DataColumns.EntityName].ToString()
										   select tblMenu[MenuDataModel.DataColumns.PrimaryDeveloper];

					assignedToValue = primaryDeveloper.First().ToString();

					Insert(assignedTo.CopyToDataTable().Rows[i], assignedToValue);					
				}

				//data inserted to FES with AssignedTo value as "Admin"

				for (int j = 0; j < assignedToAdmin.Count(); j++)
				{
					assignedToValue = "Admin";

					Insert(assignedToAdmin.CopyToDataTable().Rows[j], assignedToValue);					
				}
			}
		}

		public void Insert(DataRow dtRow, string assignedTo)
		{
			var data = new FunctionalityEntityStatusDataModel();

			data.SystemEntityTypeId = Convert.ToInt32(dtRow[SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId]);
			data.ApplicationId = Convert.ToInt32(ddlApplication.SelectedValue);
			data.FunctionalityId = FunctionalityId;
			data.Memo = Functionality; ;
			data.AssignedTo = assignedTo;
			data.FunctionalityPriorityId = ApplicationCommon.FunctionalityPriority;
			data.FunctionalityStatusId = ApplicationCommon.FunctionalityStatus;
			data.StartDate = DateTime.Now;
			data.TargetDate = DateTime.Now.AddDays(7);

			 var dtFES = FunctionalityEntityStatusDataManager.DoesExist(data, SessionVariables.RequestProfile);

			 if (dtFES.Rows.Count == 0)
			 {
				 FunctionalityEntityStatusDataManager.Create(data, SessionVariables.RequestProfile);
			 }

		}

		protected void Page_Load(object sender, EventArgs e)
		{

			if (!IsPostBack)
			{
				SetupDropDown();			
			}
		}

		protected void ddlApplication_SelectedIndexChanged(object sender, EventArgs e)
		{
			ddlFunctionality.Items.Clear();
			var functionalityData = FunctionalityDataManager.GetList(SessionVariables.RequestProfile);

			var dataView = functionalityData.DefaultView;
			dataView.RowFilter = StandardDataModel.StandardDataColumns.ApplicationId + "=" + ddlApplication.SelectedValue;

			var dt = dataView.ToTable();
			UIHelper.LoadDropDown(dt, ddlFunctionality,
				StandardDataModel.StandardDataColumns.Name,
				FunctionalityDataModel.DataColumns.FunctionalityId);

			ddlFunctionality.Items.Insert(0, new ListItem("All", "-1"));		
		}

		public void SetupDropDown()
		{
			var appData = Framework.Components.ApplicationUser.ApplicationDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(appData, ddlApplication, DataModel.Framework.AuthenticationAndAuthorization.ApplicationDataModel.DataColumns.Name,
				DataModel.Framework.AuthenticationAndAuthorization.ApplicationDataModel.DataColumns.ApplicationId);			

			var functionalityData = FunctionalityDataManager.GetList(SessionVariables.RequestProfile);

			var dataView = functionalityData.DefaultView;
			dataView.RowFilter = StandardDataModel.StandardDataColumns.ApplicationId + "=" + ddlApplication.SelectedValue;
			   
			var dt = dataView.ToTable();
			UIHelper.LoadDropDown(dt, ddlFunctionality,
				StandardDataModel.StandardDataColumns.Name,
				FunctionalityDataModel.DataColumns.FunctionalityId);

			ddlFunctionality.Items.Insert(0, new ListItem("All", "-1"));

		}
	}
}