using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using Framework.Components.UserPreference;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;
using Shared.UI.Web.Controls;
using Framework.Components.DataAccess;

namespace Shared.UI.Web.Controls
{
	public partial class eSettingsListControl : BaseControl
	{
		
		private int SystemEntityTypeId
		{

			get
			{
				if (ViewState["SystemEntityTypeId"] != null)
					return int.Parse(ViewState["SystemEntityTypeId"].ToString());
				else
					return 0;
				
			}
			set { ViewState["SystemEntityTypeId"] = value; }
		}

		private string  EntityName
		{

			get
			{
				if (ViewState["EntityName"] != null)
					return ViewState["EntityName"].ToString();
				else
					return String.Empty;
			}
			set { ViewState["EntityName"] = value; }
		}

		private string EntityFolder
		{

			get
			{
				if (ViewState["EntityFolder"] != null)
					return ViewState["EntityFolder"].ToString();
				else
					return String.Empty;
			}
			set { ViewState["EntityFolder"] = value; }
		}

		public void EditableGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			var selectedrow = e.RowIndex.ToString();
			var lbl = (Label) EditableGridView.Rows[e.RowIndex].Cells[2].FindControl("lblFieldConfigurationId");
			var data = new FieldConfigurationDataModel();
			data.FieldConfigurationId = int.Parse(lbl.Text);
			FieldConfigurationDataManager.Delete(data, SessionVariables.RequestProfile);
            BindData(ApplicationId);
		}

		private int ApplicationId
		{

			get
			{
				if (ViewState["ApplicationId"] != null)
					return int.Parse(ViewState["ApplicationId"].ToString());
				else
					return 100;
			}
			set { ViewState["ApplicationId"] = value; }
		}
		private int FCMode
		{

			get
			{
				if (ViewState["FCMode"] != null)
					return int.Parse(ViewState["FCMode"].ToString());
				else
					return 3;
			}
			set { ViewState["FCMode"] = value; }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				if (rbtnList.SelectedValue.Equals("GridView"))
				{
					EditableGridView.Visible = true;
					ReadOnlyGridView.Visible = false;
					EditableRepeater.Visible = false;
					ReadOnlyRepeater.Visible = false;
				}
				else if (rbtnList.SelectedValue.Equals("Repeater"))
				{
					EditableRepeater.Visible = true;
					ReadOnlyRepeater.Visible = false;
					EditableGridView.Visible = false;
					ReadOnlyGridView.Visible = false;
				}
				btnEdit.Visible = false;
				btnUpdate.Visible = true;
			}
		}

		public void SetUp(int systemEntityTypeId, string entityName)
		{
			SystemEntityTypeId = systemEntityTypeId;
			EntityName = entityName;
		}

		public void SetUp(int systemEntityTypeId, string entityName, string entityFolder, int FCModeId, int applicationId)
		{
			SystemEntityTypeId = systemEntityTypeId;
			EntityName = entityName;
			EntityFolder = entityFolder;
			FCMode = FCModeId;
			ApplicationId = applicationId;
			BindData(applicationId);
		}

		public static DataTable GetGridViewColumns(int systemEntityTypeId, RequestProfile requestProfile)
		{
			var obj = new FieldConfigurationDataModel();
			var odt = new DataTable();
			if (SessionVariables.AEFLTable == null)
			{
				obj.SystemEntityTypeId = systemEntityTypeId;
				odt = FieldConfigurationDataManager.Search(obj, SessionVariables.RequestProfile);
				SessionVariables.AEFLTable = odt;
			}
			else
			{
				var dtInSession = SessionVariables.AEFLTable;
				odt = dtInSession.Clone();
				var datarows = dtInSession.Select("SystemEntityTypeId = " + systemEntityTypeId);
				if (datarows.Length > 0)
				{
					foreach (var dr in datarows)
					{
						odt.ImportRow(dr);
					}
				}
				else
				{
					obj.SystemEntityTypeId = systemEntityTypeId;
					odt = FieldConfigurationDataManager.Search(obj, SessionVariables.RequestProfile);
					dtInSession.Merge(odt);
					SessionVariables.AEFLTable = dtInSession;
				}
			}
			
			return odt;
		}

		private void BindData(int applicationId)
		{
            var dtc = FieldConfigurationUtility.GetFieldConfigurations(19000, -15001, string.Empty);

			var odt = FieldConfigurationUtility.GetFieldConfigurations(SystemEntityTypeId, FCMode, string.Empty);

            foreach (DataColumn col in odt.Columns)
            {
                var colexists = false;
                foreach (DataColumn dcol in dtc.Columns)
                {
                    if (dcol.ColumnName == col.ColumnName)
                    {
                        colexists = true;
                        continue;
                    }
                }
                if (!colexists)
                    odt.Columns.Remove(col.ColumnName);
            }
            foreach (DataColumn col in dtc.Columns)
            {
                //Declare the bound field and allocate memory for the bound field.
                var bfield = new TemplateField();

                ////Initalize the DataField value.
                //bfield.HeaderTemplate = new eSettingsTemplate(ListItemType.Header, col.ColumnName);

                ////Initialize the HeaderText field value.
                //bfield.ItemTemplate = new eSettingsTemplate(ListItemType.Item, col.ColumnName);

                //Add the newly created bound field to the GridView.
                EditableGridView.Columns.Add(bfield);
            }
            EditableGridView.DataSource = odt;
            EditableGridView.DataBind();
			ReadOnlyGridView.DataSource = odt;
			ReadOnlyGridView.DataBind();
            ReadOnlyRepeater.DataSource = odt;
            ReadOnlyRepeater.DataBind();
            EditableRepeater.DataSource = odt;
            EditableRepeater.DataBind();
            Repeater2.DataSource = odt;
            Repeater2.DataBind();
            Repeater1.DataSource = odt;
            Repeater1.DataBind();
			if(!SessionVariables.IsTesting)
			{
                //EditableGridView.Columns[0].Visible = false;
                //ReadOnlyGridView.Columns[0].Visible = false;
			}
		}

        protected void ItemBound(object sender, RepeaterItemEventArgs args)
        {
            var dtc = FieldConfigurationUtility.GetFieldConfigurations(19000, -15001, string.Empty);

            var finalodt = new DataTable();
            
                finalodt.Columns.Add(new DataColumn("Name"));
                finalodt.Columns.Add(new DataColumn("Value"));
               
            if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
            {
                var childRepeater = (Repeater)args.Item.FindControl("InnerRepeater");
                var hdnfield = (HiddenField)args.Item.FindControl("hdncol");
                if (hdnfield != null && childRepeater != null)
                {
					var odt = FieldConfigurationUtility.GetFieldConfigurations(SystemEntityTypeId, FCMode, hdnfield.Value);

                    foreach (DataColumn col in odt.Columns)
                    {
                        var colexists = false;
                        foreach (DataColumn dcol in dtc.Columns)
                        {
                            if (dcol.ColumnName == col.ColumnName)
                            {
                                colexists = true;
                                continue;
                            }
                        }
                        if (!colexists)
                            odt.Columns.Remove(col.ColumnName);
                    }
                    
                    if (odt.Rows.Count >= 1)
                    {
                        
                        foreach (DataColumn dcol in dtc.Columns)
                        {
                            var row = finalodt.NewRow();
                            row["Name"] = dcol.ColumnName;
                            if(odt.Columns.Contains(dcol.ColumnName))
                                row["Value"] = odt.Rows[0][dcol.ColumnName];
                            finalodt.Rows.Add(row);
                        }
                    }                    
                }

                childRepeater.DataSource = finalodt;
                childRepeater.DataBind();
            }
        }

		protected void EditableGridView_RowDataBound(object sender, GridViewRowEventArgs e)
		{
		}

		protected void EditableGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
		{
			//var data = new FieldConfiguration.Data();


			//var lblFieldConfigurationId = (Label)EditableGridView.Rows[e.RowIndex].FindControl("lblFieldConfigurationId");
			//var txtName = (TextBox)EditableGridView.Rows[e.RowIndex].FindControl("txtName");
			//var txtValue = (TextBox)EditableGridView.Rows[e.RowIndex].FindControl("txtValue");
			//var txtWidth = (TextBox)EditableGridView.Rows[e.RowIndex].FindControl("txtWidth");
			//var txtHorizontalAlignment = (TextBox)EditableGridView.Rows[e.RowIndex].FindControl("txtHorizontalAlignment");
			//var txtFormatting = (TextBox)EditableGridView.Rows[e.RowIndex].FindControl("txtFormatting");
			//var txtControlType = (TextBox)EditableGridView.Rows[e.RowIndex].FindControl("txtControlType");
			//var txtGridViewPriority = (TextBox)EditableGridView.Rows[e.RowIndex].FindControl("txtGridViewPriority");
			//var txtDetailsViewPriority = (TextBox)EditableGridView.Rows[e.RowIndex].FindControl("txtDetailsViewPriority");

			//data.FieldConfigurationId = int.Parse(lblFieldConfigurationId.Text);
			//data.Name = txtName.Text;
			//data.Value = txtValue.Text;
			//data.SystemEntityTypeId = SystemEntityTypeId;
			//data.Width = decimal.Parse(txtWidth.Text);
			//data.Formatting = txtFormatting.Text;
			//data.ControlType = txtControlType.Text;
			//data.HorizontalAlignment = txtHorizontalAlignment.Text;
			//data.GridViewPriority = int.Parse(txtGridViewPriority.Text);
			//data.DetailsViewPriority = int.Parse(txtDetailsViewPriority.Text);
			
			//FieldConfiguration.Update(data, SessionVariables.RequestProfile);
			//UpdateGridTableInCache(SystemEntityTypeId, SessionVariables.RequestProfile);
			//EditableGridView.EditIndex = -1;
			//BindData();

		}

		private static void UpdateGridTableInCache(int systemEntityTypeId, int auditId)
		{
			var obj = new FieldConfigurationDataModel();
			var data = new DataTable();
			obj.SystemEntityTypeId = systemEntityTypeId;
			data = FieldConfigurationDataManager.Search(obj, SessionVariables.RequestProfile);
			var dtInSession = (DataTable)SessionVariables.GridColumnsTable;
			if (dtInSession != null)
			{
				var odt = dtInSession.Clone();
				var datarows = dtInSession.Select("SystemEntityTypeId = " + systemEntityTypeId);
				if (datarows.Length > 0)
				{
					foreach (var dr in datarows)
					{
						dtInSession.Rows.Remove(dr);
					}
				}

				dtInSession.Merge(data);
				SessionVariables.GridColumnsTable = dtInSession;
			}
			else
			{
				SessionVariables.GridColumnsTable = data;
			}			
		}

		protected void EditableGridView_RowEditing(object sender, GridViewEditEventArgs e)
		{
			//EditableGridView.EditIndex = e.NewEditIndex;
			//BindData();
		}

		protected void EditableGridView_RowCancelingEdit(object sender,GridViewCancelEditEventArgs e)
		{
			//EditableGridView.EditIndex = -1;
			//BindData();
		}

		protected void btnEdit_Click(object sender, EventArgs e)
		{
			BindData(ApplicationId);
			if (rbtnList.SelectedValue.Equals("GridView"))
			{
				EditableGridView.Visible = true;
				ReadOnlyGridView.Visible = false;
			}
			else
			{
				EditableRepeater.Visible = true;
				ReadOnlyRepeater.Visible = false;
			}
			btnUpdate.Visible = true;
			btnEdit.Visible = false;
		}

		private void UpdateData()
		{
			if (rbtnList.SelectedValue.Equals("GridView"))
			{
				foreach (GridViewRow row in EditableGridView.Rows)
				{
					if (row.RowType == DataControlRowType.DataRow)
					{
						var data = new FieldConfigurationDataModel();

						var lblFieldConfigurationId = (Label) row.FindControl("lblFieldConfigurationId");
						var txtName = (TextBox) row.FindControl("txtName");
						var txtValue = (TextBox) row.FindControl("txtValue");
						var txtWidth = (TextBox) row.FindControl("txtWidth");
						var txtHorizontalAlignment = (TextBox) row.FindControl("txtHorizontalAlignment");
						var txtFormatting = (TextBox) row.FindControl("txtFormatting");
						var txtControlType = (TextBox) row.FindControl("txtControlType");
						var txtGridViewPriority = (TextBox) row.FindControl("txtGridViewPriority");
						var txtDetailsViewPriority = (TextBox) row.FindControl("txtDetailsViewPriority");
						var txtAEFLModeId = (TextBox) row.FindControl("txtFieldConfigurationModeId");

						try
						{
							data.FieldConfigurationId = int.Parse(lblFieldConfigurationId.Text);

							data.Name = txtName.Text;
							data.Value = txtValue.Text;
							data.Width = decimal.Parse(txtWidth.Text);
							data.SystemEntityTypeId = SystemEntityTypeId;
							data.Formatting = txtFormatting.Text;
							data.ControlType = txtControlType.Text;
							data.HorizontalAlignment = txtHorizontalAlignment.Text;
							data.GridViewPriority = int.Parse(txtGridViewPriority.Text);
							data.DetailsViewPriority = int.Parse(txtDetailsViewPriority.Text);
							data.FieldConfigurationModeId = int.Parse(txtAEFLModeId.Text);

							FieldConfigurationDataManager.Update(data, SessionVariables.RequestProfile);
						}
						catch (Exception)
						{
							continue;							
						}
					}
				}

				EditableGridView.Visible = false;
				ReadOnlyGridView.Visible = true;
			}
			else
			{
				foreach (RepeaterItem item in EditableRepeater.Items)
				{
					
						var data = new FieldConfigurationDataModel();

						var hdnFieldConfigurationId = (HiddenField)item.FindControl("hdnFieldConfigurationId");
						var txtName = (TextBox)item.FindControl("txtName");
						var txtValue = (TextBox)item.FindControl("txtValue");
						var txtWidth = (TextBox)item.FindControl("txtWidth");
						var txtHorizontalAlignment = (TextBox)item.FindControl("txtHorizontalAlignment");
						var txtFormatting = (TextBox)item.FindControl("txtFormatting");
						var txtControlType = (TextBox)item.FindControl("txtControlType");
						var txtGridViewPriority = (TextBox)item.FindControl("txtGridViewPriority");
						var txtDetailsViewPriority = (TextBox)item.FindControl("txtDetailsViewPriority");
						var txtAEFLModeId = (TextBox)item.FindControl("txtAEFLModeId");

						try
						{
							data.FieldConfigurationId = int.Parse(hdnFieldConfigurationId.Value);

							data.Name = txtName.Text;
							data.Value = txtValue.Text;
							data.Width = decimal.Parse(txtWidth.Text);
							data.SystemEntityTypeId = SystemEntityTypeId;
							data.Formatting = txtFormatting.Text;
							data.ControlType = txtControlType.Text;
							data.HorizontalAlignment = txtHorizontalAlignment.Text;
							data.GridViewPriority = int.Parse(txtGridViewPriority.Text);
							data.DetailsViewPriority = int.Parse(txtDetailsViewPriority.Text);
							data.FieldConfigurationModeId = int.Parse(txtAEFLModeId.Text);

							FieldConfigurationDataManager.Update(data, SessionVariables.RequestProfile);
						}
						catch (Exception)
						{

							continue;
							;
						}					
				}

				EditableRepeater.Visible = false;
				ReadOnlyRepeater.Visible = true;
			}

			UpdateGridTableInCache(SystemEntityTypeId, SessionVariables.RequestProfile.AuditId);
			BindData(ApplicationId);
			
			btnEdit.Visible = true;
			btnUpdate.Visible = false;
		}

		protected void btnUpdateReturn_Click(object sender, EventArgs e)
		{
			UpdateData();
            var entityName = EntityName;
            Response.Redirect(Page.GetRouteUrl(entityName + "EntityRoute", new { Action = "Default" }), false);
		}

		protected void btnUpdate_Click(object sender, EventArgs e)
		{
			UpdateData();
		}

		protected void rbtnList_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (rbtnList.SelectedValue.Equals("GridView"))
			{
				EditableGridView.Visible = true;
				ReadOnlyGridView.Visible = false;
				EditableRepeater.Visible = false;
				ReadOnlyRepeater.Visible = false;
			}
			else if (rbtnList.SelectedValue.Equals("Repeater"))
			{
				EditableRepeater.Visible = true;
				ReadOnlyRepeater.Visible = false;
				EditableGridView.Visible = false;
				ReadOnlyGridView.Visible = false;
			}
			btnUpdate.Visible = true;
			btnEdit.Visible = false;
		}
	}
}