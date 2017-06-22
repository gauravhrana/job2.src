using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.Task;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;

namespace ApplicationContainer.UI.Web.WBS.TaskXActivityInstance.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
	{

		#region private methods

		protected override void ShowData(int taskXActivityInstanceId)
		{
			base.ShowData(taskXActivityInstanceId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new TaskXActivityInstanceDataModel();
			data.TaskXActivityInstanceId = taskXActivityInstanceId;

            var items = TaskTimeTracker.Components.BusinessLayer.Task.TaskXActivityInstanceDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			
			// should only have single match 
			if (items.Count == 1)
			{
				var item = items[0];

				lblTaskXActivityInstanceId.Text = item.TaskXActivityInstanceId.ToString();
				lblTask.Text					= item.TaskId.ToString();
				lblActivity.Text				= item.ActivityId.ToString();
				lblName.Text 					= item.Name;
				lblDescription.Text				= item.Description;
				lblSortOrder.Text				= item.SortOrder.ToString();

				oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

				oHistoryList.Setup(PrimaryEntity, taskXActivityInstanceId, "TaskXActivityInstance");
			}
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblTaskXActivityInstanceId,lblTaskText,lblActivityText, lblNameText, lblDescriptionText, lblSortOrderText });
			}

			return LabelListCore;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.TaskXActivityInstanceLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskXActivityInstance;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynTaskXActivityInstanceId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
		}

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblTaskXActivityInstanceIdText.Visible = isTesting;
				lblTaskXActivityInstanceId.Visible = isTesting;
			}

			PopulateLabelsText();
		}

		#endregion		

	}

}