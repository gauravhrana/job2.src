﻿using System;

namespace Shared.UI.Web.ApplicationManagement.Development.Demo
{
    public partial class Details : Framework.UI.Web.BaseClasses.PageDetails
    {
        private int _setId = -1;

    	private int SetId
    	{
    		get
    		{
    			try
    			{
					_setId = int.Parse(Request.QueryString["SetId"]);
    			}
    			catch (Exception)
    			{
    				_setId = -1;    				
    			}

    			return _setId;
    		}
    	}

    	private void ShowData()
        {
			//xyz.SetId = SetId;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

		protected void btnClone_Click(object sender, EventArgs e)
		{
			Response.Redirect("Clone.aspx?SetId=" + SetId + "&Home=" + "Details");
		}
        
        protected void Page_Load(object sender, EventArgs e)
        {       
    		EnsureChildControls();
            ShowData();
        }
    }
}