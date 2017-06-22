using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using Framework.Components.DataAccess;
using Framework.Components.UserPreference;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Controls
{
	public partial class DynamicUpdate : BaseControl
	{
		public Repeater DynamicUpdateRepeater
		{
			get { return repUpdate; }
		}

		public string BorderClass
		{
			set
			{
				borderdiv.Attributes["class"] = value;
			}
		}

		private string EntityName
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


		public DataTable Columns { get; set; }	

		public string[] GridColumns
		{
			get { return (string[])Session["CUColumns"]; }
			set
			{
				if (Session["CUColumns"] == null)
					Session.Add("CUColumns", value);
				else
					Session["CUColumns"] = value;
			}
		}

		public DataTable Data
		{
			get { return (DataTable)Session["CUDataObject"]; }
			set
			{
				if (Session["CUDataObject"] == null)
					Session.Add("CUDataObject", value);
				else
					Session["CUDataObject"] = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				var data = new FieldConfigurationDataModel();
				var modedt = FieldConfigurationModeDataManager.GetList(SessionVariables.RequestProfile);
				var rows = modedt.Select("Name = 'CommonEditable'");
				
				if(rows.Length == 1)
				{
					data.FieldConfigurationModeId =
						int.Parse(rows[0][FieldConfigurationModeDataModel.DataColumns.FieldConfigurationModeId].ToString());
				}

				data.SystemEntityTypeId = (int)Enum.Parse(typeof(SystemEntity), EntityName);

				Columns = FieldConfigurationDataManager.Search(data, SessionVariables.RequestProfile);
				repUpdate.DataSource = Columns;
				repUpdate.DataBind();
			}
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

				//Initialize the HeaderText field value.
				bfield.ItemTemplate = new GridViewTemplate(ListItemType.Item, col);

				//Initialize the edititemtemplate field value.
				bfield.EditItemTemplate = new GridViewTemplate(ListItemType.EditItem, col);

				//bfield.FooterTemplate = new GridViewTemplate(ListItemType.Footer, col();

				//Add the newly created bound field to the GridView.
				MainGridView.Columns.Add(bfield);
			}
		}

		public void SetUp(string[] columns, string tableName, DataTable data)
		{
			Data = data;
			GridColumns = columns;
			EntityName = tableName;

			MainGridView.DataSource = data;
			MainGridView.DataBind();

			if (Session["CUDataObject"] == null)
				Session.Add("CUDataObject", data);
			else
				Session["CUDataObject"] = data;

		}
	}
}