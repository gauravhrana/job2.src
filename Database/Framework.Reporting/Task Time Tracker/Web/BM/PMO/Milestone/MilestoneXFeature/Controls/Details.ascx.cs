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

namespace ApplicationContainer.UI.Web.MilestoneXFeature.Controls
{
    public partial class Details : ControlDetails
    {
        #region private methods

		protected override void ShowData(int milestoneXFeatureId)
		{
			base.ShowData(milestoneXFeatureId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var dataQuery = new MilestoneXFeatureDataModel();
			dataQuery.MilestoneXFeatureId = milestoneXFeatureId;

            var entityList = MilestoneXFeatureDataManager.GetEntityList(dataQuery, SessionVariables.RequestProfile);

			if (entityList.Count == 1)
			{
				foreach (var entityItem in entityList)
				{
					lblMilestoneXFeatureId.Text      = entityItem.MilestoneXFeatureId.ToString();
					lblMilestone.Text                = entityItem.Milestone.ToString();
					lblFeature.Text                  = entityItem.Feature.ToString();
					lblMilestoneFeatureState.Text    = entityItem.MilestoneFeatureState.ToString();
					lblMemo.Text = entityItem.Memo.ToString();
					oUpdateInfo.LoadText(entityItem.UpdatedDate, entityItem.UpdatedBy, entityItem.LastAction);

					oHistoryList.Setup((int)SystemEntity.MilestoneXFeature, milestoneXFeatureId, "MilestoneXFeature");

				}
			}
			
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblMilestoneXFeatureIdText, lblMilestoneText, lblMilestoneFeatureStateText, lblFeatureText, lblMemoText });
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
                lblMilestoneXFeatureIdText.Visible = isTesting;
                lblMilestoneXFeatureId.Visible = isTesting;
            }
			PopulateLabelsText();
        }

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.MilestoneXFeatureLabelDictionary;
			PrimaryEntity = SystemEntity.MilestoneXFeature;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynMilestoneXFeatureId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
		}

        #endregion
    }
}