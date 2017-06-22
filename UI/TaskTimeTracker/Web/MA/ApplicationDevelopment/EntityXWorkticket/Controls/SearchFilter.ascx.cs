using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.DataAccess;
using DataModel.Framework.Configuration;
using Framework.Components.UserPreference;
using DataModel.Framework.AuthenticationAndAuthorization;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using ApplicationContainer.UI.Web.BaseUI;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.EntityXWorkTicket.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
	{

		#region variables

		private int entityXWorkTicketId;

		public int EntityXWorkTicketId
		{
			get
			{
				return entityXWorkTicketId;
			}
			set
			{
				entityXWorkTicketId = value;
			}
		}


		public EntityXWorkTicketDataModel SearchParameters
		{
			get
			{
				var data = new EntityXWorkTicketDataModel();

				SearchFilterControl.SetSearchParameters(data);

				CommonSearchParameters();

				return data;
			}
		}

		public override void LoadListBoxSources(string fieldName, ListBox lstBoxControl)
		{
			base.LoadListBoxSources(fieldName, lstBoxControl);
			lstBoxControl.Items.Add(new ListItem("All", "-1"));
			var entityXWorkTicketsData = TaskTimeTracker.Components.Module.ApplicationDevelopment.EntityXWorkTicketDataManager.GetList(SessionVariables.RequestProfile);
            entityXWorkTicketsData = entityXWorkTicketsData.OrderBy(x => x.Entity).ToList();
            UIHelper.LoadDropDown(entityXWorkTicketsData, lstBoxControl,
                EntityXWorkTicketDataModel.DataColumns.Entity,
				EntityXWorkTicketDataModel.DataColumns.EntityXWorkTicketId);

			lstBoxControl.SelectedValue = "-1";


		}


		#endregion

		#region Events


		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			BaseSearchFilterControl = SearchFilterControl;
			SearchFilterControl.GetKendoComboBoConfigString = LoadKendoComboBoxSources;
		}

		#endregion

	}
}