using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace Shared.UI.Web.Admin.TimeZone.Controls
{
	public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
	{
		#region properties

		public int? TimeZoneId
		{
			get
			{
				if (txtTimeZoneId.Enabled)
				{
					return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtTimeZoneId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtTimeZoneId.Text);
				}
			}
			set
			{
				txtTimeZoneId.Text = (value == null) ? String.Empty : value.ToString();
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

		public decimal? TimeDifference
		{
			get
			{
				if (string.IsNullOrEmpty(txtTimeDifference.Text))
				{
					return null;
				}
				else
				{
					return decimal.Parse(txtTimeDifference.Text);
				}
			}
			set
			{
				txtTimeDifference.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		#endregion properties

		#region private methods

		public override int? Save(string action)
		{
			var data = new TimeZoneDataModel();

			data.TimeZoneId		= TimeZoneId;
			data.TimeDifference = TimeDifference;
			data.Name			= Name;
			data.Description	= Description;
			data.SortOrder		= SortOrder;

			if (action == "Insert")
			{
				if(!Framework.Components.Core.TimeZoneDataManger.DoesExist(data, SessionVariables.RequestProfile))
				{
					Framework.Components.Core.TimeZoneDataManger.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
				Framework.Components.Core.TimeZoneDataManger.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of TimeZoneID ?
			return TimeZoneId;
		}

		public override void SetId(int setId, bool chkTimeZoneId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkTimeZoneId);
			txtTimeZoneId.Enabled = chkTimeZoneId;
			//txtDescription.Enabled = !chkTimeZoneId;
			//txtName.Enabled = !chkTimeZoneId;
			//txtSortOrder.Enabled = !chkTimeZoneId;
		}

		public void LoadData(int timeZoneId, bool showId)
		{
			// clear UI				

			Clear();

			var dataQuery = new TimeZoneDataModel();
			dataQuery.TimeZoneId = timeZoneId;

			var items = Framework.Components.Core.TimeZoneDataManger.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

			if (items.Count != 1) return;

			var item = items[0];

			TimeZoneId		= item.TimeZoneId;
			TimeDifference	= item.TimeDifference;
			Name			= item.Name;
			Description		= item.Description;
			SortOrder		= item.SortOrder;

			if (!showId)
			{
				txtTimeZoneId.Text = item.TimeZoneId.ToString();

				// only show Audit History in case of Update page, not for Clone.
				oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.TimeZone, timeZoneId, "TimeZone");
			}
			else
			{
				txtTimeZoneId.Text = String.Empty;
			}

			oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new TimeZoneDataModel();

			TimeZoneId		= data.TimeZoneId;
			TimeDifference	= data.TimeDifference;
			Description		= data.Description;
			Name			= data.Name;
			SortOrder		= data.SortOrder;
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			var isTesting = SessionVariables.IsTesting;
			txtTimeZoneId.Visible = isTesting;
			lblTimeZoneId.Visible = isTesting;
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey = "TimeZone";
			FolderLocationFromRoot = "Shared/Admin/TimeZone";
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TimeZone;

			// set object variable reference            
			PlaceHolderCore = dynTimeZoneId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
		}

		#endregion

	}
}