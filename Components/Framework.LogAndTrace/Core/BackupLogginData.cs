using Framework.Components.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Components.LogAndTrace
{
    public class BackupLogginDataManager : BaseDataManager
    {

        static readonly string DataStoreKey = "";

        static BackupLogginDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("Log4Net");
		}

        public static DataTable GetTableRecords()
        {
            var sql = string.Empty;

            sql = "SELECT	o.NAME " +
	            ",	i.rowcnt  " +
                "FROM sysindexes AS i " +
                "   INNER JOIN sysobjects AS o ON i.id = o.id  " +
                "WHERE i.indid < 2  AND OBJECTPROPERTY(o.id, 'IsMSShipped') = 0 " +
                "AND ( o.name LIKE 'Log4Net%' OR o.name LIKE 'UserLogin%' ) " +
                "AND o.name <> 'UserLoginStatus'  " +
                "ORDER BY o.NAME ";

            var oDT = new Framework.Components.DataAccess.DBDataTable("Get List", sql, DataStoreKey);

            return oDT.DBTable;

        }

        public static int GetRecordCount(string sourceTable, DateTime? recordDate)
        {
            var recordCount = 0;
            var SQLKey = ".SQL.GetTableRecordCount.sql";

            var srcColumn = "Date";

            if (sourceTable == "UserLogin")
            {
                srcColumn = "RecordDate";
            }
            else if (sourceTable == "UserLoginHistory")
            {
                srcColumn = "DateVisited";
            }

            // Get SQL Template and replace parameters
            var assembly = Assembly.GetExecutingAssembly();
            var scriptTemplate = GetResourceText(assembly, SQLKey);

            var replacementFieldSet = new Dictionary<string, string>();
            var replacementOtherSet = new Dictionary<string, string>();

            replacementOtherSet.Add("@SourceTableName@", sourceTable);
            replacementOtherSet.Add("@SourceColumnName@", srcColumn);

            if (recordDate != null)
            {
                replacementOtherSet.Add("@RecordDate@", recordDate.Value.ToString());
            }
            else
            {
                replacementOtherSet.Add("'@RecordDate@'", "NULL");
                replacementOtherSet.Add("<", "=");
            }

            var sql = GetSQL(replacementFieldSet, replacementOtherSet, scriptTemplate, assembly.GetName().Name + SQLKey);

            var result = DBDML.RunScalarSQL("GetCounts", sql, DataStoreKey);

            recordCount = int.Parse(result.ToString());

            return recordCount;
        }
    }
}
