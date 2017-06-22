using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataModel.Framework.Core;
using Framework.Components.Core;
using Framework.Components.DataAccess;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;
using Newtonsoft.Json;


namespace Web.Api.Controllers
{
	//[Authorize]
	public class MenuCategoryController : ApiController
	{

		// GET api/summary/GetList
		public IEnumerable<MenuCategoryDataModel> GetList(string value, string value1)
        {
            var settingCategory = value1;
            var searchString = value;

            var dictionaryObject = JsonConvert.DeserializeObject<Dictionary<string, string>>(searchString);
                        
            // save search filter parameters in user preference
            if (dictionaryObject != null)
            {
                foreach (var searchFilterColumnName in dictionaryObject.Keys)
                {
                    var searchFilterValue = dictionaryObject[searchFilterColumnName];
                    PerferenceUtility.UpdateUserPreference(settingCategory, searchFilterColumnName, dictionaryObject[searchFilterColumnName]);
                }
            }

            var dataQuery = JsonConvert.DeserializeObject<MenuCategoryDataModel>(searchString);
			return MenuCategoryDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);
		}

		public void Create([FromBody]MenuCategoryDataModel data)
		{
			MenuCategoryDataManager.Create(data, SessionVariables.RequestProfile);
		}

		public void Update([FromBody]MenuCategoryDataModel data)
		{
			MenuCategoryDataManager.Update(data, SessionVariables.RequestProfile);
		}

		public MenuCategoryDataModel GetById(string value)
		{
			var dataQuery = new MenuCategoryDataModel();

			dataQuery.MenuCategoryId = int.Parse(value);

			var result = MenuCategoryDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile, 1);

			return result[0];
		}

		[System.Web.Http.AcceptVerbs("DELETE", "GET", "POST")]
		public void Delete(string value)
		{
			var dataQuery = new MenuCategoryDataModel();
			dataQuery.MenuCategoryId = int.Parse(value);
			MenuCategoryDataManager.Delete(dataQuery, SessionVariables.RequestProfile);
		}

	}
}
