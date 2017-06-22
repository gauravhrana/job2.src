using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shared.WebCommon.UI.Web
{
    public class UserActivityItem
    {
        public UserActivityItem()
        {
            ShowImage = true;
            ImagePath = "";
            ShowTimeGroup = false;
        }

        public string ActivityText;
        public bool ShowImage;
        public string ImagePath;
        public string TimeGrouping;
        public bool ShowTimeGroup;
    }
}