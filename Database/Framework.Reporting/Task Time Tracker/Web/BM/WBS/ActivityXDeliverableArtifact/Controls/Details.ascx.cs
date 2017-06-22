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

namespace ApplicationContainer.UI.Web.ActivityXDeliverableArtifact.Controls
{
    public partial class Details : ControlDetails
    {
        #region private methods

        protected override void ShowData(int activityXDeliverableArtifactId)
        {
			base.ShowData(activityXDeliverableArtifactId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var dataQuery = new ActivityXDeliverableArtifactDataModel();
			dataQuery.ActivityXDeliverableArtifactId = activityXDeliverableArtifactId;

            var entityList = ActivityXDeliverableArtifactDataManager.GetEntityList(dataQuery, SessionVariables.RequestProfile);
			if (entityList.Count == 1)
			{
				foreach (var entityItem in entityList)
				{
					lblActivityXDeliverableArtifactId.Text = entityItem.ActivityXDeliverableArtifactId.ToString();
					lblActivity.Text = entityItem.Activity.ToString();
					lblDeliverableArtifacts.Text = entityItem.DeliverableArtifact.ToString();
					lblDeliverableArtifactsStatus.Text = entityItem.DeliverableArtifactStatus.ToString();

					oUpdateInfo.LoadText(entityItem.UpdatedDate, entityItem.UpdatedBy, entityItem.LastAction);

					oHistoryList.Setup((int)SystemEntity.ActivityXDeliverableArtifact, activityXDeliverableArtifactId, "NotificationEventType");

				}
			}

        }

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblActivityXDeliverableArtifactIdText, lblActivityText, lblDeliverableArtifactsText, lblDeliverableArtifactsStatusText });
			}

			return LabelListCore;
		}


        #endregion

        #region Events

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.ActivityXDeliverableArtifactLabelDictionary;
			PrimaryEntity = SystemEntity.ActivityXDeliverableArtifact;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynActivityXDeliverableArtifactId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
		}

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                lblActivityXDeliverableArtifactIdText.Visible = isTesting;
                lblActivityXDeliverableArtifactId.Visible = isTesting;
            }

			PopulateLabelsText();
        }

        

        #endregion
    }
}