using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.LogAndTrace;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.DatabaseChangeLog.Controls
{

	public partial class SearchFilter : ControlSearchFilterEntity
    {

        #region variables

		public ApplicationContainer.UI.Web.BaseUI.SearchFilterControl SearchControl
		{
			get
			{
				return SearchFilterControl;
			}
		}

		public string GroupBy
		{
			get
			{
				return SearchFilterControl.GroupBy;
			}
		}

		public string SubGroupBy
		{
			get
			{
				return SearchFilterControl.SubGroupBy;
			}
		}

        public DatabaseChangeLogDataModel SearchParameters
        {
            get
            {
                var data = new DatabaseChangeLogDataModel();

				SearchFilterControl.SetSearchParameters(data);

				var recordDate = SearchFilterControl.GetParameterValueForDatePanel(DatabaseChangeLogDataModel.DataColumns.RecordDate);
				data.FromSearchDate = recordDate.Count > 0 ? recordDate[0] : null;
				data.ToSearchDate = recordDate.Count > 1 ? recordDate[1] : null;

				
                return data;

            }
        }

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