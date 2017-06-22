using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using System.Runtime.CompilerServices;
using Dapper;
using Framework.CommonServices.BusinessDomain.Utils;
using System.IO;

namespace TaskTimeTracker.Components.Module.ApplicationDevelopment
{
	public partial class FunctionalityImageDataManager : BaseDataManager
	{
        static readonly string DataStoreKey = "";

		static FunctionalityImageDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("FunctionalityImage");
		}

		#region GetList

        public static List<FunctionalityImageDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(FunctionalityImageDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region GetDetails

		public static FunctionalityImageDataModel GetDetails(FunctionalityImageDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile);

			return list.FirstOrDefault();
		}

		#endregion

		#region GetEntitySearch

		public static List<FunctionalityImageDataModel> GetEntityDetails(FunctionalityImageDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.FunctionalityImageSearch ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,	FunctionalityImageId = dataQuery.FunctionalityImageId
				,	Title = dataQuery.Title
				,	ApplicationMode = requestProfile.ApplicationModeId
				,	ReturnAuditInfo = returnAuditInfo
			};

			List<FunctionalityImageDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<FunctionalityImageDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		#endregion

		public static List<StandardListDataModel> GetFunctionalityImageList(RequestProfile requestProfile)
		{
			var data = new FunctionalityImageDataModel();
			var list = GetEntityDetails(data, requestProfile);

			var result = list.Select(item => new StandardListDataModel()
			{
				Name = item.Title,
				Value = item.FunctionalityImageId.ToString()
			})
			.ToList();

			return result;
		}

		#region Create

		public static void Create(FunctionalityImageDataModel data, RequestProfile requestProfile)
		{
			//var sql = CreateOrUpdate(data, requestProfile, "Create");

			//DBDML.RunSQL("FunctionalityHistory.Insert", sql, DataStoreKey);

			var sql = "FunctionalityImageInsert";

			var parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId));
			parameters.Add(new SqlParameter(BaseDataModel.BaseDataColumns.ApplicationId, data.ApplicationId));
			parameters.Add(new SqlParameter(FunctionalityImageDataModel.DataColumns.Title, data.Title));
			parameters.Add(new SqlParameter(FunctionalityImageDataModel.DataColumns.Image, data.Image));
			parameters.Add(new SqlParameter(FunctionalityImageDataModel.DataColumns.Description, data.Description));

			DBDML.RunSQLWithParameters("FunctionalityImage.Insert", sql, parameters.ToArray(), DataStoreKey);
		}

		#endregion

		#region Update

		public static void Update(FunctionalityImageDataModel data, RequestProfile requestProfile)
		{

			//var sql = CreateOrUpdate(data, requestProfile, "Update");

			//DBDML.RunSQL("FunctionalityHistory.Insert", sql, DataStoreKey);

			//var sql = Save(data, auditId, "Update");
			var sql = "FunctionalityImageUpdate";

			var parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId));
			parameters.Add(new SqlParameter(BaseDataModel.BaseDataColumns.ApplicationId, data.ApplicationId));
			parameters.Add(new SqlParameter(FunctionalityImageDataModel.DataColumns.Title, data.Title));
			parameters.Add(new SqlParameter(FunctionalityImageDataModel.DataColumns.Image, data.Image));
			parameters.Add(new SqlParameter(FunctionalityImageDataModel.DataColumns.FunctionalityImageId, data.FunctionalityImageId));
			parameters.Add(new SqlParameter(FunctionalityImageDataModel.DataColumns.Description, data.Description));

			DBDML.RunSQLWithParameters("FunctionalityImage.Update", sql, parameters.ToArray(), DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(FunctionalityImageDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.FunctionalityImageDelete ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				FunctionalityImageId = dataQuery.FunctionalityImageId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion

		#region Search

		//public static string ToSQLParameter(FunctionalityImageDataModel data, string dataColumnName)
		//{
		//	var returnValue = "NULL";

		//	switch (dataColumnName)
		//	{

		//		case FunctionalityImageDataModel.DataColumns.FunctionalityImageId:
		//			if (data.FunctionalityImageId != null)
		//			{
		//				returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityImageDataModel.DataColumns.FunctionalityImageId, data.FunctionalityImageId);


		//			}

		//			else
		//			{
		//				returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityImageDataModel.DataColumns.FunctionalityImageId);

		//			}
		//			break;

		//		case FunctionalityImageDataModel.DataColumns.Title:
		//			if (!string.IsNullOrEmpty(data.Title))
		//			{
		//				returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FunctionalityImageDataModel.DataColumns.Title, data.Title);
		//			}
		//			else
		//			{
		//				returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityImageDataModel.DataColumns.Title);
		//			}
		//			break;

		//		case FunctionalityImageDataModel.DataColumns.Description:
		//			if (!string.IsNullOrEmpty(data.Description))
		//			{
		//				returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FunctionalityImageDataModel.DataColumns.Description, data.Description);
		//			}
		//			else
		//			{
		//				returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityImageDataModel.DataColumns.Description);
		//			}
		//			break;

		//		case FunctionalityImageDataModel.DataColumns.Image:
		//			if (data.Image != null)
		//			{
						
		//				returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FunctionalityImageDataModel.DataColumns.Image,data.Image);
		//			}
		//			else
		//			{
		//				returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityImageDataModel.DataColumns.Image);
		//			}
		//			break;

		//		case FunctionalityImageDataModel.DataColumns.Application:
		//			if (!string.IsNullOrEmpty(data.Application))
		//			{
		//				returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FunctionalityImageDataModel.DataColumns.Application, data.Application);
		//			}
		//			else
		//			{
		//				returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityImageDataModel.DataColumns.Application);
		//			}
		//			break;

		//	}
		//	return returnValue;
		//}

		public static DataTable Search(FunctionalityImageDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

		#region CreateOrUpdate

		//private static string CreateOrUpdate(FunctionalityImageDataModel data, RequestProfile requestProfile, string action)
		//{
		//	//var sql = "EXEC ";

		//	//switch (action)
		//	//{
		//	//	case "Create":
		//	//		sql += "dbo.FunctionalityImageInsert  " + " " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) + ", ";
		//	//		break;

		//	//	case "Update":
		//	//		sql += "dbo.FunctionalityImageUpdate " + " " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) + ", "; ;
		//	//		break;

		//	//	default:
		//	//		break;

		//	//}
			
			
		//	//sql = sql + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
		//	//			", " + ToSQLParameter(data, FunctionalityImageDataModel.DataColumns.FunctionalityImageId) +
		//	//			", " + ToSQLParameter(data, FunctionalityImageDataModel.DataColumns.Title) +
		//	//			", @Image="+(new SqlParameter(FunctionalityImageDataModel.DataColumns.Image, data.Image)) +
		//	//			", " + ToSQLParameter(data, FunctionalityImageDataModel.DataColumns.Description);
		//	return string.Empty;
		//}

		#endregion

		#region DoesExist

		public static bool DoesExist(FunctionalityImageDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new FunctionalityImageDataModel();
			doesExistRequest.Title = data.Title;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

            return list.Count > 0;
		}

		#endregion
	}
}
