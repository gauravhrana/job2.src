using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.SystemIntegrity.SuperKeyDetail
{
    public partial class Insert : Framework.UI.Web.BaseClasses.PageInsert
    {

        #region private methods

        protected void InsertData()
        {
            var data = new SuperKeyDetailDataModel();

			data.SuperKeyDetailId	 = myGenericControl.SuperKeyDetailId; 
            data.SuperKeyId		     = int.Parse(myGenericControl.SuperKeyId.ToString());
            data.EntityKey			 = myGenericControl.EntityKey;

			Framework.Components.Core.SuperKeyDetailDataManager.Create(data, SessionVariables.RequestProfile);
        }

        #endregion

        #region Events

        

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			
			SettingCategory = "SuperKeyDetailDefaultView";
			
		}

        

        

        #endregion

    }
}