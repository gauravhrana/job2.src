using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace Shared.UI.Web.SystemIntegrity.SearchKeyDetailItem.Controls
{
	public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
	{
		#region properties
		
		public int? SearchKeyDetailItemId
		{
			get
			{
				if (txtSearchKeyDetailItemId.Enabled)
				{
					return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtSearchKeyDetailItemId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtSearchKeyDetailItemId.Text);
				}
			}
			set
			{
				txtSearchKeyDetailItemId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public int? SearchKeyDetailId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtSearchKeyDetailId.Text.Trim());
				else
					return int.Parse(drpSearchKeyDetailId.SelectedItem.Value);
			}
			set
			{
				txtSearchKeyDetailId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public string Value
		{
			get
			{
				return txtValue.Text;
			}
			set
			{
				txtValue.Text = value ?? String.Empty;
			}
		}

		public int? SortOrder
		{
			get
			{
				return Framework.Components.DefaultDataRules.CheckAndGetSortOrder(txtSortOrder.Text);
			}
			set
			{
				txtSortOrder.Text = (value == null) ? String.Empty : value.ToString();
			}
		}		

		#endregion properties

		#region private methods

		public override int? Save(string action)
		{
			var data = new SearchKeyDetailItemDataModel();

			data.SearchKeyDetailItemId = SearchKeyDetailItemId;
			data.SearchKeyDetailId = SearchKeyDetailId;
			data.Value = Value;
			data.SortOrder = SortOrder;

			if (action == "Insert")
			{
				if(!Framework.Components.Core.SearchKeyDetailItemDataManager.DoesExist(data, SessionVariables.RequestProfile))
				{
					Framework.Components.Core.SearchKeyDetailItemDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
				Framework.Components.Core.SearchKeyDetailItemDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of SearchKeyDetailItemId ?
			return SearchKeyDetailItemId;
		}

		public override void SetId(int setId, bool chkApplicationId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkApplicationId);
			txtSearchKeyDetailItemId.Enabled = chkApplicationId;
			//txtDescription.Disabled = chkApplicationId;
			//txtName.Enabled = !chkApplicationId;
			//txtSortOrder.Enabled = !chkApplicationId;
			//txtApplicationId.Enabled = !chkApplicationId;
			//txtSearchKeyId.Enabled = !chkApplicationId;
			//txtEntityKey.Enabled = !chkApplicationId;
		}

		public void LoadData(int searchKeyDetailItemId, bool showId)
		{
			// clear UI				
			Clear();

			// set up parameters
			var data = new SearchKeyDetailItemDataModel();
			data.SearchKeyDetailItemId = searchKeyDetailItemId;

			// get data
			var items = Framework.Components.Core.SearchKeyDetailItemDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match -- should log exception.
			if (items.Count != 1) return;

			var item = items[0];

			SearchKeyDetailItemId = item.SearchKeyDetailItemId;
			SearchKeyDetailId = item.SearchKeyDetailId;
			Value = item.Value;			
			SortOrder = item.SortOrder;

			if (!showId)
			{
				txtSearchKeyDetailItemId.Text = item.SearchKeyDetailItemId.ToString();

				//PlaceHolderAuditHistory.Visible = true;
				// only show Audit History in case of Update page, not for Clone.
				oHistoryList.Setup(PrimaryEntity, searchKeyDetailItemId, PrimaryEntityKey);
			}
			else
			{
				txtSearchKeyDetailId.Text = String.Empty;
			}

			//oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
		}	

		private void SetupDropdown()
		{
			var isTesting = SessionVariables.IsTesting;
			var SearchKeyDetaildata = Framework.Components.Core.SearchKeyDetailDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(SearchKeyDetaildata, drpSearchKeyDetailId, SearchKeyDetailDataModel.DataColumns.SearchParameter, SearchKeyDetailDataModel.DataColumns.SearchKeyDetailId);

			if (isTesting)
			{
				drpSearchKeyDetailId.AutoPostBack = true;
				if (drpSearchKeyDetailId.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtSearchKeyDetailId.Text.Trim()))
					{
						drpSearchKeyDetailId.SelectedValue = txtSearchKeyDetailId.Text;
							
					}
					else
					{
						txtSearchKeyDetailId.Text = drpSearchKeyDetailId.SelectedItem.Value;
					}
				}
				txtSearchKeyDetailId.Visible = true;
			}
			else
			{
				if (!string.IsNullOrEmpty(txtSearchKeyDetailId.Text.Trim()))
				{
					drpSearchKeyDetailId.SelectedValue = txtSearchKeyDetailId.Text;
				}
			}
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new SearchKeyDetailItemDataModel();

			SearchKeyDetailId = data.SearchKeyDetailId;
			SearchKeyDetailItemId = data.SearchKeyDetailItemId;
			SortOrder = data.SortOrder;
			Value = data.Value;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.SearchKeyDetailItem;
			PrimaryEntityKey = "SearchKeyDetailItem";
			FolderLocationFromRoot = "SearchKeyDetailItem";

			// set object variable reference            
			PlaceHolderCore = dynSearchKeyDetailId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
		}

		protected override void Page_Load(object sender, EventArgs e)
		{
			//var isTesting = SessionVariables.IsTesting;
			txtSearchKeyDetailItemId.Visible = false;
			lblSearchKeyDetailItemId.Visible = false;
			if (!IsPostBack)
			{
				SetupDropdown();
			}
		}		

		protected void drpSearchKeyDetail_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtSearchKeyDetailId.Text = drpSearchKeyDetailId.SelectedItem.Value;			
		}

		#endregion
	}
}