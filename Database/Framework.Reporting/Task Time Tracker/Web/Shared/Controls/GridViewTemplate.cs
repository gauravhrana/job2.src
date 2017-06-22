using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Controls
{
	public class GridViewTemplate : ITemplate
	{
		ListItemType _templateType;

		string _columnName;

		public GridViewTemplate(ListItemType type, string colname)
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
					lbl.DataBinding += new EventHandler(lbl1_DataBinding); 
					container.Controls.Add(lbl);
					break;

				case ListItemType.Item:
                    //Creates a new text box control and add it to the container.
                    var lbl1 = new Label();
                    lbl1.DataBinding += new EventHandler(lbl2_DataBinding);
                    lbl1.ID = _columnName;
                    container.Controls.Add(lbl1); 
					  
					break;

                case ListItemType.EditItem:
                    //Creates a new text box control and add it to the container.
                    var tb1 = new TextBox();
                    tb1.DataBinding += new EventHandler(tb1_DataBinding);
                    tb1.Columns = 15;
                    tb1.ID = _columnName;
                    container.Controls.Add(tb1);
                    break;

				case ListItemType.Footer:
					var chkColumn = new CheckBox();
					chkColumn.ID = "Chk" + _columnName;
					container.Controls.Add(chkColumn);
					break;
			}
		}

		private void tb1_DataBinding(object sender, EventArgs e)
		{
			var txtdata = (TextBox)sender;
			var container = (GridViewRow)txtdata.NamingContainer;
			var dataValue = DataBinder.Eval(container.DataItem, _columnName);
			if (dataValue != DBNull.Value)
			{
				txtdata.Text = dataValue.ToString();
			}
		}

        private void lbl2_DataBinding(object sender, EventArgs e)
        {
            var lbldata = (Label)sender;
            var container = (GridViewRow)lbldata.NamingContainer;
            var dataValue = DataBinder.Eval(container.DataItem, _columnName);
            if (dataValue != DBNull.Value)
            {
                lbldata.Text = dataValue.ToString();
            }
        }

		private void lbl1_DataBinding(object sender, EventArgs e)
		{
			var lbldata = (Label)sender;
			var container = (GridViewRow)lbldata.NamingContainer;
			var dataValue = DataBinder.Eval(container.DataItem, _columnName);
			if (dataValue != DBNull.Value && dataValue != null)
			{
				lbldata.Text = dataValue.ToString();
				lbldata.Font.Bold = true;
				lbldata.ForeColor = Color.Black; 
			}
		}

		private void lbl_DataBinding(object sender, EventArgs e)
		{
			var lbl = (Label)sender;
			lbl.Font.Bold = true;
			lbl.ForeColor = Color.White;
			var container = (GridViewRow)lbl.NamingContainer;
		}
	}
}