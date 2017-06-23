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
            gropedItems = null;
        }

        public string ActivityText;
        public string TimeStampText;
        public bool ShowImage;
        public string ImagePath;
        public string TimeGrouping;
        public bool ShowTimeGroup;
        public List<UserActivityChildItem> gropedItems;
    }

    public class UserActivityChildItem
    {
        //public string ChildAction
        //{
        //    set;
        //    get;
        //}
        public string ChildText
        {
            set;
            get;
        }
    }

}