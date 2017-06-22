using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.UI.Web;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.Module.ApplicationDevelopment;


namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityEntityStatus.Controls
{
    public partial class Details : ControlDetails
    {	
	
        #region variables


        #endregion

        #region private methods

        protected override void ShowData(int functionalityEntityStatusid)
        {
            oDetailButtonPanel.SetId = SetId;
            var data = new FunctionalityEntityStatusDataModel();
            data.FunctionalityEntityStatusId = functionalityEntityStatusid;
			var items = FunctionalityEntityStatusDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

                lblFunctionalityEntityStatusId.Text = item.FunctionalityEntityStatusId.ToString();
                lblSystemEntityTypeId.Text    = item.SystemEntityTypeId.ToString();
                lblFunctionalityId.Text                    = Convert.ToString(item.FunctionalityId);
                lblFunctionalityStatusId.Text              = Convert.ToString(item.FunctionalityStatusId);
                lblFunctionalityPriorityId.Text            = Convert.ToString(item.FunctionalityPriorityId);
                lblAssignedTo.Text                         = Convert.ToString(item.AssignedTo);
                lblMemo.Text                               = Convert.ToString(item.Memo);
				lblTargetDate.Text						   = Convert.ToString(item.TargetDate);
				lblStartDate.Text						   = Convert.ToString(item.StartDate);

                oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

                oHistoryList.Setup(PrimaryEntity, functionalityEntityStatusid, "FunctionalityEntityStatus");
            }
            else
            {
                Clear();
            }
        }

        protected override void Clear()
        {
           lblFunctionalityEntityStatusId.Text        = String.Empty;
           lblSystemEntityTypeId.Text                 = String.Empty;
           lblFunctionalityId.Text                    = String.Empty;
           lblFunctionalityStatusId.Text              = String.Empty;
           lblFunctionalityPriorityId.Text            = String.Empty;
           lblAssignedTo.Text                         = String.Empty;
           lblMemo.Text                               = String.Empty; 
           lblTargetDate.Text						  = String.Empty;
           lblStartDate.Text					      = String.Empty;
        }

		protected override void PopulateLabelsText()
		{
			var validColumns = new Dictionary<string, string>();
			var labelslist = new List<Label>(new Label[] { lblFunctionalityEntityStatusIdText, 
                                                        lblSystemEntityTypeText, lblFunctionalityText, lblFunctionalityStatusText, lblFunctionalityPriorityText,
													    lblAssignedToText, lblMemoText, lblTargetDateText, lblStartDateText});
			if (Cache[CacheConstants.FunctionalityEntityStatusLabelDictionary] == null)
			{
				validColumns = UIHelper.GetLabelDictonaryObject((int)SystemEntity.FunctionalityEntityStatus, SessionVariables.RequestProfile.AuditId);
				Cache.Add(CacheConstants.FunctionalityEntityStatusLabelDictionary, validColumns, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 60), CacheItemPriority.Default, null);

			}
			else
			{
				validColumns = (Dictionary<string, string>)Cache[CacheConstants.FunctionalityEntityStatusLabelDictionary];
			}
			UIHelper.PopulateLabelsText(validColumns, (int)SystemEntity.FunctionalityEntityStatus, SessionVariables.RequestProfile.AuditId, labelslist);

		}

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblFunctionalityEntityStatusIdText, lblFunctionalityText, lblFunctionalityPriorityText, lblFunctionalityStatusText, lblAssignedToText, lblMemoText, lblStartDateText, lblSystemEntityTypeText, lblTargetDateText });
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
                lblFunctionalityEntityStatusIdText.Visible = isTesting;
                lblFunctionalityEntityStatusId.Visible = isTesting;
            }
			PopulateLabelsText();
        }

        protected override void OnInit(EventArgs e)
        {
            // set basic variables
            DictionaryLabel = CacheConstants.FunctionalityEntityStatusLabelDictionary;
            PrimaryEntity = SystemEntity.FunctionalityEntityStatus;

            base.OnInit(e);

            // set object variable reference            
            PlaceHolderCore = dynFunctionalityEntityStatusId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;
        }

        #endregion

    }

}