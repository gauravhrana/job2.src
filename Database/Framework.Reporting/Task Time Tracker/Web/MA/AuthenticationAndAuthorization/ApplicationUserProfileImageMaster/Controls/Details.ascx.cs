using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationUserProfileImageMaster.Controls
{
	public partial class Details : ControlDetailsStandard
	{       

		#region private methods

		protected override void ShowData(int applicationUserProfileImageMasterId)
		{
			base.ShowData(applicationUserProfileImageMasterId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var dataQuery = new ApplicationUserProfileImageMasterDataModel();
			dataQuery.ApplicationUserProfileImageMasterId = applicationUserProfileImageMasterId;

			var entityList = Framework.Components.ApplicationUser.ApplicationUserProfileImageMasterDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);
			if (entityList.Count == 1)
			{
				foreach (var entityItem in entityList)
				{
					lblApplicationUserProfileImageMasterId.Text = entityItem.ApplicationUserProfileImageMasterId.ToString();
					lblTitle.Text								= entityItem.Title.ToString();
					lblApplication.Text							= entityItem.Application.ToString();
					imgApplicationUserImage.ImageUrl			= "~/Shared/AuthenticationAndAuthorization/ApplicationUserProfileImageMaster/ShowImage.aspx?imageid=" + entityItem.ApplicationUserProfileImageMasterId.ToString();

					oUpdateInfo.LoadText(entityItem.UpdatedDate, entityItem.UpdatedBy, entityItem.LastAction);

					oHistoryList.Setup(PrimaryEntity, applicationUserProfileImageMasterId, "ApplicationUserProfileImageMaster");
				}
			}
		}			

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblApplicationUserProfileImageMasterId, lblApplicationText, lblTitleText, lblImageText });
			}

			return LabelListCore;
		}       

        #endregion

        #region Events
		
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			DictionaryLabel = CacheConstants.ApplicationUserProfileImageMasterLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationUserProfileImageMaster;

			PlaceHolderCore = dynApplicationUserProfileImageMasterId;
			PlaceHolderAuditHistory = dynAuditHistory;

			BorderDiv = borderdiv;

			CoreSystemKey = lblApplicationUserProfileImageMasterId;
			
			CoreUpdateInfo = oUpdateInfo;
		}

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                lblApplicationUserProfileImageMasterIdText.Visible = isTesting;
                lblApplicationUserProfileImageMasterId.Visible = isTesting;
            }
            PopulateLabelsText();
        }

        #endregion           
		
	}
}