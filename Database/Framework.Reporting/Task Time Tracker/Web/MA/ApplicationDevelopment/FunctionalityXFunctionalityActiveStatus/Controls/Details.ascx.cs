using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.UI.Web;
using Shared.WebCommon.UI.Web;


namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityXFunctionalityActiveStatus.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
	{

		#region variables

		#endregion

		#region private methods

		protected override void ShowData(int functionalityXFunctionalityActiveStatusid)
		{
			oDetailButtonPanel.SetId = SetId;
			var data = new FunctionalityXFunctionalityActiveStatusDataModel();
			data.FunctionalityXFunctionalityActiveStatusId = functionalityXFunctionalityActiveStatusid;
			var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityXFunctionalityActiveStatusDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];
				lblFunctionalityXFunctionalityActiveStatusId.Text = item.FunctionalityXFunctionalityActiveStatusId.ToString();
				lblFunctionalityId.Text = item.Functionality;
				lblFunctionalityActiveStatusId.Text = item.FunctionalityActiveStatus;
				lblAcknowledgedBy.Text = item.AcknowledgedBy;
				lblMemo.Text = item.Memo;
				lblKnowledgeDate.Text = item.KnowledgeDate.ToString();


                oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

                oHistoryList.Setup(PrimaryEntity, functionalityXFunctionalityActiveStatusid, "FunctionalityXFunctionalityActiveStatus");
			}
			else
			{
				Clear();
			}
		}

		protected override void Clear()
		{
			lblFunctionalityXFunctionalityActiveStatusId.Text = String.Empty;
			lblFunctionalityId.Text = String.Empty;
			lblFunctionalityActiveStatusId.Text = String.Empty;
			lblAcknowledgedBy.Text = String.Empty;
			lblMemo.Text = String.Empty;
			lblKnowledgeDate.Text = String.Empty;
			
		}

		protected override void PopulateLabelsText()
		{
			var validColumns = new Dictionary<string, string>();
			var labelslist = new List<Label>(new Label[] { lblFunctionalityXFunctionalityActiveStatusIdText, 
                                                         lblFunctionalityText, lblFunctionalityActiveStatusText, 
													    lblAcknowledgedByText, lblMemoText, lblKnowledgeDateText});
			if (Cache[CacheConstants.FunctionalityXFunctionalityActiveStatusLabelDictionary] == null)
			{
				validColumns = UIHelper.GetLabelDictonaryObject((int)Framework.Components.DataAccess.SystemEntity.FunctionalityXFunctionalityActiveStatus, SessionVariables.RequestProfile.AuditId);
				Cache.Add(CacheConstants.FunctionalityXFunctionalityActiveStatusLabelDictionary, validColumns, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 60), System.Web.Caching.CacheItemPriority.Default, null);

			}
			else
			{
				validColumns = (Dictionary<string, string>)Cache[CacheConstants.FunctionalityXFunctionalityActiveStatusLabelDictionary];
			}
			UIHelper.PopulateLabelsText(validColumns, (int)Framework.Components.DataAccess.SystemEntity.FunctionalityXFunctionalityActiveStatus, SessionVariables.RequestProfile.AuditId, labelslist);

		}


		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblFunctionalityXFunctionalityActiveStatusIdText.Visible = isTesting;
				lblFunctionalityXFunctionalityActiveStatusId.Visible = isTesting;
			}
			PopulateLabelsText();
		}

        protected override void OnInit(EventArgs e)
        {
            // set basic variables
            DictionaryLabel = CacheConstants.FunctionalityXFunctionalityActiveStatusLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FunctionalityXFunctionalityActiveStatus;

            base.OnInit(e);

            // set object variable reference            
            PlaceHolderCore = dynFunctionalityXFunctionalityActiveStatusId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;
        }


		#endregion

	}

}