using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace Framework.PM.Common.BusinessDomain
{
	public enum ServerResponseCode
	{
		Success = 1,
		Unknown = 0,
		Error = -1,
		Warning = -2,
		Info = -3,
		InvalidCode = -4
	}

	[Serializable]
	public class ServerResponse
	{
		private List<ServerResponseItem> m_ResponseItems = new List<ServerResponseItem>();
		private bool m_IsSuccesful = true;
		private string m_Code = String.Empty;
		private string m_Description = String.Empty;
		private object m_ReturnValue = null;
		private ServerResponseCode m_ServerResponseCode = ServerResponseCode.Unknown;

		public ServerResponse()
		{

		}

		public ServerResponse(object returnValue)
		{
			m_ReturnValue = returnValue;
		}

		public ServerResponse(object returnValue, bool isSuccessful, string description) : this(returnValue)
		{
			m_IsSuccesful = IsSuccesful;
			m_Description = description;
		}

		public ServerResponse(SqlDataReader reader)
		{
			if (reader.FieldCount > 0)
			{
				object rv = reader.GetValue(0);

				ReturnValue = rv;

				int rc;

				if (Int32.TryParse(reader.GetValue(0).ToString(), out rc))
				{
					if (rc >= 0)
					{
						ResponseCode = ServerResponseCode.Success;
						IsSuccesful = true;
					}
					else
					{
						IsSuccesful = false;
						try
						{
							ResponseCode = (ServerResponseCode)rc;
						}
						catch (Exception)
						{
							ResponseCode = ServerResponseCode.InvalidCode;
						}
					}
				}

				if (reader.FieldCount > 2)
				{
					Code = reader.GetString(1);
					Description = reader.GetString(2);
				}
			}
		}

		public bool IsSuccesful
		{
			get { return m_IsSuccesful; }
			set { m_IsSuccesful = value; }
		}

		public object ReturnValue
		{
			get { return m_ReturnValue; }
			set { m_ReturnValue = value; }
		}

		public string Code
		{
			get { return m_Code; }
			set { m_Code = value; }
		}

		public string Description
		{
			get { return m_Description; }
			set { m_Description = value; }
		}

		public ServerResponseCode ResponseCode
		{
			get { return m_ServerResponseCode; }
			set { m_ServerResponseCode = value; }
		}

		public List<ServerResponseItem> ResponseItems
		{
			get { return m_ResponseItems; }
		}

		public string CommandName { get; set; }
	}

	[Serializable]
	public class ServerResponseItem
	{
		private int m_ErrorNumber = 0;
		private string m_Code = String.Empty;
		private string m_Description = String.Empty;

		public ServerResponseItem(string code)
		{
			m_Code = code;
		}

		public ServerResponseItem(string code, string description)
		{
			m_Code = code;
			m_Description = description;
		}

		public int ErrorNumber
		{
			get { return m_ErrorNumber; }
			set { m_ErrorNumber = value; }
		}

		public string Code
		{
			get { return m_Code; }
			set { m_Code = value; }
		}

		public string Description
		{
			get { return m_Description; }
			set { m_Description = value; }
		}


	}
}
