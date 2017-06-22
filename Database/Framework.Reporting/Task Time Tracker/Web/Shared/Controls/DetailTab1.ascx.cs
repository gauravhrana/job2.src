using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;
using System.Web.UI.HtmlControls;

namespace Shared.UI.Web.Controls
{
    public partial class DetailTab1Control : BaseControl
    {

        private TabOrientation TabOrientation
        {
            get
            {
                if (ViewState["TabOrientation"] != null)
                {
                    return (TabOrientation)ViewState["TabOrientation"];
                }
                return TabOrientation.Horizontal;
            }
            set
            {
                ViewState["TabOrientation"] = value;
            }
        }


        #region Methods

        private HtmlGenericControl GetBlankDiv()
        {
            var xDiv = new HtmlGenericControl("div");

			// TODO: Style should be outside layout structure
            xDiv.Attributes.CssStyle.Add("height", "10px");

            return xDiv;
        }

		public void AddTab(string tabHeaderId, Control detailControl, string tabHeaderValue, bool isCollapsed = true)
        {
			if (string.IsNullOrEmpty(tabHeaderValue))
			{
				tabHeaderValue = tabHeaderId;
			}

			// * * * * * * 
			// accordion header
			// * * * * * * 

			var accordinHeader = new HtmlGenericControl("h3");
			accordinHeader.InnerText = tabHeaderValue;
			accordinHeader.ID = "h3" + tabHeaderId;
			accordinHeader.Attributes["myTabIndex"] = Convert.ToString(divContainer.Controls.Count / 2);

			divContainer.Controls.Add(accordinHeader);

			// * * * * * * 
			// accordion child control
			// * * * * * * 

			var divAccordion = new HtmlGenericControl("div");
			divAccordion.Attributes.Add("class", ApplicationCommon.DetailsBorderClassName);


			//"#TabContent-" + tabId.Replace(" ", string.Empty);			
			if (detailControl != null)
			{
				divAccordion.Controls.Add(detailControl);
			}

			divContainer.Controls.Add(divAccordion);      
        }

        public void Reload()
        {
            //BeginLogMethod(logger);

            if (!SessionVariables.IsTesting || TabOrientation == TabOrientation.Vertical)
            {
                for (var i = 0; i < divContainer.Controls.Count; i++)
                {
                    try
                    {
	                    var ctrl = divContainer.Controls[i].Controls[1].Controls[0];

						// check the type of control it is
						if (ctrl is ListControl)
                        {
							((ListControl)ctrl).ShowData(false, true);
                        }
						else if (ctrl is DetailsWithChildrenControl)
                        {
                            if (!string.IsNullOrEmpty(SettingCategory))
                            {
								((DetailsWithChildrenControl)ctrl).ShowData(false, true, false, String.Empty);
                            }
                            else
                            {
								((DetailsWithChildrenControl)ctrl).ShowData(false, true);
                            }
                        }
                        else if ((divContainer.Controls[i].Controls[1].Controls[0] is DetailTabControl))
                        {
							((DetailTabControl)ctrl).Reload();
                        }
                    }
                    catch { }
                }
            }
            else
            {
                for (var i = 0; i < divContainer.Controls.Count; i++)
                {
                    try
                    {
                        if (divContainer.Controls[i].Controls.Count > 0 && divContainer.Controls[i].Controls[0] is VerticalTabChildControl)
                        {
                            var cntrl = (VerticalTabChildControl)divContainer.Controls[i].Controls[0];

                            if (cntrl.ChildGenericControl is DetailsWithChildrenControl)
                            {
                                if (!string.IsNullOrEmpty(SettingCategory))
                                {
                                    ((DetailsWithChildrenControl)divContainer.Controls[i].Controls[1].Controls[0]).ShowData(false, true, false, String.Empty);
                                }
                                else
                                {
                                    ((DetailsWithChildrenControl)divContainer.Controls[i].Controls[1].Controls[0]).ShowData(false, true);
                                }
                            }
							else if (cntrl.ChildGenericControl is ListControl)
                            {
								((ListControl)cntrl.ChildGenericControl).ShowData(false, true);
                            }
                            else if (cntrl.ChildGenericControl is DetailTabControl)
                            {
                                ((DetailTabControl)cntrl.ChildGenericControl).Reload();
                            }
                        }
                    }
                    catch { }
                }
            }
        }
        #endregion

    }
}