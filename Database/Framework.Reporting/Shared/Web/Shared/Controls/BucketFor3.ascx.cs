using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Web.UI.HtmlControls;

namespace Shared.UI.Web.Controls
{
    public partial class BucketFor3Control : BaseControl
    {

        #region Properties

        public delegate DataTable GetListDelegate();
        public delegate DataTable GetDataDelegate(int setId);
        public delegate void GetSaveDelegate(int setId, int entityId1, int entityId2);
        public delegate void GetRemoveDelegate(int setId);

        private GetListDelegate _getListEntity1;
        private GetListDelegate _getListEntity2;
        private GetDataDelegate _getData;
        private GetSaveDelegate _save;
        private GetRemoveDelegate _remove;

        private int SetId
        {
            get
            {
                return Convert.ToInt32(ViewState["SetId"]);
            }
            set
            {
                ViewState["SetId"] = value;
            }
        }

        private string EntityName1
        {
            get
            {
                return Convert.ToString(ViewState["EntityName1"]);
            }
            set
            {
                ViewState["EntityName1"] = value;
            }
        }

        private string EntityName2
        {
            get
            {
                return Convert.ToString(ViewState["EntityName2"]);
            }
            set
            {
                ViewState["EntityName2"] = value;
            }
        }

        private string Entity1TextField
        {
            get
            {
                return Convert.ToString(ViewState["Entity1TextField"]);
            }
            set
            {
                ViewState["Entity1TextField"] = value;
            }
        }

        private string Entity2TextField
        {
            get
            {
                return Convert.ToString(ViewState["Entity2TextField"]);
            }
            set
            {
                ViewState["Entity2TextField"] = value;
            }
        }

        #endregion

        #region Methods

        private void LoadSource()
        {
            var dt = _getListEntity1();
            lstSource.DataSource = dt;
            lstSource.DataTextField = Entity1TextField;
            lstSource.DataValueField = dt.Columns.Count > 0 ? dt.Columns[0].ColumnName : "Id";
            lstSource.DataBind();

        }

        private void LoadTarget()
        {
            try
            {
                var dtRecords = _getData(SetId);
                var dt = _getListEntity2();
                var iCnt = 0;

                if (dynTarget.Controls.Count > 0)
                {
                    dynTarget.Controls.Clear();
                }
                foreach (DataRow dr in dt.Rows)
                {
                    var entityId = Convert.ToString(dr[EntityName2 + "Id"]);
                    if (Convert.ToInt32(entityId) < 0)
                    {
                        entityId = entityId.Replace("-", "m");
                    }
                    var plHolder = new PlaceHolder();
                    plHolder.ID = "dynTarget" + entityId;

                    var table = new HtmlTable();
                    var row = new HtmlTableRow();

                    var cell = new HtmlTableCell();

                    var btnLeft = new Button();
                    btnLeft.ID = "btnLeft" + entityId;
                    btnLeft.Text = "<--";
                    btnLeft.Click += btnLeft_Click;
                    btnLeft.CommandName = entityId;
                    btnLeft.CommandArgument = entityId;

                    var btnRight = new Button();
                    btnRight.ID = "btnRight" + entityId;
                    btnRight.Text = "-->";
                    btnRight.Click += btnRight_Click;
                    btnRight.CommandName = entityId;
                    btnRight.CommandArgument = entityId;

                    cell.Controls.Add(btnLeft);
                    cell.Controls.Add(btnRight);

                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    var divLabel = new HtmlGenericControl("div");
                    divLabel.InnerText = Convert.ToString(dr[Entity2TextField]);
                    cell.Controls.Add(divLabel);

                    var lst = new ListBox();
                    lst.ID = "lstTarget" + entityId;
                    lst.SelectionMode = ListSelectionMode.Multiple;
                    lst.Width = 250;
                    lst.Height = 100;
                    lst.Text = entityId;

                    var drs = dtRecords.Select(EntityName2 + "Id = " + dr[EntityName2 + "Id"]);
                    if (drs.Length > 0)
                    {
                        foreach (var drRecord in drs)
                        {
                            lst.Items.Add(new ListItem(Convert.ToString(drRecord[EntityName1]), Convert.ToString(drRecord[EntityName1 + "Id"])));
                        }
                    }

                    cell.Controls.Add(lst);
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();

                    if (iCnt != 0)
                    {
                        var btnUp = new Button();
                        btnUp.ID = "btnUp" + entityId;
                        btnUp.Text = "Move Up";
						btnUp.Width = 100;
                        btnUp.Click += btnMoveUp_Click;
                        btnUp.CommandName = entityId;
                        btnUp.CommandArgument = entityId;
                        cell.Controls.Add(btnUp);
                    }

					if (iCnt != dt.Rows.Count - 1)
					{
						var btnDown = new Button();
						btnDown.ID = "btnDown" + entityId;
						btnDown.Text = "Move Down";
						btnDown.Width = 100;
						btnDown.Click += btnMoveDown_Click;
						btnDown.CommandName = entityId;
						btnDown.CommandArgument = entityId;
						cell.Controls.Add(btnDown);
					}

                    row.Cells.Add(cell);

                    table.Rows.Add(row);

                    plHolder.Controls.Add(table);

                    dynTarget.Controls.Add(plHolder);
                    iCnt++;
                }
                ManageSourceCleanUp(dtRecords);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ManageSourceCleanUp(DataTable dtRecords)
        {
            var totalCont = dynTarget.Controls.Count;
            var indexes = new List<int>();
            for (var iCount = 0; iCount < lstSource.Items.Count; iCount++)
            {
                var drs = dtRecords.Select(EntityName1 + "Id = " + lstSource.Items[iCount].Value);
                if (drs.Length == totalCont)
                {
                    indexes.Add(iCount);
                }
            }
            indexes.Reverse();
            foreach (var index in indexes)
                lstSource.Items.RemoveAt(index);
        }

        public void ConfigureBucket(string strEntity1, string strEntity2, string strEntity1TextField, string strEntity2TextField, int setId,
            GetListDelegate getListDelegateEntity1, GetListDelegate getListDelegateEntity2, GetDataDelegate getDataDelegate,
            GetSaveDelegate saveDelegate, GetRemoveDelegate removeDelegate)
        {
            EntityName1 = strEntity1;
            EntityName2 = strEntity2;

            Entity1TextField = strEntity1TextField;
            Entity2TextField = strEntity2TextField;

            lblEntity1.Text = EntityName1 + "(s) :";
            lblEntity2.Text = EntityName2 + "(s) :";
            SetId = setId;
            _getListEntity1 = getListDelegateEntity1;
            _getListEntity2 = getListDelegateEntity2;

            _getData = getDataDelegate;
            _save = saveDelegate;
            _remove = removeDelegate;

            LoadSource();
            LoadTarget();
            //LoadCurrentTarget();
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            _remove(SetId);

            var listBoxes = dynTarget.FindControlsOfType<ListBox>().ToList();
            foreach (var lst in listBoxes)
            {
                var entity2Id = lst.ID.Replace("lstTarget", "");
                if (entity2Id.StartsWith("m"))
                {
                    entity2Id = "-" + entity2Id.Remove(0, 1);
                }

                for (var iCount = 0; iCount < lst.Items.Count; iCount++)
                {
                    var id1 = Convert.ToInt32(lst.Items[iCount].Value);
                    var id2 = Convert.ToInt32(entity2Id);
                    _save(SetId, id1, id2);
                }
            }
        }

        protected void btnLeft_Click(object sender, EventArgs e)
        {
            var entityId = ((Button)sender).CommandArgument;
            var lstControl = dynTarget.FindControl("lstTarget" + entityId);
            if (lstControl != null)
            {
                var lst = (ListBox)lstControl;
                var targetIds = lst.GetSelectedIndices();
                if (targetIds.Length > 0)
                {
                    foreach (var index in targetIds)
                    {
                        if (lstSource.Items.FindByValue(lst.Items[index].Value) == null)
                        {
                            lstSource.Items.Add(new ListItem(lst.Items[index].Text, lst.Items[index].Value));
                        }
                    }
                    targetIds = targetIds.Reverse().ToArray();
                    foreach (var index in targetIds)
                    {
                        lst.Items.RemoveAt(index);
                    }
                }
            }
        }

        protected void btnRight_Click(object sender, EventArgs e)
        {
            var entityId = ((Button)sender).CommandArgument;
            var sourceIds = lstSource.GetSelectedIndices();
            var lstControl = dynTarget.FindControl("lstTarget" + entityId);
            if (sourceIds.Length > 0 && lstControl != null)
            {
                var lst = (ListBox)lstControl;
                foreach (var index in sourceIds)
                {
                    if (lst.Items.FindByValue(lstSource.Items[index].Value) == null)
                    {
                        lst.Items.Add(new ListItem(lstSource.Items[index].Text, lstSource.Items[index].Value));
                    }
                }
                var removeIndexs = new List<int>();
                var listBoxes = dynTarget.FindControlsOfType<ListBox>().ToList();
                if (listBoxes != null && listBoxes.Count > 0)
                {
                    foreach (var index in sourceIds)
                    {
                        var exists = true;
                        foreach (var lstBox in listBoxes)
                        {
                            if (lstBox.Items.FindByValue(lstSource.Items[index].Value) == null)
                            {
                                exists = false;
                                break;
                            }
                        }

                        if (exists)
                        {
                            removeIndexs.Add(index);
                        }
                    }
                    if (removeIndexs.Count > 0)
                    {
                        removeIndexs.Reverse();
                        foreach (var index in removeIndexs)
                        {
                            lstSource.Items.RemoveAt(index);
                        }
                    }
                }
            }
        }

        protected void btnMoveUp_Click(object sender, EventArgs e)
        {
            var entityId = ((Button)sender).CommandArgument;
            var lstControl = dynTarget.FindControl("lstTarget" + entityId);
            if (lstControl != null)
            {
                var lst = (ListBox)lstControl;
                var moveIds = lst.GetSelectedIndices();
                if (moveIds.Length > 0)
                {
                    var listBoxes = dynTarget.FindControlsOfType<ListBox>().ToList();
                    for (var iCount = 0; iCount < listBoxes.Count; iCount++)
                    {
                        if (listBoxes[iCount].ID == "lstTarget" + entityId)
                        {
                            if (iCount != 0)
                            {
                                foreach (var index in moveIds)
                                {
                                    if (listBoxes[iCount - 1].Items.FindByValue(lst.Items[index].Value) == null)
                                    {
                                        listBoxes[iCount - 1].Items.Add(new ListItem(lst.Items[index].Text, lst.Items[index].Value));
                                    }
                                    if (lstSource.Items.FindByValue(lst.Items[index].Value) == null)
                                    {
                                        lstSource.Items.Add(new ListItem(lst.Items[index].Text, lst.Items[index].Value));
                                    }
                                }
                            }
                            break;
                        }
                    }
                    moveIds = moveIds.Reverse().ToArray();
                    foreach (var index in moveIds)
                    {
                        lst.Items.RemoveAt(index);
                    }
                }
            }
        }

        protected void btnMoveDown_Click(object sender, EventArgs e)
        {
            var entityId = ((Button)sender).CommandArgument;
            var lstControl = dynTarget.FindControl("lstTarget" + entityId);
            if (lstControl != null)
            {
                var lst = (ListBox)lstControl;
                var moveIds = lst.GetSelectedIndices();
                if (moveIds.Length > 0)
                {
                    var listBoxes = dynTarget.FindControlsOfType<ListBox>().ToList();
                    for (var iCount = 0; iCount < listBoxes.Count; iCount++)
                    {
                        if (listBoxes[iCount].ID == "lstTarget" + entityId)
                        {
                            if (iCount != (listBoxes.Count - 1))
                            {
                                foreach (var index in moveIds)
                                {
                                    if (listBoxes[iCount + 1].Items.FindByValue(lst.Items[index].Value) == null)
                                    {
                                        listBoxes[iCount + 1].Items.Add(new ListItem(lst.Items[index].Text, lst.Items[index].Value));
                                    }
                                    if (lstSource.Items.FindByValue(lst.Items[index].Value) == null)
                                    {
                                        lstSource.Items.Add(new ListItem(lst.Items[index].Text, lst.Items[index].Value));
                                    }
                                }
                            }
                            break;
                        }
                    }
                    moveIds = moveIds.Reverse().ToArray();
                    foreach (var index in moveIds)
                    {
                        lst.Items.RemoveAt(index);
                    }
                }
            }
        }

        #endregion

    }
}