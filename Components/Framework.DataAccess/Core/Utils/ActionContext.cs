using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.CommonServices.BusinessDomain.Utils
{
    public class ActionContext : Dictionary<string, object>
    {
        public ActionContext() : base() { }

        public bool IfTrue(string key)
        {
            object savedValue = null;
            if (TryGetValue(key, out savedValue))
                return savedValue is bool && (bool)savedValue;

            return false; 
        }
    }
}
