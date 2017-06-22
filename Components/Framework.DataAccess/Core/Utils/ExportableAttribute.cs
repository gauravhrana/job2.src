using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.CommonServices.BusinessDomain.Utils
{
    public class ExportableAttribute:Attribute
    {
        private readonly int sortOrder;
        public int SortOrder
        {
            get { return sortOrder; }
        }

        public ExportableAttribute()
        {
            sortOrder = int.MaxValue;
        }

        public ExportableAttribute(int sortOrder)
        {
            this.sortOrder = sortOrder;
        }

        public static Type GetStaticType()
        {
            return new ExportableAttribute().GetType();
        }
        
    }
}
