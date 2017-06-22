using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shared.UI.WebFramework
{
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:WebCustomControl1 runat=server></{0}:WebCustomControl1>")]
	public class WebCustomControl1 : WebControl
	{

		private RequiredFieldValidator req;
		public string InvalidMessage;
		public string ClientScript = "true";

		private TextBox txtControl;
		private Button btnControl;
		private Table tbl; // Master container of button and textbox.



		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue("")]
		[Localizable(true)]
		public string Text
		{
			get
			{
				String s = (String) ViewState["Text"];
				return ((s == null) ? String.Empty : s);
			}

			set { ViewState["Text"] = value; }
		}

		//protected override void RenderContents(HtmlTextWriter output)
		//{
		//    output.Write(Text);
		//}

		//public event for handling click of the button
		public event EventHandler BtnClick; 

		//protected override void OnInit(EventArgs e)
		//{
		//    req = new RequiredFieldValidator();
		//    req.ControlToValidate = ID;
		//    req.ErrorMessage = InvalidMessage;
		//    req.EnableClientScript = (ClientScript.ToLower() != "false");
		//    Controls.Add(req);
		//}

		//protected override void Render(HtmlTextWriter w)
		//{
		//    base.Render(w);
		//    req.RenderControl(w);
		//}

		[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "Execution")]
		protected override void CreateChildControls()
		{

			txtControl = new TextBox();
			txtControl.ID = "txtControl"; 

			btnControl = new Button();
			btnControl.ID = "btnControl";
			btnControl.Text = "Submit";
			btnControl.CausesValidation = false;
			btnControl.Click += new EventHandler(btnControl_Click);

			tbl = new Table();
			tbl.Width = Unit.Percentage(25);
			tbl.ID = "tbl"; 

			TableRow tblrow = new TableRow();
			tblrow.ID = "tblrow";
			tblrow.Width = Unit.Percentage(100);

			TableCell tblCell0 = new TableCell();
			tblCell0.ID = "tblCell0";
			tblCell0.Controls.Add(txtControl);
			tblCell0.HorizontalAlign = HorizontalAlign.Left;

			TableCell tblCell1 = new TableCell();
			tblCell1.ID = "tblCell1";
			tblCell1.HorizontalAlign = HorizontalAlign.Left;
			tblCell1.Controls.Add(btnControl);

			tblrow.Cells.Add(tblCell0);
			tblrow.Cells.Add(tblCell1);
			tbl.Rows.Add(tblrow);
			Controls.Add(tbl);

			base.CreateChildControls();
		}

		//Button Click event to call public event.
		private void btnControl_Click(object sender, EventArgs e)
		{
			ControlText = txtControl.Text;
			if (BtnClick != null)
			{
				BtnClick(sender, e);
			}

		}

		public string ControlText { get; set; }
	}

}
