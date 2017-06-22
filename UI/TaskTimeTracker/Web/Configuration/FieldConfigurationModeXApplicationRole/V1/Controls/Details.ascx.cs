using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;


namespace Shared.UI.Web.Configuration.FieldConfigurationModeXApplicationRole.V1.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {
        #region private methods

        protected override void ShowData(int FieldConfigurationModeXApplicationRoleId)
        {
			base.ShowData(FieldConfigurationModeXApplicationRoleId);

            oDetailButtonPanel.SetId = SetId;

			Clear();

            var data = new FieldConfigurationModeXApplicationRoleDataModel();
            data.FieldConfigurationModeXApplicationRoleId = FieldConfigurationModeXApplicationRoleId;

			var entityList = FieldConfigurationModeXApplicationRoleDataManager.GetEntityList(data, SessionVariables.RequestProfile);

			if (entityList.Count == 1)
			{
				foreach (var entityItem in entityList)
				{

					lblFieldConfigurationModeXApplicationRoleId.Text = entityItem.FieldConfigurationModeXApplicationRoleId.ToString();
					lblFieldConfigurationMode.Text                   = entityItem.FieldConfigurationMode;
					lblApplicationRole.Text                          = entityItem.ApplicationRole;
                    lblFieldConfigurationModeAccessMode.Text         = entityItem.FieldConfigurationModeAccessMode;

					oUpdateInfo.LoadText(entityItem.UpdatedDate, entityItem.UpdatedBy, entityItem.LastAction);

					oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.FieldConfigurationModeXApplicationRole, FieldConfigurationModeXApplicationRoleId, "FieldConfigurationModeXApplicationRole");
					dynAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "FieldConfigurationModeXApplicationRole");
				}
			}
        }

        protected override void Clear()
        {
            lblFieldConfigurationModeXApplicationRoleId.Text = String.Empty;
			lblApplicationRole.Text                          = String.Empty;
            lblFieldConfigurationMode.Text                   = String.Empty;
            lblFieldConfigurationModeAccessMode.Text         = string.Empty;
        }

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
                LabelListCore = new List<Label>(new Label[] { lblFieldConfigurationModeXApplicationRoleIdText, lblFieldConfigurationModeText, lblApplicationRoleText, lblFieldConfigurationModeAccessModeText });
			}

			return LabelListCore;
		}

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                lblFieldConfigurationModeXApplicationRoleIdText.Visible = isTesting;
                lblFieldConfigurationModeXApplicationRoleId.Visible = isTesting;
            }

			PopulateLabelsText();
        }

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			DictionaryLabel         = CacheConstants.FieldConfigurationModeXApplicationRoleLabelDictionary;
			PrimaryEntity           = Framework.Components.DataAccess.SystemEntity.FieldConfigurationModeXApplicationRole;

			PlaceHolderCore         = dynFieldConfigurationModeXApplicationRoleId;
			PlaceHolderAuditHistory = dynAuditHistory;
			MainTable               = tblMain1;
			BorderDiv               = borderdiv;

		}

        #endregion

    }
}