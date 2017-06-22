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


namespace ApplicationContainer.UI.Web.TaskXDeliverableArtifact.Controls
{
    public partial class Details : ControlDetails
    {
        #region private methods

        protected override void ShowData(int taskXDeliverableArtifactId)
        {
			base.ShowData(taskXDeliverableArtifactId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var dataQuery = new TaskXDeliverableArtifactDataModel();
			dataQuery.TaskXDeliverableArtifactId = taskXDeliverableArtifactId;

            var entityList = TaskXDeliverableArtifactDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);
			if (entityList.Count == 1)
			{
				foreach (var entityItem in entityList)
				{
					lblTaskXDeliverableArtifactId.Text = entityItem.TaskXDeliverableArtifactId.ToString();
					lblTask.Text = entityItem.Task.ToString();
					lblDeliverableArtifacts.Text = entityItem.DeliverableArtifact.ToString();
					lblDeliverableArtifactStatus.Text = entityItem.DeliverableArtifactStatus.ToString();

					oUpdateInfo.LoadText(entityItem.UpdatedDate, entityItem.UpdatedBy, entityItem.LastAction);

					oHistoryList.Setup((int)SystemEntity.TaskXDeliverableArtifact, taskXDeliverableArtifactId, "TaskXDeliverableArtifact");

				}
			}
        }

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblTaskXDeliverableArtifactIdText, lblTaskText, lblDeliverableArtifactsText, lblDeliverableArtifactStatusText });
			}

			return LabelListCore;
		}
        #endregion

        #region Events

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.TaskXDeliverableArtifactLabelDictionary;
			PrimaryEntity = SystemEntity.TaskXDeliverableArtifact;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynTaskXDeliverableArtifactId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
		}

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                lblTaskXDeliverableArtifactIdText.Visible = isTesting;
                lblTaskXDeliverableArtifactId.Visible = isTesting;
            }
			PopulateLabelsText();
        }

        

        #endregion
    }
}