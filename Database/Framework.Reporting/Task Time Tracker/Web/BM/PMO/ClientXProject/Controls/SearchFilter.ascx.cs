using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using Framework.Components.DataAccess;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.ClientXProject.Controls
{
	public partial class SearchFilter : ControlSearchFilter
	{

		#region variables		

        public ClientXProjectDataModel SearchParameters
		{
			get
			{
				var data = new ClientXProjectDataModel();

				data.ProjectId = GetParameterValueAsInt(ClientXProjectDataModel.DataColumns.ProjectId);

				data.ClientId = GetParameterValueAsInt(ClientXProjectDataModel.DataColumns.ClientId);								
				
				return data;
			}
		}

		#endregion

		#region private methods

		public override string LoadKendoComboBoxSources(string fieldName, TextBox txtBox, PlaceHolder plcControlHolder)
		{
			if (fieldName.Equals("ProjectId"))
			{
				return AjaxHelper.GetKendoComboBoxConfigScript("GetProjectList", "Name", "ProjectId", plcControlHolder);
			}
			if (fieldName.Equals("ClientId"))
			{
				return AjaxHelper.GetKendoComboBoxConfigScript("GetClientList", "Name", "ClientId", plcControlHolder);
			}
			
			return string.Empty;

		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey				= "ClientXProject";
			FolderLocationFromRoot			= "ClientXProject";
			PrimaryEntity					= SystemEntity.ClientXProject;

			SearchActionBarCore				= oSearchActionBar;
			SearchParametersRepeaterCore	= SearchParametersRepeater;
		}
	
		#endregion

	}
}