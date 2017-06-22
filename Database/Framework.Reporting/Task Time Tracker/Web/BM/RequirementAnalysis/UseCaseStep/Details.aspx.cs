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

namespace ApplicationContainer.UI.Web.UseCaseStep
{
    public partial class Details : Framework.UI.Web.BaseClasses.PageDetails
    {

        #region Methods       

        protected override Control GetTabControl(int setId, Control detailsControl)
        {

            var tabControl = ApplicationCommon.GetNewDetailTabControl();

            var listControlUC = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);
            listControlUC.Setup("UseCaseXUseCaseStep", String.Empty, "UseCaseXUseCaseStepId", setId, true,
				GetData, GetUseCaseXUseCaseStepColumns, "UseCaseXUseCaseStep");
            listControlUC.SetSession("true");

            tabControl.Setup("UseCaseStepDetailsView");
            tabControl.AddTab("UseCaseStep", detailsControl, String.Empty, true);            
            tabControl.AddTab("UseCaseXUseCaseStep", listControlUC);

            return tabControl;
        }

		private DataTable GetData(string key)
		{
			return GetUseCaseXUseCaseStepData(int.Parse(key));
		}

        private DataTable GetUseCaseXUseCaseStepData(int useCaseStepId)
        {
            var data = new UseCaseXUseCaseStepDataModel();
            data.UseCaseStepId = useCaseStepId;
            var dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseXUseCaseStepDataManager.Search(data, SessionVariables.RequestProfile);
            return dt;
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

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UseCaseStep;
            PrimaryEntityKey = "UseCaseStep";
            DetailsControlPath = ApplicationCommon.GetControlPath("UseCaseStep", ControlType.DetailsControl);
            PrimaryPlaceHolder = plcDetailsList;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion

    }
}