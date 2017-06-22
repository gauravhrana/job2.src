using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;


namespace Shared.UI.Web.Admin.ConnectionString.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
	{

		#region Methods

		protected override void Clear()
		{
			lblConnectionStringId.Text = String.Empty;
			lblName.Text               = String.Empty;
			lblDescription.Text        = String.Empty;
			lblDataSource.Text         = String.Empty;
			lblInitialCatalog.Text     = String.Empty;
			lblUserName.Text           = String.Empty;
			lblPassword.Text           = String.Empty;
			lblProviderName.Text       = String.Empty;
		}

		protected override void ShowData(int ConnectionStringId)
		{

			oDetailButtonPanel.SetId = SetId;
			var data = new ConnectionStringDataModel();
			data.ConnectionStringId = ConnectionStringId;
			var items = Framework.Components.Core.ConnectionStringDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			if (items.Count == 1)
			{
				var item = items[0];

				lblConnectionStringId.Text = item.ConnectionStringId.ToString();
				lblName.Text               = item.Name;
				lblDescription.Text        = item.Description;
				lblDataSource.Text         = item.DataSource;
				lblInitialCatalog.Text     = item.InitialCatalog;
				lblUserName.Text           = item.UserName;
				lblPassword.Text           = item.Password;
				lblProviderName.Text       = item.ProviderName;

				oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

				oHistoryList.Setup(PrimaryEntity, ConnectionStringId, "ConnectionString");
			}
			else
			{
				Clear();
			}
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblConnectionStringIdText, lblNameText
													  , lblDescriptionText, lblDataSourceText, lblInitialCatalogText, lblUserNameText
													  , lblPasswordText,	lblProviderNameText	});
			}

			return LabelListCore;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{

			base.OnInit(e);

			// set basic variables
			DictionaryLabel         = CacheConstants.MenuCategoryLabelDictionary;
			PrimaryEntity           = Framework.Components.DataAccess.SystemEntity.ConnectionString;

			// set object variable reference            
			PlaceHolderCore         = dynConnectionStringId;
			PlaceHolderAuditHistory = dynAuditHistory;
			MainTable               = tblMain1;
			BorderDiv               = borderdiv;
		}

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				dynConnectionStringId.Visible = false;
				lblConnectionStringIdText.Visible = false;
			}

			PopulateLabelsText();
		}

		#endregion

	}
	
}