using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.UI.Web;
using Shared.WebCommon.UI.Web;


namespace ApplicationContainer.UI.Web.ApplicationDevelopment.ModuleOwner.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {
              
        #region private methods

        protected override void ShowData(int moduleOwnerId)
        {
            oDetailButtonPanel.SetId = SetId;
            var data = new ModuleOwnerDataModel();
            data.ModuleOwnerId = moduleOwnerId;

			var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.ModuleOwnerDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

                lblModuleOwnerId.Text        = Convert.ToString(item.ModuleOwnerId);
                lblModuleId.Text             = Convert.ToString(item.Module);
                lblDevelperRoleId.Text       = Convert.ToString(item.DeveloperRole);
                lblDeveloper.Text            = Convert.ToString(item.Developer);
                lblFeatureOwnerStatusId.Text = Convert.ToString(item.FeatureOwnerStatus);
                //lblApplication.Text = item.Application;

                oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

                oHistoryList.Setup(PrimaryEntity, moduleOwnerId, "ModuleOwner");
            }
            else
            {
                Clear();
            }
        }

        protected override void Clear()
        {
            lblModuleOwnerId.Text        =  String.Empty;
            lblModuleId.Text             =  String.Empty;
            lblDevelperRoleId.Text       =  String.Empty;
            lblDeveloper.Text            =  String.Empty;
	        var smtpClient = new SmtpClient();
            lblFeatureOwnerStatusId.Text =  String.Empty;
        }

        protected override void PopulateLabelsText()
        {
            var validColumns = new Dictionary<string, string>();
            var labelslist = new List<Label>(new Label[] { lblModuleOwnerIdText, lblModuleText, lblDeveloperRoleText, lblDeveloperText, lblFeatureOwnerStatusText});
            if (Cache[CacheConstants.ModuleOwnerLabelDictionary] == null)
            {
                validColumns = UIHelper.GetLabelDictonaryObject((int)Framework.Components.DataAccess.SystemEntity.ModuleOwner, SessionVariables.RequestProfile.AuditId);
                Cache.Add(CacheConstants.SystemEntityTypeLabelDictionary, validColumns, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 60), System.Web.Caching.CacheItemPriority.Default, null);

            }
            else
            {
                validColumns = (Dictionary<string, string>)Cache[CacheConstants.ModuleOwnerLabelDictionary];
            }
            UIHelper.PopulateLabelsText(validColumns, (int)Framework.Components.DataAccess.SystemEntity.ModuleOwner, SessionVariables.RequestProfile.AuditId, labelslist);


        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblModuleOwnerIdText, lblModuleText, lblDeveloperText, lblDeveloperRoleText, lblFeatureOwnerStatusText });
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
                lblModuleOwnerIdText.Visible = isTesting;
                lblModuleOwnerId.Visible = isTesting;
            }
            PopulateLabelsText();
        }

        protected override void OnInit(EventArgs e)
        {
            // set basic variables
            DictionaryLabel = CacheConstants.ModuleOwnerLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ModuleOwner;

            base.OnInit(e);

            // set object variable reference            
            PlaceHolderCore = dynModuleOwnerId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;
        }

        #endregion

    }
}