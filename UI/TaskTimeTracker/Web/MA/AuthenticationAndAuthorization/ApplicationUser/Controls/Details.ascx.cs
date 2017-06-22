using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using Shared.WebCommon.UI.Web;


namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationUser.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
	{

		#region private methods

		protected override void ShowData(int ApplicationUserId)
		{
			base.ShowData(ApplicationUserId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var dataQuery = new ApplicationUserDataModel();
			dataQuery.ApplicationUserId = ApplicationUserId;

			var entityList = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

			if (entityList.Count == 1)
			{
				foreach (var entityItem in entityList)
				{
					lblApplicationUserId.Text		= entityItem.ApplicationUserId.ToString();
					lblApplicationUserName.Text		= entityItem.ApplicationUserName;
					lblEmailAddress.Text            = entityItem.EmailAddress;
					lblLastName.Text				= entityItem.LastName;
					lblFirstName.Text				= entityItem.FirstName;
					lblMiddleName.Text				= entityItem.MiddleName;
					lblApplicationUserTitle.Text	= entityItem.ApplicationUserTitle;
					lblApplication.Text				= entityItem.Application;
					
					oUpdateInfo.LoadText(entityItem.UpdatedDate, entityItem.UpdatedBy, entityItem.LastAction);

					oHistoryList.Setup(PrimaryEntity, ApplicationUserId, "ApplicationUser");
				}
			}
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblApplicationUserIdText, lblApplicationUserNameText, lblEmailAddressText, lblFirstNameText, lblMiddleNameText, lblLastNameText, lblApplicationUserTitleText, lblApplicationText });
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
				lblApplicationUserIdText.Visible = isTesting;
				lblApplicationUserId.Visible = isTesting;
			}
			PopulateLabelsText();
		}

		protected override void OnInit(EventArgs e)
		{

			base.OnInit(e);

			// set basic variables
			DictionaryLabel         = CacheConstants.ApplicationUserLabelDictionary;
			PrimaryEntity           = Framework.Components.DataAccess.SystemEntity.ApplicationUser;

			// set object variable reference            
			PlaceHolderCore         = dynApplicationUserId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv               = borderdiv;
		}

		#endregion
		
	}
}