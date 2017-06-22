using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using System.Collections.Specialized;
using System.Configuration;
using System.Web.UI;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.Configuration;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Framework.Components.ApplicationUser;
using Framework.Components.DataAccess;
using Framework.Components.UserPreference;
using Shared.UI.Web;
using Framework.Components.Core;
using System.Web.UI.WebControls;
using Shared.UI.Web.Controls;
using System.Net.Mail;
using System.Reflection;
using System.IO;
using ListControl = Shared.UI.Web.Controls.ListControl;
using Shared.UI.Web.Admin;
using System.Dynamic;
using Dapper;
using Framework.CommonServices.BusinessDomain.Utils;

namespace Shared.WebCommon.UI.Web
{
	#region Named Constants

	public enum TabOrientation
	{
		Horizontal
	  , Vertical
	}

	public enum ControlType
	{
		DetailsControl
	,
		GenericControl
			, ImageHandler
	}

	#endregion
	public partial class ApplicationCommon
	{		
		#region Constructor

		static ApplicationCommon()
		{
			SetErrorEmailTemplate();
		}

		#endregion	

		#region SystemEntities

		public static string[] SystemEntities =
		{
				"AuditAction"                               
			,	"AuditHistory"                              
			,	"ApplicationRole"                           
			,	"Project"                                   
			,	"Question"                                  
			,	"Schedule"                                  
			,	"ScheduleItem"                              
			,	"ScheduleQuestion"                          
			,	"Task"                                      
			,	"TaskFormulation"                           
			,	"UserPreferenceDataType"                    
			,	"UserPreference"                            
			,	"Layer"                                     
			,	"Person"                                    
			,	"UserPreferenceKey"                         
			,	"SystemEntityType"                          
			,	"BatchFile"                                 
			,	"FileType"                                  
			,	"BatchFileStatus"                           
			,	"Feature"                                   
			,	"Need"                                      
			,	"BatchFileSet"                              
			,	"BatchFileHistory"                          
			,	"TaskEntityType"                            
			,	"TaskScheduleType"                          
			,	"TaskEntity = 2600"                         
			,	"TaskSchedule"                              
			,	"TaskRun"                                   
			,	"Milestone"                                 
			,	"Client"                                    
			,	"ApplicationMonitoredEventSource"           
			,	"ApplicationMonitoredEventProcessingState"  
			,	"ApplicationMonitoredEventEmail"            
			,	"ApplicationMonitoredEvent"                 
			,	"NeedsXFeature"                             
			,	"Activity"                                  
			,	"TaskType"                                  
			,	"TaskPriorityType"                          
			,	"TaskPriorityXApplicationUser"              
			,	"ReleaseLog"                                
			,	"ReleaseLogDetails"                         
			,	"Application"                               
			,	"Risk"                                      
			,	"Reward"                                      
			,	"ProjectTimeLine"                             
			,	"TaskRiskRewardRankingXPerson"                
			,	"PersonTitle"                                 
			,	"ProjectXNeeds"								
			,	"ApplicationUser"							
			,	"SystemEntityCategory"						
			,	"TypeOfIssue"                               
			,	"TimeZone"                                  
			,	"Country"
		};

		private readonly PreferenceUtility _perferenceCommon = new PreferenceUtility();

		#endregion

	}
}