using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace Framework.Components.DataAccess
{
	public abstract class StandardDataManager : BaseDataManager
	{
		public string EntityName;

		public static string ToSQLParameter(StandardDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case StandardDataModel.StandardDataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, StandardDataModel.StandardDataColumns.Name,
							data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, StandardDataModel.StandardDataColumns.Name);
					}

					break;

				case StandardDataModel.StandardDataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE,
							StandardDataModel.StandardDataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, StandardDataModel.StandardDataColumns.Description);
					}

					break;

				case StandardDataModel.StandardDataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, StandardDataModel.StandardDataColumns.SortOrder,
							data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, StandardDataModel.StandardDataColumns.SortOrder);
					}

					break;

				default:
					returnValue = BaseDataManager.ToSQLParameter(data, dataColumnName);
					break;
			}

			return returnValue;
		}

		protected static void SetStandardInfo(StandardDataModel dataItem, SqlDataReader dbReader)
		{
			dataItem.Name = dbReader[StandardDataModel.StandardDataColumns.Name].ToString();
			dataItem.Description = dbReader[StandardDataModel.StandardDataColumns.Description].ToString();
			dataItem.SortOrder = (int)dbReader[StandardDataModel.StandardDataColumns.SortOrder];
		}

		protected static void SetStandardInfo(StandardDataModel dataItem, DataRow dataRow)
		{
			dataItem.Name = dataRow[StandardDataModel.StandardDataColumns.Name].ToString();
			dataItem.Description = dataRow[StandardDataModel.StandardDataColumns.Description].ToString();
			dataItem.SortOrder = (int)dataRow[StandardDataModel.StandardDataColumns.SortOrder];
		}

		//public abstract static DataTable GetList(RequestProfile requestProfile);

		List<string> lstExcludedProperties = new List<string>() { 
														"CreatedDateId"
													,	"UpdatedDateId"
													,	"CreatedDate"
													,	"UpdatedDate"
													,	"UpdatedBy"
													,	"LastAction"
													,	"ModifiedDate"
													,	"CreatedByAuditId"
													,	"ModifiedByAuditId"
														};

		private void IsBaseDataMoedel(Type objType)
		{
			var isSubClass = objType.IsSubclassOf(typeof(BaseDataModel));
			if (!isSubClass)
			{
				throw new Exception("Invlalid Data Model object passed");
			}
		}

		string GetSQLType(Type propType)
		{
			string sqlType = string.Empty;

			if (propType == typeof(string))
			{
				sqlType = "VARCHAR(50)";
			}
			else if (propType == typeof(int?))
			{
				sqlType = "INT";
			}

			return sqlType;
		}

		public string GetCheckAndDropTableScript()
		{
			var script = new StringBuilder();

			script.AppendLine("IF OBJECT_ID ('dbo." + EntityName + "') IS NOT NULL");
			script.AppendLine("BEGIN");
			script.AppendLine("	DROP TABLE dbo." + EntityName);
			script.AppendLine("END");

			return script.ToString();
		}

		public string GetCreateTableScript(Type objType)
		{

			IsBaseDataMoedel(objType);

			var script = new StringBuilder();

			script.AppendLine("");
			script.AppendLine("CREATE TABLE dbo." + EntityName);
			script.AppendLine("(");

			var objProps = objType.GetProperties();

			var preText = "		";

			foreach (var propInfo in objProps)
			{
				if (!lstExcludedProperties.Contains(propInfo.Name))
				{
					var sqlType = GetSQLType(propInfo.PropertyType);
					script.AppendLine(preText + propInfo.Name + "			" + sqlType + "		NOT NULL");
					preText = "	,	";
				}
			}

			script.AppendLine(")");

			return script.ToString();
		}

		public string GetCreatePrimaryKeyScript()
		{
			var script = new StringBuilder();

			script.AppendLine("ALTER TABLE dbo." + EntityName);
			script.AppendLine("	ADD CONSTRAINT PK_" + EntityName + " PRIMARY KEY CLUSTERED");
			script.AppendLine("	(");
			script.AppendLine("		" + EntityName + "Id");
			script.AppendLine("	)");

			return script.ToString();
		}

		public string GetUniqueIndexName(List<string> uniqueColumns)
		{		
			var uniqueIndexName = "UQ_" + EntityName;

			foreach (var columnName in uniqueColumns)
			{
				uniqueIndexName += "_" + columnName;
			}

			return uniqueIndexName;
		}

		public string GetCheckAndDropUniqueIndex(List<string> uniqueColumns)
		{
			var script = new StringBuilder();

			var uniqueIndexName = GetUniqueIndexName(uniqueColumns);

			script.AppendLine("IF EXISTS");
			script.AppendLine("(");
			script.AppendLine("	SELECT	*");
			script.AppendLine("	FROM	dbo.sysindexes");
			script.AppendLine("	WHERE	id		= OBJECT_ID(N'[dbo].[" + EntityName + "]')");
			script.AppendLine("	AND		name	= N'" + uniqueIndexName + "'");
			script.AppendLine(")");
			script.AppendLine("BEGIN");
			script.AppendLine("	ALTER	TABLE dbo." + EntityName);
			script.AppendLine("	DROP	CONSTRAINT	" + uniqueIndexName);
			script.AppendLine("END");

			return script.ToString();
		}

		public string GetCreateUniqueIndex(List<string> uniqueColumns)
		{
			var script = new StringBuilder();

			var uniqueIndexName = GetUniqueIndexName(uniqueColumns);

			script.AppendLine("ALTER TABLE dbo." + EntityName);
			script.AppendLine("	ADD CONSTRAINT " + uniqueIndexName + " UNIQUE NONCLUSTERED");
			script.AppendLine("	(");

			var preText = "			";
			foreach (var uniqueColumn in uniqueColumns)
			{
				script.AppendLine(preText + uniqueColumn);
				preText = "		,	";
			}

			script.AppendLine("	)");

			return script.ToString();
		}

		private string GetAuditInfoText(string procedureType)
		{
			var script = new StringBuilder();

			script.AppendLine("");
			script.AppendLine("	-- Create Audit Record");
			script.AppendLine("	EXEC dbo.AuditHistoryInsert");
			script.AppendLine("		@SystemEntityType		= @SystemEntityType");
			script.AppendLine("	,	@EntityKey				= @" + EntityName + "Id");
			script.AppendLine("	,	@AuditAction			= '" + procedureType + "'");
			script.AppendLine("	,	@CreatedDate			= @AuditDate");
			script.AppendLine("	,	@CreatedByPersonId		= @AuditId");

			return script.ToString();
		}

		public string GetCheckAndDropProcedureScript(string procType)
		{
			var script = new StringBuilder();

			script.AppendLine("IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = '" + EntityName + procType + "')");
			script.AppendLine("BEGIN");
			script.AppendLine("	DROP PROCEDURE " + EntityName + procType);
			script.AppendLine("END");

			return script.ToString();
		}

		public virtual string GetInsertProcedureScript(Type objType)
		{

			IsBaseDataMoedel(objType);

			var script = new StringBuilder();

			script.AppendLine("CREATE Procedure dbo." + EntityName + "Insert");
			script.AppendLine("(");

			var objProps = objType.GetProperties();

			var preText = "	";

			foreach (var propInfo in objProps)
			{
				if (!lstExcludedProperties.Contains(propInfo.Name))
				{
					var sqlType = GetSQLType(propInfo.PropertyType);
					if (propInfo.Name == (EntityName + " Id"))
					{
						script.AppendLine(preText + "@" + propInfo.Name + "			" + sqlType + "		= NULL	OUTPUT");
					}
					else
					{
						script.AppendLine(preText + "@" + propInfo.Name + "			" + sqlType);
					}
					preText = ",	";
				}
			}

			script.AppendLine(",	@AuditId				INT									");
			script.AppendLine(",	@AuditDate				DATETIME	= NULL					");
			script.AppendLine(",	@SystemEntityType		VARCHAR(50) = '" + EntityName + "'	");
			script.AppendLine(")");

			script.AppendLine("AS");
			script.AppendLine("BEGIN");

			script.AppendLine("");
			script.AppendLine("	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @" + EntityName + "Id OUTPUT, @AuditId");
			script.AppendLine("");

			script.AppendLine("	INSERT INTO dbo." + EntityName + " ");
			script.AppendLine("	( ");

			preText = "		";
			foreach (var propInfo in objProps)
			{
				if (!lstExcludedProperties.Contains(propInfo.Name))
				{
					script.AppendLine(preText + propInfo.Name);
					preText = "	,	";
				}
			}

			script.AppendLine("	)");
			script.AppendLine("	VALUES");
			script.AppendLine("	(");

			preText = "		";
			foreach (var propInfo in objProps)
			{
				if (!lstExcludedProperties.Contains(propInfo.Name))
				{
					script.AppendLine(preText + "@" + propInfo.Name);
					preText = "	,	";
				}
			}

			script.AppendLine("	)");
			script.AppendLine("");
			script.AppendLine("	SELECT @" + EntityName + "Id");

			script.Append(GetAuditInfoText("Insert"));

			script.AppendLine("");
			script.AppendLine("END");

			return script.ToString();

		}

		public virtual string GetUpdateProcedureScript(Type objType)
		{

			IsBaseDataMoedel(objType);
			var script = new StringBuilder();

			script.AppendLine("CREATE Procedure dbo." + EntityName + "Update");
			script.AppendLine("(");

			var objProps = objType.GetProperties();

			var preText = "	";

			foreach (var propInfo in objProps)
			{
				if (!lstExcludedProperties.Contains(propInfo.Name)
					&& propInfo.Name != "ApplicationId")
				{
					var sqlType = GetSQLType(propInfo.PropertyType);
					script.AppendLine(preText + "@" + propInfo.Name + "			" + sqlType);
					preText = ",	";
				}
			}

			script.AppendLine(",	@AuditId				INT									");
			script.AppendLine(",	@AuditDate				DATETIME	= NULL					");
			script.AppendLine(",	@SystemEntityType		VARCHAR(50) = '" + EntityName + "'	");
			script.AppendLine(")");

			script.AppendLine("AS");
			script.AppendLine("BEGIN");
			script.AppendLine("");

			script.AppendLine("	UPDATE	dbo." + EntityName);

			preText = "	SET		";

			foreach (var propInfo in objProps)
			{
				if (!lstExcludedProperties.Contains(propInfo.Name)
					&& propInfo.Name != (EntityName + "Id")
					&& propInfo.Name != "ApplicationId")
				{
					script.AppendLine(preText + propInfo.Name + "		=	@" + propInfo.Name);
					preText = "	,	";
				}
			}
			script.AppendLine("	WHERE	" + EntityName + "Id			=	@" + EntityName + "Id");

			script.Append(GetAuditInfoText("Update"));

			script.AppendLine("");
			script.AppendLine("END");

			return script.ToString();

		}

		public virtual string GetDeleteProcedureScript()
		{

			var script = new StringBuilder();

			script.AppendLine("CREATE Procedure dbo." + EntityName + "Delete");
			script.AppendLine("(");
			script.AppendLine("		@" + EntityName + "Id 			INT						");
			script.AppendLine("	,	@AuditId				INT	");
			script.AppendLine("	,	@AuditDate				DATETIME	= NULL	");
			script.AppendLine("	,	@SystemEntityType		VARCHAR(50) = '" + EntityName + "'	");
			script.AppendLine(")");
			script.AppendLine("AS");
			script.AppendLine("BEGIN");

			script.AppendLine("");
			script.AppendLine("	DELETE	 dbo." + EntityName);
			script.AppendLine("	WHERE	" + EntityName + "Id			=	@" + EntityName + "Id");

			script.Append(GetAuditInfoText("Delete"));

			script.AppendLine("");
			script.AppendLine("END");

			return script.ToString();

		}

		public virtual string GetSearchProcedureScript(List<string> searchableColumns, Type objType)
		{

			IsBaseDataMoedel(objType);

			var script = new StringBuilder();

			script.AppendLine("CREATE Procedure dbo." + EntityName + "Search");
			script.AppendLine("(");

			var objProps = objType.GetProperties();

			var preText = "		";

			foreach (var propInfo in objProps)
			{
				if (searchableColumns.Contains(propInfo.Name))
				{
					var sqlType = GetSQLType(propInfo.PropertyType);

					var postText = "";
					if (sqlType.Contains("VARCHAR"))
					{
						postText = "''";
					}
					else
					{
						postText = "NULL";
					}

					script.AppendLine(preText + "@" + propInfo.Name + "			" + sqlType + "	= " + postText);
					preText = "	,	";
				}
			}

			script.AppendLine("	,	@AuditId				INT									");
			script.AppendLine("	,	@AuditDate				DATETIME	= NULL					");
			script.AppendLine("	,	@SystemEntityType		VARCHAR(50) = '" + EntityName + "'	");
			script.AppendLine("	,	@ApplicationMode		INT			= NULL	");
			script.AppendLine("	,	@AddAuditInfo			INT			= 1");
			script.AppendLine("	,	@AddTraceInfo			INT			= 0");
			script.AppendLine("	,	@ReturnAuditInfo		INT			= 0");
			script.AppendLine(")");

			script.AppendLine("WITH RECOMPILE");
			script.AppendLine("AS");
			script.AppendLine("BEGIN");
			script.AppendLine("");

			script.AppendLine("	SET  NOCOUNT ON");

			script.AppendLine("");
			script.AppendLine("	IF @AddTraceInfo = 1 ");
			script.AppendLine("		BEGIN ");

			// need to think about generating this dynamically
			script.AppendLine("");
			script.AppendLine("			DECLARE @InputParametersLocal	VARCHAR(500)  ");
			script.AppendLine("			DECLARE @InputValuesLocal		VARCHAR(5000)");

			script.AppendLine("");
			script.AppendLine("			SET @InputParametersLocal		= '" + EntityName + "Id' + ', ' + 'Name' + ', ' + '@Description' ");
			script.AppendLine("			SET @InputValuesLocal			= CAST(@" + EntityName + "Id AS VARCHAR(50)) + ', '+ ISNULL(@Name, 'NULL') + ', '+ ISNULL(@Description, 'NULL') ");

			script.AppendLine("");
			script.AppendLine("			EXEC dbo.StoredProcedureLogInsert");
			script.AppendLine("					@Name						= 'dbo." + EntityName + "Search'");
			script.AppendLine("				,	@InputParameters			= @InputParametersLocal");
			script.AppendLine("				,	@InputValues				= @InputValuesLocal");

			script.AppendLine("");
			script.AppendLine("		END	");

			script.AppendLine("");
			script.AppendLine("	-- Get Main System Entity Type ID");
			script.AppendLine("	DECLARE @SystemEntityTypeId AS INT");
			script.AppendLine("	SELECT @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)");

			script.AppendLine("");
			preText = "	SELECT	";

			foreach (var propInfo in objProps)
			{
				if (!lstExcludedProperties.Contains(propInfo.Name))
				{
					script.AppendLine(preText + propInfo.Name);
					preText = "		,	";
				}
			}
			script.AppendLine("	INTO	#TempMain");
			script.AppendLine("	FROM	dbo." + EntityName + " a	");

			preText = "	WHERE	";
			foreach (var propInfo in objProps)
			{
				if (searchableColumns.Contains(propInfo.Name))
				{
					var sqlType = GetSQLType(propInfo.PropertyType);
					if (sqlType.Contains("VARCHAR"))
					{
						script.AppendLine(preText + propInfo.Name + "	LIKE	@" + propInfo.Name + " + '%' ");
					}
					else if (sqlType.Contains("INT"))
					{
						script.AppendLine(preText + propInfo.Name + "	=	ISNULL(@" + propInfo.Name + ", " + propInfo.Name + ") ");
					}
					preText = "	AND		";
				}
			}

			script.AppendLine("	ORDER BY a." + EntityName + "Id	ASC");

			script.AppendLine("");
			script.AppendLine("	IF @ReturnAuditInfo = 1");
			script.AppendLine("		BEGIN");

			script.AppendLine("");
			script.AppendLine("			-- get Audit latest record matching on key, systementitytype");
			script.AppendLine("			SELECT	c.EntityKey");
			script.AppendLine("				,	MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'");
			script.AppendLine("			INTO		#HistortyInfo");
			script.AppendLine("			FROM 		#TempMain a");
			script.AppendLine("			INNER JOIN	CommonServices.dbo.AuditHistory c	");
			script.AppendLine("				ON	c.EntityKey			=	a." + EntityName + "Id");
			script.AppendLine("				AND c.SystemEntityId	=	@SystemEntityTypeId");
			script.AppendLine("				AND c.AuditActionId			IN (1,2)");
			script.AppendLine("			GROUP BY	c.EntityKey	");

			script.AppendLine("");
			script.AppendLine("			-- Get Audit Date and CreatedByPersonId for given records");
			script.AppendLine("			SELECT	a." + EntityName + "Id");
			script.AppendLine("				,	c.AuditActionId");
			script.AppendLine("				,	c.CreatedDate");
			script.AppendLine("				,	c.CreatedByPersonId	");
			script.AppendLine("				, 	c.CreatedDate						AS	'UpdatedDate'");
			script.AppendLine("				,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'");
			script.AppendLine("				,	d.Name								AS	'LastAction'");
			script.AppendLine("			INTO		#HistortyInfoDetails");
			script.AppendLine("			FROM		#TempMain a");
			script.AppendLine("			INNER JOIN	#HistortyInfo										b");
			script.AppendLine("				ON	b.EntityKey			= a." + EntityName + "Id");
			script.AppendLine("			INNER JOIN	CommonServices.dbo.AuditHistory	");
			script.AppendLine("				ON	c.AuditHistoryId	= b.MaxAuditHistoryId");
			script.AppendLine("			INNER JOIN	CommonServices.dbo.AuditAction");
			script.AppendLine("				ON	c.AuditActionId 	= d.AuditActionId");
			script.AppendLine("			INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e");
			script.AppendLine("				ON	c.CreatedByPersonId	= e.ApplicationUserId	");

			script.AppendLine("");
			script.AppendLine("			SELECT 	a.*");
			script.AppendLine("				, 	b.UpdatedDate");
			script.AppendLine("				,	b.UpdatedBy");
			script.AppendLine("				,	b.LastAction");
			script.AppendLine("			FROM #TempMain a");
			script.AppendLine("			LEFT JOIN #HistortyInfoDetails	b");
			script.AppendLine("				ON	a." + EntityName + "Id	= b." + EntityName + "Id");
			script.AppendLine("			ORDER BY	a." + EntityName + "Id				ASC");

			script.AppendLine("");
			script.AppendLine("		END");
			script.AppendLine("	ELSE");
			script.AppendLine("		BEGIN");

			script.AppendLine("");
			script.AppendLine("			SELECT 	a.*");
			script.AppendLine("				, 	UpdatedDate = '1/1/1900'");
			script.AppendLine("				,	UpdatedBy	= 'Unknown'");
			script.AppendLine("				,	LastAction	= 'Unknown'");
			script.AppendLine("			FROM #TempMain a");
			script.AppendLine("			ORDER BY	a." + EntityName + "Id				ASC");

			script.AppendLine("");
			script.AppendLine("		END");

			script.AppendLine("");
			script.AppendLine("	IF @AddAuditInfo = 1 ");
			script.AppendLine("		BEGIN");

			script.Append(GetAuditInfoText("Search"));

			script.AppendLine("");
			script.AppendLine("		END");

			script.AppendLine("");
			script.AppendLine("END");

			return script.ToString();

		}

		public virtual void DeployScript(string sql)
		{
			var DataStoreKey = SetupConfiguration.GetDataStoreKey(EntityName);
			DBDML.RunSQL("Deploy Script", sql, DataStoreKey);
		}
		 	
	}
}
