using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Controls
{
	public class ImageTemplate : ITemplate
	{
		ListItemType _templateType;

		string _columnName;

		public ImageTemplate(ListItemType type, string colname)
		{
			//Stores the template type.
			_templateType = type;

			//Stores the column name.
			_columnName = colname;
		}

		void ITemplate.InstantiateIn(Control container)
		{
			switch (_templateType)
			{
				case ListItemType.Header:
					//Creates a new label control and add it to the container.
					var lbl = new Label();
                    lbl.ID = "lbl" + _columnName;
                    //if (_columnName.Contains("Id"))
                    //    _columnName = _columnName.Replace("Id", " Id");
                    //if (_columnName.Contains("Order"))
                    //    _columnName = _columnName.Replace("Order", " Order");
						lbl.Text = _columnName;
					container.Controls.Add(lbl);
					break;

				case ListItemType.Item:
                    //Creates a new text box control and add it to the container.
                    var btnImage = new ImageButton();
					btnImage.ImageUrl = "~/Images/expand_blue.jpg";
					btnImage.CommandName = "~/Shared/QualityAssurance/FunctionalityImage/ShowImage.aspx?imageid=Eval(\"FunctionalityImageId\")";
					btnImage.Click += Image1_Click;
                    container.Controls.Add(btnImage); 
					  
					break;
			}
		}

		protected void Image1_Click(object sender, ImageClickEventArgs e)
		{
			HttpContext.Current.Response.Redirect(((ImageButton)sender).CommandName);
		}

	}
}