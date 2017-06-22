using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Text;
using Shared.UI.Web.Controls;
using Framework.Components.Import;
using DataModel.Framework.DataAccess;

namespace Shared.UI.Web.FileType
{
    public partial class Default : PageDefault
    {        
        protected override DataTable GetData()
        {
            var dt = FileTypeDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile, SessionVariables.ApplicationMode);
            return dt;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity			 = Framework.Components.DataAccess.SystemEntity.FileType;
            PrimaryEntityKey		 = "FileType";
            PrimaryEntityIdColumn	 = "FileTypeId";

            MasterPageCore			 = Master;
            SubMenuCore				 = Master.SubMenuObject;
            BreadCrumbObject		 = Master.BreadCrumbObject;

			SearchFilterCore		 = oSearchFilter.GetFilter(PrimaryEntity, PrimaryEntityKey); 
            GroupListCore			 = oGroupList;

            IsDynamicSearchControl	 = true;

            VisibilityManagerCore	 = oVC;
        }     

    }
}