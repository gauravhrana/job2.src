using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using DataModel.Framework.Configuration;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using Framework.Components.UserPreference;
//using Shared.WebCommon.UI.Web;
namespace Framework.Components
{
	public class ApplicationSecurity
	{

		public static string[] GetEntityColumns(string fieldConfigurationMode, SystemEntity systemEntityType, RequestProfile requestProfile)
		{
			string[] columns;

			var fieldConfigModeId = GetModeId(fieldConfigurationMode);

			if (fieldConfigModeId > 0)
				columns = GetGridViewColumns((int)systemEntityType, fieldConfigModeId, requestProfile);
			else
				columns = GetGridViewColumns((int)systemEntityType, requestProfile);

			return columns;
		}

		private static int GetModeId(string key)
		{
			try
			{
				if (string.IsNullOrEmpty(key) || key == "DBColumns")
				{
					key = "-1";
				}
			}
			catch { }
			var modeId = Convert.ToInt32(key);
			return modeId;
		}

		public static string[] GetGridViewColumns(int systemEntityTypeId, RequestProfile requestProfile)
		{
			var obj = new FieldConfigurationDataModel();
			var odt = new DataTable();

			// Check if the session instance of GridTable already exists
			// If not GetGridViewColumns for the particular entity and add it as Session instance "GridTable"
			if (HttpContext.Current.Session["GridTable"] == null)
			{
				obj.SystemEntityTypeId = systemEntityTypeId;
				odt = FieldConfigurationDataManager.Search(obj, requestProfile, 0);//SessionVariables.ApplicationMode);

				odt.DefaultView.Sort = FieldConfigurationDataModel.DataColumns.GridViewPriority + " ASC";
				odt = odt.DefaultView.ToTable();

				//create session instance of "GridTable" with AEFL columns for the given entity
				HttpContext.Current.Session.Add("GridTable", odt);
			}
			else
			{
				// if session instance of "GridTable" already exists retrieve it from session
				var dtInSession = (DataTable)HttpContext.Current.Session["GridTable"];
				// create a clone of the "GridTable" in session
				odt = dtInSession.Clone();
				// check to see if the AEFL columns of the given entity is already cached in the session object
				var datarows = dtInSession.Select("SystemEntityTypeId = " + systemEntityTypeId);
				// if yes import the columns data into the cloned object
				if (datarows.Length > 0)
				{
					foreach (var dr in datarows)
					{
						odt.ImportRow(dr);
					}
				}
				// if the AEFL columns for the given entity are not cached in the session add them by retrieving it from AEFL table in DB
				else
				{
					obj.SystemEntityTypeId = systemEntityTypeId;
					odt = FieldConfigurationDataManager.Search(obj, requestProfile,0);

					odt.DefaultView.Sort = FieldConfigurationDataModel.DataColumns.GridViewPriority + " ASC";
					odt = odt.DefaultView.ToTable();

					//Merge the retrieved AEFL columns for the given entity and update the GridTable object in session
					dtInSession.Merge(odt);

					dtInSession.DefaultView.Sort = FieldConfigurationDataModel.DataColumns.GridViewPriority + " ASC";
					dtInSession = dtInSession.DefaultView.ToTable();

					HttpContext.Current.Session["GridTable"] = dtInSession;
				}
			}

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
			var obj = new FieldConfigurationDataModel();
			var odt = new DataTable();
			// Check if the session instance of GridTable already exists
			// If not GetGridViewColumns for the particular entity and add it as Session instance "GridTable"
			if (HttpContext.Current.Session["GridTable"] == null)
			{
				obj.SystemEntityTypeId = systemEntityTypeId;
				odt = FieldConfigurationDataManager.Search(obj, requestProfile, 0);//SessionVariables.ApplicationMode);

				odt.DefaultView.Sort = FieldConfigurationDataModel.DataColumns.GridViewPriority + " ASC";
				odt = odt.DefaultView.ToTable();

				// create session instance of "GridTable" with AEFL columns for the given entity
				HttpContext.Current.Session.Add("GridTable", odt);
			}
			else
			{
				// if session instance of "GridTable" already exists retrieve it from session
				var dtInSession = (DataTable)HttpContext.Current.Session["GridTable"];
				// create a clone of the "GridTable" in session
				odt = dtInSession.Clone();
				// check to see if the AEFL columns of the given entity is already cached in the session object
				var datarows = dtInSession.Select("SystemEntityTypeId = " + systemEntityTypeId);
				// if yes import the columns data into the cloned object
				if (datarows.Length > 0)
				{
					foreach (var dr in datarows)
					{
						odt.ImportRow(dr);
					}
				}
				//if the AEFL columns for the given entity are not cached in the session add them by retrieving it from AEFL table in DB
				else
				{
					obj.SystemEntityTypeId = systemEntityTypeId;
					odt = FieldConfigurationDataManager.Search(obj, requestProfile, 0);//SessionVariables.ApplicationMode);

					odt.DefaultView.Sort = FieldConfigurationDataModel.DataColumns.GridViewPriority + " ASC";
					odt = odt.DefaultView.ToTable();

					//Merge the retrieved AEFL columns for the given entity and update the GridTable object in session
					dtInSession.Merge(odt);

					dtInSession.DefaultView.Sort = FieldConfigurationDataModel.DataColumns.GridViewPriority + " ASC";
					dtInSession = dtInSession.DefaultView.ToTable();

					HttpContext.Current.Session["GridTable"] = dtInSession;
				}
			}

			// build string of the valid columns for the given entity and return it
			var columns = odt.Select("FieldConfigurationModeId = " + fieldConfigurationMode);
			var validColumns = new string[columns.Length];
			for (var i = 0; i < columns.Length; i++)
			{
				validColumns[i] = columns[i][FieldConfigurationDataModel.DataColumns.Name].ToString();
			}
			return validColumns;
		}

		public static DataTable GetGridViewColumnsAsDataTable(int systemEntityTypeId, int fieldConfigurationMode, RequestProfile requestProfile)
		{
			var obj = new FieldConfigurationDataModel();
			var odt = new DataTable();
			// Check if the session instance of GridTable already exists
			// If not GetGridViewColumns for the particular entity and add it as Session instance "GridTable"
			if (HttpContext.Current.Session["GridTable"] == null)
			{
				obj.SystemEntityTypeId = systemEntityTypeId;
				odt = FieldConfigurationDataManager.Search(obj, requestProfile,0);//SessionVariables.ApplicationMode);
				// create session instance of "GridTable" with AEFL columns for the given entity
				HttpContext.Current.Session.Add("GridTable", odt);
			}
			else
			{
				// if session instance of "GridTable" already exists retrieve it from session
				var dtInSession = (DataTable)HttpContext.Current.Session["GridTable"];
				// create a clone of the "GridTable" in session
				odt = dtInSession.Clone();
				// check to see if the AEFL columns of the given entity is already cached in the session object
				var datarows = dtInSession.Select("SystemEntityTypeId = " + systemEntityTypeId);
				// if yes import the columns data into the cloned object
				if (datarows.Length > 0)
				{
					foreach (var dr in datarows)
					{
						odt.ImportRow(dr);
					}
				}
				//if the AEFL columns for the given entity are not cached in the session add them by retrieving it from AEFL table in DB
				else
				{
					obj.SystemEntityTypeId = systemEntityTypeId;
					odt = FieldConfigurationDataManager.Search(obj, requestProfile,0);//SessionVariables.ApplicationMode);
					//Merge the retrieved AEFL columns for the given entity and update the GridTable object in session
					dtInSession.Merge(odt);
					HttpContext.Current.Session["GridTable"] = dtInSession;
				}
			}

			// build string of the valid columns for the given entity and return it
			var columns = odt.Select("FieldConfigurationModeId = " + fieldConfigurationMode);
			var dt = odt.Clone();
			for (var i = 0; i < columns.Length; i++)
			{
				dt.ImportRow(columns[i]);
			}
			return dt;
		}

		//public static string[] GetGridViewColumns(int systemEntityTypeId, RequestProfile requestProfile, DataTable oGridTable)
		//{
		//    var obj = new FieldConfiguration.Data();
		//    var odt = new DataTable();

		//    //Check if the session instance of GridTable already exists
		//    //If not GetGridViewColumns for the particular entity and add it as Session instance "GridTable"
		//    if (oGridTable == null)
		//    {
		//        obj.SystemEntityTypeId = systemEntityTypeId;
		//        odt = FieldConfiguration.GetGridViewColumns(obj, requestProfile);
		//        //create session instance of "GridTable" with AEFL columns for the given entity				
		//    }
		//    else
		//    {
		//        //if session instance of "GridTable" already exists retrieve it from session
		//        var dtInSession = oGridTable;
		//        //create a clone of the "GridTable" in session
		//        odt = dtInSession.Clone();
		//        //check to see if the AEFL columns of the given entity is already cached in the session object
		//        var datarows = dtInSession.Select("SystemEntityTypeId = " + systemEntityTypeId);
		//        //if yes import the columns data into the cloned object
		//        if (datarows.Length > 0)
		//        {
		//            foreach (DataRow dr in datarows)
		//            {
		//                odt.ImportRow(dr);
		//            }
		//        }
		//        //if the AEFL columns for the given entity are not cached in the session add them by retrieving it from AEFL table in DB
		//        else
		//        {
		//            obj.SystemEntityTypeId = systemEntityTypeId;
		//            odt = FieldConfiguration.GetGridViewColumns(obj, requestProfile);
		//            //Merge the retrieved AEFL columns for the given entity and update the GridTable object in session
		//            dtInSession.Merge(odt);
		//            oGridTable = dtInSession;
		//        }
		//    }

		//    //build string of the valid columns for the given entity and return it
		//    var validColumns = new string[odt.Rows.Count];
		//    for (var i = 0; i < odt.Rows.Count; i++)
		//    {
		//        validColumns[i] = odt.Rows[i][FieldConfiguration.DataColumns.Name].ToString();
		//    }

		//    return validColumns;
		//}

		//public static string[] GetGridViewColumns(int systemEntityTypeId, int key, RequestProfile requestProfile, DataTable oGridTable)
		//{
		//    var obj = new FieldConfiguration.Data();
		//    var odt = new DataTable();

		//    //Check if the session instance of GridTable already exists
		//    //If not GetGridViewColumns for the particular entity and add it as Session instance "GridTable"
		//    if (oGridTable == null)
		//    {
		//        obj.SystemEntityTypeId = systemEntityTypeId;
		//        odt = FieldConfiguration.GetGridViewColumns(obj, requestProfile);				
		//    }
		//    else
		//    {
		//        //if session instance of "GridTable" already exists retrieve it from session
		//        var dtInSession = oGridTable;
		//        //create a clone of the "GridTable" in session
		//        odt = dtInSession.Clone();
		//        //check to see if the AEFL columns of the given entity is already cached in the session object
		//        var datarows = dtInSession.Select("SystemEntityTypeId = " + systemEntityTypeId);
		//        //if yes import the columns data into the cloned object
		//        if (datarows.Length > 0)
		//        {
		//            foreach (DataRow dr in datarows)
		//            {
		//                odt.ImportRow(dr);
		//            }
		//        }
		//        //if the AEFL columns for the given entity are not cached in the session add them by retrieving it from AEFL table in DB
		//        else
		//        {
		//            obj.SystemEntityTypeId = systemEntityTypeId;
		//            odt = FieldConfiguration.GetGridViewColumns(obj, requestProfile);
		//            //Merge the retrieved AEFL columns for the given entity and update the GridTable object in session
		//            dtInSession.Merge(odt);

		//            oGridTable = dtInSession;
		//        }
		//    }

		//    //build string of the valid columns for the given entity and return it
		//    var columns = odt.Select("FieldConfigurationModeId = " + key);
		//    var validColumns = new string[columns.Length];

		//    for (var i = 0; i < columns.Length; i++)
		//    {
		//        validColumns[i] = columns[i][FieldConfiguration.DataColumns.Name].ToString();
		//    }

		//    return validColumns;
		//}

		public static string[] GetApplicationRoleColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.ApplicationRole, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.ApplicationRole, requestProfile);

			//returnValues = new[] { "Application", "Name", "Description", "SortOrder", "ApplicationId", "Updated Date", "Updated By", "Last Action" };

			return returnValues;
		}

		public static int GetFieldConfigurationModeId(string Name, RequestProfile requestProfile)
		{
			var data = new FieldConfigurationModeDataModel();
			data.Name = Name;
			var dt = FieldConfigurationModeDataManager.Search(data, requestProfile, 0);//SessionVariables.ApplicationMode);

			if (dt.Rows.Count == 1)
				return int.Parse(dt.Rows[0][FieldConfigurationModeDataModel.DataColumns.FieldConfigurationModeId].ToString());
			else
				return -1;
		}

		public static string[] GetTimeZoneColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.TimeZone, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.TimeZone, requestProfile);

			//returnValues = new[] { "Name", "Description", "SortOrder", "TimeDifference" };

			return returnValues;
		}
		public static string[] GetUserLoginHistoryColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.UserLoginHistory, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.UserLoginHistory, requestProfile);

			//returnValues = new[] { "Name", "Description", "SortOrder", "TimeDifference" };

			return returnValues;
		}

		public static string[] GetCountryColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.Country, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.Country, requestProfile);

			//returnValues = new[] { "Name", "TimeZone", "Description", "SortOrder" };

			return returnValues;
		}

		public static string[] GetHelpPageColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.HelpPage, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.HelpPage, requestProfile);

			//returnValues = new[] { "Name", "Content", "SortOrder", "SystemEntityTypeId" };

			return returnValues;
		}

		public static string[] GetSuperKeyColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.SuperKey, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.SuperKey, requestProfile);

			//returnValues = new[] { "Name", "Description", "SortOrder", "SystemEntityTypeId", "ExpirationDate" };

			return returnValues;
		}

		public static string[] GetConnectionStringColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.ConnectionString, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.ConnectionString, requestProfile);

			//returnValues = new[] { "Name", "Description", "SortOrder", "SystemEntityTypeId", "ExpirationDate" };

			return returnValues;
		}

		public static string[] GetSearchKeyColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.SearchKey, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.SearchKey, requestProfile);

			//returnValues = new[] { "Name", "Description", "SortOrder", "SystemEntityTypeId", "ExpirationDate" };

			return returnValues;
		}


		public static string[] GetSearchKeyDetailColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.SearchKeyDetail, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.SearchKeyDetail, requestProfile);

			//returnValues = new[] { "Name", "Description", "SortOrder", "SystemEntityTypeId", "ExpirationDate" };

			return returnValues;
		}

		public static string[] GetSearchKeyDetailItemColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.SearchKeyDetailItem, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.SearchKeyDetailItem, requestProfile);

			//returnValues = new[] { "Name", "Description", "SortOrder", "SystemEntityTypeId", "ExpirationDate" };

			return returnValues;
		}

		public static string[] GetSuperKeyDetailColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.SuperKeyDetail, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.SuperKeyDetail, requestProfile);

			//returnValues = new[] { "SuperKey", "EntityKey" };

			return returnValues;
		}

		public static string[] GetSystemEntityCategoryColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.SystemEntityCategory, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.SystemEntityCategory, requestProfile);

			//returnValues = new[] { "Name", "Description", "SortOrder", "ApplicationId" };

			return returnValues;
		}

		public static string[] GetLog4NetColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.Log4Net, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.Log4Net, requestProfile);

			return returnValues;
		}

		public static string[] GetSystemDevNumbersColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.SystemDevNumbers, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.SystemDevNumbers, requestProfile);

			//returnValues = new[] { "Person", "RangeFrom", "RangeTo", "ApplicationId", "Updated Date", "Updated By", "Last Action" };

			return returnValues;
		}

		public static string[] GetFieldConfigurationColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.FieldConfiguration, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.FieldConfiguration, requestProfile);

			//returnValues = new[] { "Name", "Value", "SystemEntityTypeId", "Width", "Formatting", "ControlType", "HorizontalAlignment", "ApplicationId", "Updated Date", "Updated By", "Last Action" };

			return returnValues;
		}

		public static string[] GetFieldConfigurationModeColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.FieldConfigurationMode, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.FieldConfigurationMode, requestProfile);

			//returnValues = new[] { "Name", "Description", "SortOrder" };

			return returnValues;
		}

		public static string[] GetApplicationColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.Application, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.Application, requestProfile);

			//returnValues = new[] { "Name", "Description", "SortOrder", "Updated Date", "Updated By", "Last Action" };

			return returnValues;
		}

		public static string[] GetStoredProcedureLogColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.StoredProcedureLog, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.StoredProcedureLog, requestProfile);

			//returnValues = new[] { "NameOfSP", "TimeOfExecution", "ExecutedBy", "SystemEntity" };

			return returnValues;
		}

		public static string[] GetStoredProcedureLogRawColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;

			switch (key)
			{

				default:
					returnValues = new[] { "StoredProcedureLogId", "InputParameters", "InputValues" };
					break;
			}

			return returnValues;
		}

		public static string[] GetStoredProcedureLogDetailColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.StoredProcedureLogDetail, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.StoredProcedureLogDetail, requestProfile);

			//returnValues = new[] { "MasterId", "ParameterName", "ParameterValue" };

			return returnValues;
		}


		public static string[] GetApplicationEntityParentalHierarchyColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.ApplicationEntityParentalHierarchy, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.ApplicationEntityParentalHierarchy, requestProfile);

			//returnValues = new[] { "Name", "Description", "SortOrder", "ApplicationId" };

			return returnValues;
		}

		public static string[] GetFieldConfigurationModeCategoryColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;

			switch (key)
			{
				case "DBColumns":
					{
						returnValues = GetGridViewColumns((int)SystemEntity.ApplicationEntityParentalHierarchy, requestProfile);
					}
					break;
				default:
					returnValues = new[] { "Name", "Description", "SortOrder", "ApplicationId" };
					break;
			}

			return returnValues;
		}

		public static string[] GetApplicationModeColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;

			switch (key)
			{
				case "DBColumns":
					{
						returnValues = GetGridViewColumns((int)SystemEntity.ApplicationMode, requestProfile);
					}
					break;
				default:
					returnValues = new[] { "Name", "Description", "SortOrder", "ApplicationId" };
					break;
			}

			return returnValues;
		}

		public static string[] GetDateRangeTitleColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;

			switch (key)
			{
				case "DBColumns":
					{
						returnValues = GetGridViewColumns((int)SystemEntity.DateRangeTitle, requestProfile);
					}
					break;
				default:
					returnValues = new[] { "Name", "Description", "SortOrder", "ApplicationId" };
					break;
			}

			return returnValues;
		}

		public static string[] GetDBNameColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;

			switch (key)
			{
				case "DBColumns":
					{
						returnValues = GetGridViewColumns((int)SystemEntity.DBName, requestProfile);
					}
					break;
				default:
					returnValues = new[] { "Name", "Description", "SortOrder", "ApplicationId" };
					break;
			}

			return returnValues;
		}

		public static string[] GetAEFLModeCategoryXApplicationModeXAEFLModeColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.FieldConfigurationModeCategoryXFCMode, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.FieldConfigurationModeCategoryXFCMode, requestProfile);

			return returnValues;
		}

		public static string[] GetApplicationOperationColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.ApplicationOperation, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.ApplicationOperation, requestProfile);

			//returnValues = new[] { "Name", "Application", "SystemEntityType", "OperationValue", "Updated Date", "Updated By", "Last Action" };

			return returnValues;
		}

		public static string[] GetReleaseLogColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.ReleaseLog, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.ReleaseLog, requestProfile);

			//returnValues = new[] { "Name", "VersionNo", "ReleaseDate", "Description", "SortOrder" };

			return returnValues;
		}

		public static string[] GetReleaseLogDetailColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.ReleaseLogDetail, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.ReleaseLogDetail, requestProfile);

			//returnValues = new[] { "ReleaseLog", "ItemNo", "Description", "RequestedBy", "PrimaryDeveloper", "RequestedDate", "SortOrder" };

			return returnValues;
		}

		public static string[] GetSystemEntityTypeColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.SystemEntityType, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.SystemEntityType, requestProfile);

			//returnValues = new[] { "EntityName", "EntityDescription", "NextValue", "ApplicationId" };

			return returnValues;
		}

		public static string[] GetMenuColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;

			if (key != "DBColumns")
			{
				var fcModeId = GetModeId(key);
				returnValues = GetGridViewColumns((int)SystemEntity.Menu, fcModeId, requestProfile);
			}
			else
			{
				returnValues = GetGridViewColumns((int)SystemEntity.Menu, requestProfile);
			}
			//returnValues = new[] { "Name", "ParentMenu", "NavigateURL", "Description", "SortOrder", "IsChecked", "IsVisible" };

			return returnValues;
		}
		public static string[] GetMenuCategoryColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.MenuCategory, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.MenuCategory, requestProfile);

			//returnValues = new[] { "Name", "ParentMenu", "NavigateURL", "Description", "SortOrder", "IsChecked", "IsVisible" };

			return returnValues;
		}

		#region Audit Module Entities

		public static string[] GetTypeOfIssueColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.TypeOfIssue, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.TypeOfIssue, requestProfile);

			//returnValues = new[] { "Name", "Category", "Description", "SortOrder" };

			return returnValues;
		}

		public static string[] GetAuditActionColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.AuditAction, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.AuditAction, requestProfile);

			//returnValues = new[] { "Name", "Description", "SortOrder" };

			return returnValues;
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

		#endregion

		#region User Preference Module Entities

		public static string[] GetUserPreferenceDataTypeColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.UserPreferenceDataType, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.UserPreferenceDataType, requestProfile);

			//returnValues = new[] { "Name", "Description", "SortOrder", "ApplicationId", "Updated Date", "Updated By", "Last Action" };

			return returnValues;
		}

		public static string[] GetUserPreferenceCategoryColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.UserPreferenceCategory, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.UserPreferenceCategory, requestProfile);
			//returnValues = new[] { "Name", "Description", "SortOrder", "ApplicationId", "Updated Date", "Updated By", "Last Action" };

			return returnValues;
		}

		public static string[] GetUserPreferenceKeyColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.UserPreferenceKey, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.UserPreferenceKey, requestProfile);

			//returnValues = new[] { "UserPreferenceDataType", "Name", "Value", "Description", "SortOrder", "ApplicationId", "Updated Date", "Updated By", "Last Action" };

			return returnValues;
		}

		public static string[] GetUserPreferenceColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.UserPreference, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.UserPreference, requestProfile);

			//returnValues = new[] { "Application", "ApplicationUser", "UserPreferenceCategory", "UserPreferenceDataType", "UserPreferenceKey", "Value", "ApplicationId", "Updated Date", "Updated By", "Last Action" };

			return returnValues;
		}

		#endregion

		#region Import Module Entities

		public static string[] GetBatchFileStatusColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.BatchFileStatus, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.BatchFileStatus, requestProfile);

			//returnValues = new[] { "Name", "Description", "SortOrder", "ApplicationId" };

			return returnValues;
		}

		public static string[] GetFileTypeColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.FileType, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.FileType, requestProfile);

			//returnValues = new[] { "Name", "Description", "SortOrder", "ApplicationId" };

			return returnValues;
		}

		public static string[] GetBatchFileSetColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.BatchFileSet, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.BatchFileSet, requestProfile);

			//returnValues = new[] { "Name", "Description", "CreatedDate", "CreatedByPerson", "ApplicationId" };

			return returnValues;
		}

		public static string[] GetBatchFileColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.BatchFile, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.BatchFile, requestProfile);

			//returnValues = new[] { "SystemEntityType", "FileType", "BatchFileSet", "BatchFileStatus", "Name", "Folder", "BatchFile", "Description", "ApplicationId", "CreatedDate", "CreatedByPerson", "UpdatedDate", "UpdatedByPerson", "SortOrder" };

			return returnValues;
		}

		public static string[] GetBatchFileHistoryColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.BatchFileHistory, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.BatchFileHistory, requestProfile);

			//returnValues = new[] { "BatchFileSet", "BatchFileStatus", "BatchFileId", "ApplicationId", "UpdatedDate", "UpdatedBy", "LastAction" };

			return returnValues;
		}

		#endregion


		public static string[] GetUserPreferenceSelectedItemColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			try
			{

				var fcModeId = GetModeId(key);

				if (fcModeId > 0)
					returnValues = GetGridViewColumns((int)SystemEntity.UserPreferenceSelectedItem, fcModeId, requestProfile);
				else
					returnValues = GetGridViewColumns((int)SystemEntity.UserPreferenceSelectedItem, requestProfile);
			}
			catch
			{
				switch (key)
				{
					case "top":
						returnValues = new[] { "Value", "Count" };
						break;
					default:
						returnValues = new[] { "Application", "ApplicationUser", "UserPreferenceKey", "ParentKey", "Value", "SortOrder", "ApplicationId", "Updated Date", "Updated By", "Last Action" };
						break;
				}
			}

			return returnValues;
		}

		public static string[] GetApplicationMonitoredEventEmailColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.ApplicationMonitoredEventEmail, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.ApplicationMonitoredEventEmail, requestProfile);

			//returnValues = new[] { "ApplicationMonitoredEventSource", "User", "CorrespondenceLevel", "Active", "ApplicationId" };

			return returnValues;
		}

		public static string[] GetApplicationMonitoredEventColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.ApplicationMonitoredEvent, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.ApplicationMonitoredEvent, requestProfile);

			//returnValues = new[] { "ApplicationMonitoredEventSource", "ApplicationMonitoredEventProcessingState", "ReferenceId", "ReferenceCode", "Category", "Message", "IsDuplicate", "ApplicationId", "LastModifiedBy", "LastModifiedOn" };

			return returnValues;
		}

		#region Event Monitoring Entities

		public static string[] GetApplicationMonitoredEventSourceColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.ApplicationMonitoredEventSource, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.ApplicationMonitoredEventSource, requestProfile);

			//returnValues = new[] { "Code", "Description", "ApplicationId" };

			return returnValues;
		}

		public static string[] GetApplicationMonitoredEventProcessingStateColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.ApplicationMonitoredEventProcessingState, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.ApplicationMonitoredEventProcessingState, requestProfile);

			//returnValues = new[] { "Code", "Description", "ApplicationId" };

			return returnValues;
		}


		#endregion


		#region Task Module Entities

		public static string[] GetTaskEntityTypeColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.TaskEntityType, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.TaskEntityType, requestProfile);

			//returnValues = new[] { "ApplicationId", "Name", "Description", "Active", "SortOrder", "ApplicationId" };

			return returnValues;
		}

		public static string[] GetTaskScheduleTypeColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.TaskScheduleType, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.TaskScheduleType, requestProfile);

			//returnValues = new[] { "ApplicationId", "Name", "Description", "Active", "SortOrder", "ApplicationId" };

			return returnValues;
		}

		public static string[] GetTaskEntityColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.TaskEntity, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.TaskEntity, requestProfile);

			//returnValues = new[] { "ApplicationId", "TaskEntityType", "Name", "Description", "Active", "SortOrder", "ApplicationId" };

			return returnValues;
		}

		public static string[] GetTaskScheduleColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.TaskSchedule, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.TaskSchedule, requestProfile);

			//returnValues = new[] { "ApplicationId", "TaskScheduleType", "TaskEntity", "ApplicationId" };

			return returnValues;
		}

		public static string[] GetTaskRunColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.TaskRun, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.TaskRun, requestProfile);

			//returnValues = new[] { "ApplicationId", "TaskEntity", "TaskScheduleId", "BusinessDate", "StartTime", "EndTime", "RunBy", "ApplicationId" };

			return returnValues;
		}

		#endregion

		public static string[] GetPersonTitleColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.PersonTitle, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.PersonTitle, requestProfile);

			//returnValues = new[] { "Name", "Description", "SortOrder" };

			return returnValues;
		}

		public static string[] GetApplicationUserTitleColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.ApplicationUserTitle, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.ApplicationUserTitle, requestProfile);


			//returnValues = new[] { "Name", "Description", "SortOrder" };

			return returnValues;
		}

		public static string[] GetPersonColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.Person, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.Person, requestProfile);

			//returnValues = new[] { "PersonTitle", "FirstName", "MiddleName", "LastName" };

			return returnValues;
		}

		public static string[] GetApplicationUserColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.ApplicationUser, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.ApplicationUser, requestProfile);

			return returnValues;
		}

		public static string[] GetTabParentStructureColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.TabParentStructure, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.TabParentStructure, requestProfile);

			return returnValues;
		}

		public static string[] GetThemeCategoryColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.ThemeCategory, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.ThemeCategory, requestProfile);

			return returnValues;
		}

		public static string[] GetThemeKeyColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.ThemeKey, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.ThemeKey, requestProfile);

			return returnValues;
		}

		public static string[] GetThemeDetailsColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.ThemeDetails, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.ThemeDetails, requestProfile);

			return returnValues;

		}

		public static string[] GetThemesColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.Themes, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.Themes, requestProfile);

			return returnValues;
		}


		public static string[] GetTabChildStructureColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.TabChildStructure, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.TabChildStructure, requestProfile);

			return returnValues;
		}

		public static string[] GetHelpPageContextColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.HelpPageContext, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.HelpPageContext, requestProfile);

			return returnValues;
		}

		public static string[] GetUserLoginStatusColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.UserLoginStatus, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.UserLoginStatus, requestProfile);

			return returnValues;
		}

		public static string[] GetUserLoginColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.UserLogin, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.UserLogin, requestProfile);

			return returnValues;
		}

		public static string[] GetQuickPaginationRunColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.QuickPaginationRun, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.QuickPaginationRun, requestProfile);

			return returnValues;
		}

		public static string[] GetLanguageColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.Language, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.Language, requestProfile);

			return returnValues;
		}

		public static string[] GetModuleColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.Module, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.Module, requestProfile);

			return returnValues;
		}

		public static string[] GetDeveloperRoleColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.DeveloperRole, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.DeveloperRole, requestProfile);

			return returnValues;
		}

		public static string[] GetFeatureOwnerStatusColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.FeatureOwnerStatus, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.FeatureOwnerStatus, requestProfile);

			return returnValues;
		}

		public static string[] GetModuleOwnerColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.ModuleOwner, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.ModuleOwner, requestProfile);

			return returnValues;
		}

		public static string[] GetEntityOwnerColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.EntityOwner, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.EntityOwner, requestProfile);

			return returnValues;
		}

		public static string[] GetFunctionalityOwnerColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.FunctionalityOwner, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.FunctionalityOwner, requestProfile);

			return returnValues;
		}

		public static string[] GetTraceColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.Trace, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.Trace, requestProfile);

			return returnValues;
		}

		public static string[] GetApplicationUserProfileImageColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.ApplicationUserProfileImage, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.ApplicationUserProfileImage, requestProfile);

			return returnValues;
		}

		public static string[] GetApplicationUserProfileImageMasterColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.ApplicationUserProfileImageMaster, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.ApplicationUserProfileImageMaster, requestProfile);

			return returnValues;
		}

		public static string[] GetApplicationRouteColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.ApplicationRoute, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.ApplicationRoute, requestProfile);

			return returnValues;
		}

		public static string[] GetSystemForeignRelationshipColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.SystemForeignRelationship, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.SystemForeignRelationship, requestProfile);

			return returnValues;
		}

		public static string[] GetAllEntityDetailColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.AllEntityDetail, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.AllEntityDetail, requestProfile);

			return returnValues;
		}

		public static string[] GetApplicationRouteParameterColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.ApplicationRouteParameter, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.ApplicationRouteParameter, requestProfile);

			return returnValues;
		}

		public static string[] GetSystemForeignRelationshipTypeColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.SystemForeignRelationshipType, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.SystemForeignRelationshipType, requestProfile);

			return returnValues;
		}

		public static string[] GetSystemForeignRelationshipDatabaseColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.SystemForeignRelationshipDatabase, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.SystemForeignRelationshipDatabase, requestProfile);

			return returnValues;
		}
		#region QA Entities

		public static string[] GetFunctionalityColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.Functionality, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.Functionality, requestProfile);

			//returnValues = new[] { "FirstName", "MiddleName", "LastName", "Application" };

			return returnValues;
		}

		public static string[] GetFunctionalityXFunctionalityActiveStatusColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.FunctionalityXFunctionalityActiveStatus, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.FunctionalityXFunctionalityActiveStatus, requestProfile);

			//returnValues = new[] { "FirstName", "MiddleName", "LastName", "Application" };

			return returnValues;
		}


		public static string[] GetFunctionalityStatusColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.FunctionalityStatus, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.FunctionalityStatus, requestProfile);

			//returnValues = new[] { "FirstName", "MiddleName", "LastName", "Application" };

			return returnValues;
		}

		public static string[] GetFunctionalityActiveStatusColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			if (key == "DBColumns")
			{
				key = "-1";
			}
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.FunctionalityActiveStatus, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.FunctionalityActiveStatus, requestProfile);

			//returnValues = new[] { "FirstName", "MiddleName", "LastName", "Application" };

			return returnValues;
		}

		public static string[] GetFunctionalityDevelopmentStepColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			if (key == "DBColumns")
			{
				key = "-1";
			}
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.FunctionalityDevelopmentStep, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.FunctionalityDevelopmentStep, requestProfile);

			//returnValues = new[] { "FirstName", "MiddleName", "LastName", "Application" };

			return returnValues;
		}

		public static string[] GetFunctionalityPriorityColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			if (key != "DBColumns")
			{
				var fcModeId = GetModeId(key);
				returnValues = GetGridViewColumns((int)SystemEntity.FunctionalityPriority, fcModeId, requestProfile);
			}
			else
			{
				returnValues = GetGridViewColumns((int)SystemEntity.FunctionalityPriority, requestProfile);
			}


			//returnValues = new[] { "FirstName", "MiddleName", "LastName", "Application" };

			return returnValues;
		}

		public static string[] GetFunctionalityImageColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			if (key != "DBColumns")
			{
				var fcModeId = GetModeId(key);
				returnValues = GetGridViewColumns((int)SystemEntity.FunctionalityImage, fcModeId, requestProfile);
			}
			else
			{
				returnValues = GetGridViewColumns((int)SystemEntity.FunctionalityImage, requestProfile);
			}


			return returnValues;
		}

		public static string[] GetFunctionalityImageAttributeColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			if (key != "DBColumns")
			{
				var fcModeId = GetModeId(key);
				returnValues = GetGridViewColumns((int)SystemEntity.FunctionalityImageAttribute, fcModeId, requestProfile);
			}
			else
			{
				returnValues = GetGridViewColumns((int)SystemEntity.FunctionalityImageAttribute, requestProfile);
			}


			return returnValues;
		}

		public static string[] GetFunctionalityImageInstanceColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			if (key != "DBColumns")
			{
				var fcModeId = GetModeId(key);
				returnValues = GetGridViewColumns((int)SystemEntity.FunctionalityImageInstance, fcModeId, requestProfile);
			}
			else
			{
				returnValues = GetGridViewColumns((int)SystemEntity.FunctionalityImageInstance, requestProfile);
			}


			return returnValues;
		}

		public static string[] GetFunctionalityXFunctionalityImageColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			if (key != "DBColumns")
			{
				var fcModeId = GetModeId(key);
				returnValues = GetGridViewColumns((int)SystemEntity.FunctionalityXFunctionalityImage, fcModeId, requestProfile);
			}
			else
			{
				returnValues = GetGridViewColumns((int)SystemEntity.FunctionalityXFunctionalityImage, requestProfile);
			}


			return returnValues;
		}


		public static string[] GetFunctionalityEntityStatusColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			if (key != "DBColumns")
			{
				var fcModeId = GetModeId(key);
				returnValues = GetGridViewColumns((int)SystemEntity.FunctionalityEntityStatus, fcModeId, requestProfile);
			}
			else
			{
				returnValues = GetGridViewColumns((int)SystemEntity.FunctionalityEntityStatus, requestProfile);
			}

			//returnValues = new[] { "FirstName", "MiddleName", "LastName", "Application" };

			return returnValues;
		}

		public static string[] GetFunctionalityEntityStatusArchiveColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			if (key != "DBColumns")
			{
				var fcModeId = GetModeId(key);
				returnValues = GetGridViewColumns((int)SystemEntity.FunctionalityEntityStatusArchive, fcModeId, requestProfile);
			}
			else
			{
				returnValues = GetGridViewColumns((int)SystemEntity.FunctionalityEntityStatusArchive, requestProfile);
			}

			//returnValues = new[] { "FirstName", "MiddleName", "LastName", "Application" };

			return returnValues;
		}

		public static string[] GetMilestoneXFeatureArchiveColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.MilestoneXFeatureArchive, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.MilestoneXFeatureArchive, requestProfile);

			//returnValues = new[] { "FirstName", "MiddleName", "LastName", "Application" };

			return returnValues;
		}

		public static string[] GetReleaseLogStatusColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.ReleaseLogStatus, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.ReleaseLogStatus, requestProfile);

			//returnValues = new[] { "Name", "VersionNo", "ReleaseDate", "Description", "SortOrder" };

			return returnValues;
		}

		public static string[] GetIssueTypeReleaseColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.ReleaseIssueType, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.ReleaseIssueType, requestProfile);

			//returnValues = new[] { "Name", "VersionNo", "ReleaseDate", "Description", "SortOrder" };

			return returnValues;
		}

		public static string[] GetReleasePublishCategoryColumns(string key, RequestProfile requestProfile)
		{
			string[] returnValues;
			var fcModeId = GetModeId(key);

			if (fcModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.ReleasePublishCategory, fcModeId, requestProfile);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.ReleasePublishCategory, requestProfile);

			//returnValues = new[] { "Name", "VersionNo", "ReleaseDate", "Description", "SortOrder" };

			return returnValues;
		}
		#endregion


	}
}
