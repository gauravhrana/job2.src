using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace Shared.UI.Web.SystemIntegrity.SearchKey.Controls
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

		public int? SearchKeyId
		{
			get
			{
				if (txtSearchKeyId.Enabled)
				{
					return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtSearchKeyId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtSearchKeyId.Text);
				}
			}
			set
			{
				txtSearchKeyId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public string Name
		{
			get
			{
				return txtName.Text; 
			}
			set
			{
				txtName.Text = value ?? String.Empty;
			}
		}

		public string Description
		{
			get
			{
				return Framework.Components.DefaultDataRules.CheckAndGetDescription(txtName.Text, txtDescription.InnerText);
			}
			set
			{
				txtDescription.InnerText = value ?? String.Empty;
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

		public string View
		{
			get
			{
				return txtView.Text.Trim();				
			}
			set
			{
				txtView.Text = value ?? String.Empty;
			}
		}

		#endregion properties

		#region private methods

		public override int? Save(string action)
		{
			var data = new SearchKeyDataModel();

			data.SearchKeyId = SearchKeyId;
			data.Name = Name;
			data.Description = Description;
			data.View = View;
			data.SortOrder = SortOrder;

			if (action == "Insert")
			{
				if(!Framework.Components.Core.SearchKeyDataManager.DoesExist(data, SessionVariables.RequestProfile))
				{
					Framework.Components.Core.SearchKeyDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
				Framework.Components.Core.SearchKeyDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of SearchKeyId ?
			return SearchKeyId;
		}

		public override void SetId(int setId, bool chkApplicationId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkApplicationId);
			txtSearchKeyId.Enabled = chkApplicationId;
			//txtDescription.Disabled = chkApplicationId;
			//txtName.Enabled = !chkApplicationId;
			//txtSortOrder.Enabled = !chkApplicationId;
			//txtApplicationId.Enabled = !chkApplicationId;
			//txtSystemEntityTypeId.Enabled = !chkApplicationId;
			//txtExpirationDate.Enabled = !chkApplicationId;
		}

		public void LoadData(int searchKeyId, bool showId)
		{
			// clear UI				
			Clear();

			// set up parameters
			var data = new SearchKeyDataModel();
			data.SearchKeyId = searchKeyId;

			// get data
			var items = Framework.Components.Core.SearchKeyDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match -- should log exception.
			if (items.Count != 1) return;

			var item = items[0];

			SearchKeyId = item.SearchKeyId;
			Name = item.Name;
			Description = item.Description;
			View = item.View;
			SortOrder = item.SortOrder;

			if (!showId)
			{
				txtSearchKeyId.Text = item.SearchKeyId.ToString();

				//PlaceHolderAuditHistory.Visible = true;
				// only show Audit History in case of Update page, not for Clone.
				oHistoryList.Setup(PrimaryEntity, searchKeyId, PrimaryEntityKey);
			}
			else
			{
				txtSearchKeyId.Text = String.Empty;
			}

			oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new SearchKeyDataModel();

			SearchKeyId = data.SearchKeyId;
			Description = data.Description;
			Name = data.Name;
			SortOrder = data.SortOrder;
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			//var isTesting = SessionVariables.IsTesting;
			txtSearchKeyId.Visible = false;
			lblSearchKeyId.Visible = false;			
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.SearchKey;
			PrimaryEntityKey = "SearchKey";
			FolderLocationFromRoot = "SearchKey";

			// set object variable reference            
			PlaceHolderCore = dynSearchKeyId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
		}	

		#endregion
	}
}