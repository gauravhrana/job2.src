using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;


namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationOperation.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {
		
        #region private methods

        protected override void ShowData(int applicationoperationid)
        {
            oDetailButtonPanel.SetId = SetId;
            var data = new ApplicationOperationDataModel();
            data.ApplicationOperationId = applicationoperationid;
			var obj = Framework.Components.ApplicationUser.ApplicationOperationDataManager.GetDetails(data, SessionVariables.RequestProfile);

            if (obj != null)
            {

                
                lblName.Text                   = obj.Name;
                lblDescription.Text            = obj.Description;
                lblSortOrder.Text              = obj.SortOrder.ToString();
                lblApplicationOperationId.Text = obj.ApplicationOperationId.ToString();
                lblApplicationId.Text          = obj.Application;
                lblSystemEntityTypeId.Text     = obj.SystemEntityType;
                lblOperationValue.Text         = obj.OperationValue;

                oUpdateInfo.LoadText(obj);

                oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.ApplicationOperation, applicationoperationid, "ApplicationOperation");
                dynAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "ApplicationOperation");
            }
            else
            {
                Clear();
            }
        }

        protected override void Clear()
        {
            lblApplicationOperationId.Text = String.Empty;
            lblName.Text                   = String.Empty;
            lblDescription.Text            = String.Empty;
            lblSortOrder.Text              = String.Empty;
            lblApplicationId.Text          = String.Empty;
            lblSystemEntityTypeId.Text     = String.Empty;
            lblOperationValue.Text         = String.Empty;
        }

        protected override List<Label> GetLabels()
		{			
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblApplicationOperationIdText, lblApplicationText, lblSystemEntityTypeText
													  , lblNameText, lblDescriptionText, lblSortOrderText, lblOperationValueText });
            }

            return LabelListCore;

		}

	   
	    #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting                         = SessionVariables.IsTesting;
                lblApplicationOperationId.Visible     = false;
                lblApplicationOperationIdText.Visible = false;
            }

            PopulateLabelsText();
        }

        protected override void OnInit(EventArgs e)
        {

            base.OnInit(e);

            // set basic variables
            DictionaryLabel         = CacheConstants.ApplicationOperationLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationOperation;

            // set object variable reference            
            PlaceHolderCore         = dynApplicationOperationId;
            PlaceHolderAuditHistory = dynAuditHistory;
            MainTable               = tblMain1;
            BorderDiv               = borderdiv;
        }

        #endregion

    }

}