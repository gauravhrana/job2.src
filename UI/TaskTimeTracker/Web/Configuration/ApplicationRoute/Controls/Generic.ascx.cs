using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;


namespace Shared.UI.Web.Configuration.ApplicationRoute.Controls
{
	public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
	{

		#region properties
	
		public int? ApplicationRouteId
		{
			get
			{
				if (txtApplicationRouteId.Enabled)
				{
					return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtApplicationRouteId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtApplicationRouteId.Text);
				}
			}
			set
			{
				txtApplicationRouteId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public string RouteName
		{
			get
			{
				return txtRouteName.Text;
			}
			set
			{
				txtRouteName.Text = value ?? String.Empty;
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

		public string ProposedRoute
		{
			get
			{
				return txtProposedRoute.Text;
			}
			set
			{
				txtProposedRoute.Text = value ?? String.Empty;
			}
		}

		public string RelativeRoute
		{
			get
			{
				return txtRelativeRoute.Text;
			}
			set
			{
				txtRelativeRoute.Text = value ?? String.Empty;
			}
		}

		public string Description
		{
			get
			{
				return Framework.Components.DefaultDataRules.CheckAndGetDescription(txtDescription.Text, txtDescription.Text);
			}
			set
			{
				txtDescription.Text = value ?? String.Empty;
			}
		}
		
		#endregion properties

		#region private methods

		public override int? Save(string action)
		{
			var data = new ApplicationRouteDataModel();

			data.ApplicationRouteId = ApplicationRouteId;
			data.EntityName			= EntityName;
			data.RouteName			= RouteName;
			data.RelativeRoute		= RelativeRoute;
			data.ProposedRoute		= ProposedRoute;
			data.Description		= Description;

			if (action == "Insert")
			{
				Framework.Components.Core.ApplicationRouteDataManager.Create(data, SessionVariables.RequestProfile);
			}
			else
			{
				Framework.Components.Core.ApplicationRouteDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of ApplicationRouteID ?
			return ApplicationRouteId;
		}

		public override void SetId(int setId, bool chkApplicationRouteId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkApplicationRouteId);
			txtApplicationRouteId.Enabled = chkApplicationRouteId;
			//txtDescription.Enabled = !chkApplicationRouteId;
			//txtName.Enabled = !chkApplicationRouteId;
			//txtSortOrder.Enabled = !chkApplicationRouteId;
		}

		public void LoadData(int applicationRouteId, bool showId)
		{
			// clear UI				
			Clear();

			// set up parameters
			var data = new ApplicationRouteDataModel();
			data.ApplicationRouteId = applicationRouteId;

			// get data
			var items = Framework.Components.Core.ApplicationRouteDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match -- should log exception.
			if (items.Count != 1) return;

			var item = items[0];

			ApplicationRouteId	= item.ApplicationRouteId;
			EntityName			= item.EntityName;
			RouteName			= item.RouteName;
			RelativeRoute		= item.RelativeRoute;
			ProposedRoute		= item.ProposedRoute;
			Description			= item.Description;

			if (!showId)
			{
				txtApplicationRouteId.Text = item.ApplicationRouteId.ToString();

				//PlaceHolderAuditHistory.Visible = true;
				// only show Audit History in case of Update page, not for Clone.
				oHistoryList.Setup(PrimaryEntity, applicationRouteId, PrimaryEntityKey);
			}
			else
			{
				txtApplicationRouteId.Text = String.Empty;
			}

			oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new ApplicationRouteDataModel();

			ApplicationRouteId	= data.ApplicationRouteId;
			EntityName			= data.EntityName;
			RouteName			= data.RouteName;
			RelativeRoute		= data.RelativeRoute;
			ProposedRoute		= data.ProposedRoute;
			Description			= data.Description;			
		}
		
		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			var isTesting = SessionVariables.IsTesting;
			txtApplicationRouteId.Visible = isTesting;
			lblApplicationRouteId.Visible = isTesting;
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationRoute;
			PrimaryEntityKey = "ApplicationRoute";
			FolderLocationFromRoot = "Shared/Configuration/ApplicationRoute";

			// set object variable reference            
			PlaceHolderCore = dynApplicationRouteId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
		}

		#endregion
	}
}