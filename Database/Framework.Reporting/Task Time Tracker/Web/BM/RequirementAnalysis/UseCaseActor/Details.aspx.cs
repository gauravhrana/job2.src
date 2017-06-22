using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Framework.Components.DataAccess;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using System.Data;

namespace ApplicationContainer.UI.Web.UseCaseActor
{
    public partial class Details : Framework.UI.Web.BaseClasses.PageDetails
    {  
        #region Methods        

        protected override Control GetTabControl(int setId, Control detailsControl)
        {
            
            var tabControl = ApplicationCommon.GetNewDetailTabControl();

            var listControl = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);
			listControl.Setup("UseCaseActorXUseCase", String.Empty, "UseCaseActorXUseCaseId", setId, true, GetData,
                GetUseCaseActorXUseCaseColumns,  "UseCaseActorXUseCase");
            listControl.SetSession("true");

            tabControl.Setup("UseCaseActorDetailsView");
            tabControl.AddTab("UseCaseActor", detailsControl, String.Empty, true);
            tabControl.AddTab("UseCaseActorXUseCase", listControl);
           
            return tabControl;
        }
		
		private DataTable GetData(string key)
		{
			return GetUseCaseActorXUseCaseData(int.Parse(key));
		}

        private DataTable GetUseCaseActorXUseCaseData(int useCaseActorId)
        {
            var data = new UseCaseActorXUseCaseDataModel();
            data.UseCaseActorId = useCaseActorId;
            var dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseActorXUseCaseDataManager.Search(data, SessionVariables.RequestProfile);
            return dt;            
        }

        private string[] GetUseCaseActorXUseCaseColumns()
        {
            return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.UseCaseActorXUseCase, "DBColumns", SessionVariables.RequestProfile);
        }
       

        #endregion


        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UseCaseActor;
            PrimaryEntityKey = "UseCaseActor";
            DetailsControlPath = ApplicationCommon.GetControlPath("UseCaseActor", ControlType.DetailsControl);
			PrimaryPlaceHolder = oDetailsControl.PlaceHolderDetails;
			BreadCrumbObject = Master.BreadCrumbObject;
        }        

        #endregion

    }
}