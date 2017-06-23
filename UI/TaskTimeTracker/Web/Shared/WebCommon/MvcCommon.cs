using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shared.WebCommon.UI.Web
{
    public class MvcCommon
    {
        public static List<SelectListItem> GetListItems(DataTable dtList, string idColumn, string nameColumn, int? idColumnValue)
        {
            var listItems = new List<SelectListItem>();

			if (idColumnValue != null)
            {
				foreach (DataRow dr in dtList.Rows)
				{
                    listItems.Add(new SelectListItem()
                    {
                        Value = Convert.ToString(dr[idColumn]),
                        Text = Convert.ToString(dr[nameColumn]),
                        Selected = (Convert.ToInt32(dr[idColumn]) == idColumnValue.Value)
                    });
                }
			}
			else
			{
				foreach (DataRow dr in dtList.Rows)
				{                               
                    listItems.Add(new SelectListItem()
                    {
                        Value = Convert.ToString(dr[idColumn]),
                        Text = Convert.ToString(dr[nameColumn])
                    });
                }
            }

            return listItems;
        }

        public static List<SelectListItem> GetListItems(IEnumerable lst, string idColumn, string nameColumn, int? idColumnValue)
        {
            var listItems = new List<SelectListItem>();

            if (idColumnValue != null)
            {
                //foreach (var item in lst.Cast<object>().ToList())
                //{
                    //listItems.Add(new SelectListItem()
                    //{
                    //    Value = Convert.ToString(item[idColumn]),
                    //    Text = Convert.ToString(item[nameColumn]),
                    //    Selected = (Convert.ToInt32(item[idColumn]) == idColumnValue.Value)
                    //});
                //}
            }
            else
            {
                //foreach (DataRow dr in dtList.Rows)
                //{
                //    listItems.Add(new SelectListItem()
                //    {
                //        Value = Convert.ToString(dr[idColumn]),
                //        Text = Convert.ToString(dr[nameColumn])
                //    });
                //}
            }

            return listItems;
        }
    }
}