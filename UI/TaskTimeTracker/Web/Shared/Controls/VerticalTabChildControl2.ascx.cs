using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shared.UI.Web.Controls
{

	public partial class VerticalTabChildControl2 : UserControl
	{

		#region Properties

		public string Title
		{
			get
			{
				return TabHeader.InnerText;
			}
		}

		public Control ChildGenericControl
		{
			get
			{
				if (TabContent.Controls.Count > 1)
				{
					return TabContent.Controls[1];
				}
				else
				{
					return TabContent.Controls[0];
				}
			}
		}

		#endregion

		#region Methods

		public void Setup(string id, Control control, string title, bool isCollapsed = true, string backColor = "")
		{
			//hdnId.Value = id;
			TabHeader.InnerText = title;

			if (control != null)
			{
				TabContent.Controls.Add(control);
			}

			if (!string.IsNullOrEmpty(backColor))
			{
				try
				{
					TabHeader.Style.Add("background-color", "#" + Color.FromName(backColor).GetHashCode());
				}
				catch
				{
					TabHeader.Style.Add("background-color", "#" + Color.Gray.GetHashCode());
				}
			}
			else
			{
				TabHeader.Style.Add("background-color", "#" + Color.Gray.GetHashCode());
			}

			//if (txtIsCollapsed.Text == "false")
			//{
			//	cpExtender.Collapsed = false;
			//}
			//else
			//{
			//	cpExtender.Collapsed = true;
			//}
		}

		#endregion

		#region Events

		protected void Page_Load(object sender, EventArgs e)
		{

		}

		#endregion

	}

}