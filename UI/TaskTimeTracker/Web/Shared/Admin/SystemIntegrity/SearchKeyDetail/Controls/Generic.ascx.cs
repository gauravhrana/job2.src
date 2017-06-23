using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace Shared.UI.Web.SystemIntegrity.SearchKeyDetail.Controls
{
	public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
	{

		#region properties

		public string BorderClass
		{
			set
			{
				borderdiv.Attributes["class"] = value;
			}
		}		

		public int? SearchKeyDetailId
		{
			get
			{
				if (txtSearchKeyDetailId.Enabled)
				{
					return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtSearchKeyDetailId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtSearchKeyDetailId.Text);
				}
			}
			set
			{
				txtSearchKeyDetailId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public int? SearchKeyId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtSearchKeyId.Text.Trim());
				else
					return int.Parse(drpSearchKeyId.SelectedItem.Value);
			}
			set
			{
				txtSearchKeyId.Text = (value == null) ? String.Empty : value.ToString();
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

		public string SearchParameter
		{
			get
			{
				return txtSearchParameter.Text;
			}
			set
			{
				txtSearchParameter.Text = value ?? String.Empty;
			}
		}

		#endregion properties

		#region private methods

		public override int? Save(string action)
		{
			var data = new SearchKeyDetailDataModel();

			data.SearchKeyDetailId = SearchKeyDetailId;
			data.SearchKeyId = SearchKeyId;
			data.SearchParameter = SearchParameter;
			data.SortOrder = SortOrder;

			if (action == "Insert")
			{
				if(!Framework.Components.Core.SearchKeyDetailDataManager.DoesExist(data, SessionVariables.RequestProfile))
				{
					Framework.Components.Core.SearchKeyDetailDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
				Framework.Components.Core.SearchKeyDetailDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of SearchKeyDetailId ?
			return SearchKeyDetailId;
		}

		public override void SetId(int setId, bool chkApplicationId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkApplicationId);
			txtSearchKeyDetailId.Enabled = chkApplicationId;
			//txtDescription.Disabled = chkApplicationId;
			//txtName.Enabled = !chkApplicationId;
			//txtSortOrder.Enabled = !chkApplicationId;
			//txtApplicationId.Enabled = !chkApplicationId;
			//txtSearchKeyId.Enabled = !chkApplicationId;
			//txtEntityKey.Enabled = !chkApplicationId;
		}

		public void LoadData(int searchKeyDetailId, bool showId)
		{
			// clear UI				
			Clear();

			// set up parameters
			var data = new SearchKeyDetailDataModel();
			data.SearchKeyDetailId = searchKeyDetailId;

			// get data
			var items = Framework.Components.Core.SearchKeyDetailDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match -- should log exception.
			if (items.Count != 1) return;

			var item = items[0];

			SearchKeyDetailId = item.SearchKeyDetailId;
			SearchKeyId = item.SearchKeyId;
			SearchParameter = item.SearchParameter;
			SortOrder = item.SortOrder;

			if (!showId)
			{
				txtSearchKeyDetailId.Text = item.SearchKeyDetailId.ToString();

				//PlaceHolderAuditHistory.Visible = true;
				// only show Audit History in case of Update page, not for Clone.
				oHistoryList.Setup(PrimaryEntity, searchKeyDetailId, PrimaryEntityKey);
			}
			else
			{
				txtSearchKeyDetailId.Text = String.Empty;
			}
			
			oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
		}		

		private void SetupDropdown()
		{
			var isTesting = SessionVariables.IsTesting;
			var SearchKeydata = Framework.Components.Core.SearchKeyDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(SearchKeydata, drpSearchKeyId, StandardDataModel.StandardDataColumns.Name, SearchKeyDataModel.DataColumns.SearchKeyId);

			if (isTesting)
			{
				drpSearchKeyId.AutoPostBack = true;
				if (drpSearchKeyId.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtSearchKeyId.Text.Trim()))
					{
						drpSearchKeyId.SelectedValue = txtSearchKeyId.Text;
					}
					else
					{
						txtSearchKeyId.Text = drpSearchKeyId.SelectedItem.Value;
					}
				}
				txtSearchKeyId.Visible = true;
			}
			else
			{
				if (!string.IsNullOrEmpty(txtSearchKeyId.Text.Trim()))
				{
					drpSearchKeyId.SelectedValue = txtSearchKeyId.Text;
				}
			}
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new SearchKeyDetailDataModel();

			SearchKeyDetailId = data.SearchKeyDetailId;
			SearchKeyId = data.SearchKeyId;
			SearchParameter = data.SearchParameter;
			SortOrder = data.SortOrder;
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			//var isTesting = SessionVariables.IsTesting;
			txtSearchKeyDetailId.Visible = false;
			lblSearchKeyDetailId.Visible = false;
			if (!IsPostBack)
			{
				SetupDropdown();
			}
		}		

		protected void drpSearchKeyId_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtSearchKeyId.Text = drpSearchKeyId.SelectedItem.Value;
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.SearchKeyDetail;
			PrimaryEntityKey = "SearchKeyDetail";
			FolderLocationFromRoot = "SearchKeyDetail";

			// set object variable reference            
			PlaceHolderCore = dynSearchKeyDetailId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
		}

		#endregion
	}
}
