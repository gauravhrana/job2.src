using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.ReferenceData;

namespace ReferenceData.Components.BusinessLayer
{
	public partial class PersonSuffixDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static PersonSuffixDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("PersonSuffix");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(PersonSuffixDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case PersonSuffixDataModel.DataColumns.PersonSuffixId:
					if (data.PersonSuffixId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, PersonSuffixDataModel.DataColumns.PersonSuffixId, data.PersonSuffixId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PersonSuffixDataModel.DataColumns.PersonSuffixId);
					}
					break;

				case PersonSuffixDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, PersonSuffixDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PersonSuffixDataModel.DataColumns.Name);
					}
					break;

				case PersonSuffixDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, PersonSuffixDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PersonSuffixDataModel.DataColumns.Description);
					}
					break;

				case PersonSuffixDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, PersonSuffixDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PersonSuffixDataModel.DataColumns.SortOrder);
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

		public static List<PersonSuffixDataModel> GetEntityDetails(PersonSuffixDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.PersonSuffixSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	PersonSuffixId           = dataQuery.PersonSuffixId
				 ,	Name           = dataQuery.Name
			};

			List<PersonSuffixDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<PersonSuffixDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<PersonSuffixDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(PersonSuffixDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(PersonSuffixDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(PersonSuffixDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.PersonSuffixInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.PersonSuffixUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, PersonSuffixDataModel.DataColumns.PersonSuffixId); 
			sql = sql + ", " + ToSQLParameter(data, PersonSuffixDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, PersonSuffixDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, PersonSuffixDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(PersonSuffixDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("PersonSuffix.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(PersonSuffixDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("PersonSuffix.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(PersonSuffixDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.PersonSuffixDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   PersonSuffixId  = data.PersonSuffixId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(PersonSuffixDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new PersonSuffixDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
