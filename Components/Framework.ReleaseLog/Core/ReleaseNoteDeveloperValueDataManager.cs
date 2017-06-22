﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Framework.Components.DataAccess;
using Framework.Components.ReleaseLog.DomainModel;
using System.Data;
using DataModel.Framework.DataAccess;

namespace Framework.Components.ReleaseLog
{
	public partial class ReleaseNoteDeveloperValueDataManager : StandardDataManager
	{
        static readonly string DataStoreKey = "";

		static ReleaseNoteDeveloperValueDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("ReleaseNoteDeveloperValue");
		}

		public static string ToSQLParameter(ReleaseNoteDeveloperValueDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case ReleaseNoteDeveloperValueDataModel.DataColumns.ReleaseNoteDeveloperValueId:
					if (data.ReleaseNoteDeveloperValueId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ReleaseNoteDeveloperValueDataModel.DataColumns.ReleaseNoteDeveloperValueId, data.ReleaseNoteDeveloperValueId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseNoteDeveloperValueDataModel.DataColumns.ReleaseNoteDeveloperValueId);
					}
					break;

				case ReleaseNoteDeveloperValueDataModel.DataColumns.DateCreated:
					if (data.DateCreated != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ReleaseNoteDeveloperValueDataModel.DataColumns.DateCreated, data.DateCreated);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseNoteDeveloperValueDataModel.DataColumns.DateCreated);
					}
					break;
				case ReleaseNoteDeveloperValueDataModel.DataColumns.DateModified:
					if (data.DateModified != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ReleaseNoteDeveloperValueDataModel.DataColumns.DateModified, data.DateModified);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ReleaseNoteDeveloperValueDataModel.DataColumns.DateModified);

					}
					break;

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

		#region GetList

        public static List<ReleaseNoteDeveloperValueDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(ReleaseNoteDeveloperValueDataModel.Empty, requestProfile, 0);
		}

		#endregion GetList

		#region Search

		public static DataTable Search(ReleaseNoteDeveloperValueDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion Search

		#region GetEntitySearch

		static public List<ReleaseNoteDeveloperValueDataModel> GetEntitySearch(ReleaseNoteDeveloperValueDataModel obj, RequestProfile requestProfile)
		{
            return GetEntityDetails(obj, requestProfile, 0);
		}

		#endregion

		#region GetDetails

        public static ReleaseNoteDeveloperValueDataModel GetDetails(ReleaseNoteDeveloperValueDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
		}

		public static List<ReleaseNoteDeveloperValueDataModel> GetEntityDetails(ReleaseNoteDeveloperValueDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.ReleaseNoteDeveloperValueSearch ";

			var parameters =
			new
			{
				    AuditId                                 = requestProfile.AuditId
				,   ReleaseNoteDeveloperValueId             = dataQuery.ReleaseNoteDeveloperValueId
				,   Name                                    = dataQuery.Name
				,   Description                             = dataQuery.Description
				,   ApplicationId                           = requestProfile.ApplicationId
				,   ReturnAuditInfo                         = returnAuditInfo
			};

			List<ReleaseNoteDeveloperValueDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<ReleaseNoteDeveloperValueDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		//public static List<ReleaseNoteDeveloperValueDataModel> GetEntityDetails(ReleaseNoteDeveloperValueDataModel data, RequestProfile requestProfile)
		//{
		//	var dt = GetDetails(data, requestProfile.AuditId);

		//	var list = ToList(dt);

		//	return list;
		//}

		#endregion GetDetails

		#region CreateOrUpdate

		private static string CreateOrUpdate(ReleaseNoteDeveloperValueDataModel data, RequestProfile requestProfile, string action)
		{

			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.ReleaseNoteDeveloperValueInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.ReleaseNoteDeveloperValueUpdate  " + "\r\n" +
						   " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}

			sql = sql + ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
						 ", " + ToSQLParameter(data, ReleaseNoteDeveloperValueDataModel.DataColumns.ReleaseNoteDeveloperValueId) +
						 ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
						 ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

			return sql;
		}

		#endregion CreateOrUpdate

		#region Create

		public static int Create(ReleaseNoteDeveloperValueDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");

			var ReleaseNoteDeveloperValueId = DBDML.RunScalarSQL("ReleaseNoteDeveloperValue.Insert", sql, DataStoreKey);
			return Convert.ToInt32(ReleaseNoteDeveloperValueId);
		}

		#endregion Create

		#region Update

		public static void Update(ReleaseNoteDeveloperValueDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");

			DBDML.RunSQL("ReleaseNoteDeveloperValue.Update", sql, DataStoreKey);
		}

		#endregion Update

		#region Delete

		public static void Delete(ReleaseNoteDeveloperValueDataModel data, RequestProfile requestProfile)
		{
			const string sql = @"dbo.ReleaseNoteDeveloperValueDelete ";

			var parameters = new
			{
				AuditId = requestProfile.AuditId
				,
				ReleaseNoteDeveloperValueId = data.ReleaseNoteDeveloperValueId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion Delete

		#region DoesExist

		public static bool DoesExist(ReleaseNoteDeveloperValueDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new ReleaseNoteDeveloperValueDataModel();
			doesExistRequest.Name = data.Name;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);
            return list.Count > 0;
		}

		#endregion DoesExist

	}
}