using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;
using Shared.UI.Web.Controls;
using Framework.Components.DataAccess;

namespace Shared.UI.Web.Configuration.SystemForeignRelationship.Controls
{
	public partial class SearchFilter : ControlSearchFilter
	{
		#region variables

		public SystemForeignRelationshipDataModel SearchParameters
		{
			get
			{
				var data = new SystemForeignRelationshipDataModel();

				data.ForeignDatabaseId = GetParameterValueAsInt(SystemForeignRelationshipDataModel.DataColumns.ForeignDatabaseId);

				data.PrimaryDatabaseId = GetParameterValueAsInt(SystemForeignRelationshipDataModel.DataColumns.PrimaryDatabaseId);								
				
				return data;
			}
		}

		#endregion

		#region private methods

		public override void LoadDropDownListSources(string fieldName, DropDownList dropDownListControl)
		{
			base.LoadDropDownListSources(fieldName, dropDownListControl);

			if (fieldName.Equals("PrimaryDatabaseId"))
			{
				var primaryDatabaseData = Framework.Components.Core.SystemForeignRelationshipDatabaseDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(primaryDatabaseData, dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
					SystemForeignRelationshipDatabaseDataModel.DataColumns.SystemForeignRelationshipDatabaseId);
			}

			if (fieldName.Equals("ForeignDatabaseId"))
			{
				var foreignDatabaseIdData = Framework.Components.Core.SystemForeignRelationshipDatabaseDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(foreignDatabaseIdData, dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
					SystemForeignRelationshipDatabaseDataModel.DataColumns.SystemForeignRelationshipDatabaseId);
			}

		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = SystemEntity.SystemForeignRelationship;
			PrimaryEntityKey = "SystemForeignRelationship";
			FolderLocationFromRoot = "Shared/Configuration/SystemForeignRelationship";

			SearchActionBarCore = oSearchActionBar;
			SearchParametersRepeaterCore = SearchParametersRepeater;
		}

		#endregion
	}
}