using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Controls
{
    public partial class NumberedPager : UserControl
    {

        #region variables

        private GridView MainGridView;

        public int CurrentIndex
        {
            get
            {
                if (ViewState["CurrentIndex"] == null)
                {
                    ViewState["CurrentIndex"] = 0;
                }

                return Convert.ToInt32(ViewState["CurrentIndex"]);
            }
            set
            {
                ViewState["CurrentIndex"] = value;
            }
        }

        public int TotalPages
        {
            get
            {
                if (ViewState["TotalPages"] == null)
                {
                    ViewState["TotalPages"] = 0;
                }

                return int.Parse(ViewState["TotalPages"].ToString());
            }
            set
            {
                ViewState["TotalPages"] = value;
            }
        }

        public event EventHandler Changed;

        #endregion

        #region Methods

        public void CreateNumberedPagingControl()
        {
            
            plcPaging.Controls.Clear();

            var spacer = new Label();
            spacer.Text = "&nbsp;";
            plcPaging.Controls.Add(spacer);

            var currentModular = CurrentIndex / 10;
            if (currentModular < 0)
            {
                currentModular = 0;
            }
            var modFloor = currentModular * 10;
            var modCeil = modFloor + 10;

            if(modCeil > TotalPages)
            {
                modCeil = TotalPages;
            }

            if (modFloor > 1)
            {
                var lnk = new LinkButton();
                lnk.ID = "lnkPagePrev" + (modFloor);
                lnk.Text = "...";
                lnk.CommandArgument = (modFloor - 1).ToString();
                lnk.CommandName = (modFloor - 1).ToString();
                lnk.Click += new EventHandler(lnk_Click);
                lnk.Font.Underline = false;
                plcPaging.Controls.Add(lnk);

                spacer = new Label();
                spacer.Text = "&nbsp;";
                plcPaging.Controls.Add(spacer);
            }

            for (var i = modFloor; i < modCeil; i++)
            {
                var lnk = new LinkButton();
                lnk.ID = "lnkPage" + (i + 1);
                lnk.Text = (i + 1).ToString();
                lnk.CommandArgument = i.ToString();
                lnk.CommandName = i.ToString();
                lnk.Click += new EventHandler(lnk_Click);
                lnk.Font.Underline = false;
                plcPaging.Controls.Add(lnk);

                spacer = new Label();
                spacer.Text = "&nbsp;";
                plcPaging.Controls.Add(spacer);

                if (i == CurrentIndex)
                {
                    lnk.Enabled = false;
                    lnk.CssClass = "activePage";
                }                
            }

            if (TotalPages > modCeil)
            {
                var lnk = new LinkButton();
                lnk.ID = "lnkPageNext" + (modCeil + 1);
                lnk.Text = "...";
                lnk.CommandArgument = modCeil.ToString();
                lnk.CommandName = modCeil.ToString();
                lnk.Click += new EventHandler(lnk_Click);
                lnk.Font.Underline = false;
                plcPaging.Controls.Add(lnk);

                spacer = new Label();
                spacer.Text = "&nbsp;";
                plcPaging.Controls.Add(spacer);
            }

            for (var iCount = 0; iCount < TotalPages; iCount++)
            {
                if (iCount < modFloor || iCount > modCeil)
                {
                    var lnk = new LinkButton();
                    lnk.ID = "lnkPage" + (iCount + 1);
                    lnk.Text = (iCount + 1).ToString();
                    lnk.CommandArgument = iCount.ToString();
                    lnk.CommandName = iCount.ToString();
                    lnk.Click += new EventHandler(lnk_Click);
                    lnk.Visible = false;
                    plcPaging.Controls.Add(lnk);
                }
            }
        }

		public void Setup(int rowCount, GridView mainGridView, int defaultRowCount)
        {
            TotalPages = int.Parse((rowCount / defaultRowCount).ToString());
            if (rowCount % defaultRowCount != 0)
            {
                TotalPages += 1;
            } 
            MainGridView = mainGridView; 
        }

        #endregion

        #region Events

        void lnk_Click(object sender, EventArgs e)
        {
            var lnk = (LinkButton)sender;
            CurrentIndex = Convert.ToInt32(lnk.CommandArgument);

            MainGridView.PageIndex = CurrentIndex;

            // To Bind Data Grid Again
            if (Changed != null)
            {
                Changed(this, e);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CreateNumberedPagingControl();
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            //Response.Write(CurrentIndex);
            // your code
        }

        #endregion

    }
}