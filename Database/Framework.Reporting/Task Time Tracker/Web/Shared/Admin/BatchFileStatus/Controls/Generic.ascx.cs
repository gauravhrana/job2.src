using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.Import;
using Shared.UI.WebFramework;

namespace Shared.UI.Web.BatchFileStatus.Controls
{
	public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
	{
		#region properties

		public int? BatchFileStatusId
		{
			get
			{
				if (txtBatchFileStatusId.Enabled)
				{
					return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtBatchFileStatusId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtBatchFileStatusId.Text);
				}
			}
			set
			{
				txtBatchFileStatusId.Text = (value == null) ? String.Empty : value.ToString();
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
				return Framework.Components.DefaultDataRules.CheckAndGetDescription(txtName.Text, txtDescription.Text);
			}
			set
			{
				txtDescription.Text = value ?? String.Empty;
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
			var data = new BatchFileStatusDataModel();

			data.BatchFileStatusId	= BatchFileStatusId;
			data.Name				= Name;
			data.Description		= Description;
			data.SortOrder			= SortOrder;

			if (action == "Insert")
			{
				var dtBatchFileStatus = Framework.Components.Import.BatchFileStatusDataManager.DoesExist(data, SessionVariables.RequestProfile);

				if (dtBatchFileStatus.Rows.Count == 0)
				{
					Framework.Components.Import.BatchFileStatusDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
				Framework.Components.Import.BatchFileStatusDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of BatchFileStatusID ?
			return BatchFileStatusId;
		}

		public override void SetId(int setId, bool chkBatchFileStatusId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkBatchFileStatusId);
			txtBatchFileStatusId.Enabled = chkBatchFileStatusId;
			//txtDescription.Enabled = !chkBatchFileStatusId;
			//txtName.Enabled = !chkBatchFileStatusId;
			//txtSortOrder.Enabled = !chkBatchFileStatusId;
		}

		public void LoadData(int batchFileStatusId, bool showId)
		{
			// clear UI				

			Clear();

			var dataQuery = new BatchFileStatusDataModel();
			dataQuery.BatchFileStatusId = batchFileStatusId;

			var items = Framework.Components.Import.BatchFileStatusDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

			if (items.Count != 1) return;

			var item = items[0];

			BatchFileStatusId	= item.BatchFileStatusId;
			Name				= item.Name;
			Description			= item.Description;
			SortOrder			= item.SortOrder;

			if (!showId)
			{
				txtBatchFileStatusId.Text = item.BatchFileStatusId.ToString();

				// only show Audit History in case of Update page, not for Clone.
				oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.BatchFileStatus, batchFileStatusId, "BatchFileStatus");

			}
			else
			{
				txtBatchFileStatusId.Text = String.Empty;
			}

			oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
		}


		protected override void Clear()
		{
			base.Clear();

			var data = new BatchFileStatusDataModel();

			BatchFileStatusId	= data.BatchFileStatusId;
			Description			= data.Description;
			Name				= data.Name;
			SortOrder			= data.SortOrder;
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			var isTesting = SessionVariables.IsTesting;
			txtBatchFileStatusId.Visible = isTesting;
			lblBatchFileStatusId.Visible = isTesting;
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey = "BatchFileStatus";
			FolderLocationFromRoot = "Shared/Admin/BatchFileStatus";
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.BatchFileStatus;

			// set object variable reference            
			PlaceHolderCore = dynBatchFileStatusId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
		}

		#endregion


	}
}