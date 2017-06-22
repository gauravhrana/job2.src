using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Configuration.Theme.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
	{

		#region private methods

		protected override void ShowData(int ThemeId)
		{
			oDetailButtonPanel.SetId = SetId;
			var data = new ThemeDataModel();
			data.ThemeId = ThemeId;

			var items = Framework.Components.Core.ThemeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			if (items.Count == 1)
			{
				var item = items[0];

				lblThemeId.Text = item.ThemeId.ToString();
				lblName.Text = item.Name;
				lblDescription.Text = item.Description;
				lblSortOrder.Text = item.SortOrder.ToString();
				//lblIsAllTab.Text = item.IsAllTab.ToString();

				oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

				oHistoryList.Setup(PrimaryEntity, ThemeId, "Theme");
			}
			else
			{
				Clear();
			}
		}

		protected override void PopulateLabelsText()
		{
			var validColumns = new Dictionary<string, string>();
			var labelslist = new List<Label>(new Label[] { lblThemeIdText
													  , lblNameText, lblDescriptionText, lblSortOrderText});
			if (Cache[CacheConstants.ThemeLabelDictionary] == null)
			{
				validColumns = UIHelper.GetLabelDictonaryObject((int)Framework.Components.DataAccess.SystemEntity.Theme, SessionVariables.RequestProfile.AuditId);
				Cache.Add(CacheConstants.ThemeLabelDictionary, validColumns, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 60), System.Web.Caching.CacheItemPriority.Default, null);

			}
			else
			{
				validColumns = (Dictionary<string, string>)Cache[CacheConstants.ThemeLabelDictionary];
			}
			UIHelper.PopulateLabelsText(validColumns, (int)Framework.Components.DataAccess.SystemEntity.Theme, SessionVariables.RequestProfile.AuditId, labelslist);


		}

		protected override void Clear()
		{
			lblThemeId.Text = String.Empty;
			lblName.Text = String.Empty;
			lblDescription.Text = String.Empty;
			lblSortOrder.Text = String.Empty;
			//lblIsAllTab.Text = String.Empty;
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblThemeIdText, lblNameText, lblDescriptionText, lblSortOrderText });
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
				lblThemeIdText.Visible = isTesting;
				lblThemeId.Visible = isTesting;
			}
			PopulateLabelsText();
		}

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.ThemeLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Theme;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynThemeId;
			PlaceHolderAuditHistory = dynAuditHistory;

			BorderDiv = borderdiv;
		}

		#endregion

	}
}