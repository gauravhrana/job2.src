using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataModel.TaskTimeTracker;
using TaskTimeTracker.Components.BusinessLayer;

namespace MvcWebAPISample.Controllers
{
    public class ClientsController : ApiController
    {

        // GET api/<controller>
        public IEnumerable<ClientDataModel> GetAllClients()
        {
            int auditId = 10;
            var data = new ClientDataModel();
            return ClientDataManager.GetEntityDetails(data, SessionVariables.RequestProfile, 0);
        }

        public ClientDataModel GetClient(int id)
        {
            int auditId = 10;
            var data = new ClientDataModel();
            data.ClientId = id;
            var results = ClientDataManager.GetEntityDetails(data, SessionVariables.RequestProfile, 0);
            if (results == null || results.Count == 0)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                 {
                     Content = new StringContent(string.Format("No Client with ID = {0}", id)),
                     ReasonPhrase = "Client ID Not Found"
                 };
                throw new HttpResponseException(resp);
            }
            return results[0];
        }

        public HttpResponseMessage PostClient(ClientDataModel data)
        {
            int auditId = 10;
            data.ClientId = null;
            data.ClientId = ClientDataManager.Create(data, SessionVariables.RequestProfile);

            var response = Request.CreateResponse<ClientDataModel>(HttpStatusCode.Created, data);
            response.Headers.Location = new Uri(Request.RequestUri, "/api/clients/" + data.ClientId.ToString());
            return response;
        }

        public void PutClient(int id, ClientDataModel data)
        {
            int auditId = 10;
            data.ClientId = id;
            ClientDataManager.UpdateSortOrderOnly(data, auditId);
        }

        public void DeleteClient(int id)
        {
            int auditId = 10;
            var data = new ClientDataModel();
            data.ClientId = id;
            ClientDataManager.Delete(data, SessionVariables.RequestProfile);
        }

    }
}
