using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;
using Framework.Components.Audit;
using ApplicationContainer.UI.Web.CommonCode;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.Admin.Audit.AuditHistory.Controls
{
    public partial class Details : ControlDetails
    {
        #region private methods

        protected override void ShowData(int auditHistoryId)
        {
			base.ShowData(auditHistoryId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var dataQuery = new DataModel.Framework.Audit.AuditHistory();
			dataQuery.AuditHistoryId = auditHistoryId;

			var entityList = AuditHistoryDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

			if (entityList.Count == 1)
			{
				foreach (var entityItem in entityList)
				{
					lblAuditHistoryId.Text = entityItem.AuditHistoryId.ToString();
					lblSystemEntityId.Text = entityItem.SystemEntityId.ToString();
					lblEntityKey.Text = entityItem.EntityKey.ToString();
					lblAuditActionId.Text = entityItem.AuditActionId.ToString();
					lblCreatedDate.Text = entityItem.CreatedDate.ToString();
					lblCreatedByPersonId.Text = entityItem.Person.ToString();
				}
			}
            
        }

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblAuditHistoryIdText, lblSystemEntityIdText, lblEntityKeyText, lblAuditActionIdText, lblCreatedDateText, lblCreatedByPersonIdText });
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
				lblAuditHistoryIdText.Visible = isTesting;
				lblAuditHistoryId.Visible = isTesting;
			}
			PopulateLabelsText();
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			// set basic variables
			DictionaryLabel			= CacheConstants.AuditHistoryLabelDictionary;
			PrimaryEntity			= Framework.Components.DataAccess.SystemEntity.AuditHistory;			

			// set object variable reference            
			PlaceHolderCore			= dynAuditHistoryId;
			PlaceHolderAuditHistory = dynAuditHistory;

			BorderDiv				= borderdiv;
		}

		#endregion  

    }
}