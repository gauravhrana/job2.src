using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;
using Framework.Components.DataAccess;

namespace Shared.UI.Web.Controls
{
    public partial class ControlSearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilter
    {
        
        #region Properties

        public delegate StandardDataModel GetSearchParametersDelegate(ControlSearchFilter controlSearchFilter);
        private GetSearchParametersDelegate _getSearchParameters;

        public StandardDataModel SearchParameters
        {
            get
            {
                return _getSearchParameters(this);
            }
        }

        #endregion

        #region Methods

        public void Setup(SystemEntity primaryEntity, string primaryEntityKey, GetSearchParametersDelegate getSearchParametersDelegate)
        {
            PrimaryEntity = primaryEntity;
            PrimaryEntityKey = primaryEntityKey;
            _getSearchParameters = getSearchParametersDelegate;
        }
        
        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            //PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Client;
            //PrimaryEntityKey = "Client";
            
            FolderLocationFromRoot = PrimaryEntityKey;

            SearchActionBarCore = oSearchActionBar;
            SearchParametersRepeaterCore = SearchParametersRepeater;
        }

        #endregion

    }
}