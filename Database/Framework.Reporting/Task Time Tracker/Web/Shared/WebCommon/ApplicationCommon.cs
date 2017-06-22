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

	public enum TabOrientation
	{
			Horizontal
		,   Vertical
	}

	public enum ControlType
	{
			DetailsControl
		,   GenericControl
		,	ImageHandler
	}


	public class ApplicationCommon
	{
		private static readonly Lazy<Dictionary<int, ApplicationDataModel>> applicationCache = new Lazy<Dictionary<int,ApplicationDataModel>>(ApplicationCommon.LoadApplicationDetails);

		public static Dictionary<int, ApplicationDataModel> ApplicationCache
		{
			get
			{
				return applicationCache.Value;
			}
		}

		#region Constructor

		static ApplicationCommon()
		{
			SetErrorEmailTemplate();
		}

		#endregion

		#region Const Variables

        public const int ReturnAuditInfo = 0;

        public const string XAxisKey                          = "XAxis";
        public const string YAxisKey                          = "YAxis";
        public const string ZAxisKey                          = "ZAxis";
        public const string AggeregateFunctionKey             = "AggeregateFunction";
        public const string ShowAggeregateKey                 = "ShowAggeregate";

		public const string DetailsBorderClassName            = "DetailControlBorder";
		public const string HistoryGridVisibilityKey          = "HistoryGridVisible";
		public const string GridDefaultClickActionKey         = "GridDefaultClickAction";
		public const string DefaultRowCountKey                = "DefaultRowCount";
		public const string GridDetailLinkVisibleKey          = "GridDetailLinkVisible";
		public const string GridDeleteLinkVisibleKey          = "GridDeleteLinkVisible";
		public const string SearchFilterGridLinesKey          = "SearchFilterGridLines";
		public const string HistoryAdvancedModeGroupingKey    = "AuditHistoryAdvancedModeGrouping";
		public const string HistoryAdvancedModeIntervalKey    = "AuditHistoryAdvancedModeInterval";
		public const string UserTimeZone                      = "UserTimeZone";
		public const string AutoSearchOn                      = "AutoSearchOn";
		public const string TabOrientation                    = "TabOrientation";
		public const string WildCardSearchPrefix              = "WildCardSearchPrefix";
		public const string WildCardSearchPostfix             = "WildCardSearchPostfix";
		public const string ParentMenuId                      = "ParentMenuId";
		public const string ActiveMenuId                      = "ActiveMenuId";
		public const string UpdateInfoStyle                   = "UpdateInfoStyle";
		public const string SortArrowStyle                    = "SortArrowStyle";
		public const string DateRangeStyle					  = "DateRangeStyle";
		public const string DateControlLayout				  = "DateControlLayout";
		public const string FieldConfigurationMode            = "FieldConfigurationMode";
		public const string FieldConfigurationModeCategoryKey = "FieldConfigurationModeCategory";
		public const string UserApplicationModeId             = "UserApplicationModeId";
		public const string UserDateFormat                    = "UserDateFormat";
        public const string UserTimeFormat                    = "UserTimeFormat";
		public const string GroupListBindActiveTabKey         = "GroupListBindActiveTab";

		public const string UserActiveClient                  = "UserActiveClient";
		public const string UserActiveProject                 = "UserActiveProject";
		public const string UserActiveNeed                    = "UserActiveNeed";
		public const string UserActiveTask                    = "UserActiveTask";

		public const string ControlVisible                    = "ControlVisible";

		public const string TabHeaderBackgroundColor          = "TabHeaderBackgroundColor";
		public const string SubMenuTopBackgroundColor		  = "SubMenuTopBackgroundColor";
		public const string SubMenuTopBorderColor			  = "SubMenuTopBorderColor";
		public const string SubMenuBackgroundColor			  = "SubMenuBackgroundColor";
		public const string SubMenuForegroundColor			  = "SubMenuForegroundColor";
		public const string SubMenuHoverColor				  = "SubMenuHoverColor";
		public const string SubMenuBorderColor				  = "SubMenuBorderColor";
		public const string SubMenuBorderStyle				  = "SubMenuBorderStyle";
		public const string SubMenuFontFamily				  = "SubMenuFontFamily";
		public const string SubMenuFontSize					  = "SubMenuFontSize";

		public const string ReleaseNotesRowStyleForeColor				= "ReleaseNotesRowStyleForeColor";
		public const string ReleaseNotesRowStyleBackColor				= "ReleaseNotesRowStyleBackColor";
		
		public const string ReleaseNotesAlternatingRowStyleBackColor	= "ReleaseNotesAlternatingRowStyleBackColor";
		public const string ReleaseNotesHeaderBackColor					= "ReleaseNotesHeaderBackColor";
		public const string ReleaseNotesHeaderForeColor					= "ReleaseNotesHeaderForeColor";
		public const string ReleaseNotesGridLines						= "ReleaseNotesGridLines";
		public const string ReleaseNotesRowStyleFontSize				= "ReleaseNotesRowStyleFontSize";
		public const string ReleaseNotesAlternatingRowStyleFontSize		= "ReleaseNotesAlternatingRowStyleFontSize";
		public const string ReleaseNotesRowStyleHeight					= "ReleaseNotesRowStyleHeight";
		public const string ReleaseNotesAlternatingRowStyleHeight		= "ReleaseNotesAlternatingRowStyleHeight";
		public const string ReleaseNotesHeaderStyleHeight				= "ReleaseNotesHeaderStyleHeight";
		public const string ReleaseNotesHeaderStyleFontSize				= "ReleaseNotesHeaderStyleFontSize";
		public const string ReleaseNotesStatisticUnknown				= "unknown";
		public const string ScheduleStatisticUnknown                    = "unknown";

		public const string AceEditorNoOfLines                          = "AceEditorNoOfLines";
		public const string ScheduleNewGridLines						= "ScheduleNewGridLines";
		public const string GridActionBarBackgroundColor	            = "GridActionBarBackgroundColor";
		public const string GridActionBarForegroundColor                = "GridActionBarForegroundColor";
		public const string GridActionBarFontFamily			            = "GridActionBarFontFamily";
		public const string GridActionBarFontSize			            = "GridActionBarFontSize";
		public const string GridDefaultCharacterCount		            = "GridDefaultCharacterCount";
		public const string DynamicGridCharacter			            = "...";

        public const string ListHeaderForeColor               = "ListHeaderForeColor";
		public const string MenuBackgroundColor				  = "MenuBackgroundColor";
		public const string MenuForegroundColor				  = "MenuForegroundColor";
		public const string MenuHoverColor					  = "MenuHoverColor";
		public const string MenuBorderColor					  = "MenuBorderColor";
		public const string MenuFontFamily					  = "MenuFontFamily";
		public const string MenuFontSize					  = "MenuFontSize";
		public const string MenuColoredCategoryColor		  = "MenuColoredCategoryColor";
		public const string ColoredMenuCategory				  = "ColoredMenuCategory";

		public const string SearchBorderColor				  = "SearchBorderColor";
		public const string SearchBackgroundColor			  = "SearchBackgroundColor";
		public const string SearchForegroundColor			  = "SearchForegroundColor";
		public const string SearchFontFamily				  = "SearchFontFamily";
		public const string SearchFontSize					  = "SearchFontSize";
		public const string SearchBorderStyle				  = "SearchBorderStyle";

		public const string AllTabExists                      = "AllTabExists";
		public const string AllTabSelected                    = "AllTabSelected";
		public const string DetailsButtonPanelVisible         = "DetailsButtonPanelVisible";
		public const string DetailsPagingEnabled              = "DetailPagingEnabled";
		public const string DetailsAEFLModeEnabled            = "DetailsAEFLModeEnabled";

		public const string DateRangeControlPath              = "~/Shared/Controls/DateRange.ascx";
		public const string ListControlPath                   = "~/Shared/Controls/List/List.ascx";
		public const string BucketControlPath                 = "~/Shared/Controls/Bucket.ascx";
		public const string BucketFor3ControlPath             = "~/Shared/Controls/BucketFor3.ascx";
		public const string ReadOnlyBucketControlPath         = "~/Shared/Controls/ReadOnlyBucket.ascx";
		public const string DetailTabControlPath              = "~/Shared/Controls/DetailTab.ascx";
		public const string DetailTab1ControlPath             = "~/Shared/Controls/DetailTab1.ascx";
		public const string VerticalTabChildControlPath       = "~/Shared/Controls/VerticalTabChildControl.ascx";
		public const string DetailsWithChildrenListControl    = "~/Shared/Controls/DetailsWithChildren.ascx";
		public const string ReleaseNoteStatisticControlPath   = "~/Shared/ApplicationManagement/ReleaseNote/Controls/ReleaseNoteStatistics.ascx";
		public const string ScheduleStatisticControlPath	  = "~/BM/Scheduling/Report/ScheduleView/Controls/ScheduleStatistics.ascx";
		public const string ScheduleDetailStatisticControlPath = "~/BM/Scheduling/ScheduleDetail/Controls/ScheduleDetailStatistics.ascx";
        public const string GroupListControl                  = "~/Shared/Controls/GroupList.ascx";
		public const string ImagesControl                     = "~/Shared/Controls/Images.ascx";
		public const string DynamicMenuCharacter              = " . . . ";

		public const string DetailsControlRelativePath        = "Controls/Details.ascx";
		public const string GenericControlRelativePath        = "Controls/Generic.ascx";

		public const string SearchFieldName                   = "Name";
		public const string UserMenuCategory                  = "UserMenuCategory";
		public const string UserActiveEntity                  = "UserActiveEntity";
		public const string VersionDate                       = "VersionDate";
		public const string Language						  = "Language";
		public const string DateRangeFormat					  = "DateRangeFormat";
		public const string FromDateRange					  = "FromDateRange";
		public const string ToDateRange					      = "ToDateRange";
		
		public const string MinDate                           = "01-01-2000";
		public const string MaxDate                           = "01-01-3000";
		public const string ApplicationDateFormat			  = "MM-dd-yyyy";

		public const string Computer						  = "Computer";
		public const string ConnectionKey					  = "ConnectionKey";
		public const string LogUser							  = "LogUser";
		public const string GroupBy							  = "GroupBy";

		public const string ParentMenuIdTestValue			  = "23";
		public const int FunctionalityPriority				  = 6;
		public const int FunctionalityStatus				  = 10006;	
		
		#endregion

		#region Properties
					
		private static string ErrorEMailTemplate { get; set; }

		
		//public static  string UniqueValue
		//{
		//	get
		//	{
		//		return GetUniqueValueAsNumber();
		//	}
		//}


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
		
		public PerferenceUtility PerferenceCommon
		{
			get { return _perferenceCommon; }
		}
		
		#endregion

		#region Child Classes

		#endregion

		#region public methods

		public static Dictionary<int, ApplicationDataModel> LoadApplicationDetails()
		{
			var cacheItems = new Dictionary<int, ApplicationDataModel>();
			var list = ApplicationDataManager.GetEntityDetails(ApplicationDataModel.Empty,SessionVariables.SystemRequestProfile, ReturnAuditInfo);
			
			for (var i = 0; i < list.Count; i++)
			{
				cacheItems.Add((int)list[i].ApplicationId, list[i]);		
			}

			return cacheItems;
		}

		
		public static string ApplicationUserName()
		{
			var dt = ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);
			var userName = "";
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				if ((int)(dt.Rows[i][ApplicationUserDataModel.DataColumns.ApplicationUserId]) == SessionVariables.RequestProfile.AuditId)
				{
					userName = dt.Rows[i][ApplicationUserDataModel.DataColumns.ApplicationUserName].ToString();
					break;
				}
			}
			return userName;
		}

		//public static void SendSessionInfo(int temp, string uniqueTraceId)
		//{
		//	var alert = string.Empty;
		//	if (temp == 1)
		//	{
		//		alert = "Error Alert";
		//	}
		//	else
		//	{
		//		alert = "Information Alert";
		//	}

		//	var strToEmail = ConfigurationManager.AppSettings["InformationEmail"];
		//	var nMail = new MailMessage(ConfigurationManager.AppSettings["fromEmail"], strToEmail);
		//	nMail.IsBodyHtml = true;
		//	nMail.Subject = InformationCommon.SubjectInfo(alert, uniqueTraceId, "Session Info",null, "General");
		//	//nMail.Subject = ApplicationCache[SessionVariables.RequestProfile.ApplicationId].Code + " - " + alert + " - "+ uniqueTraceId + " - Session Info - General";
		//	nMail.Body = ApplicationCommon.SessionObjects(temp, uniqueTraceId);
		//	var sClient = new SmtpClient();
		//	sClient.Send(nMail);
		//}

		//public static void SendApplicationInfo(int temp, string uniqueTraceId)
		//{
		//	var alert = string.Empty;
		//	if (temp == 1)
		//	{
		//		alert = "Error Alert";
		//	}
		//	else
		//	{
		//		alert = "Information Alert";
		//	}

		//	var strToEmail = ConfigurationManager.AppSettings["InformationEmail"];
		//	var nMail = new MailMessage(ConfigurationManager.AppSettings["fromEmail"], strToEmail);
		//	nMail.IsBodyHtml = true;
		//	nMail.Subject = InformationCommon.SubjectInfo(alert, uniqueTraceId, "Application Info", null, "General");
		//	//nMail.Subject = ApplicationCache[SessionVariables.RequestProfile.ApplicationId].Code + " - " + alert + " - " + uniqueTraceId + " - Application Info - General";
		//	nMail.Body = ApplicationCommon.ApplicationObjects(); 
		//	var sClient = new SmtpClient();
		//	sClient.Send(nMail);
		//}

		//public static void SendSQLTraceInfo(string uniqueTraceId)
		//{
		//	var alert = "Information Alert";
		//	var strToEmail = ConfigurationManager.AppSettings["InformationEmail"];
		//	var nMail = new MailMessage(ConfigurationManager.AppSettings["fromEmail"], strToEmail);
		//	nMail.IsBodyHtml = true;
		//	nMail.Subject = InformationCommon.SubjectInfo(alert, uniqueTraceId, "SQL Trace", null, "General");
		//	//nMail.Subject = ApplicationCache[SessionVariables.RequestProfile.ApplicationId].Code + " - " + uniqueTraceId + " - SQL Trace - General";
		//	nMail.Body = ApplicationCommon.HtmlSql(new Exception(HttpContext.Current.Application.ToString()));
		//	var sClient = new SmtpClient();
		//	sClient.Send(nMail);
		//}

		public static int GetSystemAuditId(string applicationName)
		{
			return Convert.ToInt32(ConfigurationManager.AppSettings[applicationName + ".SystemAuditId"]);
		}

		public static DetailTabControl GetNewDetailTabControl()
		{
			var page = HttpContext.Current.Handler as Page;
			return (DetailTabControl)page.LoadControl(DetailTabControlPath);
		}

		public static DetailTab1Control GetNewDetail1TabControl()
		{
			var page = HttpContext.Current.Handler as Page;
			return (DetailTab1Control)page.LoadControl(DetailTab1ControlPath);
		}

		public static VerticalTabChildControl GetNewVerticalTabChildControl()
		{
			var page = HttpContext.Current.Handler as Page;
			return (VerticalTabChildControl)page.LoadControl(VerticalTabChildControlPath);
		}

		public static BucketControl GetNewBucketControl()
		{
			var page = HttpContext.Current.Handler as Page;
			return (BucketControl)page.LoadControl(BucketControlPath);
		}

		public static ListControl GetNewListControl()
		{
			var page = HttpContext.Current.Handler as Page;
			return (ListControl)page.LoadControl(ListControlPath);
		}

		public static void SetErrorEmailTemplate()
		{
			var asm = Assembly.GetExecutingAssembly();
			var stream = asm.GetManifestResourceStream(asm.GetName().Name + ".Templates.GlobalErrorEmail.html");

			using (var reader = new StreamReader(stream))
			{
				ErrorEMailTemplate = reader.ReadToEnd();
			}

			ErrorEMailTemplate = ErrorEMailTemplate.Replace("##CellTitleBackGroundColor##", "#eeeeee");
			ErrorEMailTemplate = ErrorEMailTemplate.Replace("##CellTitleFontColor##", "#000099");
			ErrorEMailTemplate = ErrorEMailTemplate.Replace("##Font-Family##", "Verdana");
		}

		public static void SendLastErrorInEmail(string machineName)
		{
			try
			{
				var uniqueValue = InformationCommon.UniqueValue;
				var ctx = HttpContext.Current;

				var exception = ctx.Server.GetLastError();

				var htmlSql = HtmlSql(exception);

				var inteneralException  = (exception.InnerException != null) ? exception.InnerException.Message : string.Empty;
				var inteneralStackTrace = (exception.InnerException != null) ? exception.InnerException.StackTrace : string.Empty;

				var localUrl = ctx.Request.Url.LocalPath;
				var localErrorDesc = exception.Message;

				var mailTemplate = ErrorEMailTemplate;

				if (!string.IsNullOrEmpty(mailTemplate))
				{
					mailTemplate = mailTemplate.Replace("##Exception##", exception.Message);
					mailTemplate = mailTemplate.Replace("##UserIdentity##", LogonUserIdentity);
					mailTemplate = mailTemplate.Replace("##InternalException##", inteneralException);
					mailTemplate = mailTemplate.Replace("##AbsoluteUrl##", ctx.Request.Url.AbsoluteUri);
					mailTemplate = mailTemplate.Replace("##InternalStackTrace##", inteneralStackTrace.Replace(Environment.NewLine, "<br/>"));
					mailTemplate = mailTemplate.Replace("##UserHostAddress##", ctx.Request.UserHostAddress);
					mailTemplate = mailTemplate.Replace("##UserHostName##", ctx.Request.UserHostName);
					mailTemplate = mailTemplate.Replace("##ApplicationServer##", machineName);
					mailTemplate = mailTemplate.Replace("##UserAgent##", ctx.Request.UserAgent);
					mailTemplate = mailTemplate.Replace("##TargetSite##", exception.TargetSite.ToString());
					mailTemplate = mailTemplate.Replace("##ExceptionSource##", exception.Source);
				}

				var strToEmail = ConfigurationManager.AppSettings["ErrorReportEmail"];

				var nMail      = new MailMessage(ConfigurationManager.AppSettings["fromEmail"], strToEmail);

				nMail.IsBodyHtml = true;

				var sqlTraceStrToEmail = ConfigurationManager.AppSettings["InformationEmail"];
				var sqlTraceMail = new MailMessage(ConfigurationManager.AppSettings["fromEmail"], sqlTraceStrToEmail);

				// TODO: cache in dictionary id, Name intially
				var appId = SessionVariables.RequestProfile.ApplicationId;
				var appName = ApplicationCache[appId];
 
				nMail.Subject = InformationCommon.SubjectInfo("Error Alert", uniqueValue, "Standard Error - (URL: ", localUrl + ")", "(Error: " + localErrorDesc)+")";
				
				nMail.Body      = mailTemplate;

				var sClient      = new SmtpClient();

				sClient.Send(nMail);

				sqlTraceMail.IsBodyHtml = true;
				sqlTraceMail.Subject = InformationCommon.SubjectInfo("Error Alert", uniqueValue, "SQL Trace - (URL: ", localUrl + ")", "(Error: " + localErrorDesc)+")";
				
				sqlTraceMail.Body	 = htmlSql;

				var sqlTraceClient = new SmtpClient();

				sqlTraceClient.Send(sqlTraceMail);

				InformationCommon.SendSessionInfo(InformationCommon.EmailType.Error, uniqueValue);
				InformationCommon.SendApplicationInfo(InformationCommon.EmailType.Error, uniqueValue);
			}
			catch
			{
				// skip it
			}
		}

		public static string HtmlSql(Exception exception)
		{
			var sql = exception.Data["SQL"] as string;

			var oMessage = new StringBuilder(sql);

            
            oMessage.AppendLine("<table border=\"1\" width='100%' >");
            oMessage.AppendLine("<caption>SQL Stack Trace</<caption>");

			for (var i = 0; i <= Log4Net.SqlStatementStack.Count - 1; i++)
			{
				var item = (Log4Net.SqlStatementStackMessage) Log4Net.SqlStatementStack.Pop();

                oMessage.AppendLine("   <tr>");
                oMessage.AppendLine("       <td valign=\"top\"><code><pre>" + item.TimeStamp + "</pre></code></td>");
				oMessage.AppendLine("       <td valign=\"top\"><code><pre>" + item.Message + "</pre></code></td>");
                oMessage.AppendLine("   </tr>");
			}
            oMessage.AppendLine("</table>");

			var htmlSql = string.Empty;

			if (oMessage.Length <= 0) return htmlSql;

            htmlSql = oMessage.ToString();

			return htmlSql;
		}

		//public static string GetUniqueValueAsNumber()
		//{
		//	return DateTime.Now.ToString("yyyyMMddhhmmss");
		//}

//		public static string ExpandSessionValue(string type, string key, string  userName, string logonUserIdentity, ApplicationDataModel appName, int temp, string uniqueValue)
//		{
//			var alert = string.Empty;
//			if (temp == 1)
//			{
//				alert = "Error Alert ";
//			}
//			else
//			{
//				alert = "Information Alert";
//			}
//			var expandedvalue = new StringBuilder("");
//			switch (type)
//			{
//				case "RequestProfile":
//					{
//						var reqProfile = (Framework.Components.DataAccess.RequestProfile)HttpContext.Current.Session[key];
//						expandedvalue.AppendFormat("ApplicationId:" + reqProfile.ApplicationId + "; ApplicationModeId:" + reqProfile.ApplicationModeId + "; AuditId:" + reqProfile.AuditId).ToString();
//						return expandedvalue.ToString();
//					}
//				case "DataTable":
//					{
//						var dt = (DataTable)HttpContext.Current.Session[key];
//						var columnName ="";

//						foreach (DataColumn dc in dt.Columns)
//						{
//							if (dc.Caption.Equals("Name") || dc.Caption.Equals("Menu"))
//							{
//								columnName = dc.Caption;
//								break;
//							}
//							else
//							{
//								columnName = dt.Columns[2].Caption;
//							}
//						}

//						var dv = new DataView(dt);
//						dv.Sort = string.Format("{0} ASC", columnName);

//						expandedvalue.Append("<table width=50 height=50 border=2 sort =ascending>");
//						var count = dt.Rows.Count;
//						expandedvalue.Append(count);
//						var colorCount=0;
//						var firstRow = "#ADD8E6";
//						var alternateRow = "white";

//						var strToEmail = ConfigurationManager.AppSettings["InformationEmail"];
//						var nMail = new MailMessage(ConfigurationManager.AppSettings["fromEmail"], strToEmail);
//						nMail.IsBodyHtml = true;
//						//nMail.Subject = InformationCommon.SubjectInfo(alert, uniqueValue, "Session Info - Table", key, "(RowCount: " + count + ")");
//						nMail.Subject = appName.Code + " - " + alert + "- "+ uniqueValue + " - Session Info - Table - " + key + " - (RowCount: " + count + ")";

//						expandedvalue.AppendFormat("<th> RowNumber </th> ");
//						foreach (DataColumn dc in dv.Table.Columns)
//						{
//							expandedvalue.AppendFormat("<th>{0}</th> ", dc.Caption);
//						}
//						expandedvalue.Append("</tr>");

//						foreach (DataRowView dr in dv)
//						{
//							var color= colorCount % 2 == 0 ? firstRow : alternateRow;
//							expandedvalue.AppendFormat("<tr style= background-color:{0}>", color);
//							var index = dv.Table.Rows.IndexOf(dr.Row);
//							expandedvalue.AppendFormat("<td>{0}</td> ", index + 1);

//							for (int i = 0; i < dr.Row.ItemArray.Length; i++)
//							{
//								if (!String.IsNullOrEmpty(dr.Row.ItemArray[i].ToString()))
//								{
//									expandedvalue.AppendFormat("<td>{0}</td>", dr.Row.ItemArray[i]);
//								}
//								else
//								{
//									expandedvalue.AppendFormat("<td>NULL</td>");
//								}

//							}

//							expandedvalue.Append("</tr>");
//							colorCount++;
//						}

//						expandedvalue.Append("</table>");

//						nMail.Body = key +"- RowCount :"+ expandedvalue;

//						var sClient = new SmtpClient();
//						sClient.Send(nMail);
//						return "";
//						//return expandedvalue.ToString();
//					}

//				case "List`1":
//					{
//						var list = SessionVariables.UserPreferences;
//						var elementProperty = typeof(UPreference).GetProperties().ToList();

//						list.Sort((x, y) => x.UserPreferenceCategory.CompareTo(y.UserPreferenceCategory));
//						var count = list.Count;
//						expandedvalue.Append(count);
//						expandedvalue.Append("<table width=50 height=50 border=1><tr>");
//						var colorCount = 0;
//						var firstRow = "#ADD8E6";
//						var alternateRow = "white";

//						var strToEmail = ConfigurationManager.AppSettings["InformationEmail"];
//						var nMail = new MailMessage(ConfigurationManager.AppSettings["fromEmail"], strToEmail);
//						nMail.IsBodyHtml = true;
//						nMail.Subject = appName.Code + " - " + alert +"- " + uniqueValue + " - Session Info - List - " + key + " - (RowCount: " + count + ")";

//						expandedvalue.AppendFormat("<th> RowNumber </th> ");
//						foreach(var headProp in elementProperty)
//						{
//							expandedvalue.AppendFormat("<th>{0}</th>", headProp.Name); 
//						}

//						expandedvalue.Append("</tr><br>");


//						for (var i = 0; i < list.Count; i++)
//						{

//							var color = colorCount % 2 == 0 ? firstRow : alternateRow;
//							expandedvalue.AppendFormat("<tr style=background-color:{0}>",color);
//							var index = i + 1;
//							expandedvalue.AppendFormat("<td>{0}</td> ", index);
//							foreach (var prop in elementProperty)
//							{
//								object value = prop.GetValue(list.ElementAt(i), null);

//								if(!String.IsNullOrEmpty(value.ToString()))
//								{
//									expandedvalue.AppendFormat("<td>{0}</td>",value);
//								}
//								else
//								{
//									expandedvalue.Append("<td>NULL</td>");
//								}

//							}

//							expandedvalue.Append("</tr>");
//							colorCount++;
//						}

//						nMail.Body = key + " - RowCount :" +  expandedvalue ;

//						var sClient = new SmtpClient();
//						sClient.Send(nMail);
//						return "";
//						//return expandedvalue.ToString();
//					}
//				default:
//					{
//						return HttpContext.Current.Session[key].ToString();
//					}
//			}
//		}

//		public static string SessionObjects(int temp, string uniqueValue)
//		{

//			var key = new StringBuilder("");
//			var value = new StringBuilder("");
//			var sessionObject = new StringBuilder("");
//			var sessionObjectList = string.Empty;

//			var logonUserIdentity = (HttpContext.Current.Request.LogonUserIdentity != null) ? HttpContext.Current.Request.LogonUserIdentity.Name : string.Empty;
//			var dt = ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);
//			var userName ="";
//			for (int i = 0; i < dt.Rows.Count; i++)
//			{
//				if ((int)(dt.Rows[i][ApplicationUserDataModel.DataColumns.ApplicationUserId]) == SessionVariables.RequestProfile.AuditId)
//				{
//					userName = dt.Rows[i][ApplicationUserDataModel.DataColumns.ApplicationUserName].ToString();
//				}
//			}

//			var traceNode = "<b>UserName:</b> "+ userName+" -" + logonUserIdentity;
//			traceNode+= "<br><br><b>Session Object List:</b>";	
//			sessionObject.Append(traceNode);
//			var appId = SessionVariables.RequestProfile.ApplicationId;
//			var appName = ApplicationCommon.ApplicationCache[appId];
//			var list = new List<string>();

//			foreach (var sessionkey1 in HttpContext.Current.Session.Keys)
//			{
//				list.Add(sessionkey1.ToString());
//			}

//			list.Sort();
//			foreach (var sessionKey in list)
//			{
//				key.AppendFormat("{0}\n", sessionKey);
//				var type = HttpContext.Current.Session[sessionKey].GetType().Name;
//				var expandedvalue = ExpandSessionValue(type, sessionKey, userName, logonUserIdentity, appName, temp, uniqueValue);
//				value.AppendFormat("{0}\n", expandedvalue);

//				if (!String.IsNullOrEmpty(expandedvalue))
//				{
//					sessionObjectList = String.Format(
//						@"<table width='100%'>
//					<tbody>
//						<tr>
//							<td>
//								<code><pre><b>{0}</b>: {1}</pre></code>
//							</td>
//						</tr>
//					</tbody>
//
//				</table>", sessionKey, expandedvalue);
//					sessionObject.Append(sessionObjectList);
//				}

//			}

//			return sessionObject.ToString();
//		}

//		public static string ApplicationObjects()
//		{
//			var keys = new StringBuilder("");
//			var value = new StringBuilder("");

//			var applicationObjectList = string.Empty;
//			var applicationInstance  = HttpContext.Current.ApplicationInstance;

//			foreach(var  key in  applicationInstance.Application.Keys) 
//			{
//				keys.AppendFormat("{0}\n",key);
//				value.AppendFormat("{0}\n", applicationInstance.Application[key.ToString()]);
//			}
//			var traceNode = "<b>Application Object List:</b>";

//			applicationObjectList = traceNode + String.Format(
//				@"<table width='100%' >
//					<tbody>
//					<tr>
//						<th>
//							Keys
//						</th>
//						<th>
//							Value
//						</th>
//					</tr>
//						<tr>
//							<td>
//								<code><pre>{0}</pre></code>
//							</td>
//							<td>
//								<code><pre>{1}</pre></code>
//							</td>
//						</tr>
//					</tbody>
//				</table><br>", keys, value);


//			return applicationObjectList;
//		}

		public static List<int> GetSuperKeyDetails(int systemEntityTypeId, string superKeyId)
		{
			var lst = new List<int>();

			var data = new SuperKeyDetailDataModel();
			data.SuperKeyId = Convert.ToInt32(superKeyId);

			data.SystemEntityTypeId = systemEntityTypeId;

			var dt = SuperKeyDetailDataManager.Search(data, SessionVariables.RequestProfile);

			if (dt != null && dt.Rows.Count > 0)
			{
				foreach (DataRow dr in dt.Rows)
				{
					lst.Add(Convert.ToInt32(dr[SuperKeyDetailDataModel.DataColumns.EntityKey]));
				}
			}

			return lst;
		}

		public static string GetControlPath(string entityNameKey, ControlType controlType)
		{
			var strPath = String.Empty;

			var data = new ApplicationRouteDataModel();
			data.EntityName = entityNameKey;

			var dt = ApplicationRouteDataManager.Search(data, SessionVariables.RequestProfile);

			if(dt != null && dt.Rows.Count > 0)
			{
				strPath = dt.Rows[0][ApplicationRouteDataModel.DataColumns.RelativeRoute].ToString();
				if(!string.IsNullOrEmpty(strPath))
				{
					strPath = strPath.Replace("{action}.aspx", String.Empty);
				}
			}

			if (controlType == ControlType.DetailsControl)
			{
				strPath += DetailsControlRelativePath;
			}
			else if(controlType == ControlType.GenericControl)
			{
				strPath += GenericControlRelativePath;
			}

			return strPath;
		}

		public static int? GetSystemEntityTypeId(string systemEntityType)
		{
			int? systemEntityTypeId = null;
			var data = new SystemEntityTypeDataModel();

			data.EntityName = systemEntityType;
			var dt = SystemEntityTypeDataManager.Search(data, SessionVariables.RequestProfile);

			if(dt.Rows.Count> 0)
			{
				systemEntityTypeId = Convert.ToInt32(dt.Rows[0][SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId]);
			}

			return systemEntityTypeId;
		}

		public static int? GetHelpPageContextId(string helpPageContext)
		{
			int? helpPageContextId = null;

			var data = new HelpPageContextDataModel();
			data.Name = helpPageContext;

			var dt = HelpPageContextDataManager.Search(data, SessionVariables.RequestProfile);

			if (dt.Rows.Count > 0)
			{
				helpPageContextId = Convert.ToInt32(dt.Rows[0][HelpPageContextDataModel.DataColumns.HelpPageContextId]);
			}

			return helpPageContextId;
		}

		public static Control GetTabControl(string primeEntity, int setId, Control detailsControl, string userPreferenceCategory)
		{
			var resultControl = detailsControl;

			var data = new TabParentStructureDataModel();
			data.Name = primeEntity;

			var dt = TabParentStructureDataManager.Search(data, SessionVariables.RequestProfile);

			if (dt != null && dt.Rows.Count > 0)
			{
				var tabParentId = Convert.ToInt32(dt.Rows[0][TabParentStructureDataModel.DataColumns.TabParentStructureId]);

				var dataChild = new TabChildStructureDataModel();
				dataChild.TabParentStructureId = tabParentId;
				var dtChild = TabChildStructureDatManager.Search(dataChild, SessionVariables.RequestProfile);

				if (dtChild != null && dtChild.Rows.Count > 0)
				{					
					//var page = HttpContext.Current.Handler as Page;

					var isAllTab = Convert.ToInt32(dt.Rows[0][TabParentStructureDataModel.DataColumns.IsAllTab]);
					var firstHeader = Convert.ToString(dt.Rows[0][StandardDataModel.StandardDataColumns.Name]);
					var boolAllTab = false;
					var tabCount = 1;

					//if (isAllTab > 0)
					//{
					//	boolAllTab = true;
					//}

					var tabControl = GetNewDetailTabControl();

					tabControl.Setup(userPreferenceCategory);
					tabControl.AddTab(firstHeader, detailsControl, String.Empty, true);

					foreach (DataRow dr in dtChild.Rows)
					{
						tabCount++;
						var entityName = Convert.ToString(dr[TabChildStructureDataModel.DataColumns.EntityName]);
						tabControl.AddTab(entityName, null);
					}

					resultControl = tabControl;
				}
			}

			//tabControl.AddTab("Project", detailsControl();
			//tabControl.AddTab("Client", "", 2, "ClientId", setId, true, GetClientData, GetClientColumns);
			//tabControl.AddTab("Need", "", 3, "NeedId", setId, true, GetNeedData, GetNeedColumns);

			return resultControl;
		}

		public static void CheckDayCareApplicationReady()
		{
			var lstEntities = new List<string>()
				{
						"Application"
					,   "ApplicationUserTitle"
					,   "ApplicationUser"
					,   "ApplicationRole"
					,   "ApplicationOperation"
					,   "SystemEntityType"
					,   "SystemEntityCategory"
					,   "TimeZone"
					,   "Country"
					,   "ApplicationEntityFieldLabel"
					,   "ApplicationEntityParentalHierarchy"
					,   "ApplicationEntityFieldLabelMode"
					,   "ApplicationEntityFieldLabelModeCategory"
					,   "Menu"
				};

			foreach (var strEntityName in lstEntities)
			{
				PerferenceUtility.CreateUserPreferenceCategoryIfNotExists(strEntityName, strEntityName);
			}
		}

		public static string GetSuperKey()
		{
			var currentPage = HttpContext.Current.CurrentHandler as Page;

			var sKey = String.Empty;

			if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["SuperKey"]) || currentPage.RouteData.Values["SuperKey"] != null)
			{
				if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["SuperKey"]))
				{
					sKey = HttpContext.Current.Request.QueryString["SuperKey"];
				}
				else if (currentPage.RouteData.Values.ContainsKey("SuperKey"))
				{
					try
					{
						sKey = Convert.ToString(currentPage.RouteData.Values["SuperKey"]);
					}
					catch { }
				}
			}

			return sKey;
		}

		public static int GetSetId()
		{
			var currentPage = HttpContext.Current.CurrentHandler as Page;

			var sId = 0;

			if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["SetId"]))
			{
				sId = Convert.ToInt32(HttpContext.Current.Request.QueryString["SetId"]);
			}
			else
			{
				object value;

				if(currentPage.RouteData.Values.TryGetValue("SetId", out value))
				{
					try
					{
						sId = Convert.ToInt32(value);
					}
					catch { }
				}
			}

			return sId;
		}

		public static int GenerateSuperKey(StringCollection sc, int systemEntityTypeId)
		{
			var data = new SuperKeyDataModel();

			data.SortOrder			= 1;
			data.SystemEntityTypeId = systemEntityTypeId;
			data.ExpirationDate		= DateTime.Now.AddDays(30);
			data.Description		= systemEntityTypeId + " : " + data.ExpirationDate;
			data.Name				= systemEntityTypeId + " : " + " : " + DateTime.Now.ToLongTimeString();

			var superKeyId = SuperKeyDataManager.Create(data, SessionVariables.RequestProfile);

			foreach (var _str in sc)
			{
				var detailData = new SuperKeyDetailDataModel();
				detailData.SuperKeyId = superKeyId;
				detailData.EntityKey = Convert.ToInt32(_str);
				SuperKeyDetailDataManager.Create(detailData, SessionVariables.RequestProfile);
			}

			return superKeyId;
		}

		public static string GetApplicationUserName()
		{
			if (string.IsNullOrEmpty(SessionVariables.ApplicationUserFullName))
			{
				SetApplicationUserFullName();
			}

			return SessionVariables.ApplicationUserFullName;
		}

		public static void SetApplicationUserFullName()
		{
			var oData = new ApplicationUserDataModel();

			oData.ApplicationUserId = SessionVariables.RequestProfile.AuditId;

			SessionVariables.ApplicationUserFullName = ApplicationUserDataManager.GetFullName(oData, SessionVariables.RequestProfile);
		}

        public static void ResetApplicationCache(string applicationCode)
        {
            var applicationId = int.Parse(ConfigurationManager.AppSettings[applicationCode + ".ApplicationId"]);

            var isStartupChecked = SessionVariables.StartupChecked;

            HttpContext.Current.Session.Clear();

            SessionVariables.CurrentApplicationCode = applicationCode;
            SessionVariables.CurrentApplicationModuleCode = applicationCode;

            SessionVariables.SystemRequestProfile = new RequestProfile(ApplicationCommon.GetSystemAuditId(applicationCode),
                                                            SessionVariables.ApplicationMode, applicationId);

            SessionVariables.RequestProfile = new RequestProfile(WebApplicationUser.GetCurrentUserId(applicationId), SessionVariables.ApplicationMode, applicationId);

            SessionVariables.UserAuthorized = WebApplicationUser.CheckIfUserIsValid(SessionVariables.RequestProfile.AuditId);

            SessionVariables.StartupChecked = isStartupChecked;

            ApplicationCommon.SetApplicationUserRoles();

            PerferenceUtility.RefreshUserPreferencesCache();
            PerferenceUtility.RefreshApplicationInstancePreferences();
            PerferenceUtility.RefreshApplicationUserPreferencesCache();
            PerferenceUtility.RefreshUserPreferenceCategoriesCache();

            FieldConfigurationUtility.SetUserFieldConfigurationModes();
            FieldConfigurationUtility.SetFieldConfigurations();
        }

        public static void ResetApplicationModuleCache(string applicationModule)
        {
            var isStartupChecked = SessionVariables.StartupChecked;

            SessionVariables.CurrentApplicationModuleCode = applicationModule;

            SessionVariables.UserPreferedMenuData = null;
        }

		public static void SetApplicationUserRoles()
		{
			var lst = new List<ApplicationUserRole>();

			var dt = ApplicationUserXApplicationRoleDataManager.GetByApplicationUser(SessionVariables.RequestProfile.AuditId, SessionVariables.RequestProfile);
			if (dt != null && dt.Rows.Count > 0)
			{
				foreach (DataRow dr in dt.Rows)
				{
					var role = new ApplicationUserRole();

					role.Id					= Convert.ToInt32(dr[ApplicationUserXApplicationRoleDataModel.DataColumns.ApplicationUserXApplicationRoleId]);
					role.ApplicationRole    = Convert.ToString(dr[ApplicationUserXApplicationRoleDataModel.DataColumns.ApplicationRole]);
					role.ApplicationRoleId  = Convert.ToInt32(dr[ApplicationUserXApplicationRoleDataModel.DataColumns.ApplicationRoleId]);
					role.ApplicationUserId  = Convert.ToInt32(dr[ApplicationUserXApplicationRoleDataModel.DataColumns.ApplicationUserId]);

					lst.Add(role);
				}
			}

			SessionVariables.ApplicationUserRoles = lst;
		}

		public static string GetSearchColumnControlType(int systementityId, string columnname, int auditId)
		{
			if (columnname.Equals("GroupBy") || columnname.EndsWith("GroupBy"))
				return "DropDownList";

			var colsdata = new FieldConfigurationDataModel();
			colsdata.FieldConfigurationModeId = SessionVariables.SearchControlColumnsModeId;
			colsdata.SystemEntityTypeId = systementityId;

			var cols = FieldConfigurationDataManager.Search(colsdata, SessionVariables.RequestProfile);

			for(var i = 0; i < cols.Rows.Count; i++)
			{
				if(cols.Rows[i][FieldConfigurationDataModel.DataColumns.Name].Equals(columnname))
				{
					return cols.Rows[i][FieldConfigurationDataModel.DataColumns.ControlType].ToString();
				}
			}

			return String.Empty;
		}

		public static DataTable GetSearchColumns(int systementityId, int auditId)
		{
			var colsData = new FieldConfigurationDataModel();
			colsData.FieldConfigurationModeId = SessionVariables.SearchControlColumnsModeId;
			colsData.SystemEntityTypeId = systementityId;

			var cols = FieldConfigurationDataManager.Search(colsData, SessionVariables.RequestProfile);

			return cols;
		}

		public static object  ExtractFieldValuefromRepeater(Repeater searchParameterRepeater, string field, bool ddlreturnvalue = true)
		{
			for (var i = 0; i < searchParameterRepeater.Items.Count; i++)
			{
				var label = (Label)searchParameterRepeater.Items[i].FindControl("label");
				var hdnfield = (HiddenField)searchParameterRepeater.Items[i].FindControl("hdnfield");

				if (hdnfield != null && searchParameterRepeater.Items[i].Visible)
				{
					if (hdnfield.Value.Equals(field))
					{
						var txtbox = (TextBox)searchParameterRepeater.Items[i].FindControl("txtbox");
						var dropdownlist = (DropDownList)searchParameterRepeater.Items[i].FindControl("dropdownlist");
						var listbox = (ListBox)searchParameterRepeater.Items[i].FindControl("listbox");

						//var datepanel = (Panel)searchParameterRepeater.Items[i].FindControl("datepanel");
						//var txtboxdate1 = (TextBox)searchParameterRepeater.Items[i].FindControl("txtboxdate1");
						//var txtboxdate2 = (TextBox)searchParameterRepeater.Items[i].FindControl("txtboxdate2");
						//var chkDate = (CheckBox)searchParameterRepeater.Items[i].FindControl("chkDate");

						var oDateRange = (DateRangeControl)searchParameterRepeater.Items[i].FindControl("oDateRange");

						if (txtbox != null && txtbox.Visible)
						{
							return txtbox.Text;
						}
						else if (dropdownlist != null && dropdownlist.Visible)
						{
							if (ddlreturnvalue)
								return dropdownlist.SelectedValue;
							else
								return dropdownlist.SelectedItem.Text;
						}
						else if (listbox != null && listbox.Visible)
						{

							var values = "";
							if (ddlreturnvalue)
							{
								var indices = listbox.GetSelectedIndices();

								for (var j = 0; j < indices.Length; j++)
								{
									values += listbox.Items[indices[j]].Value;
									if (j != indices.Length - 1)
										values += "/";
								}
								return values;
							}
							else
							{
								var indices = listbox.GetSelectedIndices();
								for (var j = 0; j < indices.Length; j++)
								{
									values += listbox.Items[indices[j]].Text;
									if (j != indices.Length - 1)
										values += "/";
								}
								return values;
							}
						}
						else
						{
							//return txtboxdate1.Text + "&" + txtboxdate2.Text + "&" + chkDate.Checked;
							if (oDateRange != null)
							{
								return oDateRange.FromDateTime + "&" + oDateRange.ToDateTime + "&" + oDateRange.Checked;
							}

						}
					}
				}
			}

			return string.Empty;
		}

		public static string  CheckAndGetFieldValue(Repeater searchParameterRepeater, int systementitytypeId, string columnname, string settingcategory, int auditId, bool ddlreturnvnvalue = true)
		{
			//Get column value based on field
			var columnvalue = ExtractFieldValuefromRepeater(searchParameterRepeater, columnname, ddlreturnvnvalue).ToString();

			//Get SerachParameterRepeater rows count
			var searchParameterRepeaterRowsCount = searchParameterRepeater.Items.Count;

			//Get Search field visibility from UP settings
			var IsSearchFieldVisible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(columnname + "Visibility", settingcategory);

			//Get control type of column name
			var controltype = GetSearchColumnControlType(systementitytypeId, columnname, auditId);

			//Check if the field is visible
			if (searchParameterRepeaterRowsCount != 0 && IsSearchFieldVisible)
			{
				//Return column value if the control type is not DropDownList and value is not empty
				if ((!controltype.Equals("DropDownList") && !string.IsNullOrEmpty(columnvalue))) 
				{
					return columnvalue;
				}
				//Return column value(SelectedValue of DropDownList) if the control type is dropdownlist and value is != -1 OR 
				else if (ddlreturnvnvalue && (controltype.Equals("DropDownList") && !string.IsNullOrEmpty(columnvalue) && Convert.ToInt32(columnvalue) != -1))
				{
					return columnvalue;
				}
				//Return DropDownList Selected Text based on ddlreturnvalue parameter
				else if (!ddlreturnvnvalue && (controltype.Equals("DropDownList") && !string.IsNullOrEmpty(columnvalue)))
				{
					return columnvalue;
				}
			}

			return String.Empty;
		}

		public static void SetDefaultValues(Repeater searchParametersRepeater, string settingCategory)
		{
			for (var i = 0; i < searchParametersRepeater.Items.Count; i++)
			{
				var hdnfield = (HiddenField)searchParametersRepeater.Items[i].FindControl("hdnfield");
				var dropdownlist = (DropDownList)searchParametersRepeater.Items[i].FindControl("dropdownlist");
				var txtbox = (TextBox)searchParametersRepeater.Items[i].FindControl("txtbox");
				var chkbox = (LinkButton)searchParametersRepeater.Items[i].FindControl("chkbox");
				var txtbox1 = (TextBox)searchParametersRepeater.Items[i].FindControl("txtbox1");
				var listbox = (ListBox)searchParametersRepeater.Items[i].FindControl("listbox");
				var datepanel = (Panel)searchParametersRepeater.Items[i].FindControl("datepanel");
				
				if (hdnfield != null && txtbox != null && chkbox != null)
				{
					if (!searchParametersRepeater.Items[i].Visible)
					{
						searchParametersRepeater.Items[i].Visible = true;
						PerferenceUtility.UpdateUserPreference(settingCategory, hdnfield.Value + "Visibility", "true");
					}
					if (txtbox.Visible)
						txtbox.Text = String.Empty;
					if (dropdownlist.Visible)
					{
						dropdownlist.SelectedIndex = -1;
						txtbox1.Text = "-1";
					}
					if (listbox.Visible)
					{
						listbox.SelectedIndex = -1;
						txtbox1.Text = "-1";
					}
					if (datepanel.Visible)
					{
						var txtboxdate1 = (TextBox)searchParametersRepeater.Items[i].FindControl("txtboxdate1");
						var txtboxdate2 = (TextBox)searchParametersRepeater.Items[i].FindControl("txtboxdate2");
						var chkdate = (CheckBox)searchParametersRepeater.Items[i].FindControl("chkDate");

						txtboxdate1.Text = String.Empty;
						txtboxdate2.Text = String.Empty;
						chkdate.Checked = false;
					}
				}
			}
		}

		public static void SetAutoSearchOn(Repeater searchParametersRepeater, bool enabled)
		{
			for (var i = 0; i < searchParametersRepeater.Items.Count; i++)
			{
				var dropdownlist = (DropDownList)searchParametersRepeater.Items[i].FindControl("dropdownlist");
				if (dropdownlist != null && dropdownlist.Visible)
				{
					dropdownlist.AutoPostBack = enabled;
				}
			}
		}

		public static void ShowDebugTextBoxes(Repeater searchParametersRepeater, bool visible)
		{
			for (var i = 0; i < searchParametersRepeater.Items.Count; i++)
			{
				var txtbox1 = (TextBox)searchParametersRepeater.Items[i].FindControl("txtbox1");
				if (txtbox1 != null)
				{
					txtbox1.Visible = visible;
				}
			}
		}

		public static string CheckAndSetFieldValue(Repeater searchParameterRepeater, string field, string value, string settingcategory)
		{
			for (var i = 0; i < searchParameterRepeater.Items.Count; i++)
			{
				//var label = (Label)searchParameterRepeater.Items[i].FindControl("label");

				var hdnfield = (HiddenField)searchParameterRepeater.Items[i].FindControl("hdnfield");
				
				if (hdnfield != null)
				{
					if (hdnfield.Value.Equals(field))
					{
						var txtbox = (TextBox)searchParameterRepeater.Items[i].FindControl("txtbox");
						var txtbox1 = (TextBox)searchParameterRepeater.Items[i].FindControl("txtbox1");
						var dropdownlist = (DropDownList)searchParameterRepeater.Items[i].FindControl("dropdownlist");
						var listbox = (ListBox)searchParameterRepeater.Items[i].FindControl("listbox");
						var datepanel = (Panel)searchParameterRepeater.Items[i].FindControl("datepanel");

						//var txtboxdate1 = (TextBox)searchParameterRepeater.Items[i].FindControl("txtboxdate1");
						//var txtboxdate2 = (TextBox)searchParameterRepeater.Items[i].FindControl("txtboxdate2");
						//var chkdate = (CheckBox)searchParameterRepeater.Items[i].FindControl("chkDate");

						var oDateRange = (DateRangeControl)searchParameterRepeater.Items[i].FindControl("oDateRange");

						if (txtbox.Visible)
							txtbox.Text = value;
						else if (dropdownlist.Visible)
						{
							dropdownlist.SelectedIndex = UIHelper.GetDropDownSelectedIndex(dropdownlist, field, settingcategory);
							txtbox1.Text = dropdownlist.SelectedValue;
						   
						}
						else if (listbox.Visible)
						{
							listbox.ClearSelection();

							if (value.Contains("/"))
							{
								var indices = value.Split('/');
								var txtvalue = "";
								txtbox1.Text = String.Empty;

								for (var j = 0; j < indices.Length; j++)
								{
									foreach (ListItem item in listbox.Items)
									{
										if (item.Text.Equals(indices[j]))
										{
											item.Selected = true;
											txtvalue += item.Value;
											txtvalue += "/";
										}
									}
								}

								txtvalue.Remove(txtvalue.Length - 1);
								txtbox1.Text = txtvalue;
							}
							else
							{
								listbox.SelectedIndex = listbox.Items.IndexOf(
										listbox.Items.FindByText(value));
								txtbox1.Text = listbox.SelectedValue;
							}
						}
						else if (datepanel.Visible)
						{
							var dates = value.Split('&');
							oDateRange.SetDateValues(dates[0], dates[1]);
							oDateRange.Checked = Boolean.Parse(dates[2]);
						}
					}
				}
			}

			return string.Empty;
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

		private readonly PerferenceUtility _perferenceCommon = new PerferenceUtility();

		#endregion

	}
}