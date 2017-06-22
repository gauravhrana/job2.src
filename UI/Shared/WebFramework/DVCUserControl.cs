using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Caching;
using System.Configuration;
using System.Web;

namespace Shared.UI.WebFramework
{
	/// <summary>
	/// Summary description for DVCUserControl.
	/// </summary>
	public class DVCUserControl : BaseControl
	{
		// This abstracts access to information about the current user that is actually held in session state
		// A real application would most probably hold this user information in a database

		//private UserInfo currentUser;

		// This is the key used to access the dynamic validation manager from the application cache

		//private const string DYNAMIC_VALIDATION_KEY = "DynamicValidation";

		// This is the dynamic validation manager
		private DynamicValidationManager dynamicValidationManager;

		protected virtual string ValidationConfigFile
		{
			get
			{
				return String.Empty; //ConfigurationSettings.AppSettings["DynamicValidatorsConfigurationFile"];
			}
		}

		// We implement a property with a GET accessor to hide the detail of getting the manager from the cache
		public DynamicValidationManager DynamicValidationManager
		{
			get
			{
				// Try and get the manager from the applciation cache

				//dynamicValidationManager = (DynamicValidationManager) Cache[DYNAMIC_VALIDATION_KEY];

				// Id there was no such item in the cache
				if (dynamicValidationManager == null)
				{
					// Retrieve the location to the configuration file
					var validatorsConfigFile = ValidationConfigFile;

					// If we retrieved a location to the configuration file
					if (validatorsConfigFile != null)
					{
						// Create a new manager
						dynamicValidationManager = new DynamicValidationManager(validatorsConfigFile);

						// Create and insert a cache dependency on the configuration file

						//    CacheDependency cacheDependency = new CacheDependency( validatorsConfigFile, DateTime.Now );
						//    Cache.Insert( DYNAMIC_VALIDATION_KEY, dynamicValidationManager, cacheDependency );
					}
					else
					{
						throw new Exception("Unable to determine the location of the dynamic validation configuration file");
					}
				}
				// Return the (possibly newly created) manager
				return (dynamicValidationManager);
			}
		}

		/// <summary>
		/// The constructor
		/// </summary>
		public DVCUserControl()
		{
		}

		/// <summary>
		/// Used to perform any initialization steps required to create and set up this instance
		/// </summary>
		/// <param name="e">The event arguments</param>
		protected override void OnInit(EventArgs e)
		{
			// Load all dynamically created validators for this user control
			DynamicValidationManager.LoadDynamicValidators(this);
		}

		// This is used to get access to the current user's information
		//public UserInfo CurrentUser
		//{
		//    get
		//    {
		//        if (currentUser == null )
		//        {
		//            currentUser = new UserInfo( Session );
		//        }
		//        return ( currentUser );
		//    }
		//}

		// The remaining properties are used to implement a basic page flow control
		public string MessageHeader
		{
			get { return (string)Session["MessageHeader"]; }
			set { Session["MessageHeader"] = value; }
		}

		public string MessageBody
		{
			get { return (string)Session["MessageBody"]; }
			set { Session["MessageBody"] = value; }
		}

		public string ContinueMessage
		{
			get { return (string)Session["ContinueMessage"]; }
			set { Session["ContinueMessage"] = value; }
		}

		public string ContinueURL
		{
			get { return (string)Session["ContinueURL"]; }
			set { Session["ContinueURL"] = value; }
		}
	}
}
