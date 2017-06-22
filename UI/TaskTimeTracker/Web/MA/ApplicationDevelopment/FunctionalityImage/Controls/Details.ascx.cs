using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.UI.Web;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.Module.ApplicationDevelopment;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityImage.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {
        #region private methods       

        protected override void ShowData(int functionalityImageId)
        {
			base.ShowData(functionalityImageId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var dataQuery = new FunctionalityImageDataModel();
			dataQuery.FunctionalityImageId = functionalityImageId;

			var entityList = FunctionalityImageDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);
			if (entityList.Count == 1)
			{
				foreach (var entityItem in entityList)
				{
					lblFunctionalityImageId.Text = entityItem.FunctionalityImageId.ToString();
					lblTitle.Text = entityItem.Title.ToString();
					lblApplication.Text = entityItem.Application.ToString();
					imgApplicationUserImage.ImageUrl = "~/ApplicationDevelopment/FunctionalityImage/ShowImage.aspx?imageid=" + entityItem.FunctionalityImageId.ToString();

                    oUpdateInfo.LoadText(entityItem.UpdatedDate, entityItem.UpdatedBy, entityItem.LastAction);

                    oHistoryList.Setup(PrimaryEntity, functionalityImageId, "FunctionalityImage");

				}
			}			
        }

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblFunctionalityImageIdText, lblApplicationText, lblTitleText, lblImageText });
			}

			return LabelListCore;
		}       

        #endregion

        #region Events

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.FunctionalityImageLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FunctionalityImage;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynFunctionalityImageId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
		}

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                lblFunctionalityImageIdText.Visible = isTesting;
                lblFunctionalityImageId.Visible = isTesting;
            }
            PopulateLabelsText();
        }

        #endregion
    }
}