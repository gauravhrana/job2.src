using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess.DomainModel;

namespace Framework.Components.UserPreference.DomainModel
{
    public class UserPreferenceCategory : StandardModel
    {
        public class DataColumns : StandardDataColumns
        {
            public const string UserPreferenceCategoryId = "UserPreferenceCategoryId";
        }

        public int? UserPreferenceCategoryId { get; set; }

        public string ToURLQuery()
        {
            return "Name = '" + Name + "'";
        }
    }
}
