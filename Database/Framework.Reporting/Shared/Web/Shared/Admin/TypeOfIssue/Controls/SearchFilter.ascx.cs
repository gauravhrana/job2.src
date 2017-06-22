using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.Audit;

namespace Shared.UI.Web.Admin.TypeOfIssue.Controls
{
	public partial class SearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilter
	{

		#region variables

		

		public TypeOfIssueDataModel SearchParameters
		{
			get
			{
				var data = new TypeOfIssueDataModel();

				if (SearchParametersRepeater.Items.Count != 0 && PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
				   StandardDataModel.StandardDataColumns.Name + "Visibility", SettingCategory)
				   && CheckAndGetFieldValue(StandardDataModel.StandardDataColumns.Name) != "")
				{
					data.Name = CheckAndGetFieldValue(
					   StandardDataModel.StandardDataColumns.Name).ToString();
				}

				if (SearchParametersRepeater.Items.Count != 0 && PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
				   TypeOfIssueDataModel.DataColumns.Category + "Visibility", SettingCategory)
				   && CheckAndGetFieldValue(
					   TypeOfIssueDataModel.DataColumns.Category) != "")
				{
					data.Category = CheckAndGetFieldValue(
					   TypeOfIssueDataModel.DataColumns.Category).ToString();
				}
				               
				return data;
			}
		}

		

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TypeOfIssue;
			PrimaryEntityKey = "TypeOfIssue";
			FolderLocationFromRoot = "Shared/Admin/TypeOfIssue";

			SearchActionBarCore = oSearchActionBar;
			SearchParametersRepeaterCore = SearchParametersRepeater;
		}

		#endregion

	}
}