using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.Configuration;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Framework.Components.ApplicationUser;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace Shared.UI.Web.Configuration.FieldConfiguration.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
    {

        #region properties

        public int? FieldConfigurationId
        {
            get
            {
                if (txtFieldConfigurationId.Enabled)
                {
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtFieldConfigurationId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtFieldConfigurationId.Text);
                }
            }
			set
			{
				txtFieldConfigurationId.Text = (value == null) ? String.Empty : value.ToString();
			}
        }

        public int? SystemEntityTypeId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtSystemEntityTypeId.Text.Trim());
                else
                    return int.Parse(drpSystemEntityTypeList.SelectedItem.Value);
            }
			set
			{
				txtSystemEntityTypeId.Text = (value == null) ? String.Empty : value.ToString();
			}
        }
		public int? ApplicationId
		{
			get
			{
				
					var isTesting = SessionVariables.IsTesting;
					if (isTesting)
						return int.Parse(txtApplication.Text.Trim());
					else
						return int.Parse(drpApplicationList.SelectedItem.Value);
				
			}
			set
			{
				txtApplication.Text = (value == null) ? String.Empty : value.ToString();
			}
		}
        public string DisplayName
        {
            get
            {
                return txtDisplayName.Text;
            }
			set
			{
				txtDisplayName.Text = value ?? String.Empty;
			}
        }

        public string Name
        {
            get
            {
                return txtName.Text;
            }
			set
			{
				txtName.Text = value ?? String.Empty;
			}
        }

        public string Value
        {
            get
            {
				return Framework.Components.DefaultDataRules.CheckAndGetDescription(txtName.Text, txtValue.InnerText);
            }
			set
			{
				txtValue.InnerText = value ?? String.Empty;
			}
        }

        public Decimal? Width
        {
            get
            {
                if (txtWidth.Text == string.Empty)
                {
                    return null;
                }
                else
                {
                    return Decimal.Parse(txtWidth.Text);
                }
            }
            set
            {
				txtWidth.Text = (value == null) ? String.Empty : value.ToString();
            }

        }

        public string Formatting
        {
            get
            {
                return txtFormatting.Text; 
            }
			set
			{
				txtFormatting.Text = value ?? String.Empty;
			}
        }

        public string ControlType
        {
            get
            {
                return txtControlType.Text; 
            }
			set
			{
				txtControlType.Text = value ?? String.Empty;
			}
        }

        public string HorizontalAlignment
        {
            get
            {
                return txtHorizontalAlignment.Text; 
            }
			set
			{
				txtHorizontalAlignment.Text = value ?? String.Empty;
			}
        }

        public int? GridViewPriority
        {
            get
            {
                if (txtGridViewPriority.Text == string.Empty)
                {
                    return null;
                }
                else
                {

                    return int.Parse(txtGridViewPriority.Text);
                }
            }
			set
			{
				txtGridViewPriority.Text = (value == null) ? String.Empty : value.ToString();
			}

        }

        public int? DetailsViewPriority
        {
            get
            {
                if (txtDetailsViewPriority.Text == string.Empty)
                {
                    return null;
                }
                else
                {

                    return int.Parse(txtDetailsViewPriority.Text);
                }
            }
			set
			{
				txtDetailsViewPriority.Text = (value == null) ? String.Empty : value.ToString();
			}
        }

        public int? DisplayColumn
        {
            get
            {
                if (txtDisplayColumn.Text == string.Empty)
                {
                    return null;
                }
                else
                {
                    return int.Parse(txtDisplayColumn.Text);
                }
            }
            set
            {
                txtDisplayColumn.Text = (value == null) ? String.Empty : value.ToString();
            }
        }

		public int? CellCount
		{
			get
			{
				if (txtCellCount.Text == string.Empty)
				{
					return null;
				}
				else
				{
					return int.Parse(txtCellCount.Text);
				}
			}
			set
			{
				txtCellCount.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

        public int? FieldConfigurationModeId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtFieldConfigurationMode.Text.Trim());
                else
                    return int.Parse(ddlFieldConfigurationMode.SelectedItem.Value);
            }
			set
			{
				txtFieldConfigurationMode.Text = (value == null) ? String.Empty : value.ToString();
			}

        }

        #endregion properties

		#region private methods

		public override int? Save(string action)
		{
			var data                           = new FieldConfigurationDataModel();

			data.FieldConfigurationId          = FieldConfigurationId;
			data.SystemEntityTypeId            = SystemEntityTypeId;
			data.ApplicationId				   = ApplicationId;
			data.FieldConfigurationDisplayName = DisplayName;
			data.Name                          = Name;
			data.Value                         = Value;
			data.Width                         = Width;
			data.Formatting                    = Formatting;
			data.ControlType                   = ControlType;
			data.HorizontalAlignment           = HorizontalAlignment;
			data.GridViewPriority              = GridViewPriority;
			data.DetailsViewPriority           = DetailsViewPriority;
			data.FieldConfigurationModeId      = FieldConfigurationModeId;
            data.DisplayColumn                 = DisplayColumn;
			data.CellCount                     = CellCount;

			if (action == "Insert")
			{
				var dtFieldConfiguration = FieldConfigurationDataManager.DoesExist(data, SessionVariables.RequestProfile);

				if (dtFieldConfiguration.Rows.Count == 0)
				{
                    data.FieldConfigurationId            = FieldConfigurationDataManager.Create(data, SessionVariables.RequestProfile);

                    var dataDisplayName                  = new FieldConfigurationDisplayNameDataModel();
                    dataDisplayName.FieldConfigurationId = data.FieldConfigurationId;
                    dataDisplayName.Value                = DisplayName;
                    dataDisplayName.LanguageId           = ApplicationCommon.LanguageId;
                    dataDisplayName.IsDefault            = 1;

                    FieldConfigurationDisplayNameDataManager.Create(dataDisplayName, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Unique Combination already exists.");
				}
			}
			else
			{
				FieldConfigurationDataManager.Update(data, SessionVariables.RequestProfile);
			}

            FieldConfigurationUtility.SetFieldConfigurations();

			// not correct ... when doing insert, we didn't get/change the value of FieldConfigurationId ?
			return data.FieldConfigurationId;
		}

		public void FormatFields(bool activate, int entityId, int modeSelected)
		{
			SetupDropdown();
			txtWidth.Text                           = "100.00";
			txtControlType.Text                     = "TextBox";
			txtHorizontalAlignment.Text             = "Left";
			txtDetailsViewPriority.Text             = "1";
			txtGridViewPriority.Text                = "1";
			ddlFieldConfigurationMode.SelectedValue = modeSelected.ToString(CultureInfo.InvariantCulture);
			drpSystemEntityTypeList.SelectedValue   = entityId.ToString();
			ddlFieldConfigurationMode.Enabled       = !activate;
			drpSystemEntityTypeList.Enabled         = !activate;
			txtSystemEntityTypeId.Text              = entityId.ToString(CultureInfo.InvariantCulture);
			txtFieldConfigurationMode.Text          = modeSelected.ToString(CultureInfo.InvariantCulture);
			txtDisplayColumn.Text                   = "1";
			txtCellCount.Text                       = "3";
		}

		public override void SetId(int setId, bool chkFieldConfigurationId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkFieldConfigurationId);
			txtFieldConfigurationId.Enabled = chkFieldConfigurationId;
			//txtDescription.Enabled = !chkFieldConfigurationId;
			//txtName.Enabled = !chkFieldConfigurationId;
			//txtSortOrder.Enabled = !chkFieldConfigurationId;
		}

		private void SetupDropdown()
		{
			var isTesting = SessionVariables.IsTesting;
			var systemEntityTypeData = Framework.Components.Core.SystemEntityTypeDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(systemEntityTypeData, drpSystemEntityTypeList, SystemEntityTypeDataModel.DataColumns.EntityName, SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId);

			var applicationData = ApplicationDataManager.GetList(SessionVariables.RequestProfile);

			UIHelper.LoadDropDown(applicationData, drpApplicationList, ApplicationDataModel.DataColumns.Name, ApplicationDataModel.DataColumns.ApplicationId);
			drpApplicationList.AutoPostBack = true;

			var data = new FieldConfigurationModeDataModel();
			data.ApplicationId = Convert.ToInt32(drpApplicationList.SelectedValue);
			//data.ApplicationId = SessionVariables.RequestProfile.ApplicationId;
			var fieldConfigurationModeData = FieldConfigurationModeDataManager.GetDetails(data, SessionVariables.RequestProfile);

			UIHelper.LoadDropDown(fieldConfigurationModeData, ddlFieldConfigurationMode, StandardDataModel.StandardDataColumns.Name, FieldConfigurationModeDataModel.DataColumns.FieldConfigurationModeId);
			if (isTesting)
			{
				drpSystemEntityTypeList.AutoPostBack = true;

				if (drpSystemEntityTypeList.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtSystemEntityTypeId.Text.Trim()))
					{
						drpSystemEntityTypeList.SelectedValue = txtSystemEntityTypeId.Text;
					}
					else
					{
						txtSystemEntityTypeId.Text = drpSystemEntityTypeList.SelectedItem.Value;
					}
				}
				
				txtSystemEntityTypeId.Visible = true;
				ddlFieldConfigurationMode.AutoPostBack = true;

				if (ddlFieldConfigurationMode.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtFieldConfigurationMode.Text.Trim()))
					{
						ddlFieldConfigurationMode.SelectedValue = txtFieldConfigurationMode.Text;
					}
					else
					{
						txtFieldConfigurationMode.Text = ddlFieldConfigurationMode.SelectedItem.Value;
					}
				}
				txtFieldConfigurationMode.Visible = true;
			}
			else
			{
				if (!string.IsNullOrEmpty(txtSystemEntityTypeId.Text.Trim()))
				{
					drpSystemEntityTypeList.SelectedValue = txtSystemEntityTypeId.Text;
				}
				if (!string.IsNullOrEmpty(txtFieldConfigurationMode.Text.Trim()))
				{
					ddlFieldConfigurationMode.SelectedValue = txtFieldConfigurationMode.Text;
				}
				txtSystemEntityTypeId.Visible = false;
				txtFieldConfigurationMode.Visible = false;
			}
		}

		public void LoadData(int fieldConfigurationId, bool showId)
		{
			// clear UI
			Clear();

			// set up parameters
			var data = new FieldConfigurationDataModel();
			data.FieldConfigurationId = fieldConfigurationId;

			// get data
            var items = FieldConfigurationDataManager.GetEntityDetails(data, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfo);

			// should only have single match 
            if (items.Count != 1)
            {
                return;
            }

			var item                              = items[0];

			txtFieldConfigurationId.Text          = item.FieldConfigurationId.ToString();
			txtName.Text                          = item.Name;
			txtApplication.Text					  = item.ApplicationId.ToString();
			
			txtSystemEntityTypeId.Text            = item.SystemEntityTypeId.ToString();
			drpSystemEntityTypeList.SelectedValue = item.SystemEntityTypeId.ToString();
			txtDisplayName.Text                   = item.FieldConfigurationDisplayName;
			txtName.Text                          = item.Name;
			txtValue.InnerText                    = item.Value;			
			txtWidth.Text                         = item.Width.ToString();
			txtFormatting.Text                    = item.Formatting;
			txtControlType.Text                   = item.ControlType;
			txtHorizontalAlignment.Text           = item.HorizontalAlignment;
			txtGridViewPriority.Text              = item.GridViewPriority.ToString();
			txtDetailsViewPriority.Text           = item.DetailsViewPriority.ToString();			
			ddlFieldConfigurationMode.SelectedValue = item.FieldConfigurationModeId.ToString();
			txtFieldConfigurationMode.Text        = item.FieldConfigurationModeId.ToString();
            txtDisplayColumn.Text                 = item.DisplayColumn.ToString();
			txtCellCount.Text                     = item.CellCount.ToString();
			
			var applicationData = new ApplicationDataModel();

			applicationData.ApplicationId = item.ApplicationId;
			var appDatas = ApplicationDataManager.GetDetails(applicationData, SessionVariables.RequestProfile);

			//drpApplicationList.SelectedValue = appDatas.Rows[0][ApplicationDataModel.DataColumns.Name].ToString();
			if (!showId)
			{
				txtFieldConfigurationId.Text = item.FieldConfigurationId.ToString();

                txtDisplayName.Enabled = false;

				//PlaceHolderAuditHistory.Visible = true;
				// only show Audit History in case of Update page, not for Clone.
				oHistoryList.Setup(PrimaryEntity, fieldConfigurationId, PrimaryEntityKey);
			}
			else
			{
				txtFieldConfigurationId.Text = String.Empty;
			}

			//oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);   
		}

		protected override void Clear()
		{
			base.Clear();

			var data                 = new FieldConfigurationDataModel();

			SystemEntityTypeId       = data.SystemEntityTypeId;			
			Name                     = data.Name;
			ApplicationId			 = data.ApplicationId;
			DisplayName              = data.FieldConfigurationDisplayName;
			Value                    = data.Value;
			Width                    = data.Width;
			Formatting               = data.Formatting;
			ControlType              = data.ControlType;
			HorizontalAlignment      = data.HorizontalAlignment;
			GridViewPriority         = data.GridViewPriority;
			DetailsViewPriority      = data.DetailsViewPriority;
			FieldConfigurationModeId = data.FieldConfigurationModeId;
            DisplayColumn            = data.DisplayColumn;
			CellCount                = data.CellCount;
		}

		#endregion        

		#region Events

		protected void drpSystemEntityType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSystemEntityTypeId.Text = drpSystemEntityTypeList.SelectedItem.Value;
        }

		protected void drpApplicationList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtApplication.Text = drpApplicationList.SelectedItem.Value;
		}


        protected void ddlFieldConfigurationMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFieldConfigurationMode.Text = ddlFieldConfigurationMode.SelectedItem.Value;
        }        

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                SetupDropdown();

            var isTesting = SessionVariables.IsTesting;
            
			txtFieldConfigurationId.Visible = isTesting;
            lblFieldConfigurationId.Visible = isTesting;
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey = "FieldConfiguration";
			FolderLocationFromRoot = "FieldConfiguration";
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FieldConfiguration;

			// set object variable reference            
			PlaceHolderCore = dynFieldConfigurationId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
		} 

		#endregion

	}
}
