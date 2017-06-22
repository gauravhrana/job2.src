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
    public partial class Insert : Framework.UI.Web.BaseClasses.PageInsert
    {

        #region private methods

        protected void InsertData()
        {
            var data = new SuperKeyDataModel();

			data.SuperKeyId				= myGenericControl.SuperKeyId; 
            data.Name					= myGenericControl.Name;
            data.Description			= myGenericControl.Description;
            data.SortOrder				= int.Parse(myGenericControl.SortOrder.ToString());
            
            data.SystemEntityTypeId		= int.Parse(myGenericControl.SystemEntityTypeId.ToString());
            data.ExpirationDate			= myGenericControl.ExpirationDate;

			Framework.Components.Core.SuperKeyDataManager.Create(data, SessionVariables.RequestProfile);
        }

        #endregion

        #region Events

        

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			
			SettingCategory = "SuperKeyDefaultView";
			
		}

        

        

        #endregion

    }
}