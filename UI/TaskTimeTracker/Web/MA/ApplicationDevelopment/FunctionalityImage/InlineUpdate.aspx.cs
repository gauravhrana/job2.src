using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using Framework.UI.Web.BaseClasses;
using System.Text;
using TaskTimeTracker.Components.Module.ApplicationDevelopment;
using Dapper;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityImage
{
    public partial class InlineUpdate : PageInlineUpdate
    {
		#region Methods

		protected override  DataTable GetData()
        {
            try
            {
                SuperKey = ApplicationCommon.GetSuperKey();
                SetId = ApplicationCommon.GetSetId();

				var selectedrows = new List<FunctionalityImageDataModel>();
                var functionalityImagedata = new FunctionalityImageDataModel();

                if (!string.IsNullOrEmpty(SuperKey))
                {
					var systemEntityTypeId = (int)PrimaryEntity;
					var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

					foreach (var entityKey in lstEntityKeys)
					{
						functionalityImagedata.FunctionalityImageId = entityKey;
						var result = FunctionalityImageDataManager.GetDetails(functionalityImagedata, SessionVariables.RequestProfile);
						selectedrows.Add(result);
					}
                }
                else if (SetId != 0)
                {
                    var key = SetId;
					functionalityImagedata.FunctionalityImageId = key;
					var result = FunctionalityImageDataManager.GetDetails(functionalityImagedata, SessionVariables.RequestProfile);
                    selectedrows.Add(result);

                }
                return selectedrows.ToDataTable();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            return null;
        }		

		protected override void Update(Dictionary<string, string> values)
        {
            var data = new FunctionalityImageDataModel();

            // copies properties from values dictionary object to data object
            PropertyMapper.CopyProperties(data, values);

			FunctionalityImageDataManager.Update(data, SessionVariables.RequestProfile);
			base.Update(values);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FunctionalityImage;
            PrimaryEntityKey = "FunctionalityImage";

            InlineEditingListCore = InlineEditingList;
            BreadCrumbObject = Master.BreadCrumbObject;

            base.OnInit(e);
        }

        #endregion

    }
}
