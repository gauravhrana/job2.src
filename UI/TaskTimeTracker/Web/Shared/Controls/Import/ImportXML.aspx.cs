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
using System.Xml;
using Framework.Components.Core;

namespace Shared.UI.Web.Import
{
    public partial class ImportXML : BasePage
    {

        #region private methods

        private void SetupSystemEntityDropdown()
        {
            drpSystemEntity.Items.Clear();
			var lst = SystemEntityTypeDataManager.GetList(SessionVariables.RequestProfile);
            if (lst != null && lst.Count > 0)
            {
                foreach (var dr in lst)
                    drpSystemEntity.Items.Add(dr.EntityName);
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

            if (XMLUpload.HasFile)
            {
                strFPath = Server.MapPath("~/Import/") + XMLUpload.FileName;
                strExt = Path.GetExtension(XMLUpload.FileName);
                XMLUpload.SaveAs(strFPath);
            }

            var lstColumns = new List<string>();
            var strTableName = drpSystemEntity.SelectedValue; // table name

            if (!string.IsNullOrEmpty(strFPath) && strExt.ToLower() == ".xml")
            {
                var lstErrors = new List<string>();
                var doc = new XmlDocument();
                doc.Load(strFPath); // read xml file

                var documentElement = doc.DocumentElement;

                if (documentElement != null)
                {
                    //iterate through elements
                    foreach (XmlElement el in documentElement)
                    {
                        strTableName = el.Name;
                        var strSQL = String.Empty; // sql string
                        foreach (XmlElement xmlChildVal in el.ChildNodes) // to add parameters to the sql string with values
                        {
                            if (string.IsNullOrEmpty(strSQL))
                                strSQL = "@" + xmlChildVal.Name + " = '" + xmlChildVal.InnerText + "'";
                            else
                                strSQL += ", @" + xmlChildVal.Name + " = '" + xmlChildVal.InnerText + "'";
                        }

						strSQL += ", @auditid = " + SessionVariables.RequestProfile.AuditId; // add aduit id
                        strSQL = strTableName + "Insert " + strSQL; // add procedure name

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
                    foreach (var strError in lstErrors)
                        Response.Write(strError + "<br/>");
                }
            }
            else
            {
                throw new Exception("InValid file format (only .xml accepted)");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        #endregion

    }
}