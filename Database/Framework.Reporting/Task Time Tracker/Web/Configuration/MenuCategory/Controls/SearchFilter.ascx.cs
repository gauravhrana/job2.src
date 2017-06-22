using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using DataModel.TaskTimeTracker;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.Configuration.MenuCategory.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
    {
        #region variables

        public MenuCategoryDataModel SearchParameters
        {
            get
            {
                var data = new MenuCategoryDataModel();

				SearchFilterControl.SetSearchParameters(data);

				CommonSearchParameters();

                return data;
            }
        }
        #endregion

        #region private methods    

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
			base.OnInit(e);

			BaseSearchFilterControl = SearchFilterControl;
        }

        #endregion
    }
}