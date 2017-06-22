using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace Shared.UI.Web.Admin.Audit.AuditAction.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard

{
        #region properties       

        public int? AuditActionId
        {
            get
            {
                if (txtAuditActionId.Enabled)
                {
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtAuditActionId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtAuditActionId.Text);
                }
            }
			set
			{
				txtAuditActionId.Text = (value == null) ? String.Empty : value.ToString();
			}
        }
       
        #endregion properties

		#region private methods

		public override int? Save(string action)
		{
			var data = new DataModel.Framework.Audit.AuditActionDataModel();

			data.AuditActionId	= SystemKeyId;			
			data.Name			= Name;
			data.Description	= Description;
			data.SortOrder		= SortOrder;

			if (action == "Insert")
			{
                var dtAuditAction = Framework.Components.Audit.AuditActionDataManager.DoesExist(data, SessionVariables.RequestProfile);

				if (dtAuditAction.Rows.Count == 0)
				{
					Framework.Components.Audit.AuditActionDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
				Framework.Components.Audit.AuditActionDataManager.Update(data, SessionVariables.RequestProfile);
			}

			return AuditActionId;
		}

		public override void SetId(int setId, bool chkAuditActionId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkAuditActionId);
			txtAuditActionId.Enabled = chkAuditActionId;
			//txtDescription.Enabled = !chkAuditActionId;
			//txtName.Enabled = !chkAuditActionId;
			//txtSortOrder.Enabled = !chkAuditActionId;
		}

		public void LoadData(int auditActionId, bool showId)
		{	

			Clear();

			var dataQuery = new DataModel.Framework.Audit.AuditActionDataModel();
			dataQuery.AuditActionId = auditActionId;

			var items = Framework.Components.Audit.AuditActionDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

			if (items.Count != 1) return;

			var item = items[0];

			SetData(item);

			if (!showId)
			{
				SystemKeyId = item.AuditActionId;
				
				oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.AuditAction, auditActionId, "AuditAction");

			}
			else
			{
				CoreSystemKey.Text = String.Empty;
			}			
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new DataModel.Framework.Audit.AuditActionDataModel();

			SetData(data);
		}

		public void SetData(DataModel.Framework.Audit.AuditActionDataModel data)
		{
			SystemKeyId = data.AuditActionId;

			base.SetData(data);
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{		
			var isTesting = SessionVariables.IsTesting;
			CoreSystemKey.Visible = isTesting;
			lblAuditActionId.Visible = isTesting;			
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey				= "AuditAction";
			FolderLocationFromRoot			= "Shared/Admin/Audit/AuditAction";
			PrimaryEntity					= Framework.Components.DataAccess.SystemEntity.AuditAction;
			
			PlaceHolderCore					= dynAuditActionId;
			PlaceHolderAuditHistory			= dynAuditHistory;
			BorderDiv						= borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

			CoreSystemKey					= txtAuditActionId;
			CoreControlName					= txtName;
			CoreControlDescription			= txtDescription;
			CoreControlSortOrder			= txtSortOrder;

			CoreUpdateInfo = oUpdateInfo;			
		}
		
		#endregion                

    }
}
