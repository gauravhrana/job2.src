using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.MilestoneXFeatureArchive.Controls
{
	public partial class SearchFilter : ControlSearchFilter
    {
        #region variables

        public MilestoneXFeatureArchiveDataModel SearchParameters
        {
			get
			{
				var data = new MilestoneXFeatureArchiveDataModel();

				data.Milestone = GetParameterValue(MilestoneXFeatureArchiveDataModel.DataColumns.Milestone);

				data.Feature = GetParameterValue(MilestoneXFeatureArchiveDataModel.DataColumns.Feature);

				data.MilestoneFeatureState = GetParameterValue(MilestoneXFeatureArchiveDataModel.DataColumns.MilestoneFeatureState);			   

				return data;
			}		
			
        }		

        #endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = SystemEntity.MilestoneXFeatureArchive;
			PrimaryEntityKey = "MilestoneXFeatureArchive";
			FolderLocationFromRoot = "MilestoneXFeatureArchive";

			SearchActionBarCore = oSearchActionBar;
			SearchParametersRepeaterCore = SearchParametersRepeater;
		}

		#endregion
    }
}