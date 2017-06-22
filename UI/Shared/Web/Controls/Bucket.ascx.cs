using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace Shared.UI.Web.Controls
{
    public partial class Bucket : System.Web.UI.UserControl
    {

        #region properties

        public delegate DataTable GetListDelegate();
        public delegate DataTable GetDataDelegate(int setId);
        public delegate void GetSaveDelegate(int pkId, List<int> fkIds);

        private GetListDelegate _getList;
        private GetDataDelegate _getData;
        private GetSaveDelegate _save;

        private int PrimaryKeyId
        {
            get
            {
                return Convert.ToInt32(ViewState["PKId"]);
            }
            set
            {
                ViewState["PKId"] = value;
            }
        }

        private string EntityName
        {
            get
            {
                return Convert.ToString(ViewState["EntityName"]);
            }
            set
            {
                ViewState["EntityName"] = value;
            }
        }

        private DataTable SourceTable
        {
            get
            {
                return (DataTable)ViewState["SourceTable"];
            }
            set
            {
                ViewState["SourceTable"] = value;
            }
        }

        private string SourceTextColumn
        {
            get
            {
                return Convert.ToString(ViewState["SourceTextColumn"]);
            }
            set
            {
                ViewState["SourceTextColumn"] = value;
            }
        }

        #endregion

        #region methods

        private void SwitchValues(ListBox source, ListBox target)
        {
            var listRemoval = new System.Collections.ArrayList();

            // Find the number of items selected in the List and items selected
            // Call the move function equal to the number of items selected
            // Remove from Source list, The items moved

            // iterate through source list
            foreach (ListItem itemCurrent in source.Items)
            {
                if (itemCurrent.Selected == true)
                {
                    // 1. DETERIMNE - find out which item(s) was selected of SOURCE LIST
                    //Response.Write(itemCurrent.ToString());

                    // 2. MOVE / COPY - Add it to TARGET LIST
                    var copy = new ListItem(itemCurrent.Text, itemCurrent.Value);
                    target.Items.Add(copy);

                    // 3. REMOVE - Add to external list so we can remove afterwards from the source
                    listRemoval.Add(itemCurrent);

                    // 4. Set the moved selection as selected, so quickly can move back
                    // avoiding the user from reselecting items, disable any preveiously selected items     
                    if (target.SelectedItem != null)
                    {
                        target.SelectedItem.Selected = false;
                    }
                    target.Items.FindByValue(copy.Value).Selected = true;
                }
            }

            foreach (ListItem itemToRemove in listRemoval)
            {
                source.Items.Remove(itemToRemove);
            }
        }

        private void LoadSource()
        {
            if (SourceTable == null)
            {
                SourceTable = _getList();
            }

            var dt = SourceTable;
            if (string.IsNullOrEmpty(SourceTextColumn))
            {
                SourceTextColumn = "Name";
                if (!dt.Columns.Contains("Name"))
                {
                    for (var iCount = 1; iCount < dt.Columns.Count; iCount++)
                    {
                        if (dt.Columns[iCount].ColumnName != "ApplicationId")
                        {
                            SourceTextColumn = dt.Columns[iCount].ColumnName;
                            break;
                        }
                    }
                }
            }

            lstSource.DataSource = dt;
            lstSource.DataTextField = SourceTextColumn;
            lstSource.DataValueField = dt.Columns.Count > 0 ? dt.Columns[0].ColumnName : "Id";
            lstSource.DataBind();
        }

        private void LoadCurrentTarget()
        {
            var dt = _getData(PrimaryKeyId);
            foreach (DataRow dr in dt.Rows)
            {
                var lstItem = new ListItem(Convert.ToString(dr[EntityName]), Convert.ToString(dr[EntityName + "Id"]));
                if (lstSource.Items.Contains(lstItem))
                {
                    lstSource.Items.Remove(lstItem);
                }
                lstTarget.Items.Add(lstItem);
            }
        }

        public void ConfigureBucket(string strEntity, int setId, int TabCount,
            GetListDelegate getListDelegate, GetDataDelegate getDataDelegate, GetSaveDelegate saveDelegate)
        {
            EntityName = strEntity;
            lblCurrentTitle.Text = "Currently Associated " + EntityName + "(s) :";
            lblPossibleTitle.Text = "Possible " + EntityName + "(s) :";
            PrimaryKeyId = setId;
            _getList = getListDelegate;
            _getData = getDataDelegate;
            _save = saveDelegate;

            LoadSource();
            LoadCurrentTarget();
        }

        private void ClearItems(ListBox lstBox)
        {
            if (lstBox.Items.Count > 0)
            {
                lstBox.Items.Clear();
            }
        }

        public void ReloadBuckerList()
        {
            ClearItems(lstSource);
            ClearItems(lstTarget);

            LoadSource();
            LoadCurrentTarget();
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLeft_Click(object sender, EventArgs e)
        {
            SwitchValues(lstSource, lstTarget);
        }

        protected void btnRight_Click(object sender, EventArgs e)
        {
            SwitchValues(lstTarget, lstSource);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var lst = new List<int>();
            foreach (ListItem itemCurrent in lstTarget.Items)
            {
                lst.Add(int.Parse(itemCurrent.Value));
            }

            _save(PrimaryKeyId, lst);
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearchSource.Text.Trim()) && SourceTable != null)
            {
                ClearItems(lstSource);
                ClearItems(lstTarget);

                var dv = SourceTable.DefaultView;
                var rowFilter = string.Empty;
                rowFilter = "[" + SourceTextColumn + "] like '%" + txtSearchSource.Text.Trim() + "%'";
                dv.RowFilter = rowFilter;

                lstSource.DataSource = dv.ToTable();
                lstSource.DataTextField = SourceTextColumn;
                lstSource.DataValueField = SourceTable.Columns.Count > 0 ? SourceTable.Columns[0].ColumnName : "Id";
                lstSource.DataBind();

                LoadCurrentTarget();
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtSearchSource.Text = string.Empty;
            ClearItems(lstSource);
            ClearItems(lstTarget);

            LoadSource();
            LoadCurrentTarget();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearItems(lstSource);
            ClearItems(lstTarget);

            LoadSource();
            LoadCurrentTarget();
            //ConfigureBucket(EntityName, PrimaryKeyId, _getList, _getData, _save);
        }

        #endregion

    }
}