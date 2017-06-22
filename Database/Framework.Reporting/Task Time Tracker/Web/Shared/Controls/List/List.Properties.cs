using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Text;
using System.Collections.Specialized;
using System.Collections;

namespace Shared.UI.Web.Controls
{
	public partial class ListControl
	{

		public int DefaultRowCount
		{
			get
			{
				return PerferenceUtility.GetUserPreferenceByKeyAsInt(ApplicationCommon.DefaultRowCountKey, SettingCategory);
			}
		}

		public int? CurrentPageIndex
		{
			get
			{
				if (ViewState["CurrentPageIndex"] == null)
				{
					return null;
				}
				else
				{
                    return Convert.ToInt32(ViewState["CurrentPageIndex"]);
				}
			}
			set
			{
                ViewState["CurrentPageIndex"] = value;
			}
		}

		public bool HideData { get; set; }

		//public string Summary
		//{
		//    get
		//    {
		//        MainGridView.DataSource = dtGlobal;
		//        MainGridView.DataBind();
		//        Label lbl = (Label)MainGridView.BottomPagerRow.FindControl("lblSummary");
		//        return lbl.Text;
		//    }
		//    set {MainGridView.DataSource = dtGlobal;
		//        MainGridView.DataBind();
		//        Label lbl = (Label) MainGridView.BottomPagerRow.FindControl("lblSummary");
		//        lbl.Text = value;
		//    }

		//    //(Label) MainGridView.BottomPagerRow.FindControl("lblSummary"); }
		//}

		public CheckBox SelectAllCheckBox
		{
			get
			{
				if (MainGridView.HeaderRow.Cells[0].FindControl("chkSelectAll") == null)
				{
					for (var i = 0; i < MainGridView.HeaderRow.Cells[0].Controls.Count; i++)
					{
						if (MainGridView.HeaderRow.Cells[0].Controls[i].ID.Equals("chkSelectAll"))
							return (CheckBox)MainGridView.HeaderRow.Cells[0].Controls[i];
					}
				}
				else
				{
					return (CheckBox)(MainGridView.HeaderRow.Cells[0].FindControl("chkSelectAll"));
				}
				return null;
			}

		}

		public bool CustomizedSearch
		{
			get
			{
				if (ViewState["CustomizedSearch"] == null)
				{
					ViewState["CustomizedSearch"] = false;
				}

				return (bool)ViewState["CustomizedSearch"];
			}
			set
			{
				ViewState["CustomizedSearch"] = value;
			}
		}

		public bool IsUpdateColumn
		{
			get
			{
				if (ViewState["IsUpdateColumn"] == null)
				{
					ViewState["IsUpdateColumn"] = true;
				}

				return (bool)ViewState["IsUpdateColumn"];
			}
			set
			{
				ViewState["IsUpdateColumn"] = value;
			}
		}

		public bool IsDeleteColumn
		{
			get
			{
				if (ViewState["IsDeleteColumn"] == null)
				{
					ViewState["IsDeleteColumn"] = true;
				}

				return (bool)ViewState["IsDeleteColumn"];
			}
			set
			{
				ViewState["IsDeleteColumn"] = value;
			}
		}

		public string UserPreferenceCategory
		{
			get;
			set;
		}

		public int UserPreferenceCategoryId
		{
			get;
			set;
		}

		public ExportMenu ExportMenu
		{
			get { return myExportMenu; }
		}

		public string FieldConfigurationMode
		{
			get
			{
				//if (ddlFieldConfigurationMode.SelectedItem != null)
				//    return ddlFieldConfigurationMode.SelectedValue;
				//else
				//    return String.Empty;
				return Convert.ToString(ViewState["FieldConfigurationMode"]);
			}
			set
			{
				ViewState["FieldConfigurationMode"] = value;
			}
		}

		public string FieldConfigurationModeText
		{
			get
			{
				return Convert.ToString(ViewState["FieldConfigurationModeText"]);
			}
			set
			{
				ViewState["FieldConfigurationModeText"] = value;
			}
		}

		public Panel ButtonPanel
		{
			get { return pnlButtonPanel; }
		}

		public Panel ConfigAndExportPanel
		{
			get { return dynBarContainer; }
		}

		public Panel FormattingPanel
		{
			get { return pnlFormatting; }
		}

		public void HideFormattingPanel()
		{
			FormattingPanel.Visible = false;
		}

		public GridView MainGridViewList
		{
			get { return MainGridView; }
		}

		private string GroupByField
		{
			get
			{
				return Convert.ToString(ViewState["GroupByField"]);
			}
			set
			{
				ViewState["GroupByField"] = value;
			}
		}

		private string GroupByFieldValue
		{
			get
			{
				return Convert.ToString(ViewState["GroupByFieldValue"]);
			}
			set
			{
				ViewState["GroupByFieldValue"] = value;
			}
		}

		private string SubGroupByField
		{
			get
			{
				return Convert.ToString(ViewState["SubGroupByField"]);
			}
			set
			{
				ViewState["SubGroupByField"] = value;
			}
		}

		private string SubGroupByFieldValue
		{
			get
			{
				return Convert.ToString(ViewState["SubGroupByFieldValue"]);
			}
			set
			{
				ViewState["SubGroupByFieldValue"] = value;
			}
		}

		//private System.Delegate delGetData;
		//public Delegate DelGetDataRef
		//{
		//	set { delGetData = value; }
		//}

		//private System.Delegate delGetColumns;
		//public Delegate DelGetColumns
		//{
		//	set { delGetColumns = value; }
		//}

	}
}