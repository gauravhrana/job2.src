using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using DataModel.Framework.DataAccess;
using Framework.CommonServices.Utils;

namespace Framework.Components.DataAccess
{
	public abstract class BaseDataManager
	{
		protected const string SQL_TEMPLATE_PARAMETER_NUMBER			= "@{0} = {1}";
		protected const string SQL_TEMPLATE_PARAMETER_STRING_OR_DATE	= "@{0} = '{1}'";
		protected const string SQL_TEMPLATE_PARAMETER_NULL				= "@{0} = NULL";

		protected const int ReturnAuditInfoOnDetails = 1;

		#region GetList
		public static DataTable GetList(int auditId, int applicationId, string dataStoreKey, string storedProcedrue)
		{
			var sql = "EXEC " + storedProcedrue +
				" " + SetCommonParameters(auditId, applicationId);

			var oDT = new DBDataTable("Get List", sql, dataStoreKey);

			return oDT.DBTable;
		}
		#endregion GetList

		protected static void SetBaseInfo(BaseDataModel dataItem, SqlDataReader dbReader)
		{
			//throws error in case the entity record doesnt not have entry for insert/update in audit history
			try
			{
				dataItem.UpdatedDate = (DateTime?)dbReader[BaseDataModel.BaseDataColumns.UpdatedDate];
			}
			catch
			{

			}

			dataItem.UpdatedBy = dbReader[BaseDataModel.BaseDataColumns.UpdatedBy].ToString();
			dataItem.LastAction = dbReader[BaseDataModel.BaseDataColumns.LastAction].ToString();
		}

		protected static void SetBaseInfo(BaseDataModel dataItem, DataRow dataRow)
		{
			if (dataRow[BaseDataModel.BaseDataColumns.UpdatedDate] != DBNull.Value)
			{
				try
				{
					dataItem.UpdatedDate = (DateTime?)dataRow[BaseDataModel.BaseDataColumns.UpdatedDate];
				}
				catch
				{
					dataItem.UpdatedDate = Convert.ToDateTime(dataRow[BaseDataModel.BaseDataColumns.UpdatedDate]);
				}
			}

			dataItem.UpdatedBy = dataRow[BaseDataModel.BaseDataColumns.UpdatedBy].ToString();
			dataItem.LastAction = dataRow[BaseDataModel.BaseDataColumns.LastAction].ToString();
		}

        protected static void SetBaseInfo(BaseModel dataItem, DataRow dataRow)
        {            
            dataItem.ApplicationId = (int?)dataRow[BaseModel.BaseColumns.ApplicationId];
        }

        protected static void SetBaseInfo(BaseModel dataItem, BaseDataModel baseItem)
        {
            dataItem.ApplicationId = baseItem.ApplicationId;
        }

        public static string ToSQLParameter(BaseModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case BaseModel.BaseColumns.ApplicationId:
					if (data.ApplicationId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, BaseModel.BaseColumns.ApplicationId, data.ApplicationId);
					}
					else
					{
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BaseModel.BaseColumns.ApplicationId);
					}

					break;
            }

            return returnValue;
        }

		public static string ToSQLParameter(BaseDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
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

				case BaseDataModel.BaseDataColumns.CreatedDate:
					if (data.CreatedDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, BaseDataModel.BaseDataColumns.CreatedDate, data.CreatedDate);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BaseDataModel.BaseDataColumns.CreatedDate);

					}
					break;

				case BaseDataModel.BaseDataColumns.ModifiedDate:
					if (data.ModifiedDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, BaseDataModel.BaseDataColumns.ModifiedDate, data.ModifiedDate);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BaseDataModel.BaseDataColumns.ModifiedDate);

					}
					break;

				case BaseDataModel.BaseDataColumns.CreatedByAuditId:
					if (data.CreatedByAuditId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, BaseDataModel.BaseDataColumns.CreatedByAuditId, data.CreatedByAuditId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BaseDataModel.BaseDataColumns.CreatedByAuditId);
					}
					break;


				case BaseDataModel.BaseDataColumns.ModifiedByAuditId:
					if (data.ModifiedByAuditId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, BaseDataModel.BaseDataColumns.ModifiedByAuditId, data.ModifiedByAuditId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BaseDataModel.BaseDataColumns.ModifiedByAuditId);
					}
					break;
			}

			return returnValue;
		}

		public static string ToSQLParameter(string dataColumnName, object value)
		{
			// by default assume NULL as the parmater to pass to uderlying stored procedure
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case BaseDataModel.BaseDataColumns.ApplicationId:
				case BaseDataModel.BaseDataColumns.AuditId:
					if (value != null)
					{
						returnValue = String.Format(SQL_TEMPLATE_PARAMETER_NUMBER, dataColumnName, value);
					}
					else
					{
						returnValue = String.Format(SQL_TEMPLATE_PARAMETER_NULL, dataColumnName);
					}
					break;

				case BaseDataModel.BaseDataColumns.UpdatedDate:
					if (value != null)
					{
						returnValue = String.Format(SQL_TEMPLATE_PARAMETER_NUMBER, dataColumnName, value);
					}
					else
					{
						returnValue = String.Format(SQL_TEMPLATE_PARAMETER_NULL, dataColumnName);
					}
					break;

				case BaseDataModel.BaseDataColumns.TraceId:
					if (value != null)
					{
						returnValue = String.Format(SQL_TEMPLATE_PARAMETER_NUMBER, dataColumnName, value);
					}
					else
					{
						returnValue = String.Format(SQL_TEMPLATE_PARAMETER_NULL, dataColumnName);
					}
					break;

				case BaseDataModel.BaseDataColumns.ApplicationMode:
					if (value != null)
					{
						returnValue = String.Format(SQL_TEMPLATE_PARAMETER_NUMBER, dataColumnName, value);
					}
					else
					{
						returnValue = String.Format(SQL_TEMPLATE_PARAMETER_NULL, dataColumnName);
					}
					break;

				case BaseDataModel.BaseDataColumns.AddAuditInfo:
					if (value != null)
					{
						returnValue = String.Format(SQL_TEMPLATE_PARAMETER_NUMBER, dataColumnName, value);
					}
					else
					{
						returnValue = String.Format(SQL_TEMPLATE_PARAMETER_NULL, dataColumnName);
					}
					break;

				case BaseDataModel.BaseDataColumns.AddTraceInfo:
					if (value != null)
					{
						returnValue = String.Format(SQL_TEMPLATE_PARAMETER_NUMBER, dataColumnName, value);
					}
					else
					{
						returnValue = String.Format(SQL_TEMPLATE_PARAMETER_NULL, dataColumnName);
					}
					break;

				case BaseDataModel.BaseDataColumns.ReturnAuditInfo:
					if (value != null)
					{
						returnValue = String.Format(SQL_TEMPLATE_PARAMETER_NUMBER, dataColumnName, value);
					}
					else
					{
						returnValue = String.Format(SQL_TEMPLATE_PARAMETER_NULL, dataColumnName);
					}
					break;

				case BaseDataModel.BaseDataColumns.PageIndex:
					if (value != null)
					{
						returnValue = String.Format(SQL_TEMPLATE_PARAMETER_NUMBER, dataColumnName, value);
					}
					else
					{
						returnValue = String.Format(SQL_TEMPLATE_PARAMETER_NULL, dataColumnName);
					}
					break;

				case BaseDataModel.BaseDataColumns.PageSize:
					if (value != null)
					{
						returnValue = String.Format(SQL_TEMPLATE_PARAMETER_NUMBER, dataColumnName, value);
					}
					else
					{
						returnValue = String.Format(SQL_TEMPLATE_PARAMETER_NULL, dataColumnName);
					}
					break;

				case BaseDataModel.BaseDataColumns.OrderBy:
					if (!String.IsNullOrEmpty(Convert.ToString(value)))
					{
						returnValue = String.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, dataColumnName, value);
					}
					else
					{
						returnValue = String.Format(SQL_TEMPLATE_PARAMETER_NULL, dataColumnName);
					}
					break;

				case BaseDataModel.BaseDataColumns.OrderByDirection:
					if (!String.IsNullOrEmpty(Convert.ToString(value)))
					{
						returnValue = String.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, dataColumnName, value);
					}
					else
					{
						returnValue = String.Format(SQL_TEMPLATE_PARAMETER_NULL, dataColumnName);
					}
					break;

				case BaseDataModel.BaseDataColumns.CreatedDate:
					if (!String.IsNullOrEmpty(Convert.ToString(value)))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, dataColumnName, value);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, dataColumnName);
					}
					break;
			}

			return returnValue;
		}

		public static void ListResources(Assembly assembly)
		{
			//var k	= string.Join(", ", assembly.GetManifestResourceNames());
			//System.Diagnostics.Debug.Print(k);

			var resourceNames = assembly.GetManifestResourceNames();
			foreach (var resourceName in resourceNames)
			{
				Trace.WriteLine(resourceName);
			}
		}

		public static string GetResourceText(Assembly assembly, string file)
		{
#if DEBUG
			//ListResources(assembly);
#endif

			var embededPath = assembly.GetName().Name + file;
			var resourceStream = assembly.GetManifestResourceStream(embededPath);

			var scriptTemplate = string.Empty;

			using (var textStreamReader = new StreamReader(resourceStream))
			{
				scriptTemplate = textStreamReader.ReadToEnd();
				textStreamReader.Close();
			}

			return scriptTemplate;
		}

		public static string GetSQL(Dictionary<string, string> replacementFieldSet, Dictionary<string, string> replacementOtherSet, string scriptTemplate, string key)
		{
			scriptTemplate = ReplaceFieldParameters(replacementFieldSet, scriptTemplate);
			scriptTemplate = ReplaceOtherParameters(replacementOtherSet, scriptTemplate);

#if DEBUG
			const string folder = @"C:\Temp\SQLLogs\";

			if (!Directory.Exists(folder))
			{
				Directory.CreateDirectory(folder);
			}

			File.WriteAllText(folder + key + DateTimeUtils2.FormatDateTimeToKey() + ".sql", scriptTemplate);
#endif

			return scriptTemplate;
		}

		protected static string ReplaceFieldParameters(Dictionary<string, string> fieldReplacementList, string scriptTemplate)
		{
			foreach (var item in fieldReplacementList)
			{
				var key = "@" + item.Key + "@";
				var value = item.Value;

				if (string.IsNullOrEmpty(value))
				{
					value = "''";
				}
				else
				{
					value = string.Format("[{0}]", value);
				}

				scriptTemplate = scriptTemplate.Replace(key, value);
			}

			return scriptTemplate;
		}

		protected static string ReplaceOtherParameters(Dictionary<string, string> fieldReplacementList, string scriptTemplate)
		{
			foreach (var item in fieldReplacementList)
			{
				var key = item.Key;
				var value = item.Value;

				scriptTemplate = scriptTemplate.Replace(key, value);
			}

			return scriptTemplate;
		}

		protected static string SetCommonParameters(int auditId, int applicaionId)
		{
			var result = ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId)
						+ ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, applicaionId);

			return result;
		}

		protected static string SetCommonParametersForSearch(int auditId, int applicaionId, int applicationModeId = 0)
		{
			var result = ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId)
						 + ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, applicaionId) +
						 ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationMode, applicationModeId);

			return result;
		}

		protected static string SetCommonParametersForDetails(int auditId)
		{
			var result = ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId)
						 + ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ReturnAuditInfo, ReturnAuditInfoOnDetails);

			return result;
		}

		protected static string SetCommonParametersForEnitityDetails(int auditId, int applicationModeId = 0)
		{
			var result = ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
							", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationMode, applicationModeId) +
							", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ReturnAuditInfo, ReturnAuditInfoOnDetails);

			return result;
		}

	}
}
