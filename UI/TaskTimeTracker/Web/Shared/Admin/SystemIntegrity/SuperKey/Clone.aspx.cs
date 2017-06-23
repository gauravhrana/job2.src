using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.SystemIntegrity.SuperKey
{
    public partial class Clone : Framework.UI.Web.BaseClasses.PageClone
    {

        #region private methods

        protected override void InsertData()
        {
            var data = new SuperKeyDataModel();

            data.SuperKeyId         = myGenericControl.SuperKeyId;
            data.Name               = myGenericControl.Name;
            data.Description        = myGenericControl.Description;
            data.SortOrder          = myGenericControl.SortOrder;
            data.SystemEntityTypeId = myGenericControl.SystemEntityTypeId;
            data.ExpirationDate     = myGenericControl.ExpirationDate;

			Framework.Components.Core.SuperKeyDataManager.Create(data, SessionVariables.RequestProfile);
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
			SuperKey = ApplicationCommon.GetSuperKey();
			SetId = ApplicationCommon.GetSetId();

			// load on first direct loading of this page
			// don't want to reload everttime, as it would 
			// reset the values the user had put.
			if (!IsPostBack)
			{
				try
				{
					myGenericControl.SetId(SetId, true);
				}
                catch (Exception ex)
                {

                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    //throw
                }

                //LoadData(setId);
            }

        }

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			
			SettingCategory = "SuperKeyDefaultView";
			
		}

        protected void lnkSave_Click(object sender, EventArgs e)
        {

            //Framework.Components.Import.Application.Create(int.Parse(txtApplicationId.Text), txtName.Text, txtDescription.Text,txtSortOrder.Text);
            InsertData();
            Response.Redirect(Page.GetRouteUrl("SuperKeyEntityRoute", new { Action = "Default", SetId = true }), false);
        }

        

        #endregion

    }
}