using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.UI.Web;
using Shared.WebCommon.UI.Web;


namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityOwner.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {

        #region variables

        

#endregion

        #region private methods

        

        protected override void ShowData(int functionalityOwnerId)
        {
            oDetailButtonPanel.SetId = SetId;
            var data = new FunctionalityOwnerDataModel();
            data.FunctionalityOwnerId = functionalityOwnerId;

            var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityOwnerDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

                lblFunctionalityOwnerId.Text        = Convert.ToString(item.FunctionalityOwnerId);
                lblFunctionalityId.Text             = Convert.ToString(item.Functionality);
                lblDevelperRoleId.Text       = Convert.ToString(item.DeveloperRole);
                lblDeveloper.Text            = Convert.ToString(item.Developer);
                lblFeatureOwnerStatusId.Text = Convert.ToString(item.FeatureOwnerStatus);

                oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

                oHistoryList.Setup(PrimaryEntity, functionalityOwnerId, "FunctionalityOwner");
            }
            else
            {
                Clear();
            }
        }

        protected override void Clear()
        {
            lblFunctionalityOwnerId.Text        =  String.Empty;
            lblFunctionalityId.Text             =  String.Empty;
            lblDevelperRoleId.Text       =  String.Empty;
            lblDeveloper.Text            =  String.Empty;
            lblFeatureOwnerStatusId.Text =  String.Empty;
        }

        protected override void PopulateLabelsText()
        {
            var validColumns = new Dictionary<string, string>();
            var labelslist = new List<Label>(new Label[] { lblFunctionalityOwnerIdText, lblFunctionalityText, lblDeveloperRoleText, lblDeveloperText, lblFeatureOwnerStatusText});
            if (Cache[CacheConstants.FunctionalityOwnerLabelDictionary] == null)
            {
                validColumns = UIHelper.GetLabelDictonaryObject((int)Framework.Components.DataAccess.SystemEntity.FunctionalityOwner, SessionVariables.RequestProfile.AuditId);
                Cache.Add(CacheConstants.SystemEntityTypeLabelDictionary, validColumns, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 60), System.Web.Caching.CacheItemPriority.Default, null);

            }
            else
            {
                validColumns = (Dictionary<string, string>)Cache[CacheConstants.FunctionalityOwnerLabelDictionary];
            }
            UIHelper.PopulateLabelsText(validColumns, (int)Framework.Components.DataAccess.SystemEntity.FunctionalityOwner, SessionVariables.RequestProfile.AuditId, labelslist);


        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblFunctionalityOwnerIdText, lblFunctionalityText, lblFeatureOwnerStatusText, lblDeveloperText, lblDeveloperRoleText });
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
                lblFunctionalityOwnerIdText.Visible = isTesting;
                lblFunctionalityOwnerId.Visible = isTesting;
            }
            PopulateLabelsText();
        }

        protected override void OnInit(EventArgs e)
        {
            // set basic variables
            DictionaryLabel = CacheConstants.FunctionalityOwnerLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FunctionalityOwner;

            base.OnInit(e);

            // set object variable reference            
            PlaceHolderCore = dynFunctionalityOwnerId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;
        }

        #endregion

    }
}