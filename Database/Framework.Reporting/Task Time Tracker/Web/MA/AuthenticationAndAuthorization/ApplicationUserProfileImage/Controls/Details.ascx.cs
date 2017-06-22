using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationUserProfileImage.Controls
{
	public partial class Details : ControlDetailsStandard
	{

		#region private methods

		protected override void ShowData(int applicationUserProfileImageId)
		{
			base.ShowData(applicationUserProfileImageId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var dataQuery = new ApplicationUserProfileImageDataModel();
			dataQuery.ApplicationUserProfileImageId = applicationUserProfileImageId;

			var entityList = Framework.Components.ApplicationUser.ApplicationUserProfileImageDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);
			if (entityList.Count == 1)
			{
				foreach (var entityItem in entityList)
				{
					lblApplicationUserProfileImageId.Text = entityItem.ApplicationUserProfileImageId.ToString();
					lblApplicationUserName.Text = entityItem.ApplicationUserName.ToString();
					lblApplication.Text = entityItem.Application.ToString();
					imgApplicationUserImage.ImageUrl = "~/Shared/AuthenticationAndAuthorization/ApplicationUserProfileImage/ShowImage.aspx?imageid=" + entityItem.ApplicationUserProfileImageId.ToString();					

					oUpdateInfo.LoadText(entityItem.UpdatedDate, entityItem.UpdatedBy, entityItem.LastAction);

					oHistoryList.Setup(PrimaryEntity, applicationUserProfileImageId, "ApplicationUserProfileImage");
				}
			}
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblApplicationUserProfileImageId, lblApplicationText, lblApplicationUserName, lblImageText });
			}

			return LabelListCore;
		}
		
		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			DictionaryLabel = CacheConstants.ApplicationUserProfileImageLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationUserProfileImage;

			PlaceHolderCore = dynApplicationUserProfileImageId;
			PlaceHolderAuditHistory = dynAuditHistory;

			BorderDiv = borderdiv;

			CoreSystemKey = lblApplicationUserProfileImageId;

			CoreUpdateInfo = oUpdateInfo;
		}

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblApplicationUserProfileImageIdText.Visible = isTesting;
				lblApplicationUserProfileImageId.Visible = isTesting;
			}
			PopulateLabelsText();
		}

		#endregion

	}
}