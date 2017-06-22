using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.UI.Web;

namespace Shared.UI.Web.Controls
{
	public partial class eList : Shared.UI.WebFramework.BaseControl
	{
		private System.Delegate _UpdateData;
		private string[] _Columns;
		private DataTable _Data;

		public string[] Columns
		{
			get { return (string[])Session["Columns"]; }
			set {
				if (Session["Columns"] == null)
					Session.Add("Columns", value);
				else
					Session["Columns"] = value;
				
			}
		}

		public DataTable Data
		{
			get { return (DataTable)Session["DataObject"]; }
			set
			{
				if (Session["DataObject"] == null)
					Session.Add("DataObject", value);
				else
					Session["DataObject"] = value;

			}
		}

		public Delegate DelUpdateRef
		{
			get { return _UpdateData;  }
			set { _UpdateData = value; }
		}
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		public void AddColumns(string[] columns)
		{
			foreach (string col in columns)
			{
				TemplateField bfield = new TemplateField();

				//Initalize the DataField value.
				bfield.HeaderTemplate = new GridViewTemplate(ListItemType.Header, col);

				//Initialize the HeaderText field value.
				bfield.ItemTemplate = new GridViewTemplate(ListItemType.Item, col);

				//Initialize the edititemtemplate field value.
				bfield.EditItemTemplate = new GridViewTemplate(ListItemType.EditItem, col);

				//Add the newly created bound field to the GridView.
				EditableGridView.Columns.Add(bfield);
			}
		}

		public void SetUp(string[] columns, string tableName, DataTable data)
		{
			Data = data;
			Columns = columns;
			this.Columns = columns;
			var ItemTmpField = new TemplateField();

			EditableGridView.DataSource = data;
			EditableGridView.DataBind();

			if(Session["DataObject"] == null)
				Session.Add("DataObject", data);
			else
				Session["DataObject"] = data;


		}

		private void BindData()
		{
			DataTable data = (DataTable) Session["DataObject"];
			EditableGridView.DataSource = data;
			EditableGridView.DataBind();
		}

		public void BindData(DataTable data)
		{
			if (Session["DataObject"] == null)
				Session.Add("DataObject", data);
			else
				Session["DataObject"] = data;
			EditableGridView.DataSource = data;
			EditableGridView.DataBind();
		}

		protected void EditableGridView_RowDataBound(object sender, GridViewRowEventArgs e)
		{
		}

		protected void EditableGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
		{
			//ArrayList values = new ArrayList();
			var values = new Dictionary<string, string>();
			for (int i = 0; i < Columns.Length; i++)
			{
				var txt = (TextBox) EditableGridView.Rows[e.RowIndex].Cells[i].FindControl(Columns[i]);
				if(txt != null)
					values.Add(Columns[i], txt.Text);
			}
			
		
			DelUpdateRef.DynamicInvoke(values);
			EditableGridView.EditIndex = -1;
			BindData();

		}
		protected void EditableGridView_RowEditing(object sender, GridViewEditEventArgs e)
		{
			EditableGridView.EditIndex = e.NewEditIndex;
			SetUp(Columns, "Client", Data);
			BindData();
		}
		protected void EditableGridView_RowCancelingEdit(object sender,
								  GridViewCancelEditEventArgs e)
		{
			EditableGridView.EditIndex = -1;
			BindData();
		}
	}
}