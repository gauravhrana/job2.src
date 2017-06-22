using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DatabaseTool.UI.Desktop
{
    static class ClassGenerator
    {

        #region private methods

        private static string GetVariableTypeByDataType(string dataType)
        {
            var varType = string.Empty;
            if (dataType == "datetime")
            {
                varType = "DateTime?";
            }
            else if (dataType == "varchar" || dataType == "text" || dataType.Contains("varchar"))
            {
                varType = "string";
            }
            else if (dataType == "decimal" || dataType == "numeric")
            {
                varType = "Decimal?";
            }
            else
            {
                varType = "int?";
            }
            return varType;
        }

        private static string OLDConstructHelperClass(string tableName, DataTable dtColumns)
        {
            var strResult = new StringBuilder();

            #region "Constructing Using Clauses"

            strResult.AppendLine("using System;");
            strResult.AppendLine("using System.Collections.Generic;");
            strResult.AppendLine("using System.Linq;");
            strResult.AppendLine("using System.Text;\n");

            #endregion

            var tmpString = new StringBuilder();
            var tmpString2 = new StringBuilder();

            foreach (DataRow dr in dtColumns.Rows)
            {
                var columnName = Convert.ToString(dr["column_name"]);
                if (columnName.ToLower() != "applicationid")
                {
                    var dataType = Convert.ToString(dr["data_type"]);
                    var varType = GetVariableTypeByDataType(dataType.ToLower());
                    tmpString.AppendLine("\t\t\tpublic string " + columnName + " = \"" + columnName + "\";");
                    tmpString2.AppendLine("\t\t\tpublic " + varType + " " + columnName + ";");
                }
            }

            strResult.AppendLine("namespace Components.Core");
            strResult.AppendLine("{"); // name space start
            strResult.AppendLine("\tpublic partial class " + tableName);
            strResult.AppendLine("\t{"); // class start

            strResult.AppendLine("\n");
            strResult.AppendLine("\t\tpublic static DataColumns Columns = new DataColumns();\n");

            strResult.AppendLine("\t\tpublic class DataColumns");
            strResult.AppendLine("\t\t{"); // innner class start
            strResult.Append(tmpString.ToString());
            strResult.AppendLine("\t\t}\n"); // inner class end


            strResult.AppendLine("\t\tpublic struct Data");
            strResult.AppendLine("\t\t{"); // struct start
            strResult.Append(tmpString2.ToString());
            strResult.AppendLine("\t\t}\n"); // struct end            

            strResult.AppendLine("\t\tpublic string ToURLQuery()");
            strResult.AppendLine("\t\t{");
            strResult.AppendLine("\t\t\treturn string.Empty;");
            strResult.AppendLine("\t\t}\n");

            strResult.AppendLine("\t}"); // class end
            strResult.AppendLine("}"); // namespace end
            return strResult.ToString();
        }

        private static string OLDConstructClass(string tableName, DataTable dtColumns)
        {
            var strResult = new StringBuilder();

            #region "Constructing Using Clauses"

            strResult.AppendLine("using System;");
            strResult.AppendLine("using System.Collections.Generic;");
            strResult.AppendLine("using System.Linq;");
            strResult.AppendLine("using System.Text;");
            strResult.AppendLine("using System.Data;\n");

            #endregion

            strResult.AppendLine("namespace Components.Core");
            strResult.AppendLine("{"); // name space start
            strResult.AppendLine("\tpublic partial class " + tableName);
            strResult.AppendLine("\t{\n"); // class start

            #region Construct Private Members & Constructors

            strResult.AppendLine("\t\tstatic string DataStoreKey = \"\";");
            strResult.AppendLine("\t\tstatic int ApplicationId;\n");

            strResult.AppendLine("\t\tpublic " + tableName + "()");
            strResult.AppendLine("\t\t{");
            strResult.AppendLine("\t\t}\n");

            strResult.AppendLine("\t\tstatic " + tableName + "()");
            strResult.AppendLine("\t\t{");
            strResult.AppendLine("\t\t\tApplicationId = SetupConfiguration.ApplicationId;");
            strResult.AppendLine("\t\t\tDataStoreKey = SetupConfiguration.GetDataStoreKey(\"" + tableName + "\");");
            strResult.AppendLine("\t\t}\n");

            #endregion

            #region Construct Formulate Parameters Method

            strResult.AppendLine("\t\t#region private methods\n");
            strResult.AppendLine("\t\tprivate static string[] FormulateParameters(Data data)");
            strResult.AppendLine("\t\t{");

            var tmpReturnString = string.Empty;
            foreach (DataRow dr in dtColumns.Rows)
            {
                var columnName = Convert.ToString(dr["column_name"]);
                if (columnName.ToLower() != "applicationid")
                {
                    var lColumnName = columnName[0].ToString().ToLower() + columnName.Substring(1);
                    strResult.AppendLine("\t\t\tvar " + lColumnName + " = \"NULL\";");

                    var dataType = Convert.ToString(dr["data_type"]);
                    var varType = GetVariableTypeByDataType(dataType.ToLower());
                    if (varType == "string")
                    {
                        strResult.AppendLine("\t\t\tif (!string.IsNullOrEmpty(data." + columnName + "))");
                        strResult.AppendLine("\t\t\t{");
                        strResult.AppendLine("\t\t\t\t" + lColumnName + " = \"'\" + data." + columnName + ".Trim() + \"'\";");
                        strResult.AppendLine("\t\t\t}\n");
                    }
                    else
                    {
                        strResult.AppendLine("\t\t\tif(data." + columnName + " != null)");
                        strResult.AppendLine("\t\t\t{");
                        strResult.AppendLine("\t\t\t\t" + lColumnName + " = data." + columnName + ".ToString();");
                        strResult.AppendLine("\t\t\t}\n");
                    }
                    if (string.IsNullOrEmpty(tmpReturnString))
                    {
                        tmpReturnString = lColumnName;
                    }
                    else
                    {
                        tmpReturnString += ", " + lColumnName;
                    }
                }
            }

            strResult.AppendLine("\t\t\treturn new string[] { " + tmpReturnString + " };");

            strResult.AppendLine("\t\t}\n");
            strResult.AppendLine("\t\t#endregion\n");

            #endregion

            #region Construct GetList Method

            strResult.AppendLine("\t\t#region GetList\n");
            strResult.AppendLine("\t\tstatic public DataTable GetList(int auditId)");
            strResult.AppendLine("\t\t{");
            strResult.AppendLine("\t\t\tvar sql = \"EXEC dbo." + tableName + "List  @AuditId=\" + Convert.ToString(auditId);");
            strResult.AppendLine("\t\t\tvar oDT = new Framework.Components.DataAccess.DBDataTable(\"" + tableName + ".List\", sql, DataStoreKey);");
            strResult.AppendLine("\t\t\treturn oDT.DBTable;");
            strResult.AppendLine("\t\t}\n");
            strResult.AppendLine("\t\t#endregion\n");

            #endregion

            #region Construct GetDetails Method

            strResult.AppendLine("\t\t#region GetDetails\n");
            strResult.AppendLine("\t\tstatic public DataTable GetDetails(Data data, int auditId)");
            strResult.AppendLine("\t\t{");
            strResult.AppendLine("\t\t\tsql = \"EXEC dbo." + tableName + "Details \" +");
            strResult.AppendLine("\t\t\t\t\t  \"@" + tableName + "Id = \" + data." + tableName + "Id.ToString() +");
            strResult.AppendLine("\t\t\t\t\t  \", @AuditId = \" + auditId.ToString();\n");
            strResult.AppendLine("\t\t\tvar oDT = new Framework.Components.DataAccess.DBDataTable(\"" + tableName + ".Details\", sql, DataStoreKey);");
            strResult.AppendLine("\t\t\treturn oDT.DBTable;");
            strResult.AppendLine("\t\t}\n");
            strResult.AppendLine("\t\t#endregion\n");

            #endregion

            #region Construct Create Method

            strResult.AppendLine("\t\t#region Create\n");
            strResult.AppendLine("\t\tstatic public void Create(Data data, int auditId)");
            strResult.AppendLine("\t\t{");
            strResult.AppendLine("\t\t\tCreateOrUpdate(data, auditId, \"Create\");");
            strResult.AppendLine("\t\t}\n");
            strResult.AppendLine("\t\t#endregion\n");

            #endregion

            #region Construct Update Method

            strResult.AppendLine("\t\t#region Update\n");
            strResult.AppendLine("\t\tstatic public void Update(Data data, int auditId)");
            strResult.AppendLine("\t\t{");
            strResult.AppendLine("\t\t\tCreateOrUpdate(data, auditId, \"Update\");");
            strResult.AppendLine("\t\t}\n");
            strResult.AppendLine("\t\t#endregion\n");

            #endregion

            #region Construct Delete Method

            strResult.AppendLine("\t\t#region Delete\n");
            strResult.AppendLine("\t\tstatic public void Delete(Data data, int auditId)");
            strResult.AppendLine("\t\t{");
            strResult.AppendLine("\t\t\tsql = \"EXEC dbo." + tableName + "Delete \" +");
            strResult.AppendLine("\t\t\t\t\t  \"@" + tableName + "Id = \" + data." + tableName + "Id.ToString() +");
            strResult.AppendLine("\t\t\t\t\t  \", @AuditId = \" + auditId.ToString();");
            strResult.AppendLine("\t\t\tDataAccess.DBDML.RunSQL(\"" + tableName + ".Delete\", sql, DataStoreKey);");
            strResult.AppendLine("\t\t}\n");
            strResult.AppendLine("\t\t#endregion\n");

            #endregion

            #region Construct Search Method

            strResult.AppendLine("\t\t#region Search\n");
            strResult.AppendLine("\t\tstatic public DataTable Search(Data data, int auditId)");
            strResult.AppendLine("\t\t{");

            strResult.AppendLine("\t\t\tvar parameters = FormulateParameters(data);");
            strResult.AppendLine("\t\t\tvar sql = string.Format(\"EXEC dbo." + tableName + "Search @" + tableName + "Id={0}, @AuditId={1}\", parameters[0], auditId);");
            strResult.AppendLine("\t\t\tvar oDT = new Framework.Components.DataAccess.DBDataTable(\"" + tableName + ".Search\", sql, DataStoreKey);");
            strResult.AppendLine("\t\t\treturn oDT.DBTable;");

            strResult.AppendLine("\t\t}\n");
            strResult.AppendLine("\t\t#endregion\n");

            #endregion

            #region Construct CreateOrUpdate Method

            strResult.AppendLine("\t\t#region CreateOrUpdate\n");
            strResult.AppendLine("\t\tstatic private void CreateOrUpdate(Data data, int auditId, string sqlcmd)");
            strResult.AppendLine("\t\t{");

            strResult.AppendLine("\t\t\tvar parameters = FormulateParameters(data);");
            strResult.AppendLine("\t\t\tvar sql = \"EXEC \";\n");

            strResult.AppendLine("\t\t\tif (sqlcmd == \"Create\")");
            strResult.AppendLine("\t\t\t{");
            strResult.AppendLine("\t\t\t\tsql = sql + \"dbo." + tableName + "Insert  \";");
            strResult.AppendLine("\t\t\t}");
            strResult.AppendLine("\t\t\telse");
            strResult.AppendLine("\t\t\t{");
            strResult.AppendLine("\t\t\t\tsql = sql + \"dbo." + tableName + "Update  \";");
            strResult.AppendLine("\t\t\t}");

            strResult.AppendLine("\t\t\tsql = sql + \"  @\" + Columns." + tableName + "Id				 + \" = {0}\" +");
            var iCount = 0;
            foreach (DataRow dr in dtColumns.Rows)
            {
                if (iCount == 0)
                {
                    iCount++;
                    continue;
                }
                var columnName = Convert.ToString(dr["column_name"]);
                if (columnName.ToLower() == "applicationid")
                {
                    continue;
                }
                strResult.AppendLine("\t\t\t\t\t\t\", @\" + Columns." + columnName + "					 + \" = {" + iCount.ToString() + "}\" +");
                iCount++;
            }
            strResult.AppendLine("\t\t\t\t\t\t\", @ApplicationId = {" + iCount.ToString() + "}\" +");
            strResult.AppendLine("\t\t\t\t\t\t\", @AuditId = {" + (iCount + 1).ToString() + "}\" ");

            var tmpString = string.Empty;
            for (int i = 0; i < iCount; i++)
                tmpString += "parameters[" + Convert.ToString(i) + "], ";
            strResult.AppendLine("\t\t\tsql = string.Format(sql, " + tmpString + "ApplicationId, auditId);");
            strResult.AppendLine("\t\t\tFramework.Components.DataAccess.DBDML.RunSQL(\"" + tableName + ".Insert/Update\", sql, DataStoreKey);");

            strResult.AppendLine("\t\t}\n");
            strResult.AppendLine("\t\t#endregion\n");

            #endregion

            strResult.AppendLine("\t}"); // class end
            strResult.AppendLine("}"); // namespace end
            return strResult.ToString();
        }

        #endregion

        #region public methods

        public static string ConstructHelperClass(string tableName, DataTable dtColumns)
        {
            var strResult = new StringBuilder();

            #region "Constructing Using Clauses"

            strResult.AppendLine("using System;");
            strResult.AppendLine("using System.Collections.Generic;");
            strResult.AppendLine("using System.Linq;");
            strResult.AppendLine("using System.Text;\n");

            #endregion

            var tmpString = new StringBuilder();
            var tmpString2 = new StringBuilder();

            foreach (DataRow dr in dtColumns.Rows)
            {
                var columnName = Convert.ToString(dr["column_name"]);
                if (columnName.ToLower() != "applicationid")
                {
                    var dataType = Convert.ToString(dr["data_type"]);
                    var varType = GetVariableTypeByDataType(dataType.ToLower());
                    tmpString.AppendLine("\t\t\tpublic const string " + columnName + " = \"" + columnName + "\";");
                    tmpString2.AppendLine("\t\t\tpublic " + varType + " " + columnName + ";");
                }
            }

            strResult.AppendLine("namespace TaskTimeTracker.Components.BusinessLayer");
            strResult.AppendLine("{"); // name space start
            strResult.AppendLine("\tpublic partial class " + tableName);
            strResult.AppendLine("\t{"); // class start

            strResult.AppendLine("\t\tpublic static class DataColumns");
            strResult.AppendLine("\t\t{"); // innner class start
            strResult.Append(tmpString.ToString());
            strResult.AppendLine("\t\t}\n"); // inner class end


            strResult.AppendLine("\t\tpublic struct Data");
            strResult.AppendLine("\t\t{"); // struct start
            strResult.Append(tmpString2.ToString() + "\n");


            strResult.AppendLine("\t\tpublic string ToURLQuery()");
            strResult.AppendLine("\t\t{");
            strResult.AppendLine("\t\t\treturn string.Empty;");
            strResult.AppendLine("\t\t}\n");

            strResult.AppendLine("\t\tpublic string ToSQLParameter(string dataColumnName)");
            strResult.AppendLine("\t\t{");
            strResult.AppendLine("\t\t\t// by default assume NULL as the parmater to pass to uderlying stored procedure");
            strResult.AppendLine("\t\t\tvar returnValue = \"NULL\";\n");
            strResult.AppendLine("\t\t\tswitch (dataColumnName)");
            strResult.AppendLine("\t\t\t{\n");

            foreach (DataRow dr in dtColumns.Rows)
            {
                var columnName = Convert.ToString(dr["column_name"]);
                if (columnName.ToLower() != "applicationid")
                {
                    var dataType = Convert.ToString(dr["data_type"]);
                    var varType = GetVariableTypeByDataType(dataType.ToLower());

                    strResult.AppendLine("\t\t\t\tcase \"" + columnName + "\":");
                    if (varType != "string")
                    {
                        strResult.AppendLine("\t\t\t\tif (" + columnName + " != null)");
                        strResult.AppendLine("\t\t\t\t{");
                        strResult.AppendLine("\t\t\t\t\treturnValue =  \"@" + columnName + " = \"	+ " + columnName + ";");
                        strResult.AppendLine("\t\t\t\t}");

                    }
                    else
                    {
                        strResult.AppendLine("\t\t\t\tif (!string.IsNullOrEmpty(" + columnName + "))");
                        strResult.AppendLine("\t\t\t\t{");
                        strResult.AppendLine("\t\t\t\t\treturnValue = \"@" + columnName + " = '\" + " + columnName + ".Trim() + \"'\";");
                        strResult.AppendLine("\t\t\t\t}");
                    }

                    strResult.AppendLine("\t\t\t\telse");
                    strResult.AppendLine("\t\t\t\t{");
                    strResult.AppendLine("\t\t\t\t\treturnValue = \"@" + columnName + " = \" +  returnValue;");
                    strResult.AppendLine("\t\t\t\t}");
                    strResult.AppendLine("\t\t\t\tbreak;\n");
                }
            }


            strResult.AppendLine("\t\t\t}");

            strResult.AppendLine("\t\t\treturn returnValue;");
            strResult.AppendLine("\t\t}\n");

            strResult.AppendLine("\t\t}\n"); // struct end

            strResult.AppendLine("\t}"); // class end
            strResult.AppendLine("}"); // namespace end
            return strResult.ToString();
        }

        public static string ConstructClass(string tableName, DataTable dtColumns)
        {
            var strResult = new StringBuilder();

            #region "Constructing Using Clauses"

            strResult.AppendLine("using System;");
            strResult.AppendLine("using System.Collections.Generic;");
            strResult.AppendLine("using System.Linq;");
            strResult.AppendLine("using System.Text;");
            strResult.AppendLine("using System.Data;");
            strResult.AppendLine("using Framework.Components.DataAccess;\n");

            #endregion

            strResult.AppendLine("namespace TaskTimeTracker.Components.BusinessLayer");
            strResult.AppendLine("{"); // name space start
            strResult.AppendLine("\tpublic partial class " + tableName + " : BaseClass");
            strResult.AppendLine("\t{\n"); // class start

            #region Construct Private Members & Constructors

            strResult.AppendLine("\t\tstatic readonly string DataStoreKey = \"\";");
            strResult.AppendLine("\t\tstatic readonly int ApplicationId;\n");

            strResult.AppendLine("\t\tstatic " + tableName + "()");
            strResult.AppendLine("\t\t{");
            strResult.AppendLine("\t\t\tApplicationId = SetupConfiguration.ApplicationId;");
            strResult.AppendLine("\t\t\tDataStoreKey = SetupConfiguration.GetDataStoreKey(\"" + tableName + "\");");
            strResult.AppendLine("\t\t}\n");

            #endregion

            #region Construct GetList Method

            strResult.AppendLine("\t\t#region GetList\n");
            strResult.AppendLine("\t\tstatic public DataTable GetList(int auditId)");
            strResult.AppendLine("\t\t{");
            strResult.AppendLine("\t\t\tvar sql = \"EXEC dbo." + tableName + "List\" \t\t+");
            strResult.AppendLine("\t\t\t\t\" \"	+ BaseColumns.ToSQLParameter(BaseColumns.AuditId, auditId)	            +");
            strResult.AppendLine("\t\t\t\t\", \"	+ BaseColumns.ToSQLParameter(BaseColumns.ApplicationId, ApplicationId)	;");

            strResult.AppendLine("\n\t\t\tvar oDT = new Framework.Components.DataAccess.DBDataTable(\"" + tableName + ".List\", sql, DataStoreKey);");
            strResult.AppendLine("\t\t\treturn oDT.DBTable;");
            strResult.AppendLine("\t\t}\n");
            strResult.AppendLine("\t\t#endregion\n");

            #endregion

            #region Construct GetDetails Method

            strResult.AppendLine("\t\t#region GetDetails\n");
            strResult.AppendLine("\t\tstatic public DataTable GetDetails(Data data, int auditId)");
            strResult.AppendLine("\t\t{");
            strResult.AppendLine("\t\t\tvar sql = \"EXEC dbo." + tableName + "Details \" +");
            strResult.AppendLine("\t\t\t\t\" \" + BaseColumns.ToSQLParameter(BaseColumns.AuditId, auditId) + ");
            strResult.AppendLine("\t\t\t\t\", \" + data.ToSQLParameter(DataColumns." + tableName + "Id);");
            strResult.AppendLine("\n\t\t\tvar oDT = new Framework.Components.DataAccess.DBDataTable(\"" + tableName + ".Details\", sql, DataStoreKey);");
            strResult.AppendLine("\t\t\treturn oDT.DBTable;");
            strResult.AppendLine("\t\t}\n");
            strResult.AppendLine("\t\t#endregion\n");

            #endregion

            #region Construct Create Method

            strResult.AppendLine("\t\t#region Create\n");
            strResult.AppendLine("\t\tstatic public void Create(Data data, int auditId)");
            strResult.AppendLine("\t\t{");
            strResult.AppendLine("\t\t\tvar sql = Save(data, auditId, \"Create\");");
            strResult.AppendLine("\t\t\tFramework.Components.DataAccess.DBDML.RunSQL(\"" + tableName + ".Insert\", sql, DataStoreKey);");
            strResult.AppendLine("\t\t}\n");
            strResult.AppendLine("\t\t#endregion\n");

            #endregion

            #region Construct Update Method

            strResult.AppendLine("\t\t#region Update\n");
            strResult.AppendLine("\t\tstatic public void Update(Data data, int auditId)");
            strResult.AppendLine("\t\t{");
            strResult.AppendLine("\t\t\tvar sql = Save(data, auditId, \"Update\");");
            strResult.AppendLine("\t\t\tFramework.Components.DataAccess.DBDML.RunSQL(\"" + tableName + ".Update\", sql, DataStoreKey);");
            strResult.AppendLine("\t\t}\n");
            strResult.AppendLine("\t\t#endregion\n");

            #endregion

            #region Construct Delete Method

            strResult.AppendLine("\t\t#region Delete\n");
            strResult.AppendLine("\t\tstatic public void Delete(Data data, int auditId)");
            strResult.AppendLine("\t\t{");
            strResult.AppendLine("\t\t\tvar sql = \"EXEC dbo." + tableName + "Delete \" \t\t\t+");

            strResult.AppendLine("\t\t\t\t\" \" + BaseColumns.ToSQLParameter(BaseColumns.AuditId, auditId) \t\t+ ");
            strResult.AppendLine("\t\t\t\t\", \" + data.ToSQLParameter(DataColumns." + tableName + "Id) \t\t;");

            strResult.AppendLine("\n\t\t\tFramework.Components.DataAccess.DBDML.RunSQL(\"" + tableName + ".Delete\", sql, DataStoreKey);");
            strResult.AppendLine("\t\t}\n");
            strResult.AppendLine("\t\t#endregion\n");

            #endregion

            #region Construct Search Method

            strResult.AppendLine("\t\t#region Search\n");
            strResult.AppendLine("\t\tstatic public DataTable Search(Data data, int auditId)");
            strResult.AppendLine("\t\t{");

            strResult.AppendLine("\t\t\t// formulate SQL");
            strResult.AppendLine("\t\t\tvar sql = \"EXEC dbo." + tableName + "Search \" +");
            strResult.AppendLine("\t\t\t\t\" \" + BaseColumns.ToSQLParameter(BaseColumns.AuditId, auditId) +");
            strResult.AppendLine("\t\t\t\t\", \" + BaseColumns.ToSQLParameter(BaseColumns.ApplicationId, ApplicationId) +");
            strResult.AppendLine("\t\t\t\t\", \" + data.ToSQLParameter(DataColumns." + tableName + "Id);");

            strResult.AppendLine("\n\t\t\tvar oDT = new Framework.Components.DataAccess.DBDataTable(\"" + tableName + ".Search\", sql, DataStoreKey);");
            strResult.AppendLine("\t\t\treturn oDT.DBTable;");

            strResult.AppendLine("\t\t}\n");
            strResult.AppendLine("\t\t#endregion\n");

            #endregion

            #region Construct Save Method

            strResult.AppendLine("\t\t#region Save\n");
            strResult.AppendLine("\t\tstatic private string Save(Data data, int auditId, string sqlcmd)");
            strResult.AppendLine("\t\t{");

            strResult.AppendLine("\t\t\tvar sql = \"EXEC \";\n");

            strResult.AppendLine("\t\t\tswitch (sqlcmd)");
            strResult.AppendLine("\t\t\t{");

            strResult.AppendLine("\t\t\t\tcase \"Create\":");
            strResult.AppendLine("\t\t\t\t\tsql += \"dbo." + tableName + "Insert  \" + ");
            strResult.AppendLine("\t\t\t\t\t\t\" \" + BaseColumns.ToSQLParameter(BaseColumns.AuditId, auditId) +");
            strResult.AppendLine("\t\t\t\t\t\t\", \" + BaseColumns.ToSQLParameter(BaseColumns.ApplicationId, ApplicationId);");
            strResult.AppendLine("\t\t\t\t\tbreak;\n");

            strResult.AppendLine("\t\t\t\tcase \"Update\":");
            strResult.AppendLine("\t\t\t\t\tsql += \"dbo." + tableName + "Update  \" + ");
            strResult.AppendLine("\t\t\t\t\t\t\" \" + BaseColumns.ToSQLParameter(BaseColumns.AuditId, auditId);");
            strResult.AppendLine("\t\t\t\t\tbreak;\n");

            strResult.AppendLine("\t\t\t\tdefault:");
            strResult.AppendLine("\t\t\t\t\tbreak;\n");

            strResult.AppendLine("\t\t\t}");

            strResult.AppendLine("\n\t\t\tsql = sql + \", \" + data.ToSQLParameter(DataColumns." + tableName + "Id) +");
            var iCount = 0;
            foreach (DataRow dr in dtColumns.Rows)
            {
                var columnName = Convert.ToString(dr["column_name"]);
                if (iCount == 0 || columnName.ToLower() == "applicationid")
                {
                    if (iCount == dtColumns.Rows.Count - 1)
                        strResult.AppendLine("\t\t\t\t\t\t\" \";");
                    iCount++;
                    continue;
                }

                if (iCount == dtColumns.Rows.Count - 1)
                {
                    strResult.AppendLine("\t\t\t\t\t\t\", \" + data.ToSQLParameter(DataColumns." + columnName + ");");
                }
                else
                {
                    strResult.AppendLine("\t\t\t\t\t\t\", \" + data.ToSQLParameter(DataColumns." + columnName + ") +");
                }

                iCount++;
            }

            strResult.AppendLine("\t\t\treturn sql;");

            strResult.AppendLine("\t\t}\n");
            strResult.AppendLine("\t\t#endregion\n");

            #endregion

            #region Construct DoesExist Method

            strResult.AppendLine("\t\t#region DoesExist\n");
            strResult.AppendLine("\t\tstatic public DataTable DoesExist(Data data, int auditId)");
            strResult.AppendLine("\t\t{");

            strResult.AppendLine("\t\t\tvar sql = \"EXEC dbo." + tableName + "DoesExist \" +");
            strResult.AppendLine("\t\t\t\" \" + BaseColumns.ToSQLParameter(BaseColumns.AuditId, auditId) + ");
            strResult.AppendLine("\t\t\t\", \" + BaseColumns.ToSQLParameter(BaseColumns.ApplicationId, ApplicationId) +");
            if (dtColumns.Rows.Count > 1 && Convert.ToString(dtColumns.Rows[1]["column_name"]).ToLower() != "applicationid")
            {
                strResult.AppendLine("\t\t\t\t\", \" + data.ToSQLParameter(DataColumns." + tableName + "Id) +");
                strResult.AppendLine("\t\t\t\t\", \" + data.ToSQLParameter(DataColumns." + Convert.ToString(dtColumns.Rows[1]["column_name"]) + ");");
            }
            else
            {
                strResult.AppendLine("\t\t\t\t\", \" + data.ToSQLParameter(DataColumns." + tableName + "Id);");
            }
            strResult.AppendLine("\n\t\t\tvar oDT = new Framework.Components.DataAccess.DBDataTable(\"" + tableName + ".DoesExist\", sql, DataStoreKey);");
            strResult.AppendLine("\t\t\treturn oDT.DBTable;");
            strResult.AppendLine("\t\t}\n");
            strResult.AppendLine("\t\t#endregion\n");

            #endregion

            strResult.AppendLine("\t}"); // class end
            strResult.AppendLine("}"); // namespace end
            return strResult.ToString();
        }

        #endregion

    }
}
