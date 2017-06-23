using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Shared.WebCommon.UI.Web
{
    public static class Extensions
    {
        
        public static string GetFirstThreeCharacters(this String str)
        {
            if (str.Length < 3)
            {
                return str;
            }
            else
            {
                return str.Substring(0, 3);
            }
        }

        public static IEnumerable<T> FindControlsOfType<T>(this Control parent)
                                                      where T : Control
        {
            foreach (Control child in parent.Controls)
            {
                if (child is T)
                {
                    yield return (T)child;
                }
                else if (child.Controls.Count > 0)
                {
                    foreach (T grandChild in child.FindControlsOfType<T>())
                    {
                        yield return grandChild;
                    }
                }
            }
        }
    }
}