using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;


namespace Shared.UI.Web.Configuration.ApplicationRoute.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
	{
		
		#region private methods

		protected override void ShowData(int applicationRouteId)
		{
			base.ShowData(applicationRouteId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new ApplicationRouteDataModel();
			data.ApplicationRouteId = applicationRouteId;

			var items = Framework.Components.Core.ApplicationRouteDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match 
			if (items.Count == 1)
			{
				var item = items[0];

				lblApplicationRouteId.Text = item.ApplicationRouteId.ToString();
				lblRouteName.Text = item.RouteName;
				lblEntityName.Text = item.EntityName;
				lblProposedRoute.Text = item.ProposedRoute;
				lblRelativeRoute.Text = item.RelativeRoute;
				lblDescription.Text = item.Description;				

				oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

				oHistoryList.Setup(PrimaryEntity, applicationRouteId, "ApplicationRoute");
			}
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblApplicationRouteIdText, lblEntityNameText,lblRouteNameText,lblProposedRouteText,lblRelativeRouteText,lblDescriptionText });
			}

			return LabelListCore;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.ApplicationRouteLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationRoute;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynApplicationRouteId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
		}

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblApplicationRouteIdText.Visible = isTesting;
				lblApplicationRouteId.Visible = isTesting;
			}

			PopulateLabelsText();
		}

		#endregion

	}
}