using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shared.UI.Web;
using System.Configuration;
using DataModel.Framework.AuthenticationAndAuthorization;


namespace Shared.WebCommon.UI.Web
{
	public partial class ApplicationCommon
	{
		#region Properties

		private static string ErrorEMailTemplate { get; set; }

		//public static  string UniqueValue
		//{
		//	get
		//	{
		//		return GetUniqueValueAsNumber();
		//	}
		//}

		private static readonly Lazy<Dictionary<int, ApplicationDataModel>> applicationCache = new Lazy<Dictionary<int, ApplicationDataModel>>(ApplicationCommon.LoadApplicationDetails);

		public static Dictionary<int, ApplicationDataModel> ApplicationCache
		{
			get
			{
				return applicationCache.Value;
			}
		}

		public static string LogonUserIdentity
		{
			get
			{
				return (HttpContext.Current.Request.LogonUserIdentity != null) ? HttpContext.Current.Request.LogonUserIdentity.Name : string.Empty;
			}
		}


		private static Lazy<int> applicationInstanceId = new Lazy<int>(WebApplicationUser.GetApplicationInstanceId);

		public static int ApplicationInstanceId
		{
			get
			{
				return applicationInstanceId.Value;
			}
		}

		private static Lazy<int> languageId = new Lazy<int>();

		public static int LanguageId
		{
			get
			{
				return Convert.ToInt32(ConfigurationManager.AppSettings["LanguageId"]);
			}
		}

		public PreferenceUtility PerferenceCommon
		{
			get { return _perferenceCommon; }
		}
		
		#endregion	
		
	}
}