using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;

using System.Data;

namespace Shared.UI.Web.Configuration.ApplicationEntityFieldLabel.Controls
{
	public partial class Generic : Shared.UI.WebFramework.DVCUserControl
	{

        #region properties

        public string BorderClass
        {
            set
            {
                borderdiv.Attributes["class"] = value;
            }
        }

        public bool IsHistoryVisible
		{
			get
			{
				return dynAuditHistory.Visible;
			}
			set
			{
				dynAuditHistory.Visible = value;
			}
		}

		public int? ApplicationEntityFieldLabelId
		{
			get
            {
                if (txtApplicationEntityFieldLabelId.Enabled)
                {
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtApplicationEntityFieldLabelId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtApplicationEntityFieldLabelId.Text);
                }
			}
		}

		public int SystemEntityTypeId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtSystemEntityTypeId.Text.Trim());
				else
					return int.Parse(drpSystemEntityTypeList.SelectedItem.Value);
			}

		}


		public string Name
		{
			get
			{
				return txtName.Text; ;
			}
		}

		public string Value
		{
			get
			{
				return Framework.Components.DefaultDataRules.CheckAndGetDescription(txtName.Text, txtValue.InnerText);
			}
		}

		public Decimal? Width
		{
			get
			{
				if (txtWidth.Text == "")
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
				
			}

		}
		public string Formatting
		{
			get
			{
				return txtFormatting.Text; ;
			}
		}
		public string ControlType
		{
			get
			{
				return txtControlType.Text; ;
			}
		}

		public string HorizontalAlignment
		{
			get
			{
				return txtHorizontalAlignment.Text; ;
			}
		}

		public int? GridViewPriority
		{
			get
			{
				if (txtGridViewPriority.Text == "")
				{
					return null;
				}
				else
				{

					return int.Parse(txtGridViewPriority.Text);
				}

			}

		}

		public int? DetailsViewPriority
		{
			get
			{
				if (txtDetailsViewPriority.Text == "")
				{
					return null;
				}
				else
				{

					return int.Parse(txtDetailsViewPriority.Text);
				}

			}

		}

		public int ApplicationEntityFieldLabelModeId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtApplicationEntityFieldLabelMode.Text.Trim());
				else
					return int.Parse(ddlApplicationEntityFieldLabelMode.SelectedItem.Value);
			}

		}
		#endregion properties

		protected override string ValidationConfigFile
		{
			get
			{
				return Server.MapPath("~/Shared/Configuration/ApplicationEntityFieldLabel/Controls/Validation.xml"); //"R:\ApplicationEntityFieldLabels\Task Time Tracker\Dev\UI\Web\ValidationDataFile\dvc.xml";
			}
		}

		public void SetId(int setId, bool chkApplicationEntityFieldLabelId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkApplicationEntityFieldLabelId);
			txtApplicationEntityFieldLabelId.Enabled = chkApplicationEntityFieldLabelId;
			//txtDescription.Enabled = !chkApplicationEntityFieldLabelId;
			//txtName.Enabled = !chkApplicationEntityFieldLabelId;
			//txtSortOrder.Enabled = !chkApplicationEntityFieldLabelId;
		}

		public void FormatFields(bool activate, int entityId,int modeselected )
		{
			SetupDropdown();
			txtWidth.Text = "100.00";
			txtControlType.Text = "TextBox";
			txtHorizontalAlignment.Text = "Left";
			txtDetailsViewPriority.Text = "1";
			txtGridViewPriority.Text = "1";
			ddlApplicationEntityFieldLabelMode.SelectedValue = modeselected.ToString();
			drpSystemEntityTypeList.SelectedValue = entityId.ToString();
			ddlApplicationEntityFieldLabelMode.Enabled = !activate;
			drpSystemEntityTypeList.Enabled = !activate;
			txtSystemEntityTypeId.Text = entityId.ToString();
			txtApplicationEntityFieldLabelMode.Text = modeselected.ToString();
		}

		public void LoadData(int applicationEntityFieldLabelId, bool showId)
		{
			var data = new Framework.Components.UserPreference.ApplicationEntityFieldLabel.Data();
			data.ApplicationEntityFieldLabelId = applicationEntityFieldLabelId;
			var oApplicationEntityFieldLabelTable = Framework.Components.UserPreference.ApplicationEntityFieldLabel.GetDetails(data, AuditId);

			if (oApplicationEntityFieldLabelTable.Rows.Count == 1)
			{
				var row = oApplicationEntityFieldLabelTable.Rows[0];

				if (!showId)
				{

                    txtApplicationEntityFieldLabelId.Text = row[Framework.Components.UserPreference.ApplicationEntityFieldLabel.DataColumns.ApplicationEntityFieldLabelId].ToString();

					// only show Audit History in case of Update page, not for Clone.
					oHistoryList.Setup("AuditHistory", "Audit", "AuditHistoryId", true, true, (int)Framework.Components.DataAccess.SystemEntity.ApplicationEntityFieldLabel, applicationEntityFieldLabelId, "ApplicationEntityFieldLabel");
					dynAuditHistory.Visible = ApplicationCommon.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "ApplicationEntityFieldLabel");
                    				
				}
				else
				{
					txtApplicationEntityFieldLabelId.Text = string.Empty;
				}
                txtValue.InnerText                        = row[Framework.Components.UserPreference.ApplicationEntityFieldLabel.DataColumns.Value].ToString();
                txtName.Text                              = row[Framework.Components.UserPreference.ApplicationEntityFieldLabel.DataColumns.Name].ToString();
                txtWidth.Text                             = row[Framework.Components.UserPreference.ApplicationEntityFieldLabel.DataColumns.Width].ToString();
                txtFormatting.Text                        = row[Framework.Components.UserPreference.ApplicationEntityFieldLabel.DataColumns.Formatting].ToString();
                txtControlType.Text                       = row[Framework.Components.UserPreference.ApplicationEntityFieldLabel.DataColumns.ControlType].ToString();
                txtSystemEntityTypeId.Text                = Convert.ToString(row[Framework.Components.UserPreference.ApplicationEntityFieldLabel.DataColumns.SystemEntityTypeId]);
                txtHorizontalAlignment.Text               = Convert.ToString(row[Framework.Components.UserPreference.ApplicationEntityFieldLabel.DataColumns.HorizontalAlignment]);
                drpSystemEntityTypeList.SelectedValue     = Convert.ToString(row[Framework.Components.UserPreference.ApplicationEntityFieldLabel.DataColumns.SystemEntityTypeId]);
				txtGridViewPriority.Text				  = row[Framework.Components.UserPreference.ApplicationEntityFieldLabel.DataColumns.GridViewPriority].ToString();
				txtDetailsViewPriority.Text				  = row[Framework.Components.UserPreference.ApplicationEntityFieldLabel.DataColumns.DetailsViewPriority].ToString();
				txtApplicationEntityFieldLabelMode.Text   = Convert.ToString(row[Framework.Components.UserPreference.ApplicationEntityFieldLabel.DataColumns.ApplicationEntityFieldLabelModeId]);
				ddlApplicationEntityFieldLabelMode.SelectedValue = Convert.ToString(row[Framework.Components.UserPreference.ApplicationEntityFieldLabel.DataColumns.ApplicationEntityFieldLabelModeId]);

				oUpdateInfo.LoadText(oApplicationEntityFieldLabelTable.Rows[0]);
			}
			else
			{
				txtApplicationEntityFieldLabelId.Text = string.Empty;
				txtValue.InnerText = string.Empty;
				txtName.Text = string.Empty;
				txtWidth.Text = string.Empty;
				txtFormatting.Text = string.Empty;
				txtControlType.Text = string.Empty;
				txtSystemEntityTypeId.Text = string.Empty;
				txtHorizontalAlignment.Text = string.Empty;
				txtGridViewPriority.Text = string.Empty;
				txtDetailsViewPriority.Text = string.Empty;
				txtApplicationEntityFieldLabelMode.Text = string.Empty;
				
			}
		}

		protected void drpSystemEntityType_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtSystemEntityTypeId.Text = drpSystemEntityTypeList.SelectedItem.Value;
		}

		protected void ddlApplicationEntityFieldLabelMode_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtApplicationEntityFieldLabelMode.Text = ddlApplicationEntityFieldLabelMode.SelectedItem.Value;
		}

		private void SetupDropdown()
		{
			var isTesting = SessionVariables.IsTesting;
            var systemEntityTypeData = Framework.Components.Core.SystemEntityType.GetList(SessionVariables.AuditId);
            LoadDropDown(systemEntityTypeData, drpSystemEntityTypeList, Framework.Components.Core.SystemEntityType.DataColumns.EntityName, Framework.Components.Core.SystemEntityType.DataColumns.SystemEntityTypeId);

			var AEFLModeData = Framework.Components.UserPreference.ApplicationEntityFieldLabelMode.GetList(SessionVariables.AuditId);
			LoadDropDown(AEFLModeData, ddlApplicationEntityFieldLabelMode, Framework.Components.UserPreference.ApplicationEntityFieldLabelMode.DataColumns.Name, Framework.Components.UserPreference.ApplicationEntityFieldLabelMode.DataColumns.ApplicationEntityFieldLabelModeId);

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
				ddlApplicationEntityFieldLabelMode.AutoPostBack = true;
				if (ddlApplicationEntityFieldLabelMode.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtApplicationEntityFieldLabelMode.Text.Trim()))
					{
						ddlApplicationEntityFieldLabelMode.SelectedValue = txtApplicationEntityFieldLabelMode.Text;
					}
					else
					{
						txtApplicationEntityFieldLabelMode.Text = ddlApplicationEntityFieldLabelMode.SelectedItem.Value;
					}
				}
				txtApplicationEntityFieldLabelMode.Visible = true;
			}
			else
			{
				if (!string.IsNullOrEmpty(txtSystemEntityTypeId.Text.Trim()))
				{
					drpSystemEntityTypeList.SelectedValue = txtSystemEntityTypeId.Text;
				}
				if (!string.IsNullOrEmpty(txtApplicationEntityFieldLabelMode.Text.Trim()))
				{
					ddlApplicationEntityFieldLabelMode.SelectedValue = txtApplicationEntityFieldLabelMode.Text;
				}
                txtSystemEntityTypeId.Visible = false;
                txtApplicationEntityFieldLabelMode.Visible = false;
			}
		}

		private void LoadDropDown(DataTable dt, DropDownList drpSource, string textField, string valueField)
		{
			drpSource.DataSource = dt;
			drpSource.DataTextField = textField;
			drpSource.DataValueField = valueField;


			drpSource.DataBind();
			drpSource.SelectedIndex = -1;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
				SetupDropdown();
			var isTesting = SessionVariables.IsTesting;
			txtApplicationEntityFieldLabelId.Visible = isTesting;
			lblApplicationEntityFieldLabelId.Visible = isTesting;
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			var isTesting = SessionVariables.IsTesting;

			if (isTesting == true)
			{
				EnableControl(true, dynApplicationEntityFieldLabelId.Controls);
			}
			else
			{
				EnableControl(false, dynApplicationEntityFieldLabelId.Controls);
			}
		}

		private void EnableControl(bool enabled, ControlCollection controls)
		{
			foreach (Control childControl in controls)
			{
				try
				{
					var webChildControl = (WebControl)childControl;
					webChildControl.Enabled = enabled;
				}
				catch
				{

				}
				finally
				{
					EnableControl(enabled, childControl.Controls);
				}
			}
		}
	}
}