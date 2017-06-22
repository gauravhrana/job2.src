using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.Feature;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;

namespace ApplicationContainer.UI.Web.RunTimeFeature.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
	{
		#region private methods

		protected override void ShowData(int runTimeFeatureId)
		{
			base.ShowData(runTimeFeatureId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new RunTimeFeatureDataModel();
			data.RunTimeFeatureId = runTimeFeatureId;

            var items = TaskTimeTracker.Components.BusinessLayer.Feature.RunTimeFeatureDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match 
			if (items.Count == 1)
			{
				var item = items[0];

				lblRunTimeFeatureId.Text = item.RunTimeFeatureId.ToString();
				lblName.Text = item.Name;
				lblDescription.Text = item.Description;
				lblSortOrder.Text = item.SortOrder.ToString();

				oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

				oHistoryList.Setup(PrimaryEntity, runTimeFeatureId, "RunTimeFeature");
			}
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblRunTimeFeatureIdText, lblNameText, lblDescriptionText, lblSortOrderText });
			}

			return LabelListCore;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.RunTimeFeatureLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.RunTimeFeature;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynRunTimeFeatureId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
		}

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblRunTimeFeatureIdText.Visible = isTesting;
				lblRunTimeFeatureId.Visible = isTesting;
			}

			PopulateLabelsText();
		}

		#endregion

		
	}

}