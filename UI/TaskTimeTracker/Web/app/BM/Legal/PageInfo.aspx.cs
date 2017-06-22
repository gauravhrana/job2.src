using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApplicationContainer.UI.Web.app.BM.Legal
{
    public partial class PageInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<KeyValuePair<string, string>> lstItems = new List<KeyValuePair<string, string>>();

            lstItems.Add(new KeyValuePair<string, string>("CaseStatus", "An information system designed to provide information on pending and disposed cases"));
            lstItems.Add(new KeyValuePair<string, string>("CaseType", "Types of cases like - civil claims & criminal cases"));
            lstItems.Add(new KeyValuePair<string, string>("ClientType", "Recors of types of client in company"));
            lstItems.Add(new KeyValuePair<string, string>("Counsel", "An advise opinion or instruction given in directing the judgment or conduct of another"));
            lstItems.Add(new KeyValuePair<string, string>("Jurisdictions", "The official power to make legal decisions and judgments"));
            lstItems.Add(new KeyValuePair<string, string>("MovantType", "A person who makes a motion before a court"));
            lstItems.Add(new KeyValuePair<string, string>("PressReleaseType", "Record of types of press release"));
            lstItems.Add(new KeyValuePair<string, string>("ReportingRequirement", "Record of report requirement"));
            lstItems.Add(new KeyValuePair<string, string>("ReportType", "Associate different business processes and subset of pick list value to different users based on their user profile"));
            lstItems.Add(new KeyValuePair<string, string>("RetrievalMethod", "The activity of obtaining information resources relevant to an information need from a collection of information resources"));
            lstItems.Add(new KeyValuePair<string, string>("SettlementStatus", "Record of settlement status of student"));
            
            gridContent.DataSource = lstItems;
            gridContent.DataBind();

        }
    }
}