using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace ApplicationContainer.UI.Web.UseCase
{
    public partial class Details : Framework.UI.Web.BaseClasses.PageDetails
    {
        #region Methods        

        protected override Control GetTabControl(int setId, Control detailsControl)
        {
            
            var tabControl = ApplicationCommon.GetNewDetailTabControl();

            var listControl = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);
			listControl.Setup("UseCaseActorXUseCase", String.Empty, "UseCaseActorXUseCaseId", setId, true, GetData, GetUseCaseActorXUseCaseColumns, "UseCaseActorXUseCase");
            listControl.SetSession("true");

            var listControlUC = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);
            listControlUC.Setup("UseCaseXUseCaseStep", String.Empty, "UseCaseXUseCaseStepId", setId, true, GetUseCaseXUseCaseStepData, GetUseCaseXUseCaseStepColumns, "UseCaseXUseCaseStep");
            listControlUC.SetSession("true");

            tabControl.Setup("UseCaseDetailsView");
            tabControl.AddTab("UseCase", detailsControl, String.Empty, true); 
            tabControl.AddTab("Actors", listControl);
            tabControl.AddTab("Steps", listControlUC); 
            
            return tabControl;
        }

		private DataTable GetData(string key)
		{
			return GetUseCaseActorXUseCaseData(int.Parse(key));
		}

		private DataTable GetUseCaseXUseCaseStepData(string key)
		{
			return GetUseCaseXUseCaseStepData(int.Parse(key));
		}

        private DataTable GetUseCaseActorXUseCaseData(int useCaseId)
        {
            var data = new UseCaseActorXUseCaseDataModel();
            data.UseCaseId = useCaseId;
            var dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseActorXUseCaseDataManager.Search(data, SessionVariables.RequestProfile);
            return dt;            
        }

        private DataTable GetUseCaseXUseCaseStepData(int useCaseId)
        {
            var data = new UseCaseXUseCaseStepDataModel();
            data.UseCaseId = useCaseId;
            var dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseXUseCaseStepDataManager.Search(data, SessionVariables.RequestProfile);
            return dt;
        }           

        private string[] GetUseCaseActorXUseCaseColumns()
        {
            return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.UseCaseActorXUseCase, "DBColumns", SessionVariables.RequestProfile);
        }

        private string[] GetUseCaseXUseCaseStepColumns()
        {
            return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.UseCaseXUseCaseStep, "DBColumns", SessionVariables.RequestProfile);
        }


        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity		= Framework.Components.DataAccess.SystemEntity.UseCase;
            PrimaryEntityKey	= "UseCase";
            DetailsControlPath	= ApplicationCommon.GetControlPath("UseCase", ControlType.DetailsControl);
			PrimaryPlaceHolder	= oDetailsControl.PlaceHolderDetails;
			BreadCrumbObject	= Master.BreadCrumbObject;
        }
      
        #endregion

    }
}