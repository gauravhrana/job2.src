using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApplicationContainer.UI.Web.app.BM.CapitalMarkets
{
    public partial class PageInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<KeyValuePair<string, string>> lstItems = new List<KeyValuePair<string, string>>();
            lstItems.Add(new KeyValuePair<string, string>("AssetType", "Two types of asset fixed asset - Land, building & Motor vehicles And current asset - Like trade debtor & cash at bank"));
            lstItems.Add(new KeyValuePair<string, string>("InvestmentType", "Anything we own & use for personal or investment purpose like home , car, stocks or bonds"));
            lstItems.Add(new KeyValuePair<string, string>("RecordType", " Let you offer different business processes, picklist values, and page layouts to different users"));
            lstItems.Add(new KeyValuePair<string, string>("Sector", "Proportional compass or military compass this was a major calculating instrument"));
            lstItems.Add(new KeyValuePair<string, string>("SecurityClass", "List standard or class of security"));
            lstItems.Add(new KeyValuePair<string, string>("Security", "Consulting and investigations and specialized security"));
            lstItems.Add(new KeyValuePair<string, string>("SecurityType", "Types of security"));
            lstItems.Add(new KeyValuePair<string, string>("SecurityTypeGroup", "We can group security type"));
            lstItems.Add(new KeyValuePair<string, string>("OrderType", "...."));
            lstItems.Add(new KeyValuePair<string, string>("OrderStatusGroup", "....."));
            lstItems.Add(new KeyValuePair<string, string>("OrderStatusType", "...."));
            lstItems.Add(new KeyValuePair<string, string>("OrderItem", "...."));
            lstItems.Add(new KeyValuePair<string, string>("OrderStatus", "...."));


            lstItems.Add(new KeyValuePair<string, string>("AccountingParameters", "Will be calculated in terms of - Income from financial intermediation, Expenses from Financial Intermediation, Operating Income and Expenses, Non Operating Income, Profit Sharing"));
            lstItems.Add(new KeyValuePair<string, string>("AccountingView", "....."));
            lstItems.Add(new KeyValuePair<string, string>("AccountSpecificType", "...."));
            lstItems.Add(new KeyValuePair<string, string>("AccountSubType", "...."));
            lstItems.Add(new KeyValuePair<string, string>("AdjustmentCategory", "...."));








            
            gridContent.DataSource = lstItems;
            gridContent.DataBind();

        }
    }
}