using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker;
using Framework.Components.Core;
using Framework.Components.DataAccess;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.Client.Controls
{
	public partial class SelectionUpdate : BaseControl
	{
		public bool IsNameSelected
		{
			get { return chkName.Checked;  }
		}
		
		public bool IsDescriptionSelected
		{
			get { return chkDescription.Checked; }
		}
		
		public bool IsSortOrderSelected
		{
			get { return chkSortOrder.Checked; }
		}
		
		public string Name
		{
			get { return txtName.Text; }
		}
		
		public string Description
		{
			get { return txtDescription.Value; }
		}
		
		public  int SortOrder
		{
			get { return int.Parse(txtSortOrder.Text);  }
		}

		private DataTable _selectedData;
		public DataTable SelectedData
		{
			get { return _selectedData; }
			set { _selectedData = value; }
		}

		private DataTable GetData()
		{
			return SelectedData;
		}

		protected void btnUpdate_Click(object sender, EventArgs e)
		{
			var updatedData = new DataTable();
			var data = new ClientDataModel();
            
			updatedData = ClientDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{

				data.ClientId = Convert.ToInt32(SelectedData.Rows[i][ClientDataModel.DataColumns.ClientId].ToString());
				if (IsNameSelected)
					data.Name = Name;
				else
					data.Name = SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();

				if (IsDescriptionSelected)
					data.Description = Description;
				else
					data.Description = SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

				if(IsSortOrderSelected)
					data.SortOrder = SortOrder;
				else
					data.SortOrder = Convert.ToInt32(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());


				ClientDataManager.Update(data, SessionVariables.RequestProfile);

				data = new ClientDataModel();
				data.ClientId = Convert.ToInt32(SelectedData.Rows[i][ClientDataModel.DataColumns.ClientId].ToString());
                var dt = ClientDataManager.Search(data, SessionVariables.RequestProfile);
				
				if(dt.Rows.Count == 1)
				{
					updatedData.ImportRow(dt.Rows[0]);
				}
			
				// if everything is done and good THEN move from thsi page.
				//Response.Redirect("Default.aspx?Added=" + true, false);
				
			}

			MainGridView.DataSource = updatedData;
			MainGridView.DataBind();
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			Response.Redirect("Default.aspx?Added=" + true, false);        
		}

		protected override void OnInit(EventArgs e)
		{

			try
			{
				var superKey = "";
				var setId = 0;
				var key = 0;
				var clientdata = new ClientDataModel();
                var results = ClientDataManager.Search(clientdata, SessionVariables.RequestProfile).Clone();

				if (Request.QueryString["SuperKey"] != null)
				{
					superKey = Request.QueryString["SuperKey"];
				}
				else if (Request.QueryString["SetId"] != null)
				{
					setId = int.Parse(Request.QueryString["SetId"]);
				}
				if (int.Parse(superKey) > 0)
				{
					var data = new SuperKeyDetailDataModel();
					data.SuperKeyId = Convert.ToInt32(superKey);

					// Change System Entity Type
					data.SystemEntityTypeId = (int)SystemEntity.Client;
					var dt = SuperKeyDetailDataManager.Search(data, SessionVariables.RequestProfile);
					if (dt != null && dt.Rows.Count > 0)
					{
						foreach (DataRow dr in dt.Rows)
						{
							key = (int)(dr[SuperKeyDetailDataModel.DataColumns.EntityKey]);
							clientdata.ClientId = key;
                            var clientdt = ClientDataManager.Search(clientdata, SessionVariables.RequestProfile);
							if (clientdt.Rows.Count == 1)
							{
								results.ImportRow(clientdt.Rows[0]);
							}
						}
					}
				}
				else
				{
					key = setId;
					clientdata.ClientId = key;
                    var clientdt = ClientDataManager.Search(clientdata, SessionVariables.RequestProfile);
					if (clientdt.Rows.Count > 1)
					{
						results.ImportRow(clientdt.Rows[0]);
					}
				}
				SelectedData = new DataTable();
				SelectedData = results.Copy();
				base.OnInit(e);
			}
			catch (Exception ex)
			{

				Debug.WriteLine(ex.Message);
				//throw
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			MainGridView.DataSource = SelectedData;
			MainGridView.DataBind();			
		}
	}
}