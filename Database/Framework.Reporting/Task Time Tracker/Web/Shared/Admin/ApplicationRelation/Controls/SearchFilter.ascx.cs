using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using System.Data;
using Shared.UI.Web.Controls;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.Admin.ApplicationRelation.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
    {

        #region variables   

        public ApplicationRelationDataModel SearchParameters
        {
            get
            {
				var data = new ApplicationRelationDataModel(); 
				
				SearchFilterControl.SetSearchParameters(data);

				CommonSearchParameters();

				return data;
            }
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			BaseSearchFilterControl                         = SearchFilterControl;
			SearchFilterControl.GetKendoComboBoConfigString = LoadKendoComboBoxSources;
		}

		#endregion

    }
}