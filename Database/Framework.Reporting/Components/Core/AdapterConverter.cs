using System;
using System.Web.UI.WebControls;

namespace Library.CommonServices.Utils
{
    public class AdapterConvertor
    {
        public static string GetString(object val)
        {
            if (IsValueInvalid(val))
                return String.Empty;
            return val.ToString();
        }

        public static DateTime GetDateTime(object val)
        {
            if (IsValueInvalid(val))
                return DateTime.MinValue;
            return Convert.ToDateTime(val);
        }

        public static double GetDouble(object val)
        {
            if (IsValueInvalid(val))
                return 0;
            return Convert.ToDouble(val);
        }


        public static bool GetBool(object val)
        {
            if (IsValueInvalid(val))
                return false;
            return Convert.ToBoolean(val);
        }

        public static decimal GetDecimalEx(object val, decimal rejectionValue)
        {
            if (IsValueInvalid(val))
                return rejectionValue;
            return Convert.ToDecimal(val);
        }

        public static decimal GetDecimal(object val)
        {
            return GetDecimalEx(val, 0);
        }

        public static int GetInt(object val)
        {
            if (IsValueInvalid(val))
                return 0;
            return Convert.ToInt32(val);
        }

        public static bool IsValueInvalid(object val)
        {
            return (val == null || (val is string && val.ToString() == "N.A.")
                    || (val is string && val.ToString().Trim() == "")
                    || val.Equals(DBNull.Value));
        }

        public static string GetDataGridTextBoxValue(object sender, DataGridCommandEventArgs e, string textboxName)
        {
            for (var i = 0; i < e.Item.Cells.Count; i++)
            {
                var t = (TextBox)e.Item.Cells[i].FindControl(textboxName);
                if (t != null) return t.Text;
            }
            return "";
        }

        public static string GetDataGridDropDownListValue(object sender, DataGridCommandEventArgs e, string dropDownName)
        {
            for (var i = 0; i < e.Item.Cells.Count; i++)
            {
                var d = (DropDownList)e.Item.Cells[i].FindControl(dropDownName);
                if (d != null) return d.SelectedValue; // make sure u dont call loaddata() on postback or this field will be empty
            }
            return "";
        }

        public static string GetDataGridDropDownListItemText(object sender, DataGridCommandEventArgs e, string dropDownName)
        {
            for (var i = 0; i < e.Item.Cells.Count; i++)
            {
                var d = (DropDownList)e.Item.Cells[i].FindControl(dropDownName);
                if (d != null) return d.SelectedItem.Text; // make sure u dont call loaddata() on postback or this field will be empty
            }
            return "";
        }
    }
}