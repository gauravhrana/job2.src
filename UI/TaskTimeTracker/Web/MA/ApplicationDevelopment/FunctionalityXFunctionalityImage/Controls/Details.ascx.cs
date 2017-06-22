using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.UI.Web;
using Shared.WebCommon.UI.Web;


namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityXFunctionalityImage.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {

        #region variables

        #endregion

        #region private methods

        protected override void ShowData(int functionalityXFunctionalityImageid)
        {
            oDetailButtonPanel.SetId = SetId;
            var data = new FunctionalityXFunctionalityImageDataModel();
            data.FunctionalityXFunctionalityImageId = functionalityXFunctionalityImageid;
			var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityXFunctionalityImageDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];
                lblFunctionalityXFunctionalityImageId.Text = item.FunctionalityXFunctionalityImageId.ToString();
                lblFunctionalityId.Text = item.Functionality;
                lblFunctionalityImageId.Text = item.FunctionalityImage;
                lblKeyString.Text = item.KeyString;
                lblTitle.Text = item.Title;
                lblDescription.Text = item.Description.ToString();
                lblSortOrder.Text = item.Functionality;
                lblCreatedBy.Text = item.FunctionalityImage;
                lblCreatedDate.Text = item.KeyString;
                lblUpdatedBy.Text = item.Title;
                lblUpdatedDate.Text = item.Description.ToString();


                oUpdateInfo.LoadText(DateTime.Parse(item.UpdatedDate.ToString()), item.UpdatedBy.ToString(), item.LastAction);

                oHistoryList.Setup(PrimaryEntity, functionalityXFunctionalityImageid, "FunctionalityXFunctionalityImage");
            }
            else
            {
                Clear();
            }
        }

        protected override void Clear()
        {
            lblFunctionalityXFunctionalityImageId.Text = String.Empty;
            lblFunctionalityId.Text = String.Empty;
            lblFunctionalityImageId.Text = String.Empty;
            lblKeyString.Text = String.Empty;
            lblTitle.Text = String.Empty;
            lblDescriptionText.Text = String.Empty;
            lblSortOrder.Text = String.Empty;
            lblCreatedBy.Text = String.Empty;
            lblCreatedDate.Text = String.Empty;
            lblUpdatedBy.Text = String.Empty;
            lblUpdatedDate.Text = String.Empty;
        }

        protected override void PopulateLabelsText()
        {
            var validColumns = new Dictionary<string, string>();
            var labelslist = new List<Label>(new Label[] { lblFunctionalityXFunctionalityImageIdText, 
                                                         lblFunctionalityText, lblFunctionalityImageText, 
													    lblKeyStringText, lblTitleText, lblDescriptionText,
                                                        lblSortOrderText, lblCreatedByText, lblCreatedDateText,
                                                        lblUpdatedByText, lblUpdatedDateText});
            if (Cache[CacheConstants.FunctionalityXFunctionalityImageLabelDictionary] == null)
            {
                validColumns = UIHelper.GetLabelDictonaryObject((int)Framework.Components.DataAccess.SystemEntity.FunctionalityXFunctionalityImage, SessionVariables.RequestProfile.AuditId);
                Cache.Add(CacheConstants.FunctionalityXFunctionalityImageLabelDictionary, validColumns, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 60), System.Web.Caching.CacheItemPriority.Default, null);

            }
            else
            {
                validColumns = (Dictionary<string, string>)Cache[CacheConstants.FunctionalityXFunctionalityImageLabelDictionary];
            }
            UIHelper.PopulateLabelsText(validColumns, (int)Framework.Components.DataAccess.SystemEntity.FunctionalityXFunctionalityImage, SessionVariables.RequestProfile.AuditId, labelslist);

        }


        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                lblFunctionalityXFunctionalityImageIdText.Visible = isTesting;
                lblFunctionalityXFunctionalityImageId.Visible = isTesting;
            }
            PopulateLabelsText();
        }

        protected override void OnInit(EventArgs e)
        {
            // set basic variables
            DictionaryLabel = CacheConstants.FunctionalityXFunctionalityImageLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FunctionalityXFunctionalityImage;

            base.OnInit(e);

            // set object variable reference            
            PlaceHolderCore = dynFunctionalityXFunctionalityImageId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;
        }


        #endregion

    }

}