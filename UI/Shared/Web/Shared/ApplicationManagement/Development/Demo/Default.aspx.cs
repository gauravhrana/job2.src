using System;
using System.Data;

namespace Shared.UI.Web.ApplicationManagement.Development.Demo
{
    public partial class Default : Framework.UI.Web.BaseClasses.PageDefault
    {
        protected override DataTable GetData()
        {
			//var data = oSearchFilter.SearchParameters;

			//var dt = Components.BusinessLayer.Layer.Search(data, AuditId);

			//return dt;

			return new DataTable();
        }

		//protected override string[] GetColumns()
		//{
		//    //return Components.BusinessLayer.ApplicationSecurity.GetLayerColumns("DBColumns", AuditId);

		//    return new string[]();
		//}


        protected void Page_Load(object sender, EventArgs e)
        {
			//oList.Setup("Demo", " ", "LayerId", true, GetData, GetColumns);
			//WebCustomControl1.Setup("Layer", " ", "LayerId", true, GetData, GetColumns);

            oSearchFilter.OnSearch += oSearchFilter_OnSearch;

			lnkExport.NavigateUrl = "~/Demo/Export.aspx?SearchCondition=" + oSearchFilter.SearchParameters.ToURLQuery();
        }

        void oSearchFilter_OnSearch(object sender, EventArgs e)
        {
			oList.ShowData(false, true);
			//WebCustomControl1.ShowData(false, true);
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            Response.Redirect("Insert.aspx");
        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("http://localhost:1206/Default.aspx");
        }    
    }
}