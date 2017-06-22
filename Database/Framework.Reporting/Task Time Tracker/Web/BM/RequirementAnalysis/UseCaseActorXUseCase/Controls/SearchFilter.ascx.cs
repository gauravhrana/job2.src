using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using System.Data;
using DataModel.TaskTimeTracker.RequirementAnalysis;

namespace ApplicationContainer.UI.Web.UseCaseActorXUseCase.Controls
{
    public partial class SearchFilter : ControlSearchFilterEntity
    {

        public UseCaseActorXUseCaseDataModel SearchParameters
        {
            get
            {
                var data = new UseCaseActorXUseCaseDataModel();

                SearchFilterControl.SetSearchParameters(data);

                CommonSearchParameters();

                return data;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            BaseSearchFilterControl = SearchFilterControl;
            SearchFilterControl.GetKendoComboBoConfigString = LoadKendoComboBoxSources;
        }

    }
}