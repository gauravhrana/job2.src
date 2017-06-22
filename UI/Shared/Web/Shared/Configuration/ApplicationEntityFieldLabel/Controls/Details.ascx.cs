using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;


namespace Shared.UI.Web.Configuration.ApplicationEntityFieldLabel.Controls
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

        private void ShowData(int applicationEntityFieldLabelId)
        {
            oDetailButtonPanel.SetId = SetId;
            var data = new Framework.Components.UserPreference.ApplicationEntityFieldLabel.Data();
            data.ApplicationEntityFieldLabelId = applicationEntityFieldLabelId;

            var dt = Framework.Components.UserPreference.ApplicationEntityFieldLabel.GetDetails(data, AuditId);

            if (dt.Rows.Count == 1)
            {
                var row = dt.Rows[0];

                lblApplicationEntityFieldLabelId.Text	 = Convert.ToString(row[Framework.Components.UserPreference.ApplicationEntityFieldLabel.DataColumns.ApplicationEntityFieldLabelId]);
                lblName.Text							 = Convert.ToString(row[Framework.Components.UserPreference.ApplicationEntityFieldLabel.DataColumns.Name]);
                lblValue.Text							 = Convert.ToString(row[Framework.Components.UserPreference.ApplicationEntityFieldLabel.DataColumns.Value]);
                lblSystemEntityTypeId.Text				 = Convert.ToString(row[Framework.Components.UserPreference.ApplicationEntityFieldLabel.DataColumns.SystemEntityTypeId]);
                lblWidth.Text							 = Convert.ToString(row[Framework.Components.UserPreference.ApplicationEntityFieldLabel.DataColumns.Width]);
                lblFormatting.Text						 = Convert.ToString(row[Framework.Components.UserPreference.ApplicationEntityFieldLabel.DataColumns.Formatting]);
                lblControlType.Text						 = Convert.ToString(row[Framework.Components.UserPreference.ApplicationEntityFieldLabel.DataColumns.ControlType]);
                lblHorizontalAlignment.Text				 = Convert.ToString(row[Framework.Components.UserPreference.ApplicationEntityFieldLabel.DataColumns.HorizontalAlignment]);

                oUpdateInfo.LoadText(dt.Rows[0]);

                oHistoryList.Setup("AuditHistory", "Audit", "AuditHistoryId", true, true, (int)Framework.Components.DataAccess.SystemEntity.ApplicationEntityFieldLabel, applicationEntityFieldLabelId, "ApplicationEntityFieldLabel");
                dynAuditHistory.Visible = ApplicationCommon.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "ApplicationEntityFieldLabel");
            }
            else
            {
                Clear();
            }
        }

        private void Clear()
        {
            lblApplicationEntityFieldLabelId.Text = string.Empty;
            lblName.Text = string.Empty;
            lblValue.Text = string.Empty;
            lblSystemEntityTypeId.Text = string.Empty;
            lblWidth.Text = string.Empty;
            lblFormatting.Text = string.Empty;
            lblControlType.Text = string.Empty;
            lblHorizontalAlignment.Text = string.Empty;
        }

        private void PopulateLabelsText()
        {
            var validColumns = new Dictionary<string, string>();
            var labelslist = new List<Label>(new Label[] { lblApplicationEntityFieldLabelIdText
													  , lblNameText, lblValueText, lblSystemEntityTypeIdText
													  , lblWidthText, lblFormattingText, lblControlTypeText, lblHorizontalAlignmentText});
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
                lblApplicationEntityFieldLabelIdText.Visible = isTesting;
                lblApplicationEntityFieldLabelId.Visible = isTesting;
            }
            PopulateLabelsText();
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

        #endregion

    }
}