using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Admin.Country.Controls
{
    public partial class SearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilter
    {
        #region variables                

        public CountryDataModel SearchParameters
        {
            get
            {
                var data = new CountryDataModel();

				if (SearchParametersRepeater.Items.Count != 0 && PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
				   StandardDataModel.StandardDataColumns.Name + "Visibility", SettingCategory)
				   && CheckAndGetFieldValue(StandardDataModel.StandardDataColumns.Name) != "")
				{
					data.Name = CheckAndGetFieldValue(StandardDataModel.StandardDataColumns.Name).ToString();
				}


				if (SearchParametersRepeater.Items.Count != 0 && PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
				   CountryDataModel.DataColumns.TimeZoneId + "Visibility", SettingCategory))
				{
					if (CheckAndGetFieldValue(CountryDataModel.DataColumns.TimeZoneId) != "-1")
					{
						data.TimeZoneId = Convert.ToInt32(
							CheckAndGetFieldValue(CountryDataModel.DataColumns.TimeZoneId));
					}
				}			

                return data;

            }
        }

		
        #endregion

		#region Methods

		public override void LoadDropDownListSources(string fieldName, DropDownList dropDownListControl)
		{
			base.LoadDropDownListSources(fieldName, dropDownListControl);

			if (fieldName.Equals("TimeZoneId"))
			{
				var TimeZoneData = Framework.Components.Core.CountryDataManager.GetList(SessionVariables.RequestProfile);
								UIHelper.LoadDropDown(TimeZoneData, dropDownListControl, StandardDataModel.StandardDataColumns.Name,
								          CountryDataModel.DataColumns.TimeZoneId);
			}			
		}

	   #endregion     

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Country;
			PrimaryEntityKey = "Country";
			FolderLocationFromRoot = "Shared/Admin/Country";

			SearchActionBarCore = oSearchActionBar;
			SearchParametersRepeaterCore = SearchParametersRepeater;
		}

		#endregion

    }
}







