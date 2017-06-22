using DataModel.Framework.Core;
using Framework.Components.Core;
using Shared.WebCommon.UI.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shared.UI.Web.Admin
{
	public partial class SearchKeyGenerator : Framework.UI.Web.BaseClasses.PageBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var searchKeyId = Page.Request.QueryString["SearchKey"];
            var entityName = Page.Request.QueryString["entityName"];

            var parameter1 = string.Empty;
            var parameter1Value = string.Empty;

            var parameter2 = string.Empty;
            var parameter2Value = string.Empty;

            if (Page.Request.QueryString.Keys.Count > 2)
            {
                parameter1 = Page.Request.QueryString.Keys[2];
                parameter1Value = Page.Request.QueryString[2].ToString();
            }

            if (Page.Request.QueryString.Keys.Count > 3)
            {
                parameter2 = Page.Request.QueryString.Keys[3];
                parameter2Value = Page.Request.QueryString[3].ToString();
            }

            if (!string.IsNullOrEmpty(searchKeyId))
            {

                var dataSearchKey = new SearchKeyDataModel();
                dataSearchKey.SearchKeyId = Convert.ToInt32(searchKeyId);

                var ds = SearchKeyDataManager.SearchByKey(dataSearchKey, SessionVariables.RequestProfile);

                var newSearchKeyId = GetSearchKey(ds, parameter1, parameter1Value, parameter2, parameter2Value, entityName);

                var url = Page.GetRouteUrl(entityName + "EntityRouteSearch", new { SearchKey = newSearchKeyId });

                Response.Redirect(url, false);

            }

        }

        private int GetSearchKey(DataSet ds, string parameter1, string parameter1Value
            , string parameter2, string parameter2Value, string entityName)
        {
            var searchKeyId = 0;

            var data = new SearchKeyDataModel();
            data.Name = DateTime.Now.ToLongTimeString();
            data.View = entityName;
            data.SortOrder = 1;
            data.Description = entityName;

            searchKeyId = SearchKeyDataManager.Create(data, SessionVariables.RequestProfile);            

            foreach (DataRow dr in ds.Tables[1].Rows)
            {     
                var columnName = dr[SearchKeyDetailDataModel.DataColumns.SearchParameter].ToString();
                

                var dataDetail = new SearchKeyDetailDataModel();
                dataDetail.SearchKeyId = searchKeyId;

                dataDetail.SearchParameter = columnName;
                dataDetail.SortOrder = 1;
                var detailId = SearchKeyDetailDataManager.Create(dataDetail, SessionVariables.RequestProfile);

                var dataDetailItem = new SearchKeyDetailItemDataModel();
                dataDetailItem.SearchKeyDetailId = detailId;
                dataDetailItem.SortOrder = 1;

                if (columnName == parameter1)
                {
                    dataDetailItem.Value = parameter1Value;
                }
                else if (columnName == parameter2)
                {
                    dataDetailItem.Value = parameter2Value;
                }
                else
                {
                    var columnValue = " ";
                    var searchKeyDetailId = dr[SearchKeyDetailDataModel.DataColumns.SearchKeyDetailId].ToString();
                    var rows = ds.Tables[2].Select(SearchKeyDetailDataModel.DataColumns.SearchKeyDetailId + "=" + searchKeyDetailId);
                    if (rows.Length > 0)
                    {
                        columnValue = rows[0][SearchKeyDetailItemDataModel.DataColumns.Value].ToString();
                    }

                    dataDetailItem.Value = columnValue;
                }
                
                SearchKeyDetailItemDataManager.Create(dataDetailItem, SessionVariables.RequestProfile);

            }

            return searchKeyId;
        }

    }
}