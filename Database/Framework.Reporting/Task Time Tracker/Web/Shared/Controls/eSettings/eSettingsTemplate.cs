using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shared.UI.Web.Controls
{
    public class eSettingsTemplate : ITemplate
    {
        //A variable to hold the type of ListItemType.
        ListItemType _templateType;

        //A variable to hold the column name.
        string _columnName;

        //Constructor where we define the template type and column name.
        public eSettingsTemplate(ListItemType type, string colname)
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

                    var lbl = new Label();
                    lbl.Text = _columnName;
                    container.Controls.Add(lbl);
                    break;

                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    var tb1 = new TextBox();
                    tb1.DataBinding += new EventHandler(tb1_DataBinding);
                    tb1.Columns = 4;
                    container.Controls.Add(tb1);
                    break;

                case ListItemType.EditItem:

                    break;

                case ListItemType.Footer:
                    var chkColumn = new CheckBox();
                    chkColumn.ID = "Chk" + _columnName;
                    container.Controls.Add(chkColumn);
                    break;
            }
        }

        /// <summary>
        /// This is the event, which will be raised when the binding happens.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tb1_DataBinding(object sender, EventArgs e)
        {
            var txtdata = (TextBox)sender;
            var container = (GridViewRow)txtdata.NamingContainer;
            var dataValue = DataBinder.Eval(container.DataItem, _columnName);
            if (dataValue != DBNull.Value)
            {
                txtdata.Text = dataValue.ToString();
            }
        }
    }
}