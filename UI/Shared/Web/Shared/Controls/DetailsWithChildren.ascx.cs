using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using DataModel.Framework.Core;
using Framework.Components.Core;
using Framework.Components.DataAccess;
using Framework.Components.UserPreference;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Collections.Specialized;
using System.Collections;
using System.Drawing;

namespace Shared.UI.Web.Controls
{
    public partial class DetailsWithChildrenControl : BaseControl
	{
		private enum Command
		{
			Delete = 0,
			Details = 1,
			Update = 2
		}

		private const string VIEW_STATE_KEY_DELETE = "Delete";
        private const string VIEW_STATE_KEY_UPDATE = "Update";
        private const string VIEW_STATE_KEY_DETAIL = "Details";
		
        public delegate DataTable GetDataDelegate(string key);
		public delegate DataTable GetSubGroupDataDelegate(string groupKey, string subGroupKey);
        public delegate string[] GetColumnDelegate();

		private GetDataDelegate _getData;
		private GetSubGroupDataDelegate _getSubGroupData;
        private GetColumnDelegate _getColumnDelegate;

        private DataTable DtGlobal = new DataTable();
        private DataTable GridFormatDt = new DataTable();
        private double TotalGridColumnWidth = 0;
		private int GridCharLength = 4;
        private GridPagiation oGridPagiation;
        private bool SkipGridReload = false;
        //private bool DisableDelete = false;
        private ArrayList FkColumnIds;
		private ArrayList DescColumnIds;
        private const int CustomPageSize = 250;

        #region properties
		
		public int DefaultRowCount
		{
			get
			{
                if (string.IsNullOrEmpty(SettingCategory))
                    SettingCategory = "General";
				return PreferenceUtility.GetUserPreferenceByKeyAsInt(ApplicationCommon.DefaultRowCountKey, SettingCategory);
			}
		}

        public bool IsFCModeVisible
        {
            get
            {
				if (ViewState["IsFCModeVisible"] != null)
                {
					return Convert.ToBoolean(ViewState["IsFCModeVisible"]);
                }
                return true;
            }
            set
            {
				ViewState["IsFCModeVisible"] = value;
            }
        }

        public GridView MainGridViewList
        {
            get { return GridView; }
        }

        private string Prefix
        {
            get
            {
                if (ViewState["Prefix"] == null)
                {
                    ViewState["Prefix"] = "_";
                }

                return (string)ViewState["Prefix"];
            }
            set
            {
                ViewState["Prefix"] = value + "_";
            }
        }

        public string FieldConfigurationMode
        {
            get
            {
				if (ViewState["FieldConfigurationMode"] == null)
				{
					ViewState["FieldConfigurationMode"] = string.Empty;
				}

				return (string)ViewState["FieldConfigurationMode"];
			}
			set
			{
				ViewState["FieldConfigurationMode"] = value;
			}
        }

        public DropDownList FCModeDropDownList
        {
            get { return ddlFieldConfigurationMode; }
        }

        public int? CurrentPageIndex
        {
            get
            {
                if (ViewState[Prefix + "CurrentPageIndex"] == null)
                {
                    return null;
                }
                else
                {
                    return Convert.ToInt32(ViewState[Prefix + "CurrentPageIndex"]);
                }
            }
            set
            {
                ViewState[Prefix + "CurrentPageIndex"] = value;
            }
        }

        private string PrimaryKey
        {
            get
            {
                if (ViewState["PrimaryKeyId"] == null)
                {
                    ViewState["PrimaryKeyId"] = true;
                }

                return ViewState["PrimaryKeyId"].ToString();
            }
            set
            {
                ViewState["PrimaryKeyId"] = value;
            }
        }

		private string SubGroupKeyName
		{
			get
			{
				if (ViewState["SubGroupKeyName"] == null)
				{
					ViewState["SubGroupKeyName"] = true;
				}

				return ViewState["SubGroupKeyName"].ToString();
			}
			set
			{
				ViewState["SubGroupKeyName"] = value;
			}
		}

		private string SubGroupKey
		{
			get
			{
				if (ViewState["SubGroupKeyId"] == null)
				{
					ViewState["SubGroupKeyId"] = true;
				}

				return ViewState["SubGroupKeyId"].ToString();
			}
			set
			{
				ViewState["SubGroupKeyId"] = value;
			}
		}

        public bool HideData { get; set; }

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

		private Color GridActionBarForeColor
		{
			get
			{
				if (ViewState["GridActionBarForeColor"] == null)
				{
					var foreColor = PreferenceUtility.GetUserPreferenceByKey(ApplicationCommon.ListHeaderForeColor);
					ViewState["GridActionBarForeColor"] = System.Drawing.ColorTranslator.FromHtml("#" + foreColor);
				}
				return (Color)ViewState["GridActionBarForeColor"];
			}
			set
			{
				ViewState["GridActionBarForeColor"] = value;
			}
		}
        #endregion properties

		#region Constructor

		public DetailsWithChildrenControl()
        {
            HideData = false;
		}
		
		#endregion

		#region Methods

		private StringCollection GetSelectedRecordIDs()
		{
			var currentPage = GridView.PageIndex;
			var currentPageSize = GridView.PageSize;
			//var totalNumOfRows = GridView.Rows.Count;
			var floor = (currentPage * currentPageSize);
			var ceil = ((currentPage * currentPageSize) + currentPageSize) - 1;

			var sc = new StringCollection();
			var id = String.Empty;
			var dt1 = new DataTable();

			if (!string.IsNullOrEmpty(SessionVariables.SortExpression) && !string.IsNullOrEmpty(SessionVariables.SortDirection))
			{
				SkipGridReload = true;
				dt1 = SortGridView(SessionVariables.SortExpression, SessionVariables.SortDirection);
				SkipGridReload = false;
			}
			else
			{
				dt1 = DtGlobal;
			}

			if (ceil > dt1.Rows.Count)
				ceil = dt1.Rows.Count - 1;

			//loop the GridView Rows
			var j = 0;

			for (var i = floor; i <= ceil; i++)
			{
				if (j < GridView.Rows.Count)
				{
					var cb = (CheckBox)GridView.Rows[j].Cells[0].FindControl("CheckBox1"); //find the CheckBox

					if (cb != null)
					{
						if (cb.Checked)
						{
							id = dt1.Rows[i][0].ToString(); // get the id of the field to be deleted
							sc.Add(id); // add the id to be deleted in the StringCollection
						}
					}
					j++;
				}
			}

			return sc;
		}

		private void RetrieveRecords(StringCollection sc, Command cmd)
		{
			try
			{
				ShowSelectedRecords(sc, cmd);
				//DataTable dt1 = new DataTable();
				//if (_getData != null)
				//{
				//    dt1 = _getData(PrimaryKeyId);
				//    GridView.DataSource = dt1;
				//    GridView.DataBind();
				//}
				//oGridPagiation = new GridPagiation();
				//oGridPagiation.Setup(plcPaging, litPagingSummary, lblCacheStatus, dt1, GridView, Page, SettingCategory);
				//oGridPagiation.Changed += oGridPagiation_Changed;
				//oGridPagiation.ManagePaging(dt1);
			}
			catch (Exception ex)
			{
				var msg = "Deletion Error:";
				msg += ex.Message;
				throw new Exception(msg);
			}
		}

		private void ShowSelectedRecords(StringCollection sc, Command cmd)
		{
			var _tableFolder = Convert.ToString(ViewState["TableFolder"]);
			var _tableName = Convert.ToString(ViewState["TableName"]);
			var redirecturl = "";

			var _tablePath = String.Empty;

			if (string.IsNullOrEmpty(_tableFolder.Trim()))
			{
				_tablePath = "~/" + _tableName;
			}
			else
			{
				_tablePath = "~/" + _tableFolder + "/" + _tableName;
			}

			if (sc.Count > 1)
			{
				if (cmd.Equals(Command.Delete))
				{
					redirecturl = _tablePath + "/Delete.aspx?SuperKey=";
				}
				else if (cmd.Equals(Command.Details))
				{
					redirecturl = _tablePath + "/Details.aspx?SuperKey=";
				}
				else
				{
					redirecturl = _tablePath + "/Update.aspx?SuperKey=";
				}

				var superKeyId = 0;
				var sData = new SystemEntityTypeDataModel();
				sData.EntityName = Convert.ToString(ViewState["TableName"]);
				var sDt = SystemEntityTypeDataManager.GetDetails(sData, SessionVariables.RequestProfile);
				if (sDt != null)
				{
                    var systemEntityTypeId = sDt.SystemEntityTypeId.Value;
					superKeyId = ApplicationCommon.GenerateSuperKey(sc, systemEntityTypeId);
				}

				redirecturl += superKeyId;
			}
			else if (sc.Count == 1)
			{
				if (cmd.Equals(Command.Delete))
				{
					redirecturl = _tablePath + "/Delete.aspx";
				}
				else if (cmd.Equals(Command.Details))
				{
					redirecturl = _tablePath + "/Details.aspx";
				}
				else
				{
					redirecturl = _tablePath + "/Update.aspx";
				}
				redirecturl += "?SetId=" + sc[0];
			}

			Response.Redirect(redirecturl, false);
		}

		private void DisableMultipleDelete(string tablename)
		{
			switch (tablename)
			{
				case "AuditAction":
				//case "TaskFormulation":
				case "Demo":
					{
						GridView.Columns[0].Visible = false;
						break;
					}
			}
		}

        private List<FieldConfigurationModeDataModel> GetApplicableModesList(int systemEntityTypeId)
        {
            var data = new FieldConfigurationDataModel();
            data.SystemEntityTypeId = systemEntityTypeId;

			var columns = FieldConfigurationDataManager.GetEntityDetails(data, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);

            var modes = FieldConfigurationModeDataManager.GetList(SessionVariables.RequestProfile);

            var validModes = new List<FieldConfigurationModeDataModel>();

            for (var j = 0; j < modes.Count; j++)
            {
                for (var i = 0; i < columns.Count; i++)
                {
                    if (
                            columns[i].FieldConfigurationModeId.Value == modes[j].FieldConfigurationModeId.Value
                        )
                    {
                        if (!validModes.Where(x => x.FieldConfigurationModeId == modes[j].FieldConfigurationModeId.Value).Any())
                        {
                            validModes.Add(modes[j]);
                        }
                    }
                }
            }

            validModes = validModes.OrderBy(x => x.SortOrder).ToList();
            return validModes;
        }

        public void HideControls()
        {
            RadioButtonList1.Visible = false;
            ButtonContainer.Visible = false;
        }

        private int SetUpDropDownAEFLMode()
        {
            var systemEntityTypeId = (int)Enum.Parse(typeof(SystemEntity), ViewState["TableName"].ToString());
            var lstModes = GetApplicableModesList(systemEntityTypeId);
            var modeSelected = GetSessionInstanceFCMode(ViewState["TableName"].ToString());

            if (lstModes.Count > 0)
            {
                //if (ddlFieldConfigurationMode.DataSource == null)
                //{
                    ddlFieldConfigurationMode.Items.Clear();
                    ddlFieldConfigurationMode.DataSource = lstModes;
                    ddlFieldConfigurationMode.DataTextField = "Name";
                    ddlFieldConfigurationMode.DataValueField = "FieldConfigurationModeId";
                   ddlFieldConfigurationMode.ClearSelection();
                    ddlFieldConfigurationMode.DataBind();
                   ddlFieldConfigurationMode.SelectedValue = modeSelected.ToString(CultureInfo.InvariantCulture);
                //}
            }
            else
            {
                ddlFieldConfigurationMode.Visible = false;
            }

			return modeSelected;
        }

        protected void ddlFieldConfigurationMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessionVariables.FieldConfigurationMode = Convert.ToInt32(ddlFieldConfigurationMode.SelectedValue);

            while (GridView.Columns.Count > 1)
            {
                if (!GridView.Columns[GridView.Columns.Count - 1].HeaderText.Equals("All"))
                    GridView.Columns.RemoveAt(GridView.Columns.Count - 1);

            }

            SaveSessionInstanceFCMode(Convert.ToInt32(ddlFieldConfigurationMode.SelectedValue));
            //Setup(ViewState["TableName"].ToString(), ViewState["TableFolder"].ToString(), ViewState["PrimaryKey"].ToString(), true, _getData, _getColumnDelegate);
            //Setup(Convert.ToString(ViewState["TableName"]), Convert.ToString(ViewState["TableFolder"]), Convert.ToString(ViewState["PrimaryKey"]),
            //    PrimaryKeyId, true, _getData, _getColumnDelegate);
            SortGridView(String.Empty, SortDirection.Ascending.ToString());
            AddCheckBox();

        }

        private void AddCheckBox()
        {
            var tcCheckCell = new TableCell();
            var chkCheckBox = new CheckBox();

            if (GridView.HeaderRow.Cells[0].FindControl("chkSelectAll") == null)
            {
                tcCheckCell = new TableCell();
                chkCheckBox = new CheckBox();
                tcCheckCell.Controls.Add(chkCheckBox);
                chkCheckBox.Text = "All";
                chkCheckBox.ID = "chkSelectAll";
                chkCheckBox.AutoPostBack = true;
                chkCheckBox.Attributes.Add("OnSelectedIndexChanged", "GridView_SelectedIndexChanged");
                GridView.HeaderRow.Cells[0].Controls.Add(chkCheckBox);
            }
            foreach (GridViewRow objRow in GridView.Rows)
            {
                var chk = (CheckBox)objRow.Cells[0].FindControl("CheckBox1");
                if (chk == null)
                {
                    chkCheckBox = new CheckBox();
                    chkCheckBox.ID = "CheckBox1";
                    tcCheckCell.Controls.Add(chkCheckBox);
                    objRow.Cells[0].Controls.Add(chkCheckBox);
                }
            }
        }

        private void SaveSessionInstanceFCMode(int selectedmode)
        {
            if (Session[ViewState["TableName"] + "SelectedMode"] == null)
                Session.Add(ViewState["TableName"] + "SelectedMode", selectedmode);
            else
                Session[ViewState["TableName"] + "SelectedMode"] = selectedmode;
        }

        private int GetSessionInstanceFCMode(string currententity)
        {
            if (!string.IsNullOrEmpty(FieldConfigurationMode))
				return Convert.ToInt32(FieldConfigurationMode);
            else
                return -1;
        }

		//private string CoreTableName
		//{
		//	get { return Convert.ToString(ViewState["TableName"]); }
		//}

		private DataTable GetGridFormatSettings()
		{
			var entityId = GetSystemEntityTypeId();

			//var auditId = SessionVariables.RequestProfile.AuditId;
			var dtGridFormat                      = new FieldConfigurationDataModel();
			dtGridFormat.SystemEntityTypeId       = entityId;
			dtGridFormat.FieldConfigurationModeId = GetSessionInstanceFCMode(ViewState["TableName"].ToString());

			//var gridFormatdt = FieldConfigurationDataManager.Search(dtGridFormat, auditId);
			GridFormatDt = FieldConfigurationUtility.GetFieldConfigurations(GetSystemEntityTypeId(), dtGridFormat.FieldConfigurationModeId, string.Empty);

			return GridFormatDt;
		}

        private int GetSystemEntityTypeId()
        {
            var entityName = Convert.ToString(ViewState["TableName"]);
            var entityId = (int)Enum.Parse(typeof(SystemEntity), entityName);
            return entityId;
		}

		public void SetVisibilityOfListFeatures(bool Isbuttonpanelvisible, bool isPagingPanelVisible, bool Issortingoptionsvisible)
		{
			ButtonContainer.Visible = Isbuttonpanelvisible;
			PagingContainer.Visible = isPagingPanelVisible;
			RadioButtonList1.Visible = Issortingoptionsvisible;
			ButtonContainer.Attributes.Add("display", "none");
			PagingContainer.Attributes.Add("display", "none");
		}

		private void oGridPagiation_Changed(object sender, EventArgs e)
		{
			SortGridView(String.Empty, SortDirection.Ascending.ToString());
		}

		// what is this
		public void SetSession(string sessionUpdated)
		{
			ViewState["SessionUpdated"] = sessionUpdated;
		}

		public void SetUserPreferenceCategory()
		{
			if (string.IsNullOrEmpty(UserPreferenceCategory) || UserPreferenceCategoryId != 0) return;

			var data = new UserPreferenceCategoryDataModel();
			data.Name = UserPreferenceCategory;
			var obj = UserPreferenceCategoryDataManager.GetDetails(data, SessionVariables.RequestProfile);

			if (obj != null)
			{
				UserPreferenceCategoryId = obj.UserPreferenceCategoryId.Value;
			}
		}

		// string searchLastName, string searchFirstName
		public void ShowData(bool dataHide, bool search)
		{
			CustomizedSearch = search;
			HideData = dataHide;

			// sort by default ...
			SortGridView(String.Empty, SortDirection.Ascending.ToString());
		}

		// string searchLastName, string searchFirstName
		public void ShowData(bool dataHide, bool search, bool isLoadPreferences, string userPreferenceCategory)
		{
			CustomizedSearch = search;
			HideData = dataHide;

			// sort by default ...
			SortGridView(String.Empty, SortDirection.Ascending.ToString());

			if (isLoadPreferences)
			{
				var isButtonPanelEnabled = PreferenceUtility.GetApplicationUserPreferenceByKeyAsBoolean(ApplicationCommon.DetailsButtonPanelVisible, userPreferenceCategory);
				if (!isButtonPanelEnabled)
				{
					ButtonContainer.Visible = false;
					GridView.Columns[0].Visible = false;
				}

				var isDetailsAEFLModeEnabled = PreferenceUtility.GetApplicationUserPreferenceByKeyAsBoolean(ApplicationCommon.DetailsAEFLModeEnabled, userPreferenceCategory);
				if (!isDetailsAEFLModeEnabled)
				{
					AEFLModeContainer.Visible = false;
				}

				var isPaging = PreferenceUtility.GetApplicationUserPreferenceByKeyAsBoolean(ApplicationCommon.DetailsPagingEnabled, userPreferenceCategory);
				if (!isPaging)
				{
					GridView.AllowPaging = false;
					GridView.DataBind();
					PagingContainer.Visible = false;
				}
			}
			else
			{
				ButtonContainer.Visible = true;
				GridView.Columns[0].Visible = true;

				AEFLModeContainer.Visible = true;

				PagingContainer.Visible = true;
				GridView.AllowPaging = true;
				GridView.DataBind();
				GridView.PageSize = CustomPageSize;
				oGridPagiation = new GridPagiation();
				oGridPagiation.Setup(plcPaging, litPagingSummary, lblCacheStatus, DtGlobal, GridView, Page, SettingCategory);
				oGridPagiation.Changed += oGridPagiation_Changed;

				if (CurrentPageIndex != null)
					oGridPagiation.PageIndexInSession = CurrentPageIndex.Value;

				oGridPagiation.ManagePaging(DtGlobal);
			}
		}

		public void ApplyAllTabSettings(string userPreferenceCategory)
		{
			var isButtonPanelEnabled = PreferenceUtility.GetApplicationUserPreferenceByKeyAsBoolean(ApplicationCommon.DetailsButtonPanelVisible, userPreferenceCategory);
			if (!isButtonPanelEnabled)
			{
				ButtonContainer.Visible = false;
				GridView.Columns[0].Visible = false;
			}

			var isDetailsAEFLModeEnabled = PreferenceUtility.GetApplicationUserPreferenceByKeyAsBoolean(ApplicationCommon.DetailsAEFLModeEnabled, userPreferenceCategory);
			if (!isDetailsAEFLModeEnabled)
			{
				AEFLModeContainer.Visible = false;
			}

			var isPaging = PreferenceUtility.GetApplicationUserPreferenceByKeyAsBoolean(ApplicationCommon.DetailsPagingEnabled, userPreferenceCategory);
			if (!isPaging)
			{
				GridView.AllowPaging = false;
				GridView.DataBind();
				PagingContainer.Visible = false;
			}
		}

		// reName to better 
		private void Sample(DataTable dt, string tableName, string tableFolder, string primaryKeyId, bool hideData, bool isTesting)
		{
			if (IsPostBack)
			{
				if (GridView.Columns.Count > 1)
				{
					return;
				}
			}

            GridFormatDt = GetGridFormatSettings();

			// boolean values set for the fields and procedures in the GridView            
			var procedureHide = hideData;
			//var fieldHide = false;

			// Create a string array for DataNavigationUrlFields            
			var str = new string[2];
			//str[0] = primaryKeyId;
			str[0] = dt.Columns[0].ColumnName;
			str[1] = primaryKeyId;

			// Create rest of columns based on what user has access to ...            
			//var key = "Default";
			var disableLink = hideData;
			var validColumns = _getColumnDelegate();

			//var idColumn = new DataColumn(str[0]);
			var idColumn = new DataColumn(str[1]);

			if (idColumn != null)
			{
				// dynamically add hyperlink fields from the data table
				if (!idColumn.ColumnName.Equals("ReleaseLogId", StringComparison.OrdinalIgnoreCase) && !idColumn.ColumnName.Equals("-1", StringComparison.OrdinalIgnoreCase))
                    SetHyperLink(idColumn, str, tableName, tableFolder, disableLink, GridFormatDt);
			}

			foreach (DataRow row in GridFormatDt.Rows)
			{
				//Considering all the columns have their width fixed from the Database
				TotalGridColumnWidth += Double.Parse(row[FieldConfigurationDataModel.DataColumns.Width].ToString());
			}

			foreach (var t in validColumns)
			{
				// get the column from the data table
				var dataColumn = dt.Columns[t];

				if (dataColumn != null)
				{
					// dynamically add hyperlink fields from the data table
                    SetHyperLink(dataColumn, str, tableName, tableFolder, disableLink, GridFormatDt);
				}
			}



			if (SessionVariables.ApplicationUserRoles == null)
			{
				ApplicationCommon.SetApplicationUserRoles();
			}

			var roles = SessionVariables.ApplicationUserRoles;
			//var role = roles.Find(item => item.ApplicationRole == "System Coordinator");

			//if (role != null)
			//{
			//	DisableDelete = false;
			//}
			//else
			//{
			//	DisableDelete = true;
			//}

			// based on another bool parameters indicating if action buttons should show            
			// add action button / links            
			if (IsUpdateColumn)
			{
				var userDetailVisibility = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.GridDetailLinkVisibleKey, UserPreferenceCategory);
				if (userDetailVisibility)
				{
					var userAction = PreferenceUtility.GetUserPreferenceByKey(ApplicationCommon.GridDefaultClickActionKey);
					//var userAction = ApplicationCommon.GetDefaultActionLink();
					if (userAction == "update")
						AddProcedures(str, VIEW_STATE_KEY_DETAIL, tableName, tableFolder, procedureHide);
					else
						AddProcedures(str, VIEW_STATE_KEY_UPDATE, tableName, tableFolder, procedureHide);
				}
			}

			if (IsDeleteColumn)
			{
				var userDeleteVisibility = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.GridDeleteLinkVisibleKey, UserPreferenceCategory);
				if (userDeleteVisibility)
				{
					AddProcedures(str, VIEW_STATE_KEY_DELETE, tableName, tableFolder, procedureHide);
				}
			}
		}

		// reName to better 
		private void SetHyperLink(DataColumn dataColumn, string[] str, string entity, string tableFolder, bool enableLInk, DataTable  dtGridFormat)
		{
			var hypField = new HyperLinkField();
			//var flagGridFormat = false;

			var strFormatting = string.Empty;

			//int totalColumnWidth = 0;

			var headerWidth = "100";
			var itemAlignment = "Center";
			var headerTextValue = "";
			//ConsoleColor headerColor	;	
			var columnsExists = false;

            var rows = dtGridFormat.Select(FieldConfigurationDataModel.DataColumns.Name + " = '" + dataColumn.ColumnName + "'");
			if (rows.Length > 0)
			{
				var row = rows[0];

				var controlType = row[FieldConfigurationDataModel.DataColumns.ControlType].ToString();

				if (controlType == "Image")
				{
					//call a method that will add image column to grid
					AddImageColumn(dataColumn, str, entity, enableLInk, rows[0]);

					// return from method as we do not need to process this column further
					return;
				}

				var headerTextName = (string)row[FieldConfigurationDataModel.DataColumns.Name].ToString();

				//headerTextValue = row[FieldConfigurationDataModel.DataColumns.Value].
				//	ToString();

				headerTextValue = (string)row[FieldConfigurationDataModel.DataColumns.FieldConfigurationDisplayName].ToString();
				
				//Set the remaining width of the column to Description, Set 100 as width for Checkbox column
				if (headerTextName == "Description" && (1000 - (TotalGridColumnWidth + 100)) > 0)
				{
					headerWidth = (1000 - (TotalGridColumnWidth + 100)).ToString();
				}
				else
				{
					headerWidth = row[FieldConfigurationDataModel.DataColumns.Width].ToString();
				}

				//headerWidth = row[FieldConfigurationDataModel.DataColumns.Width].ToString();

				itemAlignment = row[FieldConfigurationDataModel.DataColumns.HorizontalAlignment].ToString();

				//headerColor		= row[FieldConfiguration.DataColumns.Formatting].
				//ToString();
				strFormatting = Convert.ToString(row[FieldConfigurationDataModel.DataColumns.Formatting]);

				//flagGridFormat = true;

				// check visibility of column, if gridviewpriority = -1 then visible false
				var gridViewPriority = Convert.ToInt32(row[FieldConfigurationDataModel.DataColumns.GridViewPriority]);
					
				if (gridViewPriority < 0)
				{
					hypField.Visible = false;
				}

				//hypField.HeaderText = headerTextValue;
			}

			hypField.HeaderText = dataColumn.ColumnName;

			if (!headerWidth.Contains('-'))
			{
				hypField.HeaderStyle.Width = Unit.Parse(headerWidth);
			}
			else
			{
				hypField.HeaderStyle.Width = '*';
			}

			//if (!headerWidth.Contains('-'))
			//	hypField.ItemStyle.Width = Unit.Parse(headerWidth);
			//else
			//	hypField.ItemStyle.Width = '*';

			//hypField.ItemStyle.HorizontalAlign = ListHelperAlignment.GetHeaderAlignment(itemAlignment);
			//hypField.HeaderStyle.CssClass = "align-center";
			//hypField.HeaderStyle.BackColor = System.Drawing.Color.Black;

			if (!string.IsNullOrEmpty(headerTextValue))
			{
				hypField.HeaderText = headerTextValue;
			}
			hypField.ItemStyle.HorizontalAlign	= ListHelperAlignment.GetHeaderAlignment(itemAlignment);
			//hypField.HeaderStyle.HorizontalAlign = ListHelperAlignment.GetHeaderAlignment("Center");
			hypField.HeaderStyle.CssClass		= "align-center";
			hypField.HeaderStyle.ForeColor		= GridActionBarForeColor;
			hypField.HeaderStyle.BorderColor	= Color.Black;
			hypField.HeaderStyle.BorderWidth	= 1;
			hypField.DataTextField				= dataColumn.ColumnName;

			if (!string.IsNullOrEmpty(strFormatting) && strFormatting.Trim() != "-" && strFormatting.ToUpper() != "NA")
			{
				hypField.DataTextFormatString = strFormatting;
			}

			if (!enableLInk)
			{
				hypField.SortExpression = dataColumn.ColumnName;
				//hypField.HeaderText += "<img src='"+imgArrowDown+"' alt='' />";
				hypField.DataNavigateUrlFields = str;

				var userClickAction = PreferenceUtility.GetUserPreferenceByKey(ApplicationCommon.GridDefaultClickActionKey);
				userClickAction = userClickAction.ToLower();
				var actionPage = String.Empty;

				if (userClickAction == "detail")
				{
					actionPage = "Details";
				}
				else if (userClickAction == "update")
				{
					actionPage = "Update";
				}
				else
				{
					actionPage = "Details";
				}

				var userMode = String.Empty;

				if (!string.IsNullOrEmpty(Request.QueryString["user"]))
				{
					userMode = "&user=" + Request.QueryString["user"];
				}

				hypField.DataNavigateUrlFormatString = Page.GetRouteUrl(entity + "EntityRoute", new { Action = actionPage }) + "/{0}" + userMode;
			}

			//GridView.Columns.Add(hypField);
			foreach (DataControlField column in GridView.Columns)
			{
				var headertext = column.HeaderText;

				if (headertext.EndsWith("&nbsp; &uarr;"))
					headertext = headertext.Replace("&nbsp; &uarr;", string.Empty);
				else if (headertext.EndsWith("&nbsp; &darr;"))
					headertext = headertext.Replace("&nbsp; &darr;", string.Empty);

				if (headertext.Equals(hypField.HeaderText))
				{
					columnsExists = true;
					break;
				}
			}

			if (!columnsExists)
			{
				GridView.Columns.Add(hypField);
			}
		}

		private void AddImageColumn(DataColumn dataColumn, string[] str, string entity, bool enableLink, DataRow formatRow)
		{

			var entityPath = ApplicationCommon.GetControlPath(entity, ControlType.ImageHandler);

			var imgField = new ImageField();
			imgField.DataImageUrlField = entity + "Id";
			imgField.DataImageUrlFormatString = entityPath + "ShowImage.aspx?ImageId={0}";
			imgField.ControlStyle.Width = 50;
			imgField.ControlStyle.Height = 50;

			imgField.HeaderStyle.BorderColor = Color.Black;
			imgField.HeaderStyle.BorderWidth = 1;
			imgField.ItemStyle.HorizontalAlign = ListHelperAlignment.GetHeaderAlignment(formatRow[FieldConfigurationDataModel.DataColumns.HorizontalAlignment].ToString());
			imgField.HeaderText = formatRow[FieldConfigurationDataModel.DataColumns.FieldConfigurationDisplayName].ToString();

			GridView.Columns.Add(imgField);
		}

		private void AddProcedures(string[] str, string procedureName, string tableName, string tableFolder, bool procedureHide)
		{
			var hypUpdateField = new HyperLinkField { HeaderText = procedureName };

			hypUpdateField.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
			hypUpdateField.Text = procedureName;
			hypUpdateField.DataNavigateUrlFields = str;

			if (tableFolder == " ")
			{
				hypUpdateField.DataNavigateUrlFormatString = "~/" + tableName + "/" + procedureName + ".aspx?SetId={0}";
			}
			else
			{
				hypUpdateField.DataNavigateUrlFormatString = "~/" + tableFolder + "/" + tableName + "/" + procedureName + ".aspx?SetId={0}";
			}

			hypUpdateField.Visible = !procedureHide;

			GridView.Columns.Add(hypUpdateField);

			ViewState[procedureName] = GridView.Columns.Count - 1;

			// To put the Update and Delete buttons at the right Index during RowCreated Event han
			//ViewState[procedureName] = dt.Columns.IndexOf(procedureName);            
		}

		private static DataTable SortDataTable(DataTable dt, string sort, string sortdirection)
		{
			var newDT = dt.Clone();
			var rowCount = dt.Rows.Count;

			var sortstring = sort + " " + sortdirection;
			var foundRows = dt.Select(null, sortstring);
			// Sort with Column name 
			for (var i = 0; i < rowCount; i++)
			{
				var arr = new object[dt.Columns.Count];
				for (var j = 0; j < dt.Columns.Count; j++)
				{
					arr[j] = foundRows[i][j];
				}
				var dataRow = newDT.NewRow();
				dataRow.ItemArray = arr;
				newDT.Rows.Add(dataRow);
			}

			//clear the incoming dt 
			dt.Rows.Clear();

			for (var i = 0; i < newDT.Rows.Count; i++)
			{
				var arr = new object[dt.Columns.Count];

				for (var j = 0; j < dt.Columns.Count; j++)
				{
					arr[j] = newDT.Rows[i][j];
				}

				var dataRow = dt.NewRow();
				dataRow.ItemArray = arr;
				dt.Rows.Add(dataRow);
			}

			return dt;
		}

		private DataTable SortGridView(string sortExpression, string sortDirection)
		{
			var tableFolder = ViewState["TableFolder"].ToString();
			var tableName = ViewState["TableName"].ToString();
			var primaryKeyId = ViewState["PrimaryKey"].ToString();

			var customizedSearch	= CustomizedSearch;
			var currentpage			= GridView.PageIndex;
			var currentpagesize		= GridView.PageSize;
			//var totalnumofrows = GridView.Rows.Count;
			var dtlocal = new DataTable();
			var sortedTable = new DataTable();
			var dtlocal2 = new DataTable();

			var floor = (currentpage * currentpagesize);
			var ceil = ((currentpage * currentpagesize) + currentpagesize) - 1;

			if (ceil > DtGlobal.Rows.Count)
				ceil = DtGlobal.Rows.Count - 1;

			if (customizedSearch && ViewState["SessionUpdated"] != null)
			{
				if (_getSubGroupData != null)
				{
					DtGlobal = _getSubGroupData(PrimaryKey, SubGroupKey);
				}
				else
				{
					DtGlobal = _getData(PrimaryKey);
				}
			}
			else
			{
				DtGlobal = GetDataSet(tableName);
			}

			//Extract Sort Info from Session

			if (!string.IsNullOrEmpty(SessionVariables.SortExpression))
			{
				if (DtGlobal.Columns.Contains(SessionVariables.SortExpression))
					sortExpression = SessionVariables.SortExpression;

			}
			if (!string.IsNullOrEmpty(SessionVariables.SortDirection))
			{
				sortDirection = SessionVariables.SortDirection;
			}
			else
			{
				if (sortDirection.Equals("Ascending"))
					sortDirection = "ASC";
				else
				{
					sortDirection = "DESC";
				}
			}

			//Check which sort is selected
			//If View Sort is selected
			if (RadioButtonList1.SelectedItem.Value.Equals("VSort"))
			{
				var columns = new string[DtGlobal.Columns.Count];
				var coltypes = new Type[DtGlobal.Columns.Count];
				var cnt = 0;

				//create Data Table objects
				foreach (DataColumn column in DtGlobal.Columns)
				{
					columns[cnt] = column.ColumnName;
					coltypes[cnt] = column.GetType();
					cnt++;
				}

				dtlocal.Clear();
				dtlocal.Reset();
				
				foreach (string t in columns)
				{
					var dc = new DataColumn(t, typeof(string));
					sortedTable.Columns.Add(dc);

					var dc2 = new DataColumn(t, typeof(string));
					dtlocal.Columns.Add(dc2);

					var dc3 = new DataColumn(t, typeof(string));
					dtlocal2.Columns.Add(dc3);
				}

				//Load dtlocal with current rows in view 
				for (var i = floor; i <= ceil; i++)
				{
					if (DtGlobal.Rows.Count >= ceil && i < DtGlobal.Rows.Count)
						dtlocal.ImportRow(DtGlobal.Rows[i]);
				}

				//sort current rows in view 
				if (!string.IsNullOrEmpty(sortExpression))
				{
					dtlocal2 = SortDataTable(dtlocal, sortExpression, sortDirection);
				}

				//rebuild the datatable with sorted rows in view and rest of the rows
				for (var i = 0; i < DtGlobal.Rows.Count; i++)
				{
					if (i == floor)
					{
						for (var j = 0; j <= dtlocal2.Rows.Count - 1; j++)
						{
							if (dtlocal2.Rows[j] != null)
								sortedTable.ImportRow(dtlocal2.Rows[j]);
							else
							{
								sortedTable.ImportRow(DtGlobal.Rows[j]);
							}
						}

						i = ceil + 1;

					}

					if (i < DtGlobal.Rows.Count)
						sortedTable.ImportRow(DtGlobal.Rows[i]);
				}

			}

			// fix this name ..
			Sample(DtGlobal, Convert.ToString(ViewState["TableName"]), tableFolder, primaryKeyId, HideData, SessionVariables.IsTesting);

			var dv = DtGlobal.DefaultView;

			// if blank, only should really be the first time
			// then we don't want to appened the sort instruction
			if (!string.IsNullOrEmpty(sortExpression) && DtGlobal.Columns.Contains(sortExpression))
			{
				dv.Sort = sortExpression + " " + sortDirection;
				//System.Diagnostics.Debug.WriteLine(dv.Sort);
			}

			//Bind data based on the sort selection
			if (!SkipGridReload)
			{
				if (RadioButtonList1.SelectedItem.Value.Equals("FTSort"))
				{
					GridView.DataSource = dv;
					GridView.DataBind();
				}
				else
				{
					GridView.DataSource = sortedTable;
					GridView.DataBind();
				}

				if (GridView.AllowPaging)
				{
					if (oGridPagiation != null)
					{
						oGridPagiation.RefreshGrid = false;
						oGridPagiation.ManagePaging(DtGlobal);
					}
				}

				DisableMultipleDelete(ViewState["TableName"].ToString());
				//if (disabledelete)
				//    disabledeletefunctionality();

				if (GridView.Columns[0].HeaderText.Equals(String.Empty))
					GridView.Columns[0].HeaderStyle.Width = 50;

				//if (ViewState["TableName"].ToString() == "ReleaseLogDetail")
				//	styleGrid();

				if (!string.IsNullOrEmpty(FieldConfigurationMode))
				{
					StyleDataRows(GridView, ViewState, int.Parse(FieldConfigurationMode));
				}
				else
				{
					StyleDataRows(GridView, ViewState, -1);
				}

				ShowCheckBoxColumn(false);

			}
			
			if (RadioButtonList1.SelectedItem.Value.Equals("FTSort"))
			{
				return dv.ToTable(ViewState["TableName"].ToString());
			}
			else
			{
				return sortedTable;
			}
		}

		//private void StyleGrid()
		//{
		//	GridView.HeaderStyle.Height = new Unit(PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesHeaderStyleHeight));
		//	GridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesHeaderBackColor));
		//	GridView.HeaderStyle.ForeColor = ColorTranslator.FromHtml(PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesHeaderForeColor));
		//	GridView.HeaderStyle.Font.Size = new FontUnit(PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesHeaderStyleFontSize));

		//	for (var x = 0; x < GridView.Rows.Count; x = x + 2)
		//	{
		//		GridView.Rows[x].BackColor = ColorTranslator.FromHtml(PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesAlternatingRowStyleBackColor));
		//		GridView.Rows[x].Font.Size = new FontUnit(PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesAlternatingRowStyleFontSize));
		//		GridView.Rows[x].Height = new Unit(PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesAlternatingRowStyleHeight));
		//	}
		//}

		private void ShowCheckBoxColumn(bool value)
		{
			GridView.Columns[0].Visible = value;
		}

		//private void disabledeletefunctionality()
		//{
		//    GridView.Columns[GridView.Columns.Count - 1].Visible = false;
		//    ButtonDelete.Visible = false;
		//}


		public void StyleDataRows(GridView mainGridView, StateBag viewState, int modeId)
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

		private void StyleFKColumns()
		{
			FkColumnIds = new ArrayList();
			DescColumnIds = new ArrayList();

			for (var i = 1; i < GridView.Columns.Count; i++)
			{
				if (GridView.Columns[i].HeaderText.Contains("Id") || GridView.Columns[i].HeaderText.Contains("ID"))
				{
					if (!GridView.Columns[i].HeaderText.Equals(ViewState["TableName"] + " Id"))
						FkColumnIds.Add(i);
				}

				for (var j = 0; j < ApplicationCommon.SystemEntities.Length; j++)
				{
					if (GridView.Columns[i].HeaderText.Equals(ApplicationCommon.SystemEntities[j]) || GridView.Columns[i].HeaderText.Contains(ApplicationCommon.SystemEntities[j]))
					{
						if (!GridView.Columns[i].HeaderText.Equals(ViewState["TableName"] + " Id"))
							FkColumnIds.Add(i);
					}
				}
				if (GridView.Columns[i].HeaderText.Contains("Description"))
				{
					DescColumnIds.Add(i);
				}
			}

			if (FkColumnIds.Count > 0)
			{
				for (var j = 0; j < FkColumnIds.Count; j++)
				{
					var cellid = int.Parse(FkColumnIds[j].ToString());
					for (var k = 0; k < GridView.Rows.Count; k++)
					{
						GridView.Rows[k].Cells[cellid].Style.Add("text-align", "left");
					}
				}
			}

			if (DescColumnIds.Count > 0)
			{
				for (var j = 0; j < DescColumnIds.Count; j++)
				{
					var cellid = int.Parse(DescColumnIds[j].ToString());
					for (var k = 0; k < GridView.Rows.Count; k++)
					{
						GridView.Rows[k].Cells[cellid].Style.Add("text-align", "left");
					}
				}
			}

		}

		private DataTable GetDataSet(string tableName)
		{
			//var ds = (DataTable)Cache["TaskTrackerTable"];

			//var ds = (DataTable)Session[tableName];
			var ds = (DataTable)ViewState[Prefix + tableName];

			// Contact the database if necessary.
			if (ds == null || ViewState["SessionUpdated"] != null)
			{
				if (ViewState["SessionUpdated"] == null || Convert.ToBoolean(ViewState["SessionUpdated"]))
				{
					if (_getSubGroupData != null)
					{
						ds = _getSubGroupData(PrimaryKey, SubGroupKey);
					}
					else
					{
						ds = _getData(PrimaryKey);
					}
					//Cache.Insert("TaskTrackerTable", ds, null, DateTime.MaxValue,
					//TimeSpan.FromMinutes(2));

					Session[tableName] = ds;
					lblCacheStatus.Text = "Created and added to cache.";
				}
			}
			else
			{
				lblCacheStatus.Text = "Retrieved from cache.";
			}

			return ds;
		}
		
		#endregion

		#region Setup Methods

		public void Setup
		(
				string tableName
			,	string tableFolder
			,	string primaryKeyName
			,	int primaryKey
			,	bool pageLoad
			,	GetDataDelegate getDataDelegate
			,	GetColumnDelegate getColumnDelegate
			,	string userPreferenceCategory = ""
			,	int userPrferenceCategoryId = 0
		)
		{ 
			Setup
			(
					tableName
				,	tableFolder
				,	primaryKeyName
				,	primaryKey.ToString()
				,	pageLoad
				,	getDataDelegate
				,	getColumnDelegate
				,	userPreferenceCategory
				,	userPrferenceCategoryId
			);
		}

        public void Setup
		(
				string tableName
			,	string tableFolder
			,	string primaryKeyName
			,	string primaryKey
			,	bool pageLoad
			,	GetDataDelegate getDataDelegate
			,	GetColumnDelegate getColumnDelegate
			,	string userPreferenceCategory = ""
			,	int userPrferenceCategoryId = 0
		)
        {			
            Prefix = primaryKey;
            
			oGridPagiation = new GridPagiation();
            
			_getData = getDataDelegate;
            _getColumnDelegate = getColumnDelegate;
            
			PrimaryKey = primaryKey;
            //ViewState["TableFolder"] = tableFolder;

            if (SessionVariables.ActiveTableName == null)
                SessionVariables.ActiveTableName = tableName;

            if (!string.IsNullOrEmpty(SessionVariables.ActiveTableName) && !(SessionVariables.ActiveTableName.Equals(tableName)))
            {
                SessionVariables.ActiveTableName = tableName;
            }
			
            ViewState["TableName"]   = tableName;

            ViewState["TableFolder"] = tableFolder;
            ViewState["PrimaryKey"]  = primaryKeyName;            
            ViewState["PageLoad"]    = pageLoad;

            if (ViewState["TableName"] == null)
            {
                ViewState.Add("TableName", tableName);
            }

            if (ViewState["TableName"] != null && !(ViewState["TableName"].ToString().Equals(tableName)))
            {
                ViewState["TableName"] = tableName;
            }

            if (string.IsNullOrEmpty(userPreferenceCategory))
            {
                UserPreferenceCategory = tableName;
            }
            else
            {
                UserPreferenceCategory = userPreferenceCategory;
            }
            
			if (userPrferenceCategoryId == 0)
            {
                SetUserPreferenceCategory();
            }
            else
            {
                UserPreferenceCategoryId = userPrferenceCategoryId;
            }

            GridView.PageSize = CustomPageSize;

            DtGlobal = _getData(PrimaryKey);
            oGridPagiation.Setup(plcPaging, litPagingSummary, lblCacheStatus, DtGlobal, GridView, Page, SettingCategory);
            oGridPagiation.Changed += oGridPagiation_Changed;
            
			if (CurrentPageIndex != null)
                oGridPagiation.PageIndexInSession = CurrentPageIndex.Value;
            
			oGridPagiation.ManagePaging(DtGlobal);
			
            //if (!IsPostBack)
            if (IsFCModeVisible)
            {
                SetUpDropDownAEFLMode();
            }
            else
            {                
                ddlFieldConfigurationMode.Visible = false;
            }
        }

		public void Setup
		(
				string tableName
			,	string tableFolder
			,	string primaryKeyName
			,	string primaryKey
			,	string subGroupKeyName
			,	string subGroupKey
			,	bool pageLoad
			,	GetSubGroupDataDelegate getDataDelegate
			,	GetColumnDelegate getColumnDelegate
			,	string userPreferenceCategory = ""
			,	int userPrferenceCategoryId = 0
		)
		{
			Prefix = primaryKey;

			oGridPagiation = new GridPagiation();

			_getSubGroupData = getDataDelegate;
			_getColumnDelegate = getColumnDelegate;

			PrimaryKey = primaryKey;
			SubGroupKey = subGroupKey;
			SubGroupKeyName = subGroupKeyName;

			if (SessionVariables.ActiveTableName == null)
				SessionVariables.ActiveTableName = tableName;

			if (!string.IsNullOrEmpty(SessionVariables.ActiveTableName) && !(SessionVariables.ActiveTableName.Equals(tableName)))
			{
				SessionVariables.ActiveTableName = tableName;
			}

			ViewState["TableName"] = tableName;

			ViewState["TableFolder"] = tableFolder;
			ViewState["PrimaryKey"] = primaryKeyName;
			ViewState["PageLoad"] = pageLoad;

			if (ViewState["TableName"] == null)
			{
				ViewState.Add("TableName", tableName);
			}

			if (ViewState["TableName"] != null && !(ViewState["TableName"].ToString().Equals(tableName)))
			{
				ViewState["TableName"] = tableName;
			}

			if (string.IsNullOrEmpty(userPreferenceCategory))
			{
				UserPreferenceCategory = tableName;
			}
			else
			{
				UserPreferenceCategory = userPreferenceCategory;
			}

			if (userPrferenceCategoryId == 0)
			{
				SetUserPreferenceCategory();
			}
			else
			{
				UserPreferenceCategoryId = userPrferenceCategoryId;
			}

			GridView.PageSize = CustomPageSize;

			DtGlobal = _getSubGroupData(PrimaryKey, SubGroupKey);
			oGridPagiation.Setup(plcPaging, litPagingSummary, lblCacheStatus, DtGlobal, GridView, Page, SettingCategory);
			oGridPagiation.Changed += oGridPagiation_Changed;

			if (CurrentPageIndex != null)
				oGridPagiation.PageIndexInSession = CurrentPageIndex.Value;

			oGridPagiation.ManagePaging(DtGlobal);

			//if (!IsPostBack)
			if (IsFCModeVisible)
			{
				SetUpDropDownAEFLMode();
			}
			else
			{
				ddlFieldConfigurationMode.Visible = false;
			}
		}

		#endregion

		//public void Setup
		//(
		//		string tableName
		//	,	string tableFolder
		//	,	string primaryKey
		//	,	int primaryKeyId
		//	,	bool pageLoad
		//	,	GetDataDelegate getDataDelegate
		//	,	GetColumnDelegate getColumnDelegate
		//	,	bool isPaging
		//	,	bool isUpdateColumn = true
		//	,	bool isDeleteColumn = true
		//	,	string userPreferenceCategory = ""
		//)
		//{
			
		//	this.Prefix = primaryKeyId.ToString();
		//	_getData = getDataDelegate;
		//	_getColumnDelegate = getColumnDelegate;

		//	ViewState["TableFolder"] = tableFolder;

		//	if (SessionVariables.ActiveTableName == null)
		//		SessionVariables.ActiveTableName = tableName;

		//	if (!string.IsNullOrEmpty(SessionVariables.ActiveTableName) && !(SessionVariables.ActiveTableName.Equals(tableName)))
		//	{
		//		SessionVariables.ActiveTableName = tableName;
		//	}

		//	if (ViewState["TableName"] == null)
		//	{
		//		ViewState.Add("TableName", tableName);
		//	}

		//	PrimaryKeyId = primaryKeyId;

		//	ViewState["TableName"] = tableName;
		//	ViewState["PrimaryKey"] = primaryKey;
		//	//ViewState["IsTesting"] = SessionVariables.IsTesting;
		//	ViewState["PageLoad"] = pageLoad;

		//	if (ViewState["TableName"] != null && !(ViewState["TableName"].ToString().Equals(tableName)))
		//	{
		//		ViewState["TableName"] = tableName;
		//	}

		//	if (CurrentPageIndex == null)
		//	{
		//		CurrentPageIndex = 0;
		//	}
            

		//	if (string.IsNullOrEmpty(userPreferenceCategory))
		//	{
		//		UserPreferenceCategory = tableName;
		//	}
		//	else
		//	{
		//		UserPreferenceCategory = userPreferenceCategory;
		//	}

		//	SetUserPreferenceCategory();

		//	GridView.AllowPaging = isPaging;

		//	if (isPaging)
		//	{
		//		GridView.PageSize = CustomPageSize;
		//		oGridPagiation = new GridPagiation();
		//		oGridPagiation.Setup(plcPaging, litPagingSummary, lblCacheStatus, DtGlobal, GridView, Page, SettingCategory);
		//		oGridPagiation.Changed += oGridPagiation_Changed;
                
		//		if (CurrentPageIndex != null)
		//			oGridPagiation.PageIndexInSession = CurrentPageIndex.Value;
                
		//		oGridPagiation.ManagePaging(DtGlobal);

		//	}

		//	this.IsUpdateColumn = isUpdateColumn;
		//	this.IsDeleteColumn = isDeleteColumn;

		//	//if (!IsPostBack)
		//	SetUpDropDownAEFLMode();
            
		//	if (!IsFCModeVisible)
		//	{
		//		ddlFieldConfigurationMode.Visible = false;
		//	}

		//	GridView.EnableTheming = false;
		//}

		#region Events

		protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
			DescColumnIds = new ArrayList();			
		
			//if (e.Row.RowType == DataControlRowType.Header)
			//{
			//	//string tablename = ViewState["TableName"].ToString();
			//	//var row = (DataRowView)e.Row.DataItem;

			//	//var cells = e.Row.Cells;
			//}

            if (e.Row.RowType == DataControlRowType.DataRow)
            {				
                var row = (DataRowView)e.Row.DataItem;

                //var cells = e.Row.Cells;

				//for (var j = 0; j < e.Row.Cells.Count; j++)
				//{
				//	if (j < e.Row.Cells.Count)
				//	{
				//		//e.Row.Cells[j].Style.Add("font-weight", "bold");
				//		//e.Row.Cells[j].Style.Add("width", "auto");
				//		//e.Row.Cells[j].Style.Add("text-align", "center");						
				//	}

				//	//if (ViewState["TableName"].ToString() == "ReleaseLogDetail")
				//	//{
				//	//	e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml(PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesRowStyleBackColor));						
				//	//	e.Row.Height = new Unit(PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesRowStyleHeight));

				//	//	for (var k = 1; k < e.Row.Cells.Count; k++)
				//	//	{
				//	//		if (e.Row.Cells[k].Controls.Count != 0)
				//	//		{
				//	//			((HyperLink)(e.Row.Cells[k].Controls[0])).ForeColor = System.Drawing.ColorTranslator.FromHtml(PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesRowStyleForeColor));
				//	//			((HyperLink)(e.Row.Cells[k].Controls[0])).Font.Size = new FontUnit(PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesRowStyleFontSize));
				//	//		}

				//	//	}
				//	//}

				//	// this code was supposed to be for Update/Delete Hyperlink column
				//	//else if (j == e.Row.Cells.Count - 2 || j == e.Row.Cells.Count - 1)
				//	//{
				//	//    e.Row.Cells[j].Style.Add("width", "60px");
				//	//}
				//}
				
				//for (var i = 1; i < GridView.Columns.Count; i++)
				//{
					// gets the column of the Description Header text
					//if (GridView.Columns[i].HeaderText.Contains("Description"))
					//{
					//	DescColumnIds.Add(i);

				//		// gets the columnid of the Description Header text
				//		var cellid = int.Parse(DescColumnIds[0].ToString());

				//		for (var j = 0; j < e.Row.Cells.Count; j++)
				//		{
				//			if (j < e.Row.Cells.Count)
				//			{
				//				// gets the Description column text and trim the characters based on the value of GridDefaultCharacterCount constant
				//				var str = ((HyperLink)(e.Row.Cells[cellid].Controls[0])).Text;
				//				var charCount = Convert.ToInt16(PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.GridDefaultCharacterCount));

				//				if (str.Count() > charCount + GridCharLength)
				//					((HyperLink)(e.Row.Cells[cellid].Controls[0])).Text = str.Substring(0, charCount) + ApplicationCommon.DynamicGridCharacter;
				//			}
				//		}
				//	}
				//}

				// this code was supposed to be for Update/Delete Hyperlink column
				//else if (j == e.Row.Cells.Count - 2 || j == e.Row.Cells.Count - 1)
				//{
				//    e.Row.Cells[j].Style.Add("width", "60px");
				//}					
										
				var tableName = Convert.ToString(ViewState["TableName"]);
				
                NotRight(sender, e, tableName, row);			
            }
        }

	    private static void NotRight(object sender, GridViewRowEventArgs e, string tableName, DataRowView row)
	    {
		    if (tableName == "FunctionalityEntityStatusArchive")
		    {
			    var lstColumns = new List<string>() {"functionality status", "functionality priority", "assignedto", "memo"};
			    //if(Convert.ToString(row["
			    foreach (var strColumn in lstColumns)
			    {
				    try
				    {
					    var isChanged = Convert.ToInt32(row["Is" + strColumn.Replace(" ", "") + "Changed"]);

					    if (isChanged > 0)
					    {
						    var col = ((GridView) sender).Columns.Cast<DataControlField>()
							    .Where(c => c.HeaderText.ToLower().Equals(strColumn))
							    .Select(c => c).First();
						    var colIndex = ((GridView) sender).Columns.IndexOf(col);

						    if (colIndex != -1)
						    {
							    e.Row.Cells[colIndex].BackColor = Color.Yellow;
						    }
					    }
				    }
				    catch
				    {
				    }
			    }
		    }
	    }

	    protected void GridView_RowCreated(object sender, GridViewRowEventArgs e)
        {
			DescColumnIds = new ArrayList();

			if (e.Row.RowType == DataControlRowType.Header)
			{
				e.Row.HorizontalAlign = HorizontalAlign.Center;
				//var row = (DataRowView)e.Row.DataItem;
				
				//var cells = e.Row.Cells;
				
			//	// var btn = (HyperLink)cells[0].FindControl("ButtonDelete");
			//	// btn.ImageUrl = Application["Branding"] + "/Images/delete.jpg";
			}			

            if (e.Row.RowType == DataControlRowType.Pager)
            {
                var space = new LiteralControl("     ");

                var lb = new Label();
                lb.ID = "lblsummary";
                lb.Text = "";

                // Pager is rendered in a single cell as a table;   
                // each page # is in a cell by it's own  
                var table = e.Row.Cells[0].Controls[0] as Table;

                // Add ViewAll linkbutton to the last cell  
                var parentCell = table.Rows[0].Cells[table.Rows[0].Cells.Count - 1];
                parentCell.Controls.Add(space);
                parentCell.Controls.Add(lb);
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
				//e.Row.ForeColor = System.Drawing.Color.Red;
                //var rowView = (DataRowView)e.Row.DataItem;
                var myCells = e.Row.Cells;

                int _deleteIndex = 0, _updateIndex = 0;

                if (ViewState[VIEW_STATE_KEY_DELETE] != null && ViewState[VIEW_STATE_KEY_UPDATE] != null)
                {
                    int.TryParse(ViewState[VIEW_STATE_KEY_DELETE].ToString(), out _deleteIndex);
                    int.TryParse(ViewState[VIEW_STATE_KEY_UPDATE].ToString(), out _updateIndex);

                    if (_deleteIndex < myCells.Count && _updateIndex < myCells.Count && IsDeleteColumn)
                    {
                        var deleteIndex = 0;
                        int.TryParse(ViewState[VIEW_STATE_KEY_DELETE].ToString(), out deleteIndex);
                        if (myCells[_deleteIndex] != null && myCells[_deleteIndex].Controls.Count > 0)
                        {
                            var deleteLink = (HyperLink)myCells[_deleteIndex].Controls[0];
                            deleteLink.ImageUrl = Application["Branding"] + "/Images/delete.jpg";
                        }
                    }

                    if (IsUpdateColumn && _updateIndex < myCells.Count)
                    {
                        var updateIndex = 0;
                        int.TryParse(ViewState[VIEW_STATE_KEY_DELETE].ToString(), out updateIndex);

                        if (myCells[_updateIndex] != null && myCells[_updateIndex].Controls.Count > 0)
                        {
                            var updateLink = (HyperLink)myCells[_updateIndex].Controls[0];
                            updateLink.ImageUrl = Application["Branding"] + "/Images/untitled1.png";
                        }
                    }
                }
            }
        }

        protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //In-built paging implementation            
            GridView.PageIndex = e.NewPageIndex;
            GridView.DataBind();

            oGridPagiation.PageIndexInSession = e.NewPageIndex;

            //Synchronize with custom paging
            oGridPagiation = new GridPagiation();
            
			DtGlobal = _getData(PrimaryKey);

			oGridPagiation.Setup(plcPaging, litPagingSummary, lblCacheStatus, DtGlobal, GridView, Page, SettingCategory);
            oGridPagiation.Changed += oGridPagiation_Changed;
            oGridPagiation.PageIndexInSession = e.NewPageIndex;
            oGridPagiation.ManagePaging(DtGlobal);
            
			if (!string.IsNullOrEmpty(SessionVariables.SortExpression) && !string.IsNullOrEmpty(SessionVariables.SortDirection))
                SortGridView(SessionVariables.SortExpression, SessionVariables.SortDirection);
        }

		//private void RefreshGrid()
		//{
		//    if (CurrentPageIndex != null)
		//        GridView.PageIndex = CurrentPageIndex.Value;
		//    else
		//    {
		//        GridView.PageIndex = 0;
		//    }
		//    GridView.DataBind();

		//    oGridPagiation = new GridPagiation();
		//    dtGlobal = _getData(PrimaryKeyId);
		//    oGridPagiation.Setup(plcPaging, litPagingSummary, lblCacheStatus, dtGlobal, GridView, Page, SettingCategory);
		//    oGridPagiation.Changed += oGridPagiation_Changed;
		//    oGridPagiation.pageindexinsession = GridView.PageIndex;
		//    oGridPagiation.ManagePaging(dtGlobal);
		//    if (!string.IsNullOrEmpty(SessionVariables.SortExpression) && !string.IsNullOrEmpty(SessionVariables.SortDirection))
		//        SortGridView(SessionVariables.SortExpression, SessionVariables.SortDirection);

		//}

        // OnPageLoad Event Handler
        protected void Page_Load(object sender, EventArgs e)
        {
            // only need to set initial condition, else every other time it shoulld
            // be thee via state 
            if (!IsPostBack)
            {
				if (ViewState["PageLoad"] != null && (bool)ViewState["PageLoad"])
                {
                    // only go forward if settings are correct
                    if (ViewState["TableName"] != null || ViewState["PrimaryKey"] != null)
                    {
                        // can not have this, as keys above are all blank
                        SortGridView(String.Empty, SortDirection.Ascending.ToString());
                    }
                    else
                    {
                        Debug.WriteLine("Missing info ... ");
                    }
                }
            }

		}

        protected override object SaveViewState()
        {
            var baseState = base.SaveViewState();

            return new[] { baseState, DtGlobal };
        }

        protected override void LoadViewState(object savedState)
        {
            var myState = (object[])savedState;

            if (myState[0] != null)
                base.LoadViewState(myState[0]);

            if (myState[1] != null)
            {

                DtGlobal = (DataTable)myState[1];

                GridView.DataSource = DtGlobal;
                GridView.DataBind();
            }
        }

        #region sort
        public SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["SortDirection"] == null)
                {
                    ViewState["SortDirection"] = SortDirection.Ascending;
                }

                return (SortDirection)ViewState["SortDirection"];
            }
            set
            {
                ViewState["SortDirection"] = value;
            }
        }

        /// <summary>
        /// Grid OnSort EventHandler
        /// </summary>
        /// <param Name="sender"></param>
        /// <param Name="e"></param>
        protected void GridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            // get the expreson (Key)
            var sortExpression = e.SortExpression;

            SessionVariables.SortExpression = e.SortExpression;

            // alternate direction););
            if (String.IsNullOrEmpty(SessionVariables.SortDirection))
            {
                if (GridViewSortDirection == SortDirection.Ascending)
                {
                    SessionVariables.SortDirection = "ASC";
                    SortGridView(sortExpression, "ASC");
                    GridViewSortDirection = SortDirection.Descending;

                }
                else
                {
                    SessionVariables.SortDirection = "DESC";
                    SortGridView(sortExpression, "DESC");
                    GridViewSortDirection = SortDirection.Ascending;

                }
            }
            else
            {
                if (SessionVariables.SortDirection.Equals("DESC"))
                {
                    GridViewSortDirection = SortDirection.Ascending;
                    SessionVariables.SortDirection = "ASC";

                }
                else
                {
                    GridViewSortDirection = SortDirection.Descending;
                    SessionVariables.SortDirection = "DESC";
                }
                SortGridView(sortExpression, SessionVariables.SortDirection);
            }
        }

        protected void GridView_Sorted(object sender, EventArgs e)
        {
            var imgArrowDown = ApplicationVariables.Branding + "/Images/arrow-down.jpg";
            var imgArrowUp = ApplicationVariables.Branding + "/Images/arrow-up.jpg";
            var sortexpression = SessionVariables.SortExpression;
            if (sortexpression.Contains("Name"))
            {
                sortexpression = sortexpression.Replace("Name", " Name");

            }
            else if (sortexpression.Contains("Id"))
            {
                sortexpression = sortexpression.Replace("Id", " Id");
            }

            foreach (DataControlField field in GridView.Columns)
            {
                if (field.HeaderText.Contains(sortexpression) || field.HeaderText.Equals(sortexpression))
                {
                    // strip off the old ascending/descending icon
                    var iconPosition = field.HeaderText.IndexOf(@"<img ");
                    if (iconPosition > 0)
                        field.HeaderText = field.HeaderText.Substring(0, iconPosition);

                    // See where to add the sort ascending/descending icon

                    if (SessionVariables.SortDirection == "ASC")
                        field.HeaderText += "<img src='" + imgArrowUp + "' alt='' />";
                    else
                        field.HeaderText += "<img src='" + imgArrowDown + "' alt='' />";
                }
            }


        }
        #endregion

        protected void GridView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
			var currentpage     = GridView.PageIndex;
			var currentpagesize = GridView.PageSize;
			var totalnumofrows  = GridView.Rows.Count;
            var floor           = (currentpage * currentpagesize);
            var ceil            = ((currentpage * currentpagesize) + currentpagesize) - 1;
            var sc              = new StringCollection();
            var id              = String.Empty;
            var dt1             = new DataTable();
            var selectall       = (CheckBox)GridView.HeaderRow.Cells[0].FindControl("chkSelectAll");
            var chkchecked      = selectall.Checked;

            if (!string.IsNullOrEmpty(SessionVariables.SortExpression) && !string.IsNullOrEmpty(SessionVariables.SortDirection))
            {
                SkipGridReload = true;
                dt1 = SortGridView(SessionVariables.SortExpression, SessionVariables.SortDirection);
                SkipGridReload = false;
            }
            else
            {
                dt1 = DtGlobal;
            }

            GridView.DataSource = dt1;
            GridView.DataBind();
            oGridPagiation = new GridPagiation();
            oGridPagiation.Setup(plcPaging, litPagingSummary, lblCacheStatus, dt1, GridView, Page, SettingCategory);
            oGridPagiation.Changed += oGridPagiation_Changed;
            oGridPagiation.ManagePaging(dt1);
            selectall = (CheckBox)GridView.HeaderRow.Cells[0].FindControl("chkSelectAll");
            selectall.Checked = chkchecked;

            //loop the GridView Rows
            var j = 0;
            for (var i = floor; i <= ceil; i++)
            {
                if (j < GridView.Rows.Count)
                {
                    var  cb = (CheckBox)GridView.Rows[j].Cells[0].FindControl("CheckBox1"); //find the CheckBox

                    if (cb != null && selectall != null)
                    {
                        if (selectall.Checked)
                        {
                            if (!cb.Checked)
                            {
                                cb.Checked = true; // add the id to be deleted in the StringCollection
                            }
                        }
                        else
                        {
                            if (cb.Checked)
                            {
                                cb.Checked = false;
                            }
                        }
                    }

                    j++;
                }
            }
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var sc = new StringCollection();
                sc = GetSelectedRecordIDs();
                RetrieveRecords(sc, Command.Delete);
            }
            catch (Exception ex)
            {
                var msg = "Deletion Error:";
                msg += ex.Message;
                throw new Exception(msg);
            }		
        }

        protected void ButtonDetails_Click(object sender, EventArgs e)
        {

            try
            {
                var sc = new StringCollection();
                sc = GetSelectedRecordIDs();
                RetrieveRecords(sc, Command.Details);

            }
            catch (Exception ex)
            {
				var msg = "Details Error:";
                msg += ex.Message;
                throw new Exception(msg);
            }
        }

        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {

            try
            {
                var sc = new StringCollection();
                sc = GetSelectedRecordIDs();
                RetrieveRecords(sc, Command.Update);
            }
            catch (Exception ex)
            {
				var msg = "Updation Error:";
                msg += ex.Message;
                throw new Exception(msg);
            }
		}

		#endregion

    }

}