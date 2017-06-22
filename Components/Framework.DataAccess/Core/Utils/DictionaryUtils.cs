using System.Collections.Generic;

namespace Framework.CommonServices.BusinessDomain.Utils
{
    public static class DictionaryUtils
    {
        /// <summary>
        /// Copy the values of a dictionary into a new array.
        /// </summary>
        public static V[] CopyValuesToArray<K,V>(IDictionary<K,V> dict)
        {
            V[] items = new V[dict.Count];
            dict.Values.CopyTo(items, 0);
            return items;
        }
    }
}
