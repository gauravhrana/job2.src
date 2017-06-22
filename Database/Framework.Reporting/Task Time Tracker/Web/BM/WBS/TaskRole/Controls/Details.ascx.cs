using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Shared.WebCommon.UI.Web;
//using ApplicationContainer.UI.Web.CommonCode;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.TaskRole.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {

        #region private methods

        protected override void ShowData(int taskRoleId)
        {
            base.ShowData(taskRoleId);

            oDetailButtonPanel.SetId = SetId;

            Clear();

            var data = new TaskRoleDataModel();
            data.TaskRoleId = taskRoleId;

            var items = TaskTimeTracker.Components.BusinessLayer.Task.TaskRoleDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            // should only have single match 
            if (items.Count == 1)
            {
                var item = items[0];
                lblTaskRoleId.Text = item.TaskRoleId.ToString();
                lblName.Text = item.Name;
                lblDescription.Text = item.Description;
                lblSortOrder.Text = item.SortOrder.ToString();

                oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

                oHistoryList.Setup(PrimaryEntity, taskRoleId, "TaskRole");
            }
        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblTaskRoleIdText, lblNameText, lblDescriptionText, lblSortOrderText });
            }

            return LabelListCore;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            // set basic variables
            DictionaryLabel = Shared.UI.Web.CacheConstants.TaskRoleLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskRole;

            base.OnInit(e);

            // set object variable reference            
            PlaceHolderCore = dynTaskRoleId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                lblTaskRoleIdText.Visible = isTesting;
                lblTaskRoleId.Visible = isTesting;
            }

            PopulateLabelsText();
        }

        #endregion

    }

}