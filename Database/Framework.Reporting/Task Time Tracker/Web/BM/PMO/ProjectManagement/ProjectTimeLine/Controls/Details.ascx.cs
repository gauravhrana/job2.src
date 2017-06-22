using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.ProjectManagement.ProjectTimeLine.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {

        #region properties

        #endregion

        #region methods

        protected override void ShowData(int projectTimeLineId)
        {
			base.ShowData(projectTimeLineId);

            oDetailButtonPanel.SetId = SetId;
			
			Clear();

            var data = new ProjectTimeLineDataModel();
            data.ProjectTimeLineId = projectTimeLineId;

            var entityList = ProjectTimeLineDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			if (entityList.Count == 1)
            {
				foreach (var entityItem in entityList)
				{

					lblProjectTimeLineId.Text = entityItem.ProjectTimeLineId.ToString();
					lblProjectId.Text = entityItem.Project.ToString();
					lblStartDate.Text = entityItem.StartDate.ToString();
					lblEndDate.Text = entityItem.EndDate.ToString();

					oUpdateInfo.LoadText(entityItem.UpdatedDate, entityItem.UpdatedBy, entityItem.LastAction);

					oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.ProjectTimeLine, projectTimeLineId, "ProjectTimeLine");
					dynAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "ProjectTimeLine");
				}
            }
           
        }

        protected override void Clear()
        {
			lblProjectTimeLineId.Text = String.Empty;
			lblProjectId.Text = String.Empty;
            lblStartDate.Text = String.Empty;
            lblEndDate.Text = String.Empty;
            
        }		

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblProjectTimeLineIdText, lblProjectIdText, lblStartDateText
													  , lblEndDateText});
			}

			return LabelListCore;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			DictionaryLabel = CacheConstants.ProjectTimeLineLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ProjectTimeLine;

			PlaceHolderCore = dynProjectTimeLineId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
			
		}

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblProjectTimeLineIdText.Visible = isTesting;
				lblProjectTimeLineId.Visible = isTesting;
			}

			PopulateLabelsText();
		}

		

		#endregion

	}

}