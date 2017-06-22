using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApplicationContainer.UI.Web.CommonCode;
using DataModel.TaskTimeTracker.TimeTracking;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.Scheduling.CustomTimeLog.Controls
{
	public partial class Details : ControlDetails
	{

		#region properties

		
		#endregion

		#region private methods

		protected override void ShowData(int customTimeLogId)
		{
			base.ShowData(customTimeLogId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var dataQuery = new CustomTimeLogDataModel();
			dataQuery.CustomTimeLogId = customTimeLogId;

			var entityList = CustomTimeLogDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

			if (entityList.Count == 1)
			{
				foreach (var entityItem in entityList)
				{
					lblCustomTimeLogId.Text = entityItem.CustomTimeLogId.ToString();
					lblCustomTimeLogKey.Text = entityItem.CustomTimeLogKey;
					lblPersonId.Text = entityItem.Person.ToString();
					lblNoofFilesPromoted.Text = entityItem.Value.ToString();
                    lblCustomTimeCategory.Text = entityItem.CustomTimeCategory;

					oUpdateInfo.LoadText(entityItem.UpdatedDate, entityItem.UpdatedBy, entityItem.LastAction);

					oHistoryList.Setup(PrimaryEntity, customTimeLogId, "CustomTimeLog");
				}
			}
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblCustomTimeLogIdText, lblPersonText, lblCustomTimeCategoryText, lblCustomTimeLogKeyText, lblNoofFilesPromotedText  });
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
				lblCustomTimeLogIdText.Visible = isTesting;
				lblCustomTimeLogId.Visible = isTesting;
			}
			PopulateLabelsText();
		}

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.CustomTimeLogLabelDictionary;
			PrimaryEntity = SystemEntity.CustomTimeLog;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynCustomTimeLogId;
			PlaceHolderAuditHistory = dynAuditHistory;

			BorderDiv = borderdiv;
		}

		#endregion
	}
}