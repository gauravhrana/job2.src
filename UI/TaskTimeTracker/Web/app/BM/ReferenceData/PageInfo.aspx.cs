using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApplicationContainer.UI.Web.app.BM.ReferenceData
{
    public partial class PageInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<KeyValuePair<string, string>> lstItems = new List<KeyValuePair<string, string>>();

            lstItems.Add(new KeyValuePair<string, string>("Address", "The particulars of the place where someone lives or an organization is situated"));
            lstItems.Add(new KeyValuePair<string, string>("Airport", "A complex of runways and buildings for the takeoff, landing, and maintenance of civil aircraft, with facilities for passengers."));
            lstItems.Add(new KeyValuePair<string, string>("Calendar", "A chart or series of pages showing the days, weeks, and months of a particular year, or giving particular seasonal information."));
            lstItems.Add(new KeyValuePair<string, string>("City", "A place or situation characterized by a specified attribute"));
            lstItems.Add(new KeyValuePair<string, string>("Continent", "A mainland contrasted with islands."));
            lstItems.Add(new KeyValuePair<string, string>("Country", "A nation with its own government, occupying a particular territory"));
            lstItems.Add(new KeyValuePair<string, string>("Currency", "A system of money in general use in a particular country."));
            lstItems.Add(new KeyValuePair<string, string>("Ethnicity", "The fact or state of belonging to a social group that has a common national or cultural tradition."));
            lstItems.Add(new KeyValuePair<string, string>("Gender", "The state of being male or female"));
            lstItems.Add(new KeyValuePair<string, string>("GeographicRegion", "Regions are areas that are broadly divided by physical characteristics, human impact characteristics, and the interaction of humanity and the environment"));
            lstItems.Add(new KeyValuePair<string, string>("Helpline", "A telephone service providing help with problems"));
            lstItems.Add(new KeyValuePair<string, string>("Holiday", "A day of festivity or recreation when no work is done"));
            lstItems.Add(new KeyValuePair<string, string>("Mall", "A large building or series of connected buildings containing a variety of retail stores and typically also restaurants"));
            lstItems.Add(new KeyValuePair<string, string>("Monument", "A statue, building, or other structure erected to commemorate a famous or notable person or event"));
            lstItems.Add(new KeyValuePair<string, string>("PersonSuffix", "Person sir name or father's last name"));
            lstItems.Add(new KeyValuePair<string, string>("Province", "A principal administrative division of certain countries or empires"));
            lstItems.Add(new KeyValuePair<string, string>("ProvinceType", "Types of division of countries"));
            lstItems.Add(new KeyValuePair<string, string>("Region", "An area or division, especially part of a country or the world having definable characteristics but not always fixed boundaries"));
            lstItems.Add(new KeyValuePair<string, string>("Religion", "The belief in and worship of a superhuman controlling power, especially a personal God or gods."));
            lstItems.Add(new KeyValuePair<string, string>("State", "The particular condition that someone or something is in at a specific time."));
            lstItems.Add(new KeyValuePair<string, string>("TimeZone", "A region of the globe that observes a uniform standard time for legal, commercial, and social purposes"));
            lstItems.Add(new KeyValuePair<string, string>("TrainStation", "A place where passengers can get on and off trains and/or goods may be loaded or unloaded"));
        

            gridContent.DataSource = lstItems;
            gridContent.DataBind();

        }
    }
}