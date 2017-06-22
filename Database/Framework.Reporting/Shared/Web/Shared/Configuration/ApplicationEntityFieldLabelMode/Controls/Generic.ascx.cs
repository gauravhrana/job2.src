using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;


namespace Shared.UI.Web.Configuration.ApplicationEntityFieldLabelMode.Controls
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

		public int? ApplicationEntityFieldLabelModeId
		{
			get
			{
				if (txtApplicationEntityFieldLabelModeId.Enabled)
				{
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtApplicationEntityFieldLabelModeId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtApplicationEntityFieldLabelModeId.Text);
				}
			}
		}


		public string Name
		{
			get
			{
				return txtName.Text; ;
			}
		}

		public string Description
		{
			get
			{
				return Framework.Components.DefaultDataRules.CheckAndGetDescription(txtName.Text, txtDescription.InnerText);
			}
		}

		public int? SortOrder
		{
			get
			{
				if (txtSortOrder.Text == "")
				{
					return null;
				}
				else
				{

					return int.Parse(txtSortOrder.Text);
				}

			}

		}
		
		#endregion properties

		protected override string ValidationConfigFile
		{
			get
			{
				return Server.MapPath("~/Shared/Configuration/ApplicationEntityFieldLabelMode/Controls/Validation.xml"); //"R:\ApplicationEntityFieldLabels\Task Time Tracker\Dev\UI\Web\ValidationDataFile\dvc.xml";
			}
		}

		public void SetId(int setId, bool chkApplicationEntityFieldLabelModeId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkApplicationEntityFieldLabelModeId);
			txtApplicationEntityFieldLabelModeId.Enabled = chkApplicationEntityFieldLabelModeId;
			//txtDescription.Enabled = !chkApplicationEntityFieldLabelModeId;
			//txtName.Enabled = !chkApplicationEntityFieldLabelModeId;
			//txtSortOrder.Enabled = !chkApplicationEntityFieldLabelModeId;
		}

		public void LoadData(int ApplicationEntityFieldLabelModeId, bool showId)
		{
			var data = new Framework.Components.UserPreference.ApplicationEntityFieldLabelMode.Data();
			data.ApplicationEntityFieldLabelModeId = ApplicationEntityFieldLabelModeId;
			var oApplicationEntityFieldLabelModeTable = Framework.Components.UserPreference.ApplicationEntityFieldLabelMode.GetDetails(data, AuditId);

			if (oApplicationEntityFieldLabelModeTable.Rows.Count == 1)
			{
				var row = oApplicationEntityFieldLabelModeTable.Rows[0];

				if (!showId)
				{

					txtApplicationEntityFieldLabelModeId.Text = row[Framework.Components.UserPreference.ApplicationEntityFieldLabelMode.DataColumns.ApplicationEntityFieldLabelModeId].ToString();

					// only show Audit History in case of Update page, not for Clone.
					oHistoryList.Setup("AuditHistory", "Audit", "AuditHistoryId", true, true, (int)Framework.Components.DataAccess.SystemEntity.ApplicationEntityFieldLabelMode, ApplicationEntityFieldLabelModeId, "ApplicationEntityFieldLabel");
					dynAuditHistory.Visible = ApplicationCommon.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "ApplicationEntityFieldLabelMode");
				}
				else
				{
					txtApplicationEntityFieldLabelModeId.Text = string.Empty;
				}
				txtName.Text		 = row[Framework.Components.UserPreference.ApplicationEntityFieldLabelMode.DataColumns.Name].ToString();
				txtDescription.Value = row[Framework.Components.UserPreference.ApplicationEntityFieldLabelMode.DataColumns.Description].ToString();
				txtSortOrder.Text	 = row[Framework.Components.UserPreference.ApplicationEntityFieldLabelMode.DataColumns.SortOrder].ToString();

				oUpdateInfo.LoadText(oApplicationEntityFieldLabelModeTable.Rows[0]);
			}
			else
			{
				txtApplicationEntityFieldLabelModeId.Text = string.Empty;
				txtName.Text = string.Empty;
				txtDescription.Value = string.Empty;
				txtSortOrder.Text = string.Empty;
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
			
			var isTesting = SessionVariables.IsTesting;
			txtApplicationEntityFieldLabelModeId.Visible = isTesting;
			lblApplicationEntityFieldLabelModeId.Visible = isTesting;
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			var isTesting = SessionVariables.IsTesting;

			if (isTesting == true)
			{
				EnableControl(true, dynApplicationEntityFieldLabelModeId.Controls);
			}
			else
			{
				EnableControl(false, dynApplicationEntityFieldLabelModeId.Controls);
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