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
using TaskTimeTracker.Components.BusinessLayer;
using ApplicationContainer.UI.Web.CommonCode;

namespace ApplicationContainer.UI.Web.Milestone.Controls
{
    public partial class Details : ControlDetailsStandard
    {        

        #region private methods
        
		protected override void ShowData(int milestoneId)
		{
			base.ShowData(milestoneId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new MilestoneDataModel();
			data.MilestoneId = milestoneId;

            var items = MilestoneDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);
			
			// should only have single match 
			if (items.Count == 1)
			{
					var item = items[0];	

					SetData(item);

					lblProjectId.Text = item.Project.ToString();

					oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

					oHistoryList.Setup((int)SystemEntity.Milestone, milestoneId, "Milestone");

				
			}
	  }

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblMilestoneIdText, lblNameText, lblDescriptionText, lblSortOrderText,lblProjectIdText});
			}

			return LabelListCore;
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new MilestoneDataModel();

			SetData(data);
		}

		public void SetData(MilestoneDataModel item)
		{
			SystemKeyId = item.MilestoneId;

			base.SetData(item);
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblMilestoneIdText.Visible = isTesting;
				lblMilestoneId.Visible = isTesting;
			}
			PopulateLabelsText();
		}

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.MilestoneLabelDictionary;
			PrimaryEntity = SystemEntity.Milestone;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynMilestoneId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;

			CoreSystemKey = lblMilestoneId;
			CoreControlName = lblName;
			CoreControlDescription = lblDescription;
			CoreControlSortOrder = lblSortOrder;

			CoreUpdateInfo = oUpdateInfo;
		}

		#endregion

	}

}