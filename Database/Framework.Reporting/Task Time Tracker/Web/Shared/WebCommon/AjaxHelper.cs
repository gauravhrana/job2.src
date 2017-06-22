using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Shared.WebCommon.UI.Web
{
	public class AjaxHelper
	{
		private const string URL = "http://localhost:53331/API/AutoComplete.asmx/";
        //private const string URL = "http://localhost:53331/API/AutoComplete/";

		public static string GetCascadeKendoComboBoxConfigScript(string methodName, string dataTextField, string dataValueField, PlaceHolder plcControlHolder, string cascadeParent, bool addAllItem = true)
		{
			const string stringA = @"
			$(document).ready(function ()
			{{
				var local_url = '{0}';
				var local_element = '#{1}';
				var local_dataTextField = '{2}';
				var local_dataValueField = '{3}';
				var local_addElement = {4};
				var cascadeParent = '{5}';
				libary_kendo_cascade_getData(local_url, local_element, local_dataTextField, local_dataValueField, local_addElement, cascadeParent);

			}});
			";

			var a = string.Format(stringA
				, ( URL + methodName)
				, plcControlHolder.FindControl("txtBox").ClientID
				, dataTextField
				, dataValueField
				, addAllItem.ToString().ToLower()
				, cascadeParent);

			return a;
		}

        public static string GetCascadeKendoComboBoxConfigScript(string methodName, string dataTextField, string dataValueField, string controlClientId, string cascadeParent, bool addAllItem = true)
        {
            const string stringA = @"
			$(document).ready(function ()
			{{
				var local_url = '{0}';
				var local_element = '#{1}';
				var local_dataTextField = '{2}';
				var local_dataValueField = '{3}';
				var local_addElement = {4};
				var cascadeParent = '{5}';
				libary_kendo_cascade_getData(local_url, local_element, local_dataTextField, local_dataValueField, local_addElement, cascadeParent);

			}});
			";

            var a = string.Format(stringA
                , (URL + methodName)
                , controlClientId
                , dataTextField
                , dataValueField
                , addAllItem.ToString().ToLower()
                , cascadeParent);

            return a;
        }

		public static string GetKendoComboBoxConfigScript(string methodName, string dataTextField, string dataValueField, PlaceHolder plcControlHolder, bool addAllItem = true)
		{
			const string stringA = @"
			$(document).ready(function ()
			{{
				var local_url = '{0}';
				var local_element = '#{1}';
				var local_dataTextField = '{2}';
				var local_dataValueField = '{3}';
				var local_addElement = {4};
				libary_kendo_getData(local_url, local_element, local_dataTextField, local_dataValueField, local_addElement);

			}});
			";

			var a = string.Format(stringA
				, (URL + methodName)
				, plcControlHolder.FindControl("txtBox").ClientID
				, dataTextField
				, dataValueField
				, addAllItem.ToString().ToLower());

			return a;
		}

		public static string GetKendoComboBoxConfigScript(string methodName, string dataTextField, string dataValueField, HtmlGenericControl plcControlHolder, bool addAllItem = true)
		{
			const string stringA = @"
			$(document).ready(function ()
			{{
				var local_url = '{0}';
				var local_element = '#{1}';
				var local_dataTextField = '{2}';
				var local_dataValueField = '{3}';
				var local_addElement = {4};
				libary_kendo_getData(local_url, local_element, local_dataTextField, local_dataValueField, local_addElement);

			}});
			";

			var a = string.Format(stringA
				, (URL + methodName)
				, plcControlHolder.FindControl("txtBox").ClientID
				, dataTextField
				, dataValueField
				, addAllItem.ToString().ToLower());

			return a;
		}

        public static string GetKendoComboBoxConfigScript(string methodName, string dataTextField, string dataValueField, string controlClientId, bool addAllItem = true)
        {
            const string stringA = @"
			$(document).ready(function ()
			{{
				var local_url = '{0}';
				var local_element = '#{1}';
				var local_dataTextField = '{2}';
				var local_dataValueField = '{3}';
				var local_addElement = {4};
				libary_kendo_getData(local_url, local_element, local_dataTextField, local_dataValueField, local_addElement);

			}});
			";

            var a = string.Format(stringA
                , (URL + methodName)
                , controlClientId
                , dataTextField
                , dataValueField
                , addAllItem.ToString().ToLower());

            return a;
        }

		public static string GetKendoComboBoxConfigScriptForGroupBy(string methodName, string txtBoxClientId, string entityName, string fcModeId)
		{
			const string stringA = @"
			$(document).ready(function ()
			{{
				var local_url = '{0}';
				var local_element = '#{1}';
				var primary_entity = '{2}';
				var fcmode_id = '{3}';

				libary_kendo_getData_groupby(local_url, local_element, primary_entity, fcmode_id);

			}});
			";

			var a = string.Format(stringA
				, (URL + methodName)
				, txtBoxClientId
				, entityName
				, fcModeId);

			return a;
		}

	}
}