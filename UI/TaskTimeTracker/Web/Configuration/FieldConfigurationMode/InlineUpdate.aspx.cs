﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using Framework.Components.UserPreference;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using Framework.UI.Web.BaseClasses;
using Dapper;

namespace Shared.UI.Web.Configuration.FieldConfigurationMode
{
    public partial class InlineUpdate : PageInlineUpdate
	{
		#region Methods

		protected override DataTable GetData()
        {
            try
            {
				SuperKey = ApplicationCommon.GetSuperKey();
				SetId = ApplicationCommon.GetSetId();

				var selectedrows = new List<FieldConfigurationModeDataModel>();
				var fieldConfigurationModedata = new FieldConfigurationModeDataModel();

                if (!string.IsNullOrEmpty(SuperKey))
                {
					var systemEntityTypeId = (int)PrimaryEntity;
					var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

					foreach (var entityKey in lstEntityKeys)
					{
						fieldConfigurationModedata.FieldConfigurationModeId = entityKey;
						var result = FieldConfigurationModeDataManager.GetDetails(fieldConfigurationModedata, SessionVariables.RequestProfile);
                        selectedrows.Add(result);
					}
                }
                else 
                {
					fieldConfigurationModedata.FieldConfigurationModeId = SetId;
					var result = FieldConfigurationModeDataManager.GetDetails(fieldConfigurationModedata, SessionVariables.RequestProfile);
                    selectedrows.Add(result);
                }
                return selectedrows.ToDataTable();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            return null;
        }

		protected override void Update(Dictionary<string, string> values)
		{
			var data = new FieldConfigurationModeDataModel();

            // copies properties from values dictionary object to data object
            PropertyMapper.CopyProperties(data, values);

			base.Update(values);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FieldConfigurationMode;
            PrimaryEntityKey = "FieldConfigurationMode";

            InlineEditingListCore = InlineEditingList;
            BreadCrumbObject = Master.BreadCrumbObject;

            base.OnInit(e);
        }

        #endregion

    }
}