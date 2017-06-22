using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.UI.Web;
using Shared.UI.WebFramework;

namespace Shared.UI.Web.Controls
{
	public partial class eList : BaseControl
	{
		private Delegate _UpdateData;
		private string[] _Columns;
		private DataTable _Data;
        private string _TableName;

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

        public GridView EditableGridViewControl
        {
            get
            {
                return EditableGridView;
            }

        }

        public string TableName
        {
            get
            {
                if (ViewState["TableName"] != null)
                    return ViewState["TableName"].ToString();
                else
                    return String.Empty;
            }
            set {
                ViewState["TableName"] = value;
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
			var validcolumns = columns.Distinct().ToArray();
			foreach (var col in validcolumns)
			{
				var bfield = new TemplateField();

				//Initalize the DataField value.
				bfield.HeaderTemplate = new GridViewTemplate(ListItemType.Header, col);

				bfield.HeaderStyle.ForeColor = ColorTranslator.FromHtml("#F7F7F7");
				bfield.HeaderStyle.Font.Bold = true;
				bfield.HeaderStyle.BackColor = Color.Blue;
				bfield.ItemStyle.BackColor = ColorTranslator.FromHtml("#E7E7FF");
				bfield.ItemStyle.ForeColor = ColorTranslator.FromHtml("#4A3C8C");
                
				//Initialize the edititemtemplate field value.
				bfield.EditItemTemplate = new GridViewTemplate(ListItemType.EditItem, col);
                //Initialize the HeaderText field value.
                bfield.ItemTemplate = new GridViewTemplate(ListItemType.Item, col);

				//bfield.FooterTemplate = new GridViewTemplate(ListItemType.Footer, col();

				//Add the newly created bound field to the GridView.
				EditableGridView.Columns.Add(bfield);
			}
		}

		public void SetUp(string[] columns, string tableName, DataTable data)
		{
			Data = data;
			Columns = columns;
            TableName = tableName;
			Columns = columns;
			var ItemTmpField = new TemplateField();

            EditableGridView.EditIndex = -1;
			EditableGridView.DataSource = data;
			EditableGridView.DataBind();
			if(Session["DataObject"] == null)
				Session.Add("DataObject", data);
			else
				Session["DataObject"] = data;

		}

		private void BindData()
		{
			var data = (DataTable) Session["DataObject"];
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
			var validcolumns = Columns.Distinct().ToArray();
			for (var i = 0; i < validcolumns.Length; i++)
			{
				var txt = (TextBox) EditableGridView.Rows[e.RowIndex].Cells[i].FindControl(validcolumns[i]);
				if(txt != null)
					values.Add(validcolumns[i], txt.Text);
			}		
		
			DelUpdateRef.DynamicInvoke(values);
			EditableGridView.EditIndex = -1;
			BindData();

		}

		protected void EditableGridView_RowEditing(object sender, GridViewEditEventArgs e)
		{
            BindData();
			EditableGridView.EditIndex = e.NewEditIndex;
            EditableGridView.DataBind();
		}
		protected void EditableGridView_RowCancelingEdit(object sender,
								  GridViewCancelEditEventArgs e)
		{
			BindData();
            EditableGridView.EditIndex = -1;
            EditableGridView.DataBind();
		}
	}
}