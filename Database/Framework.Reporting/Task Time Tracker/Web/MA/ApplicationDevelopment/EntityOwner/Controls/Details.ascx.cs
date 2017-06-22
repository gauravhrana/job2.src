using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.UI.Web;
using Shared.WebCommon.UI.Web;
using DataModel.TaskTimeTracker.ApplicationDevelopment;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.EntityOwner.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {

        #region private methods

        

        protected override void ShowData(int entityOwnerId)
        {
            oDetailButtonPanel.SetId = SetId;
            var data = new EntityOwnerDataModel();
            data.EntityOwnerId = entityOwnerId;

			var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.EntityOwnerDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

                lblEntityOwnerId.Text        = Convert.ToString(item.EntityOwnerId);
                lblEntityId.Text             = Convert.ToString(item.Entity);
                lblDevelperRoleId.Text       = Convert.ToString(item.DeveloperRole);
                lblDeveloper.Text            = Convert.ToString(item.Developer);
                lblFeatureOwnerStatusId.Text = Convert.ToString(item.FeatureOwnerStatus);

                oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

                oHistoryList.Setup(PrimaryEntity, entityOwnerId, "EntityOwner");
            }
            else
            {
                Clear();
            }
        }

        protected override void Clear()
        {
            lblEntityOwnerId.Text        =  String.Empty;
            lblEntityId.Text             =  String.Empty;
            lblDevelperRoleId.Text       =  String.Empty;
            lblDeveloper.Text            =  String.Empty;
            lblFeatureOwnerStatusId.Text =  String.Empty;
        }

        protected override void PopulateLabelsText()
        {
            var validColumns = new Dictionary<string, string>();
            var labelslist = new List<Label>(new Label[] { lblEntityOwnerIdText, lblEntityText, lblDeveloperRoleText, lblDeveloperText, lblFeatureOwnerStatusText});
            if (Cache[CacheConstants.EntityOwnerLabelDictionary] == null)
            {
                validColumns = UIHelper.GetLabelDictonaryObject((int)Framework.Components.DataAccess.SystemEntity.EntityOwner, SessionVariables.RequestProfile.AuditId);
                Cache.Add(CacheConstants.SystemEntityTypeLabelDictionary, validColumns, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 60), System.Web.Caching.CacheItemPriority.Default, null);

            }
            else
            {
                validColumns = (Dictionary<string, string>)Cache[CacheConstants.EntityOwnerLabelDictionary];
            }
            UIHelper.PopulateLabelsText(validColumns, (int)Framework.Components.DataAccess.SystemEntity.EntityOwner, SessionVariables.RequestProfile.AuditId, labelslist);


        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblEntityOwnerIdText, lblDeveloperRoleText, lblDeveloperText, lblEntityText, lblFeatureOwnerStatusText });
            }

            return LabelListCore;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            // set basic variables
            DictionaryLabel = CacheConstants.EntityOwnerLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.EntityOwner;

            base.OnInit(e);

            // set object variable reference            
            PlaceHolderCore = dynEntityOwnerId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                lblEntityOwnerIdText.Visible = isTesting;
                lblEntityOwnerId.Visible = isTesting;
            }
            PopulateLabelsText();
        }

        

        #endregion

    }
}