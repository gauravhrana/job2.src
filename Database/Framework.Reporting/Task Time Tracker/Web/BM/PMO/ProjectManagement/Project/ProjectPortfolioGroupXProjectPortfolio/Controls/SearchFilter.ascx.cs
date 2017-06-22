using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using System.Data;
using DataModel.Framework.DataAccess;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.ProjectManagement.Project.ProjectPortfolioGroupXProjectPortfolio.Controls
{
	public partial class SearchFilter : ControlSearchFilter
	{

		#region variables

		public ProjectPortfolioGroupXProjectPortfolioDataModel SearchParameters
		{
			get
			{
				var data = new ProjectPortfolioGroupXProjectPortfolioDataModel();

				data.ProjectPortfolioGroupId = GetParameterValueAsInt(ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.ProjectPortfolioGroupId);

				data.ProjectPortfolioId = GetParameterValueAsInt(ProjectPortfolioGroupXProjectPortfolioDataModel.DataColumns.ProjectPortfolioId);

				return data;
			}
		}

		#endregion

		#region Methods

		public void SetGroupBy()
		{
			GroupBy = CheckAndGetFieldValue("GroupBy", false).ToString();
		}

		public override void LoadDropDownListSources(string fieldName, DropDownList dropDownListControl)
		{
			base.LoadDropDownListSources(fieldName, dropDownListControl);

			if (fieldName.Equals("ProjectPortfolioGroupId"))
            {
                var ppgData = ProjectPortfolioGroupDataManager.GetList(SessionVariables.RequestProfile);
                UIHelper.LoadDropDown(ppgData, dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
					ProjectPortfolioGroupDataModel.DataColumns.ProjectPortfolioGroupId);
            }
			else if (fieldName.Equals("ProjectPortfolioId"))
            {
                var ppData = ProjectPortfolioDataManager.GetList(SessionVariables.RequestProfile);
                UIHelper.LoadDropDown(ppData, dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
					ProjectPortfolioDataModel.DataColumns.ProjectPortfolioId);
            }
		}		

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = SystemEntity.ProjectPortfolioGroupXProjectPortfolio;
			PrimaryEntityKey = "ProjectPortfolioGroupXProjectPortfolio";
			FolderLocationFromRoot = "ProjectPortfolioGroupXProjectPortfolio";

			SearchActionBarCore = oSearchActionBar;
			SearchParametersRepeaterCore = SearchParametersRepeater;
		}

		#endregion

	}
}