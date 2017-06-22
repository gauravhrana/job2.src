using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security;
using System.Text;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using DataModel.Framework.Configuration;
using DataModel.Framework.Core;
using Framework.Components.Core;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;
using Shared.UI.Web.Controls;
using System.Web.UI.HtmlControls;
using System.Drawing;

namespace Framework.UI.Web.BaseClasses
{
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:SearchFilterBaseControl runat=server></{0}:SearchFilterBaseControl>")]
	public abstract class ControlSearchFilter : BaseControl
	{

		#region Variables & Properties

		public string TextBoxApplicationIdClientId;

		public delegate string GetKendoComboBoConfig(string fieldName, TextBox txtBox, PlaceHolder plcHolder);
		public GetKendoComboBoConfig GetKendoComboBoConfigString;

		public delegate void LoadComboBoxSources(string fieldName, DropDownList dropDownListControl);
		public LoadComboBoxSources LoadComboBoxSourceMethod;

		protected HtmlGenericControl TabHeaderContainer { get; set; }

		protected HtmlGenericControl TabContainer { get; set; }

		protected Repeater SearchParametersRepeaterCore { get; set; }

		protected SystemEntity PrimaryEntity { get; set; }

		public event EventHandler OnSearch;

		//protected Dictionary<string,string> LstGroupByItems = new Dictionary<string,string>();
		//protected Dictionary<string, string> LstGroupByAddItems = new Dictionary<string, string>();
		//protected Dictionary<string, string> LstGroupByItemsList = new Dictionary<string, string>();
		//List<string> LstGroupByItems = new List<string>();
		//List<string> LstGroupByAddItems = new List<string>();		
		//List<string> LstGroupByItemsList = new List<string>();

		protected virtual DataTable SearchColumns
		{
			get
			{
				if (ViewState["ValidSearchColumns"] != null)
				{
					return (DataTable)ViewState["ValidSearchColumns"];
				}

				return null;
			}
			set
			{
				ViewState["ValidSearchColumns"] = value;
			}
		}

		public string GroupBy
		{
			get
			{
				if (ViewState["GroupBy"] != null)
					return ViewState["GroupBy"].ToString();
				else
					return String.Empty;
			}
			set
			{
				ViewState["GroupBy"] = value;
			}
		}

		public string GroupByDirection
		{
			get
			{
				if (ViewState["GroupByDirection"] != null)
					return ViewState["GroupByDirection"].ToString();
				else
					return String.Empty;
			}
			set
			{
				ViewState["GroupByDirection"] = value;
			}
		}

		public bool DoesGroupByDirectionExist
		{
			get
			{
				if (ViewState["DoesGroupByDirectionExist"] != null)
					return (bool)ViewState["DoesGroupByDirectionExist"];
				else
					return false;
			}
			set
			{
				ViewState["DoesGroupByDirectionExist"] = value;
			}
		}

		public string SubGroupBy
		{
			get
			{
				if (ViewState["SubGroupBy"] != null)
					return ViewState["SubGroupBy"].ToString();
				else
					return String.Empty;
			}
			set
			{
				ViewState["SubGroupBy"] = value;
			}
		}

		public string DoesSubGroupByDirectionExist
		{
			get
			{
				if (ViewState["DoesSubGroupByDirectionExist"] != null)
					return ViewState["DoesSubGroupByDirectionExist"].ToString();
				else
					return String.Empty;
			}
			set
			{
				ViewState["DoesSubGroupByDirectionExist"] = value;
			}
		}

		public bool IsSubGroupByDirectionExists
		{
			get
			{
				if (ViewState["IsSubGroupByDirectionExists"] != null)
					return (bool)ViewState["IsSubGroupByDirectionExists"];
				else
					return false;
			}
			set
			{
				ViewState["IsSubGroupByDirectionExists"] = value;
			}
		}

		public bool IsGroupByFeatureEnabled
		{
			get
			{
				if (ViewState["IsGroupByFeatureEnabled"] != null)
					return (bool)ViewState["IsGroupByFeatureEnabled"];
				else
					return true;
			}
			set
			{
				ViewState["IsGroupByFeatureEnabled"] = value;
			}
		}

		protected SearchActionBar SearchActionBarCore { get; set; }

		#endregion

		#region Methods

		public virtual void SetSearchParameters(object obj)
		{
			if (SearchColumns != null)
			{
				foreach (DataRow dr in SearchColumns.Rows)
				{
					// name of field
					var fieldName   = dr[FieldConfigurationDataModel.DataColumns.Name].ToString();
					var controlType = dr[FieldConfigurationDataModel.DataColumns.ControlType].ToString();

					// we have seperate logics for Group By
					if (!fieldName.Contains("GroupBy") && !controlType.Contains("DatePanel"))
					{
                        var fieldValue = string.Empty;
						//if((fieldName.ToLower() == "applicationid" || fieldName.ToLower() == "application")
						//	&& ApplicationCommon.ApplicationCache[SessionVariables.RequestProfile.ApplicationId].RenderApplicationFilter == 0)
						//{
						//	//fieldValue = SessionVariables.RequestProfile.ApplicationId.ToString();
						//}
						//else{
                            fieldValue = GetParameterValue(fieldName);
                        //}

                        if (fieldValue != "-1" && fieldValue != "None" && !string.IsNullOrEmpty(fieldValue.Trim()))
                        {
                            PropertyMapper.SetProperty(obj, fieldName, fieldValue);
                        }
					}
				}
			}
		}

		public void HookUp(SystemEntity entity, string key)
		{
			PrimaryEntity = entity;
			PrimaryEntityKey = key;
		}

		public int? CheckIfValueIsValidAsInt(string strValue)
		{
			int numericValue;
			var isNumber = int.TryParse(strValue, out numericValue);

			if (strValue != "-1" && strValue != "All" && !string.IsNullOrEmpty(strValue) && !strValue.Contains('.') && isNumber)
			{
				return Convert.ToInt32(strValue);
			}

			return null;
		}

		public void SetupJqueryTabs()
		{
			if (TabContainer == null || TabHeaderContainer == null) return;

			// review
			TabContainer.Style.Add("background", "lightgrey");

			if (SearchColumns == null || SearchColumns.Rows.Count == 0) return;

			var distColumns = new List<string>();

			if (IsGroupByFeatureEnabled)
			{
				// get distinct values of display columns to decide that tabs are needed or not.
				distColumns = (from row in SearchColumns.AsEnumerable()
							   select row["DisplayColumn"].ToString().Trim())
									.Distinct(StringComparer.CurrentCultureIgnoreCase).ToList();
			}
			else // exclude columns where name contains "GroupBy"
			{
				// get distinct values of display columns to decide that tabs are needed or not.
				distColumns = (from row in SearchColumns.AsEnumerable()
							   where !row[FieldConfigurationDataModel.DataColumns.Name].ToString().Contains("GroupBy")
							   select row["DisplayColumn"].ToString().Trim())
								.Distinct(StringComparer.CurrentCultureIgnoreCase).ToList();
			}

			// if only 1 display column, then tab not required
			// TODO: Reivew
			//if (distColumns.Count == 1)
			//{
			//	return;
			//}

			// create tab headers
			for (var i = 1; i <= distColumns.Count; i++)
			{
				switch (i)
				{
					case 1:
						AddTab("Primary");
						break;
					case 2:
						AddTab("Secondary");
						break;
				}
			}

			// This flag is an indicator of the searchable columns, it will be false it only 1 searchable column
			if (IsGroupByFeatureEnabled)
			{
				AddTab("Misc");
				AddTab("All");
			}
		}

		//http://stackoverflow.com/questions/5791723/creating-and-dynamically-using-content-templates-and-controls
		private HtmlGenericControl AddTab(string code)
		{
			var divTabContentArea = new HtmlGenericControl("div");
			TabContainer.Controls.Add(divTabContentArea);

			var headerLink = new HtmlGenericControl("li");
			headerLink.ID = "li" + code.Replace(" ", "");

			var aLink = new HtmlGenericControl("a");
			aLink.Attributes.Add("href", "#" + divTabContentArea.ClientID);
			aLink.InnerHtml = code;

			headerLink.Controls.Add(aLink);

			TabHeaderContainer.Controls.Add(headerLink);

			return headerLink;
		}

		#region GetParameterValue

		public void GetAllParameterValue(dynamic data)
		{
			data.GetDynamicMemberNames();
		}

		public void GetParameterValue(StandardDataModel data, string key)
		{
			if (SearchParametersRepeaterCore.Items.Count == 0) return;

			var value = GetParameterValue(key);

			switch (key)
			{
				case StandardDataModel.StandardDataColumns.Name:
					data.Name = value;
					break;

				case StandardDataModel.StandardDataColumns.Description:
					data.Description = value;
					break;
			}
		}

		public int? GetParameterValueAsInt(string key)
		{
			int? value = null;
			int numericValue;

			var strValue = GetParameterValue(key);
			var isNumber = int.TryParse(strValue, out numericValue);

			if (!string.IsNullOrEmpty(strValue) && strValue != "-1" && strValue != "All" && isNumber)
			{
				value = Convert.ToInt32(strValue);
			}

			return value;
		}

		public string GetParameterValueForListBox(string key)
		{
			var finalValue = string.Empty;

			var strValue = GetParameterValue(key);

			var indices = strValue.Split('/');
			var indexlist = new int[indices.Length];

			for (var k = 0; k < indices.Length; k++)
			{
				if (!string.IsNullOrEmpty(indices[k]))
				{
					indexlist[k] = Convert.ToInt32(indices[k]);
				}
			}

			//if (indexlist.Length > 1 && !(string.IsNullOrEmpty(indexlist[0].ToString())))
			//{
			//    data.FunctionalityStatusIds = indexlist;
			//}
			//else
			//{
			//    if (indexlist.Length == 1 && indexlist[0] != -1 && indexlist[0] != 0 && !(string.IsNullOrEmpty(indexlist[0].ToString())))
			//    {
			//        data.FunctionalityStatusId = indexlist[0];
			//    }
			//}

			return finalValue;
		}

		public List<DateTime?> GetParameterValueForDatePanel(string key)
		{
			var result = new List<DateTime?>();

			var dateValue = GetParameterValue(key);
			if (!string.IsNullOrEmpty(dateValue))
			{
				var dates = dateValue.Split('&');
				if (Boolean.Parse(dates[2]))
				{
					result.Add(DateTimeHelper.FromApplicationDateFormatToDate(dates[0]));
					result.Add(DateTimeHelper.FromApplicationDateFormatToDate(dates[1]));
				}
			}

			return result;
		}

		public string GetParameterValue(string key)
		{
			var value = String.Empty;

			// logic commented out JIRA #3780 (see comments)
			//var isParameterVisible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(key + "Visibility", SettingCategory);
			//if (isParameterVisible)
			//{

			value = CheckAndGetFieldValue(key).ToString();

			//}

			return value;
		}

		#endregion

		protected virtual DataTable GetFilteredList(string filterName, DataTable dtData, DataTable dtDataList)
		{
			var dt = dtDataList.Clone();

			for (var i = 0; i < dtData.Rows.Count; i++)
			{
				for (var j = 0; j < dtDataList.Rows.Count; j++)
				{
					var a = dtData.Rows[i][filterName + "Id"].ToString();
					var b = dtDataList.Rows[j][filterName + "Id"].ToString();

					if (a.Equals(b))
					{
						dt.ImportRow(dtDataList.Rows[j]);
						continue;
					}
				}
			}

			return dt;
		}

		// loads the GroupBy drop downs in search control
		protected virtual void LoadGroupByDropDownList(string[] itemNames, Array itemValues, DropDownList dropDownListControl)
		{
			for (var i = 0; i <= itemNames.Length - 1; i++)
			{
				var item = new ListItem(itemNames.GetValue(i).ToString(), itemValues.GetValue(i).ToString());
				dropDownListControl.Items.Add(item);
				//dropDownListControl.CssClass = "TTT-LargeComboWidth";
			}
		}

		protected virtual void IsControlValid()
		{
			if (!string.IsNullOrEmpty(SettingCategory))
			{
				PerferenceUtility.CreateUserPreferenceCategoryIfNotExists(SettingCategory, "Search Control Name");
			}
			else
			{
				throw new Exception("Search control is not named");
			}
		}

		protected virtual void SetDefaultValues()
		{
			for (var i = 0; i < SearchParametersRepeaterCore.Items.Count; i++)
			{
				var hdnfield = (HiddenField)SearchParametersRepeaterCore.Items[i].FindControl("hdnfield");

				var dropdownlist	= (DropDownList)SearchParametersRepeaterCore.Items[i].FindControl("dropdownlist");
				var txtbox			= (TextBox)SearchParametersRepeaterCore.Items[i].FindControl("txtbox");
				var chkbox			= (LinkButton)SearchParametersRepeaterCore.Items[i].FindControl("chkbox");
				var txtDevBox			= (TextBox)SearchParametersRepeaterCore.Items[i].FindControl("txtDevBox");
				var listbox			= (ListBox)SearchParametersRepeaterCore.Items[i].FindControl("listbox");

				//dropdownlist.CssClass = "TTT-LargeComboWidth";

				// guard clause
				if (hdnfield == null || txtbox == null)
				{
					//continue;
				}

				if (!SearchParametersRepeaterCore.Items[i].Visible)
				{
					if ((hdnfield.Value == "GroupBy" || hdnfield.Value == "SubGroupBy") && !IsGroupByFeatureEnabled)
					{
						SearchParametersRepeaterCore.Items[i].Visible = false;
						return;
					}

					SearchParametersRepeaterCore.Items[i].Visible = true;
					PerferenceUtility.UpdateUserPreference(SettingCategory, hdnfield.Value + "Visibility", "true");
				}

				if (txtbox != null && txtbox.Visible)
				{
					txtbox.Text = String.Empty;
				}
				else if (dropdownlist != null && dropdownlist.Visible)
				{
					dropdownlist.SelectedIndex = -1;
					txtDevBox.Text = "-1";
				}
				else if (listbox != null && listbox.Visible)
				{
					listbox.SelectedIndex = -1;
					txtDevBox.Text = "-1";
				}
			}

			// we need to get seettings we are setting we just saved to update our cache
			PerferenceUtility.RefreshUserPreferencesCache();
		}

		// this needs to be implemented in the inheriting class to load the drop downs in search control
		public virtual void LoadDropDownListSources(string fieldName, DropDownList dropDownListControl)
		{
			if (LoadComboBoxSourceMethod != null)
			{
				LoadComboBoxSourceMethod(fieldName, dropDownListControl);
			}
		}

		// this needs to be implemented in the inheriting class to load the list boxes in search control
		public virtual void LoadListBoxSources(string fieldName, ListBox lstBoxControl)
		{
		}

		// this needs to be implemented in the inheriting class to load the text boxes with autocomplete values in search control
		public virtual void LoadTextBoxSources(string fieldName, AutoCompleteExtender txtBoxControl)
		{
		}

		// this needs to be implemented in the inheriting class to eliminate items in group by list boxes in search control
		//public virtual Dictionary<string,string> CheckGroupByListBoxSources(string fieldName)
		//{
		//	return new Dictionary<string,string>();
		//}

		//// this needs to be implemented in the inheriting class to include items in group by list boxes in search control
		//public virtual Dictionary<string,string> CheckAddGroupByListBoxSources()
		//{
		//	return new Dictionary<string,string>();
		//}

		// this needs to be implemented in the inheriting class to load the Kendo combo boxes with autocomplete values in search control
		public virtual string LoadKendoComboBoxSources(string fieldName, TextBox txtBox, PlaceHolder plcHolder)
		{
			if (GetKendoComboBoConfigString != null)
			{
				return GetKendoComboBoConfigString(fieldName, txtBox, plcHolder);
			}

			return string.Empty;
		}

		public virtual object CheckAndGetFieldValue(string field, bool ddlReturnValue = true, bool retrieveValue = false)
		{
			for (var i = 0; i < SearchParametersRepeaterCore.Items.Count; i++)
			{
				var item = SearchParametersRepeaterCore.Items[i];

				//var label = (Label)SearchParametersRepeaterCore.Items[i].FindControl("label");
				var hdnfield = (HiddenField)item.FindControl("hdnfield");

				// guard clause
				if (hdnfield == null || !SearchParametersRepeaterCore.Items[i].Visible)
				{
					continue;
				}

				if (hdnfield.Value.Equals(field))
				{
					var txtbox			= (TextBox)item.FindControl("txtbox");
					var dropdownList	= (DropDownList)item.FindControl("dropdownlist");
					var listbox			= (ListBox)item.FindControl("listbox");
					var oDateRange		= (DateRangeControl)item.FindControl("oDateRange");

					// Text Box
					if (txtbox != null && txtbox.Visible)
					{
						return txtbox.Text;
					}
					// Drop down
					else if (dropdownList != null && dropdownList.Visible)
					{
						#region MyRegion
						//if (field == "GroupBy")
						//{
						//	if (keyOrValue == true)
						//	{
						//		if (dropdownList.SelectedItem.Text != "All")
						//		{
						//			GroupBy = dropdownList.SelectedItem.Value;
						//		}
						//		else
						//		{
						//			GroupBy = String.Empty;
						//		}
						//	}
						//	else
						//	{
						//		if (dropdownList.SelectedItem.Text != "All")
						//		{
						//			GroupBy = dropdownList.SelectedItem.Text;
						//		}
						//		else
						//		{
						//			GroupBy = String.Empty;
						//		}
						//	}
						//}
						//else if (field == "SubGroupBy")
						//{
						//	if (keyOrValue == true)
						//	{
						//		if (dropdownList.SelectedItem.Text != "All")
						//		{
						//			SubGroupBy = dropdownList.SelectedItem.Value;
						//		}
						//		else
						//		{
						//			SubGroupBy = String.Empty;
						//		}
						//	}
						//	else
						//	{
						//		if (dropdownList.SelectedItem.Text != "All")
						//		{
						//			SubGroupBy = dropdownList.SelectedItem.Text;
						//		}
						//		else
						//		{
						//			SubGroupBy = String.Empty;
						//		}
						//	} 
						//}
						#endregion

						if (ddlReturnValue)
						{
							if (retrieveValue)
								return dropdownList.SelectedItem.Value;
							else
								return dropdownList.SelectedValue;
						}
						else
						{
							if (retrieveValue)
								return dropdownList.SelectedItem.Value;
							else
								return dropdownList.SelectedItem.Text;
						}
					}
					// List box
					else if (listbox != null && listbox.Visible)
					{
						var values = string.Empty;

						if (ddlReturnValue)
						{
							var indices = listbox.GetSelectedIndices();

							for (var j = 0; j < indices.Length; j++)
							{
								values += listbox.Items[indices[j]].Value;

								if (j != indices.Length - 1)
									values += "/";
							}

							return values;
						}
						else
						{
							var indices = listbox.GetSelectedIndices();

							for (var j = 0; j < indices.Length; j++)
							{
								values += listbox.Items[indices[j]].Text;
								if (j != indices.Length - 1)
									values += "/";
							}

							return values;
						}
					}
					// Date Range
					else if (oDateRange != null)
					{
						oDateRange.SaveDateValues(oDateRange.DateRangeDropDown.SelectedValue, oDateRange.FromDateTime, oDateRange.ToDateTime);

						var fromDate = DateTimeHelper.FromUserDateFormatToApplicationDateFormat(oDateRange.FromDateTime);
						var toDate = DateTimeHelper.FromUserDateFormatToApplicationDateFormat(oDateRange.ToDateTime);

						return fromDate + "&" + toDate + "&" + oDateRange.Checked;
					}
				}
				// Group By Direction is on the same row as on Group By and it doesn't have associated hidden field
				// , so we will make use of GroupBy's HiddenField to locate the row from Repeater
				else if (hdnfield.Value.Equals("groupby", StringComparison.OrdinalIgnoreCase) && field.Equals("groupbydirection", StringComparison.OrdinalIgnoreCase))
				{
					var orderByDropDown = (DropDownList)SearchParametersRepeaterCore.Items[i].FindControl("dropdownlistOrderBy");

					if (orderByDropDown != null)
					{
						GroupByDirection = orderByDropDown.SelectedItem.Text;
						return GroupByDirection;
					}
				}
				// Sub Group By Direction is on the same row as on Sub Group By and it doesn;t have associated hidden field,
				// so we will make use of SubGroupBy's HiddenField to locate the row from Repeater
				else if (hdnfield.Value.Equals("subgroupby", StringComparison.OrdinalIgnoreCase) && field.Equals("subgroupbydirection", StringComparison.OrdinalIgnoreCase))
				{
					var orderByDropDown = (DropDownList)SearchParametersRepeaterCore.Items[i].FindControl("dropdownlistOrderBy");
					if (orderByDropDown != null)
					{
						DoesSubGroupByDirectionExist = orderByDropDown.SelectedItem.Text;
						return DoesSubGroupByDirectionExist;
					}
				}

				// if it got this far it matching field
				//break;
			}

			return String.Empty;
		}

		protected virtual string CheckAndSetFieldValue(string field, string value)
		{
			for (var i = 0; i < SearchParametersRepeaterCore.Items.Count; i++)
			{
				//var label    = (Label)SearchParametersRepeaterCore.Items[i].FindControl("label");
				var hdnfield = (HiddenField)SearchParametersRepeaterCore.Items[i].FindControl("hdnfield");

				if (hdnfield == null) continue;

				if (hdnfield.Value.Equals(field))
				{
					var txtbox			= (TextBox)SearchParametersRepeaterCore.Items[i].FindControl("txtbox");
					
					var txtDevBox		= (TextBox)SearchParametersRepeaterCore.Items[i].FindControl("txtDevBox");

					var dropdownlist	= (DropDownList)SearchParametersRepeaterCore.Items[i].FindControl("dropdownlist");
					var listbox			= (ListBox)SearchParametersRepeaterCore.Items[i].FindControl("listbox");
					var oDateRange		= (DateRangeControl)SearchParametersRepeaterCore.Items[i].FindControl("oDateRange");
					var datepanel		= (Panel)SearchParametersRepeaterCore.Items[i].FindControl("datepanel");

					if (txtbox != null && txtbox.Visible)
					{
						txtbox.Text = value;
						txtDevBox.Text = value;
						if (hdnfield.Value.Equals("Description"))
						{
							//txtbox.Width = 300;
						}

						// Testing implementation of JIRA 3392
						if (field == "GroupBy")
						{
							if (txtbox.Text != "None")
							{
								GroupBy = txtbox.Text;
							}
							else
							{
								GroupBy = String.Empty;
							}
						}
						else if (field == "SubGroupBy")
						{
							if (txtbox.Text != "None")
							{
								SubGroupBy = txtbox.Text;
							}
							else
							{
								SubGroupBy = String.Empty;
							}
						}
					}
					else if (dropdownlist != null && dropdownlist.Visible)
					{
						dropdownlist.SelectedIndex = UIHelper.GetDropDownSelectedIndex(dropdownlist, field, SettingCategory);
						txtDevBox.Text = dropdownlist.SelectedValue;

						//if (field == "GroupBy")
						//{
						//	if (dropdownlist.SelectedItem.Text != "All")
						//	{
						//		GroupBy = dropdownlist.SelectedValue;
						//	}
						//	else
						//	{
						//		GroupBy = String.Empty;
						//	}
						//}
						//else if (field == "SubGroupBy")
						//{
						//	if (dropdownlist.SelectedItem.Text != "All")
						//	{
						//		SubGroupBy = dropdownlist.SelectedValue;
						//	}
						//	else
						//	{
						//		SubGroupBy = String.Empty;
						//	}
						//}
					}
					else if (listbox != null && listbox.Visible)
					{
						listbox.ClearSelection();
						if (value.Contains("/"))
						{
							var indices = value.Split('/');
							var txtvalue = string.Empty;
							txtDevBox.Text = String.Empty;

							for (var j = 0; j < indices.Length; j++)
							{
								foreach (ListItem item in listbox.Items)
								{
									if (item.Text.Equals(indices[j]))
									{
										item.Selected = true;
										txtvalue += item.Value;
										txtvalue += "/";

									}
								}
							}
							txtvalue.Remove(txtvalue.Length - 1);
							txtDevBox.Text = txtvalue;
						}
						else
						{
							listbox.SelectedIndex = listbox.Items.IndexOf(listbox.Items.FindByText(value));
							txtDevBox.Text = listbox.SelectedValue;
						}
					}
					else if ((datepanel != null && datepanel.Visible) || (oDateRange != null && oDateRange.Visible))
					{

						var dates = value.Split('&');
						oDateRange.SetDateValues(value);

						if (dates.Length > 2)
						{
							oDateRange.Checked = bool.Parse(dates[2]);
						}
					}
					break;
				}
				// Group By Direction is on the same row as on Group By and it doesn;t have associated hidden field, so we will make use of GroupBy's HiddenField to locate the row from Repeater
				else if (hdnfield.Value.ToLower() == "groupby" && field.ToLower() == "groupbydirection")
				{
					var orderByDropDown = (DropDownList)SearchParametersRepeaterCore.Items[i].FindControl("dropdownlistOrderBy");

					if (orderByDropDown != null)
					{
						orderByDropDown.SelectedIndex = UIHelper.GetDropDownSelectedIndex(orderByDropDown, field, SettingCategory);
						GroupByDirection = orderByDropDown.SelectedItem.Text;
					}
				}
				// Sub Group By Direction is on the same row as on Sub Group By and it doesn;t have associated hidden field, so we will make use of SubGroupBy's HiddenField to locate the row from Repeater
				else if (hdnfield.Value.ToLower() == "subgroupby" && field.ToLower() == "subgroupbydirection")
				{
					var orderByDropDown = (DropDownList)SearchParametersRepeaterCore.Items[i].FindControl("dropdownlistOrderBy");

					if (orderByDropDown != null)
					{
						orderByDropDown.SelectedIndex = UIHelper.GetDropDownSelectedIndex(orderByDropDown, field, SettingCategory);
						DoesSubGroupByDirectionExist = orderByDropDown.SelectedItem.Text;
					}
				}
			}

			return String.Empty;
		}

		protected virtual string GetSearchKeyValue(string colName, DataSet ds)
		{
			var strValue = String.Empty;

			if (ds.Tables.Count <= 2) return strValue;

			var drDetails = ds.Tables[1].Select(SearchKeyDetailDataModel.DataColumns.SearchParameter + " = '" + colName + "'");

			if (drDetails.Length > 0)
			{
				var detailId = Convert.ToString(drDetails[0][SearchKeyDetailDataModel.DataColumns.SearchKeyDetailId]);

				var drItems = ds.Tables[2].Select(SearchKeyDetailItemDataModel.DataColumns.SearchKeyDetailId + " = " + detailId);

				if (drItems.Length > 0)
				{
					strValue = Convert.ToString(drItems[0][SearchKeyDetailItemDataModel.DataColumns.Value]);

					if (drItems.Length > 1)
					{
						for (var iCount = 1; iCount < drItems.Length; iCount++)
						{
							strValue += "/" + Convert.ToString(drItems[iCount][SearchKeyDetailItemDataModel.DataColumns.Value]);
						}
					}
				}
			}

			return strValue;
		}

		public virtual void RaiseSearch()
		{
			if (OnSearch != null)
			{
				OnSearch(this, EventArgs.Empty);
			}
		}

		protected override void GetSettings()
		{
			var searchKeyId = Convert.ToString(Page.Request.QueryString["SearchKey"]);

			if (!string.IsNullOrEmpty(searchKeyId))
			{
				var dataSearchKey = new SearchKeyDataModel();
				dataSearchKey.SearchKeyId = Convert.ToInt32(searchKeyId);

				var ds = SearchKeyDataManager.SearchByKey(dataSearchKey, SessionVariables.RequestProfile);

				for (var i = 0; i < SearchColumns.Rows.Count; i++)
				{
					var colName = Convert.ToString(SearchColumns.Rows[i]["Name"]);

					CheckAndSetFieldValue(colName, GetSearchKeyValue(colName, ds));
				}
			}
			else
			{
				var category = SettingCategory;
				//var value = String.Empty;

				for (var i = 0; i < SearchColumns.Rows.Count; i++)
				{
					var name = SearchColumns.Rows[i]["Name"].ToString();
					var value = PerferenceUtility.GetUserPreferenceByKey(name, category);

                    if ((name.ToLower() == "applicationid" || name.ToLower() == "application")
                            && ApplicationCommon.ApplicationCache[SessionVariables.RequestProfile.ApplicationId].RenderApplicationFilter == 0)
                    {
                        value = SessionVariables.RequestProfile.ApplicationId.ToString();
                    }

					CheckAndSetFieldValue(name, value);

				}
			}
		}

		protected override void SaveSettings()
		{
			// validation logic for Group By and Sub Group By
			var groupByValue = string.Empty;

			if (SearchColumns == null) return;

			Log4Net.LogInfo("Save Settings For Search Control Start");

			foreach (DataRow dr in SearchColumns.Rows)
			{
				var columnName = Convert.ToString(dr["Name"]);
				//var columnValue = CheckAndGetFieldValue(columnName, false).ToString();
				var columnValue = string.Empty;

				switch (columnName)
				{
					case "GroupBy":
						columnValue = CheckAndGetFieldValue(columnName, true, true).ToString();
						groupByValue = columnValue;
						break;

					case "SubGroupBy":
						columnValue = CheckAndGetFieldValue(columnName, true, true).ToString();

						// get "sub group by" search previous value
						var subGroupByValue = columnValue;

						// if it is not 'none' then , change interchange it with Group By and make sub Group By None
						if ((subGroupByValue != "None" || subGroupByValue != "-1") && (groupByValue == "None" || groupByValue == "-1"))
						{
							PerferenceUtility.UpdateUserPreference(SettingCategory, "GroupBy", subGroupByValue);
							columnValue = groupByValue;

							// set values in repeater
							CheckAndSetFieldValue("GroupBy", subGroupByValue);
							CheckAndSetFieldValue(columnName, columnValue);	// for Sub Group By
						}
						else if (subGroupByValue == groupByValue)
						{
							columnValue = "None";
							// set values in repeater
							CheckAndSetFieldValue(columnName, columnValue);// for Sub Group By
						}
						break;

					default:
						columnValue = CheckAndGetFieldValue(columnName, false).ToString();
						break;
				}
                if ((columnName.ToLower() == "applicationid" || columnName.ToLower() == "application")
                            && ApplicationCommon.ApplicationCache[SessionVariables.RequestProfile.ApplicationId].RenderApplicationFilter == 0)
                {
                }
                else
                {
                    PerferenceUtility.UpdateUserPreference(SettingCategory, columnName, columnValue);
                }
				CheckAndSetFieldValue(columnName, columnValue);
			}

			// we need to get settings we are setting we just saved to update our cache
			PerferenceUtility.RefreshUserPreferencesCache();

			Log4Net.LogInfo("Save Settings For Search Control End");
		}

		protected virtual int SaveSearchKey()
		{
			var searchKeyId = 0;

			if (SearchColumns == null) return searchKeyId;

			var data = new SearchKeyDataModel();
			data.Name = DateTime.Now.ToLongTimeString();
			data.View = PrimaryEntity.ToString();
			data.SortOrder = 1;
			data.Description = PrimaryEntity.ToString();

			searchKeyId = SearchKeyDataManager.Create(data, SessionVariables.RequestProfile);

			foreach (DataRow dr in SearchColumns.Rows)
			{
				//try
				//{
					var columnName = Convert.ToString(dr["Name"]);
					var columnValue = CheckAndGetFieldValue(columnName, false).ToString();

					var dataDetail = new SearchKeyDetailDataModel();
					dataDetail.SearchKeyId = searchKeyId;

					//ApplicationCommon.UpdateUserPreference(SettingCategory, columnName, columnValue);
					dataDetail.SearchParameter = columnName;
					dataDetail.SortOrder = 1;
					var detailId = SearchKeyDetailDataManager.Create(dataDetail, SessionVariables.RequestProfile);

					var dataDetailItem = new SearchKeyDetailItemDataModel();
					dataDetailItem.SearchKeyDetailId = detailId;
					dataDetailItem.SortOrder = 1;

					dataDetailItem.Value = columnValue;
					SearchKeyDetailItemDataManager.Create(dataDetailItem, SessionVariables.RequestProfile);
				//}
				//catch
				//{
				//	// why ?
				//}
			}

			return searchKeyId;
		}

		public void SetupSearch()
		{
			//Log4Net.LogInfo("SetupSearch For Search Control Start");

			if (SearchColumns == null)
			{
				// get and cache results
				SearchColumns = FieldConfigurationUtility.GetFieldConfigurations(PrimaryEntity, SessionVariables.SearchControlColumnsModeId, string.Empty);

				// here add logic to sip items which have GridViewPrioriy as -1
				// keep the variable name same and that should do
				SearchColumns = SearchColumns.AsEnumerable()
								.Where(row => row.Field<int>(FieldConfigurationDataModel.DataColumns.GridViewPriority) != -1)
								.CopyToDataTable();			

				// validation logic for Group By and Sub Group By
				var groupByValue = string.Empty;

				// check if group by exists in FC records
				var drs = SearchColumns.Select("Name = 'GroupBy'");
				if (drs.Length > 0)
				{
					// get "group by" search previous value
					groupByValue = PerferenceUtility.GetUserPreferenceByKey("GroupBy", SettingCategory);
				}

				drs = SearchColumns.Select("Name = 'SubGroupBy'");
				if (drs.Length > 0)
				{
					// get "sub group by" search previous value
					var subGroupByValue = PerferenceUtility.GetUserPreferenceByKey("SubGroupBy", SettingCategory);

					// if it is not 'none' then , change interchange it with Group By and make sub Group By None
					if ((subGroupByValue != "None" || subGroupByValue != "-1") && (groupByValue == "None" || groupByValue == "-1"))
					{
						PerferenceUtility.UpdateUserPreference(SettingCategory, "GroupBy", subGroupByValue);
						PerferenceUtility.UpdateUserPreference(SettingCategory, "SubGroupBy", groupByValue);
					}
					else if (subGroupByValue == groupByValue)
					{
						PerferenceUtility.UpdateUserPreference(SettingCategory, "SubGroupBy", "None");
					}
				}

				// count of all columns for GrpBy and Sub Grp By
				var grpByColumnsCount = SearchColumns.AsEnumerable()
					.Count(r => r.Field<String>(FieldConfigurationDataModel.DataColumns.Name).Contains("GroupBy"));

				// count of total other columns
				var nonGrpByColumnsCount = SearchColumns.Rows.Count - grpByColumnsCount;

				// if only 1 searchable column, disable group by feature (#3754)
				if (nonGrpByColumnsCount == 1)
				{
					IsGroupByFeatureEnabled = false;
				}
			}

			// based on distinct display columns present in data, tabs will be created or not created
			SetupJqueryTabs();

			//bool flag will indicate if GroupByDirection exists or not, to be used in item_bound event
			var filterExpression = FieldConfigurationDataModel.DataColumns.Name + " = 'GroupByDirection'";
			var rows = SearchColumns.Select(filterExpression);
			if (rows.Length > 0)
			{
				DoesGroupByDirectionExist = true;
			}

			//bool flag will indicat if SubGroupByDirection exists or not, to be used in item_bound event
			filterExpression = FieldConfigurationDataModel.DataColumns.Name + " = 'SubgroupByDirection' ";
			rows = SearchColumns.Select(filterExpression);
			if (rows.Length > 0)
			{
				IsSubGroupByDirectionExists = true;
			}

			// filter expression for excluding Direction Combos, as they will not be on seperate rows
			// they will be shown via item bound event if they exist in FC, beside the respective groyp by dropdown, via flags
			filterExpression = FieldConfigurationDataModel.DataColumns.Name + " <> 'GroupByDirection' and ";
			filterExpression += FieldConfigurationDataModel.DataColumns.Name + " <> 'SubgroupByDirection' ";

			SearchColumns.DefaultView.RowFilter = filterExpression;
			SearchColumns.DefaultView.Sort = FieldConfigurationDataModel.DataColumns.GridViewPriority + " ASC";

			SearchParametersRepeaterCore.DataSource = SearchColumns.DefaultView.ToTable();
			SearchParametersRepeaterCore.DataBind();

			if (!string.IsNullOrEmpty(SettingCategory))
			{
				PerferenceUtility.CreateUserPreferenceCategoryIfNotExists(SettingCategory, "Search Control Name");
			}
			else
			{
				throw new Exception("Search control is not named");
			}

			// why is this done ?
			GetSettings();
			SaveSettings();
			RaiseSearch();

			//Log4Net.LogInfo("SetupSearch For Search Control End");
		}

		public virtual string SwitchColumnValues(string fromDateKey, string toDateKey)
		{

			// get existinv values to temp variables
			var value1 = CheckAndGetFieldValue(fromDateKey, true, true);
			var value2 = CheckAndGetFieldValue(toDateKey, true, true);

			// switch
			var tmpValue = value1;
			value1 = value2;
			value2 = tmpValue;

			// set in repeater and update to UP
			CheckAndSetFieldValue(fromDateKey, value1.ToString());			
			CheckAndGetFieldValue(fromDateKey, true, true);

			CheckAndSetFieldValue(toDateKey, value2.ToString());
			CheckAndGetFieldValue(toDateKey, true, true);		

			return string.Empty;
		}

		#endregion

		#region Page Events

		protected override void OnLoad(EventArgs e)
		{
			var panel = SearchParametersRepeaterCore.Parent as Panel;

			if (panel != null)
			{
				var isGridLines = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.SearchFilterGridLinesKey);

				panel.CssClass = isGridLines ? "searchFilterGridLines" : "searchFilterNoGridLines";
			}

			SearchActionBarCore.Setup(PrimaryEntityKey, SaveSearchKey, this);

			//var script = string.Empty;
			//script = "function SetDevBoxValue(dropdownid, txtDevBoxid) \n{\n" +
			//"var dropdown = document.getElementById(dropdownid);\n" +
			//"var txtDevBox = document.getElementById(txtDevBoxid);\n" +
			//"txtDevBox.value = dropdown.value;\n}\n";

			//ClientScriptManager cs = Page.ClientScript;

			//cs.RegisterStartupScript(this.GetType(), "SetDevBoxValue", script);
		}

		#endregion

		#region Control Events

		protected void btnSearch_Click(object sender, EventArgs e)
		{
			SaveSettings();
			RaiseSearch();			
		}

		protected void btnReset_Click(object sender, EventArgs e)
		{
			SetDefaultValues();
			SaveSettings();
			RaiseSearch();
		}

		protected void chkbox_Click(object sender, EventArgs e)
		{
			if (sender != null)
			{
				try
				{
					var name = ((LinkButton)sender).CommandName;

					for (var i = 0; i < SearchParametersRepeaterCore.Items.Count; i++)
					{
						var hdnfield = (HiddenField)SearchParametersRepeaterCore.Items[i].FindControl("hdnfield");

						if (hdnfield == null) continue;

						if (hdnfield.Value.Equals(name))
						{
							SearchParametersRepeaterCore.Items[i].Visible = false;
						}
					}

					//ApplicationCommon.UpdateUserPreference(SettingCategory, name + "Visibility", "false");

					SaveSettings();
					RaiseSearch();
				}
				catch { }
			}
		}

		protected void dropdownlist_SelectedIndexChanged(object sender, EventArgs e)
		{
			var ddl = sender as DropDownList;

			RepeaterItem item = null;

			if (ddl.Parent is RepeaterItem)
			{
				item = ddl.Parent as RepeaterItem;
			}
			// TODO: for what purspose is this --
			else if (ddl.Parent.Parent.Parent.Parent is RepeaterItem)
			{
				item = ddl.Parent.Parent.Parent.Parent as RepeaterItem;
			}

			TextBox tb = null;

			// this will be removed once code is stable. Jira #2797
			try
			{
				tb = item.FindControl("txtDevBox") as TextBox;
			}
			catch
			{
				item = ddl.Parent.Parent as RepeaterItem;
				tb = item.FindControl("txtDevBox") as TextBox;
			}

			if (tb != null)
			{
				tb.Text = ddl.SelectedValue;
			}
		}

		#endregion

		#region Grid Events

		protected virtual void SearchParametersRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			// guard clause
			if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) return;

			var label				= (Label)e.Item.FindControl("label");

			var plcHoverLinkLabel	= (PlaceHolder)e.Item.FindControl("plcHoverLinkLabel");
			var plcControlHolder	= (PlaceHolder)e.Item.FindControl("plcControlHolder");

			var txtDevBox			= (TextBox)e.Item.FindControl("txtDevBox");

			var hdnField			= (HiddenField)e.Item.FindControl("hdnfield");

			// why?
			if (label == null && plcControlHolder == null) return;

			var name = hdnField.Value;

			// check if only 1 searchable column then we do not need Group By or Sub Group By (#3754)
			if ((name == "GroupBy" || name == "SubGroupBy") && !IsGroupByFeatureEnabled)
			{
				e.Item.Visible = false;
				return;
			}

			//get container row (parent)
			var containerRow = e.Item.FindControl("containerRow") as HtmlGenericControl;

			// DisplayColumn
			var isItemVisible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(name + "Visibility", SettingCategory);

            if ((name == "ApplicationId" || name == "Application")
                            && ApplicationCommon.ApplicationCache[SessionVariables.RequestProfile.ApplicationId].RenderApplicationFilter == 0)
            {
                //isItemVisible = false;
            }

			// set container row class to respective display column, only if item is visible
			if (containerRow != null && isItemVisible)
			{
				// get display column value
				var displayColumn = Convert.ToInt32(((DataRowView)(e.Item.DataItem)).Row["DisplayColumn"]);
				containerRow.Attributes.Add("class", "DisplayColumn" + displayColumn);
			}
			else if (containerRow != null && !isItemVisible) // if not visible then set to display none
			{
				containerRow.Attributes.Add("class", "hiddebByUP");
				containerRow.Attributes.CssStyle["display"] = "none";
			}

			var rowView = (DataRowView)e.Item.DataItem;

			#region Label

			var displayName = rowView[FieldConfigurationDataModel.DataColumns.FieldConfigurationDisplayName].ToString();
			var lblName = new Label();

			var labelDiv = new HtmlGenericControl("div");
			labelDiv.Attributes.Add("class", "col-sm-2 control-label");

			// link to make the control invisible has to be added only when item is visible
			if (isItemVisible)
			{
				AddVisibleOnHoverLink(name, containerRow, labelDiv);
			}

			lblName.Text = displayName + ":";
			labelDiv.Controls.Add(lblName);
			plcHoverLinkLabel.Controls.Add(labelDiv);
			
			#endregion

			var dt = FieldConfigurationUtility.GetFieldConfigurations(PrimaryEntity, SessionVariables.SearchControlColumnsModeId, hdnField.Value);

			// WHY : 1
			if (dt.Rows.Count >= 1)
			{
				var controlType = dt.Rows[0][FieldConfigurationDataModel.DataColumns.ControlType].ToString();

				var ctrlInput = new Control();

				if (controlType.Equals("TextBox", StringComparison.OrdinalIgnoreCase))
				{
					if (plcControlHolder != null)
					{
						ctrlInput = SetupTextBox(plcControlHolder, name, txtDevBox);
					}
				}
				else if (controlType.Equals("DropDownList", StringComparison.OrdinalIgnoreCase))
				{
					ctrlInput = SetupDropdownList(plcControlHolder, name, txtDevBox);
				}
				else if (controlType.Equals("MultiSelectListBox", StringComparison.OrdinalIgnoreCase))
				{
					ctrlInput = SetupMultiSelectListBox(plcControlHolder, txtDevBox, name);
				}
				else // shouldtype
				{
					ctrlInput = SetupDateRangeControl(e, plcControlHolder, txtDevBox, name);
				}

				lblName.AssociatedControlID = ctrlInput.ID;
			}

			var divDebug = new HtmlGenericControl("div");
			divDebug.Attributes.Add("class", "col-sm-2");
			divDebug.Controls.Add(hdnField);
			divDebug.Controls.Add(txtDevBox);
			plcControlHolder.Controls.Add(divDebug);

			//e.Item.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(name + "Visibility", SettingCategory);

			var isGridLines = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.SearchFilterGridLinesKey);

			if (isGridLines)
			{
				txtDevBox.CssClass = "searchTextBoxContainerVisible";
			}
			else
			{
				txtDevBox.CssClass = "searchTextBoxContainerInVisible";
			}
		}

		private TextBox SetupTextBox(PlaceHolder plcControlHolder, string name, TextBox txtDevBox)
		{
			var outerLink = new HtmlGenericControl("div");
			outerLink.Attributes["class"] = "col-sm-8";
			plcControlHolder.Controls.Add(outerLink);

			var txtBox = new TextBox();
			txtBox.ID = "txtbox";

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

				configString = LoadKendoComboBoxSources(name, txtBox, plcControlHolder);

				//data: ""{'primaryEntity':'" + PrimaryEntity + "','txtName':'" + name + "','AuditId':'" + SessionVariables.RequestProfile.AuditId + "'}",");"""""

				var l = @"$(document).ready(function ()
					    {
							$.ajax(
					        {
								""type"" : ""POST"",
								""url"" : ""{1}"",
								""contentType"" : ""application/json; charset=utf-8"",
								""dataType"" : ""json"",
								""success"": function (msg)
								{
									$(""#{0}"").kendoAutoComplete(
										{
												""dataSource"": msg.d
											,	""filter"": ""startswith""
										});
								}
					        });
					    });";

				//l = string.Format(l, txtBox.ClientID, @"http://localhost:53331/API/AutoComplete.asmx/GetAutoCompleteList\");

				if (string.IsNullOrEmpty(configString))
				{
					var str = new StringBuilder();
					str.AppendLine("$(document).ready(function ()");
					str.AppendLine("        {");
					str.AppendLine("$.ajax(");
					str.AppendLine("        {");
					str.AppendLine("type: \"POST\",");
					str.AppendLine("url: \"http://localhost:53331/API/AutoComplete.asmx/GetAutoCompleteList\",");
					str.AppendLine("data:\"{\'primaryEntity\':\'" + PrimaryEntity + "\',\'txtName\':\'" + name + "\',\'AuditId\':\'" + SessionVariables.RequestProfile.AuditId + "\'}\",");
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

			txtBox.Attributes.Add("onchange", string.Format("javascript:SetDevBoxValue('{0}','{1}')", plcControlHolder.FindControl("txtBox").ClientID , txtDevBox.ClientID));

			if (name == "ApplicationId")
			{
				TextBoxApplicationIdClientId = txtBox.ClientID;
			}

			return txtBox;
		}

		private void SetupControlsGrouping(Control plcControlHolder, string name)
		{
			if (name.Equals("GroupBy") || name.Equals("SubGroupBy"))
			{
				//if (SessionVariables.IsTesting)
				//	txtDevBox.Visible = true;
				//else
				//	txtDevBox.Visible = false;
			}

			// Add Direction Combos Dynamically, check flag whether the corresponding record exists in FC or not.
			if (name.Equals("GroupBy") && DoesGroupByDirectionExist)
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
			else if (name.Equals("SubGroupBy") && IsSubGroupByDirectionExists)
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

		private void AddVisibleOnHoverLink(string name, HtmlGenericControl containerRow, Control parentControl)
		{
			return;

			var outerLink = new HtmlGenericControl("span");

			outerLink.Attributes["class"] = "hoverLinkCheckBox context-menu-one";
			outerLink.InnerText = "[X]";
			outerLink.Attributes["name"] = name;
			outerLink.Attributes["category"] = SettingCategory;

			if (containerRow != null)
			{
				var click = string.Format("HideSearchControlWithRowId('{0}', '{1}', '{2}')"
					, containerRow.ClientID
					, name
					, SettingCategory
					);

				outerLink.Attributes["onclick"] = click;
			}

			var div = new HtmlGenericControl("span");
			div.Attributes.Add("id", "VisibleOnHoverLink");
			div.Controls.Add(outerLink);

			parentControl.Controls.Add(div);
		}

		private DateRangeControl SetupDateRangeControl(RepeaterItemEventArgs e, PlaceHolder plcControlHolder, TextBox txtDevBox, string name)
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
				oDateRange = (DateRangeControl) e.Item.FindControl("oDateRange");
			}

			txtDevBox.Visible = false;

			oDateRange.SettingCategory = PrimaryEntity + name + "DateRangeControl";
			oDateRange.Key = e.Item.ItemIndex.ToString();

			var funccall = "Fillup" + oDateRange.GetKey() + "();";
			oDateRange.DateRangeDropDown.Attributes.Add("onchange", funccall);
			oDateRange.HideLabel();

			return oDateRange;
		}

		private ListBox SetupMultiSelectListBox(PlaceHolder plcControlHolder, TextBox txtDevBox, string name)
		{
			var listbox = new ListBox();

			if (plcControlHolder != null)
			{
				var lstBox = new ListBox();

				lstBox.SelectionMode = ListSelectionMode.Multiple;
				lstBox.ID = "listbox";
				lstBox.AppendDataBoundItems = true;

				plcControlHolder.Controls.Add(lstBox);

				listbox = lstBox;
			}

			txtDevBox.Visible = true;

			LoadListBoxSources(name, listbox);

			return listbox;
		}

		private DropDownList SetupDropdownList(PlaceHolder plcControlHolder, string name, TextBox txtDevBox)
		{
			var dropDown = new DropDownList();

			if (plcControlHolder != null)
			{
				var outerLink = new HtmlGenericControl("div");
				outerLink.Attributes["class"] = "col-sm-8";
				plcControlHolder.Controls.Add(outerLink);

				dropDown.SelectedIndexChanged += dropdownlist_SelectedIndexChanged;
				dropDown.CssClass = "form-control";
				dropDown.ID = "dropdownlist";
				outerLink.Controls.Add(dropDown);

				#region MyRegion
				
				// Add Direction Combos Dynamically, check flag whether the corresponding record exists in FC or not.
				//if (name.Equals("GroupBy") && DoesGroupByDirectionExist)
				//{
				//	plcControlHolder.Controls.Add(new Label() { Text = "&nbsp;&nbsp;" });

				//	var dropDownOrderBy = new DropDownList();
				//	dropDownOrderBy.ID = "dropdownlistOrderBy";
				//	dropDownOrderBy.Items.Add("ASC");
				//	dropDownOrderBy.Items.Add("DESC");
				//	plcControlHolder.Controls.Add(dropDownOrderBy);
				//}
				//else if (name.Equals("SubGroupBy") && IsSubGroupByDirectionExists)
				//{
				//	plcControlHolder.Controls.Add(new Label() { Text = "&nbsp;&nbsp;" });

				//	var dropDownOrderBy = new DropDownList();
				//	dropDownOrderBy.ID = "dropdownlistOrderBy";
				//	dropDownOrderBy.Items.Add("ASC");
				//	dropDownOrderBy.Items.Add("DESC");
				//	plcControlHolder.Controls.Add(dropDownOrderBy);
				//} 

				#endregion
			}
			else
			{
				//
			}

			LoadDropDownListSources(name, dropDown);

			if (!name.Equals("GroupBy") && !name.Contains("GroupBy"))
			{
				if (!dropDown.Items.Contains(new ListItem("All", "-1")))
					dropDown.Items.Insert(0, new ListItem("All", "-1"));
			}

			#region MyRegion
			//else
			//{
			//	dropDown.DataSource = LstGroupByItemsList;
			//	dropDown.DataTextField = "Key";
			//	dropDown.DataValueField = "Key";
			//	dropDown.DataBind();

			//	dropDown.Items.Insert(0, new ListItem("None", "-1"));
			//}
			#endregion

			dropDown.SelectedIndex = 0;

			//if (SessionVariables.IsTesting)
			//{
			txtDevBox.Visible = true;
			txtDevBox.Text = dropDown.SelectedValue;
			//}

			return dropDown;
		}

		#endregion

	}

}
