using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Framework.Components.DataAccess;
using Framework.Components.UserPreference;
using DataModel.Framework.Configuration;
using Dapper;

namespace Shared.WebCommon.UI.Web
{

	public class FieldConfigurationUtility
	{

		public static void SetFieldConfigurations()
		{
			var dtConfigurations = GetResultsFromDB(null, null, string.Empty);
			SessionVariables.FieldConfigurations = dtConfigurations;
		}

		private static DataTable GetResultsFromDB(int? systemEntityTypeId, int? fieldConfigurationModeId, string name)
		{
			var obj                      = new FieldConfigurationDataModel();
			obj.SystemEntityTypeId       = systemEntityTypeId;
			obj.FieldConfigurationModeId = fieldConfigurationModeId;
			obj.Name                     = name;
            obj.ApplicationId            = SessionVariables.RequestProfile.ApplicationId;

			var dt = FieldConfigurationDataManager.Search(obj, SessionVariables.SystemRequestProfile);
			return dt;
		}

		public static DataTable GetFieldConfigurations(object systemEntityTypeId, int? fieldConfigurationModeId, string name, int? DisplayColumn = 1)
		{
			return GetFieldConfigurations((int) systemEntityTypeId, fieldConfigurationModeId, name, DisplayColumn);
		}

		public static DataTable GetFieldConfigurations(int? systemEntityTypeId, int? fieldConfigurationModeId, string name, int? DisplayColumn = 1)
		{
			var dtConfigurations = SessionVariables.FieldConfigurations;

			if (dtConfigurations == null)
			{
				SetFieldConfigurations();
				dtConfigurations = SessionVariables.FieldConfigurations;
			}

			var filterExpression = "1 = 1";

			if (systemEntityTypeId != null)
			{
				filterExpression += " and SystemEntityTypeId = " + systemEntityTypeId.Value;
			}

			if (fieldConfigurationModeId != null)
			{
				filterExpression += " and FieldConfigurationModeId = " + fieldConfigurationModeId.Value;
			}

			if (!string.IsNullOrEmpty(name))
			{
				filterExpression += " and name = '" + name + "'";
			}

			//if (DisplayColumn != null)
			//{
			//	filterExpression += " and DisplayColumn = 1";
			//}

			var dv = dtConfigurations.DefaultView;
			dv.RowFilter = filterExpression;
			dv.Sort = FieldConfigurationDataModel.DataColumns.GridViewPriority + " ASC";

			var dtResults = dv.ToTable();

			return dtResults;
		}

		public static DataTable GetFieldConfigurationsByName(int? systemEntityTypeId, string fieldConfigurationModeName, string name, int? DisplayColumn = 1)
		{
			var dtConfigurations = SessionVariables.FieldConfigurations;

			if (dtConfigurations == null)
			{
				SetFieldConfigurations();
				dtConfigurations = SessionVariables.FieldConfigurations;
			}

			var filterExpression = "1 = 1";

			if (systemEntityTypeId != null)
			{
				filterExpression += " and SystemEntityTypeId = " + systemEntityTypeId.Value;
			}

			if (!string.IsNullOrEmpty(fieldConfigurationModeName))
			{
				filterExpression += " and FieldConfigurationMode = '" + fieldConfigurationModeName + "'";
			}

			if (!string.IsNullOrEmpty(name))
			{
				filterExpression += " and name = '" + name + "'";
			}

			//if (DisplayColumn != null)
			//{
			//	filterExpression += " and DisplayColumn = 1";
			//}

			var dv = dtConfigurations.DefaultView;
			dv.RowFilter = filterExpression;
			dv.Sort = FieldConfigurationDataModel.DataColumns.GridViewPriority + " ASC";

			var dtResults = dv.ToTable();

			return dtResults;
		}

		public static void SetUserFieldConfigurationModes()
		{
			var dtFieldConfigurationModeXApplicationUser = FieldConfigurationModeXApplicationUserDataManager.GetByApplicationUser(SessionVariables.RequestProfile.AuditId, SessionVariables.SystemRequestProfile);
			SessionVariables.UserFieldConfigurationModes = dtFieldConfigurationModeXApplicationUser;
		}

		public static DataTable GetFCModesByApplicationRoles(string strfieldConfigurtionModeAccessMode)
		{
			var fcModesByApplicationRoles = new DataTable();
			var fieldConfigurationModeIdColumn = FieldConfigurationModeDataModel.DataColumns.FieldConfigurationModeId;

			foreach (var applicationRole in SessionVariables.ApplicationUserRoles)
			{
				var data = new FieldConfigurationModeXApplicationRoleDataModel();
				data.ApplicationRoleId = applicationRole.ApplicationRoleId;
				data.FieldConfigurationModeAccessMode = strfieldConfigurtionModeAccessMode;

				var dt = FieldConfigurationModeXApplicationRoleDataManager.SearchByFCModeAccessMode(data, SessionVariables.SystemRequestProfile);

				if (fcModesByApplicationRoles.Rows.Count == 0)
				{
					fcModesByApplicationRoles = dt;
				}
				else
				{
					fcModesByApplicationRoles = (from a in fcModesByApplicationRoles.AsEnumerable()
												 join b in dt.AsEnumerable()
												 on (int)a[fieldConfigurationModeIdColumn] equals (int)b[fieldConfigurationModeIdColumn]
												 into g
												 where g.Any()
												 select a).CopyToDataTable();
				}

			}
			return fcModesByApplicationRoles;
		}

		public static DataTable GetFCModesByApplicationUser(string strfieldConfigurtionModeAccessMode)
		{
			var data = new FieldConfigurationModeXApplicationUserDataModel();
			data.ApplicationUserId = SessionVariables.RequestProfile.AuditId;
			data.FieldConfigurationModeAccessMode = strfieldConfigurtionModeAccessMode;

			var fcModesByApplicationUser = FieldConfigurationModeXApplicationUserDataManager.SearchByFCModeAccessMode(data, SessionVariables.SystemRequestProfile);
			return fcModesByApplicationUser;
		}

		//public static DataTable GetApplicableModesList(SystemEntity systemEntityType)
		//{

		//	var valideModes = new DataTable();

		//	// for a given entity find valid modes ?
		//	if (HttpContext.Current.Session["ValidModes_" + systemEntityType.Value()] != null)
		//	{
		//		valideModes = (DataTable)HttpContext.Current.Session["ValidModes_" + systemEntityType.Value()];
		//	}
		//	else
		//	{
		//		var validmodes = new DataTable();

		//		// static values assigned

		//		// TODO: Turn to accomodate array
		//		// get CategoryList (ID) -- wil be array not just single list
		//		var fcModeCategoryData = new FieldConfigurationModeCategoryDataModel();
		//		fcModeCategoryData.Name = "List";

		//		var fcModeCategoryDt = FieldConfigurationModeCategoryDataManager.Search(fcModeCategoryData, SessionVariables.SystemRequestProfile);

		//		var modeCategoryId = (int)fcModeCategoryDt.Rows[0][FieldConfigurationModeCategoryDataModel.DataColumns.FieldConfigurationModeCategoryId];

		//		// All FC Modes
		//		var fcModes = FieldConfigurationModeDataManager.GetList(SessionVariables.SystemRequestProfile);
				
		//		var systemEntityTypeId = systemEntityType.Value();

		//		var columns = GetFieldConfigurations(systemEntityTypeId, null, string.Empty);

		//		// FCMode By System Entity
		//		var validModesByEntity = FieldConfigurationModeCategoryDataManager.GetApplicableModesListByEntity(columns, fcModes);

		//		// FCMode by ApplicationUser
		//		var validModesByApplicationUser = SessionVariables.UserFieldConfigurationModes;

		//		// FCMode by FCModeCategory
		//		var validModesByCategory = FieldConfigurationModeCategoryXFCModeDataManager.GetByFieldConfigurationModeCategory(modeCategoryId, SessionVariables.SystemRequestProfile);

		//		// FCMode by ApplicationMode 
		//		var validModesByApplicationMode = Framework.Components.UserPreference.ApplicationModeXFieldConfigurationModeDataManager.GetByApplicationMode(SessionVariables.UserApplicationMode, SessionVariables.SystemRequestProfile);

		//		var fieldConfigurationModeIdColumn = FieldConfigurationModeDataModel.DataColumns.FieldConfigurationModeId;

		//		// virtually a copy of valid Modes By Category but with FCMode columns (easier to maintain later on)
		//		var dtMerged = (from a in fcModes.AsEnumerable()
		//						join b in validModesByCategory.AsEnumerable()
		//					   on (int)a[fieldConfigurationModeIdColumn] equals (int)b[fieldConfigurationModeIdColumn]
		//					   into g
		//						where g.Count() > 0
		//						select a).CopyToDataTable();

		//		// dtMerged = join of FCModeXFCModeCategory and FCmodeXApplicationMode (step 3)
		//		if (validModesByCategory.Rows.Count != 0 && validModesByApplicationMode.Rows.Count != 0)
		//		{
		//			// [Result 2] = merger of [Result 1] and [ApplicationMode X FCMode]
		//			dtMerged = (from a in dtMerged.AsEnumerable()
		//						join b in validModesByApplicationMode.AsEnumerable()
		//						on (int)a[fieldConfigurationModeIdColumn] equals (int)b[fieldConfigurationModeIdColumn]
		//						into g
		//						where g.Count() > 0
		//						select a).CopyToDataTable();
		//		}
		//		else
		//		{
		//			dtMerged.Rows.Clear();
		//		}

		//		// Allowed Modes By User's Application Roles (One or Multiple Roles)
		//		var fcAllowedModesByApplicationRoles = FieldConfigurationUtility.GetFCModesByApplicationRoles("Allow");

		//		if (fcAllowedModesByApplicationRoles.Rows.Count != 0)
		//		{
		//			// virtually a copy of valid Modes By ApplicationRoles but with FCMode columns
		//			fcAllowedModesByApplicationRoles = (from a in fcModes.AsEnumerable()
		//												join b in fcAllowedModesByApplicationRoles.AsEnumerable()
		//												on (int)a[fieldConfigurationModeIdColumn] equals (int)b[fieldConfigurationModeIdColumn]
		//												into g
		//												where g.Count() > 0
		//												select a).CopyToDataTable();
		//		}

		//		// merge/add result1 + Allowed Modes By User's Application Roles
		//		dtMerged.Merge(fcAllowedModesByApplicationRoles);

		//		// Denied Modes By User's Application Roles (One or Multiple Roles)
		//		var fcDeniedModesByApplicationRoles = FieldConfigurationUtility.GetFCModesByApplicationRoles("Deny");

		//		if (fcDeniedModesByApplicationRoles.Rows.Count != 0)
		//		{
		//			// Remove the modes denied by User's Application Roles
		//			dtMerged = (from a in dtMerged.AsEnumerable()
		//						join b in fcDeniedModesByApplicationRoles.AsEnumerable()
		//						on (int)a[fieldConfigurationModeIdColumn] equals (int)b[fieldConfigurationModeIdColumn]
		//						into g
		//						where g.Count() == 0
		//						select a).CopyToDataTable();
		//		}

		//		// Allowed Modes By Application User
		//		var fcAllowedModesByApplicationUser = FieldConfigurationUtility.GetFCModesByApplicationUser("Allow");

		//		if (fcAllowedModesByApplicationUser.Rows.Count != 0)
		//		{
		//			// virtually a copy of valid Modes By ApplicationUser but with FCMode columns
		//			fcAllowedModesByApplicationUser = (from a in fcModes.AsEnumerable()
		//											   join b in fcAllowedModesByApplicationUser.AsEnumerable()
		//												on (int)a[fieldConfigurationModeIdColumn] equals (int)b[fieldConfigurationModeIdColumn]
		//												into g
		//											   where g.Count() > 0
		//											   select a).CopyToDataTable();
		//		}

		//		// merge/add result + Allowed Modes By Application User
		//		dtMerged.Merge(fcAllowedModesByApplicationUser);

		//		// Denied Modes By User's Application Roles (One or Multiple Roles)
		//		var fcDeniedModesByApplicationUser = FieldConfigurationUtility.GetFCModesByApplicationUser("Deny");

		//		if (fcDeniedModesByApplicationUser.Rows.Count != 0)
		//		{
		//			// Remove the modes denied by User's Application Roles
		//			dtMerged = (from a in dtMerged.AsEnumerable()
		//						join b in fcDeniedModesByApplicationUser.AsEnumerable()
		//						on (int)a[fieldConfigurationModeIdColumn] equals (int)b[fieldConfigurationModeIdColumn]
		//						into g
		//						where g.Count() == 0
		//						select a).CopyToDataTable();
		//		}

		//		// Last Step ( join the result with all the fc modes list by entity)
		//		if (dtMerged.Rows.Count != 0 && validModesByEntity.Rows.Count != 0)
		//		{
		//			// merge [All FC Modes] and [Result 2]
		//			fcModes = (from a in fcModes.AsEnumerable()
		//					   join b in validModesByEntity.AsEnumerable()
		//					   on (int)a[fieldConfigurationModeIdColumn] equals (int)b[fieldConfigurationModeIdColumn]
		//					   into g
		//					   where g.Count() > 0
		//					   select a).CopyToDataTable();

		//		}
		//		else
		//		{
		//			fcModes.Rows.Clear();
		//		}

		//		// get all the columns of the table in an array
		//		var strColumns = (from dc in fcModes.Columns.Cast<DataColumn>()
		//						  select dc.ColumnName).ToArray();

		//		var dv = new DataView(fcModes);

		//		// Eliminates duplicate rows
		//		fcModes = dv.ToTable(true, strColumns);

		//		HttpContext.Current.Session["ValidModes_" + systemEntityType.Value()] = fcModes;

		//		valideModes = fcModes;

		//	}

		//	return valideModes;

		//}

		public static string GetFieldConfigurationColumnDisplayName(string name)
		{
			var displayName = name;

			var fieldConfigurationModeName = "FCSettings";
			const int entityTypeId = (int)SystemEntity.FieldConfiguration;

			var dtFieldConfigurations = GetFieldConfigurationsByName(entityTypeId, fieldConfigurationModeName, name);

			if (dtFieldConfigurations != null && dtFieldConfigurations.Rows.Count > 0)
			{
				displayName = dtFieldConfigurations.Rows[0][FieldConfigurationDataModel.DataColumns.FieldConfigurationDisplayName].ToString();
			}

			return displayName;
		}

		public static int GetFieldConfigurationColumnDisplayOrder(string name)
		{
			var displayOrder = 1;

			var fieldConfigurationModeName = "FCSettings";
			const int entityTypeId = (int)SystemEntity.FieldConfiguration;

			var dtFieldConfigurations = GetFieldConfigurationsByName(entityTypeId, fieldConfigurationModeName, name);

			if (dtFieldConfigurations != null && dtFieldConfigurations.Rows.Count > 0)
			{
				displayOrder = Convert.ToInt32(dtFieldConfigurations.Rows[0][FieldConfigurationDataModel.DataColumns.GridViewPriority]);
			}

			return displayOrder;
		}

		private static int GetModeId(string key)
		{
			var modeId = -1;
			try
			{
				modeId = GetFieldConfigurationModeId(key, SessionVariables.RequestProfile);				
			}
			catch { }
			//var modeId = -1;
			//try
			//{
			//	modeId = Convert.ToInt32(key);
			//}
			//catch { }
			 
			return modeId;
		}

		public static int GetFieldConfigurationModeId(string Name, RequestProfile requestProfile)
		{
			var data = new FieldConfigurationModeDataModel();
			data.Name = Name;
			var dt = FieldConfigurationModeDataManager.GetDetails(data, SessionVariables.RequestProfile);//SessionVariables.ApplicationMode);

            if (dt != null)
                return dt.FieldConfigurationModeId.Value;
            else
                return -1;
		}

		public static string[] GetEntityColumns(string fieldConfigurationMode, SystemEntity systemEntityType, RequestProfile requestProfile)
		{
			return GetEntityColumns(systemEntityType, fieldConfigurationMode, requestProfile);
		}

		public static string[] GetEntityColumns(SystemEntity systemEntityType, string fcMode, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(fcMode);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)systemEntityType, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)systemEntityType, requestProfile);
			return returnValues;
		}		

		public static string[] GetGridViewColumns(int systemEntityTypeId, RequestProfile requestProfile)
		{
			var odt = GetFieldConfigurations(systemEntityTypeId, null, null);

			// build string of the valid columns for the given entity and return it
			var validColumns = new string[odt.Rows.Count];
			for (var i = 0; i < odt.Rows.Count; i++)
			{
				validColumns[i] = odt.Rows[i][FieldConfigurationDataModel.DataColumns.Name].ToString();
			}
			return validColumns;
		}

		public static string[] GetGridViewColumns(int systemEntityTypeId, int fieldConfigurationMode, RequestProfile requestProfile)
		{
			var odt = GetFieldConfigurations(systemEntityTypeId, fieldConfigurationMode, null);

			var validColumns = new string[odt.Rows.Count];
			for (var i = 0; i < odt.Rows.Count; i++)
			{
				validColumns[i] = odt.Rows[i][FieldConfigurationDataModel.DataColumns.Name].ToString();
			}
			return validColumns;
		}

		public static string[] GetAuditHistoryColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;

			switch (key)
			{
				case "DBColumns":
					{
						returnValues = GetGridViewColumns((int)SystemEntity.AuditHistory, requestProfile);
					}
					break;
				case "Find":
					returnValues = new[] { "SystemEntity", "EntityKey", "AuditAction", "Action By", "Date" };
					break;
				case "FindByAuditAction":
					returnValues = new[] { "SystemEntity", "EntityKey", "AuditAction", "Start", "End", "Count", "SystemEntityId", "AuditActionId" };
					break;
				default:
					returnValues = new[] { "SystemEntity", "EntityKey", "AuditAction", "Action By", "Start", "End", "Count", "SystemEntityId", "AuditActionId", "PersonId" };
					break;
			}

			return returnValues;
		}

		public static DataTable GetApplicableModesList(SystemEntity systemEntityTypeId)
		{
            var sortedValidModes = new List<FieldConfigurationModeDataModel>();

            if (HttpContext.Current.Session["ValidModes_" + systemEntityTypeId] != null)
            {
                sortedValidModes = (List<FieldConfigurationModeDataModel>)HttpContext.Current.Session["ValidModes_" + systemEntityTypeId];
            }
			else
			{
				var data = new FieldConfigurationDataModel();
				data.SystemEntityTypeId = Convert.ToInt32(systemEntityTypeId);
				data.ApplicationId = SessionVariables.RequestProfile.ApplicationId;

                var fcRecordsByEntity = FieldConfigurationDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

				var dataMode = new FieldConfigurationModeDataModel();
				dataMode.ApplicationId = SessionVariables.RequestProfile.ApplicationId;
                var fcModeList = FieldConfigurationModeDataManager.GetEntityDetails(dataMode, SessionVariables.RequestProfile);

                var validModes = new List<FieldConfigurationModeDataModel>();


                var validModes1 = new List<FieldConfigurationModeDataModel>();
                var validModes2 = new List<FieldConfigurationModeDataModel>();
                var fcModesByEntity = new List<FieldConfigurationModeDataModel>();

				//hdnFieldConfigurationModeCategory.Value = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.FieldConfigurationModeCategoryKey, SettingCategory);

				var fcModeCategoryData = new FieldConfigurationModeCategoryDataModel();
				fcModeCategoryData.Name = "List";

				var fcModeCategoryDt = FieldConfigurationModeCategoryDataManager.GetDetails(fcModeCategoryData, SessionVariables.SystemRequestProfile);
				var fcModeCategoryId = fcModeCategoryDt.FieldConfigurationModeCategoryId.ToString();

				var applicationModeId = 0;
				var modedData = new ApplicationModeDataModel();

				if (SessionVariables.IsTesting)
					modedData.Name = "Testing";
				else
					modedData.Name = "Live";

				modedData.ApplicationId = SessionVariables.RequestProfile.ApplicationId;
				//modedata.Name = SessionVariables.UserApplicationMode;
				var obj = ApplicationModeDataManager.GetDetails(modedData, SessionVariables.RequestProfile);

                if (obj != null)
				{
                    applicationModeId = obj.ApplicationModeId.Value;
				}

				//Added SystemEntityTypeId = 3000 only for testing FCModeCategory
				if (!string.IsNullOrEmpty(fcModeCategoryId) && applicationModeId != 0)
                {
                    var modelAppModexFCMode = new ApplicationModeXFieldConfigurationModeDataModel();
                    modelAppModexFCMode.ApplicationModeId = applicationModeId;

					var appModeList = ApplicationModeXFieldConfigurationModeDataManager.GetEntityDetails(modelAppModexFCMode, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);

                    var distinctFieldValues = appModeList.OrderBy(x => x.FieldConfigurationModeId).Select(x => x.FieldConfigurationModeId).Distinct().ToList();

                    var modeIds = new List<int>();

                    foreach (var valModeId in distinctFieldValues)
                    {
                        modeIds.Add(valModeId.Value);
                    }

                    //var rows = fcModeList.Select(FieldConfigurationModeDataModel.DataColumns.FieldConfigurationModeId + " in (" + modeIds + ")");
                    var selection = fcModeList.Where(x => modeIds.Contains(x.FieldConfigurationModeId.Value)).ToList();
                    if (selection.Count > 0)
                    {
                        foreach (var lstItem in selection)
                        {
                            validModes1.Add(lstItem);
                        }
                    }

                    var modelFCModeCategoryXFCMode = new FieldConfigurationModeCategoryXFCModeDataModel();
                    modelFCModeCategoryXFCMode.FieldConfigurationModeCategoryId = int.Parse(fcModeCategoryId);

					var fcModeCategoryTempList = FieldConfigurationModeCategoryXFCModeDataManager.GetEntityDetails(modelFCModeCategoryXFCMode, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);

                    distinctFieldValues = fcModeCategoryTempList.OrderBy(x => x.FieldConfigurationModeId).Select(x => x.FieldConfigurationModeId).Distinct().ToList();

                    modeIds = new List<int>();

                    foreach (var valModeId in distinctFieldValues)
                    {
                        modeIds.Add(valModeId.Value);
                    }

                    selection = fcModeList.Where(x => modeIds.Contains(x.FieldConfigurationModeId.Value)).ToList();
                    if (selection.Count > 0)
                    {
                        foreach (var lstItem in selection)
                        {
                            validModes2.Add(lstItem);
                        }
                    }

                    validModes = validModes1.Intersect(validModes2).ToList();

                    fcModesByEntity = GetApplicableModesListByEntity(fcRecordsByEntity, validModes);

                    //Last Step (join the result with all the fc modes list by entity)
                    if (validModes.Count != 0 && fcModesByEntity.Count != 0)
                    {
                        fcModeList = (from a in fcModeList
                                      join b in fcModesByEntity
                                 on a.FieldConfigurationModeId equals b.FieldConfigurationModeId
                                 into g
                                      where g.Count() > 0
                                      select a).ToList();

                    }
                    else
                    {
                        fcModeList.Clear();
                    }

                    fcModeList = fcModeList.Distinct().ToList();

                    validModes = fcModeList;

					HttpContext.Current.Session["ValidModes_" + systemEntityTypeId] = fcModeList;

					//validModes.Merge(modesbysystementity, false);

				}
				else
				{
					validModes = GetApplicableModesListByEntity(fcRecordsByEntity, fcModeList);
				}

                sortedValidModes = validModes.OrderBy(x => x.SortOrder).ToList();
			}

			return sortedValidModes.ToDataTable();
		}


        static List<FieldConfigurationModeDataModel> GetApplicableModesListByEntity(List<FieldConfigurationDataModel> fcRecordsByEntity, List<FieldConfigurationModeDataModel> modes)
        {
            var listValidModes = new List<FieldConfigurationModeDataModel>();

            foreach (var fcModeItem in modes)
            {
                foreach (var fcItem in fcRecordsByEntity)
                {
                    var a = fcItem.FieldConfigurationModeId;
                    var b = fcModeItem.FieldConfigurationModeId;

                    if (a == b)
                    {
                        var count = listValidModes.Where(x => x.FieldConfigurationModeId == a).ToList().Count;
                        if (count == 0)
                        {
                            listValidModes.Add(fcModeItem);
                        }
                    }
                }
            }

            return listValidModes;

        }

	}

}