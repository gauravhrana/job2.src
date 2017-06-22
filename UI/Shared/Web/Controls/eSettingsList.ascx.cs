using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Controls
{
	public partial class eSettingsList : Shared.UI.WebFramework.BaseControl
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
					return string.Empty;
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
					return string.Empty;
			}
			set { ViewState["EntityFolder"] = value; }
		}
		private int AEFLMode
		{

			get
			{
				if (ViewState["AEFLMode"] != null)
					return int.Parse(ViewState["AEFLMode"].ToString());
				else
					return 3;
			}
			set { ViewState["AEFLMode"] = value; }
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				BindData();
				EditableGridView.Visible = true;
				ReadOnlyGridView.Visible = false;
				btnEdit.Visible = false;
				btnUpdate.Visible = true;
			}

		}
		public void SetUp(int systemEntityTypeId, string entityName)
		{
			this.SystemEntityTypeId = systemEntityTypeId;
			this.EntityName = entityName;
		}
		public void SetUp(int systemEntityTypeId, string entityName, string entityFolder, int AEFLModeId)
		{
			this.SystemEntityTypeId = systemEntityTypeId;
			this.EntityName = entityName;
			this.EntityFolder = entityFolder;
			this.AEFLMode = AEFLModeId;
			BindData();
		}
		public static DataTable GetGridViewColumns(int systemEntityTypeId, int auditId)
		{
			var obj = new Framework.Components.UserPreference.ApplicationEntityFieldLabel.Data();
			var odt = new DataTable();
			if (SessionVariables.AEFLTable == null)
			{
				obj.SystemEntityTypeId = systemEntityTypeId;
				odt = Framework.Components.UserPreference.ApplicationEntityFieldLabel.Search(obj, auditId);
				SessionVariables.AEFLTable = odt;
			}
			else
			{
				var dtInSession = SessionVariables.AEFLTable;
				odt = dtInSession.Clone();
				var datarows = dtInSession.Select("SystemEntityTypeId = " + systemEntityTypeId);
				if (datarows.Length > 0)
				{
					foreach (DataRow dr in datarows)
					{
						odt.ImportRow(dr);
					}
				}
				else
				{
					obj.SystemEntityTypeId = systemEntityTypeId;
					odt = Framework.Components.UserPreference.ApplicationEntityFieldLabel.Search(obj, auditId);
					dtInSession.Merge(odt);
					SessionVariables.AEFLTable = dtInSession;
				}
			}
			
			return odt;
		}

		private void BindData()
		{
			var obj = new Framework.Components.UserPreference.ApplicationEntityFieldLabel.Data();
			var odt = new DataTable();
			obj.SystemEntityTypeId = SystemEntityTypeId;
			obj.ApplicationEntityFieldLabelModeId = AEFLMode;
			odt = Framework.Components.UserPreference.ApplicationEntityFieldLabel.Search(obj, AuditId);
			EditableGridView.DataSource = odt;
			EditableGridView.DataBind();
			ReadOnlyGridView.DataSource = odt;
			ReadOnlyGridView.DataBind();
			if(!SessionVariables.IsTesting)
			{
				EditableGridView.Columns[0].Visible = false;
				ReadOnlyGridView.Columns[0].Visible = false;
			}
		}

		protected void EditableGridView_RowDataBound(object sender, GridViewRowEventArgs e)
		{
		}

		protected void EditableGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
		{
			//var data = new Framework.Components.UserPreference.ApplicationEntityFieldLabel.Data();


			//var lblApplicationEntityFieldLabelId = (Label)EditableGridView.Rows[e.RowIndex].FindControl("lblApplicationEntityFieldLabelId");
			//var txtName = (TextBox)EditableGridView.Rows[e.RowIndex].FindControl("txtName");
			//var txtValue = (TextBox)EditableGridView.Rows[e.RowIndex].FindControl("txtValue");
			//var txtWidth = (TextBox)EditableGridView.Rows[e.RowIndex].FindControl("txtWidth");
			//var txtHorizontalAlignment = (TextBox)EditableGridView.Rows[e.RowIndex].FindControl("txtHorizontalAlignment");
			//var txtFormatting = (TextBox)EditableGridView.Rows[e.RowIndex].FindControl("txtFormatting");
			//var txtControlType = (TextBox)EditableGridView.Rows[e.RowIndex].FindControl("txtControlType");
			//var txtGridViewPriority = (TextBox)EditableGridView.Rows[e.RowIndex].FindControl("txtGridViewPriority");
			//var txtDetailsViewPriority = (TextBox)EditableGridView.Rows[e.RowIndex].FindControl("txtDetailsViewPriority");

			//data.ApplicationEntityFieldLabelId = int.Parse(lblApplicationEntityFieldLabelId.Text);
			//data.Name = txtName.Text;
			//data.Value = txtValue.Text;
			//data.SystemEntityTypeId = SystemEntityTypeId;
			//data.Width = decimal.Parse(txtWidth.Text);
			//data.Formatting = txtFormatting.Text;
			//data.ControlType = txtControlType.Text;
			//data.HorizontalAlignment = txtHorizontalAlignment.Text;
			//data.GridViewPriority = int.Parse(txtGridViewPriority.Text);
			//data.DetailsViewPriority = int.Parse(txtDetailsViewPriority.Text);
			
			//Framework.Components.UserPreference.ApplicationEntityFieldLabel.Update(data, AuditId);
			//UpdateGridTableInCache(SystemEntityTypeId, AuditId);
			//EditableGridView.EditIndex = -1;
			//BindData();

		}
		private static void UpdateGridTableInCache(int systemEntityTypeId, int auditId)
		{
			var obj = new Framework.Components.UserPreference.ApplicationEntityFieldLabel.Data();
			var data = new DataTable();
			obj.SystemEntityTypeId = systemEntityTypeId;
			data = Framework.Components.UserPreference.ApplicationEntityFieldLabel.GetGridViewColumns(obj, auditId);
			var dtInSession = (DataTable)SessionVariables.GridColumnsTable;
			if (dtInSession != null)
			{
				var odt = dtInSession.Clone();
				var datarows = dtInSession.Select("SystemEntityTypeId = " + systemEntityTypeId);
				if (datarows.Length > 0)
				{
					foreach (DataRow dr in datarows)
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
			BindData();
			EditableGridView.Visible = true;
			ReadOnlyGridView.Visible = false;
			btnUpdate.Visible = true;
			btnEdit.Visible = false;
		}

		protected void btnUpdate_Click(object sender, EventArgs e)
		{
			foreach (GridViewRow row in EditableGridView.Rows)
			{
				if (row.RowType == DataControlRowType.DataRow)
				{
					var data = new Framework.Components.UserPreference.ApplicationEntityFieldLabel.Data();

					var lblApplicationEntityFieldLabelId = (Label) row.FindControl("lblApplicationEntityFieldLabelId");
					var txtName = (TextBox) row.FindControl("txtName");
					var txtValue = (TextBox) row.FindControl("txtValue");
					var txtWidth = (TextBox) row.FindControl("txtWidth");
					var txtHorizontalAlignment = (TextBox) row.FindControl("txtHorizontalAlignment");
					var txtFormatting = (TextBox) row.FindControl("txtFormatting");
					var txtControlType = (TextBox) row.FindControl("txtControlType");
					var txtGridViewPriority = (TextBox) row.FindControl("txtGridViewPriority");
					var txtDetailsViewPriority = (TextBox) row.FindControl("txtDetailsViewPriority");

					try
					{
						data.ApplicationEntityFieldLabelId = int.Parse(lblApplicationEntityFieldLabelId.Text);

						data.Name = txtName.Text;
						data.Value = txtValue.Text;
						data.Width = decimal.Parse(txtWidth.Text);
						data.SystemEntityTypeId = SystemEntityTypeId;
						data.Formatting = txtFormatting.Text;
						data.ControlType = txtControlType.Text;
						data.HorizontalAlignment = txtHorizontalAlignment.Text;
						data.GridViewPriority = int.Parse(txtGridViewPriority.Text);
						data.DetailsViewPriority = int.Parse(txtDetailsViewPriority.Text);

						Framework.Components.UserPreference.ApplicationEntityFieldLabel.Update(data, AuditId);
					}
					catch (Exception)
					{

						continue;
						;
					}
						
				}

			}
			UpdateGridTableInCache(SystemEntityTypeId, AuditId);
			BindData();
			EditableGridView.Visible = false;
			ReadOnlyGridView.Visible = true;
			btnEdit.Visible = true;
			btnUpdate.Visible = false;

			//if(!string.IsNullOrEmpty(EntityFolder))
			//    Response.Redirect("~/" + EntityFolder + "/" + EntityName + "/Default.aspx?Added=" + true, false);
			//else
			//    Response.Redirect("~/" + EntityName + "/Default.aspx?Added=" + true, false);
		}
	}
}