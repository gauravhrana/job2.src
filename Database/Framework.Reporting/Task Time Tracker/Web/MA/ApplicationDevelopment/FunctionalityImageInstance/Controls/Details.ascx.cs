using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.UI.Web;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityImageInstance.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {
        #region private methods

        protected override void ShowData(int FunctionalityImageInstanceId)
        {
            base.ShowData(FunctionalityImageInstanceId);

            oDetailButtonPanel.SetId = SetId;

            Clear();

            var data = new FunctionalityImageInstanceDataModel();
            data.FunctionalityImageInstanceId = FunctionalityImageInstanceId;

			var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityImageInstanceDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);
            int functionalityImageId = 0;
            if (items.Count == 1)
            {
                var item = items[0];
                functionalityImageId = (int)item.FunctionalityImageId;
                lblFunctionalityImageInstanceId.Text = item.FunctionalityImageInstanceId.ToString();
                lblFunctionalityImageId.Text = item.FunctionalityImageId.ToString();
                lblFunctionalityImageAttributeId.Text = item.FunctionalityImageAttributeId.ToString();

                imgApplicationUserImage.Visible = true;
                imgApplicationUserImage.ImageUrl = "~/ApplicationDevelopment/FunctionalityImage/ShowImage.aspx?imageid=" + functionalityImageId.ToString();
                imgApplicationUserImage1.ImageUrl = "~/ApplicationDevelopment/FunctionalityImage/ShowImage.aspx?imageid=" + functionalityImageId.ToString();

                oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

                oHistoryList.Setup(PrimaryEntity, FunctionalityImageInstanceId, "FunctionalityImageInstance");
            }
            else
            {
                Clear();
            }
        }

        protected override void Clear()
        {
            lblFunctionalityImageInstanceId.Text = String.Empty;
            lblFunctionalityImageId.Text = String.Empty;
            lblFunctionalityImageAttributeId.Text = String.Empty;

        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblFunctionalityImageAttributeIdText, lblFunctionalityImageText, lblFunctionalityImageInstanceIdText });
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
                lblFunctionalityImageInstanceIdText.Visible = isTesting;
                lblFunctionalityImageInstanceId.Visible = isTesting;
            }
            PopulateLabelsText();
        }

        protected override void OnInit(EventArgs e)
        {
            // set basic variables
            DictionaryLabel = Shared.UI.Web.CacheConstants.FunctionalityImageInstanceLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FunctionalityImageInstance;

            base.OnInit(e);

            // set object variable reference            
            PlaceHolderCore = dynFunctionalityImageInstanceId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;
        }

        #endregion

    }
}