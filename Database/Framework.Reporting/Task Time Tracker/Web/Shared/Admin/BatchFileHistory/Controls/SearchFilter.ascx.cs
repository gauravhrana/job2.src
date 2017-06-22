using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Import;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.BatchFileHistory.Controls
{
    public partial class SearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilter
    {

        #region variables

        public BatchFileHistoryDataModel SearchParameters
        {
            get
            {
                var data = new BatchFileHistoryDataModel();

				data.BatchFileSetId = GetParameterValueAsInt(BatchFileHistoryDataModel.DataColumns.BatchFileSetId);

				data.BatchFileStatusId = GetParameterValueAsInt(BatchFileHistoryDataModel.DataColumns.BatchFileStatusId);

				data.BatchFileId = GetParameterValueAsInt(BatchFileHistoryDataModel.DataColumns.BatchFileId);
				
                return data;
            }
        }

        #endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity			= Framework.Components.DataAccess.SystemEntity.BatchFileHistory;
			PrimaryEntityKey		= "BatchFileHistory";
			FolderLocationFromRoot	= "Shared/Admin/BatchFileHistory";

			SearchActionBarCore = oSearchActionBar;
			SearchParametersRepeaterCore = SearchParametersRepeater;
		}
		
		#endregion

	}
}