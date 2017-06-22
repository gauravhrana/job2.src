using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;


namespace Shared.UI.Web.Configuration.ApplicationRouteParameter.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
	{

		#region private methods

		protected override void ShowData(int applicationRouteParameterId)
		{
			base.ShowData(applicationRouteParameterId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new ApplicationRouteParameterDataModel();
			data.ApplicationRouteParameterId = applicationRouteParameterId;

			var items = Framework.Components.Core.ApplicationRouteParameterDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match 
			if (items.Count == 1)
			{
				var item = items[0];

				lblApplicationRouteParameterId.Text = item.ApplicationRouteParameterId.ToString();
				lblApplicationRoute.Text = item.ApplicationRoute.ToString();
				lblParameterName.Text = item.ParameterName;
				lblParameterValue.Text = item.ParameterValue;
				
				oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

				oHistoryList.Setup(PrimaryEntity, applicationRouteParameterId, "ApplicationRouteParameter");
			}
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblApplicationRouteParameterIdText, lblApplicationRouteText, lblParameterNameText, lblParameterValueText });
			}

			return LabelListCore;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.ApplicationRouteParameterLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationRouteParameter;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynApplicationRouteParameterId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
		}

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblApplicationRouteParameterIdText.Visible = isTesting;
				lblApplicationRouteParameterId.Visible = isTesting;
			}

			PopulateLabelsText();
		}

		#endregion

	}
}