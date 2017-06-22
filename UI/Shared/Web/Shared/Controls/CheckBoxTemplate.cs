using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shared.UI.Web.Controls
{
	
	public class CheckBoxTemplate : ITemplate
	{

			//Field to store the ListItemType value
			private ListItemType myListItemType;

			public CheckBoxTemplate()
			{
				//
				// TODO: Add default constructor logic here
				//
			}

			//Parameterrised constructor
			public CheckBoxTemplate(ListItemType Item)
			{
				myListItemType = Item;
			}

			//Overwrite the InstantiateIn() function of the ITemplate interface.
		    void ITemplate.InstantiateIn(Control container)
			{
				//Code to create the ItemTemplate and its field.
				if (myListItemType == ListItemType.Item)
				{
					var rowchk = new CheckBox();
					rowchk.ID = "CheckBox1";
					container.Controls.Add(rowchk);
				}
				else if (myListItemType == ListItemType.Header)
				{
					var chk = new CheckBox();
					chk.ID = "chkSelectAll";
					chk.Attributes.Add("OnCheckedChanged", "chkSelectAll_CheckedChanged");
					chk.AutoPostBack = true;
					container.Controls.Add(chk);

				}
			}

	}
	
}