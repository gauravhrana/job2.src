using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Collections;
using System.Text;

namespace Shared.UI.Web.Controls
{
	public partial class SearchControlBucket : UserControl
	{
		#region properties

		public delegate DataTable LoadDataDelegate();
		private LoadDataDelegate _loadData;

		private DataTable SourceTable
		{
			get
			{
				return (DataTable)ViewState["SourceTable"];
			}
			set
			{
				ViewState["SourceTable"] = value;
			}
		}

		private string SourceTextColumn
		{
			get
			{
				return Convert.ToString(ViewState["SourceTextColumn"]);
			}
			set
			{
				ViewState["SourceTextColumn"] = value;
			}
		}

		#endregion

		#region methods

		private void SwitchValues(ListBox source, ListBox target)
		{
			var listRemoval = new ArrayList();

			// Find the number of items selected in the List and items selected
			// Call the move function equal to the number of items selected
			// Remove from Source list, The items moved

			// iterate through source list
			foreach (ListItem itemCurrent in source.Items)
			{
				if (itemCurrent.Selected == true)
				{
					// 1. DETERIMNE - find out which item(s) was selected of SOURCE LIST
					//Response.Write(itemCurrent.ToString());

					// 2. MOVE / COPY - Add it to TARGET LIST
					var copy = new ListItem(itemCurrent.Text, itemCurrent.Value);
					target.Items.Add(copy);

					// 3. REMOVE - Add to external list so we can remove afterwards from the source
					listRemoval.Add(itemCurrent);

					// 4. Set the moved selection as selected, so quickly can move back
					// avoiding the user from reselecting items, disable any preveiously selected items     
					if (target.SelectedItem != null)
					{
						target.SelectedItem.Selected = false;
					}
					target.Items.FindByValue(copy.Value).Selected = true;
				}
			}

			foreach (ListItem itemToRemove in listRemoval)
			{
				source.Items.Remove(itemToRemove);
			}
		}

		public string SelectedItems()
		{
			var selectedResult = new StringBuilder();
			
			foreach (ListItem itemCurrent in lstTarget.Items)
			{
				selectedResult.Append(Convert.ToInt32(itemCurrent.Value) + ",");
			}
			var selectedItems = selectedResult.ToString();

			if (string.IsNullOrEmpty(selectedItems))
				return selectedItems;

			return selectedItems.TrimEnd(selectedItems[selectedItems.Length - 1]);
		}

		public void LoadSource()
		{
			if (SourceTable == null)
			{
				SourceTable = _loadData();
			}

			var dt = SourceTable;
			if (string.IsNullOrEmpty(SourceTextColumn))
			{
				SourceTextColumn = "Name";
				if (!dt.Columns.Contains("Name"))
				{
					for (var iCount = 1; iCount < dt.Columns.Count; iCount++)
					{
						if (dt.Columns[iCount].ColumnName != "ApplicationId")
						{
							SourceTextColumn = dt.Columns[iCount].ColumnName;
							break;
						}
					}
				}
			}
			lstSource.DataSource = dt;
			lstSource.DataTextField = SourceTextColumn;
			lstSource.DataValueField = dt.Columns.Count > 0 ? dt.Columns[0].ColumnName : "Id";
			lstSource.DataBind();
		}

		public void LoadCurrentTarget(string selectedTestCaseList)
		{
			var listSelected = selectedTestCaseList.Split(',');

			foreach (var item in listSelected)
			{
				var lstItem = new ListItem(item);
				lstItem.Value = item;				

				foreach (ListItem itemCurrent in lstSource.Items)
				{
					if (itemCurrent.Value == lstItem.Value)
					{
						lstItem.Text = itemCurrent.Text;
					}					
				}
				if (lstSource.Items.Contains(lstItem))
				{
					lstSource.Items.Remove(lstItem);
				}
				lstTarget.Items.Add(lstItem);			
			}
		}

		public void ConfigureBucket(LoadDataDelegate loadDataDelegate, string selectedTestCaseList)
		{			
			_loadData = loadDataDelegate;			

			LoadSource();			

			if(!string.IsNullOrEmpty(selectedTestCaseList))			
				LoadCurrentTarget(selectedTestCaseList);
			else
				lstTarget.Items.Clear();
		}

		#endregion

		#region Events

		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void btnLeft_Click(object sender, EventArgs e)
		{
			SwitchValues(lstSource, lstTarget);
		}

		protected void btnRight_Click(object sender, EventArgs e)
		{
			SwitchValues(lstTarget, lstSource);
		}

		#endregion
	}
}