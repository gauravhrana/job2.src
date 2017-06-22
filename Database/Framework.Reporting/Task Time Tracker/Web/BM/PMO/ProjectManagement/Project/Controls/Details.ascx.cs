using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.ProjectManagement.Project.Controls
{
    public partial class Details : ControlDetailsStandard
    {

        #region private methods

        protected override void ShowData(int projectId)
        {
            base.ShowData(projectId);

            oDetailButtonPanel.SetId = SetId;

            Clear();

            var data = new ProjectDataModel();
            data.ProjectId = projectId;

            var items = ProjectDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

                SetData(item);

                oHistoryList.Setup(PrimaryEntity, projectId, "Project");
            }
        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblProjectIdText, lblNameText, lblDescriptionText, lblSortOrderText });
            }

            return LabelListCore;
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new ProjectDataModel();

            SetData(data);
        }

        public void SetData(ProjectDataModel item)
        {
            SystemKeyId = item.ProjectId;

            base.SetData(item);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DictionaryLabel = CacheConstants.ProjectLabelDictionary;
            PrimaryEntity = SystemEntity.Project;

            PlaceHolderCore = dynProjectId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;

            CoreSystemKey = lblProjectId;
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
                lblProjectIdText.Visible = isTesting;
                CoreSystemKey.Visible = isTesting;
            }

            PopulateLabelsText();
        }

        #endregion

    }
}