using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Linq.Dynamic;

namespace Shared.UI.Web.Controls
{
    public partial class BucketControl : BaseControl
    {

        #region properties

        public delegate IEnumerable GetListDelegate();
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

        private IEnumerable SourceTable
        {
            get
            {
                return (IEnumerable)ViewState["SourceTable"];
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

        private string SourceValueColumn
        {
            get
            {
                return Convert.ToString(ViewState["SourceValueColumn"]);
            }
            set
            {
                ViewState["SourceValueColumn"] = value;
            }
        }

        #endregion

        #region methods

        private void SwitchValues(ListBox source, ListBox target)
        {
            var listRemoval = new ArrayList();

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

        private void LoadSource(bool isReloadSource = false)
        {
            if (SourceTable == null || isReloadSource)
            {
                SourceTable = _getList();
            }

            var listSourceItems = SourceTable;

            if (listSourceItems.AsQueryable().Any())
            {
                var properties = listSourceItems.Cast<object>().ToList()[0].GetType().GetProperties();

                if (string.IsNullOrEmpty(SourceTextColumn))
                {
                    SourceTextColumn = "Name";
                    if (!properties.Select(x => x.Name).Contains("Name"))
                    {
                        for (var iCount = 1; iCount < properties.Length; iCount++)
                        {
                            if (properties[iCount].Name != "ApplicationId")
                            {
                                SourceTextColumn = properties[iCount].Name;
                                break;
                            }
                        }
                    }
                }

                if (string.IsNullOrEmpty(SourceValueColumn) || !properties.Select(x => x.Name).Contains(SourceValueColumn))
                {
                    SourceValueColumn = properties.Any() ? properties[0].Name : "Id";
                }
            }

            lstSource.DataSource = listSourceItems;
            lstSource.DataTextField = SourceTextColumn;
            lstSource.DataValueField = SourceValueColumn;
            lstSource.DataBind();
        }

        private void LoadCurrentTarget()
        {
            var dt = _getData(PrimaryKeyId);
            foreach (DataRow dr in dt.Rows)
            {
                var lstItem = new ListItem(Convert.ToString(dr[EntityName]), Convert.ToString(dr[SourceValueColumn]));
                if (lstSource.Items.Contains(lstItem))
                {
                    lstSource.Items.Remove(lstItem);
                }
                lstTarget.Items.Add(lstItem);
            }
        }

        public void ConfigureBucket(string strEntity, int setId,
            GetListDelegate getListDelegate, GetDataDelegate getDataDelegate, GetSaveDelegate saveDelegate,
            string sourceTextColumn = "", string sourceValueColumn = "")
        {
            EntityName = strEntity;
            lblCurrentTitle.Text = "Currently Associated " + EntityName + "(s) :";
            lblPossibleTitle.Text = "Possible " + EntityName + "(s) :";
            PrimaryKeyId = setId;
            _getList = getListDelegate;
            _getData = getDataDelegate;
            _save = saveDelegate;

            SourceTextColumn = sourceTextColumn;
            SourceValueColumn = sourceValueColumn;

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

        public void ReloadBucketList()
        {
            ClearItems(lstSource);
            ClearItems(lstTarget);

            LoadSource();
            LoadCurrentTarget();
        }

        public void ReloadBucketList(bool isRelaodSource)
        {
            ClearItems(lstSource);
            ClearItems(lstTarget);

            LoadSource(isRelaodSource);
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
                //ClearItems(lstTarget);  

                //dynamic (string based search) search criteria
                var searchQuery = SourceTextColumn + ".Contains(@0)";

                if (SourceTable.AsQueryable().Where(searchQuery, txtSearchSource.Text.Trim()).Any())
                {
                    //dynamic (string based search) search applied
                    SourceTable = SourceTable.AsQueryable().Where(searchQuery, txtSearchSource.Text.Trim()).Cast<object>().ToList();

                    lstSource.DataSource = SourceTable;
                    lstSource.DataTextField = SourceTextColumn;
                    lstSource.DataValueField = SourceValueColumn;
                    lstSource.DataBind();
                }

                foreach (ListItem item in lstTarget.Items)
                {
                    var lstItem = new ListItem(item.Text, item.Value);
                    if (lstSource.Items.Contains(lstItem))
                    {
                        lstSource.Items.Remove(lstItem);
                    }
                }
            }
            else
            {
                LoadSource();
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtSearchSource.Text = String.Empty;
            ClearItems(lstSource);
            ClearItems(lstTarget);

            LoadSource();
            LoadCurrentTarget();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtSearchSource.Text = String.Empty;
            ClearItems(lstSource);
            ClearItems(lstTarget);

            LoadSource();
            LoadCurrentTarget();
            //ConfigureBucket(EntityName, PrimaryKeyId, _getList, _getData, _save);
        }

        #endregion

    }
}