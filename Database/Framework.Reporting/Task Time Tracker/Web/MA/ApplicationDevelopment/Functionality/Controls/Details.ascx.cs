using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.UI.Web;
using Shared.WebCommon.UI.Web;


namespace ApplicationContainer.UI.Web.ApplicationDevelopment.Functionality.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
	{

        #region variables

        


		#endregion

		#region private methods

        public void Setup(int functionalityId)
        {
            ShowData(functionalityId);
        }

		protected override void ShowData(int functionalityId)
        {
            oDetailButtonPanel.SetId = SetId;
			var data = new FunctionalityDataModel();
			data.FunctionalityId = functionalityId;

			var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			if (items.Count == 1)
			{
                var item = items[0];

                lblFunctionalityId.Text = item.FunctionalityId.ToString();
                lblApplicationId.Text = SessionVariables.RequestProfile.ApplicationId.ToString();
                lblName.Text = item.Name;
                lblDescription.Text = item.Description;
                lblSortOrder.Text = item.SortOrder.ToString();
                lblFunctionalityActiveStatusId.Text = item.FunctionalityActiveStatus.ToString();
                lblFunctionalityPriorityId.Text = item.FunctionalityPriority.ToString();

                oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

                oHistoryList.Setup(PrimaryEntity, functionalityId, "Functionality");
			}
			else
			{
				Clear();
			}
		}

		protected override void Clear()
		{
			lblFunctionalityId.Text = String.Empty;
			lblName.Text = String.Empty;
            lblApplicationId.Text = string.Empty;
			lblDescription.Text = String.Empty;
			lblSortOrder.Text = String.Empty;
            lblFunctionalityActiveStatusId.Text = string.Empty;
		}

		protected override void PopulateLabelsText()
		{
			var validColumns = new Dictionary<string, string>();
			var labelslist = new List<Label>(new Label[] { lblFunctionalityIdText
													  , lblNameText, lblApplicationIdText, lblDescriptionText, lblSortOrderText});
			if (Cache[CacheConstants.FunctionalityLabelDictionary] == null)
			{
				validColumns = UIHelper.GetLabelDictonaryObject((int)Framework.Components.DataAccess.SystemEntity.Functionality, SessionVariables.RequestProfile.AuditId);
				Cache.Add(CacheConstants.SystemEntityTypeLabelDictionary, validColumns, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 60), System.Web.Caching.CacheItemPriority.Default, null);

			}
			else
			{
				validColumns = (Dictionary<string, string>)Cache[CacheConstants.FunctionalityLabelDictionary];
			}
			UIHelper.PopulateLabelsText(validColumns, (int)Framework.Components.DataAccess.SystemEntity.Functionality, SessionVariables.RequestProfile.AuditId, labelslist);


		}

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblFunctionalityIdText, lblNameText, lblApplicationIdText, lblDescriptionText, lblSortOrderText, lblFunctionalityActiveStatusIdText});
            }

            return LabelListCore;
        }

		#endregion

		#region Events

        protected override void OnInit(EventArgs e)
        {
            // set basic variables
            DictionaryLabel = CacheConstants.FunctionalityLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Functionality;

            base.OnInit(e);

            // set object variable reference            
            PlaceHolderCore = dynFunctionalityId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;
        }

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblFunctionalityIdText.Visible = isTesting;
				lblFunctionalityId.Visible = isTesting;
			}
			PopulateLabelsText();
		}


		

		#endregion

	}
}