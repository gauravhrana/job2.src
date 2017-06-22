using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.UI.Web;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.AllEntityDetailDataManager.Controls
{
	public partial class Details : ControlDetails
	{
		#region methods

		protected override void ShowData(int allEntityDetailId)
		{
			base.ShowData(allEntityDetailId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new AllEntityDetailDataModel();
			data.AllEntityDetailId = allEntityDetailId;

			var entityList = TaskTimeTracker.Components.Module.ApplicationDevelopment.AllEntityDetailDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			if (entityList.Count == 1)
			{
				foreach (var entityItem in entityList)
				{

					lblAllEntityDetailId.Text = entityItem.AllEntityDetailId.ToString();
					lblEntityName.Text = entityItem.EntityName.ToString();
					lblDB_Name.Text = entityItem.DB_Name.ToString();
					lblDB_Project_Name.Text = entityItem.DB_Project_Name.ToString();
					lblComponent_Project_Name.Text = entityItem.Component_Project_Name.ToString();

					oUpdateInfo.LoadText(entityItem.UpdatedDate, entityItem.UpdatedBy, entityItem.LastAction);

					oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.AllEntityDetail, allEntityDetailId, "AllEntityDetail");
					dynAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "AllEntityDetail");
				}
			}

		}

		protected override void Clear()
		{
			lblAllEntityDetailId.Text = String.Empty;
			lblEntityName.Text = String.Empty;
			lblDB_Name.Text = String.Empty;
			lblDB_Project_Name.Text = String.Empty;
			lblComponent_Project_Name.Text = String.Empty;
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblAllEntityDetailIdText, lblEntityNameText, lblDB_NameText
													  , lblDB_Project_NameText,lblComponent_Project_NameText});
			}

			return LabelListCore;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			DictionaryLabel = Shared.UI.Web.CacheConstants.AllEntityDetailLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.AllEntityDetail;

			PlaceHolderCore = dynAllEntityDetailId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;

		}

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblAllEntityDetailIdText.Visible = isTesting;
				lblAllEntityDetailId.Visible = isTesting;
			}

			PopulateLabelsText();
		}



		#endregion
	}
}