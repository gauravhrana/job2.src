using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Framework.Components.DataAccess;
using DayCare.Components.BusinessLayer;
using DataModel.Framework.DataAccess;
using DataModel.DayCare;
using System.Data;
using ReferenceData.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.UnitTest.ReferenceData
{
	public partial class Sample : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void Insert_Click(object sender, EventArgs e)
		{
            //var data = new DataModel.ReferenceData.AirportDataModel();
            //data.ApplicationId = SessionVariables.RequestProfile.ApplicationId;
            //data.Description = "Reference Data Entities are getting tested using sample Data";
            //data.Name = "Airport1 Testing1";
            //data.SortOrder = 1;

            //DayCare.Components.BusinessLayer.AirportDataManager.Create(data, SessionVariables.RequestProfile);

            //var dataCity = new DataModel.ReferenceData.CityDataModel();
            //dataCity.ApplicationId = SessionVariables.RequestProfile.ApplicationId;
            //dataCity.Description = "Reference Data Entities are getting tested using sample Data";
            //dataCity.Name = "City Testing1";
            //dataCity.SortOrder = 1;

            //DayCare.Components.BusinessLayer.CityDataManager.Create(dataCity, SessionVariables.RequestProfile);

            //var dataCalendar = new DataModel.ReferenceData.CalendarDataModel();
            //dataCalendar.ApplicationId = SessionVariables.RequestProfile.ApplicationId;
            //dataCalendar.Description = "Reference Data Entities are getting tested using sample Data";
            //dataCalendar.Name = "Calendar jan";
            //dataCalendar.SortOrder = 1;

            //DayCare.Components.BusinessLayer.CalendarDataManager.Create(dataCalendar, SessionVariables.RequestProfile);

            //var dataContinent = new DataModel.ReferenceData.ContinentDataModel();
            //dataContinent.ApplicationId = SessionVariables.RequestProfile.ApplicationId;
            //dataContinent.Description = "Reference Data Entities are getting tested using sample Data";
            //dataContinent.Name = "Continent Testing1";
            //dataContinent.SortOrder = 1;

            //DayCare.Components.BusinessLayer.ContinentDataManager.Create(dataContinent, SessionVariables.RequestProfile);


            //var dataCountry = new DataModel.ReferenceData.CountryDataModel();
            //dataCountry.ApplicationId = SessionVariables.RequestProfile.ApplicationId;
            //dataCountry.Description = "Reference Data Entities are getting tested using sample Data";
            //dataCountry.Name = "America";
            //dataCountry.SortOrder = 1;

            //DayCare.Components.BusinessLayer.CountryDataManager.Create(dataCountry, SessionVariables.RequestProfile);


            //var modelCurrency = new DataModel.ReferenceData.CurrencyDataModel();
            //modelCurrency.ApplicationId = SessionVariables.RequestProfile.ApplicationId;
            //modelCurrency.Description = "Reference Data Entities are getting tested using sample Data";
            //modelCurrency.Name = "Dollar";
            //modelCurrency.SortOrder = 1;

            //DayCare.Components.BusinessLayer.CurrencyDataManager.Create(modelCurrency, SessionVariables.RequestProfile);

            //var modelGR = new DataModel.ReferenceData.GeographicRegionDataModel();
            //modelGR.ApplicationId = SessionVariables.RequestProfile.ApplicationId;
            //modelGR.Description = "Reference Data Entities are getting tested using sample Data";
            //modelGR.Name = "Russia Geographic Region Entity";
            //modelGR.SortOrder = 1;

            //DayCare.Components.BusinessLayer.GeographicRegionDataManager.Create(modelGR, SessionVariables.RequestProfile);

            var modelHL = new DataModel.ReferenceData.HelpLineDataModel();
            modelHL.ApplicationId = SessionVariables.RequestProfile.ApplicationId;
            modelHL.Description = "Reference Data Entities are getting tested using sample Data";
            modelHL.Name = "Fire Bridage";
            modelHL.SortOrder = 1;

            HelpLineDataManager.Create(modelHL, SessionVariables.RequestProfile);

            //var modelHoliday = new DataModel.ReferenceData.HolidayDataModel();
            //modelHoliday.ApplicationId = SessionVariables.RequestProfile.ApplicationId;
            //modelHoliday.Description = "Reference Data Entities are getting tested using sample Data";
            //modelHoliday.Name = "Sunday";
            //modelHoliday.SortOrder = 1;

            //DayCare.Components.BusinessLayer.HolidayDataManager.Create(modelHoliday, SessionVariables.RequestProfile);

            //var modelMall = new DataModel.ReferenceData.MallDataModel();
            //modelMall.ApplicationId = SessionVariables.RequestProfile.ApplicationId;
            //modelMall.Description = "Reference Data Entities are getting tested using sample Data";
            //modelMall.Name = "Sunshine Multiplex mall";
            //modelMall.SortOrder = 1;

            //DayCare.Components.BusinessLayer.MallDataManager.Create(modelMall, SessionVariables.RequestProfile);

            //var modelMonument = new DataModel.ReferenceData.MonumentDataModel();
            //modelMonument.ApplicationId = SessionVariables.RequestProfile.ApplicationId;
            //modelMonument.Description = "Reference Data Entities are getting tested using sample Data";
            //modelMonument.Name = "Assiant Monument";
            //modelMonument.SortOrder = 1;

            //DayCare.Components.BusinessLayer.MonumentDataManager.Create(modelMonument, SessionVariables.RequestProfile);
		}

		protected void Update_Click(object sender, EventArgs e)
		{
            //var data = new DataModel.ReferenceData.AirportDataModel();
            //data.AirportId = 10011;
            //data.Description = "Testing Update functionality";
            //data.Name = "Airport Test";
            //data.SortOrder = 10;

            //DayCare.Components.BusinessLayer.AirportDataManager.Update(data, SessionVariables.RequestProfile);

            //var dataCity = new DataModel.ReferenceData.CityDataModel();
            //dataCity.CityId = 10014;
            //dataCity.Description = "Testing Update functionality";
            //dataCity.Name = "City Testing1";
            //dataCity.SortOrder = 10;

            //DayCare.Components.BusinessLayer.CityDataManager.Update(dataCity, SessionVariables.RequestProfile);

            //var dataCalendar = new DataModel.ReferenceData.CalendarDataModel();
            //dataCalendar.CalendarId = 10010;
            //dataCalendar.Description = "Testing Update functionality";
            //dataCalendar.Name = "Calendar jan";
            //dataCalendar.SortOrder = 10;

            //DayCare.Components.BusinessLayer.CalendarDataManager.Update(dataCalendar, SessionVariables.RequestProfile);

            //var dataContinent = new DataModel.ReferenceData.ContinentDataModel();
            //dataContinent.ContinentId = 10010;
            //dataContinent.Description = "Testing Update functionality";
            //dataContinent.Name = "Continent Testing1";
            //dataContinent.SortOrder = 10;

            //DayCare.Components.BusinessLayer.ContinentDataManager.Update(dataContinent, SessionVariables.RequestProfile);


            //var dataCountry = new DataModel.ReferenceData.CountryDataModel();
            //dataCountry.CountryId = 37484;
            //dataCountry.Description = "Testing Update functionality";
            //dataCountry.Name = "America";
            //dataCountry.SortOrder = 10;

            //DayCare.Components.BusinessLayer.CountryDataManager.Update(dataCountry, SessionVariables.RequestProfile);


            //var modelCurrency = new DataModel.ReferenceData.CurrencyDataModel();
            //modelCurrency.CurrencyId = 10000;
            //modelCurrency.Description = "Testing Update functionality";
            //modelCurrency.Name = "Dollar";
            //modelCurrency.SortOrder = 10;

            //DayCare.Components.BusinessLayer.CurrencyDataManager.Update(modelCurrency, SessionVariables.RequestProfile);

            //var modelGR = new DataModel.ReferenceData.GeographicRegionDataModel();
            //modelGR.GeographicRegionId = 37474;
            //modelGR.Description = "Testing Update functionality";
            //modelGR.Name = "Russia Geographic Region Entity";
            //modelGR.SortOrder = 10;

            //DayCare.Components.BusinessLayer.GeographicRegionDataManager.Update(modelGR, SessionVariables.RequestProfile);

            var modelHL = new DataModel.ReferenceData.HelpLineDataModel();
            modelHL.HelpLineId = 21;
            modelHL.Description = "Testing Update functionality";
            modelHL.Name = "Fire Bridage";
            modelHL.SortOrder = 10;

            HelpLineDataManager.Update(modelHL, SessionVariables.RequestProfile);

            //var modelHoliday = new DataModel.ReferenceData.HolidayDataModel();
            //modelHoliday.HolidayId = 10000;
            //modelHoliday.Description = "Testing Update functionality";
            //modelHoliday.Name = "Sunday";
            //modelHoliday.SortOrder = 10;

            //DayCare.Components.BusinessLayer.HolidayDataManager.Update(modelHoliday, SessionVariables.RequestProfile);

            //var modelMall = new DataModel.ReferenceData.MallDataModel();
            //modelMall.MallId = 10009;
            //modelMall.Description = "Sunshine Multiplex mall";
            //modelMall.Name = "Testing Mall Entity";
            //modelMall.SortOrder = 10;

            //DayCare.Components.BusinessLayer.MallDataManager.Update(modelMall, SessionVariables.RequestProfile);

            //var modelMonument = new DataModel.ReferenceData.MonumentDataModel();
            //modelMonument.MonumentId = 10003;
            //modelMonument.Description = "Testing Update functionality";
            //modelMonument.Name = "Assiant Monument";
            //modelMonument.SortOrder = 10;

            //DayCare.Components.BusinessLayer.MonumentDataManager.Update(modelMonument, SessionVariables.RequestProfile);
		}

		protected void Search_Click(object sender, EventArgs e)
		{
            DataTable dt = new DataTable();

            //var data = new DataModel.ReferenceData.AirportDataModel();
            //data.Name = "City 1 Test";

            //dt = DayCare.Components.BusinessLayer.AirportDataManager.Search(data, SessionVariables.RequestProfile);
            //gvTest.DataSource = dt;
            //gvTest.DataBind();

            //var dataCity = new DataModel.ReferenceData.CityDataModel();
            //dataCity.Name = "City Testing1";

            //dt = DayCare.Components.BusinessLayer.CityDataManager.Search(dataCity, SessionVariables.RequestProfile);
            //gvTest.DataSource = dt;
            //gvTest.DataBind();

            ////var dataCalendar = new DataModel.ReferenceData.CalendarDataModel();
            ////dataCalendar.Name = "Calendar jan";

            ////dt = DayCare.Components.BusinessLayer.CalendarDataManager.Search(dataCalendar, SessionVariables.RequestProfile);
            ////gvTest.DataSource = dt;
            ////gvTest.DataBind();

            //var dataContinent = new DataModel.ReferenceData.ContinentDataModel();
            //dataContinent.Name = "Continent Testing1";

            //dt = DayCare.Components.BusinessLayer.ContinentDataManager.Search(dataContinent, SessionVariables.RequestProfile);
            //gvTest.DataSource = dt;
            //gvTest.DataBind();

            //var dataCountry = new DataModel.ReferenceData.CountryDataModel();
            //dataCountry.Name = "America";

            //dt = DayCare.Components.BusinessLayer.CountryDataManager.Search(dataCountry, SessionVariables.RequestProfile);
            //gvTest.DataSource = dt;
            //gvTest.DataBind();

            //var modelCurrency = new DataModel.ReferenceData.CurrencyDataModel();
            //modelCurrency.Name = "Dollar";
            //dt = new DataTable();

            //dt = DayCare.Components.BusinessLayer.CurrencyDataManager.Search(modelCurrency, SessionVariables.RequestProfile);
            //gvTest.DataSource = dt;
            //gvTest.DataBind();

            //var modelGR = new DataModel.ReferenceData.GeographicRegionDataModel();
            //modelGR.Name = "Russia Geographic Region Entity";

            //dt = DayCare.Components.BusinessLayer.GeographicRegionDataManager.Search(modelGR, SessionVariables.RequestProfile);
            //gvTest.DataSource = dt;
            //gvTest.DataBind();

            var modelHL = new DataModel.ReferenceData.HelpLineDataModel();
            modelHL.Name = "Fire Bridage";

            var lst = HelpLineDataManager.GetEntityDetails(modelHL, SessionVariables.RequestProfile);
            gvTest.DataSource = lst;
            gvTest.DataBind();

            //var modelHoliday = new DataModel.ReferenceData.HolidayDataModel();
            //modelHoliday.Name = "Sunday";

            //dt = DayCare.Components.BusinessLayer.HolidayDataManager.Search(modelHoliday, SessionVariables.RequestProfile);
            //gvTest.DataSource = dt;
            //gvTest.DataBind();

            //var modelMall = new DataModel.ReferenceData.MallDataModel();
            //modelMall.Name = "Sunshine Multiplex mall";

            //dt = DayCare.Components.BusinessLayer.MallDataManager.Search(modelMall, SessionVariables.RequestProfile);
            //gvTest.DataSource = dt;
            //gvTest.DataBind();

            //var modelMonument = new DataModel.ReferenceData.MonumentDataModel();
            //modelMonument.Name = "Assiant Monument";

            //dt = DayCare.Components.BusinessLayer.MonumentDataManager.Search(modelMonument, SessionVariables.RequestProfile);
            //gvTest.DataSource = dt;
            //gvTest.DataBind();
		}

		protected void Delete_Click(object sender, EventArgs e)
		{
            //var data = new DataModel.ReferenceData.AirportDataModel();
            //data.AirportId = 10021;

            //DayCare.Components.BusinessLayer.AirportDataManager.Delete(data, SessionVariables.RequestProfile);

            //var dataCity = new DataModel.ReferenceData.CityDataModel();
            //dataCity.CityId = 10014;

            //DayCare.Components.BusinessLayer.CityDataManager.Delete(dataCity, SessionVariables.RequestProfile);

            //var dataCalendar = new DataModel.ReferenceData.CalendarDataModel();
            //dataCalendar.CalendarId = 10010;

            //DayCare.Components.BusinessLayer.CalendarDataManager.Delete(dataCalendar, SessionVariables.RequestProfile);

            //var dataContinent = new DataModel.ReferenceData.ContinentDataModel();
            //dataContinent.ContinentId = 10010;

            //DayCare.Components.BusinessLayer.ContinentDataManager.Delete(dataContinent, SessionVariables.RequestProfile);


            //var dataCountry = new DataModel.ReferenceData.CountryDataModel();
            //dataCountry.CountryId = 37484;

            //DayCare.Components.BusinessLayer.CountryDataManager.Delete(dataCountry, SessionVariables.RequestProfile);


            //var modelCurrency = new DataModel.ReferenceData.CurrencyDataModel();
            //modelCurrency.CurrencyId = 37479;

            //DayCare.Components.BusinessLayer.CurrencyDataManager.Delete(modelCurrency, SessionVariables.RequestProfile);

            //var modelGR = new DataModel.ReferenceData.GeographicRegionDataModel();
            //modelGR.GeographicRegionId = 37475;

            //DayCare.Components.BusinessLayer.GeographicRegionDataManager.Delete(modelGR, SessionVariables.RequestProfile);

            var modelHL = new DataModel.ReferenceData.HelpLineDataModel();
            modelHL.HelpLineId = 21;

            HelpLineDataManager.Delete(modelHL, SessionVariables.RequestProfile);

            //var modelHoliday = new DataModel.ReferenceData.HolidayDataModel();
            //modelHoliday.HolidayId = 10009;

            //DayCare.Components.BusinessLayer.HolidayDataManager.Delete(modelHoliday, SessionVariables.RequestProfile);

            //var modelMall = new DataModel.ReferenceData.MallDataModel();
            //modelMall.MallId = 10009;

            //DayCare.Components.BusinessLayer.MallDataManager.Delete(modelMall, SessionVariables.RequestProfile);

            //var modelMonument = new DataModel.ReferenceData.MonumentDataModel();
            //modelMonument.MonumentId = 10003;

            //DayCare.Components.BusinessLayer.MonumentDataManager.Delete(modelMonument, SessionVariables.RequestProfile);
		}


	}
}