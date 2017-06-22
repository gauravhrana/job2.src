using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;


namespace Shared.UI.Web.Configuration.ApplicationEntityFieldLabelMode.Controls
{
	public partial class Details : Shared.UI.WebFramework.BaseControl
	{

		#region variables

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

		private int _setId;

		public int SetId
		{
			set
			{
				_setId = value;
				ShowData(_setId);
			}
			get
			{
				return _setId;
			}
		}

		public string BackGroundColor
		{
			set
			{
				tblMain1.BgColor = value;
			}
			get
			{
				return tblMain1.BgColor;
			}
		}

		public string BorderClass
		{
			set
			{
				borderdiv.Attributes["class"] = value;
			}
		}

		#endregion

		#region private methods

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

		private void ShowData(int applicationEntityFieldLabelModeId)
		{
			oDetailButtonPanel.SetId = SetId;
			var data = new Framework.Components.UserPreference.ApplicationEntityFieldLabelMode.Data();
			data.ApplicationEntityFieldLabelModeId = applicationEntityFieldLabelModeId;

			var dt = Framework.Components.UserPreference.ApplicationEntityFieldLabelMode.GetDetails(data, AuditId);

			if (dt.Rows.Count == 1)
			{
				var row = dt.Rows[0];

				lblApplicationEntityFieldLabelModeId.Text	 = Convert.ToString(row[Framework.Components.UserPreference.ApplicationEntityFieldLabelMode.DataColumns.ApplicationEntityFieldLabelModeId]);
				lblName.Text								 = Convert.ToString(row[Framework.Components.UserPreference.ApplicationEntityFieldLabelMode.DataColumns.Name]);
				lblDescription.Text							 = Convert.ToString(row[Framework.Components.UserPreference.ApplicationEntityFieldLabelMode.DataColumns.Description]);
				lblSortOrder.Text							 = Convert.ToString(row[Framework.Components.UserPreference.ApplicationEntityFieldLabelMode.DataColumns.SortOrder]);

                oUpdateInfo.LoadText(dt.Rows[0]);

				oHistoryList.Setup("AuditHistory", "Audit", "AuditHistoryId", true, true, (int)Framework.Components.DataAccess.SystemEntity.ApplicationEntityFieldLabelMode, applicationEntityFieldLabelModeId, "ApplicationEntityFieldLabelMode");
				dynAuditHistory.Visible = ApplicationCommon.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "ApplicationEntityFieldLabelMode");
			}
			else
			{
				Clear();
			}
		}

		private void Clear()
		{
			lblApplicationEntityFieldLabelModeId.Text = string.Empty;
			lblName.Text = string.Empty;
			lblDescription.Text = string.Empty;
			lblSortOrder.Text = string.Empty;
		}

		private void PopulateLabelsText()
		{
			var validColumns = new Dictionary<string, string>();
			var labelslist = new List<Label>(new Label[] { lblApplicationEntityFieldLabelModeIdText
													  , lblNameText, lblDescriptionText, lblSortOrderText});
			if (Cache[CacheConstants.ApplicationEntityFieldLabelLabelDictionary] == null)
			{
				validColumns = UIHelper.GetLabelDictonaryObject((int)Framework.Components.DataAccess.SystemEntity.ApplicationEntityFieldLabel, AuditId);
				Cache.Add(CacheConstants.ApplicationEntityFieldLabelLabelDictionary, validColumns, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 60), System.Web.Caching.CacheItemPriority.Default, null);

			}
			else
			{
				validColumns = (Dictionary<string, string>)Cache[CacheConstants.ApplicationEntityFieldLabelLabelDictionary];
			}
			UIHelper.PopulateLabelsText(validColumns, (int)Framework.Components.DataAccess.SystemEntity.ApplicationEntityFieldLabel, AuditId, labelslist);


		}

		#endregion

		#region Eventes

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblApplicationEntityFieldLabelModeIdText.Visible = isTesting;
				lblApplicationEntityFieldLabelModeId.Visible = isTesting;
			}
			PopulateLabelsText();
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

		#endregion

	}
}