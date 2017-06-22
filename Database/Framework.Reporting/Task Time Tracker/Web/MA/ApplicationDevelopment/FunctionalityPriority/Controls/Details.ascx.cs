using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.UI.Web;
using Shared.WebCommon.UI.Web;


namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityPriority.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
	{

        #region variables

        


		#endregion

		#region private methods

		protected override void ShowData(int functionalityPriorityId)
        {
            oDetailButtonPanel.SetId = SetId;
			var data = new FunctionalityPriorityDataModel();
			data.FunctionalityPriorityId = functionalityPriorityId;

			var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityPriorityDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			if (items.Count == 1)
			{
                var item = items[0];

                lblFunctionalityPriorityId.Text = Convert.ToString(item.FunctionalityPriorityId);
                lblName.Text                   = Convert.ToString(item.Name);
                lblDescription.Text            = Convert.ToString(item.Description);
                lblSortOrder.Text              = Convert.ToString(item.SortOrder);

                oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

                oHistoryList.Setup(PrimaryEntity, functionalityPriorityId, "FunctionalityPriority");
			}
			else
			{
				Clear();
			}
		}

		protected override void Clear()
		{
			lblFunctionalityPriorityId.Text = String.Empty;
			lblName.Text = String.Empty;
			lblDescription.Text = String.Empty;
			lblSortOrder.Text = String.Empty;
		}

		protected override void PopulateLabelsText()
		{
			var validColumns = new Dictionary<string, string>();
			var labelslist = new List<Label>(new Label[] { lblFunctionalityPriorityIdText
													  , lblNameText, lblDescriptionText, lblSortOrderText});
			if (Cache[CacheConstants.FunctionalityPriorityLabelDictionary] == null)
			{
				validColumns = UIHelper.GetLabelDictonaryObject((int)Framework.Components.DataAccess.SystemEntity.FunctionalityPriority, SessionVariables.RequestProfile.AuditId);
				Cache.Add(CacheConstants.SystemEntityTypeLabelDictionary, validColumns, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 60), System.Web.Caching.CacheItemPriority.Default, null);

			}
			else
			{
				validColumns = (Dictionary<string, string>)Cache[CacheConstants.FunctionalityPriorityLabelDictionary];
			}
			UIHelper.PopulateLabelsText(validColumns, (int)Framework.Components.DataAccess.SystemEntity.FunctionalityPriority, SessionVariables.RequestProfile.AuditId, labelslist);


		}

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblFunctionalityPriorityIdText, lblNameText, lblDescriptionText, lblSortOrderText });
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
				lblFunctionalityPriorityIdText.Visible = isTesting;
				lblFunctionalityPriorityId.Visible = isTesting;
			}
			PopulateLabelsText();
		}

        protected override void OnInit(EventArgs e)
        {
            // set basic variables
            DictionaryLabel = CacheConstants.FunctionalityPriorityLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FunctionalityPriority;

            base.OnInit(e);

            // set object variable reference            
            PlaceHolderCore = dynFunctionalityPriorityId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;
        }

		#endregion

	}
}