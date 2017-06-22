using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Framework.Components.Core;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;


namespace Shared.UI.Web.ApplicationManagement.HelpPageContext.Controls
{
	public partial class Details : ControlDetails
	{

        #region variables

        


		#endregion

		#region private methods

		protected override void ShowData(int HelpPageContextId)
        {
            oDetailButtonPanel.SetId = SetId;
			var data = new HelpPageContextDataModel();
			data.HelpPageContextId = HelpPageContextId;

			var dt = HelpPageContextDataManager.GetDetails(data, SessionVariables.RequestProfile);

			if (dt != null)
			{

                lblHelpPageContextId.Text	   = dt.HelpPageContextId.ToString();
                lblName.Text                   = dt.Name;
                lblDescription.Text            = dt.Description;
                lblSortOrder.Text              = dt.SortOrder.ToString();

                oUpdateInfo.LoadText(dt);

				oHistoryList.Setup((int)SystemEntity.HelpPageContext, HelpPageContextId, "HelpPageContext");
				dynAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "Client");
			}
			else
			{
				Clear();
			}
		}

		protected override void Clear()
		{
			lblHelpPageContextId.Text = String.Empty;
			lblName.Text = String.Empty;
			lblDescription.Text = String.Empty;
			lblSortOrder.Text = String.Empty;
		}

		protected override void PopulateLabelsText()
		{
			var validColumns = new Dictionary<string, string>();
			var labelslist = new List<Label>(new Label[] { lblHelpPageContextIdText
													  , lblNameText, lblDescriptionText, lblSortOrderText});
			if (Cache[CacheConstants.HelpPageContextLabelDictionary] == null)
			{
				validColumns = UIHelper.GetLabelDictonaryObject((int)SystemEntity.HelpPageContext, SessionVariables.RequestProfile.AuditId);
				Cache.Add(CacheConstants.SystemEntityTypeLabelDictionary, validColumns, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 60), CacheItemPriority.Default, null);

			}
			else
			{
				validColumns = (Dictionary<string, string>)Cache[CacheConstants.HelpPageContextLabelDictionary];
			}
			UIHelper.PopulateLabelsText(validColumns, (int)SystemEntity.HelpPageContext, SessionVariables.RequestProfile.AuditId, labelslist);


		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblHelpPageContextIdText.Visible = isTesting;
				lblHelpPageContextId.Visible = isTesting;
			}
			PopulateLabelsText();
		}

		

		#endregion

	}
}