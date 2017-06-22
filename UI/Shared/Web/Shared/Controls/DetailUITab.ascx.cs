using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Web.UI.HtmlControls;
using System.Data;
using System.IO;
using Shared.UI.Web.Controls;

namespace Shared.UI.Web.Controls
{
    public partial class DetailUITab : Shared.UI.WebFramework.BaseControl
    {

        #region Variables

        private int _setId;

        public int SetId
        {
            set
            {
                _setId = value;
            }
            get
            {
                return _setId;
            }
        }

        public Control GenericControl;

        #endregion

        #region Methods

        public void AddTab(string strEntity, bool selected, Control detailControl = null)
        {
            HtmlGenericControl xDiv = new HtmlGenericControl("div");
            xDiv.ID = "innerDiv" + strEntity;

            HtmlGenericControl xInnerDiv = new HtmlGenericControl("div");
            xInnerDiv.InnerHtml = "<table width='100%' cellpadding='0' cellspacing='0' style='padding:3px; background-color: #F5F5F5; margin-top:5px; margin-bottom: 5px; '><tr><td align='left'>" + strEntity + "</td></tr></table>";
            xInnerDiv.Visible = false;
            if (!selected)
            {
                xDiv.Visible = false;
            }
            xDiv.Controls.Add(xInnerDiv);

            HtmlGenericControl xInnerDiv1 = new HtmlGenericControl("div");

            if (detailControl != null)
            {
                if (GenericControl == null)
                {
                    GenericControl = detailControl;
                }
                xInnerDiv1.Controls.Add(detailControl);
            }
            xDiv.Attributes.CssStyle.Add("margin-top", "10px");
            xDiv.Attributes.CssStyle.Add("margin-bottom", "10px");
            xInnerDiv1.Attributes.Add("class", ApplicationCommon.DetailsBorderClassName);
            xDiv.Controls.Add(xInnerDiv1);
            divInnerContent.Controls.Add(xDiv);

            HtmlGenericControl xList = new HtmlGenericControl("li");
            xList.ID = "li" + strEntity;
            if (selected)
            {
                xList.Attributes.CssStyle.Add("background-color", "Yellow");
            }
            //xList.Attributes.Add("class", "selected");

            LinkButton lButton = new LinkButton();
            lButton.Text = strEntity;
            lButton.Click += new EventHandler(lButton_Click);
            xList.Controls.Add(lButton);
            toc.Controls.Add(xList);
        }

		public void AddLastTab()
		{
			HtmlGenericControl xList = new HtmlGenericControl("li");
			xList.ID = "liAll";

			LinkButton lButton = new LinkButton();
			lButton.Text = "All";
			lButton.Click += new EventHandler(lButton_Click);
			xList.Controls.Add(lButton);
			toc.Controls.Add(xList);

			
		}

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
        	var selectalltab = false;
			if(divInnerContent.Controls.Count > 2)
			{
				for(int i = 0; i < divInnerContent.Controls.Count; i++)
				{
					if (divInnerContent.Controls[i].Visible)
						selectalltab = true;
					else
						selectalltab = false;
				}
				if(selectalltab)
				{
					if (toc.Controls[0].ID == null)
					{
						toc.Controls.RemoveAt(0);
					}

					for (var iCount = 0; iCount < toc.Controls.Count; iCount++)
					{
						if (toc.Controls[iCount].ID == "liAll" )
						{
							((HtmlGenericControl)toc.Controls[iCount]).Attributes["style"] = "selected";
							((HtmlGenericControl)toc.Controls[iCount]).Attributes.CssStyle.Add("background-color", "Yellow");
						}
						else
						{
							//if (toc.Controls[iCount].GetType() == typeof(HtmlGenericControl))
							//{
							try
							{
								if (((HtmlGenericControl)toc.Controls[iCount]).Attributes["style"] == "selected" || ((HtmlGenericControl)toc.Controls[iCount]).Attributes.CssStyle["background-color"] == "Yellow")
								{
									((HtmlGenericControl)toc.Controls[iCount]).Attributes.Clear();
									((HtmlGenericControl)toc.Controls[iCount]).Attributes.CssStyle.Remove("background-color");
								}
							}
							catch { }
							//}
						}
					}
				}
			}
        }

        void lButton_Click(object sender, EventArgs e)
        {
            var strEntity = ((LinkButton)sender).Text;

            if (toc.Controls[0].ID == null)
            {
                toc.Controls.RemoveAt(0);
            }

            for (var iCount = 0; iCount < toc.Controls.Count; iCount++)
            {
                if (toc.Controls[iCount].ID == "li" + strEntity)
                {
                    ((HtmlGenericControl)toc.Controls[iCount]).Attributes["style"] = "selected";
                    ((HtmlGenericControl)toc.Controls[iCount]).Attributes.CssStyle.Add("background-color", "Yellow");
                }
                else
                {
                    //if (toc.Controls[iCount].GetType() == typeof(HtmlGenericControl))
                    //{
                    try
                    {
                        if (((HtmlGenericControl)toc.Controls[iCount]).Attributes["style"] == "selected" || ((HtmlGenericControl)toc.Controls[iCount]).Attributes.CssStyle["background-color"] == "Yellow")
                        {
                            ((HtmlGenericControl)toc.Controls[iCount]).Attributes.Clear();
                            ((HtmlGenericControl)toc.Controls[iCount]).Attributes.CssStyle.Remove("background-color");
                        }
                    }
                    catch { }
                    //}
                }
            }
            //toc.ApplyStyleSheetSkin(this.Page);
            for (var iCount = 0; iCount < divInnerContent.Controls.Count; iCount++)
            {
                if (strEntity != "All")
                {
                    if (divInnerContent.Controls[iCount].ID == "innerDiv" + strEntity)
                    {
                        divInnerContent.Controls[iCount].Visible = true;
                        try
                        {
                            ((Shared.UI.Web.Controls.DetailsWithChildren)divInnerContent.Controls[iCount].Controls[1].Controls[0]).ShowData(false, true);
                        }
                        catch { }
                    }
                    else
                    {
                        divInnerContent.Controls[iCount].Visible = false;
                    }
                    try
                    {
                        divInnerContent.Controls[iCount].Controls[0].Visible = false;
                    }
                    catch { }
                }
                else
                {
					for (var i = 0; i < divInnerContent.Controls.Count; i++)
					{
						divInnerContent.Controls[i].Visible = true;
					}
					divInnerContent.Visible = true;
                }
            }
        }

        #endregion

    }
}