using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;using Shared.WebCommon.UI.Web;
using Framework.Components.Core;

namespace Shared.UI.Web.Import
{
    public partial class ImportCSV : BasePage
    {

        #region private methods

        private void SetupSystemEntityDropdown()
        {
            drpSystemEntity.Items.Clear();
			var dt = SystemEntityTypeDataManager.GetList(SessionVariables.RequestProfile);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                    drpSystemEntity.Items.Add(Convert.ToString(dr["EntityName"]));
            }
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetupSystemEntityDropdown();
            }
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            var strFPath = String.Empty;
            var strExt = String.Empty;

            if (CSVUpload.HasFile)
            {
                strFPath = Server.MapPath("~/Import/") + CSVUpload.FileName;
                strExt = Path.GetExtension(CSVUpload.FileName);
                CSVUpload.SaveAs(strFPath);
            }

            var lstColumns = new List<string>();
            var strTableName = drpSystemEntity.SelectedValue; // table name

            if (!string.IsNullOrEmpty(strFPath) && strExt.ToLower() == ".csv")
            {
                var sReader = new StreamReader(strFPath); // read csv file
                string line = null;
                var isHeader = true;
                var lstErrors = new List<string>();
                while ((line = sReader.ReadLine()) != null)
                {
                    if (isHeader) // check for header column
                    {
                        lstColumns = line.ToLower().Split(new char[] { ',' }).ToList(); // parse the columns
                        if (string.IsNullOrEmpty(lstColumns[lstColumns.Count - 1])) // check whether last column is null/empty
                            lstColumns.RemoveAt(lstColumns.Count - 1);
                        isHeader = false;
                    }
                    else
                    {
                        var strSQL = String.Empty; // sql string

                        var lstValues = line.Split(new char[] { ',' }).ToList(); // parse the values
                        if (lstValues.Count > 0)
                        {
                            for (var i = 0; i < lstColumns.Count; i++) // to add parameters to the sql string with values
                            {
                                if (i == 0) // first column
                                    strSQL = "@" + lstColumns[i] + " = '" + lstValues[i] + "'";
                                else if (i < lstValues.Count) // condition, if column count exceeding value count
                                {

                                    strSQL += ", @" + lstColumns[i] + " = '" + lstValues[i] + "'";
                                }
                            }


                            //if (!lstColumns.Contains(strTableName.ToLower() + "id")) // check if primary column present or not. if not add it to query
                            //    strSQL = "@" + strTableName.ToLower() + "id = NULL, " + strSQL;

							strSQL += ", @auditid = " + SessionVariables.RequestProfile.AuditId; // add aduit id
                            strSQL = strTableName + "Insert " + strSQL; // add procedure name to the sql query

                            //Response.Write(strSQL + "</br>");

                            try
                            {
                                DBDML.RunSQL(strTableName + ".Insert", strSQL, "Default"); // execute sql query
                            }
                            catch (Exception ex)
                            {
                                lstErrors.Add("SQL: " + strSQL + ", Exception: " + ex.Message);
                            }
                        }
                    }
                }
                sReader.Close();

                foreach (var strError in lstErrors)
                    Response.Write(strError + "<br/>");
            }
            else
            {
                throw new Exception("InValid file format (only .csv accepted)");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        #endregion

    }
}