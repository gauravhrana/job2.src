using CapitalMarkets.Components.BusinessLayer;
using DataModel.CapitalMarkets;
using Newtonsoft.Json.Linq;
using Shared.WebCommon.UI.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace Web.Api.Controllers
{
    public class FundXPortfolioController : ApiController
    {
        
        public IEnumerable<JObject> GetSourceEntityList(string value)
        {
            var sourceEntity = new JavaScriptSerializer().Deserialize<string>(value);
            var listResult = new List<JObject>();

            if (sourceEntity == "Fund")
            {
                var dataQuery = new FundDataModel();
                var entityItems = FundDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

                foreach (var obj in entityItems)
                {
                    var jObject = new JObject();

                    jObject["Text"] = obj.Name;
                    jObject["EntityKey"] = obj.FundId;

                    listResult.Add(jObject);
                }
            }
            else if(sourceEntity == "Portfolio")
            {
                var dataQuery = new PortfolioDataModel();
                var entityItems = PortfolioDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

                foreach (var obj in entityItems)
                {
                    var jObject = new JObject();

                    jObject["Text"] = obj.Name;
                    jObject["EntityKey"] = obj.PortfolioId;

                    listResult.Add(jObject);
                }
            }

            return listResult;
        }

        public IEnumerable<FundXPortfolioDataModel> GetEntityRecords(string value, string value1)
        {
            var sourceEntity = new JavaScriptSerializer().Deserialize<string>(value);
            var dataQuery = new FundXPortfolioDataModel();

            if (sourceEntity == "Fund")
            {
                var fundId = new JavaScriptSerializer().Deserialize<string>(value1);
                dataQuery.FundId = int.Parse(fundId);
            }
            else if(sourceEntity == "Portfolio")
            {
                var portfolioId = new JavaScriptSerializer().Deserialize<string>(value1);
                dataQuery.PortfolioId = int.Parse(portfolioId);
            }

            return FundXPortfolioDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile, 0);
        }

        [System.Web.Http.AcceptVerbs("DELETE", "GET", "POST")]
        public JObject AddEntityRecords(string value1, string value2, string value3)
        {
            var sourceEntity = new JavaScriptSerializer().Deserialize<string>(value1);
            var sourceEntityId = new JavaScriptSerializer().Deserialize<string>(value2);
            var targetEntityList = new JavaScriptSerializer().Deserialize<List<string>>(value3);

            foreach (var tmpValue in targetEntityList)
            {                
                var data = new FundXPortfolioDataModel();

                if (sourceEntity == "Fund")
                {
                    data.FundId = int.Parse(sourceEntityId);
                    data.PortfolioId = int.Parse(tmpValue);
                }
                else if(sourceEntity == "Portfolio")
                {
                    data.PortfolioId = int.Parse(sourceEntityId);
                    data.FundId = int.Parse(tmpValue);
                }

                FundXPortfolioDataManager.Create(data, SessionVariables.RequestProfile);
            }

            var jObject = new JObject();
            jObject["Result"] = true;
            return jObject;
        }

        [System.Web.Http.AcceptVerbs("DELETE", "GET", "POST")]
        public JObject RemoveEntityRecords(string value, string value1)
        {
            var sourceEntity = new JavaScriptSerializer().Deserialize<string>(value);
            var dataQuery = new FundXPortfolioDataModel();

            if (sourceEntity == "Fund")
            {
                var fundId = new JavaScriptSerializer().Deserialize<string>(value1);
                dataQuery.FundId = int.Parse(fundId);
            }
            else if (sourceEntity == "Portfolio")
            {
                var portfolioId = new JavaScriptSerializer().Deserialize<string>(value1);
                dataQuery.PortfolioId = int.Parse(portfolioId);
            }
            else
            {
                dataQuery.FundId = -1;
                dataQuery.PortfolioId = -1;
            }

            FundXPortfolioDataManager.Delete(dataQuery, SessionVariables.RequestProfile);

            var jObject = new JObject();
            jObject["Result"] = true;
            return jObject;
        }
    }
}
