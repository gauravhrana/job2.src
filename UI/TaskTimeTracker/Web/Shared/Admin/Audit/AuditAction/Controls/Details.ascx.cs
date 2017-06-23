using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;


namespace Shared.UI.Web.Admin.Audit.AuditAction.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetailsStandard
    {
		#region private methods

		protected override void ShowData(int auditActionId)
		{
			base.ShowData(auditActionId);

			oDetailButtonPanel.SetId = SetId;

			Clear();			

			var data = new DataModel.Framework.Audit.AuditActionDataModel();
			data.AuditActionId = auditActionId;
			var items = Framework.Components.Audit.AuditActionDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match 
			if (items.Count == 1)
			{
				var item = items[0];

				SetData(item);

				oHistoryList.Setup(PrimaryEntity, auditActionId, "AuditAction");
			}
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblAuditActionIdText, lblNameText, lblDescriptionText, lblSortOrderText });
			}

			return LabelListCore;
		}

		protected override void Clear()
		{
			base.Clear();

            var data = new DataModel.Framework.Audit.AuditActionDataModel();

			SetData(data);
		}

        public void SetData(DataModel.Framework.Audit.AuditActionDataModel item)
		{
			SystemKeyId = item.AuditActionId;

			base.SetData(item);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			// set basic variables
			DictionaryLabel = CacheConstants.AuditActionLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.AuditAction;			

			// set object variable reference            
			PlaceHolderCore = dynAuditActionId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;

			CoreSystemKey = lblAuditActionId;
			CoreControlName = lblName;
			CoreControlDescription = lblDescription;
			CoreControlSortOrder = lblSortOrder;

			CoreUpdateInfo = oUpdateInfo;
		}

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblAuditActionIdText.Visible = isTesting;
				lblAuditActionId.Visible = isTesting;
			}

			PopulateLabelsText();
		}

		#endregion
        
	}

}