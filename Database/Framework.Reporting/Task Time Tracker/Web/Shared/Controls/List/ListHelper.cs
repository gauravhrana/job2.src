using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using Framework.Components;
using Framework.Components.DataAccess;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Controls
{
	public class ListHelper
	{
		public static void AddCheckBox(GridView mainGridView)
		{
			var tcCheckCell = new TableCell();
			var chkCheckBox = new CheckBox();

			if (mainGridView.Rows.Count > 0)
			{
				if (mainGridView.HeaderRow.Cells[0].FindControl("chkSelectAll") == null)
				{
					tcCheckCell = new TableCell();
					chkCheckBox = new CheckBox();
					tcCheckCell.Controls.Add(chkCheckBox);
					chkCheckBox.Text = "All";
					chkCheckBox.ID = "chkSelectAll";
					chkCheckBox.AutoPostBack = true;
					chkCheckBox.Attributes.Add("OnSelectedIndexChanged", "MainGridView_SelectedIndexChanged");
					
					mainGridView.HeaderRow.Cells[0].Controls.Add(chkCheckBox);
				}

				foreach (GridViewRow objRow in mainGridView.Rows)
				{
					var chk = (CheckBox)objRow.Cells[0].FindControl("CheckBox1");

					if (chk != null) continue;

					chkCheckBox = new CheckBox();
					chkCheckBox.ID = "CheckBox1";
					tcCheckCell.Controls.Add(chkCheckBox);
					objRow.Cells[0].Controls.Add(chkCheckBox);
				}
			}
		}

		public static void Style(GridView mainGridView, StateBag viewState, int modeId)
		{
			var systemEntityTypeId = (int)Enum.Parse(typeof(SystemEntity), Convert.ToString(viewState["TableName"]));

			var dt = FieldConfigurationUtility.GetFieldConfigurations(systemEntityTypeId, modeId, null);

			for (var i = 1; i < mainGridView.Columns.Count; i++)
			{
				for (var k = 0; k < dt.Rows.Count; k++)
				{
					if (mainGridView.Columns[i].HeaderText.Equals(dt.Rows[k][FieldConfigurationDataModel.DataColumns.Value]))
					{
						for (var j = 0; j < mainGridView.Rows.Count; j++)
						{
							mainGridView.Rows[j].Cells[i].Style.Add("text-align", dt.Rows[k][FieldConfigurationDataModel.DataColumns.HorizontalAlignment].ToString());
						}
					}
				}
			}
		}

		public static void StyleDataRows(GridView mainGridView, StateBag viewState, int modeId)
		{
			var systemEntityTypeId = (int)Enum.Parse(typeof(SystemEntity), Convert.ToString(viewState["TableName"]));

			var dt = FieldConfigurationUtility.GetFieldConfigurations(systemEntityTypeId, modeId, null);

			for (var i = 1; i < mainGridView.Columns.Count; i++)
			{
				for (var k = 0; k < dt.Rows.Count; k++)
				{
					if (mainGridView.Columns[i].HeaderText.Equals(dt.Rows[k][FieldConfigurationDataModel.DataColumns.Value]))
					{
						for (var j = 0; j < mainGridView.Rows.Count; j++)
						{
							mainGridView.Rows[j].Cells[i].Style.Add("text-align", dt.Rows[k][FieldConfigurationDataModel.DataColumns.HorizontalAlignment].ToString());
						}
					}
				}
			}
		}

		public static void AddProcedures(GridView mainGridView, StateBag viewState, string[] str, string procedureName, string tableName, bool procedureHide)
		{
			var hypUpdateField = new HyperLinkField { HeaderText = procedureName };

			//hypUpdateField.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
			hypUpdateField.Text = procedureName;
			hypUpdateField.DataNavigateUrlFields = str;

            var currentPage = HttpContext.Current.CurrentHandler as Page;
			if (currentPage != null)
				hypUpdateField.DataNavigateUrlFormatString = currentPage.GetRouteUrl(tableName + "EntityRoute", new { Action = procedureName }) + "/{0}";

			hypUpdateField.Visible = !procedureHide;

			mainGridView.Columns.Add(hypUpdateField);

			viewState[procedureName] = mainGridView.Columns.Count - 1;

			// To put the Update and Delete buttons at the right Index during RowCreated Event han
			//ViewState[procedureName] = dt.Columns.IndexOf(procedureName);            
		}
	}
}