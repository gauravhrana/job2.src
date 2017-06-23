using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;

namespace Shared.UI.Web.Admin.SystemEntityType.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
    {

        #region properties

		public string ConvertDateTimeFormat
		{
			get
			{
				return DateTimeHelper.CovertDateFormatToJavascript();
			}
		}

        public int? SystemEntityTypeId
        {
            get
            {
                if (txtSystemEntityTypeId.Enabled)
                {
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtSystemEntityTypeId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtSystemEntityTypeId.Text);
                }
            }
            set
            {
                txtSystemEntityTypeId.Text = (value == null) ? String.Empty : value.ToString();
            }
        }

        public string EntityName
        {
            get
            {
                return txtEntityName.Text;
            }
            set
            {
                txtEntityName.Text = value ?? String.Empty;
            }
        }

		public DateTime? CreatedDate
		{
			get
			{
				return DateTimeHelper.FromUserDateFormatToDate(txtCreatedDate.Text.Trim());
			}
			set
			{
				txtCreatedDate.Text = (value == null) ? String.Empty : value.Value.ToString(SessionVariables.UserDateFormat);
			}
		}

		public string PrimaryDatabase
		{
			get
			{
				return txtPrimaryDatabase.Text;
			}
			set
			{
				txtPrimaryDatabase.Text = value ?? String.Empty;
			}
		}

        public string EntityDescription
        {
            get
            {
                return txtEntityDescription.Text;
            }
            set
            {
                txtEntityDescription.Text = value ?? String.Empty;
            }
        }

        public int? NextValue
        {
            get
            {
                return int.Parse(txtNextValue.Text);
            }
            set
            {
                txtNextValue.Text = (value == null) ? String.Empty : value.ToString();
            }
        }

        #endregion properties

        #region methods

        public override void SetId(int setId, bool chkSystemEntityTypeId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkSystemEntityTypeId);
            txtSystemEntityTypeId.Enabled = chkSystemEntityTypeId;
            //txtEntityName.Enabled = !chkSystemEntityTypeId;
            //txtEntityDescription.Enabled = !chkSystemEntityTypeId;
            //txtNextValue.Enabled = !chkSystemEntityTypeId;
        }

		public void LoadData(int systemEntityTypeId, bool showId)
		{
			// clear UI				
			Clear();

			// set up parameters
			var data = new SystemEntityTypeDataModel();
			data.SystemEntityTypeId = systemEntityTypeId;

			// get data
			var items = Framework.Components.Core.SystemEntityTypeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match -- should log exception.
			if (items.Count != 1) return;

			var item = items[0];

			EntityDescription	= item.EntityDescription;
			EntityName			= item.EntityName;
			NextValue			= item.NextValue;
			PrimaryDatabase		= item.PrimaryDatabase;
			CreatedDate			= item.CreatedDate;
			
			if (!showId)
			{
				txtSystemEntityTypeId.Text = item.SystemEntityTypeId.ToString();
				//PlaceHolderAuditHistory.Visible = true;
				// only show Audit History in case of Update page, not for Clone.
				oHistoryList.Setup(PrimaryEntity, systemEntityTypeId, PrimaryEntityKey);
			}
			else
			{
				txtSystemEntityTypeId.Text = String.Empty;
			}

			oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
		}        

        protected override void Clear()
        {
            base.Clear();

            var data = new SystemEntityTypeDataModel();

            SystemEntityTypeId = data.SystemEntityTypeId;
            EntityName = data.EntityName;
            EntityDescription = data.EntityDescription;
            NextValue = data.NextValue;
			CreatedDate = data.CreatedDate;
			PrimaryDatabase = data.PrimaryDatabase;

        }

		public override int? Save(string action)
		{
			var data = new SystemEntityTypeDataModel();

			data.SystemEntityTypeId			= SystemEntityTypeId;
			data.EntityDescription			= EntityDescription;
			data.EntityName					= EntityName;
			data.NextValue					= NextValue;
			data.PrimaryDatabase			= PrimaryDatabase;
			data.CreatedDate				= CreatedDate;

			if (action == "Insert")
			{
				if(!Framework.Components.Core.SystemEntityTypeDataManager.DoesExist(data, SessionVariables.RequestProfile))
				{
					Framework.Components.Core.SystemEntityTypeDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
				Framework.Components.Core.SystemEntityTypeDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of ScheduleID ?
			return SystemEntityTypeId;
		}

        #endregion

        #region events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntityKey = "SystemEntityType";
            FolderLocationFromRoot = "/Shared/Admin";
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.SystemEntityType;

            // set object variable reference            
            PlaceHolderCore = dynSystemEntityTypeId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            //CalendarExtenderCreatedDate.Format = SessionVariables.UserDateFormat;
            lblUserDateTimeFormat.Text = "Date Format: " + SessionVariables.UserDateFormat;
        }

        #endregion

    }
}