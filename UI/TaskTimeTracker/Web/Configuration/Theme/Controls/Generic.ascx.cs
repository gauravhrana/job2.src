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
	public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
	{

		#region methods

		public override int? Save(string action)
		{
			var data = new ThemeDataModel();

			data.ThemeId = SystemKeyId;
			data.Name = Name;
			data.Description = Description;
			data.SortOrder = SortOrder;
			//data.IsAllTab = IsAllTab;

			if (action == "Insert")
			{
				if(!Framework.Components.Core.ThemeDataManager.DoesExist(data, SessionVariables.RequestProfile))
				{
					Framework.Components.Core.ThemeDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
				Framework.Components.Core.ThemeDataManager.Update(data, SessionVariables.RequestProfile);
			}

			return data.ThemeId;
		}

		public override void SetId(int setId, bool chkThemeId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkThemeId);
			CoreSystemKey.Enabled = chkThemeId;
			//txtDescription.Enabled = !chkThemeId;
			//txtName.Enabled = !chkThemeId;
			//txtSortOrder.Enabled = !chkThemeId;
			//txtIsAllTab.Enabled = !chkThemeId;
		}

		public void LoadData(int themeId, bool showId)
		{
			Clear();

			var data = new ThemeDataModel();
			data.ThemeId = themeId;

			var items = Framework.Components.Core.ThemeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			if (items.Count != 1) return;

			var item = items[0];

			SetData(item);

			if (!showId)
			{
				SystemKeyId = item.ThemeId;
				// oHistoryList.Setup(PrimaryEntity, ThemeId, PrimaryEntityKey);
			}
			else
			{
				CoreSystemKey.Text = String.Empty;
			}
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new ThemeDataModel();

			SetData(data);
		}

		public void SetData(ThemeDataModel data)
		{
			SystemKeyId = data.ThemeId;

			base.SetData(data);
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			var isTesting = SessionVariables.IsTesting;
			CoreSystemKey.Visible = isTesting;
			lblThemeId.Visible = isTesting;
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Theme;
			PrimaryEntityKey = "Theme";
			FolderLocationFromRoot = "Theme";

			PlaceHolderCore = dynThemeId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

			CoreSystemKey = txtThemeId;
			CoreControlName = txtName;
			CoreControlDescription = txtDescription;
			CoreControlSortOrder = txtSortOrder;

			CoreUpdateInfo = oUpdateInfo;
		}

		#endregion

	}
}