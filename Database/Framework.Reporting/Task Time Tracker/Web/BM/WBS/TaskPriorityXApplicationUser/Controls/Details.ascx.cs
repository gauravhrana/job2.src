using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;
using TaskTimeTracker.Components.Module.Priority;

namespace ApplicationContainer.UI.Web.WBS.TaskPriorityXApplicationUser.Controls
{
	public partial class Details : ControlDetails
	{

		#region private methods

		protected override void ShowData(int taskPriorityXApplicationUserId)
		{
			base.ShowData(taskPriorityXApplicationUserId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new TaskPriorityXApplicationUserDataManager.Data();
			data.TaskPriorityXApplicationUserId = taskPriorityXApplicationUserId;

            var items = TaskPriorityXApplicationUserDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			if (items.Count == 1)
			{
				var item = items[0];

				lblTaskPriorityXApplicationUserId.Text = item.TaskPriorityXApplicationUserId.ToString();
				lblTask.Text = item.TaskId.ToString();
				lblApplicationUser.Text = item.ApplicationUserId.ToString();
				lblTaskPriorityType.Text = item.TaskPriorityTypeId.ToString();				

				oHistoryList.Setup(PrimaryEntity, taskPriorityXApplicationUserId, "TaskPriorityXApplicationUser");
			}
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblTaskPriorityXApplicationUserIdText, lblTaskText, lblTaskPriorityTypeText, 
													  lblApplicationUserText });
			}

			return LabelListCore;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			DictionaryLabel = CacheConstants.TaskPriorityXApplicationUserLabelDictionary;
			PrimaryEntity = SystemEntity.TaskPriorityXApplicationUser;

			base.OnInit(e);

			PlaceHolderCore = dynTaskPriorityXApplicationUserId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
		}

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblTaskPriorityXApplicationUserIdText.Visible = isTesting;
				lblTaskPriorityXApplicationUserId.Visible = isTesting;
			}

			PopulateLabelsText();
		}

		#endregion
		
	}

}