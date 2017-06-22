using DataModel.Framework.Core;
using Framework.Components.Core;
using Shared.WebCommon.UI.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Data;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace Web.Api.Controllers
{
    public class DatabaseCleanupController : ApiController
    {

        [System.Web.Http.AcceptVerbs("GET")]
        public IEnumerable<ConnectionStringXApplicationDataModel> ListDatabaseNames()
        {
            var data = new ConnectionStringXApplicationDataModel();
            var listConnectionStrings = ConnectionStringXApplicationDataManager.GetEntityDetails(data, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfo);
            return listConnectionStrings;
        }

        [System.Web.Http.AcceptVerbs("GET")]
        public IEnumerable<string> GetDatabaseObjects(string value1, string value2, string value3)
        {
            var objectName = new JavaScriptSerializer().Deserialize<string>(value1);
            var objectType = value2;
            var databaseName = value3;

            var sql = string.Empty;
            var result = new List<string>();

            if (objectType == "p")
            {

                sql = "	SELECT * FROM sysobjects"
                        + "	WHERE	type =		'" + objectType + "' "
                        + " AND		name LIKE	'%" + objectName + "%'";
                
            }
            else
            {
                sql = " SELECT  o.name   " + 
                    "   from sys.sql_expression_dependencies ed " +
                    "   JOIN sys.objects o on ed.referencing_id = o.object_id " +
                    "   WHERE ed.referenced_id is NULL AND ed.referenced_database_name IS NULL" +
                    "   AND	o.name LIKE	'%" + objectName + "%' " +
                    "   AND  LEFT(o.name, 3) NOT IN ('sp_', 'xp_', 'ms_') "  +
                    "   ORDER BY o.name";
            }

            var oDT = new Framework.Components.DataAccess.DBDataTable("Search", sql, databaseName);

            result = oDT.DBTable.AsEnumerable().Select(a => a["Name"].ToString()).ToList();

            return result;
        }


		[System.Web.Http.AcceptVerbs("GET")]
		public IEnumerable<string> GetDatabaseObjectList()
		{
			
			var result = new List<string>();

			var sql = "SELECT top 5 name as 'Name' FROM sysobjects"
				 + "	WHERE	type =		'P' ";
					


			var oDT = new Framework.Components.DataAccess.DBDataTable("Search", sql, "Configuration");

			result = oDT.DBTable.AsEnumerable().Select(a => a["Name"].ToString()).ToList();

			return result;
		}

        [System.Web.Http.AcceptVerbs("GET")]
        public IEnumerable<string> GetDatabaseObjectText(string value1, string value2, string value3)
        {
            var objectName = value1;
            var objectType = value2;
            var databaseName = value3;
            var objectText = string.Empty;

            if (!string.IsNullOrEmpty(databaseName) && (objectType == "p" || objectType == "ip"))
            {
                var sql = "	EXEC sp_helptext N'" + objectName + "'";
                var oDT = new Framework.Components.DataAccess.DBDataTable("ProcedureText", sql, databaseName);
                if (oDT.DBTable != null && oDT.DBTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in oDT.DBTable.Rows)
                    {
                        objectText += dr[0].ToString();
                    }
                }
            }
            var result = new List<string>();
            result.Add(objectText);

            return result;
        }

        [System.Web.Http.AcceptVerbs("GET")]
        public bool DropDatabaseObject(string value1, string value2, string value3)
        {
            var objectName = value1;
            var objectType = value2;
            var databaseName = value3;
            var procNames = new List<string>();
            if (!string.IsNullOrEmpty(objectName))
            {
                procNames = objectName.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                foreach (var procName in procNames)
                {                    
                    // drop procedure code
                    var dbName = databaseName;
                    var sql = "DROP Procedure " + procName;
                    Framework.Components.DataAccess.DBDML.RunSQL("Drop Procedure", sql, dbName);
                }
            }
            return true;
        }

	}
}