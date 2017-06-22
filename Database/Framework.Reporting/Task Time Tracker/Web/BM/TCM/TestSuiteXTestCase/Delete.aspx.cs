using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TestCaseManagement;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.TCM.TestSuiteXTestCase
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TestSuiteXTestCase;
			PrimaryEntityKey = "TestSuiteXTestCase";
			BreadCrumbObject = Master.BreadCrumbObject;

			DetailsControlPath = ApplicationCommon.GetControlPath("TestSuiteXTestCase", ControlType.DetailsControl);
			PrimaryPlaceHolder = plcDetailsList;
		}

		protected void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				string[] deleteIndexList = DeleteIds.Split(',');
				foreach (string index in deleteIndexList)
				{
					var data = new TestSuiteXTestCaseDataModel();
					data.TestSuiteXTestCaseId = int.Parse(index);
					TestCaseManagement.Components.DataAccess.TestSuiteXTestCaseDataManager.Delete(data, SessionVariables.RequestProfile);
				}

				DeleteAndRedirect();
			}
			catch (Exception ex)
			{
				Response.Write(ex.Message);
			}
		}

		private void DeleteAndRedirect()
		{
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.TestSuiteXTestCase, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("TestSuiteXTestCaseEntityRoute", new { Action = "Default", SetId = true }), false);
		}

		#endregion

	
    }
}