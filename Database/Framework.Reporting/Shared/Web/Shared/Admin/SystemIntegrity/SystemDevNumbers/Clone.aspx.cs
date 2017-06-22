using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.SystemDevNumbers
{
    public partial class Clone : Framework.UI.Web.BaseClasses.PageClone
    {

        #region private methods

        protected override void InsertData()
        {
            var data = new SystemDevNumbersDataModel();

            data.SystemDevNumbersId = myGenericControl.SystemDevNumbersId;
            data.PersonId = myGenericControl.PersonId;
            data.RangeFrom= myGenericControl.RangeFrom;
            data.RangeTo = myGenericControl.RangeTo;

            Framework.Components.Core.SystemDevNumbersDataManager.Create(data, SessionVariables.RequestProfile);
        }

        #endregion

        #region Events        

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			
			SettingCategory = "SystemDevNumbersDefaultView";
			
		}

        #endregion

    }
}