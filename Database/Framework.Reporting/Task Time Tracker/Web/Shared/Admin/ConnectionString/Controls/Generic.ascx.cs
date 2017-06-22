using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;

namespace Shared.UI.Web.Admin.ConnectionString.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
    {

        #region properties

        public int? ConnectionStringId
        {
            get
            {
                if (txtConnectionStringId.Enabled)
                {
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtConnectionStringId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtConnectionStringId.Text);
                }
            }
            set
            {
                txtConnectionStringId.Text = (value == null) ? String.Empty : value.ToString();
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
				return txtDescription.Text;
			}
			set
			{
				txtDescription.Text = value ?? String.Empty;
			}
		}

		public string DataSource
		{
			get
			{
				return txtDataSource.Text;
			}
			set
			{
				txtDataSource.Text = value ?? String.Empty;
			}
		}

		public string InitialCatalog
		{
			get
			{
				return txtInitialCatalog.Text;
			}
			set
			{
				txtInitialCatalog.Text = value ?? String.Empty;
			}
		}

		public string UserName
		{
			get
			{
				return txtUserName.Text;
			}
			set
			{
				txtUserName.Text = value ?? String.Empty;
			}
		}

		public string Password
		{
			get
			{
				return txtPassword.Text;
			}
			set
			{
				txtPassword.Text = value ?? String.Empty;
			}
		}

		public string ProviderName
		{
			get
			{
				return txtProviderName.Text;
			}
			set
			{
				txtProviderName.Text = value ?? String.Empty;
			}
		}

		protected override string ValidationConfigFile
		{
			get
			{
				return Server.MapPath("~/Shared/Admin/ConnectionString/Controls/Validation.xml");
			}
		}

        #endregion properties

		#region methods

		public override int? Save(string action)
		{
			var data = new ConnectionStringDataModel();

			data.Name           = Name;
			data.Description    = Description;
			data.DataSource     = DataSource;
			data.InitialCatalog = InitialCatalog;
			data.UserName       = UserName;
			data.Password       = Password;
			data.ProviderName   = ProviderName;

			if (action == "Insert")
			{
				var dtMenu = Framework.Components.Core.ConnectionStringDataManager.DoesExist(data, SessionVariables.RequestProfile);

				if (dtMenu.Rows.Count == 0)
				{
					Framework.Components.Core.ConnectionStringDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
				data.ConnectionStringId = ConnectionStringId;
				Framework.Components.Core.ConnectionStringDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of ClientID ?
			return data.ConnectionStringId;
		}

        public override void SetId(int setId, bool chkConnectionStringId)
        {
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkConnectionStringId);
			txtConnectionStringId.Enabled = chkConnectionStringId;
        }

		public void LoadData(int ConnectionStringId, bool showId)
		{
			// clear UI				
			Clear();

			// set up parameters
			var data = new ConnectionStringDataModel();
			data.ConnectionStringId = ConnectionStringId;

			// get data
			var items = Framework.Components.Core.ConnectionStringDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match -- should log exception.
			if (items.Count != 1) return;

			var item = items[0];

			Name            = item.Name;
			Description	    = item.Description;
			DataSource		= item.DataSource;
			InitialCatalog  = item.InitialCatalog;
			UserName        = item.UserName;
			Password        = item.Password;
			ProviderName    = item.ProviderName;
			
			
			if (!showId)
			{
				txtConnectionStringId.Text = item.ConnectionStringId.ToString();
				//PlaceHolderAuditHistory.Visible = true;
				// only show Audit History in case of Update page, not for Clone.
				oHistoryList.Setup(PrimaryEntity, ConnectionStringId, PrimaryEntityKey);
			}
			else
			{
				txtConnectionStringId.Text = String.Empty;
			}

			oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
		}        

        protected override void Clear()
        {
            base.Clear();

            var item = new ConnectionStringDataModel();

            Name            = item.Name;
			Description	    = item.Description;
			DataSource		= item.DataSource;
			InitialCatalog  = item.InitialCatalog;
			UserName        = item.UserName;
			Password        = item.Password;
			ProviderName    = item.ProviderName;

        }

        #endregion

        #region events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntityKey                = "ConnectionString";
            FolderLocationFromRoot          = "/Shared/Admin";
            PrimaryEntity                   = Framework.Components.DataAccess.SystemEntity.ConnectionString;

            // set object variable reference            
            PlaceHolderCore                 = dynConnectionStringId;
            PlaceHolderAuditHistory         = dynAuditHistory;
            BorderDiv                       = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

        }

		protected override void Page_Load(object sender, EventArgs e)
		{
			var isTesting = SessionVariables.IsTesting;
			txtConnectionStringId.Visible = isTesting;
			lblConnectionStringId.Visible = isTesting;
		}

        #endregion

    }
}