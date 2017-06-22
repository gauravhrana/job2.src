using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;


namespace Shared.UI.Web.Admin.Country.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {
		#region private methods

		protected override void ShowData(int countryId)
		{
			base.ShowData(countryId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new CountryDataModel();
			data.CountryId = countryId;

			var items = Framework.Components.Core.CountryDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);			

			// should only have single match 
			if (items.Count == 1)
			{
				var item = items[0];

				lblCountryId.Text = item.CountryId.ToString();
				lblTimeZone.Text = item.TimeZoneId.ToString();
				lblName.Text = item.Name;
				lblDescription.Text = item.Description;
				lblSortOrder.Text = item.SortOrder.ToString();

				oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

				oHistoryList.Setup(PrimaryEntity, countryId, "Country");
			}
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblCountryIdText, lblNameText, lblDescriptionText, lblSortOrderText,lblTimeZoneText });
			}

			return LabelListCore;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.CountryLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Country;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynCountryId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
		}

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblCountryIdText.Visible = isTesting;
				lblCountryId.Visible = isTesting;
			}

			PopulateLabelsText();
		}

		#endregion    
		      

    }
}