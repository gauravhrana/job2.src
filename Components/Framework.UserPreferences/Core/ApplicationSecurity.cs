using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using Framework.Components.DataAccess;
using Framework.Components.DataAccess.DomainModel;

namespace Framework.Components
{
	public class ApplicationSecurity
    {

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

		public static string[] GetGridViewColumns(int systemEntityTypeId, int auditId)
		{
            var obj = new UserPreference.DomainModel.FieldConfiguration();
			var odt = new DataTable();

			// Check if the session instance of GridTable already exists
			// If not GetGridViewColumns for the particular entity and add it as Session instance "GridTable"
			if (HttpContext.Current.Session["GridTable"] == null)
			{
				obj.SystemEntityTypeId = systemEntityTypeId;
                odt = UserPreference.FieldConfiguration.GetGridViewColumns(obj, auditId);
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
					foreach (DataRow dr in datarows)
					{
						odt.ImportRow(dr);
					}
				}
				// if the AEFL columns for the given entity are not cached in the session add them by retrieving it from AEFL table in DB
				else
				{
					obj.SystemEntityTypeId = systemEntityTypeId;
					odt = UserPreference.FieldConfiguration.GetGridViewColumns(obj, auditId);
					//Merge the retrieved AEFL columns for the given entity and update the GridTable object in session
					dtInSession.Merge(odt);
					HttpContext.Current.Session["GridTable"] = dtInSession;
				}
			}

			// build string of the valid columns for the given entity and return it
			var validColumns = new string[odt.Rows.Count];
			for (var i = 0; i < odt.Rows.Count; i++)
			{
                validColumns[i] = odt.Rows[i][UserPreference.DomainModel.FieldConfiguration.DataColumns.Name].ToString();
			}
			return validColumns;
		}

        public static string[] GetGridViewColumns(int systemEntityTypeId, int fieldConfigurationMode, int auditId)
		{
            var obj = new UserPreference.DomainModel.FieldConfiguration();
			var odt = new DataTable();
			// Check if the session instance of GridTable already exists
			// If not GetGridViewColumns for the particular entity and add it as Session instance "GridTable"
			if (HttpContext.Current.Session["GridTable"] == null)
			{
				obj.SystemEntityTypeId = systemEntityTypeId;
                odt = UserPreference.FieldConfiguration.GetGridViewColumns(obj, auditId);
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
					foreach (DataRow dr in datarows)
					{
						odt.ImportRow(dr);
					}
				}
				//if the AEFL columns for the given entity are not cached in the session add them by retrieving it from AEFL table in DB
				else
				{
					obj.SystemEntityTypeId = systemEntityTypeId;
                    odt = UserPreference.FieldConfiguration.GetGridViewColumns(obj, auditId);
					//Merge the retrieved AEFL columns for the given entity and update the GridTable object in session
					dtInSession.Merge(odt);
					HttpContext.Current.Session["GridTable"] = dtInSession;
				}
			}

			// build string of the valid columns for the given entity and return it
            var columns = odt.Select("FieldConfigurationModeId = " + fieldConfigurationMode);
			var validColumns = new string[columns.Length];
			for (var i = 0; i < columns.Length; i++)
			{
                validColumns[i] = columns[i][UserPreference.DomainModel.FieldConfiguration.DataColumns.Name].ToString();
			}
			return validColumns;
		}

        public static DataTable GetGridViewColumnsAsDataTable(int systemEntityTypeId, int fieldConfigurationMode, int auditId)
        {
            var obj = new UserPreference.DomainModel.FieldConfiguration();
            var odt = new DataTable();
            // Check if the session instance of GridTable already exists
            // If not GetGridViewColumns for the particular entity and add it as Session instance "GridTable"
            if (HttpContext.Current.Session["GridTable"] == null)
            {
                obj.SystemEntityTypeId = systemEntityTypeId;
                odt = UserPreference.FieldConfiguration.GetGridViewColumns(obj, auditId);
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
                    foreach (DataRow dr in datarows)
                    {
                        odt.ImportRow(dr);
                    }
                }
                //if the AEFL columns for the given entity are not cached in the session add them by retrieving it from AEFL table in DB
                else
                {
                    obj.SystemEntityTypeId = systemEntityTypeId;
                    odt = UserPreference.FieldConfiguration.GetGridViewColumns(obj, auditId);
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

		//public static string[] GetGridViewColumns(int systemEntityTypeId, int auditId, DataTable oGridTable)
		//{
		//    var obj = new FieldConfiguration.Data();
		//    var odt = new DataTable();
			
		//    //Check if the session instance of GridTable already exists
		//    //If not GetGridViewColumns for the particular entity and add it as Session instance "GridTable"
		//    if (oGridTable == null)
		//    {
		//        obj.SystemEntityTypeId = systemEntityTypeId;
		//        odt = FieldConfiguration.GetGridViewColumns(obj, auditId);
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
		//            odt = FieldConfiguration.GetGridViewColumns(obj, auditId);
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

		//public static string[] GetGridViewColumns(int systemEntityTypeId, int key, int auditId, DataTable oGridTable)
		//{
		//    var obj = new FieldConfiguration.Data();
		//    var odt = new DataTable();
			
		//    //Check if the session instance of GridTable already exists
		//    //If not GetGridViewColumns for the particular entity and add it as Session instance "GridTable"
		//    if (oGridTable == null)
		//    {
		//        obj.SystemEntityTypeId = systemEntityTypeId;
		//        odt = FieldConfiguration.GetGridViewColumns(obj, auditId);				
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
		//            odt = FieldConfiguration.GetGridViewColumns(obj, auditId);
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

		public static string[] GetApplicationRoleColumns(string key, int auditId)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.ApplicationRole, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.ApplicationRole, auditId);

			//returnValues = new[] { "Application", "Name", "Description", "SortOrder", "ApplicationId", "Updated Date", "Updated By", "Last Action" };
					
			return returnValues;
		}

		public static int GetFieldConfigurationModeId(string Name, int auditId)
		{
            var data = new UserPreference.DomainModel.FieldConfigurationMode();
			data.Name = Name;
            var dt = UserPreference.FieldConfigurationMode.Search(data, auditId);

			if (dt.Rows.Count == 1)
                return int.Parse(dt.Rows[0][UserPreference.DomainModel.FieldConfigurationMode.DataColumns.FieldConfigurationModeId].ToString());
			else
				return -1;
		}

		public static string[] GetTimeZoneColumns(string key, int auditId)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.TimeZone, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.TimeZone, auditId);

			//returnValues = new[] { "Name", "Description", "SortOrder", "TimeDifference" };
					
			return returnValues;
		}
		public static string[] GetUserLoginHistoryColumns(string key, int auditId)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.UserLoginHistory, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.UserLoginHistory, auditId);

			//returnValues = new[] { "Name", "Description", "SortOrder", "TimeDifference" };

			return returnValues;
		}

		public static string[] GetCountryColumns(string key, int auditId)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.Country, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.Country, auditId);

			//returnValues = new[] { "Name", "TimeZone", "Description", "SortOrder" };
					
			return returnValues;
		}

		public static string[] GetHelpPageColumns(string key, int auditId)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.HelpPage, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.HelpPage, auditId);

			//returnValues = new[] { "Name", "Content", "SortOrder", "SystemEntityTypeId" };
					
			return returnValues;
		}

		public static string[] GetSuperKeyColumns(string key, int auditId)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.SuperKey, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.SuperKey, auditId);

			//returnValues = new[] { "Name", "Description", "SortOrder", "SystemEntityTypeId", "ExpirationDate" };
					
			return returnValues;
		}



		public static string[] GetSearchKeyColumns(string key, int auditId)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.SearchKey, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.SearchKey, auditId);

			//returnValues = new[] { "Name", "Description", "SortOrder", "SystemEntityTypeId", "ExpirationDate" };
					
			return returnValues;
		}

		
		public static string[] GetSearchKeyDetailColumns(string key, int auditId)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.SearchKeyDetail, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.SearchKeyDetail, auditId);

			//returnValues = new[] { "Name", "Description", "SortOrder", "SystemEntityTypeId", "ExpirationDate" };

			return returnValues;
		}

		public static string[] GetSearchKeyDetailItemColumns(string key, int auditId)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.SearchKeyDetailItem, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.SearchKeyDetailItem, auditId);

			//returnValues = new[] { "Name", "Description", "SortOrder", "SystemEntityTypeId", "ExpirationDate" };

			return returnValues;
		}

		public static string[] GetSuperKeyDetailColumns(string key, int auditId)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.SuperKeyDetail, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.SuperKeyDetail, auditId);

			//returnValues = new[] { "SuperKey", "EntityKey" };
					
			return returnValues;
		}

		public static string[] GetSystemEntityCategoryColumns(string key, int auditId)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.SystemEntityCategory, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.SystemEntityCategory, auditId);

			//returnValues = new[] { "Name", "Description", "SortOrder", "ApplicationId" };
					
			return returnValues;
		}

        public static string[] GetLog4NetColumns(string key, int auditId)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns((int)SystemEntity.Log4Net, AEFLModeId, auditId);
            else
                returnValues = GetGridViewColumns((int)SystemEntity.Log4Net, auditId);

            return returnValues;
        }

		public static string[] GetSystemDevNumbersColumns(string key, int auditId)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.SystemDevNumbers, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.SystemDevNumbers, auditId);

			//returnValues = new[] { "Person", "RangeFrom", "RangeTo", "ApplicationId", "Updated Date", "Updated By", "Last Action" };
					
			return returnValues;
		}

		public static string[] GetFieldConfigurationColumns(string key, int auditId)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.FieldConfiguration, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.FieldConfiguration, auditId);

			//returnValues = new[] { "Name", "Value", "SystemEntityTypeId", "Width", "Formatting", "ControlType", "HorizontalAlignment", "ApplicationId", "Updated Date", "Updated By", "Last Action" };
					
			return returnValues;
		}

		public static string[] GetFieldConfigurationModeColumns(string key, int auditId)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.FieldConfigurationMode, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.FieldConfigurationMode, auditId);

			//returnValues = new[] { "Name", "Description", "SortOrder" };
					
			return returnValues;
		}

		public static string[] GetApplicationColumns(string key, int auditId)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.Application, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.Application, auditId);

			//returnValues = new[] { "Name", "Description", "SortOrder", "Updated Date", "Updated By", "Last Action" };
					
			return returnValues;
		}

		public static string[] GetStoredProcedureLogColumns(string key, int auditId)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.StoredProcedureLog, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.StoredProcedureLog, auditId);

			//returnValues = new[] { "NameOfSP", "TimeOfExecution", "ExecutedBy", "SystemEntity" };
					
			return returnValues;
		}

		public static string[] GetStoredProcedureLogRawColumns(string key, int auditId)
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

		public static string[] GetStoredProcedureLogDetailColumns(string key, int auditId)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.StoredProcedureLogDetail, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.StoredProcedureLogDetail, auditId);

			//returnValues = new[] { "MasterId", "ParameterName", "ParameterValue" };
					
			return returnValues;
		}


		public static string[] GetApplicationEntityParentalHierarchyColumns(string key, int auditId)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.ApplicationEntityParentalHierarchy, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.ApplicationEntityParentalHierarchy, auditId);

			//returnValues = new[] { "Name", "Description", "SortOrder", "ApplicationId" };
					
			return returnValues;
		}

        public static string[] GetFieldConfigurationModeCategoryColumns(string key, int auditId) 
        {
            string[] returnValues;

            switch (key)
            {
                case "DBColumns":
                    {
                        returnValues = GetGridViewColumns((int)DataAccess.SystemEntity.ApplicationEntityParentalHierarchy, auditId);
                    }
                    break;
                default:
                    returnValues = new[] { "Name", "Description", "SortOrder", "ApplicationId" };
                    break;
            }

            return returnValues;
        }

        public static string[] GetApplicationModeColumns(string key, int auditId)
        {
            string[] returnValues;

            switch (key)
            {
                case "DBColumns":
                    {
                        returnValues = GetGridViewColumns((int)DataAccess.SystemEntity.ApplicationMode, auditId);
                    }
                    break;
                default:
                    returnValues = new[] { "Name", "Description", "SortOrder", "ApplicationId" };
                    break;
            }

            return returnValues;
        }

		public static string[] GetDateRangeTitleColumns(string key, int auditId)
		{
			string[] returnValues;

			switch (key)
			{
				case "DBColumns":
					{
						returnValues = GetGridViewColumns((int)DataAccess.SystemEntity.DateRangeTitle, auditId);
					}
					break;
				default:
					returnValues = new[] { "Name", "Description", "SortOrder", "ApplicationId" };
					break;
			}

			return returnValues;
		}

        public static string[] GetAEFLModeCategoryXApplicationModeXAEFLModeColumns(string key, int auditId)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns((int)SystemEntity.FieldConfigurationModeCategoryXFCMode, AEFLModeId, auditId);
            else
				returnValues = GetGridViewColumns((int)SystemEntity.FieldConfigurationModeCategoryXFCMode, auditId);

            return returnValues;
        }

		public static string[] GetApplicationOperationColumns(string key, int auditId)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.ApplicationOperation, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.ApplicationOperation, auditId);

			//returnValues = new[] { "Name", "Application", "SystemEntityType", "OperationValue", "Updated Date", "Updated By", "Last Action" };
					
			return returnValues;
		}

		public static string[] GetReleaseLogColumns(string key, int auditId)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.ReleaseLog, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.ReleaseLog, auditId);

			//returnValues = new[] { "Name", "VersionNo", "ReleaseDate", "Description", "SortOrder" };
								
			return returnValues;
		}

		public static string[] GetReleaseLogDetailColumns(string key, int auditId)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.ReleaseLogDetail, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.ReleaseLogDetail, auditId);

			//returnValues = new[] { "ReleaseLog", "ItemNo", "Description", "RequestedBy", "PrimaryDeveloper", "RequestedDate", "SortOrder" };
					
			return returnValues;
		}

		public static string[] GetSystemEntityTypeColumns(string key, int auditId)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.SystemEntityType, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.SystemEntityType, auditId);

			//returnValues = new[] { "EntityName", "EntityDescription", "NextValue", "ApplicationId" };
					
			return returnValues;
		}

		public static string[] GetMenuColumns(string key, int auditId)
		{
			string[] returnValues;

            if (key != "DBColumns")
            {
                var AEFLModeId = GetModeId(key);
                returnValues = GetGridViewColumns((int)SystemEntity.Menu, AEFLModeId, auditId);
            }
            else
            {
                returnValues = GetGridViewColumns((int)SystemEntity.Menu, auditId);
            }
			//returnValues = new[] { "Name", "ParentMenu", "NavigateURL", "Description", "SortOrder", "IsChecked", "IsVisible" };
					
			return returnValues;
		}
        public static string[] GetMenuCategoryColumns(string key, int auditId)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns((int)SystemEntity.MenuCategory, AEFLModeId, auditId);
            else
                returnValues = GetGridViewColumns((int)SystemEntity.MenuCategory, auditId);

            //returnValues = new[] { "Name", "ParentMenu", "NavigateURL", "Description", "SortOrder", "IsChecked", "IsVisible" };

            return returnValues;
        }
        
		#region Audit Module Entities

		public static string[] GetTypeOfIssueColumns(string key, int auditId)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.TypeOfIssue, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.TypeOfIssue, auditId);

			//returnValues = new[] { "Name", "Category", "Description", "SortOrder" };
					
			return returnValues;
		}

		public static string[] GetAuditActionColumns(string key, int auditId)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.AuditAction, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.AuditAction, auditId);

			//returnValues = new[] { "Name", "Description", "SortOrder" };
					
			return returnValues;
		}

		public static string[] GetAuditHistoryColumns(string key, int auditId)
		{
			string[] returnValues;

			switch (key)
			{
				case "DBColumns":
					{
						returnValues = GetGridViewColumns((int)DataAccess.SystemEntity.AuditHistory, auditId);
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

		public static string[] GetUserPreferenceDataTypeColumns(string key, int auditId)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.UserPreferenceDataType, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.UserPreferenceDataType, auditId);

			//returnValues = new[] { "Name", "Description", "SortOrder", "ApplicationId", "Updated Date", "Updated By", "Last Action" };
					
			return returnValues;
		}

		public static string[] GetUserPreferenceCategoryColumns(string key, int auditId)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.UserPreferenceCategory, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.UserPreferenceCategory, auditId);
			//returnValues = new[] { "Name", "Description", "SortOrder", "ApplicationId", "Updated Date", "Updated By", "Last Action" };
					
			return returnValues;
		}

		public static string[] GetUserPreferenceKeyColumns(string key, int auditId)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.UserPreferenceKey, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.UserPreferenceKey, auditId);

			//returnValues = new[] { "UserPreferenceDataType", "Name", "Value", "Description", "SortOrder", "ApplicationId", "Updated Date", "Updated By", "Last Action" };
					
			return returnValues;
		}

		public static string[] GetUserPreferenceColumns(string key, int auditId)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.UserPreference, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.UserPreference, auditId);

			//returnValues = new[] { "Application", "ApplicationUser", "UserPreferenceCategory", "UserPreferenceDataType", "UserPreferenceKey", "Value", "ApplicationId", "Updated Date", "Updated By", "Last Action" };
					
			return returnValues;
		}

		#endregion

		#region Import Module Entities

		public static string[] GetBatchFileStatusColumns(string key, int auditId)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.BatchFileStatus, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.BatchFileStatus, auditId);

			//returnValues = new[] { "Name", "Description", "SortOrder", "ApplicationId" };
					
			return returnValues;
		}

		public static string[] GetFileTypeColumns(string key, int auditId)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.FileType, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.FileType, auditId);

			//returnValues = new[] { "Name", "Description", "SortOrder", "ApplicationId" };
					
			return returnValues;
		}

		public static string[] GetBatchFileSetColumns(string key, int auditId)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.BatchFileSet, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.BatchFileSet, auditId);

			//returnValues = new[] { "Name", "Description", "CreatedDate", "CreatedByPerson", "ApplicationId" };
					
			return returnValues;
		}

		public static string[] GetBatchFileColumns(string key, int auditId)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.BatchFile, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.BatchFile, auditId);

			//returnValues = new[] { "SystemEntityType", "FileType", "BatchFileSet", "BatchFileStatus", "Name", "Folder", "BatchFile", "Description", "ApplicationId", "CreatedDate", "CreatedByPerson", "UpdatedDate", "UpdatedByPerson", "SortOrder" };
					
			return returnValues;
		}

		public static string[] GetBatchFileHistoryColumns(string key, int auditId)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.BatchFileHistory, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.BatchFileHistory, auditId);

			//returnValues = new[] { "BatchFileSet", "BatchFileStatus", "BatchFileId", "ApplicationId", "UpdatedDate", "UpdatedBy", "LastAction" };
					
			return returnValues;
		}

		#endregion


		public static string[] GetUserPreferenceSelectedItemColumns(string key, int auditId)
		{
			string[] returnValues;
            try
            {

                var AEFLModeId = GetModeId(key);

                if (AEFLModeId > 0)
                    returnValues = GetGridViewColumns((int)SystemEntity.UserPreferenceSelectedItem, AEFLModeId, auditId);
                else
                    returnValues = GetGridViewColumns((int)SystemEntity.UserPreferenceSelectedItem, auditId);
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

		public static string[] GetApplicationMonitoredEventEmailColumns(string key, int auditId)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.ApplicationMonitoredEventEmail, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.ApplicationMonitoredEventEmail, auditId);

			//returnValues = new[] { "ApplicationMonitoredEventSource", "User", "CorrespondenceLevel", "Active", "ApplicationId" };
					
			return returnValues;
		}

		public static string[] GetApplicationMonitoredEventColumns(string key, int auditId)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.ApplicationMonitoredEvent, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.ApplicationMonitoredEvent, auditId);

			//returnValues = new[] { "ApplicationMonitoredEventSource", "ApplicationMonitoredEventProcessingState", "ReferenceId", "ReferenceCode", "Category", "Message", "IsDuplicate", "ApplicationId", "LastModifiedBy", "LastModifiedOn" };
					
			return returnValues;
		}

		#region Event Monitoring Entities

		public static string[] GetApplicationMonitoredEventSourceColumns(string key, int auditId)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.ApplicationMonitoredEventSource, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.ApplicationMonitoredEventSource, auditId);

			//returnValues = new[] { "Code", "Description", "ApplicationId" };

			return returnValues;
		}

		public static string[] GetApplicationMonitoredEventProcessingStateColumns(string key, int auditId)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.ApplicationMonitoredEventProcessingState, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.ApplicationMonitoredEventProcessingState, auditId);

			//returnValues = new[] { "Code", "Description", "ApplicationId" };
					
			return returnValues;
		}


		#endregion


		#region Task Module Entities

		public static string[] GetTaskEntityTypeColumns(string key, int auditId)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.TaskEntityType, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.TaskEntityType, auditId);

			//returnValues = new[] { "ApplicationId", "Name", "Description", "Active", "SortOrder", "ApplicationId" };
					
			return returnValues;
		}

		public static string[] GetTaskScheduleTypeColumns(string key, int auditId)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.TaskScheduleType, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.TaskScheduleType, auditId);

			//returnValues = new[] { "ApplicationId", "Name", "Description", "Active", "SortOrder", "ApplicationId" };
					
			return returnValues;
		}

		public static string[] GetTaskEntityColumns(string key, int auditId)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.TaskEntity, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.TaskEntity, auditId);
			
			//returnValues = new[] { "ApplicationId", "TaskEntityType", "Name", "Description", "Active", "SortOrder", "ApplicationId" };
					
			return returnValues;
		}

		public static string[] GetTaskScheduleColumns(string key, int auditId)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.TaskSchedule, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.TaskSchedule, auditId);

			//returnValues = new[] { "ApplicationId", "TaskScheduleType", "TaskEntity", "ApplicationId" };
					
			return returnValues;
		}

		public static string[] GetTaskRunColumns(string key, int auditId)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.TaskRun, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.TaskRun, auditId);

			//returnValues = new[] { "ApplicationId", "TaskEntity", "TaskScheduleId", "BusinessDate", "StartTime", "EndTime", "RunBy", "ApplicationId" };
					
			return returnValues;
		}

		#endregion

		public static string[] GetPersonTitleColumns(string key, int auditId)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.PersonTitle, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.PersonTitle, auditId);

			//returnValues = new[] { "Name", "Description", "SortOrder" };
					
			return returnValues;
		}

		public static string[] GetApplicationUserTitleColumns(string key, int auditId)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.ApplicationUserTitle, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.ApplicationUserTitle, auditId);


			//returnValues = new[] { "Name", "Description", "SortOrder" };
					
			return returnValues;
		}

		public static string[] GetPersonColumns(string key, int auditId)
		{
			string[] returnValues;
            var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.Person, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.Person, auditId);

			//returnValues = new[] { "PersonTitle", "FirstName", "MiddleName", "LastName" };
					
			return returnValues;
		}

		public static string[] GetApplicationUserColumns(string key, int auditId)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.ApplicationUser, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.ApplicationUser, auditId);
					
			return returnValues;
		}

        public static string[] GetTabParentStructureColumns(string key, int auditId)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns((int)SystemEntity.TabParentStructure, AEFLModeId, auditId);
            else
                returnValues = GetGridViewColumns((int)SystemEntity.TabParentStructure, auditId);

            return returnValues;
        }

        public static string[] GetThemeCategoryColumns(string key, int auditId)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns((int)SystemEntity.ThemeCategory, AEFLModeId, auditId);
            else
                returnValues = GetGridViewColumns((int)SystemEntity.ThemeCategory, auditId);

            return returnValues;
        }

        public static string[] GetThemeKeyColumns(string key, int auditId)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns((int)SystemEntity.ThemeKey, AEFLModeId, auditId);
            else
                returnValues = GetGridViewColumns((int)SystemEntity.ThemeKey, auditId);

            return returnValues;
        }

        public static string[] GetThemeDetailsColumns(string key, int auditId)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns((int)SystemEntity.ThemeDetails, AEFLModeId, auditId);
            else
                returnValues = GetGridViewColumns((int)SystemEntity.ThemeDetails, auditId);

            return returnValues;
        }

        public static string[] GetThemesColumns(string key, int auditId)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns((int)SystemEntity.Themes, AEFLModeId, auditId);
            else
                returnValues = GetGridViewColumns((int)SystemEntity.Themes, auditId);

            return returnValues;
        }


        public static string[] GetTabChildStructureColumns(string key, int auditId)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns((int)SystemEntity.TabChildStructure, AEFLModeId, auditId);
            else
                returnValues = GetGridViewColumns((int)SystemEntity.TabChildStructure, auditId);

            return returnValues;
        }

        public static string[] GetHelpPageContextColumns(string key, int auditId)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns((int)SystemEntity.HelpPageContext, AEFLModeId, auditId);
            else
                returnValues = GetGridViewColumns((int)SystemEntity.HelpPageContext, auditId);

            return returnValues;
        }

        public static string[] GetUserLoginStatusColumns(string key, int auditId)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns((int)SystemEntity.UserLoginStatus, AEFLModeId, auditId);
            else
                returnValues = GetGridViewColumns((int)SystemEntity.UserLoginStatus, auditId);

            return returnValues;
        }

        public static string[] GetUserLoginColumns(string key, int auditId)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns((int)SystemEntity.UserLogin, AEFLModeId, auditId);
            else
                returnValues = GetGridViewColumns((int)SystemEntity.UserLogin, auditId);

            return returnValues;
        }

        public static string[] GetQuickPaginationRunColumns(string key, int auditId)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns((int)SystemEntity.QuickPaginationRun, AEFLModeId, auditId);
            else
                returnValues = GetGridViewColumns((int)SystemEntity.QuickPaginationRun, auditId);

            return returnValues;
        }

        public static string[] GetLanguageColumns(string key, int auditId)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns((int)SystemEntity.Language, AEFLModeId, auditId);
            else
                returnValues = GetGridViewColumns((int)SystemEntity.Language, auditId);

            return returnValues;
        }

        public static string[] GetModuleColumns(string key, int auditId)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns((int)SystemEntity.Module, AEFLModeId, auditId);
            else
                returnValues = GetGridViewColumns((int)SystemEntity.Module, auditId);

            return returnValues;
        }

        public static string[] GetDeveloperRoleColumns(string key, int auditId)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns((int)SystemEntity.DeveloperRole, AEFLModeId, auditId);
            else
                returnValues = GetGridViewColumns((int)SystemEntity.DeveloperRole, auditId);

            return returnValues;
        }

        public static string[] GetFeatureOwnerStatusColumns(string key, int auditId)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns((int)SystemEntity.FeatureOwnerStatus, AEFLModeId, auditId);
            else
                returnValues = GetGridViewColumns((int)SystemEntity.FeatureOwnerStatus, auditId);

            return returnValues;
        }

        public static string[] GetModuleOwnerColumns(string key, int auditId)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns((int)SystemEntity.ModuleOwner, AEFLModeId, auditId);
            else
                returnValues = GetGridViewColumns((int)SystemEntity.ModuleOwner, auditId);

            return returnValues;
        }

        public static string[] GetEntityOwnerColumns(string key, int auditId)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns((int)SystemEntity.EntityOwner, AEFLModeId, auditId);
            else
                returnValues = GetGridViewColumns((int)SystemEntity.EntityOwner, auditId);

            return returnValues;
        }

        public static string[] GetFunctionalityOwnerColumns(string key, int auditId)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns((int)SystemEntity.FunctionalityOwner, AEFLModeId, auditId);
            else
                returnValues = GetGridViewColumns((int)SystemEntity.FunctionalityOwner, auditId);

            return returnValues;
        }

        public static string[] GetTraceColumns(string key, int auditId)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns((int)SystemEntity.Trace, AEFLModeId, auditId);
            else
                returnValues = GetGridViewColumns((int)SystemEntity.Trace, auditId);

            return returnValues;
        }

        public static string[] GetApplicationUserProfileImageColumns(string key, int auditId)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns((int)SystemEntity.ApplicationUserProfileImage, AEFLModeId, auditId);
            else
                returnValues = GetGridViewColumns((int)SystemEntity.ApplicationUserProfileImage, auditId);

            return returnValues;
        }

        public static string[] GetApplicationUserProfileImageMasterColumns(string key, int auditId)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns((int)SystemEntity.ApplicationUserProfileImageMaster, AEFLModeId, auditId);
            else
                returnValues = GetGridViewColumns((int)SystemEntity.ApplicationUserProfileImageMaster, auditId);

            return returnValues;
        }

		public static string[] GetApplicationRouteColumns(string key, int auditId)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns((int)SystemEntity.ApplicationRoute, AEFLModeId, auditId);
            else
                returnValues = GetGridViewColumns((int)SystemEntity.ApplicationRoute, auditId);

            return returnValues;
        }
		
		 public static string[] GetSystemForeignRelationshipColumns(string key, int auditId)
		 {
			 string[] returnValues;
			 var AEFLModeId = GetModeId(key);

			 if (AEFLModeId > 0)
				 returnValues = GetGridViewColumns((int)SystemEntity.SystemForeignRelationship, AEFLModeId, auditId);
			 else
				 returnValues = GetGridViewColumns((int)SystemEntity.SystemForeignRelationship, auditId);

			 return returnValues;
		 }

		 public static string[] GetAllEntityDetailColumns(string key, int auditId)
		 {
			 string[] returnValues;
			 var AEFLModeId = GetModeId(key);

			 if (AEFLModeId > 0)
				 returnValues = GetGridViewColumns((int)SystemEntity.AllEntityDetail, AEFLModeId, auditId);
			 else
				 returnValues = GetGridViewColumns((int)SystemEntity.AllEntityDetail, auditId);

			 return returnValues;
		 }

		 public static string[] GetApplicationRouteParameterColumns(string key, int auditId)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns((int)SystemEntity.ApplicationRouteParameter, AEFLModeId, auditId);
            else
                returnValues = GetGridViewColumns((int)SystemEntity.ApplicationRouteParameter, auditId);

            return returnValues;
        }

		 public static string[] GetSystemForeignRelationshipTypeColumns(string key, int auditId)
		 {
			 string[] returnValues;
			 var AEFLModeId = GetModeId(key);

			 if (AEFLModeId > 0)
				 returnValues = GetGridViewColumns((int)SystemEntity.SystemForeignRelationshipType, AEFLModeId, auditId);
			 else
				 returnValues = GetGridViewColumns((int)SystemEntity.SystemForeignRelationshipType, auditId);

			 return returnValues;
		 }
        #region QA Entities

        public static string[] GetFunctionalityColumns(string key, int auditId)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns((int)SystemEntity.Functionality, AEFLModeId, auditId);
            else
                returnValues = GetGridViewColumns((int)SystemEntity.Functionality, auditId);

            //returnValues = new[] { "FirstName", "MiddleName", "LastName", "Application" };

            return returnValues;
        }

		public static string[] GetFunctionalityXFunctionalityActiveStatusColumns(string key, int auditId)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.FunctionalityXFunctionalityActiveStatus, AEFLModeId, auditId);
            else
				returnValues = GetGridViewColumns((int)SystemEntity.FunctionalityXFunctionalityActiveStatus, auditId);

            //returnValues = new[] { "FirstName", "MiddleName", "LastName", "Application" };

            return returnValues;
        }
		

        public static string[] GetFunctionalityStatusColumns(string key, int auditId)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns((int)SystemEntity.FunctionalityStatus, AEFLModeId, auditId);
            else
                returnValues = GetGridViewColumns((int)SystemEntity.FunctionalityStatus, auditId);

            //returnValues = new[] { "FirstName", "MiddleName", "LastName", "Application" };

            return returnValues;
        }

		public static string[] GetFunctionalityActiveStatusColumns(string key, int auditId)
		{
			string[] returnValues;
            if (key == "DBColumns") 
			{ 
				key = "-1"; 
			} 
 			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.FunctionalityActiveStatus, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.FunctionalityActiveStatus, auditId);

			//returnValues = new[] { "FirstName", "MiddleName", "LastName", "Application" };

			return returnValues;
		}

        public static string[] GetFunctionalityPriorityColumns(string key, int auditId)
        {
            string[] returnValues;
            if (key != "DBColumns")
            {
                var AEFLModeId = GetModeId(key);
                returnValues = GetGridViewColumns((int)SystemEntity.FunctionalityPriority, AEFLModeId, auditId);
            }
            else
            {
                returnValues = GetGridViewColumns((int)SystemEntity.FunctionalityPriority, auditId);
            }
          

            //returnValues = new[] { "FirstName", "MiddleName", "LastName", "Application" };

            return returnValues;
        }
		
        public static string[] GetFunctionalityImageColumns(string key, int auditId)
        {
            string[] returnValues;
            if (key != "DBColumns")
            {
                var AEFLModeId = GetModeId(key);
                returnValues = GetGridViewColumns((int)SystemEntity.FunctionalityImage, AEFLModeId, auditId);
            }
            else
            {
                returnValues = GetGridViewColumns((int)SystemEntity.FunctionalityImage, auditId);
            }


            return returnValues;
        }

		public static string[] GetFunctionalityImageAttributeColumns(string key, int auditId)
        {
            string[] returnValues;
            if (key != "DBColumns")
            {
                var AEFLModeId = GetModeId(key);
                returnValues = GetGridViewColumns((int)SystemEntity.FunctionalityImageAttribute, AEFLModeId, auditId);
            }
            else
            {
                returnValues = GetGridViewColumns((int)SystemEntity.FunctionalityImageAttribute, auditId);
            }


            return returnValues;
        }

		public static string[] GetFunctionalityImageXFunctionalityImageAttributeColumns(string key, int auditId)
		{
			string[] returnValues;
			if (key != "DBColumns")
			{
				var AEFLModeId = GetModeId(key);
				returnValues = GetGridViewColumns((int)SystemEntity.FunctionalityImageXFunctionalityImageAttribute, AEFLModeId, auditId);
			}
			else
			{
				returnValues = GetGridViewColumns((int)SystemEntity.FunctionalityImageXFunctionalityImageAttribute, auditId);
			}


			return returnValues;
		}
				
		public static string[] GetFunctionalityXFunctionalityImageColumns(string key, int auditId)
		{
			string[] returnValues;
			if (key != "DBColumns")
			{
				var AEFLModeId = GetModeId(key);
				returnValues = GetGridViewColumns((int)SystemEntity.FunctionalityXFunctionalityImage, AEFLModeId, auditId);
			}
			else
			{
				returnValues = GetGridViewColumns((int)SystemEntity.FunctionalityXFunctionalityImage, auditId);
			}


			return returnValues;
		}


        public static string[] GetFunctionalityEntityStatusColumns(string key, int auditId)
        {
            string[] returnValues;
            if (key != "DBColumns")
            {
                var AEFLModeId = GetModeId(key);
                returnValues = GetGridViewColumns((int)SystemEntity.FunctionalityEntityStatus, AEFLModeId, auditId);
            }
            else
            {
                returnValues = GetGridViewColumns((int)SystemEntity.FunctionalityEntityStatus, auditId);
            }

            //returnValues = new[] { "FirstName", "MiddleName", "LastName", "Application" };

            return returnValues;
        }

        public static string[] GetFunctionalityEntityStatusArchiveColumns(string key, int auditId)
        {
            string[] returnValues;
            if (key != "DBColumns")
            {
                var AEFLModeId = GetModeId(key);
                returnValues = GetGridViewColumns((int)SystemEntity.FunctionalityEntityStatusArchive, AEFLModeId, auditId);
            }
            else
            {
                returnValues = GetGridViewColumns((int)SystemEntity.FunctionalityEntityStatusArchive, auditId);
            }

            //returnValues = new[] { "FirstName", "MiddleName", "LastName", "Application" };

            return returnValues;
        }

        public static string[] GetMilestoneXFeatureArchiveColumns(string key, int auditId)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns((int)SystemEntity.MilestoneXFeatureArchive, AEFLModeId, auditId);
            else
                returnValues = GetGridViewColumns((int)SystemEntity.MilestoneXFeatureArchive, auditId);

            //returnValues = new[] { "FirstName", "MiddleName", "LastName", "Application" };

            return returnValues;
        }

		public static string[] GetReleaseLogStatusColumns(string key, int auditId)
		{
			string[] returnValues;
			var AEFLModeId = GetModeId(key);

			if (AEFLModeId > 0)
				returnValues = GetGridViewColumns((int)SystemEntity.ReleaseLogStatus, AEFLModeId, auditId);
			else
				returnValues = GetGridViewColumns((int)SystemEntity.ReleaseLogStatus, auditId);

			//returnValues = new[] { "Name", "VersionNo", "ReleaseDate", "Description", "SortOrder" };

			return returnValues;
		}

        public static string[] GetIssueTypeReleaseColumns(string key, int auditId)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns((int)SystemEntity.ReleaseIssueType, AEFLModeId, auditId);
            else
                returnValues = GetGridViewColumns((int)SystemEntity.ReleaseIssueType, auditId);

            //returnValues = new[] { "Name", "VersionNo", "ReleaseDate", "Description", "SortOrder" };

            return returnValues;
        }

        public static string[] GetReleasePublishCategoryColumns(string key, int auditId)
        {
            string[] returnValues;
            var AEFLModeId = GetModeId(key);

            if (AEFLModeId > 0)
                returnValues = GetGridViewColumns((int)SystemEntity.ReleasePublishCategory, AEFLModeId, auditId);
            else
                returnValues = GetGridViewColumns((int)SystemEntity.ReleasePublishCategory, auditId);

            //returnValues = new[] { "Name", "VersionNo", "ReleaseDate", "Description", "SortOrder" };

            return returnValues;
        }       
        #endregion


    }
}
