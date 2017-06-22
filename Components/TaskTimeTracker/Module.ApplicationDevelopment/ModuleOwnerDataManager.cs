using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using System.Runtime.CompilerServices;
using Dapper;
using Framework.CommonServices.BusinessDomain.Utils;

namespace TaskTimeTracker.Components.Module.ApplicationDevelopment
{
	public partial class ModuleOwnerDataManager : BaseDataManager
	{

		static readonly string DataStoreKey = "";

		static ModuleOwnerDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("ModuleOwner");
		}

		#region GetList

        public static List<ModuleOwnerDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(ModuleOwnerDataModel.Empty, requestProfile, 1);
		}

		#endregion

		#region GetDetails

		public static ModuleOwnerDataModel GetDetails(ModuleOwnerDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile);

			return list.FirstOrDefault();
		}

		#endregion

		#region GetEntitySearch

		public static List<ModuleOwnerDataModel> GetEntityDetails(ModuleOwnerDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.ModuleOwnerSearch ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,	ApplicationId = dataQuery.ApplicationId
				,	ApplicationMode = requestProfile.ApplicationModeId
				,	ModuleOwnerId = dataQuery.ModuleOwnerId
				,	ModuleId = dataQuery.ModuleId
				,	DeveloperRoleId = dataQuery.DeveloperRoleId
				,	Developer = dataQuery.Developer
				,	FeatureOwnerStatusId = dataQuery.FeatureOwnerStatusId
				,	ReturnAuditInfo = returnAuditInfo
				,	TotalHoursWorked = dataQuery.TotalHoursWorked
			};

			List<ModuleOwnerDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<ModuleOwnerDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		#endregion

		#region Create

		public static void Create(ModuleOwnerDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");
			DBDML.RunSQL("ModuleOwner.Insert", sql, DataStoreKey);
		}

		#endregion

		#region Update

		public static void Update(ModuleOwnerDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");
			DBDML.RunSQL("ModuleOwner.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(ModuleOwnerDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.ModuleOwnerDelete ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				ModuleOwnerId = dataQuery.ModuleOwnerId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion

		#region Search

		public static string ToSQLParameter(ModuleOwnerDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case ModuleOwnerDataModel.DataColumns.ModuleOwnerId:
					if (data.ModuleOwnerId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ModuleOwnerDataModel.DataColumns.ModuleOwnerId, data.ModuleOwnerId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ModuleOwnerDataModel.DataColumns.ModuleOwnerId);
					}
					break;

				case ModuleOwnerDataModel.DataColumns.ModuleId:
					if (data.ModuleId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ModuleOwnerDataModel.DataColumns.ModuleId, data.ModuleId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ModuleOwnerDataModel.DataColumns.ModuleId);
					}
					break;

				case ModuleOwnerDataModel.DataColumns.DeveloperRoleId:
					if (data.DeveloperRoleId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ModuleOwnerDataModel.DataColumns.DeveloperRoleId, data.DeveloperRoleId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ModuleOwnerDataModel.DataColumns.DeveloperRoleId);
					}
					break;

				case ModuleOwnerDataModel.DataColumns.Developer:
					if (data.Developer != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ModuleOwnerDataModel.DataColumns.Developer, data.Developer);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ModuleOwnerDataModel.DataColumns.Developer);
					}
					break;

				case ModuleOwnerDataModel.DataColumns.FeatureOwnerStatusId:
					if (data.FeatureOwnerStatusId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ModuleOwnerDataModel.DataColumns.FeatureOwnerStatusId, data.FeatureOwnerStatusId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ModuleOwnerDataModel.DataColumns.FeatureOwnerStatusId);
					}
					break;

				case ModuleOwnerDataModel.DataColumns.Module:
					if (data.Module != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ModuleOwnerDataModel.DataColumns.Module, data.Module);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ModuleOwnerDataModel.DataColumns.Module);
					}
					break;

				case ModuleOwnerDataModel.DataColumns.DeveloperRole:
					if (data.DeveloperRole != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ModuleOwnerDataModel.DataColumns.DeveloperRole, data.DeveloperRole);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ModuleOwnerDataModel.DataColumns.DeveloperRole);
					}
					break;

				case ModuleOwnerDataModel.DataColumns.FeatureOwnerStatus:
					if (data.FeatureOwnerStatus != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ModuleOwnerDataModel.DataColumns.FeatureOwnerStatus, data.FeatureOwnerStatus);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ModuleOwnerDataModel.DataColumns.FeatureOwnerStatus);
					}
					break;

				case BaseDataModel.BaseDataColumns.ApplicationId:
					if (data.ApplicationId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, BaseDataModel.BaseDataColumns.ApplicationId, data.ApplicationId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BaseDataModel.BaseDataColumns.ApplicationId);
					}
					break;

				case ModuleOwnerDataModel.DataColumns.Application:
					if (!string.IsNullOrEmpty(data.Application))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ModuleOwnerDataModel.DataColumns.Application, data.Application.Trim());
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ModuleOwnerDataModel.DataColumns.Application);
					}
					break;

				case ModuleOwnerDataModel.DataColumns.TotalHoursWorked:
					if (data.TotalHoursWorked!=null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ModuleOwnerDataModel.DataColumns.TotalHoursWorked, data.TotalHoursWorked);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ModuleOwnerDataModel.DataColumns.TotalHoursWorked);
					}
					break;
				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

		public static DataTable Search(ModuleOwnerDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

		#region CreateOrUpdate

		private static string CreateOrUpdate(ModuleOwnerDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.ModuleOwnerInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(data, ModuleOwnerDataModel.DataColumns.ApplicationId);
					break;

				case "Update":
					sql += "dbo.ModuleOwnerUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						 ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, ModuleOwnerDataModel.DataColumns.ModuleOwnerId) +
					", " + ToSQLParameter(data, ModuleOwnerDataModel.DataColumns.ModuleId) +
					", " + ToSQLParameter(data, ModuleOwnerDataModel.DataColumns.DeveloperRoleId) +
					", " + ToSQLParameter(data, ModuleOwnerDataModel.DataColumns.Developer) +
					", " + ToSQLParameter(data, ModuleOwnerDataModel.DataColumns.FeatureOwnerStatusId)+
					", " + ToSQLParameter(data, ModuleOwnerDataModel.DataColumns.TotalHoursWorked);
			return sql;
		}

		#endregion

		#region DoesExist

		public static bool DoesExist(ModuleOwnerDataModel data, RequestProfile requestProfile)
		{
            var doesExistRequest             = new ModuleOwnerDataModel();
            doesExistRequest.ApplicationId   = data.ApplicationId;
            doesExistRequest.ModuleId        = data.ModuleId;
            doesExistRequest.Developer       = data.Developer;
            doesExistRequest.DeveloperRoleId = data.DeveloperRoleId;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

            return list.Count > 0;
		}

		#endregion

		#region GetStatisticData

		private const string StatisticUnknown = "unknown";

		public static List<Statistic> GetStatisticData(EnumerableRowCollection<DataRow> scheduleData, int scheduleTimeSpentConstant)
		{
			var result = new List<Statistic>();
			var dataItem = new Statistic();

			// totalTimeSpent1 calculates the time of 'UnKnown' value based on the ReleaseNotesTimeSpentConstant
			decimal totalTimeSpent1 = scheduleData
										 .Where(z => (string.IsNullOrEmpty(z["TotalHoursWorked"].ToString()) || z[ModuleOwnerDataModel.DataColumns.TotalHoursWorked].ToString().ToLower() == StatisticUnknown))
										 .Count() * scheduleTimeSpentConstant;

			// totalTimeSpent2 calculates the time of TimeSpent column <> 'UnKnown'
			decimal totalTimeSpent2 = scheduleData
										 .Where(x => x[ModuleOwnerDataModel.DataColumns.TotalHoursWorked].ToString().ToLower() != StatisticUnknown)
										 .Sum(x => Convert.ToDecimal(x[ModuleOwnerDataModel.DataColumns.TotalHoursWorked]));

			// calculates the count of records whose TimeSpent <> 'UnKnown'
			var count = scheduleData
										 .Where(x => x[ModuleOwnerDataModel.DataColumns.TotalHoursWorked].ToString().ToLower() != StatisticUnknown)
										 .Select(x => x[ModuleOwnerDataModel.DataColumns.TotalHoursWorked]).Count();

			// calculates the total count of records
			var totalCount = scheduleData
										 .Select(x => x[ModuleOwnerDataModel.DataColumns.TotalHoursWorked]).Count();

			dataItem.Total = totalTimeSpent1 + totalTimeSpent2;

			dataItem.Count = totalCount;

			var dt = new DataTable();
			dt.Columns.Add("TotalHoursWorked");
			dt.AcceptChanges();

			var rowT = dt.NewRow();

			var list = scheduleData
					  .Where(x => x[ModuleOwnerDataModel.DataColumns.TotalHoursWorked].ToString().ToLower() != StatisticUnknown);

			var list1 = scheduleData
						.Where(x => x[ModuleOwnerDataModel.DataColumns.TotalHoursWorked].ToString().ToLower() == StatisticUnknown);

			var rows = from row in list1.AsEnumerable()
					   select row;
			// takes care of the logic of TimeSpent column with UnKnown value to calculate average and median
			foreach (var row in rows)
			{
				if (row[ModuleOwnerDataModel.DataColumns.TotalHoursWorked].ToString().ToLower() == StatisticUnknown)
				{
					rowT = dt.NewRow();
					rowT["TotalHoursWorked"] = scheduleTimeSpentConstant;
					dt.Rows.Add(rowT);
				}
			}

			var list2 = list.Concat(dt.AsEnumerable());

			var rowItem = from row in list2.AsEnumerable() select row;

			if (rowItem.Any())
			{
				//calculates the average value
				dataItem.Average = list2
								  .Where(x => x[ModuleOwnerDataModel.DataColumns.TotalHoursWorked].ToString().ToLower() != StatisticUnknown)
								  .Select(x => Convert.ToDecimal(x[ModuleOwnerDataModel.DataColumns.TotalHoursWorked])).Average();

				//calculates the max and min values 
				dataItem.Max = list2.Max(x => Convert.ToDecimal(x[ModuleOwnerDataModel.DataColumns.TotalHoursWorked]));
				dataItem.Min = list2.Min(x => Convert.ToDecimal(x[ModuleOwnerDataModel.DataColumns.TotalHoursWorked]));

				// gets the ordered list to find the median
				var orderedList = list2.OrderBy(p => Convert.ToDecimal(p[ModuleOwnerDataModel.DataColumns.TotalHoursWorked]));

				// calculates median for even number list
				if ((totalCount % 2) == 0)
				{
					dataItem.Median = Convert.ToDecimal(orderedList.ElementAt(totalCount / 2)[ModuleOwnerDataModel.DataColumns.TotalHoursWorked]) + Convert.ToDecimal(orderedList.ElementAt((totalCount - 1) / 2)[ModuleOwnerDataModel.DataColumns.TotalHoursWorked]);
					dataItem.Median /= 2;
				}
				else
				{
					// calculating median for odd number list
					if (totalCount == 1)
					{
						dataItem.Median = Convert.ToDecimal(orderedList.ElementAt(totalCount - 1)[ModuleOwnerDataModel.DataColumns.TotalHoursWorked]);
					}
					else
					{
						dataItem.Median = Convert.ToDecimal(orderedList.ElementAt(totalCount / 2)[ModuleOwnerDataModel.DataColumns.TotalHoursWorked]);
					}
				}
			}

			result.Add(dataItem);

			return result;
		}

		public static List<decimal> GetStatisticDataSummary(EnumerableRowCollection<DataRow> scheduleData, int scheduleTimeSpentConstant)
		{
			DataTable dt = new DataTable();
			dt.Clear();
			dt.Columns.Add("TotalHoursWorked");
			DataRow rowT = dt.NewRow();
			decimal average = 0;

			var totalTimeSpent1 = scheduleData
			.Where(z => (string.IsNullOrEmpty(z["TotalHoursWorked"].ToString()) || z[ModuleOwnerDataModel.DataColumns.TotalHoursWorked].ToString().ToLower() == StatisticUnknown)).Count() * scheduleTimeSpentConstant;

			// totalTimeSpent2 calculates the time of TimeSpent column <> 'UnKnown'
			var totalTimeSpent2 = scheduleData
				.Where(x => x[ModuleOwnerDataModel.DataColumns.TotalHoursWorked].ToString().ToLower() != StatisticUnknown)
				.Sum(x => Convert.ToDecimal(x[ModuleOwnerDataModel.DataColumns.TotalHoursWorked]));

			//finding total number o records
			var count = scheduleData
				.Select(x => x[ModuleOwnerDataModel.DataColumns.TotalHoursWorked]).Count();

			if (count != 0)
				average = (totalTimeSpent1 + totalTimeSpent2) / count;

			var list = scheduleData
				.Where(x => x[ModuleOwnerDataModel.DataColumns.TotalHoursWorked].ToString().ToLower() != StatisticUnknown);

			var list1 = scheduleData
				.Where(x => x[ModuleOwnerDataModel.DataColumns.TotalHoursWorked].ToString().ToLower() == StatisticUnknown);


			IEnumerable<DataRow> rows = from row in list1.AsEnumerable()
										select row;

			foreach (DataRow row in rows)
			{
				if (row[ModuleOwnerDataModel.DataColumns.TotalHoursWorked].ToString().ToLower() == "unknown")
				{
					rowT = dt.NewRow();
					rowT["TotalHoursWorked"] = scheduleTimeSpentConstant;
					dt.Rows.Add(rowT);
				}
			}

			var listMedian = list.Concat(dt.AsEnumerable());

			var orderedList = listMedian.OrderBy(p => Convert.ToDecimal(p[ModuleOwnerDataModel.DataColumns.TotalHoursWorked]));
			decimal median = 0;

			if (count != 0)
			{
				// calculating median for even number list
				if ((count % 2) == 0)
				{
					median = Convert.ToDecimal(orderedList.ElementAt(count / 2)[ModuleOwnerDataModel.DataColumns.TotalHoursWorked]) + Convert.ToDecimal(orderedList.ElementAt((count - 1) / 2)[ModuleOwnerDataModel.DataColumns.TotalHoursWorked]);
					median /= 2;
				}
				else
				{
					// calculating median for odd number list
					if (count == 1)
					{
						median = Convert.ToDecimal(orderedList.ElementAt(count - 1)[ModuleOwnerDataModel.DataColumns.TotalHoursWorked]);
					}
					else
					{
						median = Convert.ToDecimal(orderedList.ElementAt(count / 2)[ModuleOwnerDataModel.DataColumns.TotalHoursWorked]);
					}
				}
			}
			List<decimal> lstValues = new List<decimal>();
			lstValues.Add(average);
			lstValues.Add(median);
			return lstValues;
		}

		#endregion

	}
}
