using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.MilestoneXFeatureArchive.Controls
{
    public partial class Details : ControlDetails
    {
        #region private methods        

        protected override void ShowData(int milestoneXFeatureArchiveid)
        {
			base.ShowData(milestoneXFeatureArchiveid);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new MilestoneXFeatureArchiveDataModel();
			data.MilestoneXFeatureArchiveId = milestoneXFeatureArchiveid;

			var items = MilestoneXFeatureArchiveDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match 
			if (items.Count == 1)
			{
				var item = items[0];

				lblMilestoneXFeatureArchiveId.Text = item.MilestoneXFeatureArchiveId.ToString();
				lblFeature.Text = item.Feature;
				lblMilestone.Text = item.Milestone;
				lblMilestoneFeatureState.Text = item.MilestoneFeatureState;
				lblAcknowledgedBy.Text = item.AcknowledgedBy;
				lblAcknowledgedById.Text = item.AcknowledgedById.ToString();
				//lblKnowledgeDate.Text = item.KnowledgeDate.ToString();
				lblMemo.Text = item.Memo;
				lblMilestoneXFeatureId.Text = item.MilestoneXFeatureId.ToString();
				//oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

				oHistoryList.Setup(PrimaryEntity, milestoneXFeatureArchiveid, "MilestoneFeatureState");
			}            
            
        }

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] {lblMilestoneXFeatureIdText, lblMilestoneXFeatureArchiveIdText,lblMilestoneText,lblFeatureText,lblMilestoneFeatureStateText });
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
                lblMilestoneXFeatureArchiveIdText.Visible = isTesting;
                lblMilestoneXFeatureArchiveId.Visible = isTesting;
            }
            PopulateLabelsText();
        }

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.MilestoneXFeatureArchiveLabelDictionary;
			PrimaryEntity = SystemEntity.MilestoneXFeatureArchive;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynMilestoneXFeatureArchiveId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
		}

        #endregion
    }
}