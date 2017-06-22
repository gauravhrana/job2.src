using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataModel.CapitalMarkets;
using DataModel.DayCare;
using DayCare.Components.BusinessLayer;
using Newtonsoft.Json;
using RestSharp.Contrib;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.APIV2.Rest
{
    public class BathRoomsController : ApiController
    {
        // GET api/products
        public HttpResponseMessage  Get()
        {
            var dataQuery = new BathRoomDataModel();
            var list = BathRoomDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

            if (list == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return Request.CreateResponse<IEnumerable<BathRoomDataModel>>(HttpStatusCode.OK, list);
        }

        public HttpResponseMessage Get(int id)
        {
            var dataQuery = new BathRoomDataModel();
            dataQuery.BathRoomId = id;
            var list = BathRoomDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);
            if (list.Count > 0)
            {
                return Request.CreateResponse<BathRoomDataModel>(HttpStatusCode.OK, list[0]);
            }
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        // this method searches based on data string which is the serialized json string of data model
        //public IEnumerable<BathRoomDataModel> Get(string data)
        //{
        //    var dataQuery = JsonConvert.DeserializeObject<BathRoomDataModel>(data);
        //    return BathRoomDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);
        //}

        public HttpResponseMessage Post(BathRoomDataModel obj)
        {
            BathRoomDataManager.Create(obj, SessionVariables.RequestProfile);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        // PUT api/bathrooms/5
        public HttpResponseMessage Put(BathRoomDataModel obj)
        {
            //obj.BathRoomId = id;
            BathRoomDataManager.Update(obj, SessionVariables.RequestProfile);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        // PUT api/bathrooms/5
        public HttpResponseMessage Put(int id, BathRoomDataModel obj)
        {
            obj.BathRoomId = id;
            BathRoomDataManager.Update(obj, SessionVariables.RequestProfile);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        // DELETE api/bathrooms/5
        public HttpResponseMessage Delete(BathRoomDataModel obj)
        {
            //var dataQuery = new BathRoomDataModel(){BathRoomId = id};
            BathRoomDataManager.Delete(obj, SessionVariables.RequestProfile);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        // DELETE api/bathrooms/5
        public HttpResponseMessage Delete(int id)
        {
            var dataQuery = new BathRoomDataModel(){BathRoomId = id};
            BathRoomDataManager.Delete(dataQuery, SessionVariables.RequestProfile);
            return new HttpResponseMessage(HttpStatusCode.OK);

        }
    }
}
