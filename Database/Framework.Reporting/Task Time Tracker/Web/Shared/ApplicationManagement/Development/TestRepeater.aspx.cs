using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Development
{
	public partial class TestRepeater : System.Web.UI.Page
	{
		int AuditId = 10;

		private string PrimaryEntity = "ScheduleDetail";

		private int CurrentGridRowCellCount
		{
			get
			{
				if (ViewState["CurrentGridRowCellCount"] != null)
					return (int)ViewState["CurrentGridRowCellCount"];
				else
					return 0;
			}
			set
			{
				ViewState["CurrentGridRowCellCount"] = value;
			}
		}

		private const int GridRowCellCount = 12;
		
		private void BindRepeater()
		{
			var systemEntityTypeId = 34700;

			var SearchColumns = FieldConfigurationUtility.GetFieldConfigurations(systemEntityTypeId, SessionVariables.SearchControlColumnsModeId, string.Empty);

			SearchParametersRepeater.DataSource = SearchColumns;
			SearchParametersRepeater.DataBind();

		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				BindRepeater();
			}
		}

		#region Grid Events

		protected virtual void SearchParametersRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			// guard clause
			if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) return;

			var label               = (Label)e.Item.FindControl("label");
			var plcHoverLinkLabel   = (PlaceHolder)e.Item.FindControl("plcHoverLinkLabel");
			var plcControlHolder    = (PlaceHolder)e.Item.FindControl("plcControlHolder");
			var txtbox1             = (TextBox)e.Item.FindControl("txtbox1");
			var hdnfield            = (HiddenField)e.Item.FindControl("hdnfield"); 
			var controlContainerDiv = e.Item.FindControl("controlContainerDiv") as HtmlGenericControl;

			var rowView             = (DataRowView)e.Item.DataItem;

			var displayColumn       = Convert.ToInt32(rowView[FieldConfigurationDataModel.DataColumns.DisplayColumn]);
			var name                = Convert.ToString(rowView[FieldConfigurationDataModel.DataColumns.Name]);
			var cellCount           = Convert.ToInt32(rowView[FieldConfigurationDataModel.DataColumns.CellCount]);
			var displayName         = rowView[FieldConfigurationDataModel.DataColumns.FieldConfigurationDisplayName].ToString();
			var controlType         = rowView[FieldConfigurationDataModel.DataColumns.ControlType].ToString();

			var totalCellCount = cellCount + 2 + 1;

			if (CurrentGridRowCellCount == 0)
			{
				var addIndex = 0;

				// close any open rows
				if (e.Item.ItemIndex > 0)
				{
					e.Item.Controls.AddAt(addIndex, new LiteralControl("</div>"));
					addIndex++;
				}

				// start new row
				e.Item.Controls.AddAt(addIndex, new LiteralControl("<div class='row-fluid form-group'>"));

				CurrentGridRowCellCount = GridRowCellCount;
			}

			// check for accomodation of current item into existing row. if not, add remaining column, close row
			if (CurrentGridRowCellCount != 0 && CurrentGridRowCellCount < totalCellCount)
			{
				//var tempDiv = new HtmlGenericControl("div");
				//tempDiv.Attributes.Add("class", "col-sm-" + CurrentGridRowCellCount);

				var tempDiv = new LiteralControl("<div class='col-sm-" + CurrentGridRowCellCount + "'></div>");
				e.Item.Controls.AddAt(0, tempDiv);

				e.Item.Controls.AddAt(1, new LiteralControl("</div>"));

				CurrentGridRowCellCount = GridRowCellCount;

				// start new row
				e.Item.Controls.AddAt(2, new LiteralControl("<div class='row-fluid form-group'>"));
			}

			CurrentGridRowCellCount = CurrentGridRowCellCount - totalCellCount;

			var lblName = new Label();

			if (controlContainerDiv != null)
			{
				controlContainerDiv.Attributes.Add("class", "col-sm-" + cellCount);
			}

			var labelDiv = new HtmlGenericControl("div");
			labelDiv.Attributes.Add("class", "control-label");

			// link to make the control invisible has to be added only when item is visible

			lblName.Text = "[X]" + displayName + ": ";
			//lblName.CssClass = "control-label";

			labelDiv.Controls.Add(lblName);

			plcHoverLinkLabel.Controls.Add(labelDiv);		
			
			var ctrl = new Control();

			if (controlType.Equals("TextBox", StringComparison.OrdinalIgnoreCase))
			{
				if (plcControlHolder != null)
				{
					ctrl = SetupTextBox(plcControlHolder, name, txtbox1);
				}
			}
			else if (controlType.Equals("DropDownList", StringComparison.OrdinalIgnoreCase))
			{
				ctrl = SetupDropdownList(plcControlHolder, name, txtbox1);
			}
			else // shouldtype
			{
				ctrl = SetupDateRangeControl(e, plcControlHolder, txtbox1, name);
			}

			lblName.AssociatedControlID = ctrl.ID;

			var isGridLines = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.SearchFilterGridLinesKey);

			if (isGridLines)
			{
				txtbox1.CssClass = "searchTextBoxContainerVisible";
			}
			else
			{
				txtbox1.CssClass = "searchTextBoxContainerInVisible";
			}


		}

		private TextBox SetupTextBox(PlaceHolder plcControlHolder, string name, TextBox txtbox1)
		{
			var outerLink = new HtmlGenericControl("div");
			outerLink.Attributes["class"] = "col-sm-11";
			plcControlHolder.Controls.Add(outerLink);

			var txtBox = new TextBox();
			txtBox.ID = "txtbox";
			//txtBox.Attributes["class"] = "form-control";			

			outerLink.Controls.Add(txtBox);

			var configString = string.Empty;

			if (name.Equals("GroupBy") || name.Equals("SubGroupBy"))
			{
				var str = new StringBuilder();

				SetupControlsGrouping(outerLink, name);

				configString = AjaxHelper.GetKendoComboBoxConfigScriptForGroupBy("GetGroupByList", txtBox.ClientID, PrimaryEntity.ToString(), SessionVariables.SearchControlColumnsModeId.ToString());

			}
			else
			{
				#region LoadKendoComboBoxSources

				//configString = LoadKendoComboBoxSources(name, txtBox, plcControlHolder);

				if (string.IsNullOrEmpty(configString))
				{
					var str = new StringBuilder();
					str.AppendLine("$(document).ready(function ()");
					str.AppendLine("        {");
					str.AppendLine("$.ajax(");
					str.AppendLine("        {");
					str.AppendLine("type: \"POST\",");
					str.AppendLine("url: \"http://localhost:53331/API/AutoComplete.asmx/GetAutoCompleteList\",");
					str.AppendLine("data:\"{\'primaryEntity\':\'" + PrimaryEntity + "\',\'txtName\':\'" + name + "\',\'AuditId\':\'" + AuditId + "\'}\",");
					str.AppendLine("contentType: \"application/json; charset=utf-8\",");
					str.AppendLine("dataType: \"json\",");
					str.AppendLine("success: function (msg)");
					str.AppendLine("        {");
					str.AppendLine("$(\"#" + txtBox.ClientID + "\").kendoAutoComplete({");
					str.AppendLine("    dataSource: msg.d,filter: \"startswith\"");
					str.AppendLine("        });");
					str.AppendLine("        }");
					str.AppendLine("        });");
					str.AppendLine("        });");

					configString = str.ToString();
				}

				#endregion
			}

			ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "kendoScript" + name, configString, true);

			txtBox.Attributes.Add("onchange",
				"javascript:SetDevBoxValue('" + plcControlHolder.FindControl("txtBox").ClientID + "','" + txtbox1.ClientID + "')");

			return txtBox;
		}

		private void SetupControlsGrouping(Control plcControlHolder, string name)
		{
			if (name.Equals("GroupBy") || name.Equals("SubGroupBy"))
			{
				//if (SessionVariables.IsTesting)
				//	txtbox1.Visible = true;
				//else
				//	txtbox1.Visible = false;
			}

			// Add Direction Combos Dynamically, check flag whether the corresponding record exists in FC or not.
			if (name.Equals("GroupBy"))
			{
				//plcControlHolder.Controls.Add(new Literal() {Text = "&nbsp;&nbsp;"});

				var dropDownOrderBy = new DropDownList();

				dropDownOrderBy.ID = "dropdownlistOrderBy";
				dropDownOrderBy.CssClass = "k-input";
				dropDownOrderBy.Items.Add("ASC");
				dropDownOrderBy.Items.Add("DESC");
				dropDownOrderBy.Items.Add("Count ASC");
				dropDownOrderBy.Items.Add("Count DESC");

				plcControlHolder.Controls.Add(dropDownOrderBy);
			}
			else if (name.Equals("SubGroupBy"))
			{
				//plcControlHolder.Controls.Add(new Literal() {Text = "&nbsp;&nbsp;"});

				var dropDownOrderBy = new DropDownList();
				dropDownOrderBy.CssClass = "k-input";
				dropDownOrderBy.ID = "dropdownlistOrderBy";
				dropDownOrderBy.Items.Add("ASC");
				dropDownOrderBy.Items.Add("DESC");
				dropDownOrderBy.Items.Add("Count ASC");
				dropDownOrderBy.Items.Add("Count DESC");

				plcControlHolder.Controls.Add(dropDownOrderBy);
			}
		}

		private DateRangeControl SetupDateRangeControl(RepeaterItemEventArgs e, PlaceHolder plcControlHolder, TextBox txtbox1, string name)
		{
			//var datepanel = new Panel();
			DateRangeControl oDateRange = null;

			if (plcControlHolder != null)
			{
				oDateRange = (DateRangeControl)Page.LoadControl(ApplicationCommon.DateRangeControlPath);
				oDateRange.ID = "oDateRange";

				var dtPanel = new Panel();

				dtPanel.ID = "datepanel";
				dtPanel.CssClass = "datepanel col-sm-10";

				dtPanel.Controls.Add(oDateRange);

				plcControlHolder.Controls.Add(dtPanel);

				//datepanel = dtPanel;
			}
			else
			{
				//datepanel.Visible = true;
				oDateRange = (DateRangeControl)e.Item.FindControl("oDateRange");
			}

			txtbox1.Visible = false;

			oDateRange.SettingCategory = PrimaryEntity + name + "DateRangeControl";
			oDateRange.Key = e.Item.ItemIndex.ToString();

			var funccall = "Fillup" + oDateRange.GetKey() + "();";
			oDateRange.DateRangeDropDown.Attributes.Add("onchange", funccall);
			oDateRange.HideLabel();

			return oDateRange;
		}

		private DropDownList SetupDropdownList(PlaceHolder plcControlHolder, string name, TextBox txtbox1)
		{
			var dropDown = new DropDownList();

			var outerLink = new HtmlGenericControl("div");
			outerLink.Attributes["class"] = "col-sm-8";
			plcControlHolder.Controls.Add(outerLink);

			//dropDown.SelectedIndexChanged += dropdownlist_SelectedIndexChanged;
			dropDown.CssClass = "form-control";
			dropDown.ID = "dropdownlist";
			outerLink.Controls.Add(dropDown);

			//LoadDropDownListSources(name, dropDown);

			if (!name.Equals("GroupBy") && !name.Contains("GroupBy"))
			{
				if (!dropDown.Items.Contains(new ListItem("All", "-1")))
				{
					dropDown.Items.Insert(0, new ListItem("All", "-1"));
				}
			}

			dropDown.SelectedIndex = 0;

			txtbox1.Visible = true;
			txtbox1.Text = dropDown.SelectedValue;

			return dropDown;
		}

		#endregion

	}
}