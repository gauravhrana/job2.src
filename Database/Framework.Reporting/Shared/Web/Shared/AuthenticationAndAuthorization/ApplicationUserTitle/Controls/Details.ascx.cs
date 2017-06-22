using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using Shared.WebCommon.UI.Web;


namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationUserTitle.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
	{

		#region private methods

		protected override void ShowData(int applicationUserTitleId)
		{
			base.ShowData(applicationUserTitleId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var dataQuery = new ApplicationUserTitleDataModel();
			dataQuery.ApplicationUserTitleId = applicationUserTitleId;

			var entityList = Framework.Components.ApplicationUser.ApplicationUserTitleDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

			if (entityList.Count == 1)
			{
				foreach (var entityItem in entityList)
				{
					lblApplicationUserTitleId.Text	= entityItem.ApplicationUserTitleId.ToString();					
					lblName.Text					= entityItem.Name.ToString();
					lblDescription.Text				= entityItem.Description.ToString();
					lblSortOrder.Text				= entityItem.SortOrder.ToString();

					oUpdateInfo.LoadText(entityItem.UpdatedDate, entityItem.UpdatedBy, entityItem.LastAction);

					oHistoryList.Setup(PrimaryEntity, applicationUserTitleId, "ApplicationUserTitle");
				}
			}
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblApplicationUserTitleIdText, lblNameText, lblDescriptionText, lblSortOrderText });
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
				lblApplicationUserTitleIdText.Visible = isTesting;
				lblApplicationUserTitleId.Visible = isTesting;
			}
			PopulateLabelsText();
		}

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.ApplicationUserTitleLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationUserTitle;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynApplicationUserTitleId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
		}

		#endregion


	}
}
