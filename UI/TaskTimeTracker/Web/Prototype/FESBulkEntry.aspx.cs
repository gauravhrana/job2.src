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
				var dtMenu = MenuDataManager.Search(MenuDataModel.Empty, SessionVariables.RequestProfile);

                var entitydt = SystemEntityTypeDataManager.Search(SystemEntityTypeDataModel.Empty, SessionVariables.RequestProfile);

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
                var cnt = assignedTo.Count();

				for (int i = 0; i < cnt; i++)
				{
					var primaryDeveloper = from tblMenu in dtMenu.AsEnumerable()
										   join tblEntity in entitydt.AsEnumerable() on tblMenu.Field<string>(MenuDataModel.DataColumns.Name) equals tblEntity.Field<string>(SystemEntityTypeDataModel.DataColumns.EntityName)
										   where tblMenu.Field<string>(MenuDataModel.DataColumns.Name) == assignedTo.CopyToDataTable().Rows[i][SystemEntityTypeDataModel.DataColumns.EntityName].ToString()
										   select tblMenu[MenuDataModel.DataColumns.PrimaryDeveloper];

					assignedToValue = primaryDeveloper.First().ToString();

					Insert(assignedTo.CopyToDataTable().Rows[i], assignedToValue);					
				}

				//data inserted to FES with AssignedTo value as "Admin"

				for (int j = 0; j < cnt; j++)
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

			 if(!FunctionalityEntityStatusDataManager.DoesExist(data, SessionVariables.RequestProfile))
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

            functionalityData = functionalityData.Where(x => x.ApplicationId == int.Parse(ddlApplication.SelectedValue)).ToList();

            UIHelper.LoadDropDown(functionalityData, ddlFunctionality,
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

            functionalityData = functionalityData.Where(x => x.ApplicationId == int.Parse(ddlApplication.SelectedValue)).ToList();
            UIHelper.LoadDropDown(functionalityData, ddlFunctionality,
				StandardDataModel.StandardDataColumns.Name,
				FunctionalityDataModel.DataColumns.FunctionalityId);

			ddlFunctionality.Items.Insert(0, new ListItem("All", "-1"));

		}
	}
}