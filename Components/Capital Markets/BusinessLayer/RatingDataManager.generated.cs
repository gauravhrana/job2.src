using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.CapitalMarkets;

namespace CapitalMarkets.Components.BusinessLayer
{
	public partial class RatingDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static RatingDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("Rating");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(RatingDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case RatingDataModel.DataColumns.RatingId:
					if (data.RatingId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, RatingDataModel.DataColumns.RatingId, data.RatingId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, RatingDataModel.DataColumns.RatingId);
					}
					break;

				case RatingDataModel.DataColumns.Date:
					if (data.Date != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, RatingDataModel.DataColumns.Date, data.Date);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, RatingDataModel.DataColumns.Date);
					}
					break;

				case RatingDataModel.DataColumns.Analyst:
					if (!string.IsNullOrEmpty(data.Analyst))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, RatingDataModel.DataColumns.Analyst, data.Analyst);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, RatingDataModel.DataColumns.Analyst);
					}
					break;

				case RatingDataModel.DataColumns.Rating:
					if (data.Rating != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, RatingDataModel.DataColumns.Rating, data.Rating);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, RatingDataModel.DataColumns.Rating);
					}
					break;

				case RatingDataModel.DataColumns.Notes:
					if (!string.IsNullOrEmpty(data.Notes))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, RatingDataModel.DataColumns.Notes, data.Notes);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, RatingDataModel.DataColumns.Notes);
					}
					break;


				default:
					returnValue = BaseDataManager.ToSQLParameter(data, dataColumnName);
					break;
			}

			return returnValue;

		}

		#endregion

		#region Get Entity Details

		public static List<RatingDataModel> GetEntityDetails(RatingDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.RatingSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	RatingId           = dataQuery.RatingId
			};

			List<RatingDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<RatingDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<RatingDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(RatingDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(RatingDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(RatingDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.RatingInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.RatingUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, RatingDataModel.DataColumns.RatingId); 
			sql = sql + ", " + ToSQLParameter(data, RatingDataModel.DataColumns.Date); 
			sql = sql + ", " + ToSQLParameter(data, RatingDataModel.DataColumns.Analyst); 
			sql = sql + ", " + ToSQLParameter(data, RatingDataModel.DataColumns.Rating); 
			sql = sql + ", " + ToSQLParameter(data, RatingDataModel.DataColumns.Notes); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(RatingDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("Rating.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(RatingDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("Rating.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(RatingDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.RatingDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   RatingId  = data.RatingId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(RatingDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new RatingDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
