using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using DataModel.Framework.AuthenticationAndAuthorization;
using Framework.Components.ApplicationUser;
using Framework.Components.Core;
using Shared.WebCommon.UI.Web;
using System.Collections;
using System.Reflection;
using DataModel.Framework.Core;

namespace Shared.WebCommon.UI.Web
{
	public class InformationCommon
	{
		private static readonly Lazy<Dictionary<int, ApplicationDataModel>> applicationCache = new Lazy<Dictionary<int, ApplicationDataModel>>(ApplicationCommon.LoadApplicationDetails);

		public enum EmailType
		{
			Information = 0,
			Error = 1
		}
		
		public static class EmailHeaderMessage
		{

			public static string ErrorHeader = "Error Alert";
			public static string InformationHeader = "Information Alert";
		}

		public static string UniqueValue
		{
			get
			{
				return GetUniqueValueAsNumber();
			}
		}

		public static Dictionary<int, ApplicationDataModel> ApplicationCache
		{
			get
			{
				return applicationCache.Value;
			}
		}

		public static string GetUniqueValueAsNumber()
		{
			return DateTime.Now.ToString("yyyyMMddhhmmss");
		}

		public static string CurrentExecutionPath(string executionPath)
		{
			var paths = executionPath.Split('/');
			var data = new ApplicationRouteDataModel();
			data.RouteName = paths[1];
			var entityExists = ApplicationRouteDataManager.Search(data, SessionVariables.RequestProfile);
			
			if(entityExists.Rows.Count>0)
			{
				data.RouteName= paths[1] + "EntityRouteSearch";
				var dt = ApplicationRouteDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 0) return executionPath;

				var q = dt.Rows[0][ApplicationRouteDataModel.DataColumns.RelativeRoute];

				if ( q != null)
				{
					return q.ToString();
				}
				
				return executionPath;
			}

			return executionPath;
		}

		public static Dictionary<int, ApplicationDataModel> LoadApplicationDetails()
		{
			var cacheItems = new Dictionary<int, ApplicationDataModel>();
			var list = ApplicationDataManager.GetEntityDetails(ApplicationDataModel.Empty, SessionVariables.SystemRequestProfile);

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

		public static void SendSessionInfo(EmailType emailType, string uniqueTraceId)
		{
			var isEmailSendingEnabled = Boolean.Parse(ConfigurationManager.AppSettings["EnableErrorrSesssionAppInfo"].ToString());
			if (isEmailSendingEnabled)
			{
				var emailHeader = string.Empty;
				if (emailType == EmailType.Error)
				{
					emailHeader = EmailHeaderMessage.ErrorHeader;
				}
				else
				{
					emailHeader = EmailHeaderMessage.InformationHeader;
				}

				var strToEmail = ConfigurationManager.AppSettings["InformationEmail"];
				var nMail = new MailMessage(ConfigurationManager.AppSettings["fromEmail"], strToEmail);
				nMail.IsBodyHtml = true;
				nMail.Subject = InformationCommon.SubjectInfo(emailHeader, uniqueTraceId, "Session Info", null, "General");

				nMail.Body = InformationCommon.SessionObjects(emailType, uniqueTraceId);
				var sClient = new SmtpClient();
				sClient.Send(nMail);
			}
		}

		public static void SendApplicationInfo(EmailType emailType, string uniqueTraceId)
		{
			var isEmailSendingEnabled = Boolean.Parse(ConfigurationManager.AppSettings["EnableErrorrSesssionAppInfo"].ToString());
			if (isEmailSendingEnabled)
			{
				var emailHeader = string.Empty;
				if (emailType == EmailType.Error)
				{
					emailHeader = EmailHeaderMessage.ErrorHeader;
				}
				else
				{
					emailHeader = EmailHeaderMessage.InformationHeader;
				}

				var strToEmail = ConfigurationManager.AppSettings["InformationEmail"];
				var nMail = new MailMessage(ConfigurationManager.AppSettings["fromEmail"], strToEmail);
				nMail.IsBodyHtml = true;
				nMail.Subject = InformationCommon.SubjectInfo(emailHeader, uniqueTraceId, "Application Info", null, "General");

				nMail.Body = InformationCommon.ApplicationObjects();
				var sClient = new SmtpClient();
				sClient.Send(nMail);
			}
		}

		public static void SendSQLTraceInfo(string uniqueTraceId)
		{
			var isEmailSendingEnabled = Boolean.Parse(ConfigurationManager.AppSettings["EnableErrorrSesssionAppInfo"].ToString());
			if (isEmailSendingEnabled)
			{
				var emailHeader = EmailHeaderMessage.InformationHeader;
				var strToEmail = ConfigurationManager.AppSettings["InformationEmail"];
				var nMail = new MailMessage(ConfigurationManager.AppSettings["fromEmail"], strToEmail);
				nMail.IsBodyHtml = true;
				nMail.Subject = InformationCommon.SubjectInfo(emailHeader, uniqueTraceId, "SQL Trace", null, "General");

				nMail.Body = ApplicationCommon.HtmlSql(new Exception(HttpContext.Current.Application.ToString()));
				var sClient = new SmtpClient();
				sClient.Send(nMail);
			}
		}

		public static string ExpandSessionValue(string type, string key, string userName, string logonUserIdentity, ApplicationDataModel appName, EmailType emailType, string uniqueValue)
		{
			var emailHeader = string.Empty;
			if (emailType == EmailType.Error)
			{
				emailHeader = EmailHeaderMessage.ErrorHeader;
			}
			else
			{
				emailHeader = EmailHeaderMessage.InformationHeader;
			}
			var expandedvalue = new StringBuilder("");
			switch (type)
			{
				case "RequestProfile":
					{
						var reqProfile = (Framework.Components.DataAccess.RequestProfile)HttpContext.Current.Session[key];
						expandedvalue.AppendFormat("ApplicationId:" + reqProfile.ApplicationId + "; ApplicationModeId:" + reqProfile.ApplicationModeId + "; AuditId:" + reqProfile.AuditId).ToString();
						return expandedvalue.ToString();
					}
				case "DataTable":
					{
						var dt = (DataTable)HttpContext.Current.Session[key];
						var columnName = "";

						foreach (DataColumn dc in dt.Columns)
						{
							if (dc.Caption.Equals("Name") || dc.Caption.Equals("Menu"))
							{
								columnName = dc.Caption;
								break;
							}
							else
							{
								columnName = dt.Columns[2].Caption;
							}
						}

						var dv = new DataView(dt);
						dv.Sort = string.Format("{0} ASC", columnName);

						expandedvalue.Append("<table width=50 height=50 border=2 sort =ascending>");
						var count = dt.Rows.Count;
						expandedvalue.Append(count);
						var colorCount = 0;
						var firstRow = "#ADD8E6";
						var alternateRow = "white";

						var strToEmail = ConfigurationManager.AppSettings["InformationEmail"];
						var nMail = new MailMessage(ConfigurationManager.AppSettings["fromEmail"], strToEmail);
						nMail.IsBodyHtml = true;
						nMail.Subject = InformationCommon.SubjectInfo(emailHeader, uniqueValue, "Session Info - Table", key, "(RowCount: " + count + ")");
						//nMail.Subject = appName.Code + " - " + alert + "- " + uniqueValue + " - Session Info - Table - " + key + " - (RowCount: " + count + ")";

						expandedvalue.AppendFormat("<th> RowNumber </th> ");
						foreach (DataColumn dc in dv.Table.Columns)
						{
							expandedvalue.AppendFormat("<th>{0}</th> ", dc.Caption);
						}
						expandedvalue.Append("</tr>");

						foreach (DataRowView dr in dv)
						{
							var color = colorCount % 2 == 0 ? firstRow : alternateRow;
							expandedvalue.AppendFormat("<tr style= background-color:{0}>", color);
							var index = dv.Table.Rows.IndexOf(dr.Row);
							expandedvalue.AppendFormat("<td>{0}</td> ", index + 1);

							for (int i = 0; i < dr.Row.ItemArray.Length; i++)
							{
								if (!String.IsNullOrEmpty(dr.Row.ItemArray[i].ToString()))
								{
									expandedvalue.AppendFormat("<td>{0}</td>", dr.Row.ItemArray[i]);
								}
								else
								{
									expandedvalue.AppendFormat("<td>NULL</td>");
								}

							}

							expandedvalue.Append("</tr>");
							colorCount++;
						}

						expandedvalue.Append("</table>");

						nMail.Body = key + "- RowCount :" + expandedvalue;

						var sClient = new SmtpClient();
						sClient.Send(nMail);
						return "";
						//return expandedvalue.ToString();
					}

				case "List`1":
					{
                        var listObject = HttpContext.Current.Session[key] as IEnumerable;
                        if (listObject != null)
                        {
                            PropertyInfo[] elementProperty = null;

                            var colorCount = 0;
                            var firstRow = "#ADD8E6";
                            var alternateRow = "white";

                            var colmnHeaders = new StringBuilder();

                            var i = 0;
                            foreach (var item in listObject)
                            {

                                if (elementProperty == null)
                                {
                                    elementProperty = item.GetType().GetProperties();

                                    // build header row using column names
                                    colmnHeaders.AppendFormat("<th> RowNumber </th> ");
                                    foreach (var headProp in elementProperty)
                                    {
                                        colmnHeaders.AppendFormat("<th>{0}</th>", headProp.Name);
                                    }

                                    colmnHeaders.Append("</tr><br>");
                                }

                                var color = colorCount % 2 == 0 ? firstRow : alternateRow;
                                expandedvalue.AppendFormat("<tr style=background-color:{0}>", color);
                                var index = i + 1;
                                expandedvalue.AppendFormat("<td>{0}</td> ", index);
                                foreach (var prop in elementProperty)
                                {
                                    object value = prop.GetValue(item, null);

                                    if (!String.IsNullOrEmpty(value.ToString()))
                                    {
                                        expandedvalue.AppendFormat("<td>{0}</td>", value);
                                    }
                                    else
                                    {
                                        expandedvalue.Append("<td>NULL</td>");
                                    }

                                }

                                expandedvalue.Append("</tr>");
                                colorCount++;
                                i++;
                            }

                            // count
                            var count = i;

                            var bodyCount = new StringBuilder();

                            bodyCount.Append(count);
                            bodyCount.Append("<table width=50 height=50 border=1><tr>");

                            var strToEmail = ConfigurationManager.AppSettings["InformationEmail"];
                            var nMail = new MailMessage(ConfigurationManager.AppSettings["fromEmail"], strToEmail);
                            nMail.IsBodyHtml = true;
                            nMail.Subject = InformationCommon.SubjectInfo(emailHeader, uniqueValue, "Session Info - List", key, "(RowCount: " + count + ")");

                            //assemble body
                            nMail.Body = key + " - RowCount :" + bodyCount + colmnHeaders + expandedvalue;

                            var sClient = new SmtpClient();

                            sClient.Send(nMail);

                        }
                        return string.Empty;
					}
				default:
					{
						return HttpContext.Current.Session[key].ToString();
					}
			}
		}

		public static string SessionObjects(EmailType emailType,string uniqueValue)
		{

			var key = new StringBuilder("");
			var value = new StringBuilder("");
			var sessionObject = new StringBuilder("");
			var sessionObjectList = string.Empty;

			var logonUserIdentity = (HttpContext.Current.Request.LogonUserIdentity != null) ? HttpContext.Current.Request.LogonUserIdentity.Name : string.Empty;
			var dt = ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);
			var userName = "";
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				if ((int)(dt.Rows[i][ApplicationUserDataModel.DataColumns.ApplicationUserId]) == SessionVariables.RequestProfile.AuditId)
				{
					userName = dt.Rows[i][ApplicationUserDataModel.DataColumns.ApplicationUserName].ToString();
				}
			}

			var traceNode = "<b>UserName:</b> " + userName + " -" + logonUserIdentity;
			traceNode += "<br><br><b>Session Object List:</b>";
			sessionObject.Append(traceNode);
			var appId = SessionVariables.RequestProfile.ApplicationId;
			var appName = ApplicationCommon.ApplicationCache[appId];
			var list = new List<string>();

			foreach (var sessionkey1 in HttpContext.Current.Session.Keys)
			{
				list.Add(sessionkey1.ToString());
			}

			list.Sort();
			foreach (var sessionKey in list)
			{
				key.AppendFormat("{0}\n", sessionKey);
				var type = HttpContext.Current.Session[sessionKey].GetType().Name;
				var expandedvalue = ExpandSessionValue(type, sessionKey, userName, logonUserIdentity, appName, emailType, uniqueValue);
				value.AppendFormat("{0}\n", expandedvalue);

				if (!String.IsNullOrEmpty(expandedvalue))
				{
					sessionObjectList = String.Format(
						@"<table width='100%'>
					<tbody>
						<tr>
							<td>
								<code><pre><b>{0}</b>: {1}</pre></code>
							</td>
						</tr>
					</tbody>

				</table>", sessionKey, expandedvalue);
					sessionObject.Append(sessionObjectList);
				}

			}

			return sessionObject.ToString();
		}

		public static string ApplicationObjects()
		{
			var keys = new StringBuilder("");
			var value = new StringBuilder("");

			var applicationObjectList = string.Empty;
			var applicationInstance = HttpContext.Current.ApplicationInstance;

			foreach (var key in applicationInstance.Application.Keys)
			{
				keys.AppendFormat("{0}\n", key);
				value.AppendFormat("{0}\n", applicationInstance.Application[key.ToString()]);
			}
			var traceNode = "<b>Application Object List:</b>";

			applicationObjectList = traceNode + String.Format(
				@"<table width='100%' >
					<tbody>
					<tr>
						<th>
							Keys
						</th>
						<th>
							Value
						</th>
					</tr>
						<tr>
							<td>
								<code><pre>{0}</pre></code>
							</td>
							<td>
								<code><pre>{1}</pre></code>
							</td>
						</tr>
					</tbody>
				</table><br>", keys, value);


			return applicationObjectList;
		}

		public static string SubjectInfo(string alert, string uniqueTraceId, string sectionInfo, string section, string value)
		{
			var subjectValue = string.Empty;
			if (section != null)
			{
				subjectValue = ApplicationCache[SessionVariables.RequestProfile.ApplicationId].Code + " - " + alert + " - " + uniqueTraceId + " - " + sectionInfo + section + " - " + value;
			}
			else
			{
				subjectValue = ApplicationCache[SessionVariables.RequestProfile.ApplicationId].Code + " - " + alert + " - " + uniqueTraceId + " - " + sectionInfo + " - " + value;
			}

			return subjectValue;
		}
		
	}
}