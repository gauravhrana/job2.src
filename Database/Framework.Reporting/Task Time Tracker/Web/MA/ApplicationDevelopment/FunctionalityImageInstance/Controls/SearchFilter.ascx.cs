using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.Module.ApplicationDevelopment;
using Framework.UI.Web.BaseClasses;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityImageInstance.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
	{

		public FunctionalityImageInstanceDataModel SearchParameters
		{
			get
			{
				var data = new FunctionalityImageInstanceDataModel();

				data.FunctionalityImageId = SearchFilterControl.GetParameterValueAsInt(FunctionalityImageInstanceDataModel.DataColumns.FunctionalityImageId);

				//data.FunctionalityImageAttributeId = SearchFilterControl.GetParameterValueAsInt(FunctionalityImageInstanceDataModel.DataColumns.FunctionalityImageAttributeId);

                SearchFilterControl.SetSearchParameters(data);

				return data;
			}
		}
		//	get
		//	{
		//		var data = new FunctionalityImageInstanceDataModel();

		//		if (SearchParametersRepeater.Items.Count != 0 && PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
		//		   FunctionalityImageInstanceDataModel.DataColumns.FunctionalityImageAttribute + "Visibility", SettingCategory)
		//		   && !CheckAndGetFieldValue(
		//			   FunctionalityImageInstanceDataModel.DataColumns.FunctionalityImageAttribute).ToString().Equals("-1")
		//			&& !string.IsNullOrEmpty( CheckAndGetFieldValue(
		//			   FunctionalityImageInstanceDataModel.DataColumns.FunctionalityImageAttribute).ToString()))
		//		{
		//			data.FunctionalityImageAttributeId = Convert.ToInt32(CheckAndGetFieldValue(
		//			   FunctionalityImageInstanceDataModel.DataColumns.FunctionalityImageAttribute).ToString ());
		//		}

		//		if (SearchParametersRepeater.Items.Count != 0 && PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
		//		   FunctionalityImageInstanceDataModel.DataColumns.FunctionalityImage + "Visibility", SettingCategory)
		//		   && !CheckAndGetFieldValue(
		//			   FunctionalityImageInstanceDataModel.DataColumns.FunctionalityImage).ToString().Equals("-1")
		//			&& !string.IsNullOrEmpty(CheckAndGetFieldValue(
		//			   FunctionalityImageInstanceDataModel.DataColumns.FunctionalityImage).ToString()))
		//		{
		//			data.FunctionalityImageId = Convert.ToInt32(CheckAndGetFieldValue(
		//			   FunctionalityImageInstanceDataModel.DataColumns.FunctionalityImage));
		//		}

		//		return data;
		//	}
		//}

		//#endregion

		#region Events

		protected void Page_Load(object sender, EventArgs e)
		{
			//base.OnLoad(e);
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			//PrimaryEntity = SystemEntity.FunctionalityImageInstance;
			//PrimaryEntityKey = "FunctionalityImageInstance";
			//FolderLocationFromRoot = "Shared/QualityAssurarnce/FunctionalityImageInstance";

			BaseSearchFilterControl = SearchFilterControl;
			SearchFilterControl.GetKendoComboBoConfigString = LoadKendoComboBoxSources;
		}

		#endregion
	}
}
