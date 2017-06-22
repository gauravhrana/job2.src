using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker;
using Framework.Components.Core;
using Framework.Components.DataAccess;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.Client
{
	public partial class RenumberData : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected override void OnInit(EventArgs e)
		{
			try
			{
				var superKey = string.Empty;
				var newId = SystemEntityTypeDataManager.GetNextSequence("Client",(int) SystemEntity.Client, SessionVariables.RequestProfile);
				var clientData = new ClientDataModel();
				var systemDevData = new SystemDevNumbersDataModel();

				systemDevData.ApplicationUserId = SessionVariables.RequestProfile.AuditId;
				var dtnumbers = SystemDevNumbersDataManager.Search(systemDevData, SessionVariables.RequestProfile);

				var rangefrom = Convert.ToInt32(dtnumbers.Rows[0][SystemDevNumbersDataModel.DataColumns.RangeFrom].ToString());
				var rangeto = Convert.ToInt32(dtnumbers.Rows[0][SystemDevNumbersDataModel.DataColumns.RangeTo].ToString());

				if (Request.QueryString["SuperKey"] != null)
				{
					superKey = Request.QueryString["SuperKey"];
					var data = new SuperKeyDetailDataModel();

					data.SuperKeyId = Convert.ToInt32(superKey);
					data.SystemEntityTypeId = (int)SystemEntity.Client;
					var dt = SuperKeyDetailDataManager.Search(data, SessionVariables.RequestProfile);
					
					if (dt != null && dt.Rows.Count > 0)
					{
						foreach (DataRow dr in dt.Rows)
						{
							var key = (int)(dr[SuperKeyDetailDataModel.DataColumns.EntityKey]);
							clientData.ClientId = key;

							var clientdt = ClientDataManager.GetDetails(clientData, SessionVariables.RequestProfile);

							if (clientdt.Rows.Count == 1)
							{
								var row = clientdt.Rows[0];

								if (Request.QueryString["Mode"].Equals("Test"))
								{
									clientData.ClientId = GetNextValidId(rangefrom);
								}
								clientData.Name = Convert.ToString(row[StandardDataModel.StandardDataColumns.Name]);
								clientData.Description = Convert.ToString(row[StandardDataModel.StandardDataColumns.Description]);
								clientData.SortOrder = (int)(row[StandardDataModel.StandardDataColumns.SortOrder]);

								ClientDataManager.Create(clientData, SessionVariables.RequestProfile);


							}
						}
					}
				}
				else if (Request.QueryString["SetId"] != null)
				{
					var key = int.Parse(Request.QueryString["SetId"]);
					clientData.ClientId = key;

					var clientdt = ClientDataManager.GetDetails(clientData, SessionVariables.RequestProfile);

					if (clientdt.Rows.Count == 1)
					{
						var row = clientdt.Rows[0];

						var newclientdata = new ClientDataModel();
						
						if (Request.QueryString["Mode"].Equals("Test"))
							newclientdata.ClientId = newId = GetNextValidId(rangefrom);
						
						newclientdata.Name = Convert.ToString(row[StandardDataModel.StandardDataColumns.Name]);
						newclientdata.Description = Convert.ToString(row[StandardDataModel.StandardDataColumns.Description]);
						newclientdata.SortOrder = (int)(row[StandardDataModel.StandardDataColumns.SortOrder]);
						
						UpdatedFKDepenedencies(key, newId);

						ClientDataManager.Delete(clientData, SessionVariables.RequestProfile);
						ClientDataManager.Create(newclientdata, SessionVariables.RequestProfile);
					}
					

				}
				else if (Request.QueryString["Mode"].Equals("Renumber"))
				{
					var seed = int.Parse(Request.QueryString["Seed"]);
						//ApplicationVariables.Seed;
					var increment = int.Parse(Request.QueryString["Increment"]);
						//ApplicationVariables.Increment;
					ClientDataManager.Renumber(seed, increment, SessionVariables.RequestProfile);
				}
					base.OnInit(e);

                Response.Redirect(Page.GetRouteUrl("ClientEntityRoute", new { Action = "Default", SetId = true }), false);
			}
			catch (Exception ex)
			{
				Response.Write(ex.Message);
			}
		}

		private int GetNextValidId(int tempId)
		{
            var list = ClientDataManager.GetEntityDetails(ClientDataModel.Empty, SessionVariables.RequestProfile, 0);
			
			foreach(var item in list)
			{
				if (item.ClientId == tempId)
				{
					tempId -= 1;
					return GetNextValidId(tempId);
				}
			}
			
			return tempId;
		}

		private void UpdatedFKDepenedencies(int oldId, int newId)
		{
			try
			{
				var clientxprojectdt = ClientXProjectDataManager.GetByClient(oldId, SessionVariables.RequestProfile.AuditId);
				var projectIds = new int[clientxprojectdt.Rows.Count];
				for (var i = 0; i < clientxprojectdt.Rows.Count; i++)
				{
					projectIds[i] = int.Parse(clientxprojectdt.Rows[i][ClientXProjectDataModel.DataColumns.ProjectId].ToString());
				}
				if (projectIds.Length > 0)
				{
					ClientXProjectDataManager.DeleteByClient(oldId, SessionVariables.RequestProfile.AuditId);
                    ClientXProjectDataManager.CreateByClient(newId, projectIds, SessionVariables.RequestProfile);
					
				}
			}
			catch (Exception ex)
			{
				
				Response.Write(ex.Message);
			}
		}
	}
}