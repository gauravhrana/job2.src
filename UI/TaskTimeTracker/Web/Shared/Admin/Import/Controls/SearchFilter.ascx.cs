using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using DataModel.Framework.Import;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.BatchFile.Controls
{
    public partial class SearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilter
    {

        #region variables

        public BatchFileDataModel SearchParameters
        {
            get
            {
                var data = new BatchFileDataModel();

				data.Name = CheckAndGetFieldValue(BatchFileDataModel.DataColumns.Name).ToString();

				data.FileTypeId = GetParameterValueAsInt(BatchFileDataModel.DataColumns.FileTypeId);

				data.BatchFileSetId = GetParameterValueAsInt(BatchFileDataModel.DataColumns.BatchFileSetId);

				data.BatchFileStatusId = GetParameterValueAsInt(BatchFileDataModel.DataColumns.BatchFileStatusId);

				data.SystemEntityTypeId = GetParameterValueAsInt(BatchFileDataModel.DataColumns.SystemEntityTypeId);

				data.CreatedByPersonId = GetParameterValueAsInt(BatchFileDataModel.DataColumns.CreatedByPersonId);
				
                return data;
            }
        }

        #endregion

        #region private methods

		public override void LoadDropDownListSources(string fieldName, DropDownList dropDownListControl)
		{
			base.LoadDropDownListSources(fieldName, dropDownListControl);

			if (fieldName.Equals("FileType"))
			{
				var fileTypeData = Framework.Components.Import.FileTypeDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(fileTypeData, dropDownListControl, StandardDataModel.StandardDataColumns.Name, FileTypeDataModel.DataColumns.FileTypeId);
			}

			if (fieldName.Equals("BatchFileSet"))
			{
				var batchFileSetData = Framework.Components.Import.BatchFileSetDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(batchFileSetData, dropDownListControl, StandardDataModel.StandardDataColumns.Name, BatchFileSetDataModel.DataColumns.BatchFileSetId);
			}

			if (fieldName.Equals("BatchFileStatus"))
			{
				var batchFileStatusData = Framework.Components.Import.BatchFileStatusDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(batchFileStatusData, dropDownListControl, StandardDataModel.StandardDataColumns.Name, BatchFileStatusDataModel.DataColumns.BatchFileStatusId);
			}

			if (fieldName.Equals("SystemEntityType"))
			{
				var systemEntityTypeData = Framework.Components.Core.SystemEntityTypeDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(systemEntityTypeData, dropDownListControl, SystemEntityTypeDataModel.DataColumns.EntityName, SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId);
			}

			if (fieldName.Equals("Person"))
			{
				var personData = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(personData, dropDownListControl, ApplicationUserDataModel.DataColumns.FullName, ApplicationUserDataModel.DataColumns.ApplicationUserId);
			}
		}		

        #endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.BatchFile;
			PrimaryEntityKey = "BatchFile";
			FolderLocationFromRoot = "Shared/Admin/Import";

			SearchActionBarCore = oSearchActionBar;
			SearchParametersRepeaterCore = SearchParametersRepeater;
		}

		#endregion

    }
}