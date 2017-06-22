using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Framework.Components.Core;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Configuration.ThemeDetail.Controls
{
	public partial class Details : ControlDetails
	{

		#region variables


		#endregion

		#region private methods

		private void EnableControl(bool enabled, ControlCollection controls)
		{
			foreach (Control childControl in controls)
			{
				try
				{
					var webChildControl = (WebControl)childControl;
					webChildControl.Enabled = enabled;
				}
				catch
				{

				}
				finally
				{
					EnableControl(enabled, childControl.Controls);
				}
			}
		}

		protected override void ShowData(int ThemeDetailId)
		{
			oDetailButtonPanel.SetId = SetId;
			var data = new ThemeDetailDataModel();
			data.ThemeDetailId = ThemeDetailId;


			var items = ThemeDetailDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			if (items.Count == 1)
			{
				var item = items[0];

				lblThemeDetailId.Text = item.ThemeDetailId.ToString();
				lblApplicationId.Text = item.ApplicationId.ToString();
				lblThemeKeyId.Text = item.ThemeKeyId.ToString();
				lblThemeId.Text = item.ThemeId.ToString();
				lblThemeCategoryId.Text = item.ThemeCategoryId.ToString();
				lblThemeKeyId.Text = item.ThemeKeyId.ToString();

				oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

				oHistoryList.Setup(PrimaryEntity, ThemeDetailId, "ThemeDetail");
			}
			else
			{
				Clear();
			}
		}

		protected override void PopulateLabelsText()
		{
			var validColumns = new Dictionary<string, string>();
			var labelslist = new List<Label>(new Label[] { lblThemeDetailIdText
													  , lblApplicationIdText, lblValueText, lblThemeIdText, lblThemeKeyIdText, lblThemeCategoryIdText});
			if (Cache[CacheConstants.ThemeDetailLabelDictionary] == null)
			{
				validColumns = UIHelper.GetLabelDictonaryObject((int)SystemEntity.ThemeDetail, SessionVariables.RequestProfile.AuditId);
				Cache.Add(CacheConstants.ThemeDetailLabelDictionary, validColumns, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 60), CacheItemPriority.Default, null);

			}
			else
			{
				validColumns = (Dictionary<string, string>)Cache[CacheConstants.ThemeDetailLabelDictionary];
			}
			UIHelper.PopulateLabelsText(validColumns, (int)SystemEntity.ThemeDetail, SessionVariables.RequestProfile.AuditId, labelslist);


		}

		protected override void Clear()
		{
			lblThemeDetailId.Text = String.Empty;
			lblApplicationId.Text = String.Empty;
			lblValue.Text = String.Empty;
			lblThemeId.Text = String.Empty;
			lblThemeKeyId.Text = String.Empty;
			lblThemeCategoryId.Text = String.Empty;
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblThemeDetailIdText, lblApplicationIdText, lblValueText, lblThemeIdText, lblThemeKeyIdText, lblThemeCategoryIdText });
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
				lblThemeDetailIdText.Visible = isTesting;
				lblThemeDetailId.Visible = isTesting;
			}
			PopulateLabelsText();
		}

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.ThemeDetailLabelDictionary;
			PrimaryEntity = SystemEntity.ThemeDetail;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynThemeDetailId;
			PlaceHolderAuditHistory = dynAuditHistory;

			BorderDiv = borderdiv;
		}

		#endregion

	}
}