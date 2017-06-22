using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using System.Reflection;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.Framework.Configuration;
using System.Runtime.CompilerServices;
using DataModel.Framework.Core;
using Framework.Components.Core;
using Framework.CommonServices.BusinessDomain.Utils;

namespace Framework.Components.UserPreference
{
    public partial class FieldConfigurationDataManager : BaseDataManager
    {
        static readonly string DataStoreKey = "";

        static FieldConfigurationDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("FieldConfiguration");
        }

        #region ToSqlParameter

        public static string ToSQLParameter(FieldConfigurationDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";

            switch (dataColumnName)
            {

                case FieldConfigurationDataModel.DataColumns.FieldConfigurationId:
                    if (data.FieldConfigurationId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FieldConfigurationDataModel.DataColumns.FieldConfigurationId, data.FieldConfigurationId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationDataModel.DataColumns.FieldConfigurationId);
                    }
                    break;

                case FieldConfigurationDataModel.DataColumns.FieldConfigurationModeId:
                    if (data.FieldConfigurationModeId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FieldConfigurationDataModel.DataColumns.FieldConfigurationModeId, data.FieldConfigurationModeId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationDataModel.DataColumns.FieldConfigurationModeId);
                    }
                    break;

                case FieldConfigurationDataModel.DataColumns.Name:
                    if (!string.IsNullOrEmpty(data.Name))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FieldConfigurationDataModel.DataColumns.Name, data.Name);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationDataModel.DataColumns.Name);

                    }
                    break;

				case FieldConfigurationDataModel.DataColumns.ApplicationId:
					if (data.FieldConfigurationModeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FieldConfigurationDataModel.DataColumns.ApplicationId, data.ApplicationId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationDataModel.DataColumns.ApplicationId);
					}
					break;

                case FieldConfigurationDataModel.DataColumns.FieldConfigurationDisplayName:
                    if (!string.IsNullOrEmpty(data.FieldConfigurationDisplayName))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FieldConfigurationDataModel.DataColumns.FieldConfigurationDisplayName, data.FieldConfigurationDisplayName);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationDataModel.DataColumns.FieldConfigurationDisplayName);
                    }
                    break;

                case FieldConfigurationDataModel.DataColumns.Value:
                    if (!string.IsNullOrEmpty(data.Value))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FieldConfigurationDataModel.DataColumns.Value, data.Value);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationDataModel.DataColumns.Value);

                    }
                    break;

                case FieldConfigurationDataModel.DataColumns.SystemEntityTypeId:
                    if (data.SystemEntityTypeId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FieldConfigurationDataModel.DataColumns.SystemEntityTypeId, data.SystemEntityTypeId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationDataModel.DataColumns.SystemEntityTypeId);
                    }
                    break;

                case FieldConfigurationDataModel.DataColumns.Width:
                    if (!string.IsNullOrEmpty(data.Value))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FieldConfigurationDataModel.DataColumns.Width, data.Width);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationDataModel.DataColumns.Width);

                    }
                    break;

                case FieldConfigurationDataModel.DataColumns.Formatting:
                    if (!string.IsNullOrEmpty(data.Formatting))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FieldConfigurationDataModel.DataColumns.Formatting, data.Formatting);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FieldConfigurationDataModel.DataColumns.Formatting, data.Formatting);

                    }
                    break;

                case FieldConfigurationDataModel.DataColumns.ControlType:
                    if (!string.IsNullOrEmpty(data.ControlType))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FieldConfigurationDataModel.DataColumns.ControlType, data.ControlType);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationDataModel.DataColumns.ControlType);

                    }
                    break;

                case FieldConfigurationDataModel.DataColumns.HorizontalAlignment:
                    if (!string.IsNullOrEmpty(data.Value))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FieldConfigurationDataModel.DataColumns.HorizontalAlignment, data.HorizontalAlignment);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationDataModel.DataColumns.HorizontalAlignment);

                    }
                    break;

                case FieldConfigurationDataModel.DataColumns.GridViewPriority:
                    if (data.GridViewPriority != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FieldConfigurationDataModel.DataColumns.GridViewPriority, data.GridViewPriority);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationDataModel.DataColumns.GridViewPriority);
                    }
                    break;

                case FieldConfigurationDataModel.DataColumns.DetailsViewPriority:
                    if (data.DetailsViewPriority != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FieldConfigurationDataModel.DataColumns.DetailsViewPriority, data.DetailsViewPriority);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationDataModel.DataColumns.DetailsViewPriority);
                    }
                    break;

                case FieldConfigurationDataModel.DataColumns.DisplayColumn:
                    if (data.DisplayColumn != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FieldConfigurationDataModel.DataColumns.DisplayColumn, data.DisplayColumn);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationDataModel.DataColumns.DisplayColumn);
                    }
                    break;

                case FieldConfigurationDataModel.DataColumns.CellCount:
                    if (data.CellCount != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FieldConfigurationDataModel.DataColumns.CellCount, data.CellCount);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationDataModel.DataColumns.CellCount);
                    }
                    break;
            }
            return returnValue;
        }

        #endregion

        #region GetList

        public static List<FieldConfigurationDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(FieldConfigurationDataModel.Empty, requestProfile, 0);
        }

        #endregion

        #region GetDetails

        public static FieldConfigurationDataModel GetDetails(FieldConfigurationDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
        }

        public static List<FieldConfigurationDataModel> GetFieldConfigurationList(FieldConfigurationDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.FieldConfigurationSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                ", " + ToSQLParameter(data, FieldConfigurationDataModel.DataColumns.FieldConfigurationId) +
                ", " + ToSQLParameter(data, FieldConfigurationDataModel.DataColumns.FieldConfigurationModeId) +
                ", " + ToSQLParameter(data, FieldConfigurationDataModel.DataColumns.SystemEntityTypeId) +
                ", " + ToSQLParameter(data, FieldConfigurationDataModel.DataColumns.DisplayColumn) +
                ", " + ToSQLParameter(data, FieldConfigurationDataModel.DataColumns.Name);

            var result = new List<FieldConfigurationDataModel>();

            using (var reader = new DBDataReader("Get List", sql, DataStoreKey))
            {
                var dbReader = reader.DBReader;

                while (dbReader.Read())
                {
                    var dataItem = new FieldConfigurationDataModel();

                    dataItem.Name                          = dbReader[FieldConfigurationDataModel.DataColumns.Name].ToString();
                    dataItem.FieldConfigurationDisplayName = (string)dbReader[FieldConfigurationDataModel.DataColumns.FieldConfigurationDisplayName];
                    dataItem.Value                         = (string)dbReader[FieldConfigurationDataModel.DataColumns.Value];

                    result.Add(dataItem);
                }
            }

            return result;
        }

        public static List<FieldConfigurationDataModel> GetEntityDetails(FieldConfigurationDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
        {
            const string sql = @"dbo.FieldConfigurationSearch ";

            var parameters =
            new
            {
                    AuditId                  = requestProfile.AuditId
                //,   ApplicationId            = dataQuery.ApplicationId
                ,   ApplicationId            = requestProfile.ApplicationId
                ,   ApplicationMode          = requestProfile.ApplicationModeId
                ,   FieldConfigurationId     = dataQuery.FieldConfigurationId
                ,   SystemEntityTypeId       = dataQuery.SystemEntityTypeId
                ,   FieldConfigurationModeId = dataQuery.FieldConfigurationModeId
                ,   DisplayColumn            = dataQuery.DisplayColumn
                ,   CellCount                = dataQuery.CellCount
                ,   Name                     = dataQuery.Name
                ,   ReturnAuditInfo          = returnAuditInfo

            };

            List<FieldConfigurationDataModel> result;

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                result = dataAccess.Connection.Query<FieldConfigurationDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
            }

            return result;
        }

        #endregion

        #region GetGridViewColumns

        public static DataTable GetDetailsViewColumns(FieldConfigurationDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.ToDataTable();
        }

        #endregion

        #region Create

        public static int Create(FieldConfigurationDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Create");
            var fieldConfigurationId = DBDML.RunScalarSQL("FieldConfiguration.Insert", sql, DataStoreKey);
            return Convert.ToInt32(fieldConfigurationId);
        }

        #endregion

        #region Update

        public static void Update(FieldConfigurationDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Update");
            DBDML.RunSQL("FieldConfiguration.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

        public static void Delete(FieldConfigurationDataModel data, RequestProfile requestProfile)
        {
            const string sql = @"dbo.FieldConfigurationDelete ";

            var parameters = new
            {
                AuditId = requestProfile.AuditId
                ,
                FieldConfigurationId = data.FieldConfigurationId
            };

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {


                dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


            }
        }

        #endregion

        #region Search

        public static DataTable Search(FieldConfigurationDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile, 0);

            var table = list.ToDataTable();

            return table;
        }

        #endregion

		#region Check Default View

		public static DataTable CheckDefaultView(FieldConfigurationDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.CheckDefaultViewList" +

				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, data.ApplicationId) +
				", " + ToSQLParameter(data, FieldConfigurationDataModel.DataColumns.FieldConfigurationModeId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);				

			var oDT = new DataAccess.DBDataTable("CheckDefaultView", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

        #region Save
		
        private static string Save(FieldConfigurationDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

			//JIRA# TimeEnt-405
			//FormatNumericalData(data, requestProfile);

            switch (action)
            {
                case "Create":
                    sql += "dbo.FieldConfigurationInsert  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
                    break;

                case "Update":
                    sql += "dbo.FieldConfigurationUpdate  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
                    break;
            }

            sql = sql + ", " + ToSQLParameter(data, FieldConfigurationDataModel.DataColumns.FieldConfigurationId) +
				 ", " + ToSQLParameter(data, FieldConfigurationDataModel.DataColumns.ApplicationId) +
                  ", " + ToSQLParameter(data, FieldConfigurationDataModel.DataColumns.Name) +
                  ", " + ToSQLParameter(data, FieldConfigurationDataModel.DataColumns.Value) +
                  ", " + ToSQLParameter(data, FieldConfigurationDataModel.DataColumns.SystemEntityTypeId) +
                  ", " + ToSQLParameter(data, FieldConfigurationDataModel.DataColumns.Width) +
                  ", " + ToSQLParameter(data, FieldConfigurationDataModel.DataColumns.Formatting) +
                  ", " + ToSQLParameter(data, FieldConfigurationDataModel.DataColumns.ControlType) +
                  ", " + ToSQLParameter(data, FieldConfigurationDataModel.DataColumns.HorizontalAlignment) +
                  ", " + ToSQLParameter(data, FieldConfigurationDataModel.DataColumns.GridViewPriority) +
                  ", " + ToSQLParameter(data, FieldConfigurationDataModel.DataColumns.DetailsViewPriority) +
                  ", " + ToSQLParameter(data, FieldConfigurationDataModel.DataColumns.DisplayColumn) +
                  ", " + ToSQLParameter(data, FieldConfigurationDataModel.DataColumns.CellCount) +
                  ", " + ToSQLParameter(data, FieldConfigurationDataModel.DataColumns.FieldConfigurationModeId);
            return sql;
        }

		private static void FormatNumericalData(FieldConfigurationDataModel data, RequestProfile requestProfile)
		{
			Type type = null;

			var dataSE = new SystemEntityTypeDataModel();
			dataSE.SystemEntityTypeId = data.SystemEntityTypeId;			

			var dt = SystemEntityTypeDataManager.GetEntityDetails(dataSE, requestProfile);

			var entityModel = dt[0].EntityName + "DataModel";
			
			var currentAssembly = Assembly.GetExecutingAssembly();
			var assemblyNames = currentAssembly.GetReferencedAssemblies();

			foreach (var aName in assemblyNames)
			{
				try
				{
					var assembly = Assembly.Load(aName);
					type = assembly.GetTypes().First(t => t.Name == entityModel);
					var objInstance = Activator.CreateInstance(type);
					var objType = objInstance.GetType();

					var objProps = objType.GetProperties();
					
					foreach (var propInfo in objProps)
					{
						if (propInfo.Name.Equals(data.Name))
						{
							if (propInfo.PropertyType == typeof(int?) || propInfo.PropertyType == typeof(DateTime))
							{
								data.HorizontalAlignment = "Right";
							}
						}

					}

				}
				catch { }
			}			
		}


        #endregion

        #region DoesExist

        public static bool DoesExist(FieldConfigurationDataModel data, RequestProfile requestProfile)
        {
            var doesExistRequest = new FieldConfigurationDataModel();
            doesExistRequest.Name = data.Name;
            doesExistRequest.FieldConfigurationModeId = data.FieldConfigurationModeId;
            doesExistRequest.SystemEntityTypeId = data.SystemEntityTypeId;
            doesExistRequest.ApplicationId = requestProfile.ApplicationId;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);
            return list.Count > 0;
        }

        #endregion

        #region GetChildren

        private static DataSet GetChildren(FieldConfigurationDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.FieldConfigurationChildrenGet " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(data, FieldConfigurationDataModel.DataColumns.FieldConfigurationId);

            var oDT = new DBDataSet("Get Children", sql, DataStoreKey);
            return oDT.DBDataset;
        }

        #endregion

        #region IsDeletable

        public static bool IsDeletable(FieldConfigurationDataModel data, RequestProfile requestProfile)
        {
            var isDeletable = true;
            var ds = GetChildren(data, requestProfile);
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataTable dt in ds.Tables)
                {
                    if (dt.Rows.Count > 0)
                    {
                        isDeletable = false;
                        break;
                    }
                }
            }
            return isDeletable;
        }

        #endregion

        public static void Sync(int newApplicationId, int oldApplicationId, RequestProfile requestProfile, int languageId)
        {
            // get all records for old Application Id
            var sql = "EXEC dbo.FieldConfigurationSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                //", @" + FieldConfigurationDataModel.DataColumns.SystemEntityTypeId + "=10400" +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, oldApplicationId);

            var oDT = new DBDataTable("FieldConfiguration.Search", sql, DataStoreKey);

            // formaulate a new request Profile which will have new Applicationid
            var newRequestProfile = new RequestProfile();
            newRequestProfile.ApplicationId = newApplicationId;
            newRequestProfile.AuditId = requestProfile.AuditId;

            var fcModeData = new FieldConfigurationModeDataModel();
            var fcModeList = FieldConfigurationModeDataManager.GetEntityDetails(fcModeData, newRequestProfile, 0);

            foreach (DataRow dr in oDT.DBTable.Rows)
            {
                var fcModeName = dr[FieldConfigurationDataModel.DataColumns.FieldConfigurationMode].ToString();

                var fcRecord = fcModeList.Find(x => x.Name == fcModeName && x.ApplicationId == newApplicationId);
                    
                if(fcRecord != null)
                {
                    //get new fc mode id based on fc mode name
                    var newFCModeId = fcRecord.FieldConfigurationModeId;

                    var data = new FieldConfigurationDataModel();
                    data.ApplicationId = newApplicationId;
                    data.Name = dr[FieldConfigurationDataModel.DataColumns.Name].ToString();
                    data.SystemEntityTypeId = Convert.ToInt32(dr[FieldConfigurationDataModel.DataColumns.SystemEntityTypeId]);
                    data.FieldConfigurationModeId = newFCModeId;

                    // check for existing record in new Application Id
                    if (!DoesExist(data, newRequestProfile))
                    {
                        data.Value = dr[FieldConfigurationDataModel.DataColumns.Value].ToString();
                        data.Width = Convert.ToDecimal(dr[FieldConfigurationDataModel.DataColumns.Width]);
                        data.Formatting = dr[FieldConfigurationDataModel.DataColumns.Formatting].ToString();
                        data.ControlType = dr[FieldConfigurationDataModel.DataColumns.ControlType].ToString();
                        data.HorizontalAlignment = dr[FieldConfigurationDataModel.DataColumns.HorizontalAlignment].ToString();
                        data.GridViewPriority = Convert.ToInt32(dr[FieldConfigurationDataModel.DataColumns.GridViewPriority]);
                        data.DetailsViewPriority = Convert.ToInt32(dr[FieldConfigurationDataModel.DataColumns.DetailsViewPriority]);
                        data.DisplayColumn = Convert.ToInt32(dr[FieldConfigurationDataModel.DataColumns.DisplayColumn]);
                        data.CellCount = Convert.ToInt32(dr[FieldConfigurationDataModel.DataColumns.CellCount]);

                        if (data.Formatting == "'")
                        {
                            data.Formatting = " ";
                        }
                        //create in new application id
                        var newfcId = Create(data, newRequestProfile);

                        var dataDisplayName = new FieldConfigurationDisplayNameDataModel();
                        dataDisplayName.FieldConfigurationId = newfcId;
                        dataDisplayName.Value = dr[FieldConfigurationDataModel.DataColumns.FieldConfigurationDisplayName].ToString();
                        dataDisplayName.LanguageId = languageId;
                        dataDisplayName.IsDefault = 1;

                        // create display name entry according to the default display name
                        FieldConfigurationDisplayNameDataManager.Create(dataDisplayName, newRequestProfile);
                    }
                }

            }
        }

    }
}
