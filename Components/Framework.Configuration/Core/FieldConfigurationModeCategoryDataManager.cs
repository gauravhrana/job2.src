﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using DataModel.Framework.Configuration;
using DataModel.Framework.DataAccess;
using Framework.Components.DataAccess;
using System.Runtime.CompilerServices;
using Framework.CommonServices.BusinessDomain.Utils;

namespace Framework.Components.UserPreference
{
	public partial class FieldConfigurationModeCategoryDataManager : StandardDataManager
	{
		#region Sync

		public static void Sync(int newApplicationId, int oldApplicationId, RequestProfile requestProfile)
		{
			// get all records for old Application Id
			var sql = "EXEC dbo.FieldConfigurationModeCategorySearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, oldApplicationId);

			var oDT = new DBDataTable("FieldConfigurationModeCategory.Search", sql, DataStoreKey);

			// formaulate a new request Profile which will have new Applicationid
			var newRequestProfile = new RequestProfile();
			newRequestProfile.ApplicationId = newApplicationId;
			newRequestProfile.AuditId = requestProfile.AuditId;

			foreach (DataRow dr in oDT.DBTable.Rows)
			{
				var data = new FieldConfigurationModeCategoryDataModel();
				data.ApplicationId = newApplicationId;
				data.Name = dr[StandardDataModel.StandardDataColumns.Name].ToString();

				// check for existing record in new Application Id
				if(!DoesExist(data, newRequestProfile))
				{
					data.Description = dr[StandardDataModel.StandardDataColumns.Description].ToString();
					data.SortOrder = Convert.ToInt32(dr[StandardDataModel.StandardDataColumns.SortOrder]);

					//create in new application id
					Create(data, newRequestProfile);

				}

			}
		}

		#endregion
	}
}