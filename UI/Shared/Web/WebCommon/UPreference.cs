using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Shared.WebCommon.UI.Web
{
    public class UPreference
    {
        public int Id
        {
            set;
            get;
        }

        public string UserPreferenceCategory
        {
            get;
            set;
        }

        public string UserPreferenceKey
        {
            get;
            set;
        }

        public string value
        {
            get;
            set;
        }

        public string DataType
        {
            get;
            set;
        }

        public int ApplicationUserId
        {
            get;
            set;
        }

    }
}