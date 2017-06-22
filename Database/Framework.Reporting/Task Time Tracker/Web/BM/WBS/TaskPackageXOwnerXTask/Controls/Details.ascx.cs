using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;
using DataModel.TaskTimeTracker.Task;

namespace ApplicationContainer.UI.Web.TaskPackageXOwnerXTask.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
	{

		#region private methods

		protected override void ShowData(int taskPackageXOwnerXTaskId)
		{
			oDetailButtonPanel.SetId = SetId;

            var dataQuery = new TaskPackageXOwnerXTaskDataModel();
			dataQuery.TaskPackageXOwnerXTaskId = taskPackageXOwnerXTaskId;

            var entityList = TaskTimeTracker.Components.BusinessLayer.Task.TaskPackageXOwnerXTaskDataManager.GetEntityList(dataQuery, SessionVariables.RequestProfile);

			if (entityList.Count == 1)
			{
				foreach (var entityItem in entityList)
				{
					lblTaskPackageXOwnerXTaskId.Text = entityItem.TaskPackageXOwnerXTaskId.ToString();
					lblApplicationUser.Text			 = entityItem.ApplicationUserId.ToString();
					lblTaskPackage.Text				 = entityItem.TaskPackageId.ToString();
					lblTaskId.Text				     = entityItem.TaskId.ToString();

					oUpdateInfo.LoadText(entityItem.UpdatedDate, entityItem.UpdatedBy, entityItem.LastAction);

					oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.TaskPackageXOwnerXTask, taskPackageXOwnerXTaskId, "TaskPackageXOwnerXTask");					
				}
			}
			
		}

		
		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblTaskPackageXOwnerXTaskIdText,lblTaskText,lblTaskPackageText,lblApplicationUserText });
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
				lblTaskPackageXOwnerXTaskIdText.Visible = isTesting;
				lblTaskPackageXOwnerXTaskId.Visible = isTesting;
			}
			PopulateLabelsText();
		}

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.TaskPackageXOwnerXTaskLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskPackageXOwnerXTask;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynTaskPackageXOwnerXTaskId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
		}

		#endregion

	}
}