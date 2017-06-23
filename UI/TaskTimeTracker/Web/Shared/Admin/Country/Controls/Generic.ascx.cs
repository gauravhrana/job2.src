using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;

using System.Data;

namespace Shared.UI.Web.Admin.Country.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
    {

        #region properties

        public int? CountryId
        {
            get
            {
                if (txtCountryId.Enabled)
                {
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtCountryId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtCountryId.Text);
                }
            }
			set
			{
				txtCountryId.Text = (value == null) ? String.Empty : value.ToString();
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

        public decimal? TimeZoneId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtTimeZoneId.Text.Trim());
                else
                    return int.Parse(drpTimeZoneList.SelectedItem.Value);
            }
			set
			{
				txtTimeZoneId.Text = (value == null) ? String.Empty : value.ToString();
			}

        }     

        #endregion properties

		#region private methods

		public override int? Save(string action)
		{
			var data = new CountryDataModel();

			data.CountryId		= CountryId;
			data.TimeZoneId		= TimeZoneId;
			data.Name			= Name;
			data.Description	= Description;
			data.SortOrder		= SortOrder;

			if (action == "Insert")
			{
				if(!Framework.Components.Core.CountryDataManager.DoesExist(data, SessionVariables.RequestProfile))
				{
					Framework.Components.Core.CountryDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
				Framework.Components.Core.CountryDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of CountryID ?
			return CountryId;
		}

		public override void SetId(int setId, bool chkCountryId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkCountryId);
			txtCountryId.Enabled = chkCountryId;
			//txtDescription.Enabled = !chkCountryId;
			//txtName.Enabled = !chkCountryId;
			//txtSortOrder.Enabled = !chkCountryId;
		}

		public void LoadData(int countryId, bool showId)
		{
			// clear UI				

			Clear();

			var dataQuery = new CountryDataModel();
			dataQuery.CountryId = countryId;

			var items = Framework.Components.Core.CountryDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

			if (items.Count != 1) return;

			var item = items[0];

			CountryId	= item.CountryId;
			TimeZoneId	= item.TimeZoneId;
			Name		= item.Name;
			Description = item.Description;
			SortOrder	= item.SortOrder;

			if (!showId)
			{
				txtCountryId.Text = item.CountryId.ToString();

				// only show Audit History in case of Update page, not for Clone.
				oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.Country, countryId, "Country");

			}
			else
			{
				txtCountryId.Text = String.Empty;
			}

			oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
		}


		protected override void Clear()
		{
			base.Clear();

			var data = new CountryDataModel();

			CountryId	= data.CountryId;
			TimeZoneId	= data.TimeZoneId;
			Description = data.Description;
			Name		= data.Name;
			SortOrder	= data.SortOrder;
		}
		
        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;
			var TimeZoneData = Framework.Components.Core.TimeZoneDataManger.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(TimeZoneData, drpTimeZoneList, StandardDataModel.StandardDataColumns.Name,
                TimeZoneDataModel.DataColumns.TimeZoneId);

            if (isTesting)
            {
                drpTimeZoneList.AutoPostBack = true;
                if (drpTimeZoneList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtTimeZoneId.Text.Trim()))
                    {
                        drpTimeZoneList.SelectedValue = txtTimeZoneId.Text;
                    }
                    else
                    {
                        txtTimeZoneId.Text = drpTimeZoneList.SelectedItem.Value;
                    }
                }
                txtTimeZoneId.Visible = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtTimeZoneId.Text.Trim()))
                {
                    drpTimeZoneList.SelectedValue = txtTimeZoneId.Text;
                }
            }
        } 
       
        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                SetupDropdown();
            var isTesting = SessionVariables.IsTesting;
            txtCountryId.Visible = isTesting;
            lblCountryId.Visible = isTesting;
        }

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey = "Country";
			FolderLocationFromRoot = "Shared/Admin/Country";
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Country;

			// set object variable reference            
			PlaceHolderCore = dynCountryId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
		}

		protected void drpTimeZoneList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtTimeZoneId.Text = drpTimeZoneList.SelectedItem.Value;
		} 

        #endregion

    }
}