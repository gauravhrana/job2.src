using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Text;
using ApplicationContainer.UI.Web.CommonCode;
using TaskTimeTracker.Components.Module.ApplicationDevelopment;
using Framework.Components.DataAccess;
using System.Collections.Specialized;
using System.Collections;
using DataModel.TaskTimeTracker.ApplicationDevelopment;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.Functionality
{
    public partial class Default : Framework.UI.Web.BaseClasses.PageDefault
    {

        #region private methods
		
        protected override DataTable GetData()
        {			
            var dt1 = FunctionalityDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);

			dt1.Columns.Add("FunctionalityImageId");			

			string str = string.Empty;			

			foreach (DataRow dr in dt1.Rows)
			{
				var functionalityId = Convert.ToInt32(dr["FunctionalityId"]);
				var functionalityImageId = string.Empty;

				var objFunctionalityXFunctionalityImage = new FunctionalityXFunctionalityImageDataModel();
			
				objFunctionalityXFunctionalityImage.FunctionalityId = Convert.ToInt32(functionalityId);
				var dtFunctionalityXFunctionalityImage = FunctionalityXFunctionalityImageDataManager.GetByFunctionality(functionalityId, SessionVariables.RequestProfile);

				if (dtFunctionalityXFunctionalityImage != null && dtFunctionalityXFunctionalityImage.Rows.Count > 0)
				{
					foreach (DataRow drFunctionalityXFunctionalityImage in dtFunctionalityXFunctionalityImage.Rows)
					{
						if (string.IsNullOrEmpty(functionalityImageId))
						{
							functionalityImageId =  drFunctionalityXFunctionalityImage["FunctionalityImageId"].ToString();
						}
						else
						{
							functionalityImageId += ", " + drFunctionalityXFunctionalityImage["FunctionalityImageId"].ToString();
						}						
					}
				}
				dr["FunctionalityImageId"] = functionalityImageId;				
			}
			return dt1;			
        }	

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity			= SystemEntity.Functionality;
            PrimaryEntityKey		= "Functionality";
            PrimaryEntityIdColumn	= "FunctionalityId";

            MasterPageCore = Master;
            SubMenuCore = Master.SubMenuObject;
            BreadCrumbObject = Master.BreadCrumbObject;

			SearchFilterCore = oSearchFilter.GetFilter(PrimaryEntity, PrimaryEntityKey);
			GroupListCore = oGroupList;
            

            IsDynamicSearchControl = true;

            VisibilityManagerCore = oVC;
        }

        #endregion

    }
}