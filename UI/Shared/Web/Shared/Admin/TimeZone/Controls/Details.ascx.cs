using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Admin.TimeZone.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {

		#region private methods

		protected override void ShowData(int timeZoneId)
		{
			base.ShowData(timeZoneId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new TimeZoneDataModel();
			data.TimeZoneId = timeZoneId;

			var items = Framework.Components.Core.TimeZoneDataManger.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match 
			if (items.Count == 1)
			{
				var item = items[0];

				lblTimeZoneId.Text = item.TimeZoneId.ToString();
				lblTimeDifference.Text = item.TimeDifference.ToString();
				lblName.Text = item.Name;
				lblDescription.Text = item.Description;
				lblSortOrder.Text = item.SortOrder.ToString();

				oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

				oHistoryList.Setup(PrimaryEntity, timeZoneId, "TimeZone");
			}
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblTimeZoneIdText, lblNameText, lblDescriptionText, lblSortOrderText, lblTimeDifferenceText });
			}

			return LabelListCore;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.TimeZoneLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TimeZone;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynTimeZoneId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
		}

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblTimeZoneIdText.Visible = isTesting;
				lblTimeZoneId.Visible = isTesting;
			}

			PopulateLabelsText();
		}

		#endregion
		       

    }
}