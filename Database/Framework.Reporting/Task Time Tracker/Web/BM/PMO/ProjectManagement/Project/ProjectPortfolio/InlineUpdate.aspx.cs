using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.TaskTimeTracker;
using Framework.Components.Core;
using Framework.Components.DataAccess;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.ProjectPortfolio
{
    public partial class InlineUpdate : PageInlineUpdate
    {
        #region Methods

        protected override DataTable GetData()
        {
            try
            {
                SuperKey = ApplicationCommon.GetSuperKey();
                SetId = ApplicationCommon.GetSetId();

                var selectedrows = new DataTable();
                var projectPortfoliodata = new ProjectPortfolioDataModel();

                selectedrows = ProjectPortfolioDataManager.GetDetails(projectPortfoliodata, SessionVariables.RequestProfile).Clone();
                if (!string.IsNullOrEmpty(SuperKey))
                {
                    var data = new SuperKeyDetailDataModel();
                    data.SuperKeyId = Convert.ToInt32(SuperKey);
                   
                    data.SystemEntityTypeId = (int)SystemEntity.ProjectPortfolio;
					var dt = SuperKeyDetailDataManager.Search(data, SessionVariables.RequestProfile);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        var keys = new int[dt.Rows.Count];
                        for (var i = 0; i < dt.Rows.Count; i++)
                        {

                            keys[i] = Convert.ToInt32(dt.Rows[i][SuperKeyDetailDataModel.DataColumns.EntityKey]);
                            projectPortfoliodata.ProjectPortfolioId = keys[i];
                            var result = ProjectPortfolioDataManager.GetDetails(projectPortfoliodata, SessionVariables.RequestProfile);
                            selectedrows.ImportRow(result.Rows[0]);


                        }
                    }
                }
                else if (SetId != 0)
                {
                    var key = SetId;
                    projectPortfoliodata.ProjectPortfolioId = key;
                    var result = ProjectPortfolioDataManager.GetDetails(projectPortfoliodata, SessionVariables.RequestProfile);
                    selectedrows.ImportRow(result.Rows[0]);

                }
                return selectedrows;
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            return null;
        }

        protected override void Update(Dictionary<string, string> values)
        {
            var data = new ProjectPortfolioDataModel();

            // copies properties from values dictionary object to data object
            PropertyMapper.CopyProperties(data, values);

            ProjectPortfolioDataManager.Update(data, SessionVariables.RequestProfile);
            InlineEditingList.Data = GetData();
        }

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
            PrimaryEntity = SystemEntity.ProjectPortfolio;
            PrimaryEntityKey = "ProjectPortfolio";

            InlineEditingListCore = InlineEditingList;
            BreadCrumbObject = Master.BreadCrumbObject;

            base.OnInit(e);
        }

        #endregion

    }
}